using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public interface IRollBLL : IBLLGenerico<Role>
    {
        ///<summary>
        ///Obtiene el rol dependiendo de la descripcion
        ///</summary>
        Role GetRol(string descripcion);
    }
}
