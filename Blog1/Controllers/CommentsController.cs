using BLL;
using BLL.DTO;
using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IService<UserEntity> userService;
        private readonly IService<CommentEntity> commentService;

        public CommentsController(IService<UserEntity> service, IService<CommentEntity> commentService)
        {
            this.userService = service;
            this.commentService = commentService;
        }
        
        public PartialViewResult List()
        {
            int postid = Convert.ToInt32(RouteData.Values["id"]);
            var comments = commentService.GetAll().Where(comm => comm.PostId.Equals(postid)).Select(comm => new Blog1.Models.CommentViewModel() {
                Id = comm.Id, Text = comm.Text , User = userService.Get(comm.UserId).Email
            });
            return PartialView(comments);
        }

        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Create(FormCollection collection)
        {
            try
            {
                int postid = Convert.ToInt32( RouteData.Values["id"]);
                int userid = userService.GetAll().Where(user => user.Email.Equals(User.Identity.Name)).FirstOrDefault().Id;
                commentService.Create(new CommentEntity() { PostId = postid, UserId = userid, Text = collection["text"].ToString() });

                // TODO: Add insert logic here

                return PartialView();
            }
            catch
            {
                return PartialView();
            }
        }
        

        // GET: Comments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }    
    }
}
