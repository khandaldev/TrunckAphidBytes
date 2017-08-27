using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface ISocialNetwork
    {
        string GetData(SocialNetworkModel ob);
        void Deletedata(SocialNetworkModel ob);
        string Reterivetoken(Guid id,string category);
        bool Credit_Insert(Guid id, string channel, string category, string size, string path, string title,string track,bool active);
        
        string Modifysocialdata(Guid id,string value);
        string FindChannel(Guid id, string value);
        List<SocialNetworkModel> AddChannel(Guid id);
        bool InsertUrlLinks(UrlLinkModel model);
        string Fetch_AccountType(Guid user);
        BasicAccountViewModel GetUserInfo(string username);
    }
}
