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
    public partial class DoktorYönetim : Form
    {

        // Arama yaparken internete gitmeyelim diye verileri burada tutuyoruz.
        List<Doktor> hafizadakiDoktorlar = new List<Doktor>();


        public DoktorYönetim()
        {
            InitializeComponent();
        }

        private void DoktorYönetim_Load(object sender, EventArgs e)
        {
            // Tablo (DataGridView) Görsel Ayarları
            dtgDoktorlar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgDoktorlar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgDoktorlar.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dtgDoktorlar.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dtgDoktorlar.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 0, 50);
            dtgDoktorlar.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dtgDoktorlar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dtgDoktorlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Form açılır açılmaz veriler ekrana gelicek 
            btnYenile.PerformClick();
        }

        private async void btnYenile_Click(object sender, EventArgs e)
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // Firebase'den verileri iste
            var cevap = await baglanti.GetAsync("Doktorlar");

            if (cevap.Body != "null")
            {

                // Firebase'den gelen verileri tanımladık
                var gelenVeriler = cevap.ResultAs<Dictionary<string, Doktor>>();

                // 1. Tanımlanan verileri hafızaya aldık
                hafizadakiDoktorlar = gelenVeriler.Values.ToList();

                // 2. Tabloyu doldurduk bu hafızadaki verilerle
                dtgDoktorlar.DataSource = hafizadakiDoktorlar;

                // Sütun sırasını ayarla
                dtgDoktorlar.Columns["Ad"].DisplayIndex = 0;
                dtgDoktorlar.Columns["Soyad"].DisplayIndex = 1;
                dtgDoktorlar.Columns["TC"].DisplayIndex = 2;
                dtgDoktorlar.Columns["Unvan"].DisplayIndex = 3;
                dtgDoktorlar.Columns["Brans"].DisplayIndex = 4;
            }
            else
            {
                dtgDoktorlar.DataSource = null;
                hafizadakiDoktorlar.Clear(); // Eğer silinmezse eski veriler kalır
            }
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // A) Zorunlu Alan Kontrolü
            if (string.IsNullOrEmpty(txtTC.Text) || string.IsNullOrEmpty(txtAd.Text) ||
                string.IsNullOrEmpty(txtSoyad.Text) || string.IsNullOrEmpty(txtTelefon.Text) ||
                string.IsNullOrEmpty(comboCinsiyet.Text) || string.IsNullOrEmpty(comboBrans.Text) ||
                string.IsNullOrEmpty(comboUnvan.Text))
            {
                MessageBox.Show("Lütfen zorunlu alanları doldurun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // B) Çakışma Kontrolü
            var kontrolCevap = await baglanti.GetAsync("Doktorlar/" + txtTC.Text);
            if (kontrolCevap.Body != "null")
            {
                MessageBox.Show("Bu TC ile kayıt zaten var.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            // C) Nesne Oluşturma
            Doktor yeniDoktor = new Doktor();
            yeniDoktor.TC = txtTC.Text;
            yeniDoktor.Ad = txtAd.Text;
            yeniDoktor.Soyad = txtSoyad.Text;
            yeniDoktor.Telefon = txtTelefon.Text;
            yeniDoktor.Cinsiyet = comboCinsiyet.SelectedItem.ToString();
            yeniDoktor.Brans = comboBrans.Text;
            yeniDoktor.Unvan = comboUnvan.Text;

            // Çalışma saatini de ekleyelim
            yeniDoktor.CalismaSaatleri = txtCalismaSaatleri.Text;

            // Deneyim Yılı (Sayıya Çevirme)
            int deneyim = 0;
            int.TryParse(txtDeneyim.Text, out deneyim);
            yeniDoktor.DeneyimYili = deneyim;

            // D) Firebase'e Gönder
            await baglanti.SetAsync("Doktorlar/" + txtTC.Text, yeniDoktor);

            // E) Bilgi Ver
            double ucret = yeniDoktor.MuayeneUcretiHesapla();
            MessageBox.Show($"Doktor Kaydedildi.");

            // F) Temizle
            btnYenile.PerformClick();
            AlanlariTemizle();


        }

        // Ekrandaki kutuları temizleyen ve sıfırlayan metot
        private void AlanlariTemizle()
        {
            txtTC.Clear();
            txtAd.Clear();
            txtSoyad.Clear();
            txtTelefon.Clear();
            txtDeneyim.Clear();
            txtCalismaSaatleri.Clear();

            comboCinsiyet.SelectedIndex = -1;
            comboBrans.SelectedIndex = -1;
            comboUnvan.SelectedIndex = -1;

            dtgDoktorlar.ClearSelection();
            txtTC.Focus();
        }

        private void btnTemizle_Click_1(object sender, EventArgs e)
        {
            AlanlariTemizle();
        }

        private void comboBrans_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // Eğer kullanıcı seçimi kaldırdıysa (Temizle'ye bastıysa)
            if (comboBrans.SelectedIndex == -1)
            {
                txtCalismaSaatleri.Clear(); // Saati temizle
                return; // Ve aşağıya inme, metodu burada bitir.
            }



            switch (comboBrans.Text)
            {
                case "Dahiliye":
                case "Kulak, Burun, Boğaz (KBB)":
                case "Endokrinoloji":
                case "Gastroenteroloji":
                case "Fizik Tedavi ve Rehabilitasyon":
                    txtCalismaSaatleri.Text = "09:00 - 17:00";
                    break;

                case "Göğüs Hastalıkları":
                case "Ortopedi":
                case "Genel Cerrahi":
                    txtCalismaSaatleri.Text = "08:00 - 16:00";
                    break;

                case "Nöroloji":
                case "Hematoloji":
                    txtCalismaSaatleri.Text = "09:30 - 17:30";
                    break;

                case "Dermatoloji":
                case "Psikiyatri":
                    txtCalismaSaatleri.Text = "10:00 - 18:00";
                    break;

                case "Üroloji":
                case "Kardiyoloji":
                    txtCalismaSaatleri.Text = "08:30 - 16:30";
                    break;

                case "Aile Hekimliği":
                    txtCalismaSaatleri.Text = "08:30 - 17:00";
                    break;

                default:
                    txtCalismaSaatleri.Text = "09:00 - 17:00";
                    break;
            }
        }






        private void btnGeri_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();

            anaEkran.Show();
            this.Hide();
        }




        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTC.Text)) // Kullanıcı hiçbir doktor seçmemişse zaten TC kutusu boş olur yani kontrolü oradan yapıyoruz.
            {
                MessageBox.Show("Lütfen silinecek hastayı listeden seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Seçilmezse işlemi sonlandır, ilerlemesin
            }

            var secim = MessageBox.Show(txtAd.Text + " " + txtSoyad.Text + " kişisini silmek istiyor musunuz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secim == DialogResult.Yes)  // MessageBoxButtons.YesNo ile çalışan bir komuttur. Evet mi denmiş diye kontrol ediyoruz.
            {
                var baglanti = FirebaseBaglantisi.BaglantiGetir();

                await baglanti.DeleteAsync("Doktorlar/" + txtTC.Text); // Silme işlemi TC numarasına göre yapılır.

                MessageBox.Show("Kayıt silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnYenile.PerformClick(); // Listeyi güncelle
                AlanlariTemizle();        // Kutuları temizle
            }
        }

        private void dtgDoktorlar_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // 0'dan büyükse (başlık satırına tıklanmadıysa) bir satıra tıklanmıştır zaten
            {
                var satir = dtgDoktorlar.Rows[e.RowIndex];

                txtTC.Text = satir.Cells["TC"].Value.ToString();
                txtAd.Text = satir.Cells["Ad"].Value.ToString();
                txtSoyad.Text = satir.Cells["Soyad"].Value.ToString();
                txtTelefon.Text = satir.Cells["Telefon"].Value.ToString();
                comboCinsiyet.Text = satir.Cells["Cinsiyet"].Value.ToString();
                comboBrans.Text = satir.Cells["Brans"].Value.ToString();
                comboUnvan.Text = satir.Cells["Unvan"].Value.ToString();
                txtDeneyim.Text = satir.Cells["DeneyimYili"].Value.ToString();
                txtCalismaSaatleri.Text = satir.Cells["CalismaSaatleri"].Value.ToString();

            }
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text.ToLower();

            var filtrelenenListe = hafizadakiDoktorlar
                .Where(x => x.Ad.ToLower().Contains(aranan) || x.Soyad.ToLower().Contains(aranan) ||  x.TC.Contains(aranan)) .ToList();

            dtgDoktorlar.DataSource = filtrelenenListe;
        }
    }
}