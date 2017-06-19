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
    public class CommentService : IService<CommentEntity>
    {
        private readonly IRepository<DalComment> commentRepository;

        public CommentService(IRepository<DalComment> repository)
        {
            this.commentRepository = repository;
        }

        public void Create(CommentEntity e)
        {
            commentRepository.Create(e.ToDalComment());
        }

        public void Delete(int id)
        {
            commentRepository.Delete(id);
        }

        public CommentEntity Get(int id)
        {
            return commentRepository.Get(id).ToBllComment();
        }
        public IEnumerable<CommentEntity> GetAll()
        {
            return commentRepository.GetAll().Select(comm => comm.ToBllComment());
        }

        public void Update(CommentEntity e)
        {
            commentRepository.Update(e.ToDalComment());
        }
    }
}
