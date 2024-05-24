using SinemaOtomasyonu.enumeration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinemaOtomasyonu.model
{
    public class Film
    {
        public int FilmID { get; set; }
        public string FilmAdi { get; set; }
        public string FilmYapimYili { get; set; }
        public string FilmYonetmen { get; set; }
        public string Film_Sure { get; set; }

        public string FilmResim { get; set; }
        public string FilmTurID { get; set; }

        public string TurAD { get; set; }


    }
}
