using BackEnd.DAL;
using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BackEnd.BLL
{
    public class CasoBLLImpl : BLLGenericoImpl<Caso>, ICasoBLL
    {
        private UnidadDeTrabajo<Caso> unidad;
        private SCJ_BDEntities context;

        public bool Agregar(Caso caso)
        {
            return Add(caso);
        }

        public bool Modificar(Caso caso)
        {
            return Update(caso);
        }

        public bool Eliminar(Caso caso)
        {
            return Remove(caso);
        }
        public bool SaveChanges()
        {
            using (unidad = new UnidadDeTrabajo<Caso>(new SCJ_BDEntities()))
            {
                this.unidad.Complete();
                return true;
            }
        }

        public bool Comprobar(string numeroCaso , string idCaso)
        {
            int idCasoint = 0;

            try
            {
                idCasoint = Int32.Parse(idCaso);

            }
            catch (Exception)
            {

            }
            try
            {
                List<Caso> lista;
                using (unidad = new UnidadDeTrabajo<Caso>(new SCJ_BDEntities()))
                {
                    Expression<Func<Caso, bool>> consulta = (d => d.idCaso.Equals(idCasoint) && d.numeroCaso.Equals(numeroCaso));
                    lista = unidad.genericDAL.Find(consulta).ToList();
                    if (lista.Count() == 1)
                    {
                        return true;
                    }
                    else
                    {
                        consulta = (d => d.numeroCaso.Equals(numeroCaso));
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
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }

        public string getCorreo(int idUsuario)
        {
            using (context = new SCJ_BDEntities())
            {
                var result = this.context.sp_getUsuarioCorreo(idUsuario).FirstOrDefault();
                return result;
            }
        }

        public bool archivaCaso(int idCaso)
        {
            using (context = new SCJ_BDEntities())
            {
                this.context.sp_archivaCaso(idCaso);
                return true;
            }
        }
    }
}

