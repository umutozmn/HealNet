using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public class Doktor: Kisi
    {

        public string Brans { get; set; } // Kardiyoloji, Nöroloji, vb.

        public int DeneyimYili { get; set; }
        public string Unvan { get; set; }        // Prof. Dr., Uzm. Dr., Pratisyen

        public string CalismaSaatleri { get; set; } // Örn: 09:00-17:00

        public virtual double MuayeneUcretiHesapla()
        {
            return 500 + (DeneyimYili * 20);
        }
        
        public override string ToString()
        {
            return Unvan + " " + Ad + " " + Soyad;
        }
    }
}
