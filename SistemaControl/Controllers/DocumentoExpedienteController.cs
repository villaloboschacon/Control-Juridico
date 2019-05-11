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
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }
            if (option == "Número de Expediente" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                var listaDocumentos = documentoBll.Find(x => x.numeroDocumento.Contains(search) && x.idTipo == 4 && x.idEstado != 9 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
                    foreach (Documento documento in listaDocumentos)
                    {
                        documento.TablaGeneral1 = tablaGeneralBLL.Get(documento.idOrigen);
                        documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idTipo);
                        documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                        if (documento.idEstado.HasValue)
                        {
                            int i = (int)(documento.idEstado);
                            documento.TablaGeneral = tablaGeneralBLL.Get(i);
                        }
                    }
                }
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Número de Ingreso" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                var listaDocumentos = documentoBll.Find(x => x.numeroIngreso.Contains(search) && x.idTipo == 4 && x.idEstado != 9 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
                    foreach (Documento documento in listaDocumentos)
                    {
                        documento.TablaGeneral1 = tablaGeneralBLL.Get(documento.idOrigen);
                        documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idTipo);
                        documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                        if (documento.idEstado.HasValue)
                        {
                            int i = (int)(documento.idEstado);
                            documento.TablaGeneral = tablaGeneralBLL.Get(i);
                        }
                    }
                }
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Ubicación" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                var listaDocumentos = documentoBll.Find(x => x.ubicacion.Contains(search) && x.idTipo == 4 && x.idEstado != 9 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
                    foreach (Documento documento in listaDocumentos)
                    {
                        documento.TablaGeneral1 = tablaGeneralBLL.Get(documento.idOrigen);
                        documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idTipo);
                        documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                        if (documento.idEstado.HasValue)
                        {
                            int i = (int)(documento.idEstado);
                            documento.TablaGeneral = tablaGeneralBLL.Get(i);
                        }
                    }
                }
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "Fecha" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                DateTime date = Convert.ToDateTime(search);
                var listaDocumentos = documentoBll.Find(x => x.fecha.Equals(date) && x.idTipo == 4 && x.idEstado != 9 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
                    foreach (Documento documento in listaDocumentos)
                    {
                        documento.TablaGeneral1 = tablaGeneralBLL.Get(documento.idOrigen);
                        documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idTipo);
                        documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                        if (documento.idEstado.HasValue)
                        {
                            int i = (int)(documento.idEstado);
                            documento.TablaGeneral = tablaGeneralBLL.Get(i);
                        }
                    }
                }
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else if (option == "" || String.IsNullOrEmpty(search))
            {
                option = "";
                search = null;
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
                var documentos = documentoBll.Find(x => search == null && x.idTipo == 4 && x.idEstado != 9 || x.idTipo == 23);
                List<Documento> listaDocumentos = documentos.ToList();
                foreach (Documento documento in listaDocumentos)
                {
                    documento.TablaGeneral1 = tablaGeneralBLL.Get(documento.idOrigen);
                    documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idTipo);
                    documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                    if (documento.idEstado.HasValue)
                    {
                        int i = (int)(documento.idEstado);
                        documento.TablaGeneral = tablaGeneralBLL.Get(i);
                    }
                }
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion");
                ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion");
                ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion");
                ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion");
                var documentos = documentoBll.Find(x => search == null && x.idTipo == 4 && x.idEstado != 9 || x.idTipo == 23);
                List<Documento> listaDocumentos = documentos.ToList();
                foreach (Documento documento in listaDocumentos)
                {
                    documento.TablaGeneral1 = tablaGeneralBLL.Get(documento.idOrigen);
                    documento.TablaGeneral2 = tablaGeneralBLL.Get(documento.idTipo);
                    documento.TablaGeneral3 = tablaGeneralBLL.Get(documento.tipoOrigen);
                    if (documento.idEstado.HasValue)
                    {
                        int i = (int)(documento.idEstado);
                        documento.TablaGeneral = tablaGeneralBLL.Get(i);
                    }
                }
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
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

        [HttpPost, ValidateInput(false)]
        public ActionResult Archivar(int id)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
                documentoBll.archivaDocumento(id);
                documentoBll.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
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