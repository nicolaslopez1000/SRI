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

            IncidenteLlamadoVM incidenteLlamadoVM = (IncidenteLlamadoVM)incidenteLlamado;
            return View(incidenteLlamadoVM);
        }

        // GET: IncidenteLlamado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncidenteLlamado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,telefono_entrante,hora_inicio,hora_fin,palabrasClave,funcionario_ayudado_ci")] IncidenteLlamadoVM incidenteLlamadoVM)
        {


            string email = User.Identity.Name;

            using (db_SRI context = new db_SRI())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    IncidenteLlamado incidenteLlamado = new IncidenteLlamado();

                    incidenteLlamado.is_eliminado = false;
                    incidenteLlamado.fecha_suceso = incidenteLlamadoVM.fecha_suceso;
                    incidenteLlamado.fecha_creacion = DateTime.Now;
                    incidenteLlamado.resolucion = incidenteLlamadoVM.resolucion;
                    incidenteLlamado.emocion = (int)incidenteLlamadoVM.emocion;
                    incidenteLlamado.descripcion = incidenteLlamadoVM.descripcion;
                    incidenteLlamado.tipo = (int)TipoIncidente.llamado;

                    Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(email));
                    incidenteLlamado.Funcionario = funcionario;

                    incidenteLlamado.telefono_entrante = incidenteLlamadoVM.telefono_entrante;


                    Funcionario funcionarioAyudado = context.Funcionario.Find(incidenteLlamadoVM.funcionario_ayudado_ci);
                    incidenteLlamado.FuncionarioAyudado = funcionarioAyudado;

                    if (funcionarioAyudado == null)
                    {
                        ModelState.AddModelError(string.Empty, "No existe ningún funcionario con esa cedula , confirmela con el funcionario que se comunicó");
                    }

                    incidenteLlamado.hora_fin = incidenteLlamadoVM.hora_fin;
                    incidenteLlamado.hora_inicio = incidenteLlamadoVM.hora_inicio;


                    incidenteLlamado.palabras_clave = incidenteLlamadoVM.palabrasClave;

                  
                        if (ModelState.IsValid)
                        {
                            context.IncidentesLlamado.Add(incidenteLlamado);
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            return RedirectToAction("Index","Incidente");
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

            IncidenteLlamadoVM incidenteLlamadoVM = (IncidenteLlamadoVM)incidenteLlamado;

            return View(incidenteLlamadoVM);
        }

        // POST: IncidenteLlamado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fecha_suceso,fecha_creacion,emocion,resolucion,descripcion,telefono_entrante,hora_inicio,hora_fin,palabrasClave,funcionario_ayudado_ci")] IncidenteLlamadoVM incidenteLlamadoVM)
        {
            IncidenteLlamado incidenteLlamado = new IncidenteLlamado();

            if (ModelState.IsValid)
            {
                incidenteLlamado = db.IncidentesLlamado.Find(incidenteLlamadoVM.Id);
                incidenteLlamado.descripcion = incidenteLlamadoVM.descripcion;
                incidenteLlamado.resolucion = incidenteLlamadoVM.resolucion;

                db.Entry(incidenteLlamado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Incidente");
            }
            return View(incidenteLlamado);
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
