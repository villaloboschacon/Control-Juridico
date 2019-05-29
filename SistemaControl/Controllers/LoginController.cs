//using System.Web;
//using System.Web.Mvc;
//using SistemaControl.Models;
//using SistemaControl.App_Start;
//using Microsoft.Owin.Security;
//using System;
//using System.Collections.Generic;
//using BackEnd.Model;
//using BackEnd.BLL;
//using System.Web.Security;
//using System.DirectoryServices.AccountManagement;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System.Threading.Tasks;
//using System.Security.Claims;
//using System.Linq;

//namespace SistemaControl.Controllers
//{
//    public class LoginController : Controller
//    {
//        private IUsuarioBLL usuarioBLL;
//        private ITablaGeneralBLL tablaGeneralBLL;
//        public bool crearRoles(string createRole)
//        {
//            string[] rolesArray;
//            try
//            {
//                if (Roles.RoleExists(createRole))
//                {
//                    return false;
//                }
//                else
//                {
//                    Roles.CreateRole(createRole);
//                    rolesArray = Roles.GetAllRoles();
//                    return true;
//                }
//            }
//            catch (Exception ex)
//            {
//                ex = new Exception();
//                return false;
//            }

//        }

//        [AllowAnonymous]
//        public ActionResult Index()
//        {
//            return View();
//        }
//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public virtual ActionResult Index(LoginViewModel model, string returnUrl)
//        {
//            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
//            var authService = new AdAuthenticationService(authenticationManager);
//            var authenticationResult = authService.SignIn(model.Username, model.Password);
//            if (authenticationResult.IsSuccess)
//            {
//                string rolU;
//                try
//                {
//                    if (User.Identity.IsAuthenticated)
//                    {
//                        var roles = ((ClaimsIdentity)User.Identity).Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
//                        rolU = roles.First();
//                        tablaGeneralBLL = new TablaGeneralBLLImpl();
//                        usuarioBLL = new UsuarioBLLImpl();
//                        if (usuarioBLL.getUsuario(model.Username) == null)
//                        {
//                            Usuario usuario = new Usuario();
//                            usuario.idEstado = tablaGeneralBLL.GetIdTablaGeneral("usuarios", "estado", "activo");
//                            usuario.nombre = model.Username;
//                            usuario.usuario1 = model.Username;
//                            usuarioBLL.Add(usuario);
//                        }
//                        return RedirectToAction("Index", "Documento");
//                    }
//                    else
//                    {
//                        return RedirectToAction("Index", "Login");
//                    }
//                }
//                catch (Exception)
//                {
//                    return RedirectToAction("Index", "Login");
//                }
//            }
//            ModelState.AddModelError("", authenticationResult.ErrorMessage);
//            return View(model);
//        }

//        private ActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//            {
//                return Redirect(returnUrl);
//            }
//            return RedirectToAction("Index", "Home");
//        }

//        [ValidateAntiForgeryToken]
//        public virtual ActionResult Logoff()
//        {
//            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
//            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);

//            return RedirectToAction("Welcome", "Home");
//        }
//    }
//}
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
            // var authenticationResult = authService.SignIn("Steven Villalobos", "svch1996");
            string user;
            if (authenticationResult.IsSuccess)
            {
                user = authService.GetUsername(model.Username, model.Password);
                string rolU;

                try
                {
                    tablaGeneralBLL = new TablaGeneralBLLImpl();
                    usuarioBLL = new UsuarioBLLImpl();
                    rolU = String.Concat(usuarioBLL.gerRolForUser(usuarioBLL.getUsuario(user).nombre));
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (usuarioBLL.getUsuario(model.Username) == null)
                {
                    Usuario usuario = new Usuario();
                    usuario.idEstado = tablaGeneralBLL.GetIdTablaGeneral("usuarios", "estado", "activo");
                    usuario.nombre = model.Username;
                    usuario.usuario1 = user;
                    usuarioBLL.Add(usuario);
                }
                if (rolU == "DBA")
                {
                    if (crearRoles("DBA"))
                    {
                        Roles.AddUserToRole(model.Username, "DBA");
                    }

                }
                else if (rolU == "Abogado")
                {

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