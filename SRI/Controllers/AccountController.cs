﻿using SRI.Helpers;
using SRI.Models;
using SRI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SRI.Controllers
{
    public class AccountController : Controller
    {

        private db_SRI db = new db_SRI();

        private FuncionarioHelper fh = new FuncionarioHelper();

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Incidente");
            }

            return View();
        }


        public ActionResult Unauthorized()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FuncionarioLoginVM funcionario)
        {
            if (ModelState.IsValid)
            {


                var obj = db.Funcionario.Where(a => a.mail.Equals(funcionario.mail) && a.password.Equals(funcionario.password)).FirstOrDefault();

                 if(obj == null || obj.is_eliminado == true)
                {
                    ModelState.AddModelError(string.Empty, "Este funcionario ha sido eliminado, contactese con administración si esto es un error. ");
                }
                if (obj != null)
                {



                    FormsAuthentication.SetAuthCookie(obj.mail, false);

                    Session["FuncionarioCi"] = obj.ci.ToString();
                    Session["FuncionarioMail"] = obj.mail.ToString();
                    Session["IsAdmin"] = obj.rol;
                    Session["CurrUser"] = (FuncionarioVM)obj;
                    return RedirectToAction("Index", "Incidente");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "La combinación de mail y contraseña ingresado no corresponde con la base de datos");
                }

            }
            return View(funcionario);
        }


        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }




    }
}