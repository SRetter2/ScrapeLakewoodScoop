using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace ScrapeLakewoodScoop.Data
{
    public class ScoopRepository
    {
        public IEnumerable<Scoop> GetScoops()
        {
            var result = new List<Scoop>();
            var html = GetScoopHtml();
            var parser = new HtmlParser();
            var htmlDocument = parser.ParseDocument(html);
            var divs = htmlDocument.QuerySelectorAll("div.post");
            foreach (var div in divs)
            {
                var scoop = ParseScoopHtml(div);
                if (scoop != null)
                {
                    result.Add(scoop);
                }
            }
            return result;
        }
        private string GetScoopHtml()
        {
            var client = new HttpClient();
            return client.GetStringAsync($"https://www.thelakewoodscoop.com/").Result;
        }
        private Scoop ParseScoopHtml(IElement div)
        {
            var titleSpan = div.QuerySelector("h2 a");
            var title = titleSpan.TextContent;
            if (title == null)
            {
                return null;
            }
            var titleurl = titleSpan.Attributes["href"].Value;
            if (titleurl == null)
            {
                return null;
            }
            var imageSpan = div.QuerySelector("p a");
            var image = imageSpan.Attributes["href"].Value;
            if (image == null)
            {
                return null;
            }
            var blurbSpan = div.QuerySelector("p");
            var blurb = blurbSpan.TextContent;
            if(blurb == null)
            {
                blurb = "";
            }
            var comments = div.QuerySelector("div .backtotop");
            var commentCount = comments.TextContent;
            if(commentCount == null)
            {
                commentCount = "0";
            } 
            return new Scoop
            {
                Title = title,
                Url = titleurl,
                ImageUrl = image,
                Blurb = blurb,
                CommentCount = commentCount
            };

        }
    }
}