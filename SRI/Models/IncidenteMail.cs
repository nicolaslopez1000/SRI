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
    
    public partial class IncidenteMail : Incidente
    {
        public string asunto { get; set; }
        public string respuesta { get; set; }
        public string contenido { get; set; }
        public string remitente { get; set; }
        public string destinatariosCc { get; set; }
        public string destinatariosTo { get; set; }
    }
}
