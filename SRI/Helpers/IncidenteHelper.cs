using SRI.Models;
using SRI.Models.Enums;
using SRI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SRI.Helpers
{
    public class IncidenteHelper
    {

        public IncidenteVM IncidenteToViewModel(Incidente incidente)
        {
            IncidenteVM incidenteVM = new IncidenteVM();

            incidenteVM.fecha_suceso = incidente.fecha_suceso;
            incidenteVM.fecha_creacion = incidente.fecha_creacion;
            incidenteVM.resolucion = incidente.resolucion;
            incidenteVM.emocion = (Emocion)incidente.emocion;
            incidenteVM.descripcion = incidente.descripcion;
            incidenteVM.tipo = (TipoIncidente)incidente.tipo;
            incidenteVM.palabrasClave = incidente.palabras_clave;
            incidenteVM.Funcionario = (FuncionarioVM)incidente.Funcionario;

            return incidenteVM;

        }








    }
}