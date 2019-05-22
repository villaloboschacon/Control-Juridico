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
        ///<summary>
        ///Agrega un caso. 
        ///</summary>
        bool Agregar(Caso oCaso);

        ///<summary>
        ///Actualiza un documento. 
        ///</summary>
        bool Actualizar(Caso oCaso);

        ///<summary>
        ///Elimina un documento. 
        ///</summary>
        bool Eliminar(Caso oCaso);

        ///<summary>
        ///Recupera la lista de casos filtrada por tipo, busqueda y el campo. 
        ///</summary>
        List<Caso> Consulta(int iTipo,string sSearch,string sCampo);

        ///<summary>
        ///Actualiza el modelo.
        ///</summary>
        bool SaveChanges();

        ///<summary>
        ///Comprueba que el registro del caso no este repetido.
        ///</summary>
        bool Comprobar(string idCaso,string numeroCaso);

        ///<summary>
        ///Recupera el correo segun el usuario.
        ///</summary>
        string GetCorreo(int idUsuario);

        ///<summary>
        ///Archiva el caso.
        ///</summary>
        bool ArchivaCaso(int idCaso);
    }
}