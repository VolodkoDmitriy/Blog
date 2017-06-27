using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
//.............................
public static class PagingHelpers
{
    public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pageInfo, Func<int, string>
    pageUrl)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 1; i <= pageInfo.TotalPages; i++)
        {
            TagBuilder tag = new TagBuilder("a");
            tag.MergeAttribute("href", pageUrl(i));
            tag.InnerHtml = i.ToString();
            // если текущая страница, то выделяем ее,
            // например, добавляя класс
            if (i == pageInfo.PageNumber)
            {
                tag.AddCssClass("selected");
                tag.AddCssClass("btn-primary");
            }
            tag.AddCssClass("btn btn-default");
            result.Append(tag.ToString());
        }
        return MvcHtmlString.Create(result.ToString());
    }
}
public class PageInfo
{
    public PageInfo() { }
    public PageInfo(int page, int count, int total)
    {
        PageNumber = page;
        PageSize = count;
        TotalItems = total;
    }
    public int PageNumber { get; set; } // номер текущей страницы
    public int PageSize { get; set; } // кол-во объектов на странице
    public int TotalItems { get; set; } // всего объектов
    public int TotalPages  // всего страниц
    {
        get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
    }
}
