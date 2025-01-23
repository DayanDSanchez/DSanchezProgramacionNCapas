using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class BolsaTrabajo
    {
        [DisplayName("Bolsa de Trabajo")]
        [Required(ErrorMessage = "Bolsa de trabajo es necesario")]
        public int IdBolsaTrabajo { get; set; }
        public string NombreBolsaTrabajo { get; set; }
        public List<object>BolsaTrabajos { get; set; }
    }
}
