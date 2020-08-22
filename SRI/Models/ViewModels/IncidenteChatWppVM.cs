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
        [DataType(DataType.MultilineText)]
        public string respuesta { get; set; }

        [Display(Name = "Telefono entrante")]
        [Required(ErrorMessage = "Ingrese el tel�fono entrante")]
        public string telefono_entrante { get; set; }


        public static explicit operator IncidenteChatWppVM(IncidenteChatWpp incidenteChatWpp)
        {
            IncidenteChatWppVM incidenteChatWppVM = new IncidenteChatWppVM();

            incidenteChatWppVM.fecha_suceso = incidenteChatWpp.fecha_suceso;
            incidenteChatWppVM.fecha_creacion = incidenteChatWpp.fecha_creacion;
            incidenteChatWppVM.resolucion = incidenteChatWpp.resolucion;
            incidenteChatWppVM.emocion = (Emocion)incidenteChatWpp.emocion;
            incidenteChatWppVM.descripcion = incidenteChatWpp.descripcion;
            incidenteChatWppVM.tipo = (TipoIncidente)incidenteChatWpp.tipo;
            incidenteChatWppVM.palabrasClave = incidenteChatWpp.palabras_clave;
            incidenteChatWppVM.Funcionario = (FuncionarioVM)incidenteChatWpp.Funcionario;
            incidenteChatWppVM.FuncionarioAyudado = (FuncionarioVM)incidenteChatWpp.FuncionarioAyudado;

            incidenteChatWppVM.telefono_entrante = incidenteChatWpp.telefono_entrante;
            incidenteChatWppVM.respuesta = incidenteChatWpp.respuesta;

            return incidenteChatWppVM;
        }
    }
}
