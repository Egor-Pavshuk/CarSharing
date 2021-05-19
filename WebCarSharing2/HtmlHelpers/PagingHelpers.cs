using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebCarSharing2.Models;

namespace WebCarSharing2.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinksUsingSubmitForm(
            this HtmlHelper html,
            PagingInfoView pagingInfo,
            string functionName
        )
        {
            StringBuilder result = new StringBuilder();

            TagBuilder arrowTag = new TagBuilder("a"); //todo or not

            for (int i = 1; i <= pagingInfo.TotalPages(); i++)
            {
                TagBuilder tag = new TagBuilder("a"); //todo with div
                tag.MergeAttribute("href","javascript: PageClick(" + i + ")");
                tag.InnerHtml = i.ToString();
                if(i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag);
                
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}