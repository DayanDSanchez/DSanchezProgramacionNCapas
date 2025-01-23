using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    { 
        public int IdEmpleado { get; set; }
        public string RFC {  get; set; }
        public string NumeroEmpleado { get; set; }
        public string Nombre { get; set; }
        public string FechaControl { get; set; }
        public decimal Salario { get; set; }
        public List<object> Empleados { get; set; }
    }
}
