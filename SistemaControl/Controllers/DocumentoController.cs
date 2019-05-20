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
    //[Authorize]
    public class DocumentoController : Controller
    {
        private IDocumentoBLL documentoBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        public int pageglobal;

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

            List<Documento> aDocumentos = new List<Documento>();
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
                    if (documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, false) != null) {

                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, sOption, true);

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
                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha,"", true);
                    }
                    else
                    {
                        aDocumentos = documentoBll.Consulta(iTipo, sSearch, sSearchFecha, "Número de Oficio", true);
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

        public ActionResult IndexSalidas(string option, string search,string sSearchFecha, int page = 1, int pageSize = 7, string message = "")
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

            if (!String.IsNullOrEmpty(option) && !String.IsNullOrEmpty(search))
            {
                //El ultimo valor es falso por que no tiene que tener numero de ingreso
                try
                {
                    ViewBag.search = search;
                    ViewBag.option = option;
                    if (documentoBll.Consulta(iTipo, search, sSearchFecha,option, false) != null)
                    {

                        aDocumentos = documentoBll.Consulta(iTipo, search, sSearchFecha,option, false);

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
            else if (String.IsNullOrEmpty(option) && String.IsNullOrEmpty(search))
            {
                PagedList<Documento> model = new PagedList<Documento>(aDocumentos, page, pageSize);
                return View(model);
            }
            else
            {
                try
                {
                    ViewBag.search = search;
                    ViewBag.option = option;
                    if (String.IsNullOrEmpty(search))
                    {
                        aDocumentos = documentoBll.Consulta(iTipo, search, sSearchFecha, "", false);
                    }
                    else
                    {
                        aDocumentos = documentoBll.Consulta(iTipo, search, sSearchFecha, "Número de Oficio", false);
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

        public ActionResult IndexReferencias(string option,int id, string search, int page = 1, int pageSize = 15)
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
            search = id.ToString();

            if (!String.IsNullOrEmpty(search))
            {
                //var doc = documentoBll.Find(x => x.numeroDocumento.Equals(search) && x.idTipo == 3 && x.idEstado != 9 || search == null).ToList();
                var doc = documentoBll.Find(x => x.idDocumento.Equals(id) && x.idTipo == 3 && x.idEstado != 9).ToList();
                var referencias = documentoBll.GetReferencias(doc[0].idReferencia);
                var listaDocumentos = referencias;

              //  listaDocumentos.Union(listaDocumentos);
                if (!String.IsNullOrEmpty(search))
                {
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
            
            else
            {
                var documentos = documentoBll.Find(x => search == null && x.idTipo == 3 && x.idEstado != 9 || x.idTipo == 23);
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
                documentoBll.Agregar(documento);
                documentoBll.SaveChanges();
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
                System.Diagnostics.Process.Start(path);
                try
                {
                    path = path.Remove(path.Length - 4);
                    path += ".html";
                    System.IO.File.Delete(path);
                    path = path.Remove(path.Length - 5);
                    path += "1.docx";
                    System.IO.File.Delete(path);
                }
                catch (Exception ex)
                {

                }
                documento.texto = " ";

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
                return View();
            }
            DocumentoViewModel documento = new DocumentoViewModel();
            documento.texto = "";
            documento.fecha = DateTime.Now.Date;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "tipoOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idOrigen = new SelectList(tablaGeneralBLL.Consulta("Documentos", "idOrigen"), "idTablaGeneral", "descripcion", 0);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", null);
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
                return View();
            }
            if (ModelState.IsValid)
            {
                documentoBll.Agregar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index");
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
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Documentos", "estado"), "idTablaGeneral", "descripcion", null);
            return PartialView("Crear", documento);
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
                return RedirectToAction("Index");
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
                return View();
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
                return View();
            }
            if (ModelState.IsValid)
            {
                documentoBll.GeneraNumeroIngreso();
                documentoBll.Actualizar(documento);
                documentoBll.SaveChanges();
                return RedirectToAction("Index", new { message = "error" });
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
                return View();
            }

            Documento documento = documentoBll.GetDocumento(id);
            DocumentoViewModel documentoVista = (DocumentoViewModel)documento;
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
                return View();
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
            documentoVista.numeroVinculado = documentoBll.GetDocumento(id).numeroDocumento;
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
                listaDocumentos = documentoBll.GetEntradas();
            }
            else
            {
                listaDocumentos = documentoBll.GetSalidas();
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
                return Json("El número de ingreso no se encuentra disponible.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ComprobarDropdownList(string id)
        {
            if (id != "")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //ModelState.AddModelError("idTipo", "Last Name is required.");
                //return Json(new { success = false, errors = "Debe seleccionar una opción válida.\n Por favor inténtelo de nuevo." }, JsonRequestBehavior.AllowGet);
                return Json("Debe seleccionar una opción válida.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
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