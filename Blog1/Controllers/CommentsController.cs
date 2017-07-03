using BLL.Interface;
using BLL.Services;
using Blog1.Models;
using ORM;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IService<Users> userService;
        private readonly CommentService commentService;

        public CommentsController(IService<Users> service, CommentService commentService)
        {
            this.userService = service;
            this.commentService = commentService;
        }

        public PartialViewResult List(int id)
        {
            int postid = id;

            var comments = commentService
                        .Get(filter: comm => comm.PostId.Equals(postid))
                        .Select(comm => new Blog1.Models.CommentViewModel()
                        {
                            Id = comm.CommentId,
                            Text = HttpUtility.HtmlDecode(comm.commentText),
                            User = userService.Get(comm.UserId).Email,
                            UserId = comm.UserId
                        });

            return PartialView("List",comments);
        }
        
        public PartialViewResult Create(int id)
        {
            var comm = new NewCommentModel { PostId = id };
            if (!Request.IsAjaxRequest()) {
                return PartialView(comm);
            }
            else { return PartialView("CreateAjax", comm); }
        }

        [HttpPost]
        [Authorize]
        public ActionResult CreateComment(NewCommentModel collection)
        {
            commentService.Create(collection.PostId, collection.Text, User.Identity.Name);
            if (Request.IsAjaxRequest())
            { return List(collection.PostId); }
            else
            return RedirectToAction("Details", "Posts", new { id = collection.PostId });
        }

        [HttpGet]
        [CommentAutor]
        public ActionResult Delete(int id)
        {            
            var comment = commentService.Get(id);
                commentService.Delete(id);
            if (Request.IsAjaxRequest()) { return List(comment.PostId); }
            else
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [HttpGet]
        [CommentAutor]
        public ActionResult Edit(int id)
        {
            var commEntity = commentService.Get(id);
            var commModel = new Models.NewCommentModel()
                    {   Id= commEntity.CommentId,
                        PostId = commEntity.PostId,
                        UserId = commEntity.UserId,
                        Text = HttpUtility.HtmlDecode(commEntity.commentText)
                    };
            return View(commModel);
        }
        [HttpPost]
        [CommentAutor]
        public ActionResult Edit(int id, Models.NewCommentModel collection)
        {
            var postId = commentService.Edit(id, collection.Text);
            return RedirectToAction("Details", "Posts", new { id = postId });
        }

    }
}
