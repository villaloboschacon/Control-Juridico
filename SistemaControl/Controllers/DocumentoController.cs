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
    //[Authorize]
    public class DocumentoController : Controller
    {
        private IDocumentoBLL documentoBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        public int pageglobal;

        public ActionResult Index(string option, string search, string currentFilter,string sortOrder, int? page)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
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
                List<Documento> listaDocumentos = documentoBll.Find(x => x.asunto == search && x.idTipo == 3 || search == null && x.idTipo == 3).ToList();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                ViewBag.NumeroOficio = String.IsNullOrEmpty(sortOrder) ? "numerodocdes" : "";
                ViewBag.Ingreso = sortOrder == "Ingreso" ? "IngresoDes" : "Ingreso";
                ViewBag.FechaDeIngreso = sortOrder == "Fecha" ? "FechaDes" : "Fecha";
                var documentos = from s in documentoBll.Find(x => search == null && x.idTipo == 3 || x.idTipo == 23) select s;

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
                    documento.TablaGeneral = tablaGeneralBLL.Get(documento.idOrigen);
                    documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                    documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idOrigen);
                    if(documento.idEstado.HasValue){
                        int i = (int)(documento.idEstado);
                        documento.TablaGeneral1 = tablaGeneralBLL.Get(i);
                    }
                }
                //var m = documentoBll.listaSalidas();
                //var m0 = documentoBll.listaEntradas();
                //var m1 = documentoBll.consultaNumeroIngreso();
                //var m2 = documentoBll.generaNumIngreso();
                //var m3 = documentoBll.consultaNumeroIngreso();
                //var z = documentoBll.getNomenclatura("Servicios Informaticos");



                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearDocumento(Documento documento)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                documentoBll.Agregar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index");
            }
            DocumentoViewModel documentoVista = (DocumentoViewModel)documento;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documento.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documento.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documento.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documento.idEstado);
            return PartialView("Crear", documentoVista);
        }
        public ActionResult Crear()
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            DocumentoViewModel documento = new DocumentoViewModel();
            documento.fecha = DateTime.Now.Date;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", null);
            return PartialView("Crear",documento);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesDocumento(Documento documento)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                documentoBll.Modificar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documento.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documento.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documento.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documento.idEstado);
            return PartialView("Detalle", documento);

        }
        public ActionResult Detalles(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            Documento documento = documentoBll.Get(id);
            DocumentoViewModel documentoVista = (DocumentoViewModel)documento;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documentoVista.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documentoVista.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documentoVista.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documentoVista.idEstado);
            return PartialView("Detalle", documentoVista);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarDocumento(Documento documento)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                documentoBll.generaNumIngreso();
                documentoBll.Modificar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documento.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documento.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documento.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documento.idEstado);
            return PartialView("Editar", documento);

        }
        public ActionResult Editar(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }

            Documento documento = documentoBll.Get(id);
            DocumentoViewModel documentoVista = new DocumentoViewModel();
            documentoVista = (DocumentoViewModel)documento;
            documentoVista.fecha = documento.fecha;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documentoVista.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documentoVista.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documentoVista.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documentoVista.idEstado);
            return PartialView("Editar", documentoVista);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResponderDocumento(Documento documento)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
                
            }
            catch (Exception)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                documentoBll.Agregar(documento);
                documentoBll.generaNumIngreso();
                documentoBll.SaveChanges();
                //Cambiar esto
                //return RedirectToAction("Index");
                return View();
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documento.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documento.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documento.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documento.idEstado);
            return PartialView("Editar", documento);

        }
        public ActionResult Responder(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            DocumentoViewModel documentoVista = new DocumentoViewModel();

            documentoVista.fecha = DateTime.Now;
            documentoVista.numeroDocumento = getNumeroDocumento();

            documentoVista.idTipo = tablaGeneralBLL.getIdTablaGeneral("Documentos","tipo","Oficio");
            documentoVista.idOrigen = tablaGeneralBLL.getIdTablaGeneral("Documentos", "idOrigen", "Servicios jurídicos");
            documentoVista.tipoOrigen = tablaGeneralBLL.getIdTablaGeneral("Documentos", "tipoOrigen", "Departamento Interno");
            documentoVista.idEstado = tablaGeneralBLL.getIdTablaGeneral("Documentos", "estado", "Activo");
            documentoVista.idReferencia = documentoBll.Get(id).idDocumento;
            documentoVista.idReferenciaView = documentoBll.Get(id).numeroDocumento;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documentoVista.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documentoVista.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documentoVista.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documentoVista.idEstado);
            return PartialView("Responder", documentoVista);

        }
        public ActionResult TablaDocumentos(string option, string search, string currentFilter, string sortOrder,int salent, int? page)
        {

            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            List<Documento> listaDocumentos;
            if (salent == 1)
            {
                listaDocumentos = documentoBll.listaEntradas();
            }
            else
            {
                listaDocumentos = documentoBll.listaSalidas();
            }
            foreach (Documento documento in listaDocumentos)
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documento.TablaGeneral = tablaGeneralBLL.Get(documento.idOrigen);
                documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idOrigen);
                if (documento.idEstado.HasValue)
                {
                    int i = (int)(documento.idEstado);
                    documento.TablaGeneral1 = tablaGeneralBLL.Get(i);
                }
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
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.NumeroOficio = String.IsNullOrEmpty(sortOrder) ? "numerodocdes" : "";
            ViewBag.Ingreso = sortOrder == "Ingreso" ? "IngresoDes" : "Ingreso";
            ViewBag.FechaDeIngreso = sortOrder == "Fecha" ? "FechaDes" : "Fecha";
            var documentos = from s in documentoBll.Find(x => search == null && x.idTipo == 3 || x.idTipo == 23) select s;

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

            PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, pageNumber, pageSize);
            return View(model);
        }
        public ActionResult Referencias(string option, string search, string currentFilter, string sortOrder, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            List<Documento> listaDocumentos = new List<Documento>();
            PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page.Value, 4);
            return PartialView("Referencias", model);
            //return PartialView("Referencias",);
        }
        public string getNumeroDocumento()
        {
            string numeroDocumento = (documentoBll.consultaNumeroIngreso() + 1).ToString();
            if (numeroDocumento.Length == 1)
            {
                numeroDocumento = "000" + numeroDocumento;
            }
            else if (numeroDocumento.Length == 2)
            {
                numeroDocumento = "00" + numeroDocumento;
            }
            else
            {
                numeroDocumento = "0" + numeroDocumento;
            }
            return "MA-" + "PSJ-" + numeroDocumento + "-" + DateTime.Now.Year.ToString();
        }

        public JsonResult ComprobarDocumento(string numeroDocumento, string idDocumento)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }
            if (documentoBll.Comprobar(numeroDocumento,1, idDocumento))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de ingreso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult ComprobarIngreso(string numeroIngreso, string idDocumento)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }
            if (documentoBll.Comprobar(numeroIngreso, 2, idDocumento))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de ingreso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult Search(string name)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }
            var resultado = documentoBll.Find(x => x.numeroDocumento.Contains(name)).Select(x => x.numeroDocumento).Take(11).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
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
            catch (Exception)
            {
                
            }
            return this.Json(new { Id = "idOrigen",Reg="OIJ" ,Data = ViewBag.idOrigen }, JsonRequestBehavior.AllowGet);
        }
        
    }
}