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
    public class CasoJudicialController : Controller
    {
        // GET: CasoJudicial
        private ICasoBLL casoBLL;
        private ITablaGeneralBLL tablaGeneralBLL;
        private IUsuarioBLL usuarioBLL;
        private IPersonasBLL personaBLL;

        public JsonResult Search(string name)
        {
            var resultado = casoBLL.Find(x => x.numeroCaso.Equals(name)).Select(x => x.numeroCaso).Take(11).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index(string option, string search, string currentFilter, string sortOrder, int? page)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
                casoBLL = new CasoBLLImpl();
            }
            catch (Exception ex)
            {
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
            ViewBag.CurrentFilter = search;

            int pageSize = 4;
            int pageNumber = (page ?? 1);

            if (option == "Número de Caso")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listaCaso = casoBLL.Find(x => x.numeroCaso == 1 && x.idTipo == 20 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
                return View(model.ToPagedList(pageNumber, pageSize));
            }
            else if (option == "Materia")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listaCaso = casoBLL.Find(x => x.materia == search && x.idTipo == 20 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
                return View(model);
            }
            else if (option == "Descripción")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listaCaso = casoBLL.Find(x => x.descripcion == search && x.idTipo == 20 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
                return View(model);
            }
            else if (option == "Observación")
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listaCaso = casoBLL.Find(x => x.observacion == search && x.idTipo == 20 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listaCaso, pageNumber, pageSize);
                return View(model);
            }
            else
            {
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                ViewBag.NumeroCaso = String.IsNullOrEmpty(sortOrder) ? "CasoDes" : "";
                var casos = from s in casoBLL.Find(x => search == null && x.idTipo == 20) select s;

                switch (sortOrder)
                {
                    case "CasoDes":
                        casos = casos.OrderByDescending(s => s.numeroCaso);
                        break;
                    default:
                        casos = casos.OrderBy(s => s.numeroCaso);
                        break;
                }
                List<Caso> listacasos = casos.ToList();
                foreach (Caso caso in listacasos)
                {
                    tablaGeneralBLL = new TablaGeneralBLLImpl();
                    personaBLL = new PersonasBLLImpl();
                    usuarioBLL = new UsuarioBLLImpl();
                    caso.Persona = personaBLL.Get(caso.idPersona);
                    caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                    caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                    caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                    caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                }
                PagedList<Caso> model = new PagedList<Caso>(listacasos, pageNumber, pageSize);
                return View(model);
            }
        }

        public ActionResult Crear()
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            casoBLL = new CasoBLLImpl();
            personaBLL = new PersonasBLLImpl();
            usuarioBLL = new UsuarioBLLImpl();

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
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            casoBLL = new CasoBLLImpl();
            personaBLL = new PersonasBLLImpl();
            usuarioBLL = new UsuarioBLLImpl();

            if (ModelState.IsValid)
            {
                casoBLL.Agregar(caso);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto");
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
            return PartialView("Crear", caso);
        }

        public ActionResult Editar(int id)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            casoBLL = new CasoBLLImpl();
            personaBLL = new PersonasBLLImpl();
            usuarioBLL = new UsuarioBLLImpl();
            Caso caso = casoBLL.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto");
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
            return PartialView("Editar", caso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCaso(Caso caso)
        {
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            casoBLL = new CasoBLLImpl();
            personaBLL = new PersonasBLLImpl();
            usuarioBLL = new UsuarioBLLImpl();

            if (ModelState.IsValid)
            {
                casoBLL.Modificar(caso);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto");
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
            return PartialView("Editar", caso);
        }

        public JsonResult ComprobarCaso(int numeroCaso)
        {
            casoBLL = new CasoBLLImpl();
            if (casoBLL.Comprobar(numeroCaso))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de caso no se encuentra disponible o ya se encuentra ocupado.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
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
                        ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto");
                        break;
                }
            }
            catch (Exception ex)
            {
            }
            return this.Json(new { Id = "idPersona", Reg = "Supermercado", Data = ViewBag.idPersona }, JsonRequestBehavior.AllowGet);

        }


    }
}