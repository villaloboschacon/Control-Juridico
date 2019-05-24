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
    public class PersonaJuridicaController : Controller
    {
        private IPersonasBLL PersonasBll;
        private ITablaGeneralBLL TablaGeneralBll;

        public ActionResult Index(string sOption, string sSearch, int page = 1, int pageSize = 4, string message = "")
        {
            try
            {
                TablaGeneralBll = new TablaGeneralBLLImpl();
                PersonasBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
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
                ViewBag.search = sSearch;
                ViewBag.option = sOption;
                int iTipo = TablaGeneralBll.GetIdTablaGeneral("Persona", "Tipo", "Júridica");
                var aPersonas = PersonasBll.Consulta(iTipo, sSearch, sOption, "Juridica");
                ViewBag.idPersona = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                foreach (Persona oPersona in aPersonas)
                {
                    oPersona.TablaGeneral = TablaGeneralBll.GetTablaGeneral(oPersona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                }
                PagedList<Persona> model = new PagedList<Persona>(aPersonas, page, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idPersona = new SelectList(TablaGeneralBll.Consulta("Persona", "Tipo"), "idTablaGeneral", "descripcion");
                int iTipo = TablaGeneralBll.GetIdTablaGeneral("Persona", "Tipo", "Júridica");
                var aPersonas = PersonasBll.Consulta(iTipo);
                foreach (Persona oPersona in aPersonas)
                {
                    oPersona.TablaGeneral = TablaGeneralBll.GetTablaGeneral(oPersona.idTipo);
                }
                PagedList<Persona> model = new PagedList<Persona>(aPersonas, page, pageSize);
                return View(model);
            }
        }
        //public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        //{

        //    if (option == "Nombre")
        //    {
        //        ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Persona> listapersona = PersonasBll.Find(x => x.nombreCompleto == search && x.idTipo == 2 || search == null).ToList();
        //        PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Cédula")
        //    {
        //        ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Persona> listapersona = PersonasBll.Find(x => x.cedula == search && x.idTipo == 2 || search == null).ToList();
        //        PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Representante legal")
        //    {
        //        ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Persona> listapersona = PersonasBll.Find(x => x.RepresentanteLegal == search && x.idTipo == 2 || search == null).ToList();
        //        PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Representante social")
        //    {
        //        ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Persona> listapersona = PersonasBll.Find(x => x.RepresentanteSocial == search && x.idTipo == 2 || search == null).ToList();
        //        PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
        //        return View(model);
        //    }
        //    else if (option == "Observación")
        //    {
        //        ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Persona> listapersona = PersonasBll.Find(x => x.RepresentanteSocial == search && x.idTipo == 2 || search == null).ToList();
        //        PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
        //        return View(model);
        //    }
        //    else
        //    {
        //        ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
        //        List<Persona> listapersona = PersonasBll.Find(x => x.cedula == search && x.idTipo == 2 || x.nombreCompleto == search && x.idTipo == 2 || x.idPersona.ToString() == search && x.idTipo == 2 || x.idTipo.ToString() == search && x.idTipo == 2 || search == null && x.idTipo == 2).ToList();
        //        PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
        //        return View(model);
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPersona(Persona oPersona)
        {
            try
            {
                TablaGeneralBll = new TablaGeneralBLLImpl();
                PersonasBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { message = "error" });
            }
            if (ModelState.IsValid)
            {
                PersonasBll.Agregar(oPersona);
                PersonasBll.SaveChanges();
                TempData["cedula"] = oPersona.cedula;
                return RedirectToAction("Index", new { message = "success" });
            }
            return RedirectToAction("Index", new { message = "error" });
        }

        public ActionResult Crear()
        {
            try
            {
                TablaGeneralBll = new TablaGeneralBLLImpl();
                PersonasBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }

            PersonaViewModel persona = new PersonaViewModel();
            ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", 0);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBll.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Crear", persona);
        }

        public ActionResult Editar(int id)
        {
            TablaGeneralBll = new TablaGeneralBLLImpl();
            PersonasBll = new PersonasBLLImpl();
            Persona persona = PersonasBll.Get(id);
            PersonaViewModel personaVista = new PersonaViewModel();
            personaVista = (PersonaViewModel)persona;
            ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBll.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Editar", personaVista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersona(Persona persona)
        {
            TablaGeneralBll = new TablaGeneralBLLImpl();
            PersonasBll = new PersonasBLLImpl();
            if (ModelState.IsValid)
            {
                PersonasBll.Actualizar(persona);
                PersonasBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBll.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Editar", persona);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Eliminar(int id)
        {
            try
            {
                PersonasBll = new PersonasBLLImpl();
                Persona persona = PersonasBll.Get(id);
                PersonasBll.Eliminar(persona);
                PersonasBll.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult ComprobarPersona(string cedula, string idPersona)
        {
            try
            {
                PersonasBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }
          
            if (PersonasBll.Comprobar(cedula, idPersona))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
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
                TablaGeneralBll = new TablaGeneralBLLImpl();
                PersonasBll = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            Persona persona = PersonasBll.Get(id);
            PersonaViewModel personaVista = (PersonaViewModel)persona;
            ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", personaVista.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBll.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", personaVista.tipoIdentificacion);
            return PartialView("Detalle", personaVista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesPersonas(Persona persona)
        {
            try
            {
                TablaGeneralBll = new TablaGeneralBLLImpl();
                PersonasBll = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                PersonasBll.Actualizar(persona);
                PersonasBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(TablaGeneralBll.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(TablaGeneralBll.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Detalle", persona);
        }

    }
}