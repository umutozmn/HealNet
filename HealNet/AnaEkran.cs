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





        private void btnHastaKayit_Click(object sender, EventArgs e)
        {
            btnTemizle hastaKayit = new btnTemizle();
            hastaKayit.Show();
            this.Hide();
        }

        

        private void btnDoktor_Click(object sender, EventArgs e)
        {
            DoktorYönetim doc = new DoktorYönetim();
            doc.Show();
            this.Hide();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            RandevuYonetimi randevu = new RandevuYonetimi();
            randevu.Show();
            this.Hide();
        }

        private async void AnaEkran_Load(object sender, EventArgs e)
        {
          
            // Bağlantıyı al
            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // -------------------------------------------------------
            // 1. HASTA SAYISINI ÇEK
            // -------------------------------------------------------
            var hastaCevap = await baglanti.GetAsync("Hastalar");

            // Eğer "null" değilse sayıyı al, yoksa 0 yaz
            if (hastaCevap.Body != "null")
            {
                // Veriyi sözlük olarak alıyoruz
                var liste = hastaCevap.ResultAs<Dictionary<string, Hasta>>();
                lblHastaSayisi.Text = "Toplam Hasta: "+liste.Count.ToString();
            }
            else
            {
                lblHastaSayisi.Text = "0";
            }

            // -------------------------------------------------------
            // 2. DOKTOR SAYISINI ÇEK
            // -------------------------------------------------------
            var doktorCevap = await baglanti.GetAsync("Doktorlar");

            if (doktorCevap.Body != "null")
            {
                var liste = doktorCevap.ResultAs<Dictionary<string, Doktor>>();
                lblDoktorSayisi.Text = "Toplam Doktor: "+liste.Count.ToString();
            }
            else
            {
                lblDoktorSayisi.Text = "0";
            }

            // -------------------------------------------------------
            // 3. RANDEVU SAYISINI ÇEK
            // -------------------------------------------------------
            var randevuCevap = await baglanti.GetAsync("Randevular");

            if (randevuCevap.Body != "null")
            {
                var liste = randevuCevap.ResultAs<Dictionary<string, Randevu>>();
                lblRandevuSayisi.Text = "Toplam Randevu: "+liste.Count.ToString();
            }
            else
            {
                lblRandevuSayisi.Text = "0";
            }
        }

       
    }
}

