using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public class CasoBLLImpl : BLLGenericoImpl<Caso>, ICasoBLL
    {
        public bool Agregar(Caso caso)
        {
            this.Add(caso);
            return true;
        }

        public bool Modificar(Caso caso)
        {
            this.Update(caso);
            return true;
        }
    }
}
