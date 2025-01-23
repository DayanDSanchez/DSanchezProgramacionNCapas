using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EstatusCita
    {
        public int IdEstatusCita { get; set; }
        [DisplayName("Estatus Cita")]
        public string Nombre { get; set; }
        public List<object>EstatusCitas { get; set; }
    }
}
