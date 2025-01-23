using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Carrera
    {
        [DisplayName("Carrera")]
        [Required(ErrorMessage = "Carrera es necesario")]
        public int IdCarrera { get; set; }
        public string NombreCarrera { get; set; }
        public List<object> Carreras { get; set; }
    }
}
