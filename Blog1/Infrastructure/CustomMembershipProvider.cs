
using DAL.DTO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Security;

namespace Blog1.Infrastructure
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public IUserRepository UserRepositoryEntity
          => (IUserRepository)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserRepository));

        public MembershipUser CreateUser(string name, string email, string password, int role = 2)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return null;
            }

            var user = new DalUser
            {
                Name = name,
                Email = email,
                Password = Crypto.HashPassword(password),
                RoleId = role
            };
            UserRepositoryEntity.Create(user);
            membershipUser = GetUser(email, false);
            return membershipUser;
        }

        public override bool ValidateUser(string email, string password)
        {
            var user = UserRepositoryEntity.GetUserByEmail(email);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }
            return false;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = UserRepositoryEntity.GetUserByEmail(email);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Name,
                null, user.Email, null, null,
                false, false, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();
        
        public override MembershipPasswordFormat PasswordFormat { get; }

        internal object CreateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }       

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }
        
    }
}
