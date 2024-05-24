using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinemaOtomasyonu.enumeration;

namespace SinemaOtomasyonu.model
{
    public class User
    {
        public int ID { get; set; }

        public int TC { get; set; }
        public string Kullanici_Adi { get; set; }
        public string Sifre { get; set; }
        public string Yetki { get; set; }

        public string Ad {  get; set; } 
        public string Soyad { get; set; }   

        public string bolge { get; set; }
        public string Mail { get; set; }
        public string guvenlikSorusu { get; set; }
        public string guvenlikCevabi { get; set; }
        public LoginStatus status { get; set; }
    }
}
