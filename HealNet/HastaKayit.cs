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

        }


        private void btnGeri_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();
            anaEkran.Show();
            this.Hide();
        }



        
    }
}
