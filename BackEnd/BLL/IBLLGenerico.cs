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
        T Get(int id);
        List<T> GetAll();
        List<T> Find(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);

        bool Add(T entity);
        bool Update(T entity);
        bool Remove(T entity);
    }
}
