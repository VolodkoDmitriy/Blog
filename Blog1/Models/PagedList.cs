using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog1.Models
{
    public class PagedList<T>
    {
        public IEnumerable<T> items;
        public PageInfo pageInfo;
        public Func<int, string> url;
        public PagedList(IEnumerable<T> items, PageInfo pageInfo)
        {
            this.items = items;
            this.pageInfo = pageInfo;
        }

    }
}