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

            return View(incidenteMailVM);
        }

        // GET: IncidenteMail/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidenteMail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,palabrasClave,resolucion,descripcion,tipo,asunto,respuesta,contenido,remitente,destinatariosCc,destinatariosTo,funcionario_ayudado_ci")] IncidenteMailVM incidenteMailVM)
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

                    if (string.IsNullOrEmpty(incidenteMailVM.destinatariosCc) && string.IsNullOrEmpty(incidenteMailVM.destinatariosTo))
                    {
                        ModelState.AddModelError(string.Empty, "Debes agregar al menos un destinatario, ya sea cc o to");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(incidenteMailVM.destinatariosTo)) 
                        { 
                            string[] destinatariosToList = incidenteMailVM.destinatariosTo.Split(',');

                            foreach (string destinatario in destinatariosToList)
                            {
                                Funcionario fun = fh.GetFuncionarioByMail(destinatario);
                                if ( fun == null)
                                {
                                    ModelState.AddModelError(string.Empty, "No existe ningún funcionario con el mail " + destinatario + " , confirmela con el funcionario que se comunicó");

                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(incidenteMailVM.destinatariosCc))
                        {
                            string[] destinatariosCcList = incidenteMailVM.destinatariosTo.Split(',');

                            foreach (string destinatario in destinatariosCcList)
                            {
                                Funcionario fun = fh.GetFuncionarioByMail(destinatario);
                                if (fun == null)
                                {
                                    ModelState.AddModelError(string.Empty, "No existe ningún funcionario con el mail " + destinatario + " , confirmela con el funcionario que se comunicó");
                                }
                            }
                        }
                        
                    }

                    if (ModelState.IsValid)
                    {
                        context.IncidentesMail.Add(incidenteMail);
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index", "Incidente");
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

            IncidenteMailVM incidenteMailVM = (IncidenteMailVM)incidenteMail;

            return View(incidenteMailVM);
        }

        // POST: IncidenteMail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,tipo,asunto,respuesta,contenido,remitente")] IncidenteMailVM incidenteMailVM)
        {
            IncidenteMail incidenteMail = new IncidenteMail();

            if (ModelState.IsValid)
            {
                incidenteMail = db.IncidentesMail.Find(incidenteMailVM.Id);
                incidenteMail.descripcion = incidenteMailVM.descripcion;
                incidenteMail.asunto = incidenteMailVM.asunto;
                incidenteMail.respuesta = incidenteMailVM.respuesta;
                incidenteMail.contenido = incidenteMailVM.contenido;

                db.Entry(incidenteMail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Incidente");
            }
            return View(incidenteMail);
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
