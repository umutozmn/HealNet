using FireSharp.Config;     // Ayarlar için
using FireSharp.Interfaces; // Arayüzler için
using FireSharp.Response;   // Cevaplar için
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

namespace HealNet
{
    public partial class Giris : Form
    {

        // FORM AÇILINCA ÇALIŞAN KISIM
        public Giris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // FireabaseBaglantisi adlı sınıftaki BaglantiGetir metodu sayesinde baglantıyı alıyoruz
            var baglanti = FirebaseBaglantisi.BaglantiGetir();


            if (baglanti != null) // Eğer bağlantı boş değilse 
            {
            }
            else
            {
                MessageBox.Show("Bağlantı Hatası! İnterneti kontrol et.");
            }
        }





        private async void button1_Click(object sender, EventArgs e) // GİRİŞ BUTONU
        {
            // Butona basınca tekrar hattı çekiyoruz
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            if (checkBoxAdmin.Checked == false)
            {
                MessageBox.Show("Lütfen giriş türünü (Admin) seçiniz.");
                return; // Kod burada durur, aşağıya inmez.
            }

            else if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text)) // Eğer kullanıcı adı boş bırakılmışsa
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifreyi giriniz.");
                return;
            }

           

            if (checkBoxAdmin.Checked == true)
            {
                // Admin girişi yapıyoruz

                var kullaniciAdiData = await baglanti.GetAsync("Kullanicilar/admin/KullaniciAdi"); // Veritabanından Kullanicilar/admin/KullaniciAdi klasörü içindeki verileri al
                var sifreData = await baglanti.GetAsync("Kullanicilar/admin/Sifre");   // Veritabanından Kullanicilar/admin/Sifre klasörü içindeki verileri al

                if (kullaniciAdiData.Body == "null") // Eğer kullanıcı adı verisi yoksa firebasede
                {
                    MessageBox.Show("Admin kaydı bulunamadı");
                    return;
                }

                string kullaniciAdiDb = kullaniciAdiData.ResultAs<string>(); // Alınan sonucu stringe çevir
                string sifreDb = sifreData.ResultAs<string>(); // Alınan sonucu stringe çevir

                if (kullaniciAdiDb == txtKullaniciAdi.Text && sifreDb == txtSifre.Text)
                {
                    MessageBox.Show("Ana Ekrana Yönlendiriliyorsunuz", "Giriş Yapıldı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    AnaEkran anaEkran = new AnaEkran();
                    anaEkran.Show();
                    this.Hide();
                }

                else
                {
                    MessageBox.Show("Lütfen verilerinizi kontrol ediniz", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }

        }

        private void checkBoxSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            txtSifre.UseSystemPasswordChar = checkBoxSifreGoster.Checked;
        }



        // GÖRSELLİK AYARLARI
        private void Form1_Paint_1(object sender, PaintEventArgs e)
        {

            LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(0, 82, 212),   // koyu mavi
                Color.FromArgb(58, 141, 255), // açık mavi
                LinearGradientMode.Vertical
            );

            e.Graphics.FillRectangle(brush, this.ClientRectangle);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            int radius = 20;

            Rectangle rect = panel1.ClientRectangle;
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();

            panel1.Region = new Region(path);
        }
        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;

            // Yuvarlak köşe yarıçapı
            int radius = 20;
            Rectangle rect = btn.ClientRectangle;

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();

            // Arka plan doldurma
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 123, 255)))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(brush, path);
            }

            // Yazı
            TextRenderer.DrawText(e.Graphics,
                btn.Text,
                btn.Font,
                rect,
                Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }





    }
}

