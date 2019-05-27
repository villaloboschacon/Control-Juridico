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
        private IPersonasBLL PersonasBLL;
        private ITablaGeneralBLL TablaGeneralBLL;
        List<Persona> aPersonas = new List<Persona>();


        public ActionResult Index(string sOption, string sSearch, int page = 1, int pageSize = 9, string message = "")
        {
            try
            {
                TablaGeneralBLL = new TablaGeneralBLLImpl();
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                PagedList<Documento> model = new PagedList<Documento>(new List<Documento>(), page, pageSize);
                return View(model);
            }

            int iTipo = TablaGeneralBLL.GetIdTablaGeneral("Persona", "Tipo", "Fisica");


            if (!string.IsNullOrEmpty(message))
            {
                TempData["message"] = message;
            }
            else
            {
                TempData["message"] = "";
            }
            if (!String.IsNullOrEmpty(sSearch) && !String.IsNullOrEmpty(sOption))
            {
                try
                {
                    ViewBag.search = sSearch;
                    ViewBag.option = sOption;
                    if (PersonasBLL.Consulta(iTipo, sSearch, sOption) != null)
                    {
                        aPersonas = PersonasBLL.Consulta(iTipo, sSearch, sOption);
                        foreach (Persona oPersona in aPersonas)
                        {
                            oPersona.TablaGeneral = TablaGeneralBLL.GetTablaGeneral(oPersona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                        }
                    }
                    PagedList<Persona> model = new PagedList<Persona>(aPersonas, page, pageSize);
                    return View(model);
                }
                catch (Exception)
                {
                    PagedList<Persona> model = new PagedList<Persona>(aPersonas, page, pageSize);
                    return View(model);
                }
            }
            else
            {
                //ViewBag.idPersona = new SelectList(TablaGeneralBLL.Consulta("Persona", "Tipo"), "idTablaGeneral", "descripcion");
                aPersonas = PersonasBLL.getPersonas(iTipo);
                foreach (Persona oPersona in aPersonas)
                {
                    oPersona.TablaGeneral = TablaGeneralBLL.GetTablaGeneral(oPersona.idTipo);
                }
                PagedList<Persona> model = new PagedList<Persona>(aPersonas, page, pageSize);
                return View(model);
            }
        }

        public ActionResult Crear()
        {
            try
            {
                TablaGeneralBLL = new TablaGeneralBLLImpl();
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            PersonaViewModel oPersonaViewModel = new PersonaViewModel();
            ViewBag.idTipo = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Crear", oPersonaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPersona(Persona oPersona)
        {
            try
            {
                TablaGeneralBLL = new TablaGeneralBLLImpl();
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                PersonasBLL.Agregar(oPersona);
                PersonasBLL.SaveChanges();
                TempData["cedula"] = oPersona.cedula;
                return RedirectToAction("Index", new { message = "success" });
            }
            return RedirectToAction("Index", new { message = "error" });
        }

        public ActionResult Editar(int id)
        {
            try
            {
                TablaGeneralBLL = new TablaGeneralBLLImpl();
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            PersonaViewModel oPersonaViewModel = (PersonaViewModel)PersonasBLL.GetPersona(id);
            ViewBag.idTipo = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", oPersonaViewModel.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", oPersonaViewModel.tipoIdentificacion);
            return PartialView("Editar", oPersonaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersona(Persona Persona)
        {
            try
            {
                TablaGeneralBLL = new TablaGeneralBLLImpl();
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                PersonasBLL.Actualizar(Persona);
                PersonasBLL.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            ViewBag.idTipo = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", Persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", Persona.tipoIdentificacion);
            return RedirectToAction("Index", new { message = "error" });
        }

        public ActionResult Eliminar(int iId)
        {
            try
            {
                PersonasBLL = new PersonasBLLImpl();
                Persona oPersona = PersonasBLL.GetPersona(iId);
                PersonasBLL.Eliminar(oPersona);
                PersonasBLL.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
        }

        public JsonResult ComprobarPersona(string cedula, string idPersona)
        {
            try
            {
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }

            if (PersonasBLL.Comprobar(cedula, idPersona))
            {
                return Json(true,"", JsonRequestBehavior.AllowGet);
            }
            else
            {            
                return Json("Este número de identificación ya ha sido registrado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Detalles(int id)
        {
            try
            {
                TablaGeneralBLL = new TablaGeneralBLLImpl();
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            Persona persona = PersonasBLL.Get(id);
            PersonaViewModel personaVista = (PersonaViewModel)persona;
            ViewBag.idTipo = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", personaVista.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", personaVista.tipoIdentificacion);
            return PartialView("Detalle", personaVista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesPersonas(Persona persona)
        {
            try
            {
                TablaGeneralBLL = new TablaGeneralBLLImpl();
                PersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                PersonasBLL.Actualizar(persona);
                PersonasBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Detalle", persona);
        }
    }
}
