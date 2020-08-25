using SRI.Helpers;
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
        IncidenteHelper ic = new IncidenteHelper();
        public ActionResult Filtrado( FiltradoVM filtrado)
        {
            List<IncidenteVM> listaIncidentesVM = new List<IncidenteVM>();

            listaIncidentesVM = ic.GetIncidentes(filtrado);

            return PartialView("_Incidentes", listaIncidentesVM);
        }

        // GET: Filtrado/Details/5
        
    }
}
