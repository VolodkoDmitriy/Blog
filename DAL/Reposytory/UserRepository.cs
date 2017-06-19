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
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }
        public void Create(DalUser e)
        {

            var user = new Users()
            {
                Name = e.Name,
                Email = e.Email,
                Password = e.Password,
                RoleId = 2
            };
            context.Set<Users>().Add(user);
            context.SaveChanges();
        }
        public DalUser Get(int id)
        {
            return context.Set<Users>().Where(user => user.UserId.Equals(id)).Select(user => new DalUser()
            {
                Name = user.Name,
                Id = user.UserId,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            }).FirstOrDefault();
        }

        public DalUser GetUserByEmail(string email)
        {
            return context.Set<Users>().Where(user => user.Email.Equals(email)).Select(user => new DalUser()
            {
                Name = user.Name,
                Id = user.UserId,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            }).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<Users>().Select(user => new DalUser()
            {
                Name = user.Name,
                Id = user.UserId,
                Email = user.Email,
                Password = user.Password,
                RoleId = user.RoleId
            });
        }
        public void Update(DalUser e)
        {
            
            var users = context.Set<ORM.Users>().Where(user => user.UserId.Equals(e.Id)).Select(user => new DalUser()
            {                
                Name = user.Name,
                Id = user.UserId,
                RoleId = user.RoleId,
                Email = user.Email
            }).FirstOrDefault();
            users.Name = e.Name;            
            users.Email = e.Email;
            users.Password = e.Password;
            context.SaveChanges();
            context.Dispose();
        }
        public void Delete(int id)
        {
            var users = context.Set<ORM.Users>().Where(user => user.UserId.Equals(id)).FirstOrDefault();
            context.Set<ORM.Users>().Remove(users);
            context.SaveChanges();
            context.Dispose();
        }

    }
}
