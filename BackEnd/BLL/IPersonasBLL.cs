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
        List<Persona> Consulta(int idTipo);
        bool Agregar(Persona persona);
        bool Modificar(Persona persona);
        bool Comprobar(string cedula, string idPersona);
        bool Eliminar(Persona persona);
        bool SaveChanges();
        List<Persona> buscaPorNombre(string filtro);
        List<Persona> buscarNombre(string filtro);
    }
}
