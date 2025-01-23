using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Candidato
    {
        public int IdCandidato { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Nombre es necesario")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = " Nombre solo se acepta letras ")]
        public string Nombre { get; set; }
        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "Apellido Paterno es necesario")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Apellido Paterno solo se acepta letras")]
        public string ApellidoPaterno { get; set; }
        [DisplayName("Apellido Materno")]
        [Required(ErrorMessage = "Apellido Materno es necesario")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Apellido Materno solo se acepta letras")]
        public string ApellidoMaterno { get; set; }
        [DisplayName("Edad")]
        [Required(ErrorMessage = "Edad es necesario")]
        public string Edad { get; set; }
        [DisplayName("Correo")]
        [Required(ErrorMessage = "Correo es necesario")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ingresa un Email valido")]
        public string Correo { get; set; }
        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "Teléfono es necesario")]
        [RegularExpression(@"^55\d{8}$", ErrorMessage = "El telefono debe iniciar con 55 y tener 10 dígitos")]
        public string Telefono { get; set; }
        [DisplayName("Direccion")]
        [Required(ErrorMessage = "Direccion es necesario")]
        public string Direccion { get; set; }
        public byte[] Foto { get; set; }
        public string FotoString { get; set; }
        public byte[] Curriculum { get; set; }
        public ML.Universidad Universidad { get; set; }
        //public int IdUniversidad { get; set; }
        public ML.Carrera Carrera { get; set; }
        //public int IdCarrera { get; set; }
        public ML.BolsaTrabajo BolsaTrabajo { get; set; }
        //public int IdBolsaTrabajo { get; set; }
        public ML.Vacante Vacante { get; set; }
        //public int IdVacante { get; set; }
        public List<object>Candidatos { get; set; }
    }
}
