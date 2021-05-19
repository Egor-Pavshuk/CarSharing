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
            
             

            for (int i = 1; i <= pagingInfo.TotalPages(); i++)
            {
                if (pagingInfo.CurrentPage != 1 && i == 1)
                {
                    TagBuilder arrowTag = new TagBuilder("a");//todo or not
                    TagBuilder arrowDiv = new TagBuilder("div");
                    arrowDiv.AddCssClass("btn btn-default");
                    arrowTag.MergeAttribute("href", "javascript: PageClick(" + i + ")");
                    arrowDiv.InnerHtml = "<";

                    arrowTag.InnerHtml += arrowDiv.ToString(TagRenderMode.Normal);
                    result.Append(arrowTag);
                }

                TagBuilder div = new TagBuilder("div");
                div.AddCssClass("btn btn-default");

                TagBuilder tag = new TagBuilder("a"); 
                tag.MergeAttribute("href","javascript: PageClick(" + i + ")");

                div.InnerHtml = i.ToString();
                if(i == pagingInfo.CurrentPage)
                    div.AddCssClass("selected");
                
                tag.InnerHtml += div.ToString(TagRenderMode.Normal);
                result.Append(tag);


                if (pagingInfo.CurrentPage != pagingInfo.TotalPages() && i == pagingInfo.TotalPages())
                {
                    TagBuilder arrowTag = new TagBuilder("a");//todo or not
                    TagBuilder arrowDiv = new TagBuilder("div");
                    arrowDiv.AddCssClass("btn btn-default");
                    arrowTag.MergeAttribute("href", "javascript: PageClick(" + i + ")");
                    arrowDiv.InnerHtml = ">";

                    arrowTag.InnerHtml += arrowDiv.ToString(TagRenderMode.Normal);
                    result.Append(arrowTag);
                }
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}