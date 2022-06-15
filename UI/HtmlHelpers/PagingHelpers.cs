using System;
using System.Text;
using System.Web.Mvc;

using UI.Models;

namespace UI.HtmlHelpers
{
  public static class PagingHelpers
  {
    public static MvcHtmlString PageLinks(this HtmlHelper htmlHelper,
                                          PagingInfo pagingInfo,
                                          Func<Int32, String> pageUrl)
    {
      _ = htmlHelper;

      var result = new StringBuilder();
      for (Int32 i = 1; i <= pagingInfo.TotalPages; ++i)
      {
        var tag = new TagBuilder("a")
        {
          InnerHtml = i.ToString()
        };
        tag.MergeAttribute("href", pageUrl(i));

        if (i == pagingInfo.CurrentPage)
        {
          tag.AddCssClass("btn-primary selected");
        }

        tag.AddCssClass("btn btn-default");
        _ = result.Append(tag.ToString());
      }

      return MvcHtmlString.Create(result.ToString());
    }
  }
}