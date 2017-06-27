using DAL.Interfaces;
using ORM;
using System;
using System.Linq;
using System.Web.Security;

namespace Blog1.Infrastructure
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IRepository<Users> UserRepositoryEntity
           => (IRepository<Users>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRepository<Users>));

        public IRepository<ORM.Roles> RoleRepositoryEntity
            => (IRepository<ORM.Roles>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRepository<ORM.Roles>));

        public override bool IsUserInRole(string email, string roleName)
        {

            Users user = UserRepositoryEntity.Get(filter: u => u.Email.Equals(email)).FirstOrDefault();

            if (user == null) return false;

            ORM.Roles userRole = RoleRepositoryEntity.Get(user.RoleId);

            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {
            var roles = new string[] { };
            var user = UserRepositoryEntity.Get(_user=>_user.Email.Equals(email)).FirstOrDefault();
            if (user == null) return roles;
            var userRole = RoleRepositoryEntity.Get(user.RoleId);
            if (userRole != null)
            {
                roles = new string[] { userRole.Name };
            }
            return roles;
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