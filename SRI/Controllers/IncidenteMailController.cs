using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using SRI.Helpers;
using SRI.Models;
using SRI.Models.Enums;
using SRI.Models.ViewModels;

namespace SRI.Controllers
{
    [Authorize]
    public class IncidenteMailController : Controller
    {
        private db_SRI db = new db_SRI();

        private FuncionarioHelper fh = new FuncionarioHelper();

       
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
            IncidenteMailVM incidenteMailVM = (IncidenteMailVM)incidenteMail;

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
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,palabrasClave,resolucion,descripcion,tipo,asunto,respuesta,contenido,remitente,destinatarisoCc,destinatariosTo")] IncidenteMailVM incidenteMailVM)
        {
            string email = User.Identity.Name;

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IncidenteMail incidenteMail = new IncidenteMail();

                    incidenteMail.is_eliminado = false;
                    incidenteMail.fecha_suceso = incidenteMailVM.fecha_suceso;
                    incidenteMail.fecha_creacion = DateTime.Now;
                    incidenteMail.resolucion = incidenteMailVM.resolucion;
                    incidenteMail.emocion = (int)incidenteMailVM.emocion;
                    incidenteMail.descripcion = incidenteMailVM.descripcion;
                    incidenteMail.tipo = (int)TipoIncidente.mail;

                    Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(email));
                    incidenteMail.Funcionario = funcionario;

                    incidenteMail.asunto = incidenteMailVM.asunto;
                    incidenteMail.respuesta = incidenteMailVM.respuesta;
                    incidenteMail.contenido = incidenteMailVM.contenido;
                    incidenteMail.remitente = incidenteMailVM.remitente;

                    incidenteMail.palabras_clave = incidenteMailVM.palabrasClave;
                    incidenteMail. destinatariosCc = incidenteMailVM.destinatariosCc;
                    incidenteMail.destinatariosTo = incidenteMailVM.destinatariosTo;


                    string[] destinatariosToList = incidenteMailVM.destinatariosCc.Split(',');

                    string[] destinatariosCcList = incidenteMailVM.destinatariosCc.Split(',');

                    foreach( string destinatario in destinatariosCcList)
                    {


                        if (fh.GetFuncionarioByMail(destinatario) != null)
                        {
                            ModelState.AddModelError(string.Empty, "No existe ningún funcionario con el mail "+ destinatario +" , confirmela con el funcionario que se comunicó");

                        }

                    }

                    foreach (string destinatario in destinatariosCcList)
                    {
                        if (fh.GetFuncionarioByMail(destinatario) != null)
                        {
                            ModelState.AddModelError(string.Empty, "No existe ningún funcionario con el mail " + destinatario + " , confirmela con el funcionario que se comunicó");

                        }

                    }

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
