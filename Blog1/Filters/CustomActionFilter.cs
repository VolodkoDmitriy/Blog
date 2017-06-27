using System.Web.Mvc;
using BLL.Interface;
using System.Linq;
using System.Security.Principal;
using System;
using ORM;
using DAL.Interfaces;

namespace Blog1
{
    internal class PostAutor : ActionFilterAttribute, IActionFilter
    {
        private IRepository<Users> userService;
        private IRepository<Posts> postService;
      
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            userService = (IRepository<Users>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRepository<Users>));
            postService = (IRepository<Posts>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRepository<Posts>));

            IPrincipal user = filterContext.HttpContext.User;
            int id = Convert.ToInt32((string)filterContext.RouteData.Values["id"]);
            
                int autorid = postService.Get(id).UserId;
            
            int userid = userService.Get(u => u.Email.Equals(user.Identity.Name)).FirstOrDefault().UserId;
            
            if (autorid == userid)
            {               
            }
            else
            {
                filterContext.Result = (ActionResult)new RedirectResult("~/error/NoPermition");
            }
        }
    }
    internal class CommentAutor : ActionFilterAttribute, IActionFilter
    {
        private IRepository<Users> userService;
        private IRepository<Comments> CommentsService;

        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            userService = (IRepository<Users>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRepository<Users>));
            CommentsService = (IRepository<Comments>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRepository<Comments>));
            int autorid;
            IPrincipal user = filterContext.HttpContext.User;
            if (filterContext.ActionParameters.ContainsKey("id"))
            {
                autorid = CommentsService.Get((int)filterContext.ActionParameters["id"]).UserId;
            }
            else
            {
                var commentId = ((Blog1.Models.NewCommentModel)filterContext.ActionParameters["collection"]).Id;
                autorid = CommentsService.Get(commentId).UserId;
            }
            int userid = userService.Get(u => u.Email.Equals(user.Identity.Name)).FirstOrDefault().UserId;

            if (autorid == userid)
            {
            }
            else
            {
                filterContext.Result = (ActionResult)new RedirectResult("~/error/NoPermition");
            }
        }
    }
}