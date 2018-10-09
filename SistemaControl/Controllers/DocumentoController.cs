using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BackEnd.BLL;
using BackEnd.Model;
using PagedList;
using SistemaControl.Models;

namespace SistemaControl.Controllers
{
    public class DocumentoController : Controller
    {
        private IDocumentoBLL documentoBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        public DocumentoController()
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            documentoBll = new DocumentoBLLImpl();

        }
        public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        {
            if (option == "Número de oficio")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.numeroDocumento == search && x.idTipo == 3|| search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Número de Ingreso")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.numeroIngreso == search && x.idTipo == 3|| search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            if (option == "Asunto")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.asunto == search && x.idTipo == 3 || search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            //else if (option == "Fecha")
            //{
            //    List<Documento> listaDocumentos = documentoBll.Find(x => x.fecha == DateTime.TryParseExact(search, "yyyy-MM-dd HH:mm:ss,fff") && x.idDocumento == 5 || search == null).ToList();
            //    PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
            //    return View(model);
            //}
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => search == null && x.idTipo == 3).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                List<Documento> documento = documentoBll.GetAll();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Agregar(Documento documento)
        {
            documentoBll.Agregar(documento);
            return RedirectToAction("Index", "Documento");
        }
        public ActionResult Details(int id)
        {
            Documento documento = documentoBll.Get(id);
            return PartialView("Detalles", documento);
        }

    }
}