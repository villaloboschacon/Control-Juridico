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
    public class PersonasController : Controller
    {
        private IPersonasBLL personaBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        public PersonasController()
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
        public ActionResult Crear()
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            personaBll = new PersonasBLLImpl();
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            PersonasViewModel persona = new PersonasViewModel();
            return PartialView("Crear", persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearPersona(Persona persona)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            personaBll = new PersonasBLLImpl();
            if (ModelState.IsValid)
            {
                personaBll.Agregar(persona);
                personaBll.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            return PartialView("Crear", persona);
        }

        public ActionResult Editar(int id)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            personaBll = new PersonasBLLImpl();
            Persona persona = personaBll.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            return PartialView("Editar", persona);
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
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
            return PartialView("Editar", persona);
        }

        public JsonResult ComprobarCedula(string cedula)
        {
            personaBll = new PersonasBLLImpl();
            if (personaBll.Comprobar(cedula, 1))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de cédula no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ComprobarNombreCompleto(string nom)
        {
            personaBll = new PersonasBLLImpl();
            if (personaBll.Comprobar(nom, 2))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El nombre de la persona no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
            }
        }

    }
}