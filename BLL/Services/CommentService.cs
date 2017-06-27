using BLL.Interface;
using ORM;
using System.Linq;
using DAL.Interfaces;
using System.Web;

namespace BLL.Services
{
    public class CommentService : BLService<Comments>
    {
        private readonly IService<Users> userService;
        public CommentService(IRepository<Comments> repository, IService<Users> userService) : base(repository)
        {
            this.userService = userService;
        }
        public void Create(int postId, string text, string email)
        {
            var user = userService.Get(u => u.Email.Equals(email))
                                    .FirstOrDefault();
            var comment = new Comments()
            {
                commentText = HttpUtility.HtmlEncode(text),
                UserId = user.UserId,
                PostId = postId                
            };
            Create(comment);
        }

        public int Edit(int id, string text)
        {
            var comment = Get(id);
            comment.commentText = HttpUtility.HtmlEncode(text);
            Update(comment);
            return comment.PostId;
        }
    }
}
