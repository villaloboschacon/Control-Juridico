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
        bool Comprobar(string numeroDocumento,int opcion,string idDocumento);
        List<Documento> listaSalidas();
        List<Documento> listaEntradas();
        Nullable<long> consultaNumeroIngreso();
        Nullable<long> generaNumIngreso();
        Nullable<long> generaNumeroReferencia();
        string getNomenclatura(string nombreDept);
        List<Documento> listaReferencias(long? referencia);
        bool archivaDocumento(int idDocumento);
    }
}
