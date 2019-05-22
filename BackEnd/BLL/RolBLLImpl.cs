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

        public Role GetRol(string sDescripcion)
        {
            try
            {
                List<Role> aRoles;
                using (unidad = new UnidadDeTrabajo<Role>(new SCJ_BDEntities()))
                {
                    Expression<Func<Role, bool>> consulta = (oRoles => oRoles.descripcion.Equals(sDescripcion));
                    aRoles = unidad.genericDAL.Find(consulta).ToList();
                }
                if (aRoles.Count == 1)
                {
                    return aRoles.First();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
