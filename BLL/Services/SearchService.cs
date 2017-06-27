using BLL.Interface;
using DAL.Interfaces;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SearchService
    {
        private readonly IService<Posts> postService;
        public SearchService(IService<Posts> service)
        {
            postService = service;
        }

        public IEnumerable<ORM.Posts> Search(string str)
        {
            return postService.Get(filter: p => p.Text.Contains(str));
        }
    }
}
