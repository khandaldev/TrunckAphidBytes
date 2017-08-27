using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using DotNetOpenAuth.AspNet.Clients;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using System.Web.Mvc;
using ASPSnippets.GoogleAPI;
using System.Web.Script.Serialization;
using AphidBytes.Web.Models;
using AphidBytes.Core.Configuration;
//using ASPSnippets.GoogleAPI;
//using System.Web.Script.Serialization;

namespace AphidBytes.Web.Models
{
    public class GoogleOAuth2Client
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(GoogleOAuth2ClientLoginBasic));
        public ConfigurationValue<string> App_Id = new ConfigurationValue<string>("google.apiID");
        public ConfigurationValue<string> App_Secret = new ConfigurationValue<string>("google.apiKey");
        public FbModel init()
        {


            GoogleConnect.ClientId = App_Id.Value;
            GoogleConnect.ClientSecret = App_Secret.Value;
            /*
            GoogleConnect.ClientId = "35058061053-ne9e80fja7h66bkg0odk24ifqgcco2qn.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "U-JrjuEqMUzm3FNIAmdi0Wwu";
            */

            var id = HttpContext.Current.Request.Url.AbsoluteUri.Split('=')[1];
            int aux = 0;
            string url = string.Empty;
            if (int.TryParse(id, out aux))
                url = HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0] + "/" + id;
            else
                url = HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];

            if (!id.Contains("profile&code"))
            {
                if (!url.Contains("/" + id))
                {
                    url = url + "/" + id;
                }
            }
            GoogleConnect.RedirectUri = url;
            logger.Error("Return URI SignUp - " + GoogleConnect.RedirectUri);
            if (HttpContext.Current.Request["code"] == null)
            {
                GoogleConnect.Authorize("profile", "email");
                return null;
            }
            else
            {
                string code = HttpContext.Current.Request.QueryString["code"];
                //string state = HttpContext.Current.Request.QueryString["State"];
                string json = GoogleConnect.Fetch("me", code);
                logger.Error("GoogleOAuth2Client- Pass Fetch method");
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var jsonobj = JObject.Parse(json);
                //string json = GoogleConnect.Fetch("me?fields=id,email,first_name,last_name,gender,picture", code);
                FbModel fb = new FbModel();

                fb.email = Convert.ToString(jsonobj["emails"].First["value"]);
                fb.first_name = Convert.ToString(jsonobj["name"]["givenName"]);
                fb.last_name = Convert.ToString(jsonobj["name"]["familyName"]);
                fb.picture = Convert.ToString(jsonobj["image"]["url"]);
                //fb.picture = Convert.ToString(jsonobj["picture"]["data"]["url"]);
                fb.username = (jsonobj)["displayName"].ToString();

                //GoogleConnect.Clear();
                return fb;


            }
        }
    }
    public class GoogleOAuth2ClientLoginBasic
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(GoogleOAuth2ClientLoginBasic));
        public ConfigurationValue<string> App_Id = new ConfigurationValue<string>("google.apiID");
        public ConfigurationValue<string> App_Secret = new ConfigurationValue<string>("google.apiKey");
        public FbModel init()
        {
            GoogleConnect.ClientId = App_Id.Value;
            GoogleConnect.ClientSecret = App_Secret.Value;
            
            GoogleConnect.RedirectUri = HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];
            logger.Error("Return URI - " + GoogleConnect.RedirectUri);
            if (HttpContext.Current.Request["code"] == null)
            {
                GoogleConnect.Authorize("profile", "email");
                logger.Error("Google plus Authorize pass");
                return null;
            }
            else
            {
                string code = HttpContext.Current.Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                logger.Error("Google plus json pass");
                JavaScriptSerializer ser = new JavaScriptSerializer();
                var jsonobj = JObject.Parse(json);
                //string json = GoogleConnect.Fetch("me?fields=id,email,first_name,last_name,gender,picture", code);
                FbModel fb = new FbModel();

                fb.email = Convert.ToString(jsonobj["emails"].First["value"]);
                fb.first_name = Convert.ToString(jsonobj["name"]["givenName"]);
                fb.last_name = Convert.ToString(jsonobj["name"]["familyName"]);
                fb.picture = Convert.ToString(jsonobj["image"]["url"]);
                //fb.picture = Convert.ToString(jsonobj["picture"]["data"]["url"]);
                fb.username = (jsonobj)["displayName"].ToString();
                //GoogleConnect.Clear();
                return fb;
            }
           
        }
    }
}
