using BackEnd.Model;
using System.Collections.Generic;

namespace BackEnd.BLL
{
    public interface IUsuarioBLL : IBLLGenerico<Usuario>
    {
        ///<summary>
        ///Recupera la lista de usuarios.
        ///</summary>
        List<Usuario> Consulta();

        ///<summary>
        ///Recupera el usuario dependiendo del nombre de usuario.
        ///</summary>
        Usuario GetUsuario(string sUsername);

        ///<summary>
        ///Recupera el role dependiendo del usuario.
        ///</summary>
        string GetRol(string sUsername);
    }
}
