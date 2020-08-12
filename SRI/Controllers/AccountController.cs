using SRI.Helpers;
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
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public ActionResult Unauthorized()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FuncionarioLoginViewModel funcionario)
        {
            if (ModelState.IsValid)
            {


                var obj = db.Funcionario.Where(a => a.mail.Equals(funcionario.mail) && a.password.Equals(funcionario.password)).FirstOrDefault();
                if (obj != null)
                {
                    FormsAuthentication.SetAuthCookie(obj.mail, false);

                    Session["FuncionarioCi"] = obj.ci.ToString();
                    Session["FuncionarioMail"] = obj.mail.ToString();
                    Session["IsAdmin"] = obj.rol;
                    Session["CurrUser"] = obj;
                    return RedirectToAction("Index", "Home");
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