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
using System.Web.Script.Serialization;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using RestSharp;

namespace AphidBytes.Web.Models
{
    public class LinkedLinModel
    {
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
        public void init(Guid Aphid)
        {
            string Apikey = "75f1ygevhclp5y";
            string ApiSecret = "Cxs4xevh5dEOKgEk";
            string url = HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0];
            if (HttpContext.Current.Request["code"] == null)
            {
                HttpContext.Current.Response.Redirect(string.Format("https://www.linkedin.com/uas/oauth2/authorization?response_type=code&client_id={0}&scope={1}&state={2}&redirect_uri={3}", Apikey, "rw_nus", "AphidBytes", url));
            }
            else
            {
                string urldata = string.Format("https://www.linkedin.com/uas/oauth2/accessToken?grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}", HttpContext.Current.Request["code"].ToString(), url, Apikey, ApiSecret);

                string vals = GetAccessToken(urldata);
                JavaScriptSerializer ser = new JavaScriptSerializer();
                Dictionary<string, object> jsonobj = ser.Deserialize<Dictionary<string, object>>(vals);
                string access_token = jsonobj["access_token"].ToString();
                string expires = jsonobj["expires_in"].ToString();
                //string linked_id = GetID(access_token);
                //DateTime newexpires = GetNewExpires(access_token);
                DateTime Expires = DateTime.Now.AddDays(Math.Floor(ConvertSecondsToDays(Convert.ToDouble(expires))));
                if ((access_token != null) && (expires != null))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.Access_Token = access_token;
                    model.Aphid_id = Aphid;
                    model.category = "LinkedLin";
                    model.Expires = Expires;
                    model.ID = Guid.NewGuid();
                    model.RefereshToken = access_token;
                    model.IsDeleted = true;
                    string linkstatus = social.GetData(model);
                    HttpContext.Current.Session["linkstatus"] = linkstatus;
                }

            }

        }
        public double ConvertSecondsToDays(double seconds)
        {
            return TimeSpan.FromSeconds(seconds).TotalDays;
        }

        public string GetAccessToken(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            using (HttpWebResponse responce = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(responce.GetResponseStream());
                string vals = reader.ReadToEnd();
                request = null;
                return vals;
            }
        }
        //        @params={:"oauth_token"=>"XXXXXXXXXXX",:oauth_token_secret=>"XXXXXXXXXXX",:oauth_expires_in=>"5184000"}
        public DateTime GetNewExpires(string token)
        {
            string url = string.Format("https://api.linkedin.com/uas/oauth/requestToken");
            HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
            using (HttpWebResponse resp = req.GetResponse() as HttpWebResponse)
            {
                StreamReader read = new StreamReader(resp.GetResponseStream());
                string value = read.ReadToEnd();
                req = null;
                string url1 = string.Format("https//www.linkedin.com/uas/oauth/authenticate?oauth_token={0}", value);
                HttpWebRequest reqt = WebRequest.Create(url1) as HttpWebRequest;
                using (HttpWebResponse reponse = reqt.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(reponse.GetResponseStream());
                    string vals = reader.ReadToEnd();
                    string stream = vals.Substring(vals.LastIndexOf(":oauth_expires_in=>") + 20, ((vals.LastIndexOf('}') - 2) - (vals.LastIndexOf(":oauth_expires_in") + 20)));
                    DateTime expires = DateTime.Now.AddDays(Math.Floor(ConvertSecondsToDays(Convert.ToDouble(stream))));
                    return expires;
                }

            }
        }
        public void Linkedin_Unlink(Guid Aphid)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "LinkedLin";
            model.Aphid_id = Aphid;
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            social.Deletedata(model);
        }
        public string Post_to_link(Guid id, string category, string[] data, string type_of_data,string track, string path_size)
        {
            //var responsePost = "";
            string title = "";
            string path = "";
            byte[] text;
            string data_size = "";
            string accesstoken = social.Reterivetoken(id, category);
            if (accesstoken == "Invalid")
            {
                return "Setup";
            }
            String requestUrl = "https://api.linkedin.com/v1/people/~/shares?oauth2_access_token=" + accesstoken;
            if (data.Length > 0)
            {
                path = data[0];
                title = data[1];
            }
            text = Encoding.ASCII.GetBytes(title);
            string str = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" + path;
            bool status = social.Credit_Insert(id, "LinkedLin", type_of_data, "", str, title,track,true);
            string title_size = CalculateFileSize.Size(text);
            string val = Regex.Match(title_size, @"\d+\.\d+").Value;
            string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
            data_size = (float.Parse(val) + float.Parse(val1)).ToString();
            if (status == true)
            {
                return "LinkedLin";
            }
            else
            {

                try
                {
                    if (type_of_data == "Photos")
                    {
                        var shareMsg = new
                        {
                            comment = "Testing out the LinkedIn Share API with JSON....",
                            content = new
                            {
                                title = title,
                                submitted_url = "",
                                submitted_image_url = str
                            },
                            visibility = new
                            {
                                code = "anyone"
                            }
                        };
                        RestClient rc = new RestClient();
                        RestRequest request = new RestRequest(requestUrl, Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("x-li-format", "json");
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(shareMsg);
                        RestResponse restResponse = (RestResponse)rc.Execute(request);
                        ResponseStatus responseStatus = restResponse.ResponseStatus;
                        if (restResponse.Content != null)
                        {
                            if ((restResponse.Content.Contains("401")) || (restResponse.Content.Contains("password")))
                            {
                                SocialNetworkModel model = new SocialNetworkModel();
                                model.IsDeleted = false;
                                model.category = "LinkedLin";
                                model.Aphid_id = id;
                                social.Deletedata(model);
                                return "Social index";
                            }
                        }
                    }
                    if (type_of_data == "Music")
                    {
                        var shareMsg = new
                        {
                            comment = "Testing out the LinkedIn Share API with JSON....",
                            content = new
                            {
                                title = title,
                                submitted_url = str,
                                submitted_image_url = ""
                            },
                            visibility = new
                            {
                                code = "anyone"
                            }
                        };
                        RestClient rc = new RestClient();
                        RestRequest request = new RestRequest(requestUrl, Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("x-li-format", "json");
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(shareMsg);
                        RestResponse restResponse = (RestResponse)rc.Execute(request);
                        ResponseStatus responseStatus = restResponse.ResponseStatus;
                        if (restResponse.Content != null)
                        {
                            if ((restResponse.Content.Contains("401")) || (restResponse.Content.Contains("password")))
                            {
                                SocialNetworkModel model = new SocialNetworkModel();
                                model.IsDeleted = false;
                                model.category = "LinkedLin";
                                model.Aphid_id = id;
                                social.Deletedata(model);
                                return "Social index";
                            }
                        }
                    }
                    if (type_of_data == "Pdf")
                    {
                        var shareMsg = new
                        {
                            comment = "Testing out the LinkedIn Share API with JSON....",
                            content = new
                            {
                                title = title,
                                submitted_url = str,
                                submitted_image_url = ""
                            },
                            visibility = new
                            {
                                code = "anyone"
                            }
                        };
                        RestClient rc = new RestClient();
                        RestRequest request = new RestRequest(requestUrl, Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("x-li-format", "json");
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(shareMsg);
                        RestResponse restResponse = (RestResponse)rc.Execute(request);
                        ResponseStatus responseStatus = restResponse.ResponseStatus;
                        if (restResponse.Content != null)
                        {
                            if ((restResponse.Content.Contains("401")) || (restResponse.Content.Contains("password")))
                            {
                                SocialNetworkModel model = new SocialNetworkModel();
                                model.IsDeleted = false;
                                model.category = "LinkedLin";
                                model.Aphid_id = id;
                                social.Deletedata(model);
                                return "Social index";
                            }
                        }
                    }
                    if (type_of_data == "Files")
                    {
                        var shareMsg = new
                        {
                            comment = "Testing out the LinkedIn Share API with JSON....",
                            content = new
                            {
                                title = title,
                                submitted_url = str,
                                submitted_image_url = ""
                            },
                            visibility = new
                            {
                                code = "anyone"
                            }
                        };
                        RestClient rc = new RestClient();
                        RestRequest request = new RestRequest(requestUrl, Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("x-li-format", "json");
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(shareMsg);
                        RestResponse restResponse = (RestResponse)rc.Execute(request);
                        ResponseStatus responseStatus = restResponse.ResponseStatus;
                        if (restResponse.Content != null)
                        {
                            if ((restResponse.Content.Contains("401")) || (restResponse.Content.Contains("password")))
                            {
                                SocialNetworkModel model = new SocialNetworkModel();
                                model.IsDeleted = false;
                                model.category = "LinkedLin";
                                model.Aphid_id = id;
                                social.Deletedata(model);
                                return "Social index";
                            }
                        }
                    }
                    if(type_of_data == "Videos")
                    {
                        var shareMsg = new
                        {
                            comment = "Testing out the LinkedIn Share API with JSON....",
                            content = new
                            {
                                title = title,
                                submitted_url = str,
                                submitted_image_url = ""
                            },
                            visibility = new
                            {
                                code = "anyone"
                            }
                        };
                        RestClient rc = new RestClient();
                        RestRequest request = new RestRequest(requestUrl, Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddHeader("x-li-format", "json");
                        request.RequestFormat = DataFormat.Json;
                        request.AddBody(shareMsg);

                        RestResponse restResponse = (RestResponse)rc.Execute(request);
                        ResponseStatus responseStatus = restResponse.ResponseStatus;
                        if (restResponse.Content != null)
                        {
                            if ((restResponse.Content.Contains("401")) || (restResponse.Content.Contains("password")))
                            {
                                SocialNetworkModel model = new SocialNetworkModel();
                                model.IsDeleted = false;
                                model.category = "LinkedLin";
                                model.Aphid_id = id;
                                social.Deletedata(model);
                                return "deleted";
                            }
                        }

                    }
                  
                    social.Credit_Insert(id, "LinkedLin", type_of_data, data_size, str, title,track,true);
                    return "inserted";

                }

                catch (Exception ex)
                {
                    social.Credit_Insert(id, "LinkedLin", type_of_data, data_size, str, title,track, false);
                    return "Timedout";
                }


            }
        }
    }
}