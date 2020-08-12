﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SRI.Models;
using SRI.Models.ViewModels;

namespace SRI.Controllers
{
    public class IncidenteLlamadoController : Controller
    {
        private db_SRI db = new db_SRI();

        // GET: IncidenteLlamado
        public ActionResult Index()
        {
            return View(db.Incidentes.ToList());
        }

        // GET: IncidenteLlamado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteLlamado incidenteLlamado = db.Incidentes.Find(id);
            if (incidenteLlamado == null)
            {
                return HttpNotFound();
            }
            return View(incidenteLlamado);
        }

        // GET: IncidenteLlamado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidenteLlamado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,telefono_saliente,telefono_entrante,hora_inicio,hora_fin,nombre_persona_llama")] IncidenteLlamadoVM incidenteLlamadoVM)
        {



            string email = User.Identity.Name;

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IncidenteLlamado incidenteLlamado = new IncidenteLlamado();

                    incidenteLlamado.hora_fin = incidenteLlamadoVM.hora_fin;
                    incidenteLlamado.hora_inicio = incidenteLlamadoVM.hora_inicio;
                    incidenteLlamado.fecha_suceso = incidenteLlamadoVM.fecha_suceso;
                    incidenteLlamado.emocion = (int)incidenteLlamadoVM.emocion;
                    incidenteLlamado.telefono_entrante = incidenteLlamadoVM.telefono_entrante;
                    incidenteLlamado.telefono_saliente = incidenteLlamadoVM.telefono_saliente;
                    incidenteLlamado.hora_inicio = incidenteLlamadoVM.hora_inicio;
                    incidenteLlamado.hora_fin = incidenteLlamadoVM.hora_fin;
                    incidenteLlamado.nombre_persona_llama = incidenteLlamadoVM.nombre_persona_llama;


                    Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(email));
                    incidenteLlamado.Funcionario = funcionario;

                    if (ModelState.IsValid)
                    {
                        context.Incidentes.Add(incidenteLlamado);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index");
                    }

                }
            }

            return View(incidenteLlamado);
        }

        // GET: IncidenteLlamado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteLlamado incidenteLlamado = db.Incidentes.Find(id);
            if (incidenteLlamado == null)
            {
                return HttpNotFound();
            }
            return View(incidenteLlamado);
        }

        // POST: IncidenteLlamado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,telefono_saliente,telefono_entrante,hora_inicio,hora_fin,nombre_persona_llama")] IncidenteLlamado incidenteLlamado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidenteLlamado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidenteLlamado);
        }

        // GET: IncidenteLlamado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteLlamado incidenteLlamado = db.Incidentes.Find(id);
            if (incidenteLlamado == null)
            {
                return HttpNotFound();
            }
            return View(incidenteLlamado);
        }

        // POST: IncidenteLlamado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidenteLlamado incidenteLlamado = db.Incidentes.Find(id);
            db.Incidentes.Remove(incidenteLlamado);
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
