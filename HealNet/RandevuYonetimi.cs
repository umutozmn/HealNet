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
        // --- BU LİSTELERİ EKLE ---
        // Tüm doktorları buraya çekeceğiz, sonra branşa göre süzeceğiz.
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


            comboDoktorlar.Items.Clear();

            foreach (var doktor in hafizadakiDoktorlar)
            {
                comboDoktorlar.Items.Add(doktor);
            }


            var baglanti = FirebaseBaglantisi.BaglantiGetir();

            // A) DOKTORLARI ÇEK VE HAFIZAYA AT
            var doktorCevap = await baglanti.GetAsync("Doktorlar");
            if (doktorCevap.Body != "null")
            {
                var dictDoktorlar = doktorCevap.ResultAs<Dictionary<string, Doktor>>();
                hafizadakiDoktorlar = dictDoktorlar.Values.ToList();
            }
            // B) RANDEVULARI ÇEK VE TABLOYA BAS
            Yenile();
        }

        // Randevu listesini yenileyen metot
        private async void Yenile()
        {
            var baglanti = FirebaseBaglantisi.BaglantiGetir();
            var randevuCevap = await baglanti.GetAsync("Randevular");

            if (randevuCevap.Body != "null")
            {
                var dictRandevular = randevuCevap.ResultAs<Dictionary<string, Randevu>>();
                hafizadakiRandevular = dictRandevular.Values.ToList();
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

            // Hastalar tablosuna git ve bu TC'yi sor
            var cevap = await baglanti.GetAsync("Hastalar/" + txtHastaTC.Text);

            if (cevap.Body != "null")
            {
                // Veriyi "Hasta" sınıfına çevir
                Hasta bulunanHasta = cevap.ResultAs<Hasta>();

                // Kutulara doldur (ReadOnly oldukları için kodla yazıyoruz)
                txtHastaAdSoyad.Text = bulunanHasta.Ad + " " + bulunanHasta.Soyad;
                txtHastaTelefon.Text = bulunanHasta.Telefon;

                MessageBox.Show("Hasta bulundu!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Bu TC numarasına ait hasta bulunamadı. Lütfen önce Hasta Kayıt yapınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHastaAdSoyad.Clear();
                txtHastaTelefon.Clear();
            }
        }

        // ============================================================
        // 4. BÖLÜM: AKILLI DOKTOR FİLTRESİ (BRAIN) 🧠
        // ============================================================
        private void comboBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 1. Önce kullanıcının seçtiği branşı alalım (Örn: Göz)
            string secilenBrans = comboBrans.Text;

            // 2. Doktor kutusunu temizleyelim ki eskiler gitsin
            comboDoktorlar.Items.Clear();

            // 3. Sayaç tutalım (Kaç doktor bulduk bilmek için)
            int bulunanSayisi = 0;

            // 4. HAFIZADAKİ TÜM DOKTORLARI TEK TEK GEZİYORUZ (Döngü)
            foreach (Doktor dr in hafizadakiDoktorlar)
            {
                // Eğer sıradaki doktorun branşı, bizim seçtiğimizle aynıysa...
                if (dr.Brans == secilenBrans)
                {
                    // ...Onu kutuya ekle
                    comboDoktorlar.Items.Add(dr.Unvan + " " + dr.Ad + " " + dr.Soyad);

                    // Sayacı bir arttır
                    bulunanSayisi++;
                }
            }

            

            // 5. Sonucu göster
            MessageBox.Show(bulunanSayisi + " tane doktor bulundu.");
        }

        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            // A) Boş Alan Kontrolü
            if (string.IsNullOrEmpty(txtHastaAdSoyad.Text) || string.IsNullOrEmpty(comboDoktorlar.Text) ||
                string.IsNullOrEmpty(comboSaat.Text))
            {
                MessageBox.Show("Lütfen hasta bulunuz, doktor ve saat seçiniz.");
                return;
            }

            string secilenDoktorYazisi = comboDoktorlar.Text;
            string secilenTarih = dtpTarih.Value.ToShortDateString(); // Sadece tarih (saatsiz)
            string secilenSaat = comboSaat.Text;


            // -------------------------------------------------------------
            // B) POLİMORFİZM İLE ÜCRET HESAPLAMA (ŞOV ZAMANI) 🎩
            // -------------------------------------------------------------

            double hesaplananUcret = 0;

            // 1. Önce listeden seçilen doktorun "Verilerini" bulalım
            Doktor bulunanDoktorVerisi = null;

            foreach (Doktor dr in hafizadakiDoktorlar)
            {
                // Senin Doktor sınıfında ToString() metodunu ezdiğin için işimiz çok kolaylaştı!
                // dr.ToString() bize direkt "Unvan Ad Soyad" verir.
                if (dr.ToString() == secilenDoktorYazisi)
                {
                    bulunanDoktorVerisi = dr;
                    break;
                }
            }

            // 2. Şimdi Unvana göre doğru "Sınıfı" canlandıralım
            if (bulunanDoktorVerisi != null)
            {
                // Geçici bir doktor değişkeni (Bu her kılığa girebilir)
                Doktor islemYapacakDoktor;

                string unvan = bulunanDoktorVerisi.Unvan;

                if (unvan.Contains("Profesör") || unvan.Contains("Prof"))
                {
                    islemYapacakDoktor = new Prof(); // Prof sınıfını devreye sok!
                }
                else if (unvan.Contains("Doçent") || unvan.Contains("Doç"))
                {
                    islemYapacakDoktor = new Docent(); // Doçent sınıfını devreye sok!
                }
                else if (unvan.Contains("Uzman") || unvan.Contains("Op"))
                {
                    islemYapacakDoktor = new Uzman(); // Uzman sınıfını devreye sok!
                }
                else
                {
                    islemYapacakDoktor = new Doktor(); // Düz (Pratisyen) doktor.
                }

                // ÖNEMLİ DETAY: Deneyim yılını aktarmazsak hesap yapamaz!
                islemYapacakDoktor.DeneyimYili = bulunanDoktorVerisi.DeneyimYili;

                // Ve büyülü an: Hangi sınıfı ürettiysek onun hesabı çalışır!
                hesaplananUcret = islemYapacakDoktor.MuayeneUcretiHesapla();
            }
            // -------------------------------------------------------------



            // B) KRİTİK KONTROL: DOKTOR DOLU MU?
            // Hafızadaki randevulara bak:
            // Aynı Doktor AND Aynı Tarih AND Aynı Saat var mı?
            bool doluMu = hafizadakiRandevular.Any(r =>
                            r.DoktorAd == secilenDoktorYazisi &&
                            r.Tarih == secilenTarih &&
                            r.Saat == secilenSaat);

            if (doluMu)
            {
                MessageBox.Show("DİKKAT! Seçilen doktorun bu saatte başka bir randevusu var.\nLütfen başka bir saat seçiniz.", "Randevu Çakışması", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; // Kaydetme, çık!
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
            yeniRandevu.Ucret = hesaplananUcret.ToString() + " TL";

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
                // Gizli olan ID sütunundan değeri al
                string id = dtgRandevular.CurrentRow.Cells["RandevuID"].Value.ToString();

                var baglanti = FirebaseBaglantisi.BaglantiGetir();


                MessageBox.Show("Randevuyu silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DialogResult.Yes == DialogResult.Yes)
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


        // Doktorun çalışma saat aralığına göre (Örn: 09:00 - 17:00) saatleri doldurur
        private void SaatleriDoldur(string calismaAraligi)
        {
            comboSaat.Items.Clear(); // Önce eski saatleri temizle

            try
            {
                // "09:00 - 17:00" yazısını ortadan ikiye bölüyoruz
                string[] parcalar = calismaAraligi.Split('-');

                // Başlangıç ve Bitiş saatlerini alıyoruz (Boşlukları temizleyerek)
                TimeSpan baslangic = TimeSpan.Parse(parcalar[0].Trim());
                TimeSpan bitis = TimeSpan.Parse(parcalar[1].Trim());

                // Döngü kuruyoruz: Başlangıçtan bitişe kadar 15'er dakika ekle
                while (baslangic < bitis)
                {
                    // Saati listeye ekle (Örn: 09:00)
                    comboSaat.Items.Add(baslangic.ToString(@"hh\:mm"));

                    // 15 dakika ileri sar (İstersen 20 veya 30 yapabilirsin)
                    baslangic = baslangic.Add(TimeSpan.FromMinutes(15));
                }
            }
            catch
            {
                // Eğer doktorun saati "Belirsiz" falan yazıyorsa standart saatleri koy
                comboSaat.Items.Add("09:00");
                comboSaat.Items.Add("10:00");
                comboSaat.Items.Add("11:00");
                comboSaat.Items.Add("12:00");
                comboSaat.Items.Add("13:00");
                comboSaat.Items.Add("14:00");
                comboSaat.Items.Add("15:00");
                comboSaat.Items.Add("16:00");
            }
        }


        private void comboDoktorlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kutudan seçilen yazı boş değilse
            if (comboDoktorlar.SelectedIndex != -1)
            {
                string secilenDoktorYazisi = comboDoktorlar.Text;

                // Hafızadaki listeden bu isme sahip doktoru bul
                // (Unvan + Ad + Soyad formatında birleştirip karşılaştırıyoruz)
                Doktor secilenDoktor = null;

                foreach (var dr in hafizadakiDoktorlar)
                {
                    string tamIsim = dr.Unvan + " " + dr.Ad + " " + dr.Soyad;

                    if (tamIsim == secilenDoktorYazisi)
                    {
                        secilenDoktor = dr;
                        break;
                    }
                }

                // Eğer doktor bulunduysa saatlerini doldur
                if (secilenDoktor != null)
                {
                    SaatleriDoldur(secilenDoktor.CalismaSaatleri);
                }
            }
        }


        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text.ToLower();

            var filtrelenenListe = hafizadakiRandevular
                .Where(x => x.HastaAdSoyad.ToLower().Contains(aranan) || x.HastaTC.Contains(aranan)).ToList();

            dtgRandevular.DataSource = filtrelenenListe;
        }

           private void btnGeri_Click(object sender, EventArgs e)
        {
            AnaEkran anaEkran = new AnaEkran();
            anaEkran.Show();
            this.Hide();
        }
    }
}


