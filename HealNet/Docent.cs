using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public class Docent : Doktor
    {

        public override double MuayeneUcretiHesapla()
        {
            return 1000 + (DeneyimYili * 20);
        }
    }
}
