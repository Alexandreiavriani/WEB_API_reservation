using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alexandre_IavrianiFinal
{
    public class MovieDTO
    {
        public int Id_movie { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public DateTime Time { get; set; }

        public DateTime MoviePremiere { get; set; }

        public int price { get; set; }

        
    }
}