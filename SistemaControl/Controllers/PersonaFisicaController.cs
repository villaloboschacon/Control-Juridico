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
        private IPersonasBLL personaBll;
        private ITablaGeneralBLL tablaGeneralBLL;

        public JsonResult Search(string filtro)
        {

            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBll = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return null;
            }
            ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            var data = personaBll.buscarNombre(filtro).ToList();

            var jsonData = Json(data, JsonRequestBehavior.AllowGet);
            return jsonData;
        }
        public ActionResult Referencias(string option, string search, string filtro, string sortOrder, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            var data = personaBll.buscarNombre(filtro).ToList();

            List<Persona> listaPersonas = data;

            PagedList<Persona> model = new PagedList<Persona>(listaPersonas, page.Value, 4);
            return View(model);
            //return PartialView("Referencias",);
        }
        public ActionResult Searchm(string searchString, int? page)
        {
            try

            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBll = new PersonasBLLImpl();
                searchString = "1667";

            }
            catch (Exception ex)
            {
                ex = new Exception();
                return View();
            }
            ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            var listaPersonas = personaBll.GetAll().AsQueryable();
            //var folders = db.Folders.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                listaPersonas = listaPersonas.Where(p => p.cedula.ToLower().Contains(searchString.ToLower()));
            }

            //int pageSize = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PersonaPageSize"]);
            int pageSize = 4;
            int pageNumber = (page ?? 1);

            //List<Persona> listaPersonas = personas.ToList();
            foreach (Persona persona in listaPersonas)
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                persona.TablaGeneral = tablaGeneralBLL.Get(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
            }
            PagedList<Persona> model = new PagedList<Persona>(listaPersonas, pageNumber, pageSize);
            return View(model);
           // return View(per.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
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

            if (option == "Cédula" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                var listaPersonas = personaBll.Find(x => x.cedula.Contains(search) && x.idTipo == 1 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                    foreach (Persona persona in listaPersonas)
                    {
                        persona.TablaGeneral = tablaGeneralBLL.Get(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                    }
                }
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, page, pageSize);
                return View(model);
            }
            else if (option == "Nombre Completo" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                var listaPersonas = personaBll.Find(x => x.nombreCompleto.Contains(search) && x.idTipo == 1 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                    foreach (Persona persona in listaPersonas)
                    {
                        persona.TablaGeneral = tablaGeneralBLL.Get(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                    }
                }
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, page, pageSize);
                return View(model);
            }
            else if (option == "Correo Electrónico" && !String.IsNullOrEmpty(search))
            {
                ViewBag.search = search;
                ViewBag.option = option;
                var listaPersonas = personaBll.Find(x => x.correo.Contains(search) && x.idTipo == 1 || search == null).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                    foreach (Persona persona in listaPersonas)
                    {
                        persona.TablaGeneral = tablaGeneralBLL.Get(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                    }
                }
                PagedList<Persona> model = new PagedList<Persona>(listaPersonas, page, pageSize);
                return View(model);
            }
            else if (option == "" || String.IsNullOrEmpty(search))
            {
                option = "";
                search = null;
                
                ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                var personas = personaBll.Find(x => search == null && x.idTipo == 1);
                foreach (Persona persona in personas)
                {
                    persona.TablaGeneral = tablaGeneralBLL.Get(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                }
                PagedList<Persona> model = new PagedList<Persona>(personas, page, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idPersona = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                var personas = personaBll.Find(x => search == null && x.idTipo == 1);
                foreach (Persona persona in personas)
                {
                    persona.TablaGeneral = tablaGeneralBLL.Get(persona.idTipo); //TablaGeneral es el {get;set} para poder traer idTipo de tabla general
                }
                PagedList<Persona> model = new PagedList<Persona>(personas, page, pageSize);
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
            ViewBag.tipoIdentificacion = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
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
            ViewBag.tipoIdentificacion = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", 0);
            return PartialView("Crear", persona);
        }

        public ActionResult Editar(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBll = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {

            }

            PersonaViewModel persona = (PersonaViewModel)personaBll.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion",persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);

            return PartialView("Editar", persona);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Eliminar(int id)
        {
            try
            {
                personaBll = new PersonasBLLImpl();
                Persona persona = personaBll.Get(id);
                personaBll.Eliminar(persona);
                personaBll.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPersona(Persona persona)
        {

            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBll = new PersonasBLLImpl();
            }
            catch (Exception ex)
            {

            }
            if (ModelState.IsValid)
            {
                personaBll.Modificar(persona);
                personaBll.SaveChanges();
                return RedirectToAction("Index"); ;
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion", persona.idTipo);
            ViewBag.tipoIdentificacion = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipoIdentificacion"), "idTablaGeneral", "descripcion", persona.tipoIdentificacion);
            return PartialView("Editar", (PersonaViewModel)persona);
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
                return Json(true,"", JsonRequestBehavior.AllowGet);
            }
            else
            {            
                return Json("Este número de identificación ya ha sido registrado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);

            }
        }
    }
}
