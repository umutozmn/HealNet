using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealNet
{
    public partial class btnTemizle : Form
    {
        // ============================================================
        // 1. BÖLÜM: DEĞİŞKENLER VE TANIMLAMALAR
        // ============================================================

        // Arama yaparken internete gitmeyelim diye verileri burada tutuyoruz.
        List<Hasta> hafizadakiHastalar = new List<Hasta>();

        public btnTemizle()
        {
            InitializeComponent();
        }

        // ============================================================
        // 2. BÖLÜM: FORM YÜKLENME VE AYARLAR
        // ============================================================

        private void HastaKayit_Load(object sender, EventArgs e)
        {
            // Tablo (DataGridView) Görsel Ayarları
            dtgHastalar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgHastalar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgHastalar.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dtgHastalar.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtgHastalar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 50);
            dtgHastalar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgHastalar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dtgHastalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Form açılır açılmaz veriler ekrana gelicek 
            btnYenile.PerformClick();
        }

        // ============================================================
        // 3. BÖLÜM: YARDIMCI METOTLAR (ROBOTLAR)
        // ============================================================

        // Ekrandaki kutuları temizleyen ve sıfırlayan metot
        private void AlanlariTemizle()
        {
            txtTC.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtTelefon.Clear();
            txtKronik.Clear();

            dateTimePicker1.Value = DateTime.Now;
            comboCinsiyet.SelectedIndex = -1;
            comboKanGrubu.SelectedIndex = -1;

            dtgHastalar.ClearSelection(); // Mavi seçimi kaldır
            txtTC.Focus(); // İmleci başa al
        }


       
        // 4. BÖLÜM: VERİ ÇEKME (READ) İŞLEMLERİ
       

        private async void btnYenile_Click(object sender, EventArgs e)
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // Firebase'den verileri iste
            var cevap = await baglanti.GetAsync("Hastalar");

            if (cevap.Body != "null")
            {

                // Firebase'den gelen verileri tanımladık
                var gelenVeriler = cevap.ResultAs<Dictionary<string, Hasta>>();

                // 1. Tanımlanan verileri hafızaya aldık
                hafizadakiHastalar = gelenVeriler.Values.ToList();

                // 2. Tabloyu doldurduk bu hafızadaki verilerle
                dtgHastalar.DataSource = hafizadakiHastalar;

                // Sütun sırasını ayarla
                dtgHastalar.Columns["Ad"].DisplayIndex = 0;
                dtgHastalar.Columns["Soyad"].DisplayIndex = 1;
                dtgHastalar.Columns["TC"].DisplayIndex = 2;
            }
            else
            {
                dtgHastalar.DataSource = null;
                hafizadakiHastalar.Clear(); // Eğer silinmezse eski veriler kalır
            }
        }

        // ============================================================
        // 5. BÖLÜM: VERİ KAYDETME VE SİLME (CREATE / DELETE)
        // ============================================================

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // Kullanıcı zorunlu alanları girmek zorunda
            if (string.IsNullOrEmpty(txtTC.Text) || string.IsNullOrEmpty(txtAd.Text) ||
                string.IsNullOrEmpty(txtSoyad.Text) || string.IsNullOrEmpty(txtTelefon.Text) ||
                string.IsNullOrEmpty(comboCinsiyet.Text) || string.IsNullOrEmpty(comboKanGrubu.Text))
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Bu zorunlu alanlar doldurulmazsa işlemi sonlandır, ilerlemesin
            }


            // B) Kronik Hastalık Boşsa Otomatik "Yok" Yaz
            if (string.IsNullOrEmpty(txtKronik.Text))
            {
                txtKronik.Text = "Yok";
            }


            // C) Aynı TC var mı kontrolü
            var kontrolCevap = await baglanti.GetAsync("Hastalar/" + txtTC.Text);
            if (kontrolCevap.Body != "null")
            {
                MessageBox.Show("Bu TC numarasıyla sisteme daha önce kayıt yapılmış. Lütfen kontrol ediniz.", "Çakışma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // Aynı TC ile kayıt yapılmaya çalışılırsa işlemi sonlandır, ilerlemesin
            }

            // D) Nesne Oluşturma ve Veri Doldurma
            Hasta yeniHasta = new Hasta();

            // Kisi sınıfından gelenler
            yeniHasta.TC = txtTC.Text;
            yeniHasta.Ad = txtAd.Text;
            yeniHasta.Soyad = txtSoyad.Text;
            yeniHasta.Telefon = txtTelefon.Text;
            yeniHasta.Cinsiyet = comboCinsiyet.SelectedItem.ToString();
            yeniHasta.DogumTarihi = dateTimePicker1.Value.ToShortDateString();

            // Hasta sınıfından gelenler
            yeniHasta.KanGrubu = comboKanGrubu.SelectedItem.ToString();
            yeniHasta.KronikRahatsizliklar = txtKronik.Text;

            // E) Firebase'e Gönder
            await baglanti.SetAsync("Hastalar/" + txtTC.Text, yeniHasta); // Hastalar klasörü altında TC klasörü açılır ve altına yeniHasta kayıtları gelir

            MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " adlı hasta kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // F) Temizlik ve Yenileme
            btnYenile.PerformClick(); // Yenile butonunda yaptığımız işlemi çalıştırarak listeyi günceller
            AlanlariTemizle();        // Kutuları temizle
        }



        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTC.Text)) // Kullanıcı hiçbir hasta seçmemişse zaten TC kutusu boş olur yani kontrolü oradan yapıyoruz.
            {
                MessageBox.Show("Lütfen silinecek hastayı listeden seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Seçilmezse işlemi sonlandır, ilerlemesin
            }

            var secim = MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " kişisini silmek istiyor musunuz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (secim == DialogResult.Yes)  // MessageBoxButtons.YesNo ile çalışan bir komuttur. Evet mi denmiş diye kontrol ediyoruz.
            {
                var baglanti = FirebaseBaglantisi.BaglantiGetir(); 

                await baglanti.DeleteAsync("Hastalar/" + txtTC.Text); // Silme işlemi TC numarasına göre yapılır.

                MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " adlı kayıt silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnYenile.PerformClick(); // Listeyi güncelle
                AlanlariTemizle();        // Kutuları temizle
            }
        }


              

        // Tablodan birine tıklayınca kutuları doldur
        private void dtgHastalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // 0'dan büyükse (başlık satırına tıklanmadıysa) bir satıra tıklanmıştır zaten
            {
                var satir = dtgHastalar.Rows[e.RowIndex];

                txtTC.Text = satir.Cells["TC"].Value.ToString();
                txtAd.Text = satir.Cells["Ad"].Value.ToString();
                txtSoyad.Text = satir.Cells["Soyad"].Value.ToString();
                txtTelefon.Text = satir.Cells["Telefon"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(satir.Cells["DogumTarihi"].Value);
                comboCinsiyet.Text = satir.Cells["Cinsiyet"].Value.ToString();
                comboKanGrubu.Text = satir.Cells["KanGrubu"].Value.ToString();

                if (satir.Cells["KronikRahatsizliklar"].Value != null)
                {
                    txtKronik.Text = satir.Cells["KronikRahatsizliklar"].Value.ToString();
                }
            }
        }



        // Canlı Arama (Yazarken filtrele)
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text.ToLower();

            var filtrelenenListe = hafizadakiHastalar
                .Where(x => x.Ad.ToLower().Contains(aranan) ||
                            x.Soyad.ToLower().Contains(aranan) ||
                            x.TC.Contains(aranan))
                .ToList();

            dtgHastalar.DataSource = filtrelenenListe;
        }


        private void button1_Click(object sender, EventArgs e) // Temizle Butonu bu
        {
            AlanlariTemizle();
        }


        // Geri Dön Butonu
        private void btnGeri_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();
            anaEkran.Show();
            this.Hide();
        }

      
    }
}