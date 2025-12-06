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


        // --- POLİMORFİZM / BUSINESS LOGIC ---
        public double MuayeneUcretiHesapla()
        {
            double ucret = 0;

            if (Unvan == "Profesör")
            {
                ucret = 1500;
            }
            else if (Unvan == "Doçent")
            {
                ucret = 1000;
            }
            else if (Unvan == "Uzman Dr.")
            {
                ucret = 800;
            }
            else
            {
                ucret = 500; // Pratisyen vb.
            }
            double deneyimBonus = DeneyimYili * 20;

            return ucret+ deneyimBonus;


        }
    }
}
