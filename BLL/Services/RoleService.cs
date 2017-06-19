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
    public class RoleService : IService<RoleEntity>
    {
        private readonly IRepository<DalRole> RoleRepository;

        public RoleService(IRepository<DalRole> repository)
        {
            this.RoleRepository = repository;
        }

        public void Create(RoleEntity e)
        {
            RoleRepository.Create(e.ToDalRole());
        }
        public RoleEntity Get(int id)
        {
            return RoleRepository.Get(id).ToBllRole();
        }
        public IEnumerable<RoleEntity> GetAll()
        {
            return RoleRepository.GetAll().Select(role => role.ToBllRole());
        }
        public void Update(RoleEntity e)
        {
            RoleRepository.Update(e.ToDalRole());
        }
        public void Delete(int id)
        {
            RoleRepository.Delete(id);
        }
    }
}
