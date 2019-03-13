using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControl.Controllers
{
    [Authorize]
    public class CrearDocumentoController : Controller
    {
        // GET: CrearDocumento
        public ActionResult Index()
        {
            return View();
        }
    }
}