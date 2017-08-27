using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.App_Code;

namespace AphidBytes.Web.Models
{
    public class ScribdModel
    {
        ISocialNetwork social = DependencyResolver.Current.GetService<ISocialNetwork>();
        public string Scribd_init(SocialNetworkModel model)
        {

            HttpContext.Current.Session["scribd"] = social.GetData(model);
            return HttpContext.Current.Session["scribd"].ToString();
        }
        public string Scribd_post(Guid id, string category, string[] data, string type_of_data, string track, string path_size)
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
            string token = social.Reterivetoken(id, category);
            if (token == "Invalid")
            {
                return "Setup";
            }
            bool status = social.Credit_Insert(id, "Scribd", type_of_data, "", str, title, track, true);
            if (status == true)
            {
                return "Scribd";
            }
            else
            {
                if (type_of_data == "Pdf")
                {
                    Scribd.Net.Service.APIKey = "27yug6asbcpu1um9cargp";
                    Scribd.Net.Service.SecretKey = "61qo7is9mgwxps7bicy3sl19gr";
                    Scribd.Net.Service.PublisherID = "20017764678336700000";
                    Scribd.Net.Document _doc = new Scribd.Net.Document();
                    string title_size = CalculateFileSize.Size(text);
                    string val = Regex.Match(title_size, @"\d+\.\d+").Value;
                    string val1 = Regex.Match(path_size, @"\d+\.\d+").Value;
                    data_size = (float.Parse(val) + float.Parse(val1)).ToString();
                    bool login = Scribd.Net.User.Login(token.Split(',')[0], CryptorEngine.Decrypt(token.Split(',')[1], true));
                    if (login == false)
                    {
                        SocialNetworkModel model = new SocialNetworkModel();
                        model.IsDeleted = false;
                        model.category = "Scribd";
                        model.Aphid_id = id;
                        social.Deletedata(model);
                        return "deleted";
                    }
                    else
                    {
                        _doc.Title = title;
                        _doc = Scribd.Net.Document.Upload(HttpContext.Current.Server.MapPath(path), Scribd.Net.AccessTypes.Public, false);
                        if (_doc.DocumentId != null)
                        {
                            social.Credit_Insert(id, "Scribd", type_of_data, data_size, str, title, track, true);
                        }
                        else
                        {
                            social.Credit_Insert(id, "Scribd", type_of_data, data_size, str, title, track, false);
                            return "Timeout";
                        }
                    }                

                }
                return "inserted";
            }
        }
        public void Scribd_unlink(Guid aphid)
        {
            SocialNetworkModel model = new SocialNetworkModel();
            model.IsDeleted = false;
            model.category = "Scribd";
            model.Aphid_id = aphid;
            social.Deletedata(model);
        }
    }
}