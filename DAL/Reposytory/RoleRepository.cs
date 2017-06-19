using DAL.DTO;
using DAL.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RoleRepository:IRepository<DalRole>
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalRole e)
        {
            var role = new Role()
            {
                Name = e.Name,
                RoleId = e.Id
            };
            context.Set<Role>().Add(role);            
        }
        public void Update(DalRole e)
        {
            var roles = context.Set<ORM.Roles>().Where(role => role.RoleId.Equals(e.Id)).Select(role => new DalRole()
            {
                 Name = role.Name,
                Id = role.RoleId
            }).FirstOrDefault();
            roles.Name = e.Name;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var roles = context.Set<ORM.Roles>().Where(role => role.RoleId.Equals(id)).FirstOrDefault();
            context.Set<ORM.Roles>().Remove(roles);
            context.SaveChanges();
        }
        public DalRole Get(int id)
        {
            return context.Set<Roles>().Where(roles=> roles.RoleId.Equals(id)).Select(roles => new DalRole()
            {
                Id = roles.RoleId,
                Name = roles.Name
            }).FirstOrDefault();
        }
        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Roles>().Select(user => new DalRole()
            {
                Id = user.RoleId,
                Name = user.Name
            });
        }

    }
}
