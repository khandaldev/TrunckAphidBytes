using AphidBytes.Web.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    public class MusicController : AphidController
    {
        //
        // GET: /Music/

        public ActionResult Index()
        {
            return View();
        }

    }
}
