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

        public ActionResult Search(string sSearch, int? iPage)
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

        public ActionResult Index(string sOption, string sSearch, int iPage = 1, int iPageSize = 4, string sMessage = "")
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
            if (!string.IsNullOrEmpty(sMessage))
            {
                TempData["message"] = sMessage;
            }
            else
            {
                TempData["message"] = "";
            }
            if (!String.IsNullOrEmpty(sSearch) && !String.IsNullOrEmpty(sOption))
            {
                ViewBag.search = sSearch;
                ViewBag.option = sOption;
                int iTipo = oTablaGeneralBLL.GetIdTablaGeneral("Persona", "Tipo", "Fisica");
                var aPersonas = oPersonasBLL.Consulta(iTipo, sSearch, sOption);
                ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                foreach (Persona oPersona in aPersonas)
                {
                    oPersona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(oPersona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                }
                PagedList<Persona> model = new PagedList<Persona>(aPersonas, iPage, iPageSize);
                return View(model);
            }
            else
            {
                ViewBag.idPersona = new SelectList(oTablaGeneralBLL.Consulta("Persona", "Tipo"), "idTablaGeneral", "descripcion");
                int iTipo = oTablaGeneralBLL.GetIdTablaGeneral("Persona", "Tipo", "Fisica");
                var aPersonas = oPersonasBLL.Consulta(iTipo);
                foreach (Persona oPersona in aPersonas)
                {
                    oPersona.TablaGeneral = oTablaGeneralBLL.GetTablaGeneral(oPersona.idTipo);
                }
                PagedList<Persona> model = new PagedList<Persona>(aPersonas, iPage, iPageSize);
                return View(model);
            }
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
            PersonaViewModel oPersonaViewModel = new PersonaViewModel();
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Crear", oPersonaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPersona(Persona oPersona)
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
                oPersonasBLL.Agregar(oPersona);
                oPersonasBLL.SaveChanges();
                TempData["cedula"] = oPersona.cedula;
                return RedirectToAction("Index", new { message = "success" });
            }
            return RedirectToAction("Index", new { message = "error" });
        }



        public ActionResult Editar(int iId)
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
            PersonaViewModel oPersonaViewModel = (PersonaViewModel)oPersonasBLL.GetPersona(iId);
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", oPersonaViewModel.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", oPersonaViewModel.tipoIdentificacion);
            return PartialView("Editar", oPersonaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersona(Persona oPersona)
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
                oPersonasBLL.Actualizar(oPersona);
                oPersonasBLL.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", oPersona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", oPersona.tipoIdentificacion);
            return RedirectToAction("Index", new { message = "error" });
        }

        public ActionResult Eliminar(int iId)
        {
            try
            {
                oPersonasBLL = new PersonasBLLImpl();
                Persona oPersona = oPersonasBLL.GetPersona(iId);
                oPersonasBLL.Eliminar(oPersona);
                oPersonasBLL.SaveChanges();
                return RedirectToAction("Index", new { message = "success" });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
        }
        public JsonResult ComprobarPersona(string oCedula, string sIdPersona)
        {
            try
            {
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }

            if (oPersonasBLL.Comprobar(oCedula, sIdPersona))
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
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            Persona persona = oPersonasBLL.Get(id);
            PersonaViewModel personaVista = (PersonaViewModel)persona;
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", personaVista.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", personaVista.tipoIdentificacion);
            return PartialView("Detalle", personaVista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesPersonas(Persona persona)
        {
            try
            {
                oTablaGeneralBLL = new TablaGeneralBLLImpl();
                oPersonasBLL = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                oPersonasBLL.Actualizar(persona);
                oPersonasBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(oTablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Detalle", persona);
        }
    }
}
