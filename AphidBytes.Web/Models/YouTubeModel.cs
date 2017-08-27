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
using AphidBytes.Web.App_Code;
using Google.GData.Client;
using Google.GData.Extensions.MediaRss;
using Google.GData.YouTube;
using Google.YouTube;

namespace AphidBytes.Web.Models
{
    public class YouTubeModel
    {
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
        public string init(SocialNetworkModel model)
        {
            HttpContext.Current.Session["youtubestatus"] = social.GetData(model);
            return HttpContext.Current.Session["youtubestatus"].ToString();
        }
        public string Youtube_post(Guid id, string category, string[] data, string type_of_data, string track, string path_size)
        {
            string title = "";
            string path = "";
            byte[] text;
            string data_size = "";
            string str = "",tag="",cat="";
            if (data.Length > 0)
            {
                path = data[0];
                title = data[1];
                tag = data[2];
                cat = data[3];
            }
            text = Encoding.ASCII.GetBytes(title);
            str = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/" + path;
            string token = social.Reterivetoken(id, category);
            if (token == "Invalid")
            {
                return "Setup";
            }
            bool status = social.Credit_Insert(id, "YouTube", type_of_data, "", str, title, track, true);
            if (status == true)
            {
                return "YouTube";
            }
            else
            {
                if (type_of_data == "Videos")
                {
                   
                    string title_size = CalculateFileSize.Size(text);
                    string val = Regex.Match(title_size, @"\d+\.\d+").Value;
                    string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
                    data_size = (float.Parse(val) + float.Parse(val1)).ToString();
                    YouTubeRequestSettings settings = new YouTubeRequestSettings("Demo", "AIzaSyCiXB5Ej31vebftCIN7_JbrxBqV6lbF0EA", token.Split(',')[0], CryptorEngine.Decrypt(token.Split(',')[1], true));
                    settings.Timeout = 999999990;
                    settings.Maximum = 2000000000;
                    YouTubeRequest request = new YouTubeRequest(settings);
                    Video newVideo = new Video();
                    newVideo.Title = title;
                    newVideo.Tags.Add(new MediaCategory(tag, YouTubeNameTable.CategorySchema));
                    newVideo.Keywords = "";
                    newVideo.Description = "Videos";
                    newVideo.YouTubeEntry.Private = false;
                    newVideo.Categories.Add(new AtomCategory(cat, YouTubeNameTable.CategorySchema));
                    //newVideo.Tags.Add(new MediaCategory("","", YouTubeNameTable.DeveloperTagSchema));
                    int len = path.Split('.').Length;
                    string type = path.Split('.')[len-1].ToString();
                    string format = string.Format("video/{0}", type);
                    newVideo.YouTubeEntry.MediaSource = new MediaFileSource(HttpContext.Current.Server.MapPath(path),format);
                    try
                    {
                        Video createdVideo = request.Upload(newVideo);
                         social.Credit_Insert(id, "YouTube", type_of_data, data_size, str, title, track, true);
                        
                    }
                    catch(Exception ex)
                    {
                        if (ex.Message.Contains("credentials"))
                        {
                            SocialNetworkModel model = new SocialNetworkModel();
                            model.IsDeleted = false;
                            model.category = "YouTube";
                            model.Aphid_id = id;
                            social.Deletedata(model);
                            return "deleted";
                        }
                        else
                        {
                            social.Credit_Insert(id, "YouTube", type_of_data, data_size, str, title, track, false);
                            return "Timedout";
                        }
                    }
                }
                return "inserted";
            }
        }
        public void Youtube_unlink(Guid aphid)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "YouTube";
            model.Aphid_id = aphid;
            social.Deletedata(model);
        }
    }
}