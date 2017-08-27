using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidTise.Entity.Repository
{
    public class RepositoryFeedBack : GenericRepository<tbl_FeedBack>
    {
        public bool InsertFeedBack(FeedBackModel model)
        {
            try
            {
                var FeedBackData = new tbl_FeedBack { CreatedDate = System.DateTime.Now, Id = 0, Text = model.Text, UserEmail = model.Email,UserID=model.Userid,Subject=model.Subject };
                context.tbl_FeedBack.Add(FeedBackData);
                context.SaveChanges();
                return true; 
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}
