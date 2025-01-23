﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Vacante
    {
        [DisplayName("Vacante")]
        [Required(ErrorMessage = "Vacante es necesario")]
        public int IdVacante { get; set; }
        public string NombreVacante { get; set; }
        public string FechaPublicacion{ get; set; }
        public string FechaLimite{ get; set; }
        public string UrlVacante{ get; set; }
        public ML.EstatusVacante EstatusVacante { get; set; }
        //public int IsEstatusVacante{ get; set; }
        public List<object>Vacantes{ get; set; }
    }
}
