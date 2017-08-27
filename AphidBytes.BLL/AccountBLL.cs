using AphidBytes.Accounts.Contracts.Model;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.BLL
{
    public class AccountBLL
    {
        #region Properties
        RepositoryAccounts repository = new RepositoryAccounts();
        #endregion
        #region Public Methods
        public List<AphidAccountType> GetAccountTypes()
        {
            return repository.GetAccounttypes();
        }

        /// <summary>
        /// Check if the user name is available
        /// </summary>
        /// <param name="UserName">username</param>
        /// <returns></returns>
        public bool IsUsernameAvailable(string UserName)
        {
            bool returnvalue = false;
            returnvalue = repository.IsUserNameAvailable(UserName);
            return returnvalue;
        }

        public UserSubscribeModel GetUserInfoByUserName(string userName)
        {
            return repository.GetUserInfoByUserName(userName);
        }
        /// <summary>
        /// check if the email is already registered
        /// </summary>
        /// <param name="Email">email</param>
        /// <returns></returns>
        public bool IsEmailAlreadyRegistered(string Email)
        {
            bool returnvalue = false;
            returnvalue = repository.IsEmailAlreadyRegistered(Email);
            return returnvalue;
        }

        /// <summary>
        /// Stores Social Network Login
        /// </summary>
        /// <param name="SocialNetworkSource">Social Netork Source</param>
        /// <param name="UserID">UserID</param>
        /// <param name="UserName">UserName</param>
        /// <returns></returns>
        public int SaveSocialNetworkLogin(string SocialNetworkSource,Guid UserID,string UserName)
        {
            try {
                return repository.InsertSocialNetworkLogin(SocialNetworkSource, UserID, UserName);
            }catch(Exception ex)
            {
                //Log Error
                return 0;
            }
        }

        /// <summary>
        /// Gets the accountType based on the socialnetworklogin
        /// </summary>
        /// <param name="Email">email</param>
        /// <param name="SocialNetworkSource">social network Login</param>
        /// <returns></returns>
        public int GetTypeofAccountBySocialNetworkLogin(string Email,string SocialNetworkSource)
        {
            try
            {
                
                var user=repository.GetUserInformationByMailAndSocialNetworkSource(Email, SocialNetworkSource);
                if (user != null && user.UserId != null && user.AccountTypeID.HasValue)
                {
                    return user.AccountTypeID.Value;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception ex)
            {
                return 0;
                //log error
            }
        }


        /// <summary>
        /// Gets the accountType based on the socialnetworklogin
        /// </summary>
        /// <param name="Email">email</param>
        /// <param name="SocialNetworkSource">social network Login</param>
        /// <returns></returns>
        public Guid GetUserIdBySocialNetworkLogin(string Email, string SocialNetworkSource)
        {
            try
            {

                var user = repository.GetUserInformationByMailAndSocialNetworkSource(Email, SocialNetworkSource);
                if (user != null && user.UserId != null && user.AccountTypeID.HasValue)
                {
                    return user.UserId;
                }
                else
                {
                    return new Guid();
                }
            }
            catch (Exception ex)
            {
                return new Guid();
                //log error
            }
        }
        #endregion
    }
}
