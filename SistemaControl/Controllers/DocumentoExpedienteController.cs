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
    public class DocumentoExpedienteController : Controller
    {
        private IDocumentoBLL documentoBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        public DocumentoExpedienteController()
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            documentoBll = new DocumentoBLLImpl();

        }
        public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        {
            if (option == "Número de oficio")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.numeroDocumento == search && x.idTipo == 4 || search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Número de Ingreso")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.numeroIngreso == search && x.idTipo == 4 || search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Asunto")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.asunto == search && x.idTipo == 4 || search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Ubicación")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.asunto == search && x.idTipo == 4 || search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Descripción")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.asunto == search && x.idTipo == 4 || search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => search == null && x.idTipo == 4).ToList();
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