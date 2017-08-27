using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface IAccounts
    {
        string TestMethod(string name);
        bool RegisterAphidTiseAccount(AphidTiseAccountViewModel account);
        bool RegisterBasicAccount(BasicAccountViewModel account);
        bool RegisterByterAccount(ByterAccountViewModel account);
        bool VerifyPremiumAccountCode(string verificationCode); 

        string AccountInfo(string user,int i);      
       
        bool RegisterPremiumAccount(PremiumAccountViewModel account);
        LoginProfileDto LoginWithUser(LoginUser Login);
        LoginProfileDto LoginWithToken(string tokenId);
        LoginProfileDto LoginWithSocialSite(string userId);

        bool activationInsert(Guid userid, Guid token, string uname);
        string activateUser(string token);
        bool ChangePassword(string pass, string userid);
         ForgetPassword ForgetPasword(ForgetPassword model);
        List<SocialNetworkModel> SocialStatus(Guid Aphid);
        int UpdateExpires(Guid Aphid, string category, DateTime Expire, string refreshtoken,string accesstoken=null);
        bool InsertForgetPasswordDetail(ForgetPassword ob);
        bool UserConfirmationForgrtPassword(string token);
        bool UpdatePassword(string token,string pass);
        List<ForgetPassword> Secure_Getdata(string data);
        bool RegisterAdmin(RegisterAdmin obvRegisterAdmin);
        bool fetch_isdel(Guid userid,int acType);

        //myAdminModel fet_data_chat(string usern, string userid);
        bool InsertUserSubscribe(UserSubscribeModel account);
        UserSubscribeModel GetUserSubscribe(Guid? subscribeID, Guid? userID);
        bool RegisterAphidlabAccount(AphidLabAccountModel AphidLAbmodel);
    }
}
