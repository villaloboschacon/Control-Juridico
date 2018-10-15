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
        public ActionResult Index(string option, string search, string sortOrder, int page = 1, int pageSize = 2)
        {
            ViewBag.NumeroOficio = String.IsNullOrEmpty(sortOrder) ? "numerodoc" : "";
            ViewBag.FechaDeIngreso = sortOrder == "Fecha" ? "FechaDes" : "Fecha";
            var documentos = from s in documentoBll.GetAll() select s;
            switch (sortOrder)
            {
                case "numerodocdes":
                    documentos = documentos.OrderByDescending(s => s.numeroDocumento);
                    break;
                case "Fecha":
                    documentos = documentos.OrderBy(s => s.fecha);
                    break;
                case "FechaDes":
                    documentos = documentos.OrderByDescending(s => s.fecha);
                    break;
                default:
                    documentos = documentos.OrderBy(s => s.numeroDocumento);
                    break;
            }
            if (option == "Número de oficio")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.numeroDocumento == search && x.idTipo == 3|| search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model.ToPagedList(page,pageSize));
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
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => search == null && x.idTipo == 3).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                List<Documento> documento = documentoBll.GetAll();
                return View(model.ToPagedList(page, pageSize));
            }
        }

        [HttpPost]
        public ActionResult Agregar(Documento documento)
        {
            documentoBll.Agregar(documento);
            return RedirectToAction("Index", "Documento");
        }
        [HttpPost]
        public ActionResult Responder(int id)
        {
            Documento documento = documentoBll.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            return PartialView("Responder", documento);
        }

        [HttpPost]
        public ActionResult Crear()
        {
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            return PartialView("Crear");
        }

        [HttpPost]
        public ActionResult Editar(int id)
        {
            Documento documento = documentoBll.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            return PartialView("Editar",documento);
        }
    }
}