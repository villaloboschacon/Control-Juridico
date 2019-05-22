using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BLL
{
    public interface IBLLGenerico<T> where T : class
    {
        ///<summary>
        ///Recupera la entidad por medio del id.
        ///</summary>
        T Get(int id);

        ///<summary>
        ///Recupera toda la lista entidad.
        ///</summary>
        List<T> GetAll();

        ///<summary>
        ///Recupera la lista con un filtro especifico.
        ///</summary>
        List<T> Find(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        ///<summary>
        ///Agrega la entidad
        ///</summary>
        bool Add(T entity);

        ///<summary>
        ///Actualiza la entidad
        ///</summary>
        bool Update(T entity);

        ///<summary>
        ///Elimina la entidad
        ///</summary>
        bool Remove(T entity);
    }
}
