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
    
    public partial class PalabraClave
    {
        public string valor { get; set; }
        public int Id { get; set; }
    
        public virtual Incidente Incidente { get; set; }
    }
}
