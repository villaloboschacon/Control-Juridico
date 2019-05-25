using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
        List<Caso> aCasos = new List<Caso>();
        private IUsuarioBLL usuarioBLL;
        private IPersonasBLL personaBLL;

        public ActionResult Index(string sOption, string sSearch, int page = 1, int pageSize = 7, string message = "")
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
                PagedList<Caso> model = new PagedList<Caso>(new List<Caso>(), page, pageSize);
                return View(model);
            }

            int iTipo = tablaGeneralBLL.GetIdTablaGeneral("Casos", "tipo", "Judicial");

            if (!string.IsNullOrEmpty(message))
            {
                TempData["message"] = message;
            }
            else
            {
                TempData["message"] = "";
            }

            //Busqueda cuando se selecciona el tipo de busqueda y personaliza la busqueda
            if (!String.IsNullOrEmpty(sOption) && !String.IsNullOrEmpty(sSearch))
            {
                //El ultimo valor es falso por que no tiene que tener numero de ingreso
                try
                {
                    ViewBag.search = sSearch;
                    ViewBag.option = sOption;
                    if (casoBLL.Consulta(iTipo, sSearch, sOption) != null)
                    {
                        aCasos = casoBLL.Consulta(iTipo, sSearch, sOption);
                        foreach (Caso caso in aCasos)
                        {
                            caso.Persona = personaBLL.Get(caso.idPersona);
                            caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                            caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                            caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                            caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                        }
                    }

                    PagedList<Caso> model = new PagedList<Caso>(aCasos, page, pageSize);
                    return View(model);
                }
                catch (Exception)
                {
                    PagedList<Caso> model = new PagedList<Caso>(new List<Caso>(), page, pageSize);
                    return View(model);
                }

            }
            else
            {
                aCasos = casoBLL.getCasosJudiciales();
                foreach (Caso caso in aCasos)
                {
                    caso.Persona = personaBLL.Get(caso.idPersona);
                    caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                    caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                    caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                    caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
                }
                PagedList<Caso> model = new PagedList<Caso>(aCasos, page, pageSize);
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
            catch (Exception ex)
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
                string correo = casoBLL.getCorreo(caso.idUsuario);

                try
                {
                    MailMessage mail = new MailMessage();
                    // mail.To.Add(email);
                    mail.To.Add(correo);
                    mail.From = new MailAddress(correo);
                    mail.Subject = "Asignación de caso";

                    mail.Body = "El departamento de Servicios Juridicos de la Municipalidad de Alajuela te informa que se te ha asignado un nuevo caso. " +
                        "Ingresa al <a href=\"http://localhost:53772/CasoJudicial/Index?SOption=Número+de+proceso&sSearch="
                        + caso.numeroCaso + "\">link</a> para ver la informacion relacionada a este.  ";


                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         ("pruebamuni0@gmail.com", "munpru08_"); // ***use valid credentials***
                    smtp.Port = 587;

                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    //print("Exception in sendEmail:" + ex.Message);
                }


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
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
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
                caso.idTipo = casoBLL.getTipoCaso(caso.idCaso);
                casoBLL.Modificar(caso);
                casoBLL.SaveChanges();
                string correo = casoBLL.getCorreo(caso.idUsuario);

                try
                {
                    MailMessage mail = new MailMessage();
                    // mail.To.Add(email);
                    mail.To.Add(correo);
                    mail.From = new MailAddress(correo);
                    mail.Subject = "Asignación de caso";

                    mail.Body = "El departamento de Servicios Juridicos de la Municipalidad de Alajuela te informa que se te ha modificado la información del caso " + caso.numeroCaso + ". " +
                        "Ingresa al <a href=\"http://localhost:53772/CasoJudicial/Index?SOption=Número+de+proceso&sSearch="
                        + caso.numeroCaso + "\" > link</a> para ver la información actualizada de este.  ";

                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential
                         ("pruebamuni0@gmail.com", "munpru08_"); // ***use valid credentials***
                    smtp.Port = 587;

                    //Or your Smtp Email ID and Password
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    //print("Exception in sendEmail:" + ex.Message);
                }
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Editar", (CasoViewModel)caso);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Archivar(int id)
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

            Caso caso = casoBLL.Get(id);
            if (ModelState.IsValid)
            {
                casoBLL.archivaCaso(id);
                casoBLL.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", caso.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", caso.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", caso.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return RedirectToAction("Index");
        }

        public JsonResult ComprobarCaso(string numeroCaso, string idCaso)
        {
            casoBLL = new CasoBLLImpl();
            if (casoBLL.Comprobar(numeroCaso, idCaso))
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

        public ActionResult Detalles(int id)
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
                return View();
            }
            Caso caso = casoBLL.Get(id);
            CasoViewModel casoVista = (CasoViewModel)caso;
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion", casoVista.idTipo);
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion", casoVista.idEstado);
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion", casoVista.tipoLitigante);
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto", casoVista.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", casoVista.idUsuario);
            return PartialView("Detalle", casoVista);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DetallesCasos(Caso caso)
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
                return View();
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
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(2), "idPersona", "nombreCompleto", caso.idPersona);
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre", caso.idUsuario);
            return PartialView("Detalle", caso);
        }

    }
}