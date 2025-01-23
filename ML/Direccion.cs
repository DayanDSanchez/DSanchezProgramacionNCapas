using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion {  get; set; }
        [DisplayName("Calle")]
        [Required(ErrorMessage = "Calle obligatoria")]
        public string Calle {  get; set; }
        [DisplayName("Numero Interior")]
        public string NumeroInterior {  get; set; }
        [DisplayName("Numero Exterior")]
        [Required(ErrorMessage = "Numero Exterior obligatorio")]
        public string NumeroExterior {  get; set; }
        public ML.Colonia Colonia { get; set; }
    }
}
