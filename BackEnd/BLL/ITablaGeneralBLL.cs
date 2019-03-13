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
        List<TablaGeneral> Consulta(string tabla,string campo);
        TablaGeneral getTablaGeneral(string tabla, string campo);
        int getIdTablaGeneral(string tabla, string campo, string descripcion);
    }
}
