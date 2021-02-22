using System;
using System.Web;

namespace GoogleMapsManager.Helpers
{
    public static class HttpContextHelper
    {
        public static string BaseUrl()
        {
            var baseUrl = "";

            if (HttpContext.Current == null)
            {
                throw new Exception("No HttpContext.Current");
            }

            baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;

            if (!baseUrl.EndsWith("/")) baseUrl += "/";

            return baseUrl;
        }
    }
}