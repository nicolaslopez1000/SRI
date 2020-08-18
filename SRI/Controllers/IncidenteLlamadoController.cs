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
    [Authorize]
    public class IncidenteLlamadoController : Controller
    {
        private db_SRI db = new db_SRI();

        // GET: IncidenteLlamado
        public ActionResult Index()
        {
            return View(db.IncidentesLlamado.ToList());
        }

        // GET: IncidenteLlamado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteLlamado incidenteLlamado = db.IncidentesLlamado.Find(id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,telefono_saliente,telefono_entrante,hora_inicio,hora_fin,nombre_persona_llama,palabrasClave")] IncidenteLlamadoVM incidenteLlamadoVM)
        {

            string email = User.Identity.Name;

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IncidenteLlamado incidenteLlamado = new IncidenteLlamado();

                    incidenteLlamado.fecha_suceso = incidenteLlamadoVM.fecha_suceso;
                    incidenteLlamado.fecha_creacion = DateTime.Now;
                    incidenteLlamado.resolucion = incidenteLlamadoVM.resolucion;
                    incidenteLlamado.emocion = (int)incidenteLlamadoVM.emocion;
                    incidenteLlamado.descripcion = incidenteLlamadoVM.descripcion;
                    incidenteLlamado.tipo = (int)TipoIncidente.llamado;

                    Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(email));
                    incidenteLlamado.Funcionario = funcionario;

                    incidenteLlamado.telefono_entrante = incidenteLlamadoVM.telefono_entrante;
                    incidenteLlamado.telefono_saliente = incidenteLlamadoVM.telefono_saliente;
                    incidenteLlamado.nombre_persona_llama = incidenteLlamadoVM.nombre_persona_llama;
                    incidenteLlamado.hora_fin = incidenteLlamadoVM.hora_fin;
                    incidenteLlamado.hora_inicio = incidenteLlamadoVM.hora_inicio;
                    


                    string palabrasClave = incidenteLlamadoVM.palabrasClave;
                    string[] palabrasStringList = palabrasClave.Split(',');

                    foreach (string palabra in palabrasStringList)
                    {

                        PalabraClave pc = new PalabraClave();
                        pc.valor = palabra;
                        incidenteLlamado.PalabraClave.Add(pc);

                    }


                    if (ModelState.IsValid)
                    {
                        context.IncidentesLlamado.Add(incidenteLlamado);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index");
                    }

                }
            }

            return View(incidenteLlamadoVM);
        }

        // GET: IncidenteLlamado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncidenteLlamado incidenteLlamado = db.IncidentesLlamado.Find(id);
            if (incidenteLlamado == null)
            {
                return HttpNotFound();
            }
            return View(incidenteLlamado);
        }

        // POST: IncidenteLlamado/Edit/5
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
            IncidenteLlamado incidenteLlamado = db.IncidentesLlamado.Find(id);
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
            IncidenteLlamado incidenteLlamado = db.IncidentesLlamado.Find(id);
            db.IncidentesLlamado.Remove(incidenteLlamado);
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
