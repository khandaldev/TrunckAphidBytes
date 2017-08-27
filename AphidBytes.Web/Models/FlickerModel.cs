using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using FlickrNet;

namespace AphidBytes.Web.Models
{
    public class FlickerModel
    {
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
        public void init(Guid Aphid_ID1)
        {

            if (HttpContext.Current.Request.QueryString["oauth_verifier"] == null && HttpContext.Current.Session["RequestToken"] == null)
            {
                Flickr f = FlickrManager.GetInstance();
                OAuthRequestToken token = f.OAuthGetRequestToken(HttpContext.Current.Request.Url.AbsoluteUri.Split('?')[0]);

                HttpContext.Current.Session["RequestToken"] = token;

                string url = f.OAuthCalculateAuthorizationUrl(token.Token, AuthLevel.Write);
                HttpContext.Current.Response.Redirect(url);
            }
            else
            {
                Flickr f = FlickrManager.GetInstance();
                OAuthRequestToken requestToken = HttpContext.Current.Session["RequestToken"] as OAuthRequestToken;
                OAuthAccessToken accessToken = f.OAuthGetAccessToken(requestToken, HttpContext.Current.Request.QueryString["oauth_verifier"].ToString());
                string oauth_token = accessToken.Token;
                string oauth_secret = accessToken.TokenSecret;
                DateTime Expire_in = DateTime.Now;
                if ((oauth_token != null) && (oauth_secret != null))
                {
                    SocialNetworkModel model = new SocialNetworkModel();
                    model.ID = Guid.NewGuid();
                    model.category = "Flicker";
                    model.Access_Token = oauth_token;
                    model.Aphid_id = Aphid_ID1;
                    model.Expires = Expire_in;
                    model.IsDeleted = true;
                    model.RefereshToken = oauth_secret;
                    string status = social.GetData(model);
                    HttpContext.Current.Session["flickstatus"] = status;
                }
            }
        }

        public void Flick_Unlink(Guid id)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "Flicker";
            model.Aphid_id = id;
            ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
            social.Deletedata(model);
        }
        public string Post_on_Flicker(Guid id, string category, string[] data, string type_of_data, string track,string path_size)
        {
            string oauth_token = "";
            string oauth_token_secret = "";
            string token = social.Reterivetoken(id, category);
            if (token == "Invalid")
            {
                return "Setup";
            }
            string path = "", title = "", str = "", data_size = "",tag="";
            byte[] text;
            if (token.Length > 0)
            {
                oauth_token = token.Split(',')[0];
                oauth_token_secret = token.Split(',')[1];
            }
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
                bool status = social.Credit_Insert(id, "Flicker", type_of_data, "", str, title, track,true);
                if (status == true)
                {
                    return "Flicker";
                }
                if (status == false)
                {
                    if (type_of_data == "Photos")
                    {
                        Flickr f = FlickrManager.GetInstance();
                        f.OAuthAccessToken = oauth_token;
                        f.OAuthAccessTokenSecret = oauth_token_secret;                      
                       // string pic = new Uri(str).LocalPath;
                        string pic = HttpContext.Current.Server.MapPath(path);
                       string demo = f.UploadPicture(pic, title,"Photos",tag);
                        string title_size = CalculateFileSize.Size(text);
                        string val = Regex.Match(title_size, @"\d+\.\d+").Value;
                        string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
                        data_size = (float.Parse(val) + float.Parse(val1)).ToString();
                        social.Credit_Insert(id, "Flicker", type_of_data, data_size, str, title,track, true);                        
                    }
                }
                return "inserted";
            }
            catch
            {
                social.Credit_Insert(id, "Flicker", type_of_data, data_size, str, title, track,false);
                return "Error";
            }
        }
    }
}