using System.Collections.Generic;
using BackEnd.Model;
namespace BackEnd.BLL
{
    ///<summary>
    ///Interfaz encargada de realizar las funciones relacionadas con las personas.
    ///</summary>
    public interface IPersonasBLL:IBLLGenerico<Persona>
    {
        ///<summary>
        ///Agrega el registro de la persona.
        ///</summary>
        bool Agregar(Persona oPersona);

        ///<summary>
        ///Actualiza el registro de la persona.
        ///</summary>
        bool Actualizar(Persona oPersona);

        ///<summary>
        ///Elimina el registro de la persona. 
        ///</summary>
        bool Eliminar(Persona oPersona);

        ///<summary>
        ///Comprueba que la cedula de la persona no este registrada.
        ///</summary>
        bool Comprobar(string sCedula, string sIdPersona);

        ///<summary>
        ///Actualiza el modelo de la capa de datos. 
        ///</summary>
        bool SaveChanges();

        ///<summary>
        ///Recupera la lista de personas filtrandolas por tipo. 
        ///</summary>
        List<Persona> Consulta(int iTipo);

        ///<summary>
        ///Recupera la lista de personas filtrandolas por nombre de persona, tipo de persona y campo de busqueda. 
        ///</summary>
        List<Persona> Consulta(int iTipo, string sFiltro, string sCampo);

        ///<summary>
        ///Recupera la lista de todas las personas.  
        ///</summary>
        List<Persona> GetPersonas();

        ///<summary>
        ///Recupera una persona desde el id.  
        ///</summary>
        Persona GetPersona(int iId);
    }
}
