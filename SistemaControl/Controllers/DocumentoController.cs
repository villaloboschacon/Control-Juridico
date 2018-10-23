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

        public JsonResult Search(string name)
        {
            var resultado = documentoBll.Find(x => x.numeroDocumento.Contains(name)).Select(x => x.numeroDocumento).Take(11).ToList();
            return Json(resultado,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string option, string search, string currentFilter,string sortOrder, int? page)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();

            }
            catch (Exception ex)
            {
                return View();
            }
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

            int pageSize = 4;
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
                foreach (Documento documento in listaDocumentos)
                {
                    tablaGeneralBLL = new TablaGeneralBLLImpl();
                    documento.Origen = tablaGeneralBLL.Get(documento.idOrigen);
                    documento.TablaGeneral = tablaGeneralBLL.Get(documento.tipoOrigen);
                    documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idOrigen);
                    if(documento.idEstado.HasValue){
                        int i = (int)(documento.idEstado);
                        documento.TablaGeneral3 = tablaGeneralBLL.Get(i);
                    }
                }
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Responder(int id)
        {           
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
                Documento documento = documentoBll.Get(id);
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                return PartialView("Responder", documento);
            }
            catch (Exception ex)
            {
                return View();
            }
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
            return PartialView("Crear",documento);
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

        public ActionResult Editar(int id)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            documentoBll = new DocumentoBLLImpl();
            Documento documento = documentoBll.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
            return PartialView("Editar", documento);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDocumento(Documento documento)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            documentoBll = new DocumentoBLLImpl();
            if (ModelState.IsValid)
            {
                documentoBll.Modificar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
            return PartialView("Editar", documento);

        }
        public JsonResult ComprobarDocumento(string numeroDocumento)
        {
            documentoBll = new DocumentoBLLImpl();
            if (documentoBll.Comprobar(numeroDocumento,1))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de documento no se encuentra disponible.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult ComprobarIngreso(string numeroDocumento)
        {
            documentoBll = new DocumentoBLLImpl();
            if (documentoBll.Comprobar(numeroDocumento,2))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de ingreso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Add()
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
                ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
                return PartialView("Add");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        [HttpPost]
        public JsonResult GetTipoOrigen(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
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
            }
            catch (Exception ex)
            {
                
            }
            //var roles = tablaGeneralBLL.Consulta("Documentos", "idOrigenExterno");
            //return Json(roles.Select(role => new SelectListItem() { Text = role.descripcion, Value = role.idTablaGeneral.ToString() }), JsonRequestBehavior.AllowGet);
            return this.Json(new { Id = "idOrigen",Reg="OIJ" ,Data = ViewBag.idOrigen }, JsonRequestBehavior.AllowGet);
            //return Json(ViewBag.idOrigen);
        }
        
    }
}