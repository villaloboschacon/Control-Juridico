using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public class DocumentoBLLImpl : BLLGenericoImpl<Documento>, IDocumentoBLL
    {
        private UnidadDeTrabajo<Documento> unidad;

        public bool Agregar(Documento documento)
        {
            this.Add(documento);
            return true;
        }

        public bool Modificar(Documento documento)
        {
            this.Update(documento);
            return true;
        }
    }
}
