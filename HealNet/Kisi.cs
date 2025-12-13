using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealNet
{
    public abstract class Kisi
    {
        public string TC { get; set; } // Kimlik ID olacak
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Cinsiyet { get; set; }

        public string Telefon { get; set; }

        // --- İŞTE GERÇEK SOYUTLAMA ---
        // Bu bir metot değil, bir EMİRDİR.
        // Diyor ki: "Beni miras alan herkes, RoluNedir diye bir metot yazmak ZORUNDA!"
        // Ama benim içim boş (abstract), çünkü ben ne olduğumu bilmiyorum.
        public abstract string RoluNedir();

    }
}
