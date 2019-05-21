using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using BackEnd.BLL;
using BackEnd.Model;
using PagedList;
using SistemaControl.Models;

namespace SistemaControl.Controllers
{
    public class CasoJudicialController : Controller
    {
        // GET: CasoJudicial
        private ICasoBLL casoBLL;
        private ITablaGeneralBLL tablaGeneralBLL;
        private IUsuarioBLL usuarioBLL;
        private IPersonasBLL personaBLL;
        private static PagedList<Caso> model; // variable se cambio

        public JsonResult Search(string name)
        {
            var resultado = casoBLL.Find(x => x.numeroCaso.Equals(name)).Select(x => x.numeroCaso).Take(11).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
                casoBLL = new CasoBLLImpl();
            }
            catch (Exception ex)
            {
                return null;
            }

            if (option == "Número de proceso" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                //ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                //List<Caso> listaCaso = casoBLL.Find(x => x.numeroCaso == search && x.idTipo == 20 || search == null).ToList();
                //PagedList<Caso> model = new PagedList<Caso>(listaCaso, page, pageSize);
                //return View(model.ToPagedList(page, pageSize));
                var casosSearch = casoBLL.Find(x => x.numeroCaso.Contains(search) && x.idTipo == 20 && x.idEstado != 95 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
                    ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
                    ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                    ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                    foreach (Caso caso in casosSearch)
                    {
                        caso.Persona = personaBLL.Get(caso.idPersona);
                        caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                        caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                        caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                        caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                    }
                }
                PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
                return View(model);
            }
            else if (option == "Abogado" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                //ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                //List<Caso> listaCaso = casoBLL.Find(x => x.materia == search && x.idTipo == 20 || search == null).ToList();
                //PagedList<Caso> model = new PagedList<Caso>(listaCaso, page, pageSize);
                //return View(model);
                var casosSearch = casoBLL.Find(x => x.Usuario.nombre.Contains(search) && x.idTipo == 20 && x.idEstado != 95 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
                    ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
                    ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                    ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                    foreach (Caso caso in casosSearch)
                    {
                        caso.Persona = personaBLL.Get(caso.idPersona);
                        caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                        caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                        caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                        caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                    }
                }
                PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
                return View(model);
            }
            else if (option == "Persona" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                //ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                //List<Caso> listaCaso = casoBLL.Find(x => x.descripcion == search && x.idTipo == 20 || search == null).ToList();
                //PagedList<Caso> model = new PagedList<Caso>(listaCaso, page, pageSize);
                //return View(model);
                var casosSearch = casoBLL.Find(x => x.Persona.nombreCompleto.Contains(search) && x.idTipo == 20 && x.idEstado != 95 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
                    ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
                    ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                    ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                    foreach (Caso caso in casosSearch)
                    {
                        caso.Persona = personaBLL.Get(caso.idPersona);
                        caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                        caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                        caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                        caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                    }
                }
                PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
                return View(model);
            }
            else if (option == "Estado" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                //ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                //List<Caso> listaCaso = casoBLL.Find(x => x.observacion == search && x.idTipo == 20 || search == null).ToList();
                //PagedList<Caso> model = new PagedList<Caso>(listaCaso, page, pageSize);
                //return View(model);
                var casosSearch = casoBLL.Find(x => x.TablaGeneral.descripcion.Contains(search) && x.idTipo == 20 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
                    ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
                    ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                    ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                    foreach (Caso caso in casosSearch)
                    {
                        caso.Persona = personaBLL.Get(caso.idPersona);
                        caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                        caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                        caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                        caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                    }
                }
                PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
                return View(model);
            }
            else if (option == "" || String.IsNullOrEmpty(search))
            {
                search = null;
                option = "";
                ViewBag.tipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
                ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listacaso = casoBLL.Find(x => search == null && x.idTipo == 20 && x.idEstado != 95).ToList();
                foreach (Caso caso in listacaso)
                {
                    caso.Persona = personaBLL.Get(caso.idPersona);
                    caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                    caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                    caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                    caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                }
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.tipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
                ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listacaso = casoBLL.Find(x => search == null && x.idTipo == 20 && x.idEstado != 95).ToList();
                foreach (Caso caso in listacaso)
                {
                    caso.Persona = personaBLL.Get(caso.idPersona);
                    caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                    caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                    caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                    caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                }
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
        }

       

        //public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        //{
        //    try
        //    {
        //        tablaGeneralBLL = new TablaGeneralBLLImpl();
        //        casoBLL = new CasoBLLImpl();
        //        personaBLL = new PersonasBLLImpl();
        //        usuarioBLL = new UsuarioBLLImpl();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    if (option == "Abogado" && !String.IsNullOrEmpty(search))
        //    {
        //        //List<Caso> listacaso = casoBLL.Find(x => x.numeroCaso == search && x.idCaso == 3 || search == null).ToList();
        //        //PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
        //        //return View(model);
        //        var casosSearch = casoBLL.Find(x => x.Usuario.nombre.Contains(search) && x.idTipo == 20 || search == null).ToList();
        //        if (!String.IsNullOrEmpty(search))
        //        {
        //            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
        //            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
        //            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
        //            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
        //            foreach (Caso caso in casosSearch)
        //            {
        //                caso.Persona = personaBLL.Get(caso.idPersona);
        //                caso.Usuario = usuarioBLL.Get(caso.idUsuario);
        //                caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
        //                caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
        //                caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
        //            }
        //        }
        //        PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Persona" && !String.IsNullOrEmpty(search))
        //    {
        //        //List<Caso> listacaso = casoBLL.Find(x => x.tipoLitigante == Int32.Parse(search) && x.idCaso == 3 || search == null).ToList();
        //        //PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
        //        //return View(model);
        //        var casosSearch = casoBLL.Find(x => x.Persona.nombreCompleto.Contains(search) && x.idTipo == 20 || search == null).ToList();
        //        if (!String.IsNullOrEmpty(search))
        //        {
        //            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
        //            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
        //            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
        //            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
        //            foreach (Caso caso in casosSearch)
        //            {
        //                caso.Persona = personaBLL.Get(caso.idPersona);
        //                caso.Usuario = usuarioBLL.Get(caso.idUsuario);
        //                caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
        //                caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
        //                caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
        //            }
        //        }
        //        PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Número de proceso" && !String.IsNullOrEmpty(search))
        //    {
        //        //List<Caso> listacaso = casoBLL.Find(x => x.numeroCaso == search && x.idCaso == 3 || search == null).ToList();
        //        //PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
        //        //return View(model);
        //        //var casosSearch = casoBLL.GetAll();
        //        var casosSearch = casoBLL.Find(x => x.numeroCaso.Contains(search) && x.idTipo == 20 || search == null).ToList();
        //        if (!String.IsNullOrEmpty(search))
        //        {
        //            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
        //            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
        //            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
        //            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
        //            foreach (Caso caso in casosSearch)
        //            {
        //                caso.Persona = personaBLL.Get(caso.idPersona);
        //                caso.Usuario = usuarioBLL.Get(caso.idUsuario);
        //                caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
        //                caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
        //                caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
        //            }
        //        }
        //        PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Estado" && !String.IsNullOrEmpty(search))
        //    {
        //        //List<Caso> listacaso = casoBLL.Find(x => x.idEstado == Int32.Parse(search) && x.idCaso == 3 || search == null).ToList();
        //        //PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
        //        //return View(model);
        //        //var casosSearch = casoBLL.GetAll();
        //        var casosSearch = casoBLL.Find(x => x.TablaGeneral.descripcion.Contains(search) && x.idTipo == 20 || search == null).ToList();
        //        if (!String.IsNullOrEmpty(search))
        //        {
        //            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
        //            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
        //            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
        //            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
        //            foreach (Caso caso in casosSearch)
        //            {
        //                caso.Persona = personaBLL.Get(caso.idPersona);
        //                caso.Usuario = usuarioBLL.Get(caso.idUsuario);
        //                caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
        //                caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
        //                caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
        //            }
        //        }
        //        PagedList<Caso> model = new PagedList<Caso>(casosSearch, page, pageSize);
        //        return View(model);
        //    }
        //    else
        //    {

        //        ViewBag.tipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
        //        ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
        //        ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
        //        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Caso> listacaso = casoBLL.Find(x => search == null && x.idTipo == 20).ToList();
        //        foreach (Caso caso in listacaso)
        //        {
        //            caso.Persona = personaBLL.Get(caso.idPersona);
        //            caso.Usuario = usuarioBLL.Get(caso.idUsuario);
        //            caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
        //            caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
        //            caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
        //        }
        //        PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
        //        List<Caso> documento = casoBLL.GetAll();
        //        return View(model);
        //    }
        //}

        //public ActionResult Index(string option, string search, string currentFilter, string sortOrder, int? page)
        //{
        //    try
        //    {
        //        tablaGeneralBLL = new TablaGeneralBLLImpl();
        //        personaBLL = new PersonasBLLImpl();
        //        usuarioBLL = new UsuarioBLLImpl();
        //        casoBLL = new CasoBLLImpl();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex = new Exception();
        //        return View();
        //    }
        //    ViewBag.CurrentSort = sortOrder;
        //    if (search != null)
        //    {
        //        page = 1;
        //    }
        //    else
        //    {
        //        search = currentFilter;
        //    }
        //    ViewBag.CurrentFilter = search;

        //    int pageSize = 4;
        //    int pageNumber = (page ?? 1);

        //    if (option == "Número de Caso")
        //    {
        //        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Caso> listaCaso = casoBLL.Find(x => x.numeroCaso == search && x.idTipo == 20 || search == null).ToList();
        //        PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
        //        return View(model.ToPagedList(pageNumber, pageSize));
        //    }
        //    else if (option == "Materia")
        //    {
        //        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Caso> listaCaso = casoBLL.Find(x => x.materia == search && x.idTipo == 20 || search == null).ToList();
        //        PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Descripción")
        //    {
        //        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Caso> listaCaso = casoBLL.Find(x => x.descripcion == search && x.idTipo == 20 || search == null).ToList();
        //        PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Observación")
        //    {
        //        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Caso> listaCaso = casoBLL.Find(x => x.observacion == search && x.idTipo == 20 || search == null).ToList();
        //        PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
        //        return View(model);
        //    }
        //    else
        //    {
        //        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
        //        ViewBag.NumeroCaso = String.IsNullOrEmpty(sortOrder) ? "CasoDes" : "";
        //        var casos = from s in casoBLL.Find(x => search == null && x.idTipo == 20) select s;

        //        switch (sortOrder)
        //        {
        //            case "CasoDes":
        //                casos = casos.OrderByDescending(s => s.numeroCaso);
        //                break;
        //            default:
        //                casos = casos.OrderBy(s => s.numeroCaso);
        //                break;
        //        }
        //        List<Caso> listacasos = casos.ToList();
        //        foreach (Caso caso in listacasos)
        //        {
        //            tablaGeneralBLL = new TablaGeneralBLLImpl();
        //            personaBLL = new PersonasBLLImpl();
        //            usuarioBLL = new UsuarioBLLImpl();
        //            caso.Persona = personaBLL.Get(caso.idPersona);
        //            caso.Usuario = usuarioBLL.Get(caso.idUsuario);
        //            caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
        //            caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
        //            caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
        //        }
        //        PagedList<Caso> model = new PagedList<Caso>(listacasos, pageNumber, pageSize);
        //        return View(model);
        //    }
        //}

        public ActionResult Crear()
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }

            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
            CasoViewModel caso = new CasoViewModel();
            return PartialView("Crear", caso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearCaso(Caso caso)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }

            if (ModelState.IsValid)
            {
                casoBLL.Agregar(caso);
                casoBLL.SaveChanges();
                string correo = casoBLL.getCorreo(caso.idUsuario);

                try
                {
                    MailMessage mail = new MailMessage();
                   // mail.To.Add(email);
                    mail.To.Add(correo);
                    mail.From = new MailAddress(correo);
                    mail.Subject = "Asignación de caso";
                   
                    mail.Body= "El departamento de Servicios Juridicos de la Municipalidad de Alajuela te informa que se te ha asignado un nuevo caso. " +
                        "Ingresa al <a href=\"http://localhost:53772/CasoJudicial/Index?search=" + caso.numeroCaso +
                        "&submit=Buscar&option=Número+de+proceso\">link</a> para ver la informacion relacionada a este.  ";
                    
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         ("pruebamuni0@gmail.com", "munpru08_"); // ***use valid credentials***
                    smtp.Port = 587;

                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    //print("Exception in sendEmail:" + ex.Message);
                }
            

                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto");
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
            return PartialView("Crear", caso);
        }

        public ActionResult Editar(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }

            CasoViewModel caso = (CasoViewModel)casoBLL.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Editar", caso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCaso(Caso caso)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }
            if (ModelState.IsValid)
            {
                casoBLL.Modificar(caso);
                casoBLL.SaveChanges();
                string correo = casoBLL.getCorreo(caso.idUsuario);

                try
                {
                    MailMessage mail = new MailMessage();
                    // mail.To.Add(email);
                    mail.To.Add(correo);
                    mail.From = new MailAddress(correo);
                    mail.Subject = "Asignación de caso";

                    mail.Body = "El departamento de Servicios Juridicos de la Municipalidad de Alajuela te informa que se te ha modificado la información del caso "+caso.numeroCaso+". " +
                        "Ingresa al <a href=\"http://localhost:53772/CasoJudicial/Index?search=" + caso.numeroCaso +
                        "&submit=Buscar&option=Número+de+proceso\">link</a> para ver la información actualizada de este.  ";

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         ("pruebamuni0@gmail.com", "munpru08_"); // ***use valid credentials***
                    smtp.Port = 587;

                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    //print("Exception in sendEmail:" + ex.Message);
                }
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Editar", (CasoViewModel)caso);
        }

        

        //public ActionResult Eliminar(int id)
        //{
        //    try
        //    {
        //        tablaGeneralBLL = new TablaGeneralBLLImpl();
        //        casoBLL = new CasoBLLImpl();
        //        personaBLL = new PersonasBLLImpl();
        //        usuarioBLL = new UsuarioBLLImpl();
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    Caso caso = casoBLL.Get(id);
        //    if (ModelState.IsValid)
        //    {
        //        casoBLL.Eliminar(caso);
        //        casoBLL.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
        //    ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
        //    ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
        //    ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
        //    ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
        //    return PartialView("Index", (CasoViewModel)caso);
        //}

        [HttpPost, ValidateInput(false)]
        public ActionResult Archivar(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }

            Caso caso = casoBLL.Get(id);
            if (ModelState.IsValid)
            {
                casoBLL.archivaCaso(id);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return RedirectToAction("Index");
        }

        public JsonResult ComprobarCaso(string numeroCaso,string idCaso)
        {
            casoBLL = new CasoBLLImpl();
            if (casoBLL.Comprobar(numeroCaso,idCaso))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de caso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult GetTipoPersona(int id)
        {
            try
            {
                casoBLL = new CasoBLLImpl();
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBLL = new PersonasBLLImpl();
                switch (id)
                {
                    case 19:
                        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                        ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                        break;
                    case 20:
                        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                        ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto");
                        break;
                    default:
                        ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                        ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                        break;
                }
            }
            catch (Exception ex)
            {
                ex = new Exception();
            }
            return this.Json(new { Id = "idPersona", Reg = "Supermercado", Data = ViewBag.idPersona }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Detalles(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            Caso caso = casoBLL.Get(id);
            CasoViewModel casoVista = (CasoViewModel)caso;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", casoVista.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", casoVista.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", casoVista.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto", casoVista.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", casoVista.idUsuario);
            return PartialView("Detalle", casoVista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesCasos(Caso caso)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                casoBLL.Modificar(caso);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Detalle", caso);
        }

        // PDF Casos  judicial 
        public ActionResult ReportesCasosJudicial()
        {

            List<Caso> _casos = new List<Caso>();


            foreach (Caso iTem in model)
            {
                Caso caso = new Caso();

                TablaGeneral tablaGeneral = new TablaGeneral();
                TablaGeneral tablaGeneral1 = new TablaGeneral();
                TablaGeneral tablaGeneral2 = new TablaGeneral();
                Persona persona = new Persona();
                Usuario usuario = new Usuario();


                caso.numeroCaso = iTem.numeroCaso.ToString();

                usuario.nombre = iTem.Usuario.nombre;
                caso.Usuario = usuario;

                caso.materia = iTem.materia.ToString();

                persona.nombreCompleto = iTem.Persona.nombreCompleto;
                caso.Persona = persona;

                tablaGeneral1.descripcion = iTem.TablaGeneral1.descripcion;
                caso.TablaGeneral1 = tablaGeneral1;

                tablaGeneral2.descripcion = iTem.TablaGeneral2.descripcion;
                caso.TablaGeneral2 = tablaGeneral2;

                tablaGeneral.descripcion = iTem.TablaGeneral.descripcion;
                caso.TablaGeneral = tablaGeneral;



                _casos.Add(caso);

            }

            DateTime fecha = DateTime.Now;
            string _fecha = fecha.ToString("dd/MM/yyyy");

            ReporteModel reportes = new ReporteModel();
            byte[] abyte = reportes.PrepareReportCasoJudicial(_casos, _fecha);
            return File(abyte, "application/pdf");
        }

    }
}