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
using SRI.Models.ViewModels;

namespace SRI.Controllers
{
    [Authorize]
    public class IncidenteController : Controller
    {
        private db_SRI db = new db_SRI();

        private IncidenteHelper ih = new IncidenteHelper();

        // GET: Incidente
        public ActionResult Index()
        {

            List<Incidente> listaIncidentes = db.Incidente.ToList();

            List<IncidenteVM> listaIncidentesVM = new List<IncidenteVM>();

            foreach ( Incidente incidente in listaIncidentes)
            {
                IncidenteVM incidenteVM = ih.IncidenteToViewModel(incidente);
                listaIncidentesVM.Add(incidenteVM);
            }


            return View(listaIncidentesVM);
        }

        // GET: Incidente/Details/5
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
            return View(incidente);
        }

        // GET: Incidente/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,palabrasClave,descripcion")] IncidenteVM incidente)
        {
            if (ModelState.IsValid)
            {
               
                return RedirectToAction("Index");
            }

            return View(incidente);
        }





























        // GET: Incidente/Edit/5
        public ActionResult Edit(int? id)
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
            return View(incidente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion")] Incidente incidente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidente);
        }


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
            return View(incidente);
        }

        // POST: Incidente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incidente incidente = db.Incidente.Find(id);
            db.Incidente.Remove(incidente);
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
