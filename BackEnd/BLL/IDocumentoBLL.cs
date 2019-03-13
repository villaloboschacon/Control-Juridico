using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public interface IDocumentoBLL : IBLLGenerico<Documento>
    {
        bool Agregar(Documento documento);
        bool Modificar(Documento documento);
        bool SaveChanges();
        bool Comprobar(string idDocumento,int opcion);
        List<sp_listaSalidas_Result> listaSalidas();
        List<sp_listaEntradas_Result> listaEntradas();
        Nullable<long> consultaNumeroIngreso();
        Nullable<long> generaNumIngreso();
        string getNomenclatura(string nombreDept);

    }
}
