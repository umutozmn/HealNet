using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Response;

namespace HealNet
{
    public class FirebaseBaglantisi
    {
        // Bağlantıyı 1 kere kurup isteyenlere veren sınıf
        private static FirebaseConfig baglanti = new FirebaseConfig // static dedik çünkü diğer sınıflarda her seferinde yeni nesne oluşturmasın
        {
            AuthSecret = "USoiJDXJiPetiw5QjaRJbmnaVDHXNvJmqGPxsgdC", 
            BasePath = "https://healnet-56cd7-default-rtdb.europe-west1.firebasedatabase.app"
        };

        private static IFirebaseClient client; // Bağlantı nesnesi

        // Diğer sınıfların kullanması için bağlantı getiren metot
        public static IFirebaseClient BaglantiGetir()
        {
            // Eğer bağlantı daha önce kurulmadıysa kur
            if (client == null)
            {
                client = new FireSharp.FirebaseClient(baglanti);
            }

            // Hazır olan bağlantıyı gönder
            return client;
        }
    }
}


