using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public interface ICasoBLL : IBLLGenerico<Caso>
    {
        bool Agregar(Caso caso);
        bool Modificar(Caso caso);
        bool Eliminar(Caso caso);
        bool SaveChanges();
        bool Comprobar(string idCaso,string numeroCaso);
        string getCorreo(int idUsuario);
        bool archivaCaso(int idCaso);
    }
}