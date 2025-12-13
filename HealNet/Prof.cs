using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public class Prof: Doktor
    {

        // OVERRIDE: "Babama bakma, benim ücretim farklı" demek.
        public override double MuayeneUcretiHesapla()
        {
            return 1500 + (DeneyimYili * 20);
        }
    }
}
