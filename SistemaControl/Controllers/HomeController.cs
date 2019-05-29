using BackEnd.BLL;
using BackEnd.Model;
using PagedList;
using SistemaControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaControl.Controllers
{
    public class HomeController : Controller
    {
        private ICasoBLL casoBLL;
        private ITablaGeneralBLL tablaGeneralBLL;
        private IUsuarioBLL usuarioBLL;
        private IPersonasBLL personaBLL;
        private IDocumentoBLL documentoBLL;
        private IPersonasBLL personasBLL;

        public ActionResult Index(string user, int page = 1, int pageSize = 7)
        {
            try
            {
                tablaGeneralBLL = new TablaGeneralBLLImpl();
                casoBLL = new CasoBLLImpl();
                personaBLL = new PersonasBLLImpl();
                usuarioBLL = new UsuarioBLLImpl();
                documentoBLL = new DocumentoBLLImpl();
                personasBLL = new PersonasBLLImpl();
            }
            catch (Exception)
            {
                return View();
            }
            HomeViewModel dashboard = new HomeViewModel();
            int iTipoAdministrativo = tablaGeneralBLL.GetIdTablaGeneral("Casos", "tipo", "Administrativo");
            int iTipoJudicial = tablaGeneralBLL.GetIdTablaGeneral("Casos", "tipo", "Judicial");
            dashboard.numeroAdministrativos = casoBLL.Consulta(iTipoAdministrativo, User.Identity.Name, "Abogado").Count();
            dashboard.numeroJudiciales = casoBLL.Consulta(iTipoJudicial, User.Identity.Name, "Abogado").Count();
            dashboard.numeroCasos = dashboard.numeroJudiciales + dashboard.numeroAdministrativos;


            dashboard.numeroDocumentos = documentoBLL.Find(x => x.idEstado != 9).Count;
            dashboard.numEntradas = documentoBLL.Find(x => x.idTipo == 3 && x.idEstado != 9 && x.idOrigen != 85).Count;
            dashboard.numSalidas = documentoBLL.Find(x => x.idTipo == 3 && x.idEstado != 9 && x.idOrigen == 85).Count;
            dashboard.numExpedientes = documentoBLL.Find(x => x.idTipo == 4 && x.idEstado != 9).Count;

            dashboard.numeroPersonas = personasBLL.GetAll().Count;
            dashboard.numeroFisicas = personasBLL.Find(x => x.idTipo == 1).Count;
            dashboard.numeroJuridicas = personasBLL.Find(x => x.idTipo == 2).Count;

            ViewBag.numeroCasos = dashboard.numeroCasos;
            ViewBag.numeroAdministrativos = dashboard.numeroAdministrativos;
            ViewBag.numeroJudiciales = dashboard.numeroJudiciales;

            ViewBag.numeroDocumentos = dashboard.numeroDocumentos;
            ViewBag.numEntradas = dashboard.numEntradas;
            ViewBag.numSalidas = dashboard.numSalidas;
            ViewBag.numExpedientes = dashboard.numExpedientes;

            ViewBag.numeroPersonas = dashboard.numeroPersonas;
            ViewBag.numeroFisicas = dashboard.numeroFisicas;
            ViewBag.numeroJuridicas = dashboard.numeroJuridicas;
           
            var casosSearch = casoBLL.Find(x => x.Usuario.nombre.Equals(User.Identity.Name) && (x.idTipo == 19 || x.idTipo == 20) && x.idEstado != 95).ToList();
            ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
            ViewBag.idEstado = new SelectList(tablaGeneralBLL.Consulta("Casos", "estado"), "idTablaGeneral", "descripcion");
            ViewBag.TipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipoLitigio"), "idTablaGeneral", "descripcion");
            ViewBag.idPersona = new SelectList(personaBLL.Consulta(1), "idPersona", "nombreCompleto");
            ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
            foreach (Caso caso in casosSearch)
            {
                caso.Persona = personaBLL.Get(caso.idPersona);
                caso.Usuario = usuarioBLL.Get(caso.idUsuario);
                caso.TablaGeneral = tablaGeneralBLL.Get(caso.idEstado);
                caso.TablaGeneral1 = tablaGeneralBLL.Get(caso.idTipo);
                caso.TablaGeneral2 = tablaGeneralBLL.Get(caso.tipoLitigante);
            }
            PagedList<Caso> modelpage = new PagedList<Caso>(casosSearch, page, pageSize);
            return View("Index", modelpage);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Ayuda()
        {
            ViewBag.Message = "Your help page.";

            return View();
        }
        public ActionResult Welcome()
        {
            ViewBag.Message = "Your welcome page.";

            return View();
        }
    }
}