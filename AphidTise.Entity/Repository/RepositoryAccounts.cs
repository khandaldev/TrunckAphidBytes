using System.ComponentModel;
using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.IO;
using System.Text;
using AphidBytes.Accounts.Contracts.ContentServers;
using AphidBytes.Core.Images;
using AphidBytes.Core.PaymentServices;
using AphidTise.Entity.DataMapper;
namespace AphidTise.Entity.Repository
{
    public class RepositoryAccounts : GenericRepository<tblAphidTiseAccount>
    {
        public bool RegisterAphidTiseAccount(AphidTiseAccountViewModel accountData)
        {
            Guid aphidTiseRegistreID = Guid.NewGuid();
            Guid addressID = Guid.NewGuid();
            Guid secuirityQuesionID = Guid.NewGuid();

            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    var stripeCustomer = StripeClient.CreateStripeCustomer(StripePackages.AphidLabs, accountData.EmailAddress, accountData.FirstName, accountData.LastName, accountData.AccountIdAphid.ToString(), accountData.StripeToken);
                    if (stripeCustomer == null)
                    {
                        throw new Exception($"Could not create stripe customer for user {accountData.EmailAddress}");
                    }

                    //Insert data in Personal Address
                    context.sp_InsertPersonAddress(addressID, accountData.AddressLine1, accountData.AddressLine2, accountData.City, accountData.Region, accountData.PostalCode);

                    //Insert data in Security Queastion Answer
                    context.sp_InsertSecurityQuestions(secuirityQuesionID, accountData.SecurityQuestion1, accountData.Answer1, accountData.SecurityQuestion2, accountData.Answer2);

                    //Insert data in AphidTise Table
                    context.sp_InsertAphidTiseRegstration(accountData.AccountIdAphid, accountData.UserName, accountData.Password, accountData.FirstName, accountData.LastName, accountData.EmailAddress, accountData.DOB, accountData.Phone, addressID, secuirityQuesionID, 2, accountData.CreateDate, accountData.CreateDate, false, accountData.Informations, accountData.Website, accountData.ProductService);

                    //Insert data in user table
                    context.sp_InsertUsers(accountData.AccountIdAphid, accountData.UserName, accountData.Password, false, 3, accountData.ProfilePicturePath, accountData.ProfilePictureServerId, accountData.EmailAddress, stripeCustomer.Id);

                    tranScope.Complete();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool RegisterBasicAccount(BasicAccountViewModel accountData)
        {
            Guid basicAccountUserID = Guid.NewGuid();
            Guid addressID = Guid.NewGuid();
            Guid secuirityQuesionID = Guid.NewGuid();

            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    var stripeCustomer = StripeClient.CreateStripeCustomer(StripePackages.Basic, accountData.EmailAddress, accountData.FirstName, accountData.LastName, accountData.AccountIdBasic.ToString(), accountData.StripeToken, accountData.PromoCode);
                    if (stripeCustomer == null)
                    {
                        throw new Exception($"Could not create stripe customer for user {accountData.EmailAddress}");
                    }

                    //Insert data in Personal Address
                    context.sp_InsertPersonAddress(addressID, accountData.AddressLine1, accountData.AddressLine2, accountData.City, accountData.Region, accountData.PostalCode);

                    //Insert data in Security Queastion Answer
                    context.sp_InsertSecurityQuestions(secuirityQuesionID, accountData.SecurityQuestion1, accountData.Answer1, accountData.SecurityQuestion2, accountData.Answer2);

                    //Insert data in AphidTise Table
                    context.sp_InsertBasicAccount(accountData.AccountIdBasic, accountData.UserName, accountData.Password, accountData.FirstName, accountData.LastName, accountData.EmailAddress, accountData.DOB, accountData.Phone, null, null, accountData.WebSiteUrl, addressID, secuirityQuesionID, 2, DateTime.Now, DateTime.Now, false);

                    //Insert data in user table
                    context.sp_InsertUsers(accountData.AccountIdBasic, accountData.UserName, accountData.Password, false, 2, accountData.ProfilePicturePath, accountData.ProfilePictureServerId, accountData.EmailAddress, stripeCustomer.Id);

                    //Insert Into Data Plan Table

                    context.sp_InsertDataPlan(accountData.AccountIdBasic, "5GB", null, "5368709120", 2, DateTime.Now.AddYears(1));

                    context.sp_AudioInteruptionFile(accountData.AccountIdBasic, "/DefaultFiles/DEFAULT_APHIDBYTE_WARNING_AUDIO.MP3", "Default", false, System.DateTime.Now, System.DateTime.Now);

                    context.sp_InsertUpdateImageInteruptionFileNew(accountData.AccountIdBasic, accountData.imageInterruption, accountData.watermarkImageName, accountData.isActive, accountData.Flag);

                    if(!string.IsNullOrEmpty( accountData.SocialNetworkSource))
                        InsertSocialNetworkLogin(accountData.SocialNetworkSource, accountData.AccountIdBasic, accountData.UserName);
                    // context.sp_InsertUpdateImageInteruptionFileNew(accountData.AccountIdBasic, "/DefaultFiles/Logo_Tech_.png", "Default", false, "INS");
                    tranScope.Complete();
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool RegisterByterAccount(ByterAccountViewModel accountData)
        {
            Guid byterAccountUserID = Guid.NewGuid();
            Guid addressID = Guid.NewGuid();
            Guid secuirityQuesionID = Guid.NewGuid();
            
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    var stripeCustomer = StripeClient.CreateStripeCustomer(StripePackages.Byter, accountData.EmailAddress, accountData.FirstName, accountData.LastName, accountData.AccountID.ToString(), accountData.StripeToken);                    
                    if (stripeCustomer == null)
                    {
                        throw new Exception($"Could not create stripe customer for user {accountData.EmailAddress}");
                    }

                    //Insert data in Personal Address
                    context.sp_InsertPersonAddress(addressID, accountData.AddressLine1, accountData.AddressLine2, accountData.City, accountData.Region, accountData.PostalCode);

                    //Insert data in Security Queastion Answer
                    context.sp_InsertSecurityQuestions(secuirityQuesionID, accountData.SecurityQuestion1, accountData.Answer1, accountData.SecurityQuestion2, accountData.Answer2);

                    //Insert data in AphidTise Table
                    context.sp_InsertByterAccount(accountData.AccountID, accountData.UserName, accountData.Password, accountData.FirstName, accountData.LastName, accountData.EmailAddress, accountData.DOB, accountData.Phone, accountData.RecoveryEmail, addressID, secuirityQuesionID, 1, DateTime.Now, DateTime.Now, false);

                    //Insert Data in Users Login Table Name :- tblUsers
                    context.sp_InsertUsers(accountData.AccountID, accountData.UserName, accountData.Password, false, 1, accountData.ProfilePicturePath, accountData.ProfilePictureServerId, accountData.EmailAddress, stripeCustomer.Id);

                    if (!string.IsNullOrEmpty(accountData.SocialNetworkSource))
                        InsertSocialNetworkLogin(accountData.SocialNetworkSource, accountData.AccountID, accountData.UserName);

                    tranScope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(@"D:\Errorlog.txt", true);
                sw.WriteLine(System.DateTime.Now + "Error Message Exception :- " + ex.Message);
                sw.WriteLine(System.DateTime.Now + "Error Message InnerException :- " + ex.InnerException);
                sw.WriteLine(System.DateTime.Now + "Error Message Source :- " + ex.Source);
                sw.WriteLine(System.DateTime.Now + "Error Message TargetSite :- " + ex.TargetSite);
                sw.Flush();
                sw.Close();

                return false;
            }
        }
        public bool RegisterPremiumAccount(PremiumAccountViewModel accountData)
        {
            Guid premiumAccountUserID = Guid.NewGuid();
            Guid bankAccountID = Guid.NewGuid();
            Guid addressID = Guid.NewGuid();
            Guid secuirityQuesionID = Guid.NewGuid();
            Guid ChannelID = Guid.NewGuid();
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    var stripeCustomer = StripeClient.CreateStripeCustomer(StripePackages.Premium, accountData.EmailAddress, accountData.FirstName, accountData.LastName, accountData.AccountIDPre.ToString(), accountData.StripeToken);
                    if (stripeCustomer == null)
                    {
                        throw new Exception($"Could not create stripe customer for user {accountData.EmailAddress}");
                    }

                    //Insert data in Personal Address
                    context.sp_InsertPersonAddress(addressID, accountData.AddressLine1, accountData.AddressLine2, accountData.City, accountData.Region, accountData.PostalCode);

                    //Insert data in Security Queastion Answer
                    context.sp_InsertSecurityQuestions(secuirityQuesionID, accountData.SecurityQuestion1, accountData.Answer1, accountData.SecurityQuestion2, accountData.Answer2);

                    //Insert data in AphidTise Table
                    context.sp_InsertPremiumAccount(accountData.AccountIDPre, accountData.UserName, accountData.Password, accountData.DOB, accountData.FirstName, accountData.LastName, accountData.Biography, accountData.Website, accountData.EmailAddress, accountData.Phone, addressID, secuirityQuesionID, 3, DateTime.Now, DateTime.Now, false);

                    //Insert data in user table premium 3
                    context.sp_InsertUsers(accountData.AccountIDPre, accountData.UserName, accountData.Password, false, 3, accountData.ProfilePicturePath, accountData.ProfilePictureServerId, accountData.EmailAddress, stripeCustomer.Id);

                    //Insert DataPlan Table
                    context.sp_InsertDataPlan(accountData.AccountIDPre, "5 GB", null, "5368709120", 3, DateTime.Now.AddYears(1));

                    //Insert Channel Page
                    var user = new tblChannelPage { ChannelBiography = "", ChannelID = ChannelID, ChannelImagePath = "", CreatedDate = System.DateTime.Now, ModifiedDate = null, UserID = accountData.AccountIDPre, UserName = accountData.UserName };
                    context.tblChannelPages.Add(user);
                    context.SaveChanges();

                    if (!string.IsNullOrEmpty(accountData.SocialNetworkSource))
                        InsertSocialNetworkLogin(accountData.SocialNetworkSource, accountData.PremiumUserID, accountData.UserName);

                        tranScope.Complete();
                }
                return true;
            }
            catch
            {
                throw;
            }
        }
        public bool RegisterAphidlabAccount(AphidLabAccountModel accountData)
        {
            Guid AphidLabAccountUserID = Guid.NewGuid();
            Guid addressID = Guid.NewGuid();
            Guid secuirityQuesionID = Guid.NewGuid();

            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    var stripeCustomer = StripeClient.CreateStripeCustomer(StripePackages.AphidLabs, accountData.EmailAddress, accountData.FirstName, accountData.LastName, accountData.AphidlabUserID.ToString(), accountData.StripeToken, accountData.PromoCode);
                    if (stripeCustomer == null)
                    {
                        throw new Exception($"Could not create stripe customer for user {accountData.EmailAddress}");
                    }

                    //Insert data in Personal Address
                    context.sp_InsertPersonAddress(addressID, accountData.AddressLine1, accountData.AddressLine2, accountData.City, accountData.Region, accountData.PostalCode);

                    //Insert data in Security Queastion Answer
                    context.sp_InsertSecurityQuestions(secuirityQuesionID, accountData.SecurityQuestion1, accountData.Answer1, accountData.SecurityQuestion2, accountData.Answer2);

                    //Insert data in AphidTise Table
                    context.sp_InsertAphidlabAccount(accountData.AphidlabUserID, accountData.UserName, accountData.EmailAddress, accountData.Password, accountData.FirstName, accountData.LastName, accountData.DOB, accountData.Phone, "", "", addressID, secuirityQuesionID, 4, DateTime.Now, DateTime.Now, true);

                    //Insert Data in Users Login Table Name :- tblUsers
                    context.sp_InsertUsers(accountData.AphidlabUserID, accountData.UserName, accountData.Password, false, 4, accountData.ProfilePicturePath, accountData.ProfilePictureServerId, accountData.EmailAddress, stripeCustomer.Id);

                    //INsert In datalimit table
                    context.sp_InsertDataPlan(accountData.AphidlabUserID, "5GB", null, "5368709120", 4, DateTime.Now.AddYears(1));
                    if (!string.IsNullOrEmpty(accountData.SocialNetworkSource))
                        InsertSocialNetworkLogin(accountData.SocialNetworkSource, accountData.AphidlabUserID.Value, accountData.UserName);

                    tranScope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(@"D:\Errorlog.txt", true);
                sw.WriteLine(System.DateTime.Now + "Error Message Exception :- " + ex.Message);
                sw.WriteLine(System.DateTime.Now + "Error Message InnerException :- " + ex.InnerException);
                sw.WriteLine(System.DateTime.Now + "Error Message Source :- " + ex.Source);
                sw.WriteLine(System.DateTime.Now + "Error Message TargetSite :- " + ex.TargetSite);
                sw.Flush();
                sw.Close();

                return false;
            }
            throw new NotImplementedException();
        }

        public string AccountInfo(string user, int type)
        {
            bool IsTrue = false;
            string value = "";
            string Email = "";
            try
            {
                if (type == 3)
                {

                    bool any = context.tblBasicAccounts.Any(m => !string.IsNullOrEmpty(m.Phone) && m.Phone.Equals(user));
                    bool any1 = context.tblByterAccounts.Any(m => !string.IsNullOrEmpty(m.Phone) && m.Phone.Equals(user));
                    bool any2 = context.tblAphidlabAccounts.Any(m => !string.IsNullOrEmpty(m.Phonenumber) && m.Phonenumber.Equals(user));
                    bool any3 = context.tblAphidTiseAccounts.Any(m => !string.IsNullOrEmpty(m.Phone) && m.Phone.Equals(user));
                    if (any || any1 || any2 || any3)
                    {
                        Email = "Phone Number Already Exist";
                        return Email;
                    }
                }
                List<ForgetPassword> li = new List<ForgetPassword>();
                var data = context.sp_forgetpassword().ToList();

                foreach (var item in data)
                {
                    li.Add(new ForgetPassword()
                    {
                        EmailAddress = item.EmailAddress,
                        UserName = item.UserName

                    });
                }
                if (type == 1)
                {
                    for (int i = 0; i < li.Count; i++)
                    {
                        if (user != null)
                        {
                            if ((li[i].UserName.Equals(user)))
                            {
                                IsTrue = true;
                                Email = "UserName Already Exist";
                                break;
                            }
                        }
                    }
                }
                if (type == 2)
                {
                    for (int i = 0; i < li.Count; i++)
                    {
                        if (user != null)
                        {
                            if (li[i].EmailAddress!=null)
                            {
                                if ((li[i].EmailAddress.Equals(user)))
                                {
                                    IsTrue = true;
                                    Email = "Email Already Exist";
                                    break;
                                }
                                
                            }
                          
                        }
                    }
                }

                return Email;
            }
            catch
            {
                throw;
            }
        }

        public bool InsertUserSubscribe(UserSubscribeModel accountData)
        {
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {
                    //Insert data in AphidTise Table
                    context.sp_InserttblUserSubscribe(accountData.SubscribeUserId, accountData.UserId);

                    tranScope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter(@"D:\Errorlog.txt", true);
                sw.WriteLine(System.DateTime.Now + "Error Message Exception :- " + ex.Message);
                sw.WriteLine(System.DateTime.Now + "Error Message InnerException :- " + ex.InnerException);
                sw.WriteLine(System.DateTime.Now + "Error Message Source :- " + ex.Source);
                sw.WriteLine(System.DateTime.Now + "Error Message TargetSite :- " + ex.TargetSite);
                sw.Flush();
                sw.Close();

                return false;
            }
        }

        public sp_gettblUserSubscribe_Result getUserSubscribe(Guid? subscribeID,Guid? userID)
        {
            return context.sp_gettblUserSubscribe(subscribeID, userID).FirstOrDefault();
        }
        public LoginProfileDto LoginWithUser(LoginUser user)
        {
            bool status = false;
            sp_UserLogin_Result ff = null;
            tblLoginTokens lt = null; 
            try
            {
                var ss = context.sp_GetUserStatus(user.UserName, user.Password).FirstOrDefault();

                if (ss == null)
                {
                    return null;
                }

                var userInfo = context.tblUsers.Where(u => u.UserId == ss.UserId).SingleOrDefault();

                status = true;
                user.Status = status;
                ss.UserStatus = user.Status;

                using (TransactionScope tranScope = new TransactionScope())
                {
                    ff = context.sp_UserLogin(user.UserName, user.Password).SingleOrDefault();

                    lt = new tblLoginTokens { AccountTypeId = ss.AccountTypeID ?? 0, ExpirationDate = DateTime.UtcNow.AddDays(7d), UserId = ss.UserId, TokenId = Guid.NewGuid() }; 

                    context.tblLoginTokens.Add(lt);

                    context.SaveChanges();
                    tranScope.Complete(); 
                }

                return new LoginProfileDto
                {
                    AccountTypeId = lt.AccountTypeId, 
                    ExpirationDate = lt.ExpirationDate, 
                    UserId = lt.UserId.ToString(), 
                    Token = lt.TokenId.ToString(), 
                    Username = ff.UserName, 
                    EmailAddress = ff.EmailAddress,
                    ProfilePicture = userInfo.PicturePath,//GetLoginProfilePicture(userInfo),
                    Status = true,
                };
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public LoginProfileDto LoginWithSocialSite(string userId)
        {
            var guidFromUserId = new Guid(userId);
            tblLoginTokens lt = null;

            try
            {
                var userInfo = context.tblUsers.Where(user => user.UserId == guidFromUserId).SingleOrDefault();
                if (userInfo == null)
                {
                    return null;
                }

                using (TransactionScope tranScope = new TransactionScope())
                {
                    lt = new tblLoginTokens { AccountTypeId = userInfo.AccountTypeID ?? 0, ExpirationDate = DateTime.UtcNow.AddDays(7d), UserId = userInfo.UserId, TokenId = Guid.NewGuid() };

                    context.tblLoginTokens.Add(lt);

                    context.SaveChanges();
                    tranScope.Complete();
                }

                return new LoginProfileDto
                {
                    AccountTypeId = lt.AccountTypeId,
                    ExpirationDate = lt.ExpirationDate,
                    UserId = lt.UserId.ToString(),
                    Token = lt.TokenId.ToString(),
                    Username = userInfo.UserName,
                    EmailAddress = userInfo.EmailAddress,
                    ProfilePicture = userInfo.PicturePath,// GetLoginProfilePicture(userInfo),
                    Status = true,
                };
            }
            catch
            {
                throw;
            }
        }
        public LoginProfileDto LoginWithToken(string token)
        {
            var guidFromToken = new Guid(token); 

            try
            {
                var loginToken = context.tblLoginTokens.Where(lt => lt.TokenId == guidFromToken).SingleOrDefault(); 
                if (loginToken == null)
                {
                    return null;
                }

                var userInfo = context.tblUsers.Where(user => user.UserId == loginToken.UserId).SingleOrDefault(); 
                if (userInfo == null)
                {
                    return null;
                }

                return new LoginProfileDto
                {
                    AccountTypeId = loginToken.AccountTypeId,
                    ExpirationDate = loginToken.ExpirationDate,
                    UserId = loginToken.UserId.ToString(),
                    Token = loginToken.TokenId.ToString(),
                    Username = userInfo.UserName,
                    EmailAddress = userInfo.EmailAddress,
                    ProfilePicture = userInfo.PicturePath,//GetLoginProfilePicture(userInfo),
                    Status = true,
                };
            }
            catch
            {
                throw;
            }
        }
        private string GetLoginProfilePicture(tblUser userInfo)
        {
            var contentServer = ContentServerManager.GetServerById(userInfo.PictureServerId ?? 0);
            if (contentServer == null)
            {
                contentServer = ContentServerManager.GetDefaultContentServer();
            }
            var profilePicturePath = string.IsNullOrWhiteSpace(userInfo.PicturePath)
                ? ImageUtility.DefaultProfilePicturePath
                : userInfo.PicturePath;
            return contentServer.GetUri(profilePicturePath);
        }

        public bool InserActivation(Guid userId, Guid token, string username)
        {
            try
            {
                int re = context.sp_UserActivation(userId, token, username, false);
                if (re == 1)
                {
                    return true;
                }
                else { return false; }
            }
            catch
            {
                throw;
            }
        }

        public string activateUser(string token)
        {
            Guid g= new Guid(token);
            List<string> li = new List<string>();
            string msg = "";
            try
            {
                var data = context.sp_VerifyUser(token).SingleOrDefault();
                if (data != null)
                {
                    if (data.TokenId.ToString() == token)
                    {
                        if (data.Status == true)
                        {
                            msg = "User Already Activated";
                        }
                        else
                        {
                            // context.sp_ActivateUser(token, 2, data.UserName);
                            var uactivate = context.tblUserActivations.Where(m => m.TokenId == g).SingleOrDefault();
                            if (uactivate.TokenId != null)
                            {
                                uactivate.Status = true;
                            }
                            context.SaveChanges();
                            var tblup = context.tblUsers.Where(m => m.UserName == data.UserName).SingleOrDefault();
                            if (tblup.UserName != null)
                            {
                                tblup.UserStatus = true;
                            }
                            context.SaveChanges();
                            msg = "User Activated";
                        }
                    }
                    else
                    {
                        msg = "Invalid User";
                    }

                }
                else
                {
                    msg = "Invalid User";
                }
                return msg;
            }
            catch
            {
                throw;
            }
        }

        public bool changePassword(string pass, string userid)
        {
            bool ss=false;
            try
            {
                var guid = Guid.Parse(userid);
                int re = context.sp_ChangePassword(guid.ToString(), pass);
                if (re == 1)
                {
                    ss = true;
                }
                return ss;
            }
            catch
            {
                throw;
            }
        }

        public ForgetPassword ForgetPasword(ForgetPassword model)
        { 
            bool IsTrue=false;
            string Email = null;
            string username = "";
            string accType = "";
            int i;
            List<ForgetPassword> li = new List<ForgetPassword>();
            try
            {
                var data = context.sp_GetUserList().ToList();
                if (data != null)
                {

                    foreach (var item in data)
                    {
                        li.Add(new ForgetPassword()
                        {
                            EmailAddress = item.EmailAddress,
                            UserName = item.UserName,
                            AccountType = item.AccType
                        });
                    }
                    for (i = 0; i < li.Count; i++)
                    {
                        if (model.UserName != null)
                        {
                            if ((li[i].UserName.Contains(model.UserName)))
                            {
                                IsTrue = true;
                                Email = li[i].EmailAddress;
                                accType = li[i].AccountType;
                                username = li[i].UserName;
                            }
                        }
                        if (model.EmailAddress != null)
                        {
                            if ((li[i].EmailAddress.Contains(model.EmailAddress)))
                            {
                                IsTrue = true;
                                Email = li[i].EmailAddress;
                                accType = li[i].AccountType;
                                username = li[i].UserName;
                                model.AccountType = accType;
                                model.UserName = username;
                                model.UserId = li[i].UserId;
                                break;
                            }
                        }
                    }
                    if (Email != null)
                    {
                        if (model.UserName != null)
                        {
                            var dat1 = context.tblUsers.Where(m => m.UserName == username).SingleOrDefault();
                            if (dat1 != null)
                            {
                                dat1.UserPassword = "ChangeReq";
                                context.SaveChanges();
                            }
                            if (accType == "Basic Account")
                            {
                                var dat2 = context.tblBasicAccounts.Where(m => m.EmailAddress == Email).SingleOrDefault();
                                if (dat2 != null)
                                {
                                    dat2.Password = "ChangeReq";
                                    context.SaveChanges();
                                }
                            }
                            if (accType == "ByterAccount")
                            {
                                var dat2 = context.tblByterAccounts.Where(m => m.EmailAddress == Email).SingleOrDefault();
                                if (dat2 != null)
                                {
                                    dat2.Password = "ChangeReq";
                                    context.SaveChanges();
                                }
                            }
                            if (accType == "AphidTiseAccount")
                            {
                                var dat2 = context.tblAphidTiseAccounts.Where(m => m.EmailAddress == Email).SingleOrDefault();
                                if (dat2 != null)
                                {
                                    dat2.Password = "ChangeReq";
                                    context.SaveChanges();
                                }
                            }
                            if (accType == "Premium  Account")
                            {
                                var dat2 = context.tblPremiumAccounts.Where(m => m.EmailAddress == Email).SingleOrDefault();
                                if (dat2 != null)
                                {
                                    dat2.Password = "ChangeReq";
                                    context.SaveChanges();
                                }
                            }
                        }
                    }
                }
                return model;
            }
            catch
            {
                throw;
            }
        }

        public bool InsertForgetPasswordDetail(ForgetPassword ob)
        {
            bool IsValid=false;
            try
            {
               
                context.sp_InsertForgetPasswordDetail(ob.UserName, ob.VerifiedEmail, ob.Token, true);
                IsValid = true;
            }
            catch (Exception)
            {
                
                throw;
            }
            return IsValid;
        }

        public bool UpdatePassword(string token, string pass)
        {
            bool IsValid = false;
            try
            {
                context.sp_ForgetPasswordChange(token, pass);
                IsValid = true;
            }
            catch
            {
                throw;
            }
            return IsValid;
        }

        public bool UserConfirmationForgrtPassword(string token)
        {
            bool IsVAlid = false;
           
            try
            {
                Guid token1 = new Guid(token);
                var data = context.tblForgetPasswords.Where(m => m.Token == token1).ToList();
               if (data!=null)
               {
                   IsVAlid = true;    
               }
            }
            catch (Exception)
            {
                
                throw;
            }
            return IsVAlid;
        }
        public List<ForgetPassword> SecurityQuestion(string dat)
        {
            bool IsTrue = false;
            string UserName = "";
            string Email = "";
            List<ForgetPassword> li = new List<ForgetPassword>();
            List<ForgetPassword> list = new List<ForgetPassword>();
            try
            {
                var data = context.sp_SecurityQuestionDetails().ToList();
                foreach (var item in data)
                {
                    li.Add(new ForgetPassword()
                    {
                        EmailAddress = item.EmailAddress,
                        UserName = item.UserName,
                        UserId = item.BasicUserID,
                        SecurityQuestion1 = item.SecurityQuestion1,
                        SecurityAnswer1 = item.Answer1,
                        SecurityAnswer2 = item.Answer2,
                        SecurityQuestion2 = item.SecurityQuestion2
                    });
                }

                for (int i = 0; i < li.Count; i++)
                {
                    if (dat != null)
                    {

                        if ((li[i].UserName.Contains(dat)))
                        {
                            list.Add(new ForgetPassword()
                            {
                                EmailAddress = li[i].EmailAddress,
                                UserName = li[i].UserName,
                                UserId = li[i].UserId,
                                SecurityQuestion1 = li[i].SecurityQuestion1,
                                SecurityAnswer1 = li[i].SecurityAnswer1,
                                SecurityAnswer2 = li[i].SecurityAnswer2,
                                SecurityQuestion2 = li[i].SecurityQuestion2
                            });
                        }
                        else if ((li[i].EmailAddress.Contains(dat)))
                        {
                            list.Add(new ForgetPassword()
                            {
                                EmailAddress = li[i].EmailAddress,
                                UserId = li[i].UserId,
                                UserName = li[i].UserName,
                                SecurityQuestion1 = li[i].SecurityQuestion1,
                                SecurityAnswer1 = li[i].SecurityAnswer1,
                                SecurityAnswer2 = li[i].SecurityAnswer2,
                                SecurityQuestion2 = li[i].SecurityQuestion2
                                 
                            });


                        }
                    }
                }

                return list;
            }
            catch
            {
                throw;
            }
        }

        public bool RegisterAdmin(RegisterAdmin obvRegisterAdmin)
        {
            try
            {
                Guid userid = Guid.NewGuid();
                var user = new tblUser { UserId = userid, UserName = obvRegisterAdmin.UserName, UserPassword = obvRegisterAdmin.ConfirmPassword, UserStatus = true, AccountTypeID = 5 };
                context.tblUsers.Add(user);
                if (context.SaveChanges() > 0)
                    return true;
                return false;
            }
            catch (Exception)
            {

                return false;
            }
         
        }

        public bool fetch_isdel(Guid userid, int acType)
        {
            try
            {
                var result="False";
                
                switch (acType)
                {
                    case 1:
                        var data = context.tblBasicAccounts.Where(m => (m.BasicUserID == userid)).SingleOrDefault();
                        if(data!=null)
                        {result = data.IsDelete.ToString();}
                        break;
                    case 2:
                        var data1 = context.tblAphidTiseAccounts.Where(m => (m.AphidTiseUserID == userid)).SingleOrDefault();
                        if (data1 != null)
                        { result = data1.IsDelete.ToString(); }
                        break;
                    case 3:
                        var data11 = context.tblByterAccounts.Where(m => (m.ByterUserID == userid)).SingleOrDefault();
                        if (data11 != null)
                        { result = data11.IsDelete.ToString(); }
                        break;
                    case 4:
                        var data111 = context.tblPremiumAccounts.Where(m => (m.PremiumUserID == userid)).SingleOrDefault();
                        if (data111 != null)
                        { result = data111.IsDelete.ToString(); }
                        break;

                }
                return Boolean.Parse(result); 
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool VerifyPremiumAccountCode(string verificationCode)
        {
            try
            {
                
                var matchingCodeRecord = context.tblPremiumCodes.Where(pc => pc.PremiumCode == verificationCode).FirstOrDefault(); 
                if (matchingCodeRecord == null)
                {
                    return false; 
                }
                context.Entry(matchingCodeRecord).State = System.Data.EntityState.Modified;

                matchingCodeRecord.AlreadyRedeemed = true;
                context.SaveChanges(); 
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<AphidAccountType> GetAccounttypes()
        {
            List<AphidAccountType> accounts = new List<AphidAccountType>();            
            context.tblAccountTypes.ToList().ForEach(c=>accounts.Add(c.ToAphidAccountType()));
            return accounts;
        }


        public bool IsUserNameAvailable(string UserName)
        {
            return context.tblUsers.Any(u => u.UserName.ToLower().Trim() == UserName.ToLower().Trim());
        }
        public UserSubscribeModel GetUserInfoByUserName(string userName)
        {
            var objUser = context.tblUsers.FirstOrDefault(x => x.UserName.ToLower().Trim() == userName.ToLower().Trim());
            UserSubscribeModel objModel = null;

            if (objUser != null)
            {
                objModel = new UserSubscribeModel();
                objModel.UserId = objUser.UserId;
                objModel.Email = objUser.EmailAddress;
                objModel.ProfilePic = objUser.PicturePath;
                objModel.UserName = objUser.UserName;
            }

            return objModel;
        }
        public bool IsEmailAlreadyRegistered(string Email)
        {
            return context.tblUsers.Any(u => u.EmailAddress.ToLower().Trim() == Email.ToLower().Trim());
        }

        /// <summary>
        /// Inserts Social network Login
        /// </summary>
        /// <param name="SocialNetworkSource">social network soirce</param>
        /// <param name="UserID"></param>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public int InsertSocialNetworkLogin(string SocialNetworkSource,Guid UserID,string UserName)
        {
            try {
                context.SocialNetworkLogins.Add(new SocialNetworkLogin
                {
                    IsActive=true,
                    SocialNetworSource= SocialNetworkSource,
                    Userid= UserID,
                    Username=UserName
                });
                return context.SaveChanges();
            }catch(Exception ex)
            {
                //log error
                return 0;
                
            }
        }

        /// <summary>
        /// Gets the user Asociated to a social network login
        /// </summary>
        /// <param name="Email">email</param>
        /// <param name="SocialNetworkLogin">social network login</param>
        /// <returns></returns>
        public tblUser GetUserInformationByMailAndSocialNetworkSource(string Email, string SocialNetworkSource)
        {
            try
            {
                tblUser user = new tblUser();

                user = context.tblUsers.Where(e => e.EmailAddress.ToLower().Trim() == Email.ToLower().Trim()).FirstOrDefault();
                if (user != null)
                {
                    if (context.SocialNetworkLogins.Any(u => u.Userid.HasValue && u.Userid.Value == user.UserId &&
                     u.SocialNetworSource.ToLower().Trim() == SocialNetworkSource.ToLower().Trim()))
                    {
                        return user;
                    }
                    else
                        return user;
                }
                return user;

            }
            catch (Exception ex)
            {
                return new tblUser();
            }
        }

    }
}
