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
        Role getRol(string descripcion);
    }
}
