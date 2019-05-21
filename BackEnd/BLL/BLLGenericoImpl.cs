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
    public class BLLGenericoImpl<T> : IBLLGenerico<T> where T : class
    {
        private UnidadDeTrabajo<T> unidad;

        public bool Add(T entity)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    unidad.genericDAL.Add(entity);
                    unidad.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                List<T> resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.Find(predicate).ToList();
                }
                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public T Get(int id)
        {
            try
            {
                T resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.Get(id);
                }
                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<T> GetAll()
        {
            try
            {
                List<T> resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.GetAll().ToList();
                }
                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    unidad.genericDAL.Remove(entity);
                    unidad.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                T resultado;
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    resultado = unidad.genericDAL.SingleOrDefault(predicate);
                }
                return resultado;

            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool Update(T entity)
        {
            try
            {
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    unidad.genericDAL.Update(entity);
                    unidad.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
