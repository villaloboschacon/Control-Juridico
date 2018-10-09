using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Model;
namespace BackEnd.BLL
{
    public interface IPersonasBLL:IBLLGenerico<Persona>
    {
        List<Persona> ConsultaPorNombre(string nombre);
        bool Agregar(Persona persona);
        bool Modificar(Persona persona);
    }
}
