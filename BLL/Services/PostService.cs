using BLL.Interface;
using ORM;
using System;
using System.Linq;
using DAL.Interfaces;
using System.Web;
using System.IO;

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
        public string Create(string text,string email, HttpPostedFileBase poImgFile = null)
        {
            var user = userService.Get(u => u.Email.Equals(email))
                                    .FirstOrDefault();
            var post = new Posts()
            {
                Text = HttpUtility.HtmlEncode(text),
                UserId = user.UserId,
                CreateDate = DateTime.Now
            };
            if (poImgFile != null)
            {
                byte[] imageData = null;
                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }
                post.Photo = imageData;
            }
            Create(post);
            return user.Name;
        }

        public void Edit(int id, string text, HttpPostedFileBase poImgFile = null)
        {
            var post = Get(id);
            post.Text = HttpUtility.HtmlEncode(text);
            if (poImgFile != null)
            {
                byte[] imageData = null;
                using (var binary = new BinaryReader(poImgFile.InputStream))
                {
                    imageData = binary.ReadBytes(poImgFile.ContentLength);
                }
                post.Photo = imageData;
            }
            else post.Photo = null;
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
