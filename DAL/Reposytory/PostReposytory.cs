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
    public class PostReposytory : IRepository<DalPost> 
    {
        private readonly DbContext context;

        public PostReposytory(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(DalPost e)
        {
            var post = new ORM.Posts()
            {
                Text = e.Text
            };            
            context.Set<ORM.Posts>().Add(post);
            context.SaveChanges();
        }
        public void Update(DalPost e)
        {
            var posts = context.Set<ORM.Posts>().Where(post => post.PostId.Equals(e.Id)).FirstOrDefault();
            posts.Text = e.Text;
            context.Entry(posts).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var posts = context.Set<ORM.Posts>().Where(post => post.PostId.Equals(id)).FirstOrDefault();
            context.Set<ORM.Posts>().Remove(posts);         

            context.SaveChanges();
        }
        public DalPost Get(int id)
        {
            return context.Set<ORM.Posts>().Where(post => post.PostId.Equals(id)).Select(post => new DalPost()
            {
                Id = post.PostId,
                Text = post.Text
            }).FirstOrDefault();
        }
        public IEnumerable<DalPost> GetAll()
        {
            return context.Set<ORM.Posts>().Select(post => new DalPost()
            {
                Id = post.PostId,
                Text = post.Text
            });
        }
    }
}
