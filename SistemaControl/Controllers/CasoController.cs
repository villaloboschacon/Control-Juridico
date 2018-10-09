using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BackEnd.BLL;
using BackEnd.Model;
using PagedList;


namespace SistemaControl.Controllers
{
    public class CasoController : Controller
    {
        private ICasoBLL casoBLL;
        private ITablaGeneralBLL tablaGeneralBLL;
        private IUsuarioBLL usuarioBLL;
        public CasoController()
        {
            usuarioBLL = new UsuarioBLLImpl();
            tablaGeneralBLL = new TablaGeneralBLLImpl();
            casoBLL = new CasoBLLImpl();

        }
        public ActionResult Index(string option, string search, int page = 1, int pageSize = 4)
        {
            if (option == "Materia")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.materia == search && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else if (option == "Abogado")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.numeroCaso == Int32.Parse(search) && x.idCaso == 3 || search == null).ToList();
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
                List<Caso> listacaso = casoBLL.Find(x => x.numeroCaso == Int32.Parse(search) && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            else if (option == "Estado")
            {
                List<Caso> listacaso = casoBLL.Find(x => x.idEstado == Int32.Parse(search) && x.idCaso == 3 || search == null).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                return View(model);
            }
            //else if (option == "Fecha")
            //{
            //    List<Documento> listaDocumentos = documentoBll.Find(x => x.fecha == DateTime.TryParseExact(search, "yyyy-MM-dd HH:mm:ss,fff") && x.idDocumento == 5 || search == null).ToList();
            //    PagedList<Documento> model = new PagedList<Documento>(listaDocumentos, page, pageSize);
            //    return View(model);
            //}
            else { 
            
                ViewBag.tipoLitigante = new SelectList(tablaGeneralBLL.Consulta("Casos","tipoLitigio"), "idTablaGeneral", "descripcion");
                ViewBag.idUsuario = new SelectList(usuarioBLL.Consulta(), "idUsuario", "nombre");
                ViewBag.idTipo = new SelectList(tablaGeneralBLL.Consulta("Casos", "tipo"), "idTablaGeneral", "descripcion");
                List<Caso> listacaso = casoBLL.Find(x => search == null && x.idTipo == 19).ToList();
                PagedList<Caso> model = new PagedList<Caso>(listacaso, page, pageSize);
                List<Caso> documento = casoBLL.GetAll();
                return View(model);
            }
        }
    }
}