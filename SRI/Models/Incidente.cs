//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Incidente
    {
        public int Id { get; set; }
        public System.DateTime fecha_suceso { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public int emocion { get; set; }
        public string resolucion { get; set; }
        public string descripcion { get; set; }
        public int tipo { get; set; }
        public string palabras_clave { get; set; }
        public Nullable<bool> is_eliminado { get; set; }
    
        public virtual Funcionario Funcionario { get; set; }
    }
}
