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
        // 1. AYARLAR KISMI (Eski adı: config)
        IFirebaseConfig firebaseAyarlari = new FirebaseConfig
        {
            // Gizli şifren buraya
            AuthSecret = "USoiJDXJiPetiw5QjaRJbmnaVDHXNvJmqGPxsgdC",

            // Linkin buraya (Sonunda / olmadan)
            BasePath = "https://healnet-56cd7-default-rtdb.europe-west1.firebasedatabase.app"
        };

        // 2. BAĞLANTI NESNESİ
        IFirebaseClient baglanti;



        // FORM AÇILINCA ÇALIŞAN KISIM
        public Giris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 1. Santralden hattı çekiyoruz
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // 2. Kontrol edip mesajı veriyoruz (Senin istediğin gibi)
            if (baglanti != null)
            {
                MessageBox.Show("Firebase Bağlantısı Başarılı!");
            }
            else
            {
                MessageBox.Show("Bağlantı Hatası! İnterneti kontrol et.");
            }
        }





        private async void button1_Click(object sender, EventArgs e)
        {
            // Butona basınca tekrar hattı çekiyoruz
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            if (checkBoxAdmin.Checked == false)
            {
                MessageBox.Show("Lütfen kullanıcı seçiniz .");
                return; // Kod burada durur, aşağıya inmez.
            }



            try
            {
                // Veriyi çek
                var kullaniciData = await baglanti.GetAsync("Kullanicilar/admin/KullaniciAdi");

                if (kullaniciData.Body == "null")
                {
                    MessageBox.Show("Kullanıcı Bulunamadı!");
                    return;
                }

                string kullaniciDb = kullaniciData.ResultAs<string>();

                var sifreData = await baglanti.GetAsync("Kullanicilar/admin/Sifre");
                string sifreDb = sifreData.ResultAs<string>();

                if (kullaniciDb == txtKullaniciAdi.Text && sifreDb == txtSifre.Text)
                {
                    MessageBox.Show("Giriş Yapıldı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    // Ana Ekranı aç
                    AnaEkran anaEkran = new AnaEkran();
                    anaEkran.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Hatalı Şifre", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata oluştu: " + hata.Message);
            }
        }







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
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(0, 91, 181);
            button1.Invalidate(); // yeniden çiz
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.FromArgb(0, 123, 255);
            button1.Invalidate();
        }

        private void checkBoxSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            txtSifre.UseSystemPasswordChar = checkBoxSifreGoster.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();
            anaEkran.Show();
            this.Hide();
        }
    }
}

