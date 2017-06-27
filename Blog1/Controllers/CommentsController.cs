using BLL.Interface;
using BLL.Services;
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
                            User = userService.Get(comm.UserId).Email
                        });

            return PartialView(comments);
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(int id,FormCollection collection)
        {
            commentService.Create(id, collection["text"], User.Identity.Name);
            return RedirectToAction("Details", "Posts", new { id = id });
        }

        [HttpGet]
        [CommentAutor]
        public ActionResult Delete(int id)
        {            
            var comment = commentService.Get(id);
                commentService.Delete(id);
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
