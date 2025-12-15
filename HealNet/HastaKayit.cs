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
    public partial class HastaKayit : Form
    {

        // Arama yaparken internete gitmeyelim diye verileri burada tutuyoruz.
        List<Hasta> hafizadakiHastalar = new List<Hasta>();

        public HastaKayit()
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
            txtTC.Focus(); // Her zaman Tc kutusuna odaklanır
        }


       
       
        private async void btnYenile_Click(object sender, EventArgs e)
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir(); // BaglantıGetir metodunu bir nesnede tutmak gerek ki sonra kullanabilelim

            // Firebase'den verileri iste
            var hastaData = await baglanti.GetAsync("Hastalar"); // Hastalar klasöründeki tüm verileri çek

            if (hastaData.Body != "null") 
            {

                // Verileri string olarak alıyoruz
                var hastaDataString = hastaData.ResultAs<Dictionary<string, Hasta>>();

                // String verileri listeye çeviriyoruz 
                hafizadakiHastalar = hastaDataString.Values.ToList();

                // 2. Tabloyu doldurduk bu hafızadaki verilerle
                dtgHastalar.DataSource = hafizadakiHastalar;

                // Sütun sırasını ayarla
                dtgHastalar.Columns["Ad"].DisplayIndex = 0;
                dtgHastalar.Columns["Soyad"].DisplayIndex = 1;
                dtgHastalar.Columns["TC"].DisplayIndex = 2;
            }
            else
            {
                dtgHastalar.DataSource = null; // Eğer silme işlemi sonrası liste boş kalırsa, dtgHastalar'ın DataSource'u null olur. Ama biz hala hafızadakiHastalar listesini tutuyor olabiliriz. Bu durumda liste boş olsa bile eski veriler ekranda kalır. O yüzden DataSource'u null yapıyoruz ki ekranda da boş görünsün.
                hafizadakiHastalar.Clear(); // Veriler tutulduğu yerden de silinmeli zira sadece görüntüde silmiş oluruz. 
            }
        }

        //  VERİ KAYDETME VE SİLME

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir(); // BaglantıGetir metodunu bir nesnede tutmak gerek ki sonra kullanabilelim

            // Hasta kayıt yaparken tc numarası, ad, soyad, telefon, cinsiyet ve kan grubu zorunlu alanlardır. Buralar null kalırsa kayıt yapılmasın.
            if (string.IsNullOrEmpty(txtTC.Text) || string.IsNullOrEmpty(txtAd.Text) ||
                string.IsNullOrEmpty(txtSoyad.Text) || string.IsNullOrEmpty(txtTelefon.Text) ||
                string.IsNullOrEmpty(comboCinsiyet.Text) || string.IsNullOrEmpty(comboKanGrubu.Text))
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Bu zorunlu alanlar doldurulmazsa işlemi sonlandır, ilerlemesin
            }


            // Kronik Hastalık Boşsa Otomatik "Yok" Yaz
            if (string.IsNullOrEmpty(txtKronik.Text))
            {
                txtKronik.Text = "Yok";
            }


            //  Aynı TC var mı kontrolü
            var tcKontrol = await baglanti.GetAsync("Hastalar/" + txtTC.Text); // Girilen TC numarasına sahip başka hasta var mı diye kontrol edecek tcKontrol nesnesi
           
            if (tcKontrol.Body != "null")
            {
                MessageBox.Show("Bu TC numarasıyla sisteme daha önce kayıt yapılmış. Lütfen kontrol ediniz.", "Çakışma Hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // Aynı TC ile kayıt yapılmaya çalışılırsa işlemi sonlandır, ilerlemesin
            }

            // Veri doldurmak için nesne gerekiyor
            Hasta yeniHasta = new Hasta();

            // Kisi sınıfından gelenler
            yeniHasta.TC = txtTC.Text;
            yeniHasta.Ad = txtAd.Text;
            yeniHasta.Soyad = txtSoyad.Text;
            yeniHasta.Telefon = txtTelefon.Text;
            yeniHasta.Cinsiyet = comboCinsiyet.SelectedItem.ToString();

            // Hasta sınıfından gelenler
            yeniHasta.KanGrubu = comboKanGrubu.SelectedItem.ToString();
            yeniHasta.KronikRahatsizliklar = txtKronik.Text;
            yeniHasta.DogumTarihi = dateTimePicker1.Value.ToShortDateString();


            //  Firebase'e Gönder
            await baglanti.SetAsync("Hastalar/" + txtTC.Text, yeniHasta); // Hastalar klasörü altında TC klasörü açılır ve altına yeniHasta kayıtları gelir

            MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " adlı hasta kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // F) Temizlik ve Yenileme
            btnYenile.PerformClick(); // Yenile butonunda yaptığımız işlemi çalıştırarak listeyi günceller
            AlanlariTemizle();        // Kutuları temizle
        }



        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTC.Text)) // Kullanıcı hiçbir hasta seçmemişse zaten TC kutusu boş olacağından kontrolü oradan yapıyoruz.
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

          
            // CellClick eventi tetiklendiğinde, bu evente ait DataGridViewCellEventArgs türünde bir nesne olarak gelir.
            // Bu nesneye genellikle "e" adı verilir ve tıklanan hücrenin satır ve sütun indeksleri gibi bilgileri içerir.
            // Yani "e" nesnesi, kullanıcının hangi hücreye tıkladığını bize bildirir.
            if (e.RowIndex >= 0) 
            {
                var satir = dtgHastalar.Rows[e.RowIndex]; //  Burada Rows bir koleksiyondur. Satır kodlarını içerir. 

                txtTC.Text = satir.Cells["TC"].Value.ToString(); // "TC" isimli hücrenin (cell) değeri null değilse (yani bir değeri varsa) o değeri alıyoruz ve stringe çevirip txtTC kutusuna yazıyoruz. Değer dediği şey , o hücredeki veridir.
                txtAd.Text = satir.Cells["Ad"].Value.ToString();
                txtSoyad.Text = satir.Cells["Soyad"].Value.ToString();
                txtTelefon.Text = satir.Cells["Telefon"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(satir.Cells["DogumTarihi"].Value); 
                comboCinsiyet.Text = satir.Cells["Cinsiyet"].Value.ToString();
                comboKanGrubu.Text = satir.Cells["KanGrubu"].Value.ToString();
                txtKronik.Text = satir.Cells["KronikRahatsizliklar"].Value.ToString();

            }
        }


        // ARAMA İŞLEMLERİ
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text.ToLower(); 

            List<Hasta> arananListe = new List<Hasta>();  // Sonuçları buna koyacağız 

            foreach (Hasta h in hafizadakiHastalar)
            {
                if (h.Ad.ToLower().Contains(aranan) || h.Soyad.ToLower().Contains(aranan) || h.TC.Contains(aranan)) // Hastanın adı, soyadı veya TC'si aranan metni içeriyorsa
                {
                    arananListe.Add(h);
                }
            }

            dtgHastalar.DataSource = arananListe;
        }


        


        // Geri Dön Butonu
        private void btnGeri_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();
            anaEkran.Show();
            this.Hide();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            AlanlariTemizle();

        }
    }
}