using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SRI.Authorization;
using SRI.Models;
using SRI.Models.ViewModels;

namespace Web.Controllers
{
    [AuthorizationAdmin]
    public class HorarioController : Controller
    { 
        private db_SRI db = new db_SRI();

        // GET: Horario
        public ActionResult Index()
        {
            List<Horario> listHorario = db.Horario.Where(x => x.is_eliminado == false).ToList();
            List<HorarioVM> listHorarioVM = new List<HorarioVM>();

            foreach ( Horario horario in listHorario)
            {
                HorarioVM horarioVM = (HorarioVM)horario;
                listHorarioVM.Add(horarioVM);
            }

            return View(listHorarioVM);
        }

        

        // GET: Horario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Horario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,hora_inicio,hora_fin")] HorarioVM horarioVM)
        {

            Horario horario = new Horario();

            horario.hora_inicio = horarioVM.hora_inicio;

            horario.hora_fin = horarioVM.hora_fin;

            horario.is_eliminado = false;

         
            if (ModelState.IsValid)
            {
                db.Horario.Add(horario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(horarioVM);
        }

        // GET: Horario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horario horario = db.Horario.Find(id);
            if (horario == null)
            {
                return HttpNotFound();
            }

            return View((HorarioVM)horario);
        }

        // POST: Horario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,hora_inicio,hora_fin")] HorarioVM horarioVM)
        {


             using (db_SRI context = new db_SRI())
            {
                Horario horario = context.Horario.Find(horarioVM.Id);

                horario.hora_fin = horarioVM.hora_fin;
                horario.hora_inicio = horarioVM.hora_inicio;

                if (ModelState.IsValid)
                {
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            

            return View(horarioVM);
        }

        // GET: Horario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Horario horario = db.Horario.Find(id);
            if (horario == null)
            {
                return HttpNotFound();
            }
            HorarioVM horarioVM = (HorarioVM)horario;
            return View(horarioVM);
        }

        // POST: Horario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Horario horario = db.Horario.Find(id);
            horario.is_eliminado = true;
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
