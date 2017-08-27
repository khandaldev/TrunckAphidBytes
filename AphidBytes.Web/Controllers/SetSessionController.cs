using AphidBytes.Web.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    public class SetSessionController : AphidController
    {
        //
        // GET: /SetSession/

        public ActionResult SetVariable(string key, string value)
        {
            Session["Flag"] = "Update";
            Session[key] = value;
            if (key == "AudioFile")
            {
                Session["SelectedAduio"] = value;
            }
            else { Session["SelectedImage"] = value; }
            return this.Json(new { success = true });
        }
    }
}
