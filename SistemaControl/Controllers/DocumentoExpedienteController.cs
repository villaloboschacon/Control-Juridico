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
            return PartialView("Crear", documento);
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
            if (documentoBll.Comprobar(numeroDocumento, 1, idDocumento))
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
            return this.Json(new { Id = "idOrigen", Reg = "OIJ", Data = ViewBag.idOrigen }, JsonRequestBehavior.AllowGet);

        }

    }
}