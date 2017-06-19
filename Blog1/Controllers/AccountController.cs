using BLL;
using BLL.Interface;
using Blog1.Infrastructure;
using Blog1.Infrastructure.Mappers;
using Blog1.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Blog1.Controllers
{
    public class AccountController : Controller
    {
        private readonly IService<UserEntity> service;

        public AccountController(IService<UserEntity> service)
        {
            this.service = service;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "administrator")]
        public ActionResult Users()
        {
            return View(service.GetAll().Select(user => user.ToMvcUser()));
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LogOnViewModel user)
        {
            
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(user.Email, user.Password))
                //Проверяет учетные данные пользователя и управляет параметрами пользователей
                {
                    FormsAuthentication.SetAuthCookie(user.Email, true);
                    //Управляет службами проверки подлинности с помощью форм для веб-приложений
                    
                     return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password.");
                }
            }
            return View(user);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel viewModel)
        {
            if (viewModel.Captcha != (string)Session[CaptchaImage.CaptchaValueKey])
            {
                ModelState.AddModelError("Captcha", "Incorrect input.");
                return View(viewModel);
            }

            var anyUser = service.GetAll().Any(u => u.Email.Contains(viewModel.Email));

            if (anyUser)
            {
                ModelState.AddModelError("", "User with this address already registered.");
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                    .CreateUser(viewModel.Name, viewModel.Email, viewModel.Password);

                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(viewModel.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
            }
            return View(viewModel);
        }
        [AllowAnonymous]
        public ActionResult Captcha()
        {
            Session[CaptchaImage.CaptchaValueKey] =
                new Random(DateTime.Now.Millisecond).Next(1111, 9999).ToString(CultureInfo.InvariantCulture);
            var ci = new CaptchaImage(Session[CaptchaImage.CaptchaValueKey].ToString(), 211, 50, "Helvetica");

            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";

            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);

            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            return null;
        }
        public PartialViewResult log()
        {
            if(User.Identity.IsAuthenticated)
            ViewBag.userName = User.Identity.Name;
            return PartialView("_login");
        }
    }
}