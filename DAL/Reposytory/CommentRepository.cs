using DAL.DTO;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentRepository: IRepository<DalComment>
    {
        private readonly DbContext context;

        public CommentRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalComment e)
        {
            var post = new ORM.Comments()
            {
                UserId = e.UserId,                
                PostId = e.PostId,
                commentText = e.Text                
            };
            context.Set<ORM.Comments>().Add(post);
            context.SaveChanges();
        }
        public DalComment Get(int id)
        {
            return context.Set<ORM.Comments>().Where(comm => comm.PostId.Equals(id)).Select(comment => new DalComment()
            {
                UserId = comment.UserId,
                PostId = comment.PostId,
                Text = comment.commentText,
                Id = comment.CommentId
            }).FirstOrDefault();
        }
        public IEnumerable<DalComment> GetAll()
        {
            return context.Set<ORM.Comments>().Select(comment => new DalComment()
            {
                UserId = comment.UserId,
                PostId = comment.PostId,
                Text = comment.commentText,
                Id = comment.CommentId
            });
        }
    }
}
