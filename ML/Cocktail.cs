using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    // Representa la respuesta general de la API (contiene los resultados, como una lista de cocktails)
    public class Cocktail
    {
        public List<DatosCocktail> Drinks { get; set; }
    }
    public class DatosCocktail
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
        public string strInstructionsES { get; set; }
        public string strDrinkThumb { get; set; }

    }
}
