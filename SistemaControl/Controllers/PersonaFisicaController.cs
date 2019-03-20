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
    public class PersonaFisicaController : Controller
    {
        private IPersonasBLL personaBll;
        private ITablaGeneralBLL tablaGeneralBLL;

        public JsonResult Search(string name)
        {
            var resultado = personaBll.Find(x => x.cedula.Contains(name)).Select(x => x.cedula).Take(11).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string option, string search, string currentFilter, string sortOrder, int? page)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBll = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {
                ex = new Exception();
                return View();
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
            ViewBag.CurrenFilter = search;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            if (option == "Cédula")
            {
                ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listaPersonas = personaBll.Find(x => x.cedula == search && x.idTipo == 1 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, pageNumber, pageSize);
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            else if (option == "Nombre Completo")
            {
                ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listaPersonas = personaBll.Find(x => x.cedula == search && x.idTipo == 1 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, pageNumber, pageSize);
                return View(model);
            }
            else if (option == "Correo")
            {
                ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listaPersonas = personaBll.Find(x => x.cedula == search && x.idTipo == 1 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, pageNumber, pageSize);
                return View(model);
            }
            else if (option == "Observación")
            {
                ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listaPersonas = personaBll.Find(x => x.cedula == search && x.idTipo == 1 || search == null).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, pageNumber, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                ViewBag.Cedula = String.IsNullOrEmpty(sortOrder) ? "cedulades" : "";
                ViewBag.NombreCompleto = sortOrder == "NombreCompleto" ? "nombrecomdes" : "NombreCompleto";

                var personas = from s in personaBll.Find(x => search == null && x.idTipo == 1) select s;

                switch (sortOrder)
                {
                    case "cedulades":
                        personas = personas.OrderByDescending(s => s.cedula);
                        break;
                    case "nombrecomdes":
                        personas = personas.OrderByDescending(s => s.nombreCompleto);
                        break;
                    case "NombreCompleto":
                        personas = personas.OrderBy(s => s.nombreCompleto);
                        break;
                    default:
                        personas = personas.OrderBy(s => s.cedula);
                        break;
                }
                List<Persona> listaPersonas = personas.ToList();
                foreach (Persona persona in listaPersonas)
                {
                    tablaGeneralBLL = new TablaGeneralBLLImpl();
                    persona.TablaGeneral = tablaGeneralBLL.Get(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                }
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, pageNumber, pageSize);
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
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion",persona.idTipo);
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
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion",persona.idTipo);
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
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion",persona.idTipo);
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
