using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity:IEntity
    {
        void Create(TEntity e);
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity e);
        void Delete(int id);
    }
}
