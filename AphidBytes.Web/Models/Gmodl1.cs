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
using AphidBytes.Web.Models;

namespace AphidBytes.Web.Models
{
    //public class WebRequest
    //{
    //    string _profileId=;
    //    string key="AIzaSyCEnviQK7sxusO7n40ZJQajvIhDatG-8X4";
    //}

    //        public object GetUserData()
    //        {
    //            //try
    //            //{
    //                string requestString = "https://www.googleapis.com/plus/v1/people/" + _profileId + "?key=" + key;
    //                WebRequest objWebRequest = WebRequest.Create(requestString);
    //                WebResponse objWebResponse = objWebRequest.GetResponse();

    //                Stream objWebStream = objWebResponse.GetResponseStream();

    //                using (StreamReader objStreamReader = new StreamReader(objWebStream))
    //                {
    //                    string result = objStreamReader.ReadToEnd();

    //                    return result;
    //                }
                    
    //                JavaScriptSerializer js = new JavaScriptSerializer();
    //              //  string requestString = "https://www.googleapis.com/plus/v1/people/" 
    //              //+ _profileId + "?key=" + _apiKey;
    //                string result = GetWebData(requestString);
    //                GPlusPerson activity = js.Deserialize(result);
    


    //            }
    //            public class GPlusPerson
    //            {
    //             public string kind { get; set; }
    //             public string id { get; set; }
    //             public string displayName { get; set; }
    //             public string tagline { get; set; }
    //             public string birthday { get; set; }
    //             public string gender { get; set; }
    //             public string aboutMe { get; set; }
    //             public string url { get; set; }
    ////public GImage image { get; set; }
    ////public Url[] urls { get; set; }
    ////public Organization[] organizations { get; set; }
    ////public PlaceLived[] placesLived { get; set; }
}

                 
                //catch (Exception)
                //{
                //    throw new NotImplementedException();
                //}
            

   
