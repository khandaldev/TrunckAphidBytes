using System;
using System.Web.Mvc;

namespace AphidBytes.Web.Helpers
{
    public static class Images
    {
        public static MvcHtmlString ImageUrl(this HtmlHelper helper, string imageName)
        {
            var versionNumber = Guid.NewGuid().ToString().Substring(0, 5);
            var urlString = string.Format("/img/{0}?v={1}", imageName, versionNumber);
            return new MvcHtmlString(urlString); 
        }
    }
}