using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public interface IUsuarioBLL : IBLLGenerico<Usuario>
    {      
        List<Usuario> Consulta();
        Usuario getUsuario(string username);
        string gerRolForUser(string userName);
    }
}
