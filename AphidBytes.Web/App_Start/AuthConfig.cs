using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;


namespace AphidBytes.Web
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "1433551906892171",
            //    appSecret: "b9a50e104749bf570695d14e6edc60c4");

            //OAuthWebSecurity.RegisterLinkedInClient(
            //    consumerKey: ConfigurationManager.AppSettings["consumerKey"],
            //    consumerSecret: ConfigurationManager.AppSettings["consumerSecret"]
            //    );
            //OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
