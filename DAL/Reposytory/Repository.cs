using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly DbContext context;

        public Repository(DbContext uow)
        {
            this.context = uow;
        }
        public void Create(T e)
        {
            context.Set<T>().Add(e);
            context.SaveChanges();
        }
        public T Get(int id)
        {
            return context.Set<T>().Find(id);
        }
        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public void Update(T e)
        {
            context.Entry(e).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var item = Get(id);
                context.Set<T>().Remove(Get(id));
                context.SaveChanges();            
        }
        public void Delete(T e)
        {
            context.Set<T>().Remove(e);
            context.SaveChanges();
        }
        public IEnumerable<T> GetWithInclude(params Expression<Func<T, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public IEnumerable<T> GetWithInclude(Func<T, bool> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate);
        }

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
        
    }
}

