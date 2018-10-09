using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControl.Controllers
{
    public class MasterPageController : Controller
    {
        // GET: MasterPage
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Documento()
        {
            return View("Documento");
        }
        public ActionResult DocumentoExpediente()
        {
            return View("DocumentoExpediente");
        }
        public ActionResult Caso()
        {
            return View("Caso");
        }
        public ActionResult CasoJudicial()
        {
            return View("CasoJudicial");
        }
        public ActionResult Personas()
        {
            return View("Personas");
        }
        public ActionResult PersonaFisica()
        {
            return View("PersonaFisica");
        }

        public ActionResult Ayuda()
        {
            return View("Ayuda");
        }

        public ActionResult About()
        {
            return View("About");
        }
    }
}