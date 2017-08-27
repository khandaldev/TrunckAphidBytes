using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using Twitterizer;
using Org.BouncyCastle.Asn1.Ocsp;

namespace AphidBytes.Web.Models
{
    public class TwitterModel
    {

        //string consumerKey = "Ovdbg37iOxkFm4vPznGpqkpWv";
        //string consumerSecret = "RslpkIQkbFawmkkilDbeJdyhRHMduMGsgh0oIvjMcyrWAoRvkx";
        string consumerKey = "1KVcfvZ3vEhK1lSoyHSfKjdZV";
        string consumerSecret = "apX2iw8kp4RGPtTvlsEC5AMNgCDlXJisf8eJLOKYayvJZrg05t";
        //string facebookURL = "https://api.twitter.com/1.1/statuses/update.json";
        string URL = "https://api.twitter.com/1.1/statuses/update.json";
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
        string oauth_version = "1.0";
        string oauth_signature_method = "HMAC-SHA1";

        public void Post(string title, string accesstoken, string tweeterText)
        {
            try
            {

            string URL = "https://api.twitter.com/1.1/statuses/update.json";
            if (HttpContext.Current.Request["oauth_token"] == null)
            {
                OAuthTokenResponse reqToken = OAuthUtility.GetRequestToken(
                    consumerKey,
                    consumerSecret,
                    HttpContext.Current.Request.Url.AbsoluteUri);

                HttpContext.Current.Response.Redirect(string.Format("http://twitter.com/oauth/authorize?oauth_token={0}",
                    reqToken.Token));
            }
            else
            {
                string requestToken = HttpContext.Current.Request["oauth_token"].ToString();
                string pin = HttpContext.Current.Request["oauth_verifier"].ToString();

                var tokens = OAuthUtility.GetAccessToken(
                    consumerKey,
                    consumerSecret,
                    requestToken,
                    pin);

                OAuthTokens accesstoken1 = new OAuthTokens()
                {
                    AccessToken = tokens.Token,
                    AccessTokenSecret = tokens.TokenSecret,
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret
                };



                TwitterResponse<TwitterStatus> response = TwitterStatus.Update(accesstoken1, tweeterText, new StatusUpdateOptions() { UseSSL = true, APIBaseAddress = "http://api.twitter.com/1.1/" });

                //TwitterResponse<TwitterStatus> response = TwitterStatus.Update(
                //    accesstoken1,
                //    "Testing!! It works (hopefully).");

                //if (response.Result == RequestResult.Success)
                //{
                //    Response.Write("we did it!");
                //}
                //else
                //{
                //    Response.Write("it's all bad.");
                //}
            }

            }
            catch (Exception)
            {

                
            }
        }

 
       
    //post
        public void init(Guid Aphid_ID1)
        {
            //If User is not valid user
            if (HttpContext.Current.Request.QueryString["oauth_token"] == null)
            {
                //Step 1: Get Request Token
                OAuthTokenResponse RequestToken = OAuthUtility.GetRequestToken(consumerKey, consumerSecret, HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0]);

                //Step 2: Redirect User to Requested Token
                HttpContext.Current.Response.Redirect("http://twitter.com/oauth/authorize?oauth_token=" + RequestToken.Token);
            }
            else
            {
                //For Valid User
                string Oauth_Token = HttpContext.Current.Request.QueryString["oauth_token"].ToString();
                string oauth_verifier = HttpContext.Current.Request.QueryString["oauth_verifier"].ToString();
                var accessToken = OAuthUtility.GetAccessToken(consumerKey, consumerSecret, Oauth_Token, oauth_verifier);
                OAuthTokens access = new OAuthTokens();
                access.AccessToken = accessToken.Token;
                string oauth_token = accessToken.Token;
                string oauth_token_secret = accessToken.TokenSecret;
                DateTime Expire_in = DateTime.Now;
                if ((oauth_token != null) && (oauth_token_secret != null))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.ID = Guid.NewGuid();
                    model.category = "Twitter";
                    model.Access_Token = oauth_token;
                    model.Aphid_id = Aphid_ID1;
                    model.Expires = Expire_in;
                    model.IsDeleted = true;
                    model.RefereshToken = oauth_token_secret;
                    string status = social.GetData(model);
                    HttpContext.Current.Session["twstatus"] = status;
                }

                // set the oauth version and signature methods


                // create unique request details

            }


        }
        public string Post_on_Twitter(Guid id, string category, string[] data, string type_of_data, string track,string path_size)
        {
            string oauth_token = "";
            string oauth_token_secret = "";
            string title = "";
            string path = "";
            byte[] text;
            string data_size = "";
            string str = "";
            string token = social.Reterivetoken(id, category);
            if (token == "Invalid")
            {
                return "Setup";
            }
            if (token.Length > 0)
            {
                oauth_token = token.Split(',')[0];
                oauth_token_secret = token.Split(',')[1];
            }
            string oauth_nonce = Convert.ToBase64String(new System.Text.ASCIIEncoding().GetBytes(DateTime.Now.Ticks.ToString()));
            System.TimeSpan timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            string oauth_timestamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();
            // create oauth signature
            try
            {
                if (data.Length > 0)
                {
                    path = data[0];
                    title = data[1];
                }

                text = Encoding.ASCII.GetBytes(title);
                str = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" + path;
                bool status = social.Credit_Insert(id, "Twitter", type_of_data, "", str, title, track,true);
                string title_size = CalculateFileSize.Size(text);
                string val = Regex.Match(title_size, @"\d+\.\d+").Value;
                string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
                data_size = (float.Parse(val) + float.Parse(val1)).ToString();
                if (status == true)
                {
                    return "Twitter";
                }
                if (status == false)
                {
                    string pattern = string.Format(title + " " + str);
                    if (type_of_data == "Photos")
                    {

                        string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";
                        string baseString = string.Format(
                          baseFormat,
                          consumerKey,
                          oauth_nonce,
                          oauth_signature_method,
                          oauth_timestamp,
                          oauth_token,
                          oauth_version,
                          Uri.EscapeDataString(title + str)
                      );

                        string oauth_signature = null;
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(oauth_token_secret))))
                        {
                            oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes("POST&" + Uri.EscapeDataString(URL) + "&" + Uri.EscapeDataString(baseString))));
                        }

                        // create the request header
                        string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

                        string authorizationHeader = string.Format(
                            authorizationFormat,
                            Uri.EscapeDataString(consumerKey),
                            Uri.EscapeDataString(oauth_nonce),
                            Uri.EscapeDataString(oauth_signature),
                            Uri.EscapeDataString(oauth_signature_method),
                            Uri.EscapeDataString(oauth_timestamp),
                            Uri.EscapeDataString(oauth_token),
                            Uri.EscapeDataString(oauth_version)
                        );

                        HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                        objHttpWebRequest.Headers.Add("Authorization", authorizationHeader);
                        objHttpWebRequest.Method = "POST";
                        objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        using (Stream objStream = objHttpWebRequest.GetRequestStream())
                        {
                            byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(title + str));
                            objStream.Write(content, 0, content.Length);
                        }

                        var responseResult = "";
                        //success posting
                        WebResponse objWebResponse = objHttpWebRequest.GetResponse();
                        StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                        responseResult = objStreamReader.ReadToEnd().ToString();

                    }
                    else if (type_of_data == "Video")
                    {
                        string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";
                        string baseString = string.Format(
                          baseFormat,
                          consumerKey,
                          oauth_nonce,
                          oauth_signature_method,
                          oauth_timestamp,
                          oauth_token,
                          oauth_version,
                          Uri.EscapeDataString(title + str)
                      );

                        string oauth_signature = null;
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(oauth_token_secret))))
                        {
                            oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes("POST&" + Uri.EscapeDataString(URL) + "&" + Uri.EscapeDataString(baseString))));
                        }

                        // create the request header
                        string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

                        string authorizationHeader = string.Format(
                            authorizationFormat,
                            Uri.EscapeDataString(consumerKey),
                            Uri.EscapeDataString(oauth_nonce),
                            Uri.EscapeDataString(oauth_signature),
                            Uri.EscapeDataString(oauth_signature_method),
                            Uri.EscapeDataString(oauth_timestamp),
                            Uri.EscapeDataString(oauth_token),
                            Uri.EscapeDataString(oauth_version)
                        );

                        HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                        objHttpWebRequest.Headers.Add("Authorization", authorizationHeader);
                        objHttpWebRequest.Method = "POST";
                        objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        using (Stream objStream = objHttpWebRequest.GetRequestStream())
                        {
                            byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(title + str));
                            objStream.Write(content, 0, content.Length);
                        }

                        var responseResult = "";

                        //success posting
                        WebResponse objWebResponse = objHttpWebRequest.GetResponse();
                        StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                        responseResult = objStreamReader.ReadToEnd().ToString();



                    }
                    else if (type_of_data == "Music")
                    {
                        string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";
                        string baseString = string.Format(
                          baseFormat,
                          consumerKey,
                          oauth_nonce,
                          oauth_signature_method,
                          oauth_timestamp,
                          oauth_token,
                          oauth_version,
                          Uri.EscapeDataString(title + str)
                      );

                        string oauth_signature = null;
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(oauth_token_secret))))
                        {
                            oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes("POST&" + Uri.EscapeDataString(URL) + "&" + Uri.EscapeDataString(baseString))));
                        }

                        // create the request header
                        string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

                        string authorizationHeader = string.Format(
                            authorizationFormat,
                            Uri.EscapeDataString(consumerKey),
                            Uri.EscapeDataString(oauth_nonce),
                            Uri.EscapeDataString(oauth_signature),
                            Uri.EscapeDataString(oauth_signature_method),
                            Uri.EscapeDataString(oauth_timestamp),
                            Uri.EscapeDataString(oauth_token),
                            Uri.EscapeDataString(oauth_version)
                        );

                        HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                        objHttpWebRequest.Headers.Add("Authorization", authorizationHeader);
                        objHttpWebRequest.Method = "POST";
                        objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        using (Stream objStream = objHttpWebRequest.GetRequestStream())
                        {
                            byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(title + str));
                            objStream.Write(content, 0, content.Length);
                        }

                        var responseResult = "";

                        //success posting
                        WebResponse objWebResponse = objHttpWebRequest.GetResponse();
                        StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                        responseResult = objStreamReader.ReadToEnd().ToString();



                    }
                    else if (type_of_data == "Pdf")
                    {
                        string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";
                        string baseString = string.Format(
                          baseFormat,
                          consumerKey,
                          oauth_nonce,
                          oauth_signature_method,
                          oauth_timestamp,
                          oauth_token,
                          oauth_version,
                          Uri.EscapeDataString(title + str)
                      );

                        string oauth_signature = null;
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(oauth_token_secret))))
                        {
                            oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes("POST&" + Uri.EscapeDataString(URL) + "&" + Uri.EscapeDataString(baseString))));
                        }

                        // create the request header
                        string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

                        string authorizationHeader = string.Format(
                            authorizationFormat,
                            Uri.EscapeDataString(consumerKey),
                            Uri.EscapeDataString(oauth_nonce),
                            Uri.EscapeDataString(oauth_signature),
                            Uri.EscapeDataString(oauth_signature_method),
                            Uri.EscapeDataString(oauth_timestamp),
                            Uri.EscapeDataString(oauth_token),
                            Uri.EscapeDataString(oauth_version)
                        );

                        HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                        objHttpWebRequest.Headers.Add("Authorization", authorizationHeader);
                        objHttpWebRequest.Method = "POST";
                        objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        using (Stream objStream = objHttpWebRequest.GetRequestStream())
                        {
                            byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(title + str));
                            objStream.Write(content, 0, content.Length);
                        }

                        var responseResult = "";
                        //success posting
                        WebResponse objWebResponse = objHttpWebRequest.GetResponse();
                        StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                        responseResult = objStreamReader.ReadToEnd().ToString();


                    }
                    else
                    {
                        string baseFormat = "oauth_consumer_key={0}&oauth_nonce={1}&oauth_signature_method={2}" + "&oauth_timestamp={3}&oauth_token={4}&oauth_version={5}&status={6}";
                        string baseString = string.Format(
                          baseFormat,
                          consumerKey,
                          oauth_nonce,
                          oauth_signature_method,
                          oauth_timestamp,
                          oauth_token,
                          oauth_version,
                          Uri.EscapeDataString(title + str)
                      );

                        string oauth_signature = null;
                        using (HMACSHA1 hasher = new HMACSHA1(ASCIIEncoding.ASCII.GetBytes(Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(oauth_token_secret))))
                        {
                            oauth_signature = Convert.ToBase64String(hasher.ComputeHash(ASCIIEncoding.ASCII.GetBytes("POST&" + Uri.EscapeDataString(URL) + "&" + Uri.EscapeDataString(baseString))));
                        }

                        // create the request header
                        string authorizationFormat = "OAuth oauth_consumer_key=\"{0}\", oauth_nonce=\"{1}\", " + "oauth_signature=\"{2}\", oauth_signature_method=\"{3}\", " + "oauth_timestamp=\"{4}\", oauth_token=\"{5}\", " + "oauth_version=\"{6}\"";

                        string authorizationHeader = string.Format(
                            authorizationFormat,
                            Uri.EscapeDataString(consumerKey),
                            Uri.EscapeDataString(oauth_nonce),
                            Uri.EscapeDataString(oauth_signature),
                            Uri.EscapeDataString(oauth_signature_method),
                            Uri.EscapeDataString(oauth_timestamp),
                            Uri.EscapeDataString(oauth_token),
                            Uri.EscapeDataString(oauth_version)
                        );

                        HttpWebRequest objHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                        objHttpWebRequest.Headers.Add("Authorization", authorizationHeader);
                        objHttpWebRequest.Method = "POST";
                        objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
                        using (Stream objStream = objHttpWebRequest.GetRequestStream())
                        {
                            byte[] content = ASCIIEncoding.ASCII.GetBytes("status=" + Uri.EscapeDataString(title + str));
                            objStream.Write(content, 0, content.Length);
                        }

                        var responseResult = "";
                        //success posting
                        WebResponse objWebResponse = objHttpWebRequest.GetResponse();
                        StreamReader objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                        responseResult = objStreamReader.ReadToEnd().ToString();


                    }
                   
                    social.Credit_Insert(id, "Twitter", type_of_data, data_size, str, title,track, true);
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
                    model.category = "Twitter";
                    model.Aphid_id = id;
                    social.Deletedata(model);
                    return "deleted";
                }
                else if (name.Contains("403"))
                {
                    return "Twitter";
                }
                else
                {
                    social.Credit_Insert(id, "Twitter", type_of_data, data_size, str, title, track,false);
                    return "Timedout";
                }
            }

        }
        public void twUnlink(Guid id)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "Twitter";
            model.Aphid_id = id;
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            social.Deletedata(model);

        }
    }
}
