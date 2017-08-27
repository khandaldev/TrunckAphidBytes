using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidTise.Entity.Repository;

namespace AphidBytes.Accounts.Processor
{
   public class SocialNetwork : ISocialNetwork
    {
       RepositorySocial repository = new RepositorySocial();

       public string GetData(SocialNetworkModel ob)
       {
          return repository.getdata(ob);
       }

       public BasicAccountViewModel GetUserInfo(string username)
       {
           return repository.GetUserInfo(username);
       }


       public void Deletedata(SocialNetworkModel ob)
       {
           repository.Delete(ob);
       }
       public string  Reterivetoken(Guid id,string catg)
       {
           return repository.Ret_accesstoken(id,catg);
       }
       public bool Credit_Insert(Guid id, string channel,string category, string size, string path, string title,string track,bool active)
       {
          return repository.Credit_Insert(id, channel, category, size, path, title,track,active);
       }
     
       public string Modifysocialdata(Guid id,string value)
       {
           return repository.Modifysocialdata(id, value);
       }
       public string FindChannel(Guid id, string value)
       {
           return repository.FindChannel(id, value);
       }
       public List<SocialNetworkModel> AddChannel(Guid id)
       {
           return repository.Addchannel(id);
       }
       public bool InsertUrlLinks(UrlLinkModel model)
       {
           return repository.InsertUrlLinks(model);
       }
       public string Fetch_AccountType(Guid user)
       {
           return repository.Fetch_AccountType(user);
       }

   }
}
