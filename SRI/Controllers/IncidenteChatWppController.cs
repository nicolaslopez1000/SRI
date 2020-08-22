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

            IncidenteChatWppVM incidenteChatWppVM = (IncidenteChatWppVM)incidenteChatWpp;

            return View(incidenteChatWppVM);
        }

        // GET: IncidenteChatWpp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidenteChatWpp/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,tipo,respuesta,telefono_entrante,telefono_saliente,nombre_persona_escribe,palabrasClave,funcionario_ayudado_ci")] IncidenteChatWppVM incidenteChatWppVM)
        {
            string email = User.Identity.Name;

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IncidenteChatWpp incidenteChatWpp = new IncidenteChatWpp();

                    incidenteChatWpp.is_eliminado = false;
                    incidenteChatWpp.fecha_suceso =  incidenteChatWppVM.fecha_suceso;
                    incidenteChatWpp.fecha_creacion = DateTime.Now;
                    incidenteChatWpp.resolucion = incidenteChatWppVM.resolucion;
                    incidenteChatWpp.emocion = (int)incidenteChatWppVM.emocion;
                    incidenteChatWpp.descripcion = incidenteChatWppVM.descripcion;
                    incidenteChatWpp.tipo = (int)TipoIncidente.chatWpp;

                    Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(email));
                    incidenteChatWpp.Funcionario = funcionario;

                    Funcionario funcionarioAyudado = context.Funcionario.Find(incidenteChatWppVM.funcionario_ayudado_ci);
                    incidenteChatWpp.FuncionarioAyudado = funcionarioAyudado;



                    incidenteChatWpp.telefono_entrante = incidenteChatWppVM.telefono_entrante;
                    incidenteChatWpp.respuesta = incidenteChatWppVM.respuesta;


                    incidenteChatWpp.palabras_clave = incidenteChatWppVM.palabrasClave;

                    if (funcionarioAyudado != null)
                    {
                        if (ModelState.IsValid)
                        {
                            context.IncidentesChatWpp.Add(incidenteChatWpp);
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            return RedirectToAction("Index", "Incidente");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No existe ningún funcionario con esa cedula , confirmela con el funcionario que se comunicó");
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

            IncidenteChatWppVM incidenteChatWppVM = (IncidenteChatWppVM)incidenteChatWpp;

            return View(incidenteChatWppVM);
        }

        // POST: IncidenteChatWpp/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,respuesta,telefono_entrante,palabrasClave")] IncidenteChatWppVM incidenteChatWppVM)
        {

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {


                    IncidenteChatWpp incidenteChatWpp = context.IncidentesChatWpp.Find(incidenteChatWppVM.Id);

                    
                    incidenteChatWpp.resolucion = incidenteChatWppVM.resolucion;
                    incidenteChatWpp.descripcion = incidenteChatWppVM.descripcion;
                    incidenteChatWpp.telefono_entrante = incidenteChatWppVM.telefono_entrante;
                    incidenteChatWpp.respuesta = incidenteChatWppVM.respuesta;


                    if (ModelState.IsValid)
                    {
                        context.SaveChanges();
                        dbContextTransaction.Commit();
                        return RedirectToAction("Index", "Incidente");
                    }

                }
            }


            return View(incidenteChatWppVM);

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
