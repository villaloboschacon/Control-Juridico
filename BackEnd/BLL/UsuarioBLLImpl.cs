using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DAL;
using BackEnd.Model;

namespace BackEnd.BLL
{
    public class UsuarioBLLImpl : BLLGenericoImpl<Usuario>, IUsuarioBLL
    {
        private UnidadDeTrabajo<Usuario> unidad;
        private SCJ_BDEntities context;

        public List<Usuario> Consulta()
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<Usuario>(new SCJ_BDEntities()))
                {
                    ITablaGeneralBLL tablaGeneralBLL = new TablaGeneralBLLImpl();
                    int iIdEstado = tablaGeneralBLL.GetIdTablaGeneral("Usuarios", "estado", "activo");
                    Expression<Func<Usuario, bool>> consulta = (oUsuario => oUsuario.idEstado.Equals(iIdEstado));
                    return unidad.genericDAL.Find(consulta).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Usuario GetUsuario(string sUsername)
        {
            try
            {
                List<Usuario> aUsuarios;
                using (unidad = new UnidadDeTrabajo<Usuario>(new SCJ_BDEntities()))
                {
                    Expression<Func<Usuario, bool>> consulta = (oUsuario => oUsuario.usuario1.Equals(sUsername));
                    aUsuarios = unidad.genericDAL.Find(consulta).ToList();
                }
                if (aUsuarios.Count == 1)
                {
                    return aUsuarios.First();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GetRol(string sUsername)
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    return context.sp_getRolesForUser(sUsername).ToArray().First();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
