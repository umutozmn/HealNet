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
    public partial class RandevuYonetimi : Form
    {


        // Tüm doktorları burada alıp hafızada tutacağız.
        List<Doktor> hafizadakiDoktorlar = new List<Doktor>();

        // Randevuları buraya çekeceğiz.
        List<Randevu> hafizadakiRandevular = new List<Randevu>();
        // -------------------------

        public RandevuYonetimi()
        {
            InitializeComponent();
        }

        private async void RandevuYonetimi_Load(object sender, EventArgs e)
        {
            // Tablo makyajı
            dtgRandevular.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dtgRandevular.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dtgRandevular.DefaultCellStyle.SelectionBackColor = Color.DarkBlue;
            dtgRandevular.DefaultCellStyle.SelectionForeColor = Color.White;
            dtgRandevular.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;



            comboDoktorlar.Items.Clear(); // Doktor silseydin comboda hala kalırdı, o yüzden temizle.

            foreach (var doktor in hafizadakiDoktorlar)
            {
                comboDoktorlar.Items.Add(doktor);
            }


            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // DOKTORLARI ÇEK VE HAFIZAYA AT
            var doktorData = await baglanti.GetAsync("Doktorlar");
            if (doktorData.Body != "null")
            {
                var doktorDataString = doktorData.ResultAs<Dictionary<string, Doktor>>();
                hafizadakiDoktorlar = doktorDataString.Values.ToList();
            }


            Yenile();
        }

        // Randevu listesini yenileyen metot
        private async void Yenile()
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir();
            var randevuData = await baglanti.GetAsync("Randevular");

            if (randevuData.Body != "null")
            {
                var randevuDataString = randevuData.ResultAs<Dictionary<string, Randevu>>();
                hafizadakiRandevular = randevuDataString.Values.ToList();
                dtgRandevular.DataSource = hafizadakiRandevular;

                // İstemediğin sütunları gizle (ID gibi)
                dtgRandevular.Columns["RandevuID"].Visible = false;
            }
            else
            {
                dtgRandevular.DataSource = null;
                hafizadakiRandevular.Clear();
            }
        }

        private async void btnAra_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtHastaTC.Text))
            {
                MessageBox.Show("Lütfen bir TC numarası giriniz.");
                return;
            }

            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // Hastalar tablosuna gidip o tcdeki veriyi alacak
            var hastaData = await baglanti.GetAsync("Hastalar/" + txtHastaTC.Text);

            if (hastaData.Body != "null")
            {
                // Veriyi "Hasta" sınıfına çevir. Atama yapacağız ya o yüzden
                Hasta bulunanHasta = hastaData.ResultAs<Hasta>();

                
                txtHastaTC.Text = bulunanHasta.TC;
                txtHastaAdSoyad.Text = bulunanHasta.Ad + " " + bulunanHasta.Soyad;
                txtHastaTelefon.Text = bulunanHasta.Telefon;

                MessageBox.Show("Hasta bulundu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bu TC numarasına ait hasta bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

                txtHastaTC.Clear();
                txtHastaAdSoyad.Clear();
                txtHastaTelefon.Clear();
            }
        }




        //  AKILLI DOKTOR FİLTRESİ 
        private void comboBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Önce kullanıcının seçtiği branşı alalım (Örn: Kardiyoloji)
            string secilenBrans = comboBrans.Text;

            // Yapmasaydık: İlk seçtiğimiz bölümün doktorları gelecekti oraya ama sonra bölüm değiştirsek eski bölümün doktorları orada durmaya devam edecekti.
            comboDoktorlar.Items.Clear();

            //  Kaç doktor bulduk
            int doktorSayisi = 0;

            // Hafızamızdaki doktorlara bakıyoruz 
            foreach (Doktor dr in hafizadakiDoktorlar)
            {
                // Eğer sıradaki doktorun branşı, bizim seçtiğimizle aynıysa...
                if (dr.Brans == secilenBrans)
                {
                    // ...Onu kutuya ekle
                    comboDoktorlar.Items.Add(dr.Unvan + " " + dr.Ad + " " + dr.Soyad);

                    // Sayacı bir arttır
                    doktorSayisi++;
                }
            }

            MessageBox.Show(doktorSayisi + " tane doktor bulundu.");
        }



        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            // Zorunlu alanlar 
            if (string.IsNullOrEmpty(txtHastaAdSoyad.Text) ||  string.IsNullOrEmpty(comboDoktorlar.Text) ||
                string.IsNullOrEmpty(comboSaat.Text))
            {
                MessageBox.Show("Lütfen hasta bulunuz, doktor ve saat seçiniz.");
                return;
            }

            string secilenDoktorYazisi = comboDoktorlar.Text;
            string secilenTarih = dtpTarih.Value.ToShortDateString(); // Sadece tarih (saatsiz)
            string secilenSaat = comboSaat.Text;


            // POLİMORFİZM İLE ÜCRET HESAPLAMA yapıyoruz

            double hesaplananUcret = 0;

            Doktor doktorData = null;

            foreach (Doktor dr in hafizadakiDoktorlar) // Hafızadaki doktorlara bak
            {
                if (dr.ToString() == secilenDoktorYazisi) // Burada dr içinde unvan, ad, soyad var ve biz de aynı formatta karşılaştırıyoruz
                {
                    doktorData = dr;
                    break;
                }
            }



            if (doktorData != null) // Eğer doktor bulunduysa
            {
                Doktor islemYapacakDoktor; // Burada islemYapacakDoktor referansını tutacağız

                string unvan = doktorData.Unvan;  //  doktorData içinde Unvan, Ad ve Soyad var. Biz içinden Unvan'ı alıyoruz

                if (unvan.Contains("Profesör Doktor") ) 
                {
                    islemYapacakDoktor = new Prof(); // Profesör sınıfı devreye girecek!
                }
                else if (unvan.Contains("Doçent Doktor") )
                {
                    islemYapacakDoktor = new Docent(); // Doçent sınıfı devreye girecek!
                }
                else if (unvan.Contains("Uzman Doktor")) 
                {
                    islemYapacakDoktor = new Uzman(); // Uzman sınıfı devreye girecek!
                }
                else
                {
                    islemYapacakDoktor = new Doktor(); //  (Pratisyen) sınıfı yani Doktor sınıfı devreye girecek.
                }

                islemYapacakDoktor.DeneyimYili = doktorData.DeneyimYili; // Deneyim yılını da atıyoruz çünkü ücret hesaplamada lazım

                hesaplananUcret = islemYapacakDoktor.MuayeneUcretiHesapla(); // hesaplananUcret değişkenine sonucu atıyoruz
            }



            // Randevu çakışma kontrolü 
            bool doluMu = false;

            foreach (Randevu randevu in hafizadakiRandevular) // Hafızadaki randevulara bakıyoruz ve bunları randevu değişkenine atarak kontrol edeceğiz altta
            {
                if (randevu.DoktorAd == secilenDoktorYazisi && randevu.Tarih == secilenTarih &&  randevu.Saat == secilenSaat) // Yeni randevuda girdiğin datalar kayıttaki randevuyla eşleşiyorsa
                {
                    doluMu = true;
                    break; 
                    
                }
                // Mesajı neden 
            }

            if (doluMu) // 
            {
                MessageBox.Show( "DİKKAT! Seçilen doktorun bu saatte başka bir randevusu var.\nLütfen başka bir saat seçiniz.",
                    "Randevu Çakışması", MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return;
            }


            // C) Kaydetme İşlemleri
            Randevu yeniRandevu = new Randevu();

            // Benzersiz bir ID oluşturuyoruz (Guid)
            yeniRandevu.RandevuID = Guid.NewGuid().ToString();

            yeniRandevu.HastaTC = txtHastaTC.Text;
            yeniRandevu.HastaAdSoyad = txtHastaAdSoyad.Text;
            yeniRandevu.HastaTelefon = txtHastaTelefon.Text;
            yeniRandevu.DoktorAd = secilenDoktorYazisi;
            yeniRandevu.Tarih = secilenTarih;
            yeniRandevu.Saat = secilenSaat;


            // Hesapladığımız ücreti buraya yazıyoruz
            yeniRandevu.Ucret = hesaplananUcret.ToString() + " TL"; // 

            var baglanti = FirebaseBaglantisi.BaglantiGetir();
            await baglanti.SetAsync("Randevular/" + yeniRandevu.RandevuID, yeniRandevu);

            MessageBox.Show("Randevu oluşturuldu.\nMuayene Ücreti: " + yeniRandevu.Ucret);

            Yenile();
        }

        private async void btnSil_Click(object sender, EventArgs e)
        {
            // Tablodan seçilen satırı silme
            if (dtgRandevular.SelectedRows.Count > 0) 
            {
                
                string id = dtgRandevular.CurrentRow.Cells["RandevuID"].Value.ToString();  // Buradaki currentrow, seçili satırı verir. Ondan da RandevuID hücresinin değerini alıyoruz stringe çevirerek. 

                var baglanti = FirebaseBaglantisi.BaglantiGetir();

                
             var secim= MessageBox.Show("Randevuyu silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (secim ==DialogResult.Yes)
                {
                    await baglanti.DeleteAsync("Randevular/" + id);
                    MessageBox.Show("Randevu iptal edildi.");
                }

                Yenile();
            }
            else
            {
                MessageBox.Show("Lütfen listeden silinecek randevuyu seçiniz.");
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtHastaTC.Clear();
            txtHastaAdSoyad.Clear();
            txtHastaTelefon.Clear();
            comboBrans.SelectedIndex = -1;
            comboDoktorlar.Items.Clear(); // Doktorları da boşalt
            comboSaat.SelectedIndex = -1;
        }


        // 1. BU ANA METOT (Karar Verici)
        private void SaatleriDoldur(string calismaAraligi)
        {
            // Önce eski saatleri temizle, yoksa üst üste biner
            comboSaat.Items.Clear();

            // Senin yazdığın mantığı metodun içine aldık:
            if (calismaAraligi == "09:00 - 17:00")
                SaatleriDoldur_09_17();
            else if (calismaAraligi == "08:30 - 16:30")
                SaatleriDoldur_0830_1630();
            else if (calismaAraligi == "08:00 - 16:00")
                SaatleriDoldur_08_16();
            else if (calismaAraligi == "09:30 - 17:30")
                SaatleriDoldur_0930_1730();
            else if (calismaAraligi == "10:00 - 18:00")
                SaatleriDoldur_10_18();
            else if (calismaAraligi == "08:30 - 17:00")
                SaatleriDoldur_0830_17();
            else
            {
                // Hiçbiri tutmazsa standart bir şey gelsin (Hata vermesin)
                SaatleriDoldur_09_17();
            }
        }

        // 2. BUNLAR DA SENİN HAZIRLADIĞIN METOTLAR
        private void SaatleriDoldur_09_17()
        {
            comboSaat.Items.Add("09:00"); comboSaat.Items.Add("09:30");
            comboSaat.Items.Add("10:00"); comboSaat.Items.Add("10:30");
            comboSaat.Items.Add("11:00"); comboSaat.Items.Add("11:30");
            comboSaat.Items.Add("13:00"); comboSaat.Items.Add("13:30");
            comboSaat.Items.Add("14:00"); comboSaat.Items.Add("14:30");
            comboSaat.Items.Add("15:00"); comboSaat.Items.Add("15:30");
            comboSaat.Items.Add("16:00");
        }

        private void SaatleriDoldur_0830_1630()
        {
            comboSaat.Items.Add("08:30"); comboSaat.Items.Add("09:00");
            comboSaat.Items.Add("09:30"); comboSaat.Items.Add("10:00");
            comboSaat.Items.Add("10:30"); comboSaat.Items.Add("11:00");
            comboSaat.Items.Add("11:30"); comboSaat.Items.Add("13:00");
            comboSaat.Items.Add("13:30"); comboSaat.Items.Add("14:00");
            comboSaat.Items.Add("14:30"); comboSaat.Items.Add("15:00");
            comboSaat.Items.Add("15:30"); comboSaat.Items.Add("16:00");
        }

        private void SaatleriDoldur_08_16()
        {
            comboSaat.Items.Add("08:00"); comboSaat.Items.Add("08:30");
            comboSaat.Items.Add("09:00"); comboSaat.Items.Add("09:30");
            comboSaat.Items.Add("10:00"); comboSaat.Items.Add("10:30");
            comboSaat.Items.Add("11:00"); comboSaat.Items.Add("11:30");
            comboSaat.Items.Add("13:00"); comboSaat.Items.Add("13:30");
            comboSaat.Items.Add("14:00"); comboSaat.Items.Add("14:30");
            comboSaat.Items.Add("15:00"); comboSaat.Items.Add("15:30");
        }

        private void SaatleriDoldur_0930_1730()
        {
            comboSaat.Items.Add("09:30"); comboSaat.Items.Add("10:00");
            comboSaat.Items.Add("10:30"); comboSaat.Items.Add("11:00");
            comboSaat.Items.Add("11:30"); comboSaat.Items.Add("13:00");
            comboSaat.Items.Add("13:30"); comboSaat.Items.Add("14:00");
            comboSaat.Items.Add("14:30"); comboSaat.Items.Add("15:00");
            comboSaat.Items.Add("15:30"); comboSaat.Items.Add("16:00");
            comboSaat.Items.Add("16:30"); comboSaat.Items.Add("17:00");
        }

        private void SaatleriDoldur_10_18()
        {
            comboSaat.Items.Add("10:00"); comboSaat.Items.Add("10:30");
            comboSaat.Items.Add("11:00"); comboSaat.Items.Add("11:30");
            comboSaat.Items.Add("13:00"); comboSaat.Items.Add("13:30");
            comboSaat.Items.Add("14:00"); comboSaat.Items.Add("14:30");
            comboSaat.Items.Add("15:00"); comboSaat.Items.Add("15:30");
            comboSaat.Items.Add("16:00"); comboSaat.Items.Add("16:30");
            comboSaat.Items.Add("17:00"); comboSaat.Items.Add("17:30");
        }

        private void SaatleriDoldur_0830_17()
        {
            comboSaat.Items.Add("08:30"); comboSaat.Items.Add("09:00");
            comboSaat.Items.Add("09:30"); comboSaat.Items.Add("10:00");
            comboSaat.Items.Add("10:30"); comboSaat.Items.Add("11:00");
            comboSaat.Items.Add("11:30"); comboSaat.Items.Add("13:00");
            comboSaat.Items.Add("13:30"); comboSaat.Items.Add("14:00");
            comboSaat.Items.Add("14:30"); comboSaat.Items.Add("15:00");
            comboSaat.Items.Add("15:30"); comboSaat.Items.Add("16:00");
            comboSaat.Items.Add("16:30");
        }





        private void comboDoktorlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kutudan seçilen yazı boş değilse
            if (comboDoktorlar.SelectedIndex != -1)
            {
                string secilenDoktorYazisi = comboDoktorlar.Text;

                Doktor doktorData = null; // Başta null yapıyoruz çünkü bulamayabiliriz

                foreach (Doktor dr in hafizadakiDoktorlar)
                {
                    string tamIsim = dr.Unvan + " " + dr.Ad + " " + dr.Soyad;

                    if (tamIsim == secilenDoktorYazisi)
                    {
                        doktorData = dr;
                        break;
                    }
                }

                // Eğer doktor bulunduysa saatlerini doldur
                if (doktorData != null)
                {
                    SaatleriDoldur(doktorData.CalismaSaatleri); 
                }
            }
        }


        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text.ToLower();

            List<Randevu> arananListe = new List<Randevu>();  // Sonuçları buna koyacağız 

            foreach (Randevu r in hafizadakiRandevular)
            {
                if (r.HastaAdSoyad.ToLower().Contains(aranan) ||  r.HastaTC.Contains(aranan)) 
                {
                    arananListe.Add(r);
                }
            }

            dtgRandevular.DataSource = arananListe;
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();
            anaEkran.Show();
            this.Hide();
        }

    }
}


