using System.Web;
using System.Web.Mvc;
using SistemaControl.Models;
using SistemaControl.App_Start;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using BackEnd.Model;
using BackEnd.BLL;

namespace SistemaControl.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioBLL usuarioBLL;
        private ITablaGeneralBLL tablaGeneralBLL;

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // usually this will be injected via DI. but creating this manually now for brevity
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);

            var authenticationResult = authService.SignIn(model.Username, model.Password);
            string nombre = User.Identity.Name;
            if (authenticationResult.IsSuccess)
            {
                // we are in!            
                try
                {
                    usuarioBLL = new UsuarioBLLImpl();
                    tablaGeneralBLL = new TablaGeneralBLLImpl();
                }
                catch (Exception ex)
                {
                    return View();
                }
                if (usuarioBLL.getUsuario(model.Username) == null)
                {
                    Usuario usuario = new Usuario();
                    usuario.idEstado = tablaGeneralBLL.getIdTablaGeneral("usuarios","estado","activo").idTablaGeneral; ;
                    usuario.usuario1 = model.Username;
                    usuario.nombre = User.Identity.Name;
                    usuarioBLL.Add(usuario);
                }
                else
                {

                }

                return RedirectToLocal(returnUrl);
            }

            ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return View(model);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        [ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);

            return RedirectToAction("Welcome", "Home");
        }
    }
}