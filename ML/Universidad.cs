using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Universidad
    {
        [DisplayName("Universidad")]
        [Required(ErrorMessage = "Universidad es necesario")]
        public int IdUniversidad { get; set; }
        public string NombreUniversidad { get; set; }
        public List<object>Universidades { get; set; }
    }
}
