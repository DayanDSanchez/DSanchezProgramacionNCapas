﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Rol
    {
        [DisplayName("Rol")]
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public List<object>Rols { get; set; }
    }
}
