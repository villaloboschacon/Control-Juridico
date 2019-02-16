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
    [Authorize]
    public class PersonasController : Controller
    {
        private IPersonasBLL personaBll;
        private ITablaGeneralBLL tablaGeneralBLL;
        public PersonasController()
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            personaBll = new PersonasBLLImpl();
        }
        public ActionResult Index(string option, string search, int page = 1, int pageSize = 8)
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
            else if(option == "Representante legal")
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
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Persona", "tipo"), "idTablaGeneral", "descripcion");
                List<Persona> listapersona = personaBll.Find(x => x.cedula == search && x.idTipo==2|| x.nombreCompleto == search && x.idTipo == 2 || x.idPersona.ToString() == search && x.idTipo == 2 || x.idTipo.ToString() == search && x.idTipo == 2 || search == null && x.idTipo == 2).ToList();
                PagedList<Persona> model = new PagedList<Persona>(listapersona, page, pageSize);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Agregar(Persona persona)
        {
            personaBll.Agregar(persona);
            return RedirectToAction("Index", "Department");
        }

        public ActionResult Details(int id)
        {
            Persona persona = personaBll.Get(id);
            return PartialView("Detalles",persona);
        }
        [HttpPost]
        public ActionResult Index()
        {
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Personas","tipo"), "idTablaGeneral", "descripcion");
            List<Persona> listapersona = new List<Persona>();
            Persona persona = personaBll.Get(2);
            listapersona.Add(persona);
            PagedList<Persona> model = new PagedList<Persona>(listapersona, 1, 4);
            return View(model);
        } 
        //public ActionResult MyAction()
        //{
        //    var model = new PersonasViewModel();
        //    model.PersonaIdTemplate = new SelectList(tablaGeneralBLL.Consulta("personas"), "idTipo", "descripcion", 1);
        //    return View(model);
        //}

    }
}