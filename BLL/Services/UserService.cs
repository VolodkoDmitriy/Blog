using BLL.Interface;
using BLL.Mappers;
using DAL;
using DAL.DTO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{   
    public class UserService : IService<UserEntity>
    {        
        private readonly IRepository<DalUser> userRepository;

        public UserService(IUserRepository repository)
        {        
            this.userRepository = repository;
        }

        public void Create(UserEntity e)
        {
            userRepository.Create(e.ToDalUser());
        }
        public UserEntity Get(int id)
        {
            return userRepository.Get(id).ToBllUser();
        }
        public IEnumerable<UserEntity> GetAll()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }
        public void Update(UserEntity e)
        {
            userRepository.Update(e.ToDalUser());
        }
        public void Delete(int id)
        {
            userRepository.Delete(id);
        }
    }
}
