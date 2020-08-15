﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SRI.Models;
using SRI.Models.Enums;
using SRI.Models.ViewModels;

namespace SRI.Controllers
{
    public class IncidenteMailController : Controller
    {
        private db_SRI db = new db_SRI();

        // GET: IncidenteMail
        public ActionResult Index()
        {
            return View(db.IncidentesMail.ToList());
        }

        // GET: IncidenteMail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteMail incidenteMail = db.IncidentesMail.Find(id);
            if (incidenteMail == null)
            {
                return HttpNotFound();
            }
            return View(incidenteMail);
        }

        // GET: IncidenteMail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidenteMail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,palabrasClave,resolucion,descripcion,tipo,asunto,respuesta,contenido,remitente")] IncidenteMailVM incidenteMailVM)
        {
            string email = User.Identity.Name;

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IncidenteMail incidenteMail = new IncidenteMail();

                    incidenteMail.fecha_suceso = incidenteMailVM.fecha_suceso;
                    incidenteMail.fecha_creacion = DateTime.Now;
                    incidenteMail.resolucion = incidenteMailVM.resolucion;
                    incidenteMail.emocion = (int)incidenteMailVM.emocion;
                    incidenteMail.descripcion = incidenteMailVM.descripcion;

                    Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(email));
                    incidenteMail.Funcionario = funcionario;

                    incidenteMail.tipo = (int)TipoIncidente.mail;
                    incidenteMail.asunto = incidenteMailVM.asunto;
                    incidenteMail.respuesta = incidenteMailVM.respuesta;
                    incidenteMail.contenido = incidenteMailVM.contenido;
                    incidenteMail.remitente = incidenteMailVM.remitente;

                    //falta agregar lista de destinatarios ,cc y to


                    if (ModelState.IsValid)
                    {
                        context.IncidentesMail.Add(incidenteMail);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index");
                    }

                }
            }

            return View(incidenteMailVM);
        }

        // GET: IncidenteMail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteMail incidenteMail = db.IncidentesMail.Find(id);
            if (incidenteMail == null)
            {
                return HttpNotFound();
            }
            return View(incidenteMail);
        }

        // POST: IncidenteMail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,tipo,asunto,respuesta,contenido,remitente")] IncidenteMail incidenteMail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidenteMail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidenteMail);
        }

        // GET: IncidenteMail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteMail incidenteMail = db.IncidentesMail.Find(id);
            if (incidenteMail == null)
            {
                return HttpNotFound();
            }
            return View(incidenteMail);
        }

        // POST: IncidenteMail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidenteMail incidenteMail = db.IncidentesMail.Find(id);
            db.IncidentesMail.Remove(incidenteMail);
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
