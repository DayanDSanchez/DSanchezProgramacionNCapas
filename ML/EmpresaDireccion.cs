using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EmpresaDireccion
    {
        public int IdDireccionEmpresa { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public List<object> DireccionEmpresas { get; set; }
    }
}
