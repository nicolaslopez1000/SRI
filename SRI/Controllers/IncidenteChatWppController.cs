using System;
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
    public class IncidenteChatWppController : Controller
    {
        private db_SRI db = new db_SRI();

        // GET: IncidenteChatWpp
        public ActionResult Index()
        {
            return View(db.IncidentesChatWpp.ToList());
        }

        // GET: IncidenteChatWpp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteChatWpp incidenteChatWpp = db.IncidentesChatWpp.Find(id);
            if (incidenteChatWpp == null)
            {
                return HttpNotFound();
            }
            return View(incidenteChatWpp);
        }

        // GET: IncidenteChatWpp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidenteChatWpp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,tipo,respuesta,telefono_entrante,telefono_saliente,nombre_persona_llama")] IncidenteChatWppVM incidenteChatWppVM)
        {
            string email = User.Identity.Name;

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IncidenteChatWpp incidenteChatWpp = new IncidenteChatWpp();

                    incidenteChatWpp.fecha_suceso = incidenteChatWppVM.fecha_suceso;
                    incidenteChatWpp.fecha_creacion = DateTime.Now;
                    incidenteChatWpp.resolucion = incidenteChatWppVM.resolucion;
                    incidenteChatWpp.emocion = (int)incidenteChatWppVM.emocion;
                    incidenteChatWpp.descripcion = incidenteChatWppVM.descripcion;

                    Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(email));
                    incidenteChatWpp.Funcionario = funcionario;
                    
                    incidenteChatWpp.tipo = (int)TipoIncidente.chatWpp;
                    incidenteChatWpp.telefono_entrante = incidenteChatWppVM.telefono_entrante;
                    incidenteChatWpp.telefono_saliente = incidenteChatWppVM.telefono_saliente;
                    incidenteChatWpp.nombre_persona_escribe = incidenteChatWppVM.nombre_persona_escribe;
                    incidenteChatWpp.respuesta = incidenteChatWppVM.respuesta;

                    //falta agregar lista de destinatarios ,cc y to


                    if (ModelState.IsValid)
                    {
                        context.IncidentesChatWpp.Add(incidenteChatWpp);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index");
                    }

                }
            }

            return View(incidenteChatWppVM);
        }

        // GET: IncidenteChatWpp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteChatWpp incidenteChatWpp = db.IncidentesChatWpp.Find(id);
            if (incidenteChatWpp == null)
            {
                return HttpNotFound();
            }
            return View(incidenteChatWpp);
        }

        // POST: IncidenteChatWpp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,tipo,respuesta,telefono_entrante,telefono_saliente,nombre_persona_llama")] IncidenteChatWpp incidenteChatWpp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incidenteChatWpp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incidenteChatWpp);
        }

        // GET: IncidenteChatWpp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteChatWpp incidenteChatWpp = db.IncidentesChatWpp.Find(id);
            if (incidenteChatWpp == null)
            {
                return HttpNotFound();
            }
            return View(incidenteChatWpp);
        }

        // POST: IncidenteChatWpp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncidenteChatWpp incidenteChatWpp = db.IncidentesChatWpp.Find(id);
            db.Incidente.Remove(incidenteChatWpp);
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
