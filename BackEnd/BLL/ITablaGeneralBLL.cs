using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;

namespace BackEnd.BLL
{
    public interface ITablaGeneralBLL:IBLLGenerico<TablaGeneral>
    {
        ///<summary>
        ///Recupera la lista de una tabla en un campo especifico. 
        ///</summary>
        List<TablaGeneral> Consulta(string sTabla, string sCampo);
        ///<summary>
        ///Recupera una Tabla General. 
        ///</summary>
        TablaGeneral GetTablaGeneral(string sTabla, string sCampo);
        ///<summary>
        ///Recupera una Tabla General por medio del id. 
        ///</summary>
        TablaGeneral GetTablaGeneral(int iId);
        ///<summary>
        ///Recupera el id de una Tabla General. 
        ///</summary>
        int GetIdTablaGeneral(string sTabla, string sCampo, string sDescripcion);
    }
}
