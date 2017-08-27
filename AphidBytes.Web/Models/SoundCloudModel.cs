using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using Ewk.SoundCloud.ApiLibrary;
using Krystalware.UploadHelper;
namespace AphidBytes.Web.Models
{
    public class SoundCloudModel
    {
        string client_key = "85bbba41db1b1090136e6e5f0193f4c0";
        string client_secret = "d23769006442b0fff8240d8269993811";
        static string url = HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
        public void init(Guid Aphid)
        {
            if (HttpContext.Current.Request["code"] == null)
            {
                HttpContext.Current.Response.Redirect(string.Format("https://soundcloud.com/connect?scope=non-expiring&response_type=code&display=popup&client_id={0}&redirect_uri={1}&state=Aphidbytes", client_key, url));
            }
            else
            {
                string token_url = string.Format("https://api.soundcloud.com/oauth2/token");
                string postdata = string.Format("code={0}&client_id={1}&client_secret={2}&redirect={3}&grant_type={4}", HttpContext.Current.Request["code"].ToString(), client_key, client_secret, url, "client_credentials");
                string data = HttpReq(token_url, postdata);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(data);
                string access_token = jsonobj["access_token"].ToString();
                string expires = jsonobj["expires_in"].ToString();
                string refresh_token = jsonobj["refresh_token"].ToString();
                DateTime expires_soundcloud = DateTime.Now.AddDays(Math.Floor(ConvertSecondsToDays(Convert.ToDouble(expires))));
                if ((access_token != null) && (expires != null))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.Access_Token = access_token;
                    model.Aphid_id = Aphid;
                    model.category = "SoundCloud";
                    model.Expires = expires_soundcloud.Date;
                    model.ID = Guid.NewGuid();
                    model.IsDeleted = true;
                    model.RefereshToken = refresh_token;
                    HttpContext.Current.Session["soundstatus"] = social.GetData(model);
                }
            }
        }

        public CookieContainer cookieContainer = new CookieContainer();
        public double ConvertSecondsToDays(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalDays;
        }
        public string HttpReq(string uri, string postdata)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
                req.CookieContainer = cookieContainer;
                req.ContentType = "application/json";
                req.AllowAutoRedirect = true;
                req.KeepAlive = true;
                if (postdata != "")
                {
                    req.Method = "POST";
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] PostDataBytes = encoding.GetBytes(postdata);
                    req.ContentLength = PostDataBytes.Length;
                    Stream stream = req.GetRequestStream();
                    stream.Write(PostDataBytes, 0, PostDataBytes.Length);
                    stream.Close();
                }
                else req.Method = "GET";
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.GetEncoding("windows-1252"));
                req = null;
                return sr.ReadToEnd();

            }
            catch { return "fail|error"; }
        }
        public string RenewToken(string refereshtoken)
        {
            string url1 = string.Format("https://api.soundcloud.com/oauth2/token");
            string data = string.Format("client_id={0}&client_secret={1}&refresh_token={2}&grant_type={3}&redirect_uri={4}", client_key, client_secret, refereshtoken, "refresh_token",url);
            string str = HttpReq(url1, data);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(str);
            string access_token = jsonobj["access_token"].ToString();
            string expires = jsonobj["expires_in"].ToString();
            string refresh_token = jsonobj["refresh_token"].ToString();
            DateTime expires_soundcloud = DateTime.Now.AddDays(Math.Floor(ConvertSecondsToDays(Convert.ToDouble(expires))));
            return access_token + '_' + expires_soundcloud + '_' + refresh_token;
        }

        public void SoundCloud_unlink(Guid id)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "SoundCloud";
            model.Aphid_id = id;
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            social.Deletedata(model);
        }
        readonly SoundCloud _soundCloud = new SoundCloud("85bbba41db1b1090136e6e5f0193f4c0");
        public string POST(Guid id, string category, string[] data, string type_of_data, string track, string path_size)
        {
            string title = "";
            string path = "";
            byte[] text;
            string data_size = "";
            string str = "";
            if (data.Length > 0)
            {
                path = data[0];
                title = data[1];
            }
            text = Encoding.ASCII.GetBytes(title);
            str = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" + path;
            string title_size = CalculateFileSize.Size(text);
            string val = Regex.Match(title_size, @"\d+\.\d+").Value;
            string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
            data_size = (float.Parse(val) + float.Parse(val1)).ToString();
            string token = social.Reterivetoken(id, category);
            if (token == "Invalid")
            {
                return "Setup";
            }
            bool status = social.Credit_Insert(id, "SoundCloud", type_of_data, "", str, title, track, true);
            if (status == true)
            {
                return "SoundCloud";
            }
            else
            {
                if (type_of_data == "Music")
                {
                    System.Net.ServicePointManager.Expect100Continue = false;
                    var request = WebRequest.Create("https://api.soundcloud.com/tracks") as HttpWebRequest;
                    //some default headers (from demo request)
                    request.Accept = "*/*";
                    request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
                    request.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
                    request.Headers.Add("Accept-Language", "en-US,en;q=0.8,ru;q=0.6");
                    //file array
                    var files = new UploadFile[] { 
            new UploadFile(HttpContext.Current.Server.MapPath(path), "track[asset_data]", "application/octet-stream")   };
                    //other form data
                    var form = new NameValueCollection();
                    form.Add("track[title]", title);
                    form.Add("track[sharing]", "private");
                    form.Add("oauth_token", token);
                    form.Add("format", "json");
                    //form.Add("Filename", HttpContext.Current.Session["LinkPath"].ToString());
                    form.Add("Upload", "Submit Query");
                    try
                    {
                        using (var response = HttpUploadHelper.Upload(request, files, form))
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                social.Credit_Insert(id, "SoundCloud", type_of_data, data_size, str, title, track, true);
                                return "inserted";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("401"))
                        {
                            SocialNetworkModel model = new SocialNetworkModel();
                            model.IsDeleted = false;
                            model.category = "SoundCloud";
                            model.Aphid_id = id;
                            social.Deletedata(model);
                            return "deleted";
                        }
                        else
                        {
                            social.Credit_Insert(id, "SoundCloud", type_of_data, data_size, str, title, track, false);
                            return "Timedout";
                        }
                    }


                }
                else
                    return "";
            }
        }
    }
}
