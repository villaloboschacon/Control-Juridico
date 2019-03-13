using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        public bool SaveChanges()
        {
            using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
            {
                this.unidad.Complete();
                return true;
            }
        }
        public bool Comprobar(string validar, int opcion)
        {
            try
            {
                List<Documento> lista;
                if (opcion == 1)
                {
                    using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                    {
                        Expression<Func<Documento, bool>> consulta = (d => d.numeroDocumento.Equals(validar));
                        lista = unidad.genericDAL.Find(consulta).ToList();
                    }
                    if(lista.Count() == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                    {
                        Expression<Func<Documento, bool>> consulta = (d => d.numeroIngreso.Equals(validar));
                        lista = unidad.genericDAL.Find(consulta).ToList();
                    }
                    if (lista.Count() == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}
