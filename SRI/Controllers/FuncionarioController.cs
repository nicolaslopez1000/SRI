using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SRI.Authorization;
using SRI.Helpers;
using SRI.Models;
using SRI.Models.ViewModels;

namespace Web.Controllers
{
    [AuthorizationAdmin]
    public class FuncionarioController : Controller
    {
        private db_SRI db = new db_SRI();

        private FuncionarioHelper fh = new FuncionarioHelper();


        // GET: Funcionario
        public ActionResult Index()
        {
            return View(db.Funcionario.ToList().Where(x => x.is_eliminado == false));
        }

        // GET: Funcionario/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View((FuncionarioVM)funcionario);
        }

        // GET: Funcionario/Create
        public ActionResult Create(FuncionarioLoginVM funcionarioLoginVM)
        {

            ViewBag.Horarios = fh.GetListaHorarios();

            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre,ci,mail,apellido,celular,password,rol,horario_id")] FuncionarioVM funcionarioVM)
        {
            Funcionario funcionario = new Funcionario();
            Horario horario = db.Horario.SingleOrDefault(e => e.Id == funcionarioVM.horario_id);
           
            funcionario.Horario = horario;
            funcionario.nombre = funcionarioVM.nombre;
            funcionario.apellido = funcionarioVM.apellido;
            funcionario.ci = funcionarioVM.ci;
            funcionario.mail = funcionarioVM.mail;
            funcionario.celular = funcionarioVM.celular;
            funcionario.password = funcionarioVM.password;
            funcionario.rol = (int)funcionarioVM.rol;  

            var func_mail = fh.GetFuncionarioByMail(funcionario.mail);
            var func_ci = db.Funcionario.FirstOrDefault(x => x.ci == funcionario.ci);
            if (func_mail == null && func_ci == null)
            {
                if (ModelState.IsValid)
                {
                    db.Funcionario.Add(funcionario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else if ( func_mail != null)
            {
             
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con este mail");
            }
            else if (func_ci != null)
            {
                ModelState.AddModelError(string.Empty, "Ya existe un usuario con este numero de documento");
            }



            ViewBag.Horarios = fh.GetListaHorarios();

            return View(funcionarioVM);
        }

        // GET: Funcionario/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            FuncionarioVM funcionarioVM = (FuncionarioVM)funcionario;
            ViewBag.Horarios = fh.GetListaHorarios(funcionarioVM.horario_id);
            return View(funcionarioVM);
        }

        // POST: Funcionario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nombre,ci,mail,apellido,celular,password,rol,horario_id")] FuncionarioVM funcionarioVM)
        {

            using (db_SRI context = new db_SRI())
            {
                Funcionario funcionario = context.Funcionario.FirstOrDefault(a => a.mail.Equals(funcionarioVM.mail));

                Horario horario = context.Horario.FirstOrDefault(e => e.Id == funcionarioVM.horario_id);

                funcionario.Horario = horario;
                funcionario.nombre = funcionarioVM.nombre;
                funcionario.apellido = funcionarioVM.apellido;
                funcionario.ci = funcionarioVM.ci;
                funcionario.mail = funcionarioVM.mail;
                funcionario.celular = funcionarioVM.celular;
                funcionario.password = funcionarioVM.password;
                funcionario.rol = (int)funcionarioVM.rol;
                if (ModelState.IsValid)
                {
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Horarios = fh.GetListaHorarios(funcionarioVM.horario_id);

            return View(funcionarioVM);
        }

        // GET: Funcionario/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Funcionario funcionario = db.Funcionario.Find(id);
            if (funcionario == null)
            {
                return HttpNotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Funcionario funcionario = db.Funcionario.Find(id);
            funcionario.is_eliminado = true;
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
