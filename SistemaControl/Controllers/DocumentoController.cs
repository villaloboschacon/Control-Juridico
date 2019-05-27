using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using BackEnd.BLL;
using BackEnd.Model;
using PagedList;
using SistemaControl.Models;
using Spire.Doc;
using Spire.Doc.Documents;
using Xceed.Words.NET;

namespace SistemaControl.Controllers
{
    public class DocumentoController : Controller
    {
        private IDocumentoBLL documentoBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        List<Documento> aDocumentos = new List<Documento>();

        public ActionResult Index(string sOption, string sSearch, string sSearchFecha, int page = 1, int pageSize = 9, string message = "")
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

            int iTipo = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "tipo", "Oficio");
            
            if (!string.IsNullOrEmpty(message))
            {
                TempData["message"] = message;
            }
            else {
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
                    if (documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "OficioEntrada") != null) {

                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "OficioEntrada");

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
            else
            {
                try
                {
                    var aDocumentos = documentoBll.GetEntradas();
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
        public ActionResult IndexSNI(string sOption, string sSearch, string sSearchFecha, int page = 1, int pageSize = 7, string message = "")
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }

            List<Documento> aDocumentos = new List<Documento>();
            int iTipo = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "tipo", "SNI");

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
                    if (documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "SNI") != null)
                    {

                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "SNI");

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
            else
            {
                try
                {
                    //Cambiar
                    aDocumentos = documentoBll.GetEntradas();
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
        public ActionResult IndexSalidas(string sOption, string sSearch, string sSearchFecha, int page = 1, int pageSize = 7, string message = "")
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }

            List<Documento> aDocumentos = new List<Documento>();
            int iTipo = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "tipo", "Oficio");

            if (!string.IsNullOrEmpty(message))
            {
                TempData["message"] = message;
            }
            else
            {
                TempData["message"] = "";
            }

            if (!String.IsNullOrEmpty(sOption) && !String.IsNullOrEmpty(sSearch))
            {
                //El ultimo valor es falso por que no tiene que tener numero de ingreso
                try
                {
                    ViewBag.search = sSearch;
                    ViewBag.option = sOption;
                    if (documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "OficioSalida") != null)
                    {

                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, "OficioSalida");

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
            else
            {
                aDocumentos = documentoBll.GetSalidas();
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
                PagedList<Documento> model = new PagedList<Documento>(aDocumentos, page, pageSize);
                return View(model);
            }
        }

        public ActionResult IndexReferencias(string option,int id, string search, int page = 1, int pageSize = 15,int pageReferencias=0)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }

            try
            {
                ViewBag.pageReferencias = pageReferencias;
                ViewBag.page = page;
                ViewBag.id = id;
                ViewBag.idReferencia = documentoBll.GetDocumento(id).numeroDocumento;

                Documento documento = documentoBll.GetDocumento(id);
                if (documento.idReferencia != null)
                {
                    aDocumentos = documentoBll.GetReferencias(documento.idReferencia);
                }
                else
                {
                    aDocumentos = documentoBll.GetReferencias(documento.idDocumento);
                }
                if (aDocumentos.Count != 0)
                {
                    var oDocumentoSearch = aDocumentos.Find(x=>x.idDocumento.Equals(documento.idDocumento));
                    if (oDocumentoSearch == null)
                    {
                        aDocumentos.Insert(0,documento);
                    }
                }
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
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            PagedList<Documento> model = new PagedList<Documento>(aDocumentos, page, pageSize);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CrearDocumento(Documento documento)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                documento.idReferencia = documentoBll.GeneraNumeroReferencia();
                documentoBll.Agregar(documento);
                documentoBll.SaveChanges();
                if (documento.idTipo != tablaGeneralBLL.GetIdTablaGeneral("Documentos", "tipo", "SNI"))
                {

                    //Crea el html del texto recibido en el modal
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + documento.numeroDocumento+".html";
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        using (StreamWriter w = new StreamWriter(fs, Encoding.UTF8))
                        {
                            w.WriteLine(documento.texto);
                        }
                    }
                    //Carga el html de la direccion de path
                    Spire.Doc.Document document = new Spire.Doc.Document();
                    document.LoadFromFile(path, FileFormat.Html, XHTMLValidationType.None);

                    //Lo almacena en la misma ubicacion con formato Docx
                    string ContentDocx = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + documento.numeroDocumento + "1.docx";
                    document.SaveToFile(ContentDocx, FileFormat.Docx);

                    //Carga el template
                    string templateDocument = AppDomain.CurrentDomain.BaseDirectory + "Content\\template.docx";
                    var templateOficio = DocX.Load(templateDocument);

                    //reemplaza los numeros adentro del documento
                    templateOficio.ReplaceText("[numeroOficio]", documento.numeroDocumento, false);
                    templateOficio.ReplaceText("[numeroIngreso]", documento.numeroIngreso, false);

                    //carga e inserta el documento que se convirtio de html to docx y lo guarda
                    var docxHtml = DocX.Load(ContentDocx);
                    templateOficio.InsertDocument(docxHtml, true);

                    //lo almacena en el escritorio
                    path = path.Remove(path.Length - 5);
                    path += ".docx";
                    templateOficio.SaveAs(path);

                    //Recupera el docx que genera y crea un PDF
                    document.LoadFromFile(path, FileFormat.PDF, XHTMLValidationType.None);
                    path = path.Remove(path.Length - 5);
                    path += ".pdf";
                    document.SaveToFile(path, FileFormat.PDF);
                    //Launch Document  
                   // System.Diagnostics.Process.Start(path);
                    try
                    {
                        path = path.Remove(path.Length - 4);
                        path += ".html";
                        System.IO.File.Delete(path);
                        path = path.Remove(path.Length - 5);
                        path += "1.docx";
                        System.IO.File.Delete(path);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index", new { message = "error" });
                    }
                }
                TempData["DocumentoId"] = documento.numeroDocumento;
                return RedirectToAction("Index",new { message = "success" });
            }
            return RedirectToAction("Index", new { message = "error" });
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
                return RedirectToAction("Index", new { message = "error" });
            }
            DocumentoViewModel documento = new DocumentoViewModel();
            documento.texto = "";
            documento.fecha = DateTime.Now.Date;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Crear",documento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmitirDocumento(Documento documento)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                documentoBll.Agregar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            return RedirectToAction("Index", new { message = "error" });
        }

        public ActionResult Emitir()
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
            documento.numeroDocumento = getNumeroDocumento();
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Responder", documento);
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
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                documentoBll.Actualizar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            return RedirectToAction("Index", new { message = "error" });
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
                return RedirectToAction("Index", new { message = "error" });
            }
            Documento documento = documentoBll.GetDocumento(id);
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
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                documento.idReferencia = documentoBll.GetNumeroReferencia(documento.idDocumento);
                documento.idTipo = documentoBll.Get(documento.idDocumento).idTipo;
                documento.tipoOrigen = documentoBll.Get(documento.idDocumento).tipoOrigen;
                documento.idOrigen = documentoBll.Get(documento.idDocumento).idOrigen;
                documento.numeroDocumento = documentoBll.Get(documento.idDocumento).numeroDocumento;
                documento.numeroIngreso = documentoBll.Get(documento.idDocumento).numeroIngreso;
                documentoBll.Actualizar(documento);
                documentoBll.SaveChanges();
                if(documento.numeroIngreso == null)
                {
                    return RedirectToAction("IndexSalidas");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index", new { message = "error" });
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
                return RedirectToAction("Index", new { message = "error" });
            }

            Documento documento = documentoBll.Get(id);
            DocumentoViewModel documentoVista = (DocumentoViewModel)documento;
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
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                documentoBll.Agregar(documento);
                documentoBll.GeneraNumeroIngreso();
                documentoBll.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            return RedirectToAction("Index", new { message = "error" });
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
                return RedirectToAction("Index", new { message = "error" });
            }
            DocumentoViewModel documentoVista = new DocumentoViewModel();

            documentoVista.fecha = DateTime.Now;
            documentoVista.numeroDocumento = getNumeroDocumento();
            documentoVista.idTipo = tablaGeneralBLL.GetIdTablaGeneral("Documentos","tipo","Oficio");
            documentoVista.idOrigen = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "idOrigen", "Servicios jurídicos");
            documentoVista.tipoOrigen = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "tipoOrigen", "Departamento Interno");
            documentoVista.idEstado = tablaGeneralBLL.GetIdTablaGeneral("Documentos", "estado", "Activo");
            if (documentoBll.GetDocumento(id).idReferencia == null)
            {
                documentoVista.idReferencia = documentoBll.GetDocumento(id).idDocumento;
            }
            else
            {
                documentoVista.idReferencia = documentoBll.GetDocumento(id).idReferencia;
            }

            documentoVista.numeroVinculado = documentoBll.GetReferencias((int)documentoVista.idReferencia).Where(oDcumento => oDcumento.numeroIngreso != null).First().numeroDocumento; 
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", documentoVista.idTipo);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", documentoVista.tipoOrigen);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", documentoVista.idOrigen);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", documentoVista.idEstado);
            return PartialView("Responder", documentoVista);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Archivar(int id)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
                documentoBll.ArchivarDocumento(id);
                documentoBll.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
        }

        public ActionResult Referencias(string option, string search, string currentFilter, string sortOrder, int? page)
        {
            try
            {
                if (page == null)
                {
                    page = 1;
                }
                List<Documento> listaDocumentos = new List<Documento>();
                PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page.Value, 4);
                return PartialView("Referencias", model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
        }

        public string getNumeroDocumento()
        {
            try
            {
                string numeroDocumento = (documentoBll.GetNumeroIngreso() + 1).ToString();
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
            catch (Exception)
            {
                return null;
            }
        }

        public JsonResult ComprobarDocumento(string numeroDocumento, string idDocumento)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
                if (idDocumento == "0")
                {
                    if (documentoBll.Comprobar(numeroDocumento, idDocumento, true, true))
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("El número de ingreso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (documentoBll.Comprobar(numeroDocumento, idDocumento, true, false))
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("El número de ingreso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public JsonResult ComprobarIngreso(string numeroIngreso, string idDocumento)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
                if (idDocumento == "0")
                {
                    if (documentoBll.Comprobar(numeroIngreso, idDocumento, false, true))
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("El número de ingreso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (documentoBll.Comprobar(numeroIngreso, idDocumento, false, false))
                    {
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("El número de ingreso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public JsonResult ComprobarDropdownList(string id)
        {
            try
            {
                if (id != "")
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Debe seleccionar una opción válida.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public JsonResult Search(string name)
        {
            try
            {
                documentoBll = new DocumentoBLLImpl();
                var resultado = documentoBll.Find(x => x.numeroDocumento.Contains(name)).Select(x => x.numeroDocumento).Take(11).ToList();
                return Json(resultado, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
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
                return this.Json(new { Data = ViewBag.idOrigen }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public JsonResult GetNomenclatura(int idOrigen, int tipoOrigen)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                documentoBll = new DocumentoBLLImpl();

                string sIdOrigen = tablaGeneralBLL.GetTablaGeneral(idOrigen).nomenclatura;
                if (tablaGeneralBLL.GetTablaGeneral(tipoOrigen).descripcion == "Departamento Interno")
                {
                    return Json(new { data = tablaGeneralBLL.GetTablaGeneral(idOrigen).nomenclatura + "-" },JsonRequestBehavior.AllowGet);
                }
                else
                {
                    
                    return Json(new { data = "SSISCT-" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}