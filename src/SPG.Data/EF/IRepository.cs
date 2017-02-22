using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SPG.Data.EF
{
    public interface IRepository<T>{
        IEnumerable<T> GetAll();
        T Get(long id);
        void Insert(T entity);        
        void Update(T entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> where);
    }
}
