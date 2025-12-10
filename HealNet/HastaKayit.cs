using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;


namespace HealNet
{
    public partial class HastaKayit : Form
    {


        public HastaKayit()
        {
            InitializeComponent();
        }


        private void HastaKayit_Load(object sender, EventArgs e)
        {
            // Satırların rengi (Okuması kolay olsun diye)
            dtgHastalar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgHastalar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Seçilen satırın rengi (Turkuaz/Mavi tonu)
            dtgHastalar.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dtgHastalar.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;

            // Başlık Rengi (Senin formun koyu mavisiyle uyumlu)
            dtgHastalar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 50);
            dtgHastalar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgHastalar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);

            // --- 3. GENEL DAVRANIŞ AYARLARI ---
            dtgHastalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Sığdır


            btnYenile.PerformClick();

        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            // Bağlantıyı merkezden al
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            if (string.IsNullOrEmpty(txtTC.Text) || string.IsNullOrEmpty(txtAd.Text) || string.IsNullOrEmpty(txtSoyad.Text) || string.IsNullOrEmpty(txtTelefon.Text) || string.IsNullOrEmpty(txtAd.Text) || string.IsNullOrEmpty(dateTimePicker1.Text) || string.IsNullOrEmpty(comboCinsiyet.Text) || string.IsNullOrEmpty(comboKanGrubu.Text))

            {
                MessageBox.Show("Lütfen zorunlu alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtKronik.Text))
            {
                txtKronik.Text = "Yok";
            }

            // Firebase'e soruyoruz: "Bu TC numarasıyla kayıtlı biri var mı?"
            var kontrolCevap = await baglanti.GetAsync("Hastalar/" + txtTC.Text);

            // Eğer gelen cevap "null" değilse, yani içeride veri varsa:
            if (kontrolCevap.Body != "null")
            {
                MessageBox.Show("Bu TC Kimlik Numarası ile zaten bir kayıt var! Aynı kişiyi tekrar ekleyemezsiniz.", "Çakışma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // İşlemi durdur, aşağıya inme.
            }

            Hasta yeniHasta = new Hasta();



            // Kisi sınıfından gelen özellikler
            yeniHasta.TC = txtTC.Text;
            yeniHasta.Ad = txtAd.Text;
            yeniHasta.Soyad = txtSoyad.Text;
            yeniHasta.Telefon = txtTelefon.Text;
            yeniHasta.Cinsiyet = comboCinsiyet.SelectedItem.ToString();
            yeniHasta.DogumTarihi = dateTimePicker1.Value.ToShortDateString();

            // Hasta sınıfından gelen özellikler
            yeniHasta.KanGrubu = comboKanGrubu.SelectedItem.ToString();
            yeniHasta.KronikRahatsizliklar = txtKronik.Text;



            var cevap = await baglanti.SetAsync("Hastalar/" + txtTC.Text, yeniHasta);

            MessageBox.Show("Hasta kaydedildi.");
            btnYenile.PerformClick();

            txtTC.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtTelefon.Clear();
            txtKronik.Clear();
            dateTimePicker1.Value = DateTime.Now;
            comboCinsiyet.SelectedIndex = -1;
            comboKanGrubu.SelectedIndex = -1;
        


        }


        private void btnGeri_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();
            anaEkran.Show();
            this.Hide();
        }



        private async void btnYenile_Click(object sender, EventArgs e)
        {
            // 1. Bağlantıyı merkezden al
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // 2. Firebase'den "Hastalar" klasörünü iste
            var cevap = await baglanti.GetAsync("Hastalar");


            // 4. Veriyi Sözlük (Dictionary) olarak çek
            // Key: TC Kimlik No, Value: Hasta Bilgileri
            var gelenVeriler = cevap.ResultAs<Dictionary<string, Hasta>>();

            // 5. Sözlüğün sadece "Değerler" kısmını alıp listeye çevir
            var hastaListesi = gelenVeriler.Values.ToList();

            // 6. Tabloya (DataGridView) bağla
            dtgHastalar.DataSource = hastaListesi;
            dtgHastalar.Columns["Ad"].DisplayIndex = 0;
            dtgHastalar.Columns["Soyad"].DisplayIndex = 1;
            dtgHastalar.Columns["TC"].DisplayIndex = 2;

        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            // 1. Önce TC kutusu dolu mu diye bak (Kimse seçili değilse silme yapamayız)
            if (string.IsNullOrEmpty(txtTC.Text))
            {
                MessageBox.Show("Lütfen silinecek hastayı listeden seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kullanıcıya "Emin misin?" diye sor
            var secim = MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " isimli hastayı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // 3. Eğer "Evet" derse sil
            if (secim == DialogResult.Yes)
            {
                var baglanti = FirebaseBaglantisi.BaglantiGetir();

                // Firebase'den silme komutu: DeleteAsync
                await baglanti.DeleteAsync("Hastalar/" + txtTC.Text);

                MessageBox.Show("Hasta kaydı başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 4. Temizlik İşlemleri
                // Kutuları boşalt
                txtTC.Clear(); txtAd.Clear(); txtSoyad.Clear(); txtTelefon.Clear(); txtKronik.Clear();

                // Listeyi yenile (Senin harika taktiğinle)
                btnYenile.PerformClick();

            }
        }

        private void dtgHastalar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Başlık satırına tıklarsa hata vermesin
            if (e.RowIndex >= 0)
            {
                // Tıklanan satırı al
                var satir = dtgHastalar.Rows[e.RowIndex];

                // Kutuları doldur (Hücre isimleri Class'taki property isimleriyle aynıdır)
                txtTC.Text = satir.Cells["TC"].Value.ToString();
                txtAd.Text = satir.Cells["Ad"].Value.ToString();
                txtSoyad.Text = satir.Cells["Soyad"].Value.ToString();
                txtTelefon.Text = satir.Cells["Telefon"].Value.ToString();

                // Tarih seçiciyi ayarla
                dateTimePicker1.Value = Convert.ToDateTime(satir.Cells["DogumTarihi"].Value);

                // Comboboxları seç
                comboCinsiyet.Text = satir.Cells["Cinsiyet"].Value.ToString();
                comboKanGrubu.Text = satir.Cells["KanGrubu"].Value.ToString();

                // Kronik hastalık null gelebilir, kontrol edelim
                if (satir.Cells["KronikRahatsizliklar"].Value != null)
                {
                    txtKronik.Text = satir.Cells["KronikRahatsizliklar"].Value.ToString();
                }
            }
        }
    }
}

