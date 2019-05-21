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
        List<Documento> aDocumentos = new List<Documento>();

        public ActionResult Index(string sOption, string sSearch, string sSearchFecha, int page = 1, int pageSize = 7, string message = "")
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                PagedList<Documento> model = new PagedList<Documento>(new List<Documento>(), page, pageSize);
                return View(model);
            }          
            int iTipo = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "tipo", "Expediente");

            if (!string.IsNullOrEmpty(message))
            {
                TempData["message"] = message;
            }
            else
            {
                TempData["message"] = "";
            }
            //Busqueda cuando se selecciona el tipo de busqueda y personaliza la busqueda
            if (!String.IsNullOrEmpty(sOption) && !String.IsNullOrEmpty(sSearch))
            {
                //El ultimo valor es falso por que no tiene que tener numero de ingreso
                try
                {
                    ViewBag.search = sSearch;
                    ViewBag.option = sOption;
                    ViewBag.finalDate = sSearchFecha;
                    if (documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "Expediente") != null)
                    {

                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "Expediente");

                        foreach (Documento oDocumento in aDocumentos)
                        {
                            oDocumento.TablaGeneral1 = tablaGeneralBLL.Get(oDocumento.idOrigen);
                            oDocumento.TablaGeneral2 = tablaGeneralBLL.Get(oDocumento.idTipo);
                            oDocumento.TablaGeneral3 = tablaGeneralBLL.Get(oDocumento.tipoOrigen);
                            if (oDocumento.idEstado.HasValue)
                            {
                                int i = (int)(oDocumento.idEstado);
                                oDocumento.TablaGeneral = tablaGeneralBLL.Get(i);
                            }
                        }
                    }
                    PagedList<Documento> model = new PagedList<Documento>(aDocumentos, page, pageSize);
                    return View(model);
                }
                catch (Exception)
                {
                    PagedList<Documento> model = new PagedList<Documento>(new List<Documento>(), page, pageSize);
                    return View(model);
                }

            }
            //Busqueda cuando no pone la busqueda ni la opcion de busqueda
            //Cuando se inicia el index
            else if (String.IsNullOrEmpty(sOption) && String.IsNullOrEmpty(sSearch))
            {
                PagedList<Documento> model = new PagedList<Documento>(aDocumentos, page, pageSize);
                return View(model);
            }
            //Busqueda cuando no pone la opcion de busqueda pero si la busqueda
            //Por defecto va a buscar en el numero de documento
            else
            {
                try
                {
                    ViewBag.search = sSearch;
                    ViewBag.option = sOption;
                    ViewBag.finalDate = sSearchFecha;
                    if (String.IsNullOrEmpty(sSearch))
                    {
                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, "", "Expediente");
                    }
                    else
                    {
                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, "Número de Oficio", "Expediente");
                    }
                    foreach (Documento documento in aDocumentos)
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
                    PagedList<Documento> model = new PagedList<Documento>(aDocumentos, page, pageSize);
                    return View(model);
                }
                catch (Exception)
                {
                    PagedList<Documento> model = new PagedList<Documento>(new List<Documento>(), page, pageSize);
                    return View(model);
                }
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
                return RedirectToAction("Index", new { message = "success" });
            }
            return PartialView("Crear", documento);
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
                documentoBll.GeneraNumeroIngreso();
                documentoBll.Actualizar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
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
            documentoVista.parte = "";
            int iTipo = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "tipo", "Oficio");
            ViewBag.numeroDocumento = new SelectList(aDocumentos = documentoBll.Consulta(iTipo, "", "", "", "OficioEntrada"), "idDocumento", "numeroDocumento", documentoVista.idDocumento);
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
                documentoBll.Actualizar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
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
                documentoBll.ArchivarDocumento(id);
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
            if (documentoBll.Comprobar(numeroDocumento, idDocumento,false,true))
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
            if (documentoBll.Comprobar(numeroIngreso, idDocumento,true,false))
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