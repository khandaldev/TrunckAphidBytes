using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlickrNet;

namespace AphidBytes.Web.Models
{
    public class FlickrManager
    {
        public const string ApiKey = "efa00fe73d52481fa20715f9fe500eb1";
        public const string SharedSecret = "f95ab10a19f1961f";        
        public static Flickr GetInstance()
        {
            return new Flickr(ApiKey, SharedSecret);
        }

        public static Flickr GetAuthInstance()
        {
            var f = new Flickr(ApiKey, SharedSecret);
            if (OAuthToken != null)
            {
                f.OAuthAccessToken = OAuthToken.Token;
                f.OAuthAccessTokenSecret = OAuthToken.TokenSecret;
            }
            return f;
        }

        public static OAuthAccessToken OAuthToken
        {
            get
            {
                if (!HttpContext.Current.Request.Cookies.AllKeys.Contains("OAuthToken"))
                {
                    return null;
                }
                var values = HttpContext.Current.Request.Cookies["OAuthToken"].Values;
                return new OAuthAccessToken
                           {
                               FullName = values["FullName"],
                               Token = values["Token"],
                               TokenSecret = values["TokenSecret"],
                               UserId = values["UserId"],
                               Username = values["Username"]
                           };
            }
            set
            {
                var cookie = new HttpCookie("OAuthToken")
                {
                    Expires = DateTime.UtcNow.AddDays(1),
                };
                cookie.Values["FullName"] = value.FullName;
                cookie.Values["Token"] = value.Token;
                cookie.Values["TokenSecret"] = value.TokenSecret;
                cookie.Values["UserId"] = value.UserId;
                cookie.Values["Username"] = value.Username;
                HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
    }
}


