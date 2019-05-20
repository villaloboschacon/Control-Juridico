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
                List<Usuario> lista;
                using (unidad = new UnidadDeTrabajo<Usuario>(new SCJ_BDEntities()))
                {
                    Expression<Func<Usuario, bool>> consulta = (d => d.idEstado.Equals(12));
                    lista = unidad.genericDAL.Find(consulta).ToList();
                }
                return lista;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        //public string[] getIdRol(string idUsuario)
        //{
        //    string[] result;
        //    try
        //    {
        //        using (context = new SCJ_BDEntities())
        //        {
        //            result = context.sp_getRolesForUser(idUsuario).ToArray();
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }



        //}

        public Usuario getUsuario(string username)
        {
            try
            {
                List<Usuario> lista;
                using (unidad = new UnidadDeTrabajo<Usuario>(new SCJ_BDEntities()))
                {
                    Expression<Func<Usuario, bool>> consulta = (d => d.usuario1.Equals(username));
                    lista = unidad.genericDAL.Find(consulta).ToList();
                }
                if (lista.Count == 1)
                {
                    return lista[0];
                }
                return null;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public string gerRolForUser(string userName)
        {
            string[] result;
            using (context = new SCJ_BDEntities())
            {
                result = context.sp_getRolesForUser(userName).ToArray();
            }
            return result[0];

        }
    }
}
