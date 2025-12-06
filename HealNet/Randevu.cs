using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public class Randevu
    {

        public string RandevuID { get; set; }
        public string HastaTC { get; set; }
        public string HastaAdSoyad { get; set; } // Listede kolay görünsün diye
        public string DoktorTC { get; set; }
        public string DoktorAdSoyad { get; set; }
        public string Tarih { get; set; }
        public string Saat { get; set; }
        public string Sikayet { get; set; } // Baş ağrısı vb.
    }
}
