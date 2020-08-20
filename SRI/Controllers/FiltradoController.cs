using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRI.Controllers
{
    public class FiltradoController : Controller
    {
        // GET: Filtrado
        public ActionResult Index()
        {
            return View();
        }

        // GET: Filtrado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Filtrado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filtrado/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Filtrado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Filtrado/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Filtrado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Filtrado/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
