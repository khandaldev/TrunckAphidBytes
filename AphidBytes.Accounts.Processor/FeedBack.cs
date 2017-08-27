using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Processor
{
   public class FeedBack:IFeedBack
    {
       RepositoryFeedBack repository = new RepositoryFeedBack();
       public bool InsertFeedBack(FeedBackModel model)
       {
           return repository.InsertFeedBack(model);
       }
    }
}
