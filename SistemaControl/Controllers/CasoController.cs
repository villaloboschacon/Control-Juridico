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
    public class CasoController : Controller
    {
        private ICasoBLL casoBLL;
        private ITablaGeneralBLL tablaGeneralBLL;
        private IUsuarioBLL usuarioBLL;
        private IPersonasBLL personaBLL;

        public CasoController()
        {

        }
        public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }
            if (option == "Materia")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.materia == search && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else if (option == "Abogado")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.numeroCaso == search && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else if (option == "Tipo de litigante")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.tipoLitigante == Int32.Parse(search) && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else if (option == "Número de caso")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.numeroCaso == search && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else if (option == "Estado")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.idEstado == Int32.Parse(search) && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else {
 
                ViewBag.tipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos","tipoLitigio"), "idTablaGeneral", "descripcion");
                ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listacaso = casoBLL.Find(x => search == null && x.idTipo == 19).ToList();
                foreach (Caso caso in listacaso)
                {
                    caso.Persona = personaBLL.Get(caso.idPersona);
                    caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                    caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                    caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                    caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                }
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                List<Caso> documento = casoBLL.GetAll();
                return View(model);
            }
        }
        public ActionResult Crear()
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch(Exception ex)
            {

            }
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
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }

            if (ModelState.IsValid)
            {
                casoBLL.Agregar(caso);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
            return PartialView("Crear", caso);
        }

        public ActionResult Editar(int id)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }

            CasoViewModel caso = (CasoViewModel)casoBLL.Get(id);
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion",caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion",caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion",caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto",caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre",caso.idUsuario);
            return PartialView("Editar", caso);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarCaso(Caso caso)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
            }
            catch (Exception ex)
            {

            }
            if (ModelState.IsValid)
            {
                casoBLL.Modificar(caso);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Editar", (CasoViewModel)caso);
        }


        public JsonResult ComprobarCaso(string numeroCaso,string idCaso)
        {
            try
            {
                casoBLL = new CasoBLLImpl();
            }
            catch (Exception ex)
            {

            }
            if (casoBLL.Comprobar(numeroCaso,idCaso))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("El número de caso no se encuentra disponible.\n Por favor inténtelo de nuevo.", JsonRequestBehavior.AllowGet);
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
                        ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
                        break;
                }
            }
            catch (Exception ex)
            {
                ex = new Exception();
            }
            return this.Json(new { Id = "idPersona", Reg = "Supermercado", Data = ViewBag.idPersona }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Search(string name)
        {
            var resultado = casoBLL.Find(x => x.numeroCaso.Equals(name)).Select(x => x.numeroCaso).Take(11).ToList();
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }
    }
}