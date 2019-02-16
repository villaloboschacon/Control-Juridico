using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    class RolBLLImpl : BLLGenericoImpl<Role>, IRollBLL
    {
        private UnidadDeTrabajo<Role> unidad;
        public Role getRol(string descripcion)
        {
            try
            {
                List<Role> lista;
                using (unidad = new UnidadDeTrabajo<Role>(new SCJ_BDEntities()))
                {
                    Expression<Func<Role, bool>> consulta = (d => d.descripcion.Equals(descripcion));
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
    }
}
