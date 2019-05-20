using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BackEnd.DAL
{
    public interface IDALGenerico<TEntity> where TEntity : class
    {

        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Remove(TEntity entity);
    }
}
