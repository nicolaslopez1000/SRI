
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SRI.Helpers;
using SRI.Models;
using SRI.Models.Enums;
using SRI.Models.ViewModels;

namespace SRI.Controllers
{
    
    public class IncidenteController : Controller
    {
        private db_SRI db = new db_SRI();

        private IncidenteHelper ih = new IncidenteHelper();

        public ActionResult GetPartialIncidentes()
        {

            List<Incidente> listaIncidentes = db.Incidente.Where(x => x.is_eliminado == false).ToList();

            List<IncidenteVM> listaIncidentesVM = new List<IncidenteVM>();

            foreach (Incidente incidente in listaIncidentes)
            {
                IncidenteVM incidenteVM = (IncidenteVM)incidente;
                listaIncidentesVM.Add(incidenteVM);
            }


            return PartialView("_Incidentes", listaIncidentesVM);
        }


        // GET: Incidente.

        [Authorize]
        public ActionResult Index()
        {

            List<Incidente> listaIncidentes = db.Incidente.Where(x => x.is_eliminado == false).ToList();

            List<IncidenteVM> listaIncidentesVM = new List<IncidenteVM>();

            foreach ( Incidente incidente in listaIncidentes)
            {
                IncidenteVM incidenteVM = (IncidenteVM)incidente;
                listaIncidentesVM.Add(incidenteVM);
            }


            return View(listaIncidentesVM);
        }

        // GET: Incidente/Details/5

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidente incidente = db.Incidente.Find(id);

            if (incidente == null)
            {
                return HttpNotFound();
            }

            switch (incidente.tipo)
            {
                case (int)TipoIncidente.chatWpp:
                    return RedirectToAction("Details", "IncidenteChatWpp", new { id = id }); ;
                case (int)TipoIncidente.llamado:
                    return RedirectToAction("Details", "IncidenteLlamado", new { id = id }); ;
                case (int)TipoIncidente.mail:
                    return RedirectToAction("Details", "IncidenteMail", new { id = id }); ;

                default:
                    break;
            }

            return RedirectToAction("Index", "Incidente");
        }



        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidente incidente  = db.Incidente.Find(id);
            if (incidente == null)
            {
                return HttpNotFound();
            }

            switch (incidente.tipo)
            {
                case (int)TipoIncidente.chatWpp:
                    return RedirectToAction("Edit", "IncidenteChatWpp", new { id = id }); ;
                case (int)TipoIncidente.llamado:
                    return RedirectToAction("Edit", "IncidenteLlamado", new { id = id }); ;
                case (int)TipoIncidente.mail:
                    return RedirectToAction("Edit", "IncidenteMail", new { id = id }); ;

                default:
                    break;
            }

            return HttpNotFound();
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidente incidente = db.Incidente.Find(id);
            if (incidente == null)
            {
                return HttpNotFound();
            }
            IncidenteVM incidenteVM = (IncidenteVM)incidente;
            return View(incidenteVM);
        }

        // POST: Incidente/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incidente incidente = db.Incidente.Find(id);
            incidente.is_eliminado = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
