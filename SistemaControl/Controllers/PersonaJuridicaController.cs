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
        private IPersonasBLL personaBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        public PersonaJuridicaController()
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            personaBll = new PersonasBLLImpl();
        }
        public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        {

            if (option == "Nombre")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listapersona = personaBll.Find(x => x.nombreCompleto == search && x.idTipo == 2 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
                return View(model);
            }
            else if (option == "Cédula")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listapersona = personaBll.Find(x => x.cedula == search && x.idTipo == 2 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
                return View(model);
            }
            else if (option == "Representante legal")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listapersona = personaBll.Find(x => x.RepresentanteLegal == search && x.idTipo == 2 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
                return View(model);
            }
            else if (option == "Representante social")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listapersona = personaBll.Find(x => x.RepresentanteSocial == search && x.idTipo == 2 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
                return View(model);
            }
            else if (option == "Observación")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listapersona = personaBll.Find(x => x.RepresentanteSocial == search && x.idTipo == 2 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listapersona = personaBll.Find(x => x.cedula == search && x.idTipo == 2 || x.nombreCompleto == search && x.idTipo == 2 || x.idPersona.ToString() == search && x.idTipo == 2 || x.idTipo.ToString() == search && x.idTipo == 2 || search == null && x.idTipo == 2).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
                return View(model);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPersona(Persona persona)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                personaBll.Agregar(persona);
                personaBll.SaveChanges();
                return RedirectToAction("Index");
            }
            PersonaViewModel personaVista = (PersonaViewModel)persona;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            return PartialView("Crear", personaVista);
        }

        public ActionResult Crear()
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }

            PersonaViewModel persona = new PersonaViewModel();
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Crear", persona);
        }

        public ActionResult Editar(int id)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            personaBll = new PersonasBLLImpl();
            Persona persona = personaBll.Get(id);
            PersonaViewModel personaVista = new PersonaViewModel();
            personaVista = (PersonaViewModel)persona;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            return PartialView("Editar", personaVista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersona(Persona persona)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            personaBll = new PersonasBLLImpl();
            if (ModelState.IsValid)
            {
                personaBll.Modificar(persona);
                personaBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            return PartialView("Editar", persona);
        }
        public JsonResult ComprobarPersona(string cedula, string idPersona)
        {
            try
            {
                personaBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }
          
            if (personaBll.Comprobar(cedula, idPersona))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Este número de cédula ya ha sido registrado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }
        }

    }
}