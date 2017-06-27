using BLL.Interface;
using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace BLL.Services
{
    
    public class BLService<T> : IService<T>
    {
        private readonly IRepository<T> repository;

        public BLService(IRepository<T> repository)
        {        
            this.repository = repository;
        }
        public void Create(T e)
        {
            repository.Create(e);
        }
        public T Get(int id)
        {
            return repository.Get(id);
        }
        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            return repository.Get(filter: filter, orderBy: orderBy);
        }
        public void Update(T e)
        {
            repository.Update(e);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return repository.GetWithInclude(includeProperties);
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return repository.GetWithInclude(predicate,includeProperties);
        }
        
    }
}
