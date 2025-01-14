using Campus.School.Management.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Campus.SchoolManagment.Web.Helper
{
    public static class PagingHelper
    {
        public static MvcHtmlString Pagelinks(this HtmlHelper html, Paginginfo pageinfo, Func<int, string> pageurl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageinfo.totalpage; i++)
            {
                TagBuilder tag = new TagBuilder("a");

                tag.MergeAttribute("href", pageurl(i));
                tag.InnerHtml = i.ToString();

                if (i == pageinfo.currentpage)
                {
                    tag.AddCssClass("Selected");
                    tag.AddCssClass("btn-Primary");
                }

                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}