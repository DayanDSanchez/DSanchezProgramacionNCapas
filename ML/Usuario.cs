using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario{ get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "Nombre es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = " Nombre solo se acepta letras ")]
        public string Nombre{ get; set; }
        //[DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "Apellido Paterno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Apellido Paterno solo se acepta letras")]
        public string ApellidoPaterno{ get; set; }
        [DisplayName("Apellido Materno")]
        [Required(ErrorMessage = "Apellido Materno es obligatorio")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Apellido Materno solo se acepta letras")]
        public string ApellidoMaterno{ get; set; }
        [DisplayName("Telefono")]
        [Required(ErrorMessage = "Telefono es obligatorio")]
        [RegularExpression(@"^55\d{8}$", ErrorMessage = "El telefono debe iniciar con 55 y tener 10 dígitos")]
        public string Telefono{ get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage ="Email es obligatorio")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "Ingresa un Email valido")]
        public string Email{ get; set; }
        [DisplayName("UserName")]
        [Required(ErrorMessage = "UserName es obligatorio")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]+$", ErrorMessage = "UserName solo permite letras, números y _ . -")]
        public string UserName { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password es obligatorio")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "La contraseña debe tener al menos 1 mayúscula, 1 minúscula, 1 número, 1 carácter especial y 8 caracteres de largo")]
        public string Password { get; set; }
        [DisplayName("Sexo")]
        [Required(ErrorMessage = "Sexo es obligatorio")]
        public string Sexo { get; set; }
        [DisplayName("Celular")]
        [Required(ErrorMessage = "Celular es obligatorio")]
        [RegularExpression(@"^55\d{8}$", ErrorMessage = "El Celular debe iniciar con 55 y tener 10 dígitos ")]
        public string Celular { get; set; }
        [DisplayName("Fecha de Nacimiento")]
        [Required(ErrorMessage = "Fecha de Nacimiento obligatoria")]
        public string FechaNacimiento { get; set; }
        [DisplayName("CURP")]
        [Required(ErrorMessage = "CURP es obligatorio")]
        [RegularExpression(@"^[A-Z]{4}\d{6}[HM][A-Z]{5}[A-Z\d]{2}$", ErrorMessage = " ")]
        public string Curp { get; set; }
        //public int IdRol { get; set; }
        public ML.Rol Rol { get; set; }
        public byte[] Imagen { get; set; }
        public ML.Direccion Direccion { get; set; }
        public bool Status { get; set; } 
        public List<object>Usuarios { get; set; }
    }
}
