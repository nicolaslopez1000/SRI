using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SRI.Models;

namespace SRI.Controllers
{
    public class IncidenteController : Controller
    {
        private db_SRI db = new db_SRI();

        // GET: Incidente
        public ActionResult Index()
        {
            return View(db.Incidente.ToList());
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

        // POST: Incidente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion")] Incidente incidente)
        {
            if (ModelState.IsValid)
            {
                db.Incidente.Add(incidente);
                db.SaveChanges();
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

        // POST: Incidente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Incidente/Delete/5
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
