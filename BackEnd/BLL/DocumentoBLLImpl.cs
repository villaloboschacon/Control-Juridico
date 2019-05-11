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
        private SCJ_BDEntities context;

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
        public bool Comprobar(string numeroDocumento, int opcion,string idDocumento)
        {
            int id = 0;
            try
            {
                id = Int32.Parse(idDocumento);
            }
            catch (Exception)
            {

            }
            try
            {
                List<Documento> lista;
                if (opcion == 1)
                {
                    using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                    {
                        Expression<Func<Documento, bool>> consulta = (d => d.numeroDocumento.Equals(numeroDocumento) && d.idDocumento.Equals(id));
                        lista = unidad.genericDAL.Find(consulta).ToList();
                        if (lista.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            consulta = (d => d.numeroDocumento.Equals(numeroDocumento));
                            lista = unidad.genericDAL.Find(consulta).ToList();
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
                }
                else
                {
                    using (unidad = new UnidadDeTrabajo<Documento>(new SCJ_BDEntities()))
                    {
                        Expression<Func<Documento, bool>> consulta = (d => d.numeroIngreso.Equals(numeroDocumento) && d.idDocumento.Equals(id));
                        lista = unidad.genericDAL.Find(consulta).ToList();
                        if (lista.Count() == 1)
                        {
                            return true;
                        }
                        else
                        {
                            consulta = (d => d.numeroIngreso.Equals(numeroDocumento));
                            lista = unidad.genericDAL.Find(consulta).ToList();
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
                }

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public List<Documento> listaSalidas()
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaSalidas().ToList();
                    if (result != null)
                    {
                        return result;
                    }

                    return null;
                }

            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<Documento> listaEntradas()
        {
            try
            {
                using (context = new SCJ_BDEntities())
                {
                    var result = this.context.sp_listaEntradas().ToList();
                    if (result != null)
                    {
                        return result;
                    }
                    return null;
                }

            }
            catch (Exception)
            {

                return null;
            }
        }

        public Nullable<long> consultaNumeroIngreso()
        {
            using (context = new SCJ_BDEntities())
            {
               var result = this.context.sp_ConsultaNumerodeIngreso().FirstOrDefault();
               return result;
            }
            
        }



        public Nullable<long> generaNumIngreso()
        { 
            using (context = new SCJ_BDEntities())
             {
                 var result = this.context.sp_GeneraNumerodeIngreso().FirstOrDefault();
                 return result;
             }
        }

        public string getNomenclatura(string nombreDept)
        {
            using (context = new SCJ_BDEntities())
            {
                var result = this.context.getNomenclatura(nombreDept).FirstOrDefault();
                return result;
            }
        }

        public List<Documento> listaReferencias(long? referencia)
        {
                try
                {
                    using (context = new SCJ_BDEntities())
                    {
                        var result = this.context.sp_ListaReferencias(referencia).ToList();
                        if (result != null)
                        {
                            return result;
                        }
                        return null;
                    }

                }
                catch (Exception)
                {

                    return null;
                }
            
        }

        public bool archivaDocumento(int idDocumento)
        {
            using (context = new SCJ_BDEntities())
            {
                this.context.sp_archivaDocumento(idDocumento);
                return true;
            }
        }

        public long? generaNumeroReferencia()
        {
            using (context = new SCJ_BDEntities())
            {
                var result = this.context.sp_GeneraNumerodeReferencia().FirstOrDefault();
                return result;
            }
        }
    }
}
