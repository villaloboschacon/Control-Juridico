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
    public class PersonaFisicaController : Controller
    {
        private IPersonasBLL oPersonasBLL;
        private ITablaGeneralBLL oTablaGeneralBLL;

        public ActionResult Searchm(string sSearch, int? iPage)
        {
            try
            {
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            var aPersonas = oPersonasBLL.GetPersonas().AsQueryable();

            if (!String.IsNullOrEmpty(sSearch))
            {
                aPersonas = aPersonas.Where(oPersona => oPersona.cedula.ToLower().Contains(sSearch.ToLower()));
            }

            int iPageSize = 4;
            int iPageNumber = (iPage ?? 1);

            foreach (Persona oPersona in aPersonas)
            {
                oPersona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(oPersona.idTipo);
            }
            PagedList<Persona> model = new PagedList<Persona>(aPersonas, iPageNumber, iPageSize);
            return View(model);
        }

        public ActionResult Index(string sOption, string sSearch, int page = 1, int pageSize = 4, string message = "")
        {
            try
            {
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (!string.IsNullOrEmpty(message))
            {
                TempData["message"] = "success";
            }
            else if (message == "error")
            {
                TempData["message"] = "error";
            }
            else
            {
                TempData["message"] = "";
            }
            if (!String.IsNullOrEmpty(sSearch))
            {
                ViewBag.search = sSearch;
                ViewBag.option = sOption;
                var aPersonas = oPersonasBLL.Consulta(1, sSearch, sOption);
                ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                foreach (Persona persona in aPersonas)
                {
                    persona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                }
                PagedList<Persona> model = new PagedList<Persona>(aPersonas, page, pageSize);
                return View(model);
            }

            //if (sOption == "Cédula" && !String.IsNullOrEmpty(sSearch))
            //{


            //    ViewBag.search = sSearch;
            //    ViewBag.option = sOption;
            //    var aPersonas = oPersonasBLL.Consulta(1, sSearch, sOption);
            //    if (!String.IsNullOrEmpty(sSearch))
            //    {
            //        ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            //        foreach (Persona persona in lPersonas)
            //        {
            //            persona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
            //        }
            //    }
            //    PagedList<Persona> model = new PagedList<Persona>(lPersonas, page, pageSize);
            //    return View(model);
            //}
            //else if (sOption == "Nombre Completo" && !String.IsNullOrEmpty(sSearch))
            //{
            //    ViewBag.search = sSearch;
            //    ViewBag.option = sOption;
            //    var listaPersonas = oPersonasBLL.Find(x => x.nombreCompleto.Contains(sSearch) && x.idTipo == 1 || sSearch == null).ToList();
            //    if (!String.IsNullOrEmpty(sSearch))
            //    {
            //        ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            //        foreach (Persona persona in listaPersonas)
            //        {
            //            persona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
            //        }
            //    }
            //    PagedList<Persona> model = new PagedList<Persona>(listaPersonas, page, pageSize);
            //    return View(model);
            //}
            //else if (sOption == "Correo Electrónico" && !String.IsNullOrEmpty(sSearch))
            //{
            //    ViewBag.search = sSearch;
            //    ViewBag.option = sOption;
            //    var listaPersonas = oPersonasBLL.Find(x => x.correo.Contains(sSearch) && x.idTipo == 1 || sSearch == null).ToList();
            //    if (!String.IsNullOrEmpty(sSearch))
            //    {
            //        ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            //        foreach (Persona persona in listaPersonas)
            //        {
            //            persona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
            //        }
            //    }
            //    PagedList<Persona> model = new PagedList<Persona>(listaPersonas, page, pageSize);
            //    return View(model);
            //}
            //else if (sOption == "" || String.IsNullOrEmpty(sSearch))
            //{
            //    sOption = "";
            //    sSearch = null;
                
            //    ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            //    var personas = oPersonasBLL.Find(x => sSearch == null && x.idTipo == 1);
            //    foreach (Persona persona in personas)
            //    {
            //        persona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
            //    }
            //    PagedList<Persona> model = new PagedList<Persona>(personas, page, pageSize);
            //    return View(model);
            //}
            else
            {
                ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                var aPersonas = oPersonasBLL.Consulta(1);
                foreach (Persona oPersona in aPersonas)
                {
                    oPersona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(oPersona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                }
                PagedList<Persona> model = new PagedList<Persona>(aPersonas, page, pageSize);
                return View(model);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPersona(Persona persona)
        {
            try
            {
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                oPersonasBLL.Agregar(persona);
                oPersonasBLL.SaveChanges();
                TempData["cedula"] = persona.cedula;
                return RedirectToAction("Index", new { message = "success" });
            }
            PersonaViewModel personaVista = (PersonaViewModel)persona;
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion",persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Crear", personaVista);
        }

        public ActionResult Crear()
        {
            try
            {
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }

            PersonaViewModel persona = new PersonaViewModel();
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Crear", persona);
        }

        public ActionResult Editar(int id)
        {
            try
            {
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }

            PersonaViewModel persona = (PersonaViewModel)oPersonasBLL.Get(id);
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion",persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);

            return PartialView("Editar", persona);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Eliminar(int id)
        {
            try
            {
                oPersonasBLL = new PersonasBLLImpl();
                Persona persona = oPersonasBLL.Get(id);
                oPersonasBLL.Eliminar(persona);
                oPersonasBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersona(Persona persona)
        {

            try
            {
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                oPersonasBLL.Actualizar(persona);
                oPersonasBLL.SaveChanges();
                return RedirectToAction("Index"); ;
            }
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Editar", (PersonaViewModel)persona);
        }

        public JsonResult ComprobarPersona(string cedula, string idPersona)
        {
            try
            {
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }

            if (oPersonasBLL.Comprobar(cedula, idPersona))
            {
                return Json(true,"", JsonRequestBehavior.AllowGet);
            }
            else
            {            
                return Json("Este número de identificación ya ha sido registrado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);

            }
        }
    }
}
