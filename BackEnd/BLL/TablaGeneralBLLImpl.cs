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
            catch (Exception e)
            {
                e = new Exception();
                throw new NotImplementedException();
            }
        }
        public TablaGeneral getTablaGeneral(string tabla, string campo)
        {
            try
            {
                List<TablaGeneral> lista;
                using (unidad = new UnidadDeTrabajo<TablaGeneral>(new SCJ_BDEntities()))
                {
                    Expression<Func<TablaGeneral, bool>> consulta = (d => d.tabla.Equals(tabla) && d.campo.Equals(campo));
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
        public int getIdTablaGeneral(string tabla, string campo, string descripcion)
        {
            try
            {
                List<TablaGeneral> lista;
                using (unidad = new UnidadDeTrabajo<TablaGeneral>(new SCJ_BDEntities()))
                {
                    Expression<Func<TablaGeneral, bool>> consulta = (d => d.tabla.Equals(tabla) && d.campo.Equals(campo) && d.descripcion.Equals(descripcion));
                    lista = unidad.genericDAL.Find(consulta).ToList();
                }
                if (lista.Count == 1)
                {
                    return lista[0].idTablaGeneral;
                }
                return -1;
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
