using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pelicula
    {
        public int page {  get; set; }
        public List<DatosPelicula> results { get; set; }
    }
    public class DatosPelicula
    {
        public int id { get; set; }
        public string poster_path { get; set; }
        public string title { get; set; }
        public string overview { get; set; }
    }

}
