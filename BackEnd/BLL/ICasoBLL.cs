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
        List<Caso> Consulta(int iTipo,string sSearch,string sCampo);
        bool SaveChanges();
        bool Comprobar(string idCaso,string numeroCaso);
        string getCorreo(int idUsuario);
        bool archivaCaso(int idCaso);

        ///<summary>
        ///Busca Caso por Nombre de Persona
        ///</summary>
        List<Caso> buscaPorPersona(string nombrePersona);

        ///<summary>
        ///Busca Caso por Nombre de Abogado
        ///</summary>
        List<Caso> buscaPorAbogado(string nombreAbogado);

        ///<summary>
        ///Busca Caso por descripcion de estado
        ///</summary>
        List<Caso> buscaPorEstado(string estado);

        ///<summary>
        ///Recupera la lista de casos administrativos. 
        ///</summary>
        List<Caso> getCasosAdministrativos();

        ///<summary>
        ///Recupera la lista de casos juduciales. 
        ///</summary>
        List<Caso> getCasosJudiciales();

        ///<summary>
        ///Recupera el tipo caso
        ///</summary>
        int getTipoCaso(int idCaso);
    }
}