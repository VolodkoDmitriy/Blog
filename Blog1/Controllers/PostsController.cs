using BLL.Interface;
using BLL.Services;
using Blog1.Models;
using ORM;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class PostsController : Controller
    {
        private readonly PostService postService;
        private readonly IService<Users> userService;
        private readonly SearchService searchService;

        public PostsController(PostService service,  IService<Users> userService)
        {
            this.postService = service;
            this.userService = userService;
            this.searchService = new SearchService(postService);
        }       

        [HttpGet]
        public ActionResult Index(int count = 10, int page = 1)
        {        
            return List(count , page);
        }
        
        [HttpGet]        
        public ViewResult List(int count=10, int page=1)
        {
            var posts = postService.Get(orderBy: post => post.OrderBy(q => q.CreateDate));            

            var pageinfo = new PageInfo(page,count, posts.Count());

            var modelPosts =  posts
                                    .Skip(count * (page - 1))
                                    .Take(count)
                                    .Select(post => new PostViewModel()
                                    {
                                        Text = HttpUtility.HtmlDecode(post.Text),
                                        Id = post.PostId,
                                        UserName = userService.Get(post.UserId).Email
                                    });

            var results = new PagedList<PostViewModel>(modelPosts, pageinfo);
            results.url = x => Url.Action("Index", new { page = x });
            ViewBag.Title = "Index";
            ViewBag.Header = "Last posts";

            return View("list",results);
        }

        [HttpGet]
        public ViewResult Autor(string id,int count = 10, int page = 1)
        {
            var user = userService.Get(_user => _user.Name.Equals(id)).FirstOrDefault();           

            if (user != null)
            {
                ViewBag.Title = user.Name;
                ViewBag.Header = user.Name +" last posts";

                var allPosts = postService.Get(post => post.UserId.Equals(user.UserId));

                var pageinfo = new PageInfo(page, count, allPosts.Count());

                var pagePosts = allPosts.Skip((page-1) * count)
                                 .Take(count)
                                 .OrderBy(post => post.CreateDate)
                        .Select(post => new PostViewModel()
                        {   Text = HttpUtility.HtmlDecode(post.Text),
                            Id = post.PostId,
                            UserName = userService.Get(post.UserId).Email
                        });
                
                var results = new PagedList<PostViewModel>(pagePosts, pageinfo);
                results.url = x => Url.Action("Autor", new {id=id, page = x });
                return View("list", results);
            }
            return View("NotFoundAutor");
        }

        [HttpGet]
        public ActionResult Search(string SearchString, int count = 10, int page = 1)
        {
            var searchResults = searchService.Search(SearchString);

            if (searchResults.Count() > 0)
            {
                var pageinfo = new PageInfo(page, count, searchResults.Count());

                var posts = searchResults
                    .Skip((page - 1) * count)
                    .Take(count)
                    .Select(post => new PostViewModel()
                    {
                        Text = HttpUtility.HtmlDecode(post.Text),
                        Id = post.PostId,
                        UserName = userService.Get(post.UserId).Email
                    });

                var results = new PagedList<PostViewModel>(posts, pageinfo);

                results.url = x => Url.Action("Search", new { SearchString = SearchString, page = x });
                ViewBag.Title = "Search Results";
                ViewBag.Header = "Search Results";
                return View("list", results);
            }
            else return View("SearchNotFound");
        }
                
        [HandleError(ExceptionType = typeof(NullReferenceException),View = "PostNotFound")]
        public ActionResult Details(int id)
        {
            var postentity = postService.Get(id);
            var username = userService.Get(postentity.UserId).Name;
            var post = new PostViewModel()
            {
                Text = HttpUtility.HtmlDecode(postentity.Text),
                Id = postentity.PostId,
                UserId = postentity.UserId,
                UserName = username,
                CreateDate = postentity.CreateDate,
                isPhoto = postentity.Photo != null
            };            
            return View(post);
        }
        [HttpGet]
        [Authorize(Roles = "user")]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "user")]        
        public ActionResult Create([Bind(Exclude = "Photo")]PostNewModel e)
        {
            if (Request.Files.Count > 0)
            {
                postService.Create(e.Text, User.Identity.Name, Request.Files["Photo"]);
            }
            else postService.Create(e.Text, User.Identity.Name);

            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [HttpGet]
        [PostAutor]
        public ActionResult Delete(int id)
        {
            var autor = userService.Get(postService.Get(id).UserId).Name;
            postService.Remove(id);
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [HttpGet]
        [PostAutor]        
        public ActionResult Edit(int id)
        {
            var postEntity = postService.Get(id);
            var postModel = new PostNewModel() { Id = postEntity.PostId, Text = HttpUtility.HtmlDecode(postEntity.Text) };
            return View(postModel);
        }

        [HttpPost]
        [PostAutor]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Exclude = "Photo")]PostNewModel collection)
        {            
            if (Request.Files.Count > 0)
            {
                if(Request.Files["Photo"].ContentLength != 0) { 
                postService.Edit(collection.Id, collection.Text, Request.Files["Photo"]);
                }
                else postService.Edit(collection.Id, collection.Text);
            }
            else postService.Edit(collection.Id, collection.Text);
            return RedirectToAction("Details", "Posts", new { id = collection.Id });
        }

        public FileContentResult Photo(int id)
        {
            var post = postService.Get(id);            
            return File(post.Photo, "image/png");
        }

    }
}