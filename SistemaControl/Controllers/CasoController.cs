using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BackEnd.BLL;
using BackEnd.Model;
using PagedList;
using SistemaControl.Models;

namespace SistemaControl.Controllers
{
    public class CasoController : Controller
    {
        private ICasoBLL casoBLL;
        private ITablaGeneralBLL tablaGeneralBLL;
        private IUsuarioBLL usuarioBLL;
        private IPersonasBLL personaBLL;
        List<Caso> aCasos = new List<Caso>();

        public ActionResult Index(string sOption, string sSearch, string sSearchFecha, int page = 1, int pageSize = 7, string message = "")
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception)
            {
                PagedList<Caso> model = new PagedList<Caso>(new List<Caso>(), page, pageSize);
                return View(model);
            }
            int iTipo = tablaGeneralBLL.GetIdTablaGeneral("Casos", "tipo", "Administrativo");

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
                    if (casoBLL.Consulta(iTipo, sSearch, sOption) != null)
                    {

                        aCasos = casoBLL.Consulta(iTipo, sSearch, sOption);

                        foreach (Caso caso in aCasos)
                        {
                            caso.TablaGeneral = tablaGeneralBLL.Get(caso.idCaso);
                            caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idPersona);
                            caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.idTipo);
                            //if (caso.idEstado.HasValue)
                            //{
                            //    int i = (int)(oDocumento.idEstado);
                            //    oDocumento.TablaGeneral = tablaGeneralBLL.Get(i);
                            //}
                        }
                    }
                    PagedList<Caso> model = new PagedList<Caso>(aCasos, page, pageSize); 
                    return View(model);
                }
                catch (Exception)
                {
                    PagedList<Caso> model = new PagedList<Caso>(aCasos, page, pageSize);
                    return View(model);
                }

            }
            //Busqueda cuando no pone la busqueda ni la opcion de busqueda
            //Cuando se inicia el index
            else if (String.IsNullOrEmpty(sOption) && String.IsNullOrEmpty(sSearch))
            {
                PagedList<Caso> model = new PagedList<Caso>(aCasos, page, pageSize); 
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
                        aCasos = casoBLL.Consulta(iTipo, sSearch, "");
                    }
                    else
                    {
                        aCasos = casoBLL.Consulta(iTipo, sSearch, "Número de Oficio");
                    }
                    foreach (Caso caso in aCasos)
                    {
                        caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idUsuario);
                        caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.idTipo);
                        caso.TablaGeneral = tablaGeneralBLL.Get(caso.tipoLitigante);
                        caso.Persona = personaBLL.GetPersona(caso.idPersona);
                        caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                    }
                    PagedList<Caso> model = new PagedList<Caso>(aCasos, page, pageSize); 
                    return View(model);
                }
                catch (Exception)
                {
                    PagedList<Caso> model = new PagedList<Caso>(aCasos, page, pageSize);
                    return View(model);
                }
            }
        }
       
        public ActionResult Crear()
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception)
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
            catch (Exception)
            {

            }

            if (ModelState.IsValid)
            {
                casoBLL.Agregar(caso);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
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
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion",caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion",caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion",caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto",caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre",caso.idUsuario);
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
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Editar", (CasoViewModel)caso);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Archivar(int id)
        {
          try
            {
                casoBLL = new CasoBLLImpl();
                casoBLL.archivaCaso(id);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }


        public JsonResult ComprobarCaso(string numeroCaso,string idCaso)
        {
            try
            {
                casoBLL = new CasoBLLImpl();
            }
            catch (Exception ex)
            {

            }
            if (casoBLL.Comprobar(numeroCaso,idCaso))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de caso no se encuentra disponible.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
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

        public JsonResult Search(string name)
        {
            var resultado = casoBLL.Find(x => x.numeroCaso.Equals(name)).Select(x => x.numeroCaso).Take(11).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
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
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", casoVista.idPersona);
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
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Detalle", caso);
        }
    }
}