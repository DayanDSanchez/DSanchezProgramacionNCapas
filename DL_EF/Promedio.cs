//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL_EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Promedio
    {
        public int IdPromedio { get; set; }
        public Nullable<int> IdCandidato { get; set; }
        public string PromedioUniversidad { get; set; }
        public string PromedioPreparatoria { get; set; }
        public string PromedioSecundaria { get; set; }
    
        public virtual Candidato Candidato { get; set; }
    }
}
