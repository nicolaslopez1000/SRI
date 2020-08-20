//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SRI.Models.ViewModels
{
    using SRI.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class IncidenteChatWppVM : IncidenteVM
    {
        [Display(Name = "Respuesta")]
        [Required(ErrorMessage = "Ingrese la respuesta")]
        public string respuesta { get; set; }

        [Display(Name = "Telefono entrante")]
        [Required(ErrorMessage = "Ingrese el tel�fono entrante")]
        public string telefono_entrante { get; set; }
        [Display(Name = "Tel�fono entrante")]
        [Required(ErrorMessage = "Ingrese el tel�fono saliente")]
        public string telefono_saliente { get; set; }

        [Display(Name = "Nombre de persona que escribe")]
        [Required(ErrorMessage = "Ingrese el nombre de la persona que escribe")]
        public string nombre_persona_escribe { get; set; }

        public static explicit operator IncidenteChatWppVM(IncidenteChatWpp incidenteChatWpp)
        {
            IncidenteChatWppVM incidenteChatWppVM = new IncidenteChatWppVM();

            incidenteChatWppVM.fecha_suceso = incidenteChatWpp.fecha_suceso;
            incidenteChatWppVM.fecha_creacion = incidenteChatWpp.fecha_creacion;
            incidenteChatWppVM.resolucion = incidenteChatWpp.resolucion;
            incidenteChatWppVM.emocion = (Emocion)incidenteChatWpp.emocion;
            incidenteChatWppVM.descripcion = incidenteChatWpp.descripcion;
            incidenteChatWppVM.tipo = (TipoIncidente)incidenteChatWpp.tipo;
            incidenteChatWppVM.Funcionario = (FuncionarioVM)incidenteChatWpp.Funcionario;


            incidenteChatWppVM.telefono_entrante = incidenteChatWpp.telefono_entrante;
            incidenteChatWppVM.telefono_saliente = incidenteChatWpp.telefono_saliente;
            incidenteChatWppVM.nombre_persona_escribe = incidenteChatWpp.nombre_persona_escribe;
            incidenteChatWppVM.respuesta = incidenteChatWpp.respuesta;

            return incidenteChatWppVM;
        }
    }
}
