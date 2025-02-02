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

    public partial class IncidenteVM
    {
    
        public int Id { get; set; }

        [Display(Name = "Palabras clave")]
        public string palabrasClave { get; set; }

        [Display(Name = "Fecha suceso")]
        [Required(ErrorMessage = "Ingrese la fecha del suceso")]
        [DataType(DataType.Date)]
        public System.DateTime fecha_suceso { get; set; }

        [Display(Name = "Fecha creacion")]
        [DataType(DataType.Date)]
        public DateTime fecha_creacion { get; set; }

        [Display(Name = "Emoci�n")]
        [Required(ErrorMessage = "Seleccione la emocion")]
        public Emocion emocion { get; set; }

        [Display(Name = "Resolucion")]
        public string resolucion { get; set; }

        [Display(Name = "Descripci�n")]
        [DataType(DataType.MultilineText)]
        public string descripcion { get; set; }

        [Display(Name = "Tipo de incidente")]
        public TipoIncidente tipo { get; set; }

        public virtual FuncionarioVM Funcionario { get; set; }

        public virtual FuncionarioVM FuncionarioAyudado { get; set; }

        [Display(Name = "C�dula del funcionario ayudado")]
        public string funcionario_ayudado_ci { get; set; }

        public static explicit operator IncidenteVM(Incidente incidente)
        {
            IncidenteVM incidenteVM = new IncidenteVM();

            incidenteVM.Id = incidente.Id;
            incidenteVM.fecha_suceso = incidente.fecha_suceso;
            incidenteVM.fecha_creacion = incidente.fecha_creacion;
            incidenteVM.resolucion = incidente.resolucion;
            incidenteVM.emocion = (Emocion)incidente.emocion;
            incidenteVM.descripcion = incidente.descripcion;
            incidenteVM.tipo = (TipoIncidente)incidente.tipo;
            incidenteVM.palabrasClave = incidente.palabras_clave;
            incidenteVM.Funcionario = (FuncionarioVM)incidente.Funcionario;

            if (incidente.FuncionarioAyudado != null)
            {
                incidenteVM.FuncionarioAyudado = (FuncionarioVM)incidente.FuncionarioAyudado;
            }

            return incidenteVM;
        }
    }
}
