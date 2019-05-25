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

        ///<summary>
        ///Busca una persona fisica o juridica por su numero de identificacion.  
        ///</summary>
        List<Persona> buscaPorIdentificacion(string cedula, int iTipo);

        ///<summary>
        ///Busca una persona fisica o juridica  por su nombre completo.  
        ///</summary>
        List<Persona> buscaPorNombreCompleto(string nombre, int iTipo);

        ///<summary>
        ///Busca una persona juridica por su representante social.  
        ///</summary>
        List<Persona> buscaPorRepresentanteSocial(string nombRepresentante);

        ///<summary>
        ///Busca una persona juridica por su representante legal. 
        ///</summary>
        List<Persona> buscaPorRepresentanteLegal(string nombRepresentante);

        ///<summary>
        ///Busca una persona fisica o juridica  por su correo.  
        ///</summary>
        List<Persona> buscaPorCorreo(string correo, int iTipo);

        ///<summary>
        ///Lista todas las personas administrativas
        ///</summary>
        List<Persona> listarPersonasAdministrativas();

        ///<summary>
        ///Lista todas las personas judiciales
        ///</summary>
        List<Persona> listarPersonasJudiciales();
    }
}
