using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using Facebook;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using ImpactWorks.FBGraph.Core;
using AphidBytes.Core.Configuration;
using Newtonsoft.Json;
using AphidBytes.Accounts.Processor;

namespace AphidBytes.Web.Models
{
    public class FaceBookModel
    {
        string scope = "user_hometown,email,user_about_me,user_birthday,manage_pages";

        public ConfigurationValue<string> App_Id = new ConfigurationValue<string>("facebook.apiID");
        public ConfigurationValue<string> App_Secret = new ConfigurationValue<string>("facebook.apiKey");
        //string App_Id = "155168161639941";
        //string App_Secret = "523936b55b7d6b7af76ceded76704512";
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();

        //pkr posting code
        public void posting(string title, string accesstoken)
        {
            var responsePost = "";
            var objFacebookClient = new FacebookClient(accesstoken);
            if (HttpContext.Current.Request["code"] == null)
            {
                //FaceBookConnect.Authorize(HttpContext.Current.Request.Url.AbsoluteUri, scope);
                HttpContext.Current.Response.Redirect(string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}&display=popup", App_Id.Value, HttpContext.Current.Request.Url.AbsoluteUri, scope));
            }

            else
            {

               var result= objFacebookClient.Get("oauth/access_token", new
                {
                    client_id = App_Id.Value,
                    client_secret = App_Secret.Value,
                    grant_type = "client_credentials"
                });

                string access_token = Get_access_token();
                string id = Get_ID(access_token);
                DateTime Expire_in = Get_Extended_Token(access_token);
                //if ((access_token != null) && (Expire_in != null)) ;
                responsePost = objFacebookClient.Post("https://graph.facebook.com/me/feed?",
                                     new
                                     {
                                         //link = path,
                                         //picture = str,
                                         message = title,
                                         access_token = access_token
                                     }).ToString();


            }

        }

        public void init1(Guid Aphid_ID1)
        {
            if (HttpContext.Current.Request["code"] == null)
            {
                // FaceBookConnect.Authorize(HttpContext.Current.Request.Url.AbsoluteUri, scope);
                HttpContext.Current.Response.Redirect(string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}&display=popup", App_Id.Value, HttpContext.Current.Request.Url.AbsoluteUri, scope));
            }

            else
            {

                string access_token = Get_access_token();
                string id = Get_ID(access_token);
                DateTime Expire_in = Get_Extended_Token(access_token);
                if ((access_token != null) && (Expire_in != null))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.ID = Guid.NewGuid();
                    model.category = "Facebook";
                    model.Access_Token = access_token;
                    model.Aphid_id = Aphid_ID1;
                    model.Expires = Expire_in;
                    model.IsDeleted = true;
                    model.RefereshToken = access_token;
                    string status = social.GetData(model);
                    HttpContext.Current.Session["status"] = status;
                }
            }
        }
        public double ConvertSecondsToDays(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalDays;
        }
        private string Get_access_token()
        {
            Dictionary<string, string> tokens = new Dictionary<string, string>();
            string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}", App_Id.Value, HttpContext.Current.Request.Url.AbsoluteUri, scope, HttpContext.Current.Request["code"].ToString(), App_Secret.Value);
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            string access_token = string.Empty;
            using (HttpWebResponse responce = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(responce.GetResponseStream());
                string vals = reader.ReadToEnd();
                                
                JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(vals);
                access_token = jObject.SelectToken("access_token").ToString();

                foreach (string token in vals.Split('&'))
                {
                    tokens.Add(token.Substring(0, token.IndexOf(":")), token.Substring(token.IndexOf(":") + 1, token.Length - token.IndexOf(":") - 1));
                }
            }
            //access_token = tokens["access_token"];
            //string expires = tokens["expires"];
            return access_token;
        }
        public DateTime Get_Extended_Token(string accesstoken)
        {
            FacebookClient client = new FacebookClient();
            dynamic result = client.Get("https://graph.facebook.com/oauth/access_token?", new
            {
                grant_type = "fb_exchange_token",
                client_id = App_Id.Value,
                client_secret = App_Secret.Value,
                fb_exchange_token = accesstoken
            });
            DateTime Expire_in = DateTime.Now.AddDays(Math.Floor(ConvertSecondsToDays(Convert.ToDouble(result.expires))));
            return Expire_in;
        }
        private string Get_ID(string accesstoken)
        {
            FacebookClient client = new FacebookClient();
            dynamic result = client.Get("https://graph.facebook.com/oauth/access_token?", new
            {
                grant_type = "fb_exchange_token",
                client_id = App_Id.Value,
                client_secret = App_Secret.Value,
                fb_exchange_token = accesstoken
            });

            //var client = new FacebookClient(Session["access_token"].ToString());
            //dynamic fbresult = client.Get("me?fields=id,email,first_name,last_name,gender,locale,link,timezone,location,picture");
            //FacebookUserModel facebookUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserModel>(fbresult.ToString());

            client = new FacebookClient(result.access_token);
            dynamic me = client.Get("me?fields=id,email,first_name,last_name,gender,locale,link,timezone,location,picture");
            //dynamic me = client.Get("/me");
            string id = me.id;
            return id;
        }
        public void fbUnlink(Guid id)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "Facebook";
            model.Aphid_id = id;
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            social.Deletedata(model);

        }
        public string PostTowall(Guid id, string category, string[] data, string type_of_data, string track, string path_size)
        {
            var responsePost = "";
            string title = "";
            string path = "";
            byte[] text;
            byte[] url_path;
            string data_size = "";
            string str = "";
            try
            {
                string accesstoken = social.Reterivetoken(id, category);
                if (accesstoken == "Invalid")
                {
                    return "Setup";
                }
                //create the facebook account object
                var objFacebookClient = new FacebookClient(accesstoken);
                if (data.Length > 0)
                {
                    path = data[0];
                    title = data[1];

                }
                text = Encoding.ASCII.GetBytes(title);
                str = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" + path;
                bool status = social.Credit_Insert(id, "Facebook", type_of_data, "", str, title, track, true);
                string title_size = CalculateFileSize.Size(text);
                string val = Regex.Match(title_size, @"\d+\.\d+").Value;
                string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
                data_size = (float.Parse(val) + float.Parse(val1)).ToString();
                if (status == true)
                {
                    return "Facebook";
                }
                if (status == false)
                {
                    if (type_of_data == "Photos")
                    {

                        if ((str != null) && (title != null))
                        {
                            responsePost = objFacebookClient.Post("https://graph.facebook.com/me/feed?",
                                 new
                                 {
                                     //link = path,
                                     picture = str,
                                     message = title,
                                     access_token = accesstoken
                                 }).ToString();
                        }
                    }
                    else if (type_of_data == "Files")
                    {
                        if ((str != null) && (title != null))
                        {
                            responsePost = objFacebookClient.Post("https://graph.facebook.com/me/feed?",
                                 new
                                 {
                                     link = str,
                                     picture = "",
                                     message = title,
                                     access_token = accesstoken
                                 }).ToString();
                        }
                    }
                    else if (type_of_data == "Pdf")
                    {
                        if ((str != null) && (title != null))
                        {
                            responsePost = objFacebookClient.Post("https://graph.facebook.com/me/feed?",
                                 new
                                 {
                                     link = str,
                                     picture = "",
                                     message = title,
                                     access_token = accesstoken
                                 }).ToString();
                        }
                    }
                    else if (type_of_data == "Music")
                    {
                        if ((str != null) && (title != null))
                        {
                            responsePost = objFacebookClient.Post("https://graph.facebook.com/me/feed?",
                                 new
                                 {
                                     link = str,
                                     picture = "",
                                     message = title,
                                     access_token = accesstoken
                                 }).ToString();
                        }
                    }
                    else if (type_of_data == "Videos")
                    {

                        if ((str != null) && (title != null))
                        {
                            responsePost = objFacebookClient.Post("https://graph.facebook.com/me/feed?",
                                 new
                                 {
                                     link = str,
                                     picture = "",
                                     message = title,// purposes",
                                     access_token = accesstoken
                                 }).ToString();
                        }
                    }

                    social.Credit_Insert(id, "Facebook", type_of_data, data_size, str, title, track, true);
                }
                return "inserted";
            }

            catch (Exception ex)
            {
                string name = ex.Message;
                if ((name.Contains("190") || (name.Contains("graph.facebook"))))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.IsDeleted = false;
                    model.category = "Facebook";
                    model.Aphid_id = id;
                    social.Deletedata(model);
                    return "deleted";
                }
                else
                {
                    social.Credit_Insert(id, "Facebook", type_of_data, data_size, str, title, track, false);
                    return "Timedout";
                }
            }
        }
        public FbModel FbLogin()
        {

            FbModel fb = new FbModel();
            if (HttpContext.Current.Request["code"] == null)
            {
                string url1 = HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];
                HttpContext.Current.Response.Redirect(string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}", App_Id.Value, HttpContext.Current.Request.Url.AbsoluteUri, scope));
                return null;
            }

            else
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();
                string url = string.Format("https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}", App_Id.Value, HttpContext.Current.Request.Url.AbsoluteUri, scope, HttpContext.Current.Request["code"].ToString(), App_Secret.Value);
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string vals = reader.ReadToEnd();
                    var fbtoken=JsonConvert.DeserializeObject<FacebookLoginTokenResponse>(vals);
                    /*foreach (string token in vals.Split('&'))
                    {

                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }*/
                    tokens.Add("access_token", fbtoken.access_token);
                    tokens.Add("expires", fbtoken.expires_in);
                }

                string access_token = tokens["access_token"];

                string expires = tokens["expires"];

                FacebookClient client = new FacebookClient();
                dynamic result = client.Get("https://graph.facebook.com/oauth/access_token?", new
                {
                    grant_type = "fb_exchange_token",
                    client_id = App_Id.Value,
                    client_secret = App_Secret.Value,
                    fb_exchange_token = access_token
                });

                var client_data = new FacebookClient(access_token);

                dynamic fbresult = client_data.Get("me?fields=id,email,first_name,last_name,gender,locale,link,timezone,location,picture");

                fb.email = Convert.ToString(fbresult["email"]);
                fb.first_name = Convert.ToString(fbresult["first_name"]);
                fb.last_name = Convert.ToString(fbresult["last_name"]);
                fb.picture = Convert.ToString(fbresult["picture"]["data"]["url"]);
                return fb;

            }
            return fb;
        }

        public FbModel RegisterFb()
        {
            FbModel fb = new FbModel();

            if (HttpContext.Current.Request["code"] == null)
            {

                string url =
                       string.Format(
                           "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                           App_Id.Value,
                           HttpContext.Current.Request.Url.AbsoluteUri,
                           scope);
                HttpContext.Current.Response.Redirect(url);
                return null;
                //FaceBookConnect.Authorize(HttpContext.Current.Request.Url.AbsoluteUri, scope);
                //HttpContext.Current.Response.Redirect(string.Format("https:://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}&display=popup", App_Id, HttpContext.Current.Request.Url.AbsoluteUri, scope));
            }

            else
            {
               
                string access_token = Get_access_token();
                fb = Sign_Get_ID(access_token);
                return fb;
            }

        }

        private FbModel Sign_Get_ID(string accesstoken)
        {
            FacebookClient client = new FacebookClient();
            dynamic result = client.Get("https://graph.facebook.com/oauth/access_token?", new
            {
                grant_type = "fb_exchange_token",
                client_id = App_Id.Value,
                client_secret = App_Secret.Value,
                fb_exchange_token = accesstoken
            });

            //var client = new FacebookClient(Session["access_token"].ToString());
            //dynamic fbresult = client.Get("me?fields=id,email,first_name,last_name,gender,locale,link,timezone,location,picture");
            //FacebookUserModel facebookUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserModel>(fbresult.ToString());

            client = new FacebookClient(result.access_token);

            object me = client.Get("me?fields=birthday,email,first_name,last_name,picture");
            string vals = me.ToString();
            //dynamic me = client.Get("/me");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var jsonobj = JObject.Parse(vals);
            //FbModel facebookUser = Newtonsoft.Json.JsonConvert.DeserializeObject<FbModel>(vals.ToString());
            FbModel fb = new FbModel();

            fb.email = Convert.ToString(jsonobj["email"]);
            fb.first_name = Convert.ToString(jsonobj["first_name"]);
            fb.last_name = Convert.ToString(jsonobj["last_name"]);
            fb.picture = jsonobj["picture"]["data"]["url"].ToString();
            //fb.birthday =Convert.ToString (jsonobj["birthday"]);
            //fb.username = jsonobj["username"].ToString();       

            return fb;
        }
    }
        public class Authentication
    {
        public ImpactWorks.FBGraph.Connector. Facebook FacebookAuth()
        {

            ImpactWorks.FBGraph.Connector.Facebook facebook = new ImpactWorks.FBGraph.Connector.Facebook();
            facebook.AppID = "";//"1031792090241721";//
            facebook.CallBackURL = "http://localhost:55517/SocialNetworks/Success/";
            facebook.Secret = "";//"1d0fb1fcf4da39beed6e353cd31b5ff9";


            List<FBPermissions> permissions = new List<FBPermissions>() {
                FBPermissions.user_about_me,              
               
                FBPermissions.user_status,
               FBPermissions.user_photos
               
            };


            facebook.Permissions = permissions;
            return facebook;
        }

    }

}