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

        public List<TablaGeneral> Consulta(string sTabla,string sCampo)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<TablaGeneral>(new SCJ_BDEntities()))
                {
                    Expression<Func<TablaGeneral, bool>> consulta = (oTablaGeneral => oTablaGeneral.tabla.Equals(sTabla) && oTablaGeneral.campo.Equals(sCampo));
                    return unidad.genericDAL.Find(consulta).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TablaGeneral GetTablaGeneral(string tabla, string campo)
        {
            try
            {
                List<TablaGeneral> aTablaGeneral;
                using (unidad = new UnidadDeTrabajo<TablaGeneral>(new SCJ_BDEntities()))
                {
                    Expression<Func<TablaGeneral, bool>> consulta = (oTablaGeneral => oTablaGeneral.tabla.Equals(tabla) && oTablaGeneral.campo.Equals(campo));
                    aTablaGeneral = unidad.genericDAL.Find(consulta).ToList();
                }
                if (aTablaGeneral.Count == 1)
                {
                    return aTablaGeneral.First();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TablaGeneral GetTablaGeneral(int iId)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<TablaGeneral>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.Get(iId);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetIdTablaGeneral(string sTabla, string sCampo, string sDescripcion)
        {
            try
            {
                List<TablaGeneral> aTablaGeneral;
                using (unidad = new UnidadDeTrabajo<TablaGeneral>(new SCJ_BDEntities()))
                {
                    Expression<Func<TablaGeneral, bool>> consulta = (oTablaGeneral => oTablaGeneral.tabla.Equals(sTabla) && oTablaGeneral.campo.Equals(sCampo) && oTablaGeneral.descripcion.Equals(sDescripcion));
                    aTablaGeneral = unidad.genericDAL.Find(consulta).ToList();
                }
                if (aTablaGeneral.Count == 1)
                {
                    return aTablaGeneral.First().idTablaGeneral;
                }
                return -1;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
