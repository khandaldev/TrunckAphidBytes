using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Web.Extensions;
using AphidBytes.Web.Models;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.Utility;
using AphidBytes.Web.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AphidBytes.Web.Controllers
{
    //[SessionHelper]
    public class FeedBackController : AphidController
    {
        public ActionResult Index()
        {
            var model = new FeedbackModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(FeedbackModel feedback)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    feedback.Validation.FillFromModelState(ModelState);
                    return View(feedback);
                }

                var regexUtility = new RegexUtilities();
                if (!regexUtility.IsValidEmail(feedback.EmailAddress))
                {
                    feedback.Validation.AddError("The email address provided is not valid.");
                    return View(feedback);
                }

                FeedBackModel model = new FeedBackModel();
                IFeedBack feedBack = DependencyResolver.Current.GetService<IFeedBack>();
                if (AphidSession.Current.AuthenticatedUser?.Identity?.UserId  != null)
                {
                    Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId .ToString());
                    model.Userid = userID;
                }
                model.Email = feedback.EmailAddress;
                model.Text = feedback.Body;
                model.Subject = feedback.Subject;
                bool re = feedBack.InsertFeedBack(model);
                if (re == true)
                {
                    Email email = new Email();
                    //  email.sendMail(Email.Trim(), "FeedBack", feedback, "",model.Subject);
                    return View(new FeedbackModel { ConfirmationSent = true });
                }
                else
                {
                    feedback.Validation.AddError(ErrorConstants.GenericError);
                    return View(feedback);
                }
            }
            catch
            {
                var model = new FeedbackModel();
                model.Validation.AddError(ErrorConstants.GenericError);
                return View(model);
            }
        }
        
    }
}
