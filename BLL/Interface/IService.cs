using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLL.Interface
{
    public interface IService<T>    
    {        
        T Get(int id);
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        void Create(T e);
        void Update(T e);
        void Delete(int id);

        IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties);

        IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
           params Expression<Func<T, object>>[] includeProperties);
        
    }
}
