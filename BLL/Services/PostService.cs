using BLL.Interface;
using ORM;
using System;
using System.Linq;
using DAL.Interfaces;
using System.Web;

namespace BLL.Services
{
    public class PostService: BLService<Posts>
    {
        private readonly IService<Comments> commentsService;
        private readonly IService<Users> userService;
        public PostService(IRepository<Posts> repository,IService<Comments> commentsService, IService<Users> userService) : base(repository)
        {
            this.commentsService = commentsService;
            this.userService = userService;
        }
        public string Create(string text,string email)
        {
            var user = userService.Get(u => u.Email.Equals(email))
                                    .FirstOrDefault();
            var post = new Posts()
            {
                Text = HttpUtility.HtmlEncode(text),
                UserId = user.UserId,
                CreateDate = DateTime.Now
            };
            Create(post);
            return user.Name;
        }

        public void Edit(int id, string text)
        {
            var post =Get(id);
            post.Text = HttpUtility.HtmlEncode(text);
            Update(post);
        }
        public void Remove(int id)
        {
            var post = GetWithInclude(p => p.PostId == id, c => c.Comments).FirstOrDefault();
            foreach (var item in post.Comments.ToList())
            {
                commentsService.Delete(item.CommentId);
            }
            Delete(id);
        }
    }
}
