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
    [Authorize]
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
        public ActionResult Crear()
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            documentoBll = new DocumentoBLLImpl();
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
            DocumentoViewModel documento = new DocumentoViewModel();
            documento.fecha = DateTime.Now;
            return PartialView("Crear", documento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDocumento(Documento documento)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            documentoBll = new DocumentoBLLImpl();
            if (ModelState.IsValid)
            {
                documentoBll.Agregar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
            return PartialView("Crear", documento);
        }

    }
}