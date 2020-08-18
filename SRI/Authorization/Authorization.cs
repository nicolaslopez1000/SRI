using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SRI.Helpers;
using SRI.Models;
using SRI.Models.Enums;
using static System.Net.WebRequestMethods;


namespace SRI.Authorization
{
    public class AuthorizationAdmin : AuthorizeAttribute
    {

        private db_SRI db = new db_SRI();

        private FuncionarioHelper fh = new FuncionarioHelper();

        protected override bool AuthorizeCore(HttpContextBase httpContextBase)
        {
            bool isAuthorize = base.AuthorizeCore(httpContextBase);

            if (!isAuthorize)
            {
                return false;
            }

            String functionarioMail = (String)httpContextBase.User.Identity.Name;



            Funcionario CurrUser = fh.GetFuncionarioByMail(functionarioMail);

            if (CurrUser != null && CurrUser.rol == (int)Rol.admin)
            {

                return true;

            }

            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //Esto es para hacer un redirect a otra pagina en caso de que el usuario no esta autenticado 
            base.OnAuthorization(filterContext);

            String funcionarioMail = filterContext.HttpContext.User.Identity.Name;

            if (funcionarioMail == "")
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
            else
            {
                if (filterContext.Result is HttpUnauthorizedResult)
                {
                    filterContext.Result = new RedirectResult("/Account/Unauthorized");
                }
            }


        }
    }

}
