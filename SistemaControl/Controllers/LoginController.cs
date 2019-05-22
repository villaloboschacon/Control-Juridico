using System.Web;
using System.Web.Mvc;
using SistemaControl.Models;
using SistemaControl.App_Start;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using BackEnd.Model;
using BackEnd.BLL;
using System.Web.Security;
using System.DirectoryServices.AccountManagement;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace SistemaControl.Controllers
{
    public class LoginController : Controller
    {
        private IUsuarioBLL usuarioBLL;
        private ITablaGeneralBLL tablaGeneralBLL;
        public bool crearRoles(string createRole)
        {
            string[] rolesArray;
            try
            {
                if (Roles.RoleExists(createRole))
                {
                    return false;
                }
                else
                {
                    Roles.CreateRole(createRole);
                    rolesArray = Roles.GetAllRoles();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ex = new Exception();
                return false;
            }

        }

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
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);
            var authenticationResult = authService.SignIn(model.Username, model.Password);
            var roles = ((ClaimsIdentity)User.Identity).Claims
                                    .Where(c => c.Type == ClaimTypes.Role)
                                    .Select(c => c.Value);
            // var authenticationResult = authService.SignIn("Steven Villalobos", "svch1996");
            if (authenticationResult.IsSuccess)
            {
                string rolU;

                try
                {
                    tablaGeneralBLL = new TablaGeneralBLLImpl();
                    usuarioBLL = new UsuarioBLLImpl();
                    rolU = roles.First();
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Login");
                }
                if (usuarioBLL.GetUsuario(model.Username) == null)
                {
                    Usuario usuario = new Usuario();
                    usuario.idEstado = tablaGeneralBLL.GetIdTablaGeneral("usuarios", "estado", "activo");
                    usuario.nombre = model.Username;
                    usuario.usuario1 = model.Username;
                    usuarioBLL.Add(usuario);
                }
                if (rolU == "Jefatura")
                {
                    return RedirectToAction("Index", "Documento");

                }
                else if (rolU == "Abogado")
                {
                    
                }
                else
                {

                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (usuarioBLL.GetUsuario(model.Username) != null)
                {
                    Usuario usuario = new Usuario();
                    usuario.idEstado = tablaGeneralBLL.GetIdTablaGeneral("usuarios", "estado", "activo");
                    usuario.nombre = model.Username;
                    usuario.usuario1 = model.Username;
                    usuarioBLL.Add(usuario);
                }
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