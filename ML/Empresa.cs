using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empresa
    {
        public int IdEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public List<object> Empresas { get; set; }
        public ML.EmpresaDireccion EmpresaDireccion { get; set; }
    }
}
