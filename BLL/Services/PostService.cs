using BLL.DTO;
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
    public class PostService : IService<PostEntity>
    {
        private readonly IRepository<DalPost> PostRepository;

        public PostService(IRepository<DalPost> repository)
        {
            this.PostRepository = repository;
        }

        public void Create(PostEntity e)
        {
            PostRepository.Create(e.ToDalPost());
        }
        public PostEntity Get(int id)
        {
            return PostRepository.Get(id).ToBllPost();
        }
        public IEnumerable<PostEntity> GetAll()
        {
            return PostRepository.GetAll().Select(post => post.ToBllPost());
        }
        public void Update(PostEntity e)
        {
            PostRepository.Update(e.ToDalPost());
        }
        public void Delete(int id)
        {
            PostRepository.Delete(id);
        }
    }
}
