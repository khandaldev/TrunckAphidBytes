using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidTise.Entity;

namespace AphidBytes.Accounts.Processor
{
    public class Accounts : IAccounts
    {
        RepositoryAccounts repository = new RepositoryAccounts();
        RepositorySocial social = new RepositorySocial();
        public List<ForgetPassword> Secure_Getdata(string data)
        {
            return repository.SecurityQuestion(data);
        }
        public string TestMethod(string name)
        {
            return name + " is called ";
        }

        public bool activationInsert(Guid userid, Guid token, string uname)
        {
            return repository.InserActivation(userid, token, uname);
        }

        public bool RegisterAphidTiseAccount(AphidTiseAccountViewModel account)
        {
            return repository.RegisterAphidTiseAccount(account);
        }
        public bool RegisterBasicAccount(BasicAccountViewModel account)
        {
            return repository.RegisterBasicAccount(account);
        }
        public bool RegisterByterAccount(ByterAccountViewModel account)
        {
            return repository.RegisterByterAccount(account);
        }
        public bool RegisterPremiumAccount(PremiumAccountViewModel account)
        {
            return repository.RegisterPremiumAccount(account);
        }
        public bool RegisterAphidlabAccount(AphidLabAccountModel account)
        {
            return repository.RegisterAphidlabAccount(account);
        }
        public string AccountInfo(string user,int i)
        {
            return repository.AccountInfo(user,i);
        }
        public bool InsertUserSubscribe(UserSubscribeModel account)
        {
            return repository.InsertUserSubscribe(account);
        }
        //getUserSubscribe
        public UserSubscribeModel GetUserSubscribe(Guid? subscribeID, Guid? userID)
        {
            sp_gettblUserSubscribe_Result objResult = new sp_gettblUserSubscribe_Result();
            UserSubscribeModel objModel = new UserSubscribeModel();

            objResult = repository.getUserSubscribe(subscribeID, userID);
            if (objResult != null)
            {
                objModel.SubscribeUserId = objResult.SubscribeUserId.Value;
                objModel.UserId = objResult.UserId.Value;
            }
            return objModel;
        }
        public LoginProfileDto LoginWithUser(LoginUser user)
        {
            LoginUser loginUser = new LoginUser();
            user.UserName = user.UserName.ToLower();
            return repository.LoginWithUser(user);
        }

        public LoginProfileDto LoginWithToken(string tokenId)
        {
            return repository.LoginWithToken(tokenId);
        }

        public LoginProfileDto LoginWithSocialSite(string userId)
        {
            return repository.LoginWithSocialSite(userId);
        }

        public string activateUser(string token)
        {
            return repository.activateUser(token);
        }

        public bool ChangePassword(string pass,string userid)
        {
            return repository.changePassword(pass, userid);
        }
        public ForgetPassword ForgetPasword(ForgetPassword model)
        {
            return repository.ForgetPasword(model);
        }
        public int UpdateExpires(Guid Aphid, string category, DateTime Expire, string refreshtoken, string accesstoken = null)
        {
            return social.update(Aphid, category, Expire, refreshtoken,accesstoken);
        }

        public List<SocialNetworkModel> SocialStatus(Guid Aphid)
        {
            return social.reterive(Aphid);
        }
        public bool InsertForgetPasswordDetail(ForgetPassword ob)
        {
            return repository.InsertForgetPasswordDetail(ob);
        }
        public bool UserConfirmationForgrtPassword(string token)
        {
            return repository.UserConfirmationForgrtPassword(token);
           
        }

        public bool UpdatePassword(string token, string pass)
        {
            return repository.UpdatePassword(token, pass);
        }

        public bool RegisterAdmin(RegisterAdmin obvRegister)
        {
            return repository.RegisterAdmin(obvRegister);
        }

        public bool fetch_isdel(Guid userid,int acType)
        {
            return repository.fetch_isdel(userid, acType);
        }

        public bool VerifyPremiumAccountCode(string verificationCode)
        {
            return repository.VerifyPremiumAccountCode(verificationCode);
        }

        public List<AphidAccountType> GetAphidAccountTypes()
        {
            return repository.GetAccounttypes();
        }
    }
}
