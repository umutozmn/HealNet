using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public  class Hasta: Kisi
    {

        public string KanGrubu { get; set; }
        public string KronikRahatsizliklar { get; set; }
        public string DogumTarihi { get; set; }

        // Babanın emrini uyguluyoruz (Override)
        public override string RoluNedir()
        {
            return "Ben Hasta sınıfıyım";
        }
    }
}
