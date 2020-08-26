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


        public List<IncidenteVM> GetIncidentes(FiltradoVM filtro)
        {
            List<IncidenteVM> listIncidentesVM = new List<IncidenteVM>();
            List<Incidente> listIncidentes = new List<Incidente>();

            using (db_SRI db = new db_SRI())
            {
                if (string.IsNullOrEmpty(filtro.funcionario_ci))
                {
                    if (filtro.tipoIncidente == TipoIncidente.comun)
                    {
                        listIncidentes = GetIncidentesByMonth(filtro.mes);
                    }
                    else
                    {
                       listIncidentes = GetIncidentesByMonth(filtro.mes).Where(a => a.tipo == (int)filtro.tipoIncidente).ToList();
                    }

                }
                else
                {

                    if (filtro.tipoIncidente == TipoIncidente.comun)
                    {
                        listIncidentes = GetIncidentesByMonth(filtro.mes).Where(a => a.Funcionario.ci == filtro.funcionario_ci).ToList(); 
                    }
                    else
                    {
                        listIncidentes = GetIncidentesByMonth(filtro.mes).Where(a => a.tipo == (int)filtro.tipoIncidente && a.Funcionario.ci == filtro.funcionario_ci).ToList();
                    }
                }

                foreach (Incidente incidente in listIncidentes)
                {

                    IncidenteVM incidenteVM = (IncidenteVM)incidente;
                    listIncidentesVM.Add(incidenteVM);
                }
            }

            return listIncidentesVM;

        }


        public List<Incidente> GetIncidentesByMonth(DateTime month )
        {

            var firstDayOfMonth = new DateTime(month.Year, month.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);

            List<Incidente> listIncidentes = new List<Incidente>();

            using (db_SRI db = new db_SRI())
            {
                listIncidentes = db.Incidente
                    .Include("Funcionario.Horario")
                    .Include("FuncionarioAyudado.Horario")
                    .Where(a => a.fecha_creacion > firstDayOfMonth && a.fecha_creacion < lastDayOfMonth)
                    .ToList();
            }

            return listIncidentes;

        }
    }
}