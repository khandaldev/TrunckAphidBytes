using System;
using System.Collections.Generic;
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
using Newtonsoft.Json;

namespace AphidBytes.Web.Models
{
    public class Dailymotion
    {
        string clientid = "9751b3c0e51378380568";
        string client_secret = "356bd217e1a3e38c4774046115c9daee298f9bda";
        string redirect_uri = "http://aphidbytes.jobseeders.com/";
        string code = "a4e0953901a319fd3f533faf9b1f65a7bf515339";
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
        // string redirect_uri;
        public void init(Guid Aphid)
        {
            if (HttpContext.Current.Request["code"] == null)
            {
                string str = string.Format("https://api.dailymotion.com/oauth/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope=manage_videos&display=popup", clientid, HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0]);
                // string str = string.Format("https://api.dailymotion.com/oauth/authorize?client_id={0}&redirect_uri={1}&response_type=code&scope=read+write+manage_videos+delete&display=popup", clientid, redirect_uri);
                HttpContext.Current.Response.Redirect(str);
            }
            else
            {
                string accesstoken = GetAccessToken(code);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(accesstoken);
                string access_token = jsonobj["access_token"].ToString();
                string expires = jsonobj["expires_in"].ToString();
                string refresh_token = jsonobj["refresh_token"].ToString();
                DateTime Expires = DateTime.Now.AddDays(Math.Floor(ConvertSecondsToDays(Convert.ToDouble(expires))));
                if ((access_token != null) && (expires != null))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.Access_Token = access_token;
                    model.Aphid_id = Aphid;
                    model.category = "DailyMotion";
                    model.Expires = Expires;
                    model.ID = Guid.NewGuid();
                    model.RefereshToken = refresh_token;
                    model.IsDeleted = true;
                    string linkstatus = social.GetData(model);
                    HttpContext.Current.Session["dailystatus"] = linkstatus;
                }
            }

        }

        public double ConvertSecondsToDays(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalDays;
        }
        public string GetAccessToken(string code)
        {
            var request = "https://api.dailymotion.com/oauth/token?";
            string postdata = string.Format("grant_type={0}&client_id={1}&client_secret={2}&redirect_uri={3}&code={4}", "authorization_code", clientid, client_secret, redirect_uri, code);
            string vals = HttpReq(request, postdata);
            return vals;
        }
        public string HttpReq(string uri, string postdata)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(uri);
                req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; nb-NO; rv:8.0.1) Gecko/20100101 Firefox/8.0.1";
                //req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,/*;q=0.8";
                req.Headers.Add("Accept-Language: nb,no;q=0.8,nn;q=0.6,en-us;q=0.4,en;q=0.2");
                req.Headers.Add("Accept-Charset: ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                req.ContentType = "application/x-www-form-urlencoded";
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
                return sr.ReadToEnd();

            }
            catch
            {
                return "fail|error";
            }
        }

        public string post(Guid id, string category, string[] data, string type_of_data, string track, string path_size)
        {
            string title = "";
            string path = "";
            byte[] text;
            string data_size = "";
            string str = "";
            string tag = "";
            string token = social.Reterivetoken(id, category);
            if (token == "Invalid")
            {
                return "Setup";
            }
            //  var fileToUpload = @"C:\Users\jobseeder\Desktop\Desktop data\Dailymotion.mp4";
            try
            {
                if (data.Length > 0)
                {
                    path = data[0];
                    title = data[1];
                    tag = data[2];
                }
                text = Encoding.ASCII.GetBytes(title);
                str = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" + path;
                bool status = social.Credit_Insert(id, "DailyMotion", type_of_data, "", str, title, track, true);
                if (status == true)
                {
                    return "DailyMotion";
                }
                if (status == false)
                {
                    if (type_of_data == "Videos")
                    {
                        var uploadUrl = GetFileUploadUrl(token);
                        var response = GetFileUploadResponse(HttpContext.Current.Server.MapPath(path), token, uploadUrl);
                        var uploadedResponse = PublishVideo(response, token, title, tag);
                        string title_size = CalculateFileSize.Size(text);
                        string val = Regex.Match(title_size, @"\d+\.\d+").Value;
                        string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
                        data_size = (float.Parse(val) + float.Parse(val1)).ToString();
                        social.Credit_Insert(id, "DailyMotion", type_of_data, data_size, str, title, track, true);
                    }
                }
                return "inserted";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("invalid token"))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.IsDeleted = false;
                    model.category = "DailyMotion";
                    model.Aphid_id = id;
                    social.Deletedata(model);
                    return "deleted";
                }
                social.Credit_Insert(id, "DailyMotion", type_of_data, data_size, str, title, track, false);
                return "Timeout";
            }
        }
        private string GetFileUploadResponse(string fileToUpload, string accessToken, string uploadUrl)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", "OAuth " + accessToken);

            var responseBytes = client.UploadFile(uploadUrl, fileToUpload);

            var responseString = Encoding.UTF8.GetString(responseBytes);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(responseString);

            // var response = JsonConvert.DeserializeObject<UploadResponse>(responseString);

            return jsonobj["url"].ToString();
        }

        private static string PublishVideo(string uploadResponse, string accessToken, string title, string tag)
        {
            var request = WebRequest.Create("https://api.dailymotion.com/me/videos?url=" + HttpUtility.UrlEncode(uploadResponse));
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Authorization", "OAuth " + accessToken);
            var requestString = String.Format("title={0}&tags={1}&channel={2}", title, tag, "Videos");
            //var requestString = String.Format("title={0}",title);

            var requestBytes = Encoding.UTF8.GetBytes(requestString);

            var requestStream = request.GetRequestStream();

            requestStream.Write(requestBytes, 0, requestBytes.Length);

            var response = request.GetResponse();

            var responseStream = response.GetResponseStream();
            string responseString;
            using (var reader = new StreamReader(responseStream))
            {
                responseString = reader.ReadToEnd();
            }

            //  var uploadedResponse = JsonConvert.DeserializeObject<UploadedResponse>(responseString);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(responseString);

            return responseString;
        }
        private static string GetFileUploadUrl(string accessToken)
        {
            var client = new WebClient();
            client.Headers.Add("Authorization", "OAuth " + accessToken);

            var urlResponse = client.DownloadString("https://api.dailymotion.com/file/upload");

            //  var response = JsonConvert.DeserializeObject<UploadRequestResponse>(urlResponse).upload_url;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(urlResponse);
            string upload_url = jsonobj["upload_url"].ToString();
            return upload_url;
        }
        public string GetExte_time(string refresh_token)
        {
            var request = "https://api.dailymotion.com/oauth/token?";
            string postdata = string.Format("grant_type={0}&client_id={1}&client_secret={2}&refresh_token={3}", "refresh_token", clientid, client_secret, refresh_token);
            string vals = HttpReq(request, postdata);
            JavaScriptSerializer ser = new JavaScriptSerializer();
            Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(vals);
            string access_token = jsonobj["access_token"].ToString();
            string expires = jsonobj["expires_in"].ToString();
            string refreshtoken = jsonobj["refresh_token"].ToString();
            DateTime Expires = DateTime.Now.AddDays(Math.Floor(ConvertSecondsToDays(Convert.ToDouble(expires))));
            return access_token + '_' + Expires + '_' + refresh_token;
        }
        public void Dailymotion_Unlink(Guid Aphid)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "DailyMotion";
            model.Aphid_id = Aphid;
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            social.Deletedata(model);
        }

    }
}
