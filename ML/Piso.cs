using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Piso
    {
        public int IdPiso{ get; set; }
        [DisplayName("Piso")]
        public string Nombre { get; set; }
        public List<object>Pisos { get; set; }
    }
}
