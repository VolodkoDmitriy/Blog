
using DAL.DTO;
using DAL.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Security;

namespace Blog1.Infrastructure
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IUserRepository UserRepositoryEntity
           => (IUserRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository));

        public IRepository<DalRole> RoleRepositoryEntity
            => (IRepository<DalRole>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRepository<DalRole>));

        public override bool IsUserInRole(string email, string roleName)
        {

            DalUser user = UserRepositoryEntity.GetAll().FirstOrDefault(u => u.Email == email);

            if (user == null) return false;

            DalRole userRole = RoleRepositoryEntity.Get(user.RoleId);

            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            
            {
                var roles = new string[] { };

                var user = UserRepositoryEntity.GetUserByEmail(email);                 

                if (user == null) return roles;

                var userRole = RoleRepositoryEntity.Get(user.RoleId);

                if (userRole != null)
                {
                   roles = new string[] { userRole.Name };
                }
                return roles;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}