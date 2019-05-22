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
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.Find(predicate).ToList();
                }
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
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.Get(id);
                }
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
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.GetAll().ToList();
                }
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
                    return true;
                }
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
                using (unidad = new UnidadDeTrabajo<T>(new SCJ_BDEntities()))
                {
                    return unidad.genericDAL.SingleOrDefault(predicate);
                }
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
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
