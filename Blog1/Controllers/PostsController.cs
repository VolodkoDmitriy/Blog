﻿using BLL.DTO;
using BLL.Interface;
using Blog1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class PostsController : Controller
    {
        private readonly IService<PostEntity> postService;
        private readonly IService<CommentEntity> commentService;

        public PostsController(IService<PostEntity> service, IService<CommentEntity> commentService)
        {
            this.postService = service;
        }

        // GET: Post
        public ActionResult Index()
        {        
            return View();
        }
        [HttpGet]
        public PartialViewResult list(int count=10,int slide=0)
        {
            return PartialView(postService.GetAll().Select(post => new PostViewModel()
            {
                Text = post.Text,
                Id = post.Id
               // Comments = commentService.GetAll().Where(comm => comm.PostId == user.Id).ToString()
            
            }
                ));
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var postentity = postService.Get(id);
            var post = new PostViewModel()
            {
                Text = postentity.Text,
                Id = postentity.Id
            };            
            return View(post);
        }
        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(PostNewModel e)
        {
            var post = new PostEntity()
            {
                Text = e.Text
            };
            postService.Create(post);
            return View();
        }


    }
}