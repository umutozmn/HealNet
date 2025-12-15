using FireSharp.Config;
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

namespace HealNet
{
    public partial class AnaEkran : Form
    {
        public AnaEkran()
        {
            InitializeComponent();
        }


        private async void AnaEkran_Load(object sender, EventArgs e) // Ana ekran yüklendiğinde çalışacak kodlar. Async yapıyoruz çünkü Firebase'den veri çekeceğiz. Async açılımı "asynchronous" yani "eşzamansız" demek. Yani işlemler arka planda çalışacak ve uygulama donmayacak.
        {
          
            // Bağlantıyı al
            var baglanti = FirebaseBaglantisi.BaglantiGetir(); // Bağlantıyı getirip buna baglanti diyoruz 

            // 1. HASTA SAYISINI ALMA 
            var hastaVeri = await baglanti.GetAsync("Hastalar"); // Firebase'den "Hastalar" verilerini çekiyoruz. await ile bekliyoruz çünkü bu işlem zaman alabilir.

            // Eğer "null" değilse sayıyı al, yoksa 0 yaz
            if (hastaVeri.Body != "null") // 
            {
                // Veriyi sözlük olarak alıyoruz
                var hastaVeriString = hastaVeri.ResultAs<Dictionary<string, Hasta>>(); // Burada stringe çeviriyoruz veriyi çünkü Firebaseden birçok türde veri gelebilir
                lblHastaSayisi.Text = "Toplam Hasta: "+hastaVeriString.Count.ToString(); // Stringe çevrilen değeri sayıyor ve labela yazdırıyoruz
            }
            else
            {
                lblHastaSayisi.Text = "0";
            }



            // -------------------------------------------------------
            // 2. DOKTOR SAYISINI ALMA
            // -------------------------------------------------------
            var doktorVeri = await baglanti.GetAsync("Doktorlar");

            if (doktorVeri.Body != "null")
            {
                var doktorVeriString = doktorVeri.ResultAs<Dictionary<string, Doktor>>();
                lblDoktorSayisi.Text = "Toplam Doktor: "+doktorVeriString.Count.ToString();
            }
            else
            {
                lblDoktorSayisi.Text = "0";
            }



            // -------------------------------------------------------
            // 3. RANDEVU SAYISINI ÇEK
            // -------------------------------------------------------
            var randevuVeri = await baglanti.GetAsync("Randevular");

            if (randevuVeri.Body != "null")
            {
                var randevuVeriString = randevuVeri.ResultAs<Dictionary<string, Randevu>>();
                lblRandevuSayisi.Text = "Toplam Randevu: "+ randevuVeriString.Count.ToString();
            }
            else
            {
                lblRandevuSayisi.Text = "0";
            }
        }



        private void btnHastaKayit_Click(object sender, EventArgs e) // Hasta kayıt ekranına geçiş butonu
        {
            HastaKayit hastaKayit = new HastaKayit();
            hastaKayit.Show();
            this.Hide();
        }



        private void btnDoktor_Click(object sender, EventArgs e) // Doktor yönetim ekranına geçiş butonu
        {
            DoktorYönetim doc = new DoktorYönetim();
            doc.Show();
            this.Hide();
        }

        private void btnRandevu_Click(object sender, EventArgs e) // Randevu yönetim ekranına geçiş butonu
        {
            RandevuYonetimi randevu = new RandevuYonetimi();
            randevu.Show();
            this.Hide();
        }




        private void btnCikis_Click(object sender, EventArgs e) // Çıkış butonu
        {
            Application.Exit();
        }
    }
}

