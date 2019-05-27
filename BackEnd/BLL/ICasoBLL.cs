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
        List<Caso> Consulta(int iTipo, string sSearch, string sCampo);
        bool SaveChanges();
        bool Comprobar(string idCaso, string numeroCaso);

        ///<summary>
        ///Obtiene el Correo del abogado para asignar el caso
        ///</summary>
        string getCorreo(int idUsuario);

        ///<summary>
        ///Archiva el Caso seleccionado
        ///</summary>
        bool archivaCaso(int idCaso);

        ///<summary>
        ///Busca Caso Administrativo o Judicial por Nombre de Persona
        ///</summary>
        List<Caso> buscaPorPersona(string nombrePersona, int iTipo);

        ///<summary>
        ///Busca Caso  Administrativo o Judicial por Nombre de Abogado
        ///</summary>
        List<Caso> buscaPorAbogado(string nombreAbogado, int iTipo);

        ///<summary>
        ///Busca Caso  Administrativo o Judicial por descripcion de estado
        ///</summary>
        List<Caso> buscaPorEstado(string estado, int iTipo);


        ///<summary>
        ///Busca Caso  Administrativo o Judicial por descripcion de estado
        ///</summary>
        List<Caso> buscaPorNumeroDeProceso(string numeroDeProceso, int iTipo);

        ///<summary>
        ///Recupera la lista de casos administrativo o judicial dependiendo del tipo. 
        ///</summary>
        List<Caso> getCasos(int iTipo);

        ///<summary>
        ///Recupera el tipo caso
        ///</summary>
        int getTipoCaso(int idCaso);
    }
}