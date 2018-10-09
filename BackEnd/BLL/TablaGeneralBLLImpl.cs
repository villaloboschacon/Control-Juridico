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
    public class TablaGeneralBLLImpl:BLLGenericoImpl<TablaGeneral>, ITablaGeneralBLL
    {
        private UnidadDeTrabajo<TablaGeneral> unidad;


        public List<TablaGeneral> Consulta(string tabla,string campo)
        {
            try
            {
                List<TablaGeneral> lista;
                using (unidad = new UnidadDeTrabajo<TablaGeneral>(new SCJ_BDEntities()))
                {
                    Expression<Func<TablaGeneral, bool>> consulta = (d => d.tabla.Equals(tabla) && d.campo.Equals(campo));
                    lista = unidad.genericDAL.Find(consulta).ToList();
                }
                return lista;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
