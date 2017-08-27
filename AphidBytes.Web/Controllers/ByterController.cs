using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
//using AphidBytes.Web.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AphidBytes.Web.Models;
using AphidBytes.Web.Session_Helper;
using AphidBytes.Web.Web;
using AphidBytes.Web.App_Code;
using AphidBytes.Web.Extensions;
using AphidBytes.Web.Utility;

namespace AphidBytes.Web.Controllers
{
    [SessionHelper]
    public class ByterController : AphidController
    {
        log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ByterController));
        //
        // GET: /Byter/
        public static byte[] ImgByte;
        private readonly IByter _byter;
        private readonly ICommon _cmn;
        public ByterController()
        {
            _byter = DependencyResolver.Current.GetService<IByter>();
            _cmn = DependencyResolver.Current.GetService<ICommon>();

        }
        public ActionResult ByterAccountInfo()
        {
            logger.Error("ByterAccountInfo");
            ByterAccountViewModel model = new ByterAccountViewModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _byter.GetByterAccountInfo(userID);
                IAccounts accountsService = DependencyResolver.Current.GetService<IAccounts>();
                var loginProfile = accountsService.LoginWithSocialSite(userID.ToString());
                int count = _byter.GetByterCount(userID);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = count;
                ViewBag.MegCount = count;
                if (!model.isActive.HasValue || !model.isActive.Value)
                {
                    model.Validation = new ValidationModel();
                    model.Validation.AddWarning("An email to verify your account was sent, check your Inbox or Spam folder");
                }
            }
            catch (Exception exx)
            {
                logger.Error(exx.Message + Environment.NewLine + exx.StackTrace);
                model.Validation.AddError("Oops, something happened, try again later");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ByterAccountInfo(ByterAccountViewModel bytermodel)
        {
            try
            {
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.Password);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.ConfirmPassword);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.PostalCode);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.Region);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.City);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.AddressLine1);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.AddressLine2);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.NameOnCard);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.CSV);
                ModelState.RemoveFor<AphidLabAccountModel>(x => x.UserName);

                var userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                var byterData = _byter.GetByterAccountInfo(userID);

                if (!ModelState.IsValid)
                {
                    bytermodel.UserName = byterData.UserName;
                    bytermodel.Validation.FillFromModelState(ModelState);
                    return View(bytermodel);
                }
                if (string.IsNullOrEmpty(byterData.ProfilePicturePath))
                {
                    ImageUploader.UploadProfilePictureAndSetLocation(bytermodel);
                }
                else
                {
                    if (ImageUploader.DeleteProfileImage(byterData.ProfilePicturePath))
                        ImageUploader.UploadProfilePictureAndSetLocation(bytermodel);
                }
                int count = _byter.GetByterCount(userID);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = count;
                ViewBag.MegCount = count;
                bytermodel.ByterUserID = userID;
                bytermodel.AddressID = byterData.AddressID;
                bytermodel.BankAccountID = byterData.BankAccountID;
                bytermodel.SecurityQuestionID = byterData.SecurityQuestionID;

                bool updateByter = _byter.UpdateByterAccountInfo(bytermodel);
                var aphidTiseData1 = _byter.GetByterAccountInfo(userID);

                return View(aphidTiseData1);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                return RedirectToAction("ByterAccountInfo");
            }
        }
        public ActionResult ChangePassword()
        {
            ByterAccountViewModel model = new ByterAccountViewModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _byter.GetByterAccountInfo(userID);
                int count = _byter.GetByterCount(userID);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = count;
                ViewBag.MegCount = count;
            }
            catch (Exception ex)
            {
                model.Validation.AddError("Oops, something happened, try again later");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ChangePassword(ByterAccountViewModel bytermodel)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                ByterAccountViewModel byterData = _byter.GetByterAccountInfo(userID);
                bytermodel.ByterUserID = userID;
                var encryptPwd = CryptorEngine.Encrypt(bytermodel.Password, true);
                var success = _cmn.UpdatePassword(userID, encryptPwd);
                if (!success)
                {
                    byterData.Validation.AddError("Unable to change the current password");
                }
                else
                {
                    byterData.Validation.AddInformation("Successfully changed the password");
                }

                return View(byterData);
            }
            catch (Exception)
            {
                return RedirectToAction("ByterAccountInfo");
            }
        }
        public ActionResult CreditCardInfo()
        {
            ByterAccountViewModel model = new ByterAccountViewModel();
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                model = _byter.GetByterAccountInfo(userID);
                int count = _byter.GetByterCount(userID);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = count;
                ViewBag.MegCount = count;
            }
            catch (Exception ex)
            {
                model.Validation.AddError("Oops, something happened, try again later");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult CreditCardInfo(ByterAccountViewModel bytermodel, string stripeToken)
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                ByterAccountViewModel byterData = _byter.GetByterAccountInfo(userID);
                var success = _cmn.UpdateStripeCard(userID, stripeToken);
                if (!success)
                {
                    byterData.Validation.AddError("Unable to change the credit card information on file");
                }
                else
                {
                    byterData.Validation.AddInformation("Successfully changed your credit card information");
                }

                return View(byterData);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + Environment.NewLine + ex.StackTrace);
                return RedirectToAction("ByterAccountInfo");
            }
        }

        public ActionResult GetPostQueueData()
        {
            try
            {


                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                List<GetPostData> li = new List<GetPostData>();
                li = _byter.GetPostData(userID);
                return Json(li);
            }
            catch
            {
                return RedirectToAction("ByterAccountInfo");
            }

        }
        public ActionResult Order(string Sort, string Class)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                List<GetPostData> li = new List<GetPostData>();
                List<GetPostData> list = new List<GetPostData>();
                var data = _byter.GetSortOrder(userID, Sort, Class);
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].poststatus == "True")
                    {
                        list.Add(data[i]);
                    }
                    else
                    {
                        if (data[i].Linktopost != true) { li.Add(data[i]); }
                        else { li.Add(data[i]); }

                    }

                }
                if (Class == "one")
                {
                    return Json(list);
                }
                else
                {
                    return Json(li);
                }

            }
            catch
            {
                return RedirectToAction("ByterAccountInfo");
            }
        }

        public ActionResult Searching(string Text, string Class)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                List<GetPostData> li = new List<GetPostData>();
                List<GetPostData> list = new List<GetPostData>();
                var data = _byter.GetPostData1(userID, Text, Class);
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].poststatus == "True")
                    {
                        list.Add(data[i]);
                    }
                    else
                    {
                        if (data[i].Linktopost != true) { li.Add(data[i]); }
                        else { li.Add(data[i]); }
                    }

                }
                if (Class == "one")
                {
                    return Json(list);
                }

                else
                {
                    return Json(li);
                }


            }
            catch
            {
                return RedirectToAction("ByterAccountInfo");
            }
        }

        public string Facebookpost(string track)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                FaceBookModel fbmodel = new FaceBookModel();
                string status = "", displaypath = "";
                if (list.CatID == 1)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.AudioFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.AudioFilePath.Substring(list.AudioFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Music", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Music", track, list.filesize);
                    }
                    if (status == "Facebook")
                    {
                        return "Already Posted on Facebook";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on the Facebook";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Facebook";
                    }
                    else
                        return "Authentication Failure on Facebook";
                }
                else if (list.CatID == 2)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.VideoFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.VideoFilePath.Substring(list.VideoFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Videos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1)); ;
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Videos", track, list.filesize);
                    }
                    if (status == "Facebook")
                    {
                        return "Already Posted on Facebook";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on the Facebook";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Facebook";
                    }
                    else
                        return "Authentication Failure on Facebook";
                }
                else if (list.CatID == 3)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadImageFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadImageFilePath.Substring(list.UploadImageFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Photos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath).Split('_')[1];
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Photos", track, list.filesize);
                    }
                    if (status == "Facebook")
                    {
                        return "Already Posted on Facebook";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on the Facebook";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Facebook";
                    }
                    else
                        return "Authentication Failure on Facebook";
                }
                else if (list.CatID == 4)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadPDFFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadPDFFilePath.Substring(list.UploadPDFFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Pdf", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Pdf", track, list.filesize);
                    }
                    if (status == "Facebook")
                    {
                        return "Already Posted on Facebook";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on the Facebook";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Facebook";
                    }
                    else
                        return "Authentication Failure on Facebook";
                }
                else if (list.CatID == 5)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.RARFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.RARFilePath.Substring(list.RARFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Files", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = fbmodel.PostTowall(userID, "Facebook", dat, "Files", track, list.filesize);
                    }
                    if (status == "Facebook")
                    {
                        return "Already Posted on Facebook";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on the Facebook";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Facebook";
                    }
                    else
                        return "Authentication Failure on Facebook";
                }
                return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }

        public string Linkedinpost(string track)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                LinkedLinModel link = new LinkedLinModel();
                string status = "", displaypath = "";
                if (list.CatID == 1)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.AudioFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.AudioFilePath.Substring(list.AudioFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Music", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Music", track, list.filesize);
                    }
                    if (status == "LinkedLin")
                    {
                        return "Already Posted on Linkedin";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Linkedin";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Linkedin";
                    }
                    else
                        return "Authentication Failure in Linkedin";
                }
                else if (list.CatID == 2)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.VideoFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.VideoFilePath.Substring(list.VideoFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Videos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Videos", track, list.filesize);
                    }
                    if (status == "LinkedLin")
                    {
                        return "Already Posted on Linkedin";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Linkedin";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Linkedin";
                    }
                    else
                        return "Authentication Failure in Linkedin";
                }
                else if (list.CatID == 3)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadImageFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadImageFilePath.Substring(list.UploadImageFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Photos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Photos", track, list.filesize);
                    }
                    if (status == "LinkedLin")
                    {
                        return "Already Posted on Linkedin";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Linkedin";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Linkedin";
                    }
                    else
                        return "Authentication Failure in Linkedin";
                }
                else if (list.CatID == 4)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadPDFFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadPDFFilePath.Substring(list.UploadPDFFilePath.IndexOf('_') + 1));

                        status = link.Post_to_link(userID, "LinkedLin", dat, "Pdf", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Pdf", track, list.filesize);
                    }
                    if (status == "LinkedLin")
                    {
                        return "Already Posted on Linkedin";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Linkedin";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Linkedin";
                    }
                    else
                        return "Authentication Failure in Linkedin";
                }
                else if (list.CatID == 5)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.RARFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.RARFilePath.Substring(list.RARFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Files", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = link.Post_to_link(userID, "LinkedLin", dat, "Files", track, list.filesize);
                    }
                    if (status == "LinkedLin")
                    {
                        return "Already Posted on Linkedin";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Linkedin";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured on Linkedin";
                    }
                    else
                        return "Authentication Failure in Linkedin";
                }
                return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }

        public string Twitterpost(string track)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                TwitterModel face = new TwitterModel();
                string status = "", displaypath = "";
                if (list.CatID == 1)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.AudioFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.AudioFilePath.Substring(list.AudioFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "LinkedLin", dat, "Music", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "Twitter", dat, "Music", track, list.filesize);
                    }
                    if (status == "Twitter")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Twitter";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on Twitter";
                }
                else if (list.CatID == 2)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.VideoFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.VideoFilePath.Substring(list.VideoFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "Twitter", dat, "Videos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "Twitter", dat, "Videos", track, list.filesize);
                    }
                    if (status == "Twitter")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Twitter";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on Twitter";
                }
                else if (list.CatID == 3)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadImageFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadImageFilePath.Substring(list.UploadImageFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "Twitter", dat, "Photos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "Twitter", dat, "Photos", track, list.filesize);
                    }
                    if (status == "Twitter")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Twitter";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on Twitter";
                }
                else if (list.CatID == 4)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadPDFFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadPDFFilePath.Substring(list.UploadPDFFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "Twitter", dat, "Pdf", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = face.Post_on_Twitter(userID, "Twitter", dat, "Pdf", track, list.filesize);
                    }
                    if (status == "Twitter")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Twitter";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on Twitter";
                }
                return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }

        public string YouTubepost(string track)
        {
            try
            {
                YouTubeModel tube = new YouTubeModel();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                string status = "", displaypath = "";
                if (list.CatID == 2)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.VideoFilePath, list.Title, list.Tag };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.VideoFilePath.Substring(list.VideoFilePath.IndexOf('_') + 1));
                        status = tube.Youtube_post(userID, "YouTube", dat, "Videos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = tube.Youtube_post(userID, "YouTube", dat, "Videos", track, list.filesize);
                    }
                    if (status == "Twitter")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on YouTube";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on You Tube";
                }
                else
                    return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }

        public string Soundcloudpost(string track)
        {
            try
            {
                SoundCloudModel link = new SoundCloudModel();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                string status = "", displaypath = "";
                if (list.CatID == 1)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.AudioFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.AudioFilePath.Substring(list.AudioFilePath.IndexOf('_') + 1));
                        status = link.POST(userID, "SoundCloud", dat, "Music", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = link.POST(userID, "SoundCloud", dat, "Music", track, list.filesize);
                    }
                    if (status == "SoundCloud")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on SoundCloud";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on SoundCloud";
                }
                return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }

        public string Scribdpost(string track)
        {
            try
            {
                ScribdModel daily = new ScribdModel();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                string status = "", displaypath = "";
                if (list.CatID == 4)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadPDFFilePath, list.Title };
                        displaypath = displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadPDFFilePath.Substring(list.UploadPDFFilePath.IndexOf('_') + 1));
                        status = daily.Scribd_post(userID, "Scribd", dat, "Pdf", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.RARFilePath, list.Title };
                        displaypath = displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = daily.Scribd_post(userID, "Scribd", dat, "Pdf", track, list.filesize);
                    }
                    if (status == "Scribd")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Scribd";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on Scribd";
                }
                return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }

        public string DailyMotionpost(string track)
        {
            try
            {
                Dailymotion daily = new Dailymotion();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                string status = "", displaypath = "";
                if (list.CatID == 2)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.VideoFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.VideoFilePath.Substring(list.VideoFilePath.IndexOf('_') + 1));
                        status = daily.post(userID, "DailyMotion", dat, "Videos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title, list.Tag };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = daily.post(userID, "DailyMotion", dat, "Videos", track, list.filesize);
                    }
                    if (status == "DailyMotion")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on DailyMotion";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on Dailymotion";
                }
                else
                    return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }

        public string Flickerpost(string track)
        {
            try
            {
                FlickerModel flick = new FlickerModel();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                AllGenerateCloneModel list = _byter.GetUploadfile(track, userID);
                string status = "", displaypath = "";
                if (list.CatID == 3)
                {
                    if ((list.UploadFilePath == null) || (list.UploadFilePath == ""))
                    {
                        string[] dat = new string[] { list.UploadImageFilePath, list.Title, list.Tag };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadImageFilePath.Substring(list.UploadImageFilePath.IndexOf('_') + 1));
                        status = flick.Post_on_Flicker(userID, "Flicker", dat, "Photos", track, list.filesize);
                    }
                    else
                    {
                        string[] dat = new string[] { list.UploadFilePath, list.Title };
                        displaypath = (System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + " / " + list.UploadFilePath.Substring(list.UploadFilePath.IndexOf('_') + 1));
                        status = flick.Post_on_Flicker(userID, "Flicker", dat, "Photos", track, list.filesize);
                    }
                    if (status == "Flicker")
                    {
                        return "Already Posted";
                    }
                    else if (status == "inserted")
                    {
                        return "Successfully Posted on Flicker";
                    }
                    else if (status == "Timedout")
                    {
                        return "Some Error Occured";
                    }
                    else
                        return "Authentication Failure on Flicker";
                }
                return "Retry";
            }
            catch
            {
                return "Session Expired.Please Again Login";
            }
        }
        public ActionResult UpdateMessageData(string TrackingNo)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string username = AphidSession.Current.AuthenticatedUser?.Identity?.Username.ToString();
                bool result = _byter.CheckMsgStatus(userID, TrackingNo);
                if (result == true)
                {
                    bool re = _byter.UpdateMessage(TrackingNo, userID, username);
                    return Json(re == true ? "True" : "False");

                }
                else
                {
                    return Json("AlreadyAdded");
                }

            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }




        public ActionResult UpdatePostQueue(string TrackNo)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                bool re = _byter.UpdatePostQueue(TrackNo);
                return Json(re == true ? "True" : "False");

            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult GetLinkToPostM()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                List<GetPostData> li = new List<GetPostData>();
                li = _byter.GetLinkToPostMData(userID);

                return Json(li);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult GetLink(string Data)
        {

            Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            var res = _byter.GetLinkPostRecord(user, Data);
            return Json(res);

        }

        public ActionResult ReduceMsgCount()
        {
            try
            {

                Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                bool res = _byter.ReduceMsgCount(user);
                return Json(res);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult Message()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                MessageModel model = new MessageModel();
                string result = _byter.GetMessageCount(userID);
                if (result != "0")
                {
                    string[] sp = result.Split('+');
                    model.TotalCredit = Convert.ToInt32(sp[0]);
                    model.NewCount = Convert.ToInt32(sp[1]);
                }
                else
                {
                    model.TotalCredit = 0;
                    model.NewCount = 0;
                }
                int count = _byter.GetByterCount(userID);
                //model.NewCount+=count;
                ViewBag.MegCount = count;
                ViewBag.TotalCredit = model.TotalCredit;
                List<GetPostData> li = new List<GetPostData>();
                li = _byter.GetByterMessage(userID);
                return View(li);
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }


        public ActionResult ShowNetwork(string Selected)
        {
            try
            {
                string track = Selected.Split(',')[0];
                string type = Selected.Split(',')[1];
                List<ShowSelectedNetwork> li = new List<ShowSelectedNetwork>();
                Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                li = _byter.Network(user, track, type);
                if (li != null)
                {
                    foreach (var item in li)
                    {
                        if ((item.Id == 1) && (item.category == "All"))
                        {

                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/ficone.png";

                        }
                        if ((item.Id == 2) && (item.category == "All"))
                        {
                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/iicone.png";

                        }
                        if ((item.Id == 3) && (item.category == "Music"))
                        {
                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/suicone.png";


                        }
                        if ((item.Id == 4) && (item.category == "Video"))
                        {
                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/youticone.png";

                        }
                        if ((item.Id == 5) && (item.category == "Photos"))
                        {
                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/flickricone.png";


                        }
                        if ((item.Id == 6) && (item.category == "Alter"))
                        {
                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/ticone.png";


                        }
                        if ((item.Id == 7) && (item.category == "Video"))
                        {
                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/vimeoicone.png";

                        }
                        if ((item.Id == 8) && (item.category == "Pdf"))
                        {
                            item.Path = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/img/scribdicone.png";

                        }
                    }
                    return Json(li);
                }

                else
                {
                    return Json(null);
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ByterErrorPage()
        {
            return View();
        }
        public ActionResult History()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                int cnt = _byter.GetByterCount(userID);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = cnt;
                ViewBag.MegCount = cnt;

                return View();
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult Favorites()
        {
            return View();
        }
        public ActionResult ByterCreditSummary()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                int cnt = _byter.GetByterCount(userID);
                MessageModel model = new MessageModel();
                model.NewCount = cnt;
                ViewBag.MegCount = cnt;
                List<GetPostData> li = new List<GetPostData>();
                List<GetPostData> list = new List<GetPostData>();
                li = _byter.GetPostedDataResult(userID);
                var count = _byter.GetTotalCredits(userID);


                for (int i = 0; i < li.Count; i++)
                {

                    if (li[i].poststatus == "True")
                    {
                        list.Add(new GetPostData()
                        {
                            Title = li[i].Title,
                            channel = li[i].channel,
                            Category = li[i].Category,
                            TrackingId = li[i].TrackingId,
                            Composer = li[i].Composer,
                            Credit = li[i].Credit


                        });
                        ViewBag.PostData = list;
                    }


                }
                ViewBag.Count = count;
                return View(list);
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult ByterPostQueue()
        {
            return View();
        }
        public ActionResult ByterLinksToPostM()
        {
            return View();
        }
        public ActionResult ByterAdCreditSummary()
        {
            try
            {

                Guid user = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                int cnt = _byter.GetByterCount(user);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = cnt;
                ViewBag.MegCount = cnt;

                return View();

            }
            catch (Exception)
            {

                return RedirectToAction("Loginuser", "Accounts");
            }

        }
        public ActionResult CreditSubscription()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                List<ChannelModel> model = new List<ChannelModel>();
                model = _byter.BindUserChannel(userID);
                int cnt = _byter.GetByterCount(userID);
                MessageModel mdl = new MessageModel();
                mdl.NewCount = cnt;
                ViewBag.MegCount = cnt;
                var count = _byter.GetTotalCredits(userID);

                ViewBag.Count = count;
                return View(model);
            }
            catch
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
        }

        public ActionResult UnSubChannel(string ChannelID)
        {
            try
            {
                IPremium pre = DependencyResolver.Current.GetService<IPremium>();
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                Guid Channel = new Guid(ChannelID);
                bool re = pre.UnsubscribeChannel(userID, Channel);
                return Json(re == true ? "True" : "False");

            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public ActionResult ShowChannel(string UserID, string PremiumID, string From)
        {
            return RedirectToAction("ChannelPage", "Premium", new { UserID = UserID, PremiumID = PremiumID, From = From });
        }

        public string before_file_preview(string Trackingnumber)
        {

            string cat = _byter.fetch_cat(Trackingnumber);
            return cat;
        }

        public ActionResult PlaylistPrivew()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();
                string Playlist_Name = Session["Playlist_Name"].ToString();
                AllGenerateCloneModel record = new AllGenerateCloneModel();
                try
                {
                    record = _byter.Get_A_Record_via_trackID_new(Trackingnumber);
                    ViewBag.Record_To_Preview = record;
                    ViewBag.Playlist_Name = Playlist_Name;
                }
                catch
                {
                    return RedirectToAction("LoginUser", "Accounts");
                }
                return View();
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }

        public string filePrivew1(string Trackingnumber, string category)
        {

            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            Session["Trackingnumber"] = Trackingnumber;
            if (category == "0")
            {
                return ("MusicPrivew");
            }

            if (category == "1")
            {
                return ("VideoPrivew");
            }
            if (category == "2")
            {
                return ("ArtAndPhotographyPrivew");
            }
            if (category == "4")
            {
                return ("FilePrivew");
            }
            if (category == "3")
            {
                return ("EbookPrivew");
            }
            else
            {
                return ("ByterCreditSummary");
            }


        }
        public ActionResult DeleteMessageRecords(string Track)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                var res = _cmn.Deleteitem(userID, Track);
                return Json(res);


            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

        }




        public ActionResult DeleteRecords(string Track)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                var res = _cmn.Deleteitem(userID, Track);
                return Json(res);


            }
            catch (Exception)
            {

                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public ActionResult Overview()
        {
            try
            {
                if (ImgByte != null)
                {
                    byte[] objByteArray = ImgByte;
                    return File(objByteArray, "image/jpeg");
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }
        public ActionResult MusicPrivew()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();
                string Song_To_Preview = null;
                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _byter.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        UploadAudioPath = li[i].UploadAudioPath,
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });

                    Song_To_Preview = li[i].UploadAudioPath;
                }

                ViewBag.Song_To_Preview = Song_To_Preview;
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult VideoPrivew()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();
                string Video_To_Preview = null;
                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _byter.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        UploadImagePath = li[i].UploadImagePath,
                        VideoFile = li[i].VideoFile,
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });
                    Video_To_Preview = li[i].VideoFile;

                }

                ViewBag.Video_To_Preview = Video_To_Preview;
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult FilePrivew()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _byter.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        VideoFile = li[i].VideoFile,
                        UploadFilePDFPath = li[i].PdfFilePath
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });


                }


                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult ArtAndPhotographyPrivew()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _byter.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        UploadImagePath = li[i].UploadImagePath,
                        Interruptedfile = li[i].Interruptedfile,
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });


                }
                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult EbookPrivew()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                string Trackingnumber = Session["Trackingnumber"].ToString();

                List<PremiumGenerateCloneModel> li = new List<PremiumGenerateCloneModel>();
                List<PremiumGenerateCloneModel> list = new List<PremiumGenerateCloneModel>();
                li = _byter.fileprivew(Trackingnumber);
                for (int i = 0; i < li.Count; i++)
                {

                    list.Add(new PremiumGenerateCloneModel()
                    {
                        Title = li[i].Title,
                        AlbumTitle = li[i].AlbumTitle,
                        ExplicitContent = li[i].ExplicitContent,
                        ArtistName = li[i].ArtistName,
                        Composer = li[i].Composer,
                        AvailableDownload = li[i].AvailableDownload,
                        TrackingNumber = li[i].TrackingNumber,
                        MatrixImageBytePath = li[i].MatrixImageBytePath,
                        VideoFile = li[i].VideoFile,
                        UploadFilePDFPath = li[i].PdfFilePath
                        //MatrixImage = li[i].MatrixImageBytePath.ToString(),
                        //Audio=li[i].InterruptedAudioPath,
                        //Image=li[i].UploadImagePath,
                        //Video=li[i].Video,
                    });


                }


                ViewBag.PostData = list;
                return View(list);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult Playlists()
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                int count = _byter.GetByterCount(userID);
                MessageModel mod = new MessageModel();
                mod.NewCount = count;
                ViewBag.MegCount = count;
                List<string> li = new List<string>();
                li = _byter.GetPlaylistNames(userID, null);
                if (li.Count != 0)
                {
                    ViewBag.PostdataPlaylist = li;
                }
                else
                {
                    ViewBag.PostdataPlaylist = null;
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
            return View();
        }

        public ActionResult GetPlayList(string TrackingID)
        {
            List<string> li = new List<string>();
            try
            {

                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                li = _byter.GetPlaylistNames(UserID, TrackingID);
            }
            catch (Exception)
            {
                return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(li);
        }

        public JsonResult GetSongList(string PlaylistName)
        {
            List<PlaylistModel> li = new List<PlaylistModel>();
            try
            {

                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                li = _byter.GetSongList(UserID, PlaylistName);
            }
            catch (Exception)
            {
                //               return RedirectToAction("Loginuser", "Accounts");
            }
            return Json(li);
        }

        public ActionResult DelSongFromPlay(string PlaylistName, string TrackingID)
        {
            bool result = false;
            try
            {

                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                result = _byter.DelSongFromPlay(PlaylistName, TrackingID);
            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
            return View(result);
        }
        public JsonResult AddSongToPlaylist(string PlaylistName, string TrackingID)
        {
            bool result = false;
            try
            {

                Guid UserID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                result = _byter.AddSongToPlaylist(PlaylistName, TrackingID, UserID);
            }
            catch (Exception)
            {

            }
            return Json(result);
        }

        public ActionResult deleteAccount()
        {
            try
            {
                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                bool result = _byter.deleteAccount(userID);
                return Json(result == true ? "Success" : "Failed");

            }
            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }
        }
        public ActionResult CreditSummary()
        {
            return View();
        }
        public ActionResult GetPostedDataResult(string trackingnumber)
        {
            try
            {

                Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
                var trackingId = trackingnumber;
                List<GetPostData> li = new List<GetPostData>();
                List<GetPostData> list = new List<GetPostData>();
                li = _byter.GetPostedDataResult(userID);

                for (int i = 0; i < li.Count; i++)
                {

                    if (li[i].poststatus == "True")
                    {
                        list.Add(new GetPostData()
                        {
                            Title = li[i].Title,
                            channel = li[i].channel,
                            Category = li[i].Category,
                            TrackingId = li[i].TrackingId,
                            Composer = li[i].Composer,
                            Credit = li[i].Credit
                        });
                    }


                }
                return Json(list);
            }

            catch
            {
                return RedirectToAction("LoginUser", "Accounts");
            }

        }

        public string Before_PlaylistPrivew(string TrackingNumber, string Playlist_Name)
        {
            Session["Trackingnumber"] = TrackingNumber;
            Session["Playlist_Name"] = Playlist_Name;
            return "PlaylistPrivew";
        }

        public ActionResult Verification(Guid id)
        {


            ByterAccountViewModel byterData = _byter.GetByterAccountInfo(id);
            if (byterData.isActive == true)
            {
                ViewBag.Message = "you are already verified";
            }
            else
            {
                bool result = _byter.VerifyEmailAccount(id);
                if (result)
                {
                    ViewBag.Message = "you account has been verified";
                }
                else
                {
                    ViewBag.Message = "there is a problem with your account, contact support@aphidbyte.com";
                }
            }
            return View();



        }

        public ActionResult SendVerificationMail()
        {
            Guid userID = new Guid(AphidSession.Current.AuthenticatedUser?.Identity?.UserId.ToString());
            ByterAccountViewModel byterData = _byter.GetByterAccountInfo(userID);
            Email mail = new Email();//send mail                
            mail.sendMaill(byterData.ByterUserID, byterData.EmailAddress, "Byter", new Guid(), byterData.UserName, "VerifyEmail");
            return View();
        }
    }
}
