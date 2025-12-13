using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public class Uzman : Doktor

    {

        public override double MuayeneUcretiHesapla()
        {
            return 800 + (DeneyimYili * 20);
        }
    }
}
