using SRI.Helpers;
using SRI.Models.Classes;
using SRI.Models.Enums;
using SRI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SRI.Controllers
{
    public class FiltradoController : Controller
    {
        IncidenteHelper ih = new IncidenteHelper();


        
        public ActionResult FiltradoPorMesDiaTotal()
        {
            return View();
        }

        
        public ActionResult FiltradoPorMesTipoIncidente()
        {
            return View();
        }

        
        public ActionResult FiltradoPorMesFuncionario()
        {
            return View();
        }


        [HttpPost]
        public ActionResult GetFiltradoPorMesDiaTotal(FiltradoVM filtrado)
        {
            FiltradoPorMesDiaTotalModelView resultModel = new FiltradoPorMesDiaTotalModelView();

            List<IncidenteVM> listaIncidentesVM = new List<IncidenteVM>();
            listaIncidentesVM = ih.GetIncidentes(filtrado);

            List<IncidentesPorDiaEmocion> listaIncidentesGroupedByDay = listaIncidentesVM.GroupBy(x => x.fecha_creacion.Date)
                .Select(
                    group =>
                    new IncidentesPorDiaEmocion
                    {
                        fecha = group.Key,
                        incidentes = group.ToList(),
                        cantidadPositivos = group.Count(x => x.emocion == Emocion.positivo),
                        cantidadNegativos = group.Count(x => x.emocion == Emocion.negativo),
                        cantidadNeutrales = group.Count(x => x.emocion == Emocion.neutral),
                    }).ToList();

            resultModel.listaIncidentesGroupedByDay = listaIncidentesGroupedByDay;
            resultModel.totalNegativos = listaIncidentesVM.Count(x => x.emocion == Emocion.negativo);
            resultModel.totalNeutros = listaIncidentesVM.Count(x => x.emocion == Emocion.neutral);
            resultModel.totalPositivos = listaIncidentesVM.Count(x => x.emocion == Emocion.positivo);


            return PartialView("ParcialFiltradoPorMesDiaEmocion", resultModel);
        }

        [HttpPost]
        public ActionResult GetFiltradoPorMesTipoIncidente([Bind(Include = "mes,tipoIncidente")] FiltradoVM filtrado)
        {
            List<IncidenteVM> listaIncidentesVM = new List<IncidenteVM>();

            listaIncidentesVM = ih.GetIncidentes(filtrado);

            return PartialView("_Incidentes", listaIncidentesVM);
        }

        [HttpPost]
        public ActionResult GetFiltradoPorMesFuncionario([Bind(Include = "mes,funcionario_ci")] FiltradoVM filtrado)
        {
            List<IncidenteVM> listaIncidentesVM = new List<IncidenteVM>();

            listaIncidentesVM = ih.GetIncidentes(filtrado);

            string chatWppStringView = ViewHelper.RenderRazorViewToString(this.ControllerContext, "_Incidentes", listaIncidentesVM.Where(x => x.tipo == TipoIncidente.chatWpp));
            string llamadoStringView = ViewHelper.RenderRazorViewToString(this.ControllerContext, "_Incidentes", listaIncidentesVM.Where(x => x.tipo == TipoIncidente.llamado));
            string mailStringView = ViewHelper.RenderRazorViewToString(this.ControllerContext, "_Incidentes", listaIncidentesVM.Where(x => x.tipo == TipoIncidente.mail));

            return Json(new { chatWppStringView, llamadoStringView, mailStringView });
        }


    }
}
