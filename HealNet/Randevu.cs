using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public class Randevu
    {

        public string RandevuID { get; set; }    // Firebase ID'si (Silmek için lazım)

        public string HastaTC { get; set; }      // Kimin randevusu?

        public string HastaAdSoyad { get; set; } // Listede "Ahmet Yılmaz" diye görmek için

        public string DoktorAd { get; set; }     // "Prof. Dr. Mehmet Öz" şeklinde tam isim

        public string Tarih { get; set; }        // 12.12.2025

        public string Saat { get; set; }         // 14:30

    }
}
