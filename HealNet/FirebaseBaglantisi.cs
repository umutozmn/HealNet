using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace HealNet
{
    public class FirebaseBaglantisi
    {
            // 1. AYARLARI SADECE BURADA YAPIYORUZ
            private static IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "USoiJDXJiPetiw5QjaRJbmnaVDHXNvJmqGPxsgdC",
                BasePath = "https://healnet-56cd7-default-rtdb.europe-west1.firebasedatabase.app"
            };

            private static IFirebaseClient client;

            // 2. BAĞLANTIYI İSTEYENE VEREN METOT
            public static IFirebaseClient BaglantiGetir()
            {
                // Eğer bağlantı daha önce kurulmadıysa kur
                if (client == null)
                {
                    client = new FireSharp.FirebaseClient(config);
                }

                // Hazır olan bağlantıyı gönder
                return client;
            }
        }
    }


