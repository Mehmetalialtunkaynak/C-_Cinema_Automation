using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinemaOtomasyonu.model
{
    public class LoginTable
    {

        public int ID { get; set; }
        public string Kullanici_Adi { get; set; }
        public string Sifre { get; set; }

        public string Yetki { get; set; }
        public string Mail { get; set; }
        public string guvenlikSorusu { get; set; }

        public string guvenlikCevabi { get; set; }


    }
}
