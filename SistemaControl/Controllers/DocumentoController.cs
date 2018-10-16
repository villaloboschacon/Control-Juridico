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
        public JsonResult Search(string name)
        {
            var resultado = documentoBll.Find(x => x.numeroDocumento.Contains(name)).Select(x => x.numeroDocumento).Take(11).ToList();
            return Json(resultado,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string option, string search, string currentFilter,string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            ViewBag.CurrentFilter = search;

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            if (option == "Número de oficio")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.numeroDocumento == search && x.idTipo == 3|| search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            else if (option == "Número de Ingreso")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.numeroIngreso == search && x.idTipo == 3|| search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
                return View(model);
            }
            if (option == "Asunto")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                List<Documento> listaDocumentos = documentoBll.Find(x => x.asunto == search && x.idTipo == 3 || search == null).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                ViewBag.NumeroOficio = String.IsNullOrEmpty(sortOrder) ? "numerodocdes" : "";
                ViewBag.Ingreso = sortOrder == "Ingreso" ? "IngresoDes" : "Ingreso";
                ViewBag.FechaDeIngreso = sortOrder == "Fecha" ? "FechaDes" : "Fecha";
                var documentos = from s in documentoBll.Find(x => search == null && x.idTipo == 3) select s;

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
                    case "Ingreso":
                        documentos = documentos.OrderBy(s => s.numeroIngreso);
                        break;
                    case "IngresoDes":
                        documentos = documentos.OrderByDescending(s => s.numeroIngreso);
                        break;
                    default:
                        documentos = documentos.OrderBy(s => s.numeroDocumento);
                        break;
                }
                List<Documento> listaDocumentos = documentos.ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
                return View(model);
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
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
            return PartialView("Editar",documento);
        }
        [HttpPost]
        public ActionResult EditarDocumento(Documento documento)
        {
            documentoBll.Modificar(documento);
            return View(documento);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Editar([Bind(Include = "idDocumento, idTipo, idOrigen, tipoOrigen, idEstado, idReferencia, numeroDocumento, numeroIngreso, fecha, asunto, descripcion, ubicacion, observacion")]Documento documento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        documentoBll.Modificar(documento);
        //        return RedirectToAction("Index");
        //    }
           
        //    documentoBll.Modificar(documento);
        //    return View(documento);
        //}
        public JsonResult GetTipoOrigen(int id)
        {
            switch (id)
            {
                case 5:
                    ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                    break;
                case 6:
                    ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigenExterno"), "idTablaGeneral", "descripcion");
                    break;
                default:
                    ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                    break;
            }
            return Json(ViewBag.idOrigen);
        }
    }
}