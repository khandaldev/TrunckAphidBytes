using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AphidTise.Entity.Repository
{
    public class RepositorySocial : GenericRepository<tblSocialNetwork>
    {

        public string getdata(SocialNetworkModel model)
        {
            int output;
            try
            {
                var category = context.tblCategories.Where(m => m.CategoryName == model.category).SingleOrDefault();
                var data = context.tblSocialNetworks.Where(m => m.Aphid == model.Aphid_id && m.Category == category.CategoryID).SingleOrDefault();
                if (data == null)
                {
                    if ((category.CategoryName == "Scribd") || (category.CategoryName == "YouTube"))
                    {
                        output = context.sp_InsUpdDelSocialNetwork_tbl(model.ID, model.category, model.UserName, DateTime.Now, model.Aphid_id, "INS", model.Password, model.IsDeleted);
                    }
                    else
                    {
                        output = context.sp_InsUpdDelSocialNetwork_tbl(model.ID, model.category, model.Access_Token, model.Expires, model.Aphid_id, "INS", model.RefereshToken, true);
                    }
                    return "inserted";
                }

                else
                {
                    if ((category.CategoryName == "Scribd") || (category.CategoryName == "YouTube"))
                    {
                        data.AccessToken = model.UserName;
                        data.Refreshtoken = model.Password;
                    }
                    if ((model.RefereshToken != null) || (model.RefereshToken != null))
                    {
                        data.Refreshtoken = model.RefereshToken;
                        data.AccessToken = model.Access_Token;
                    }
                    else
                    {
                        data.AccessToken = model.Access_Token;
                        data.Refreshtoken = model.Access_Token;
                    }
                    data.Expires = model.Expires;
                    data.IsDeleted = true;
                    context.SaveChanges();
                    return "updated";
                }
            }
            catch { throw; }
        }


        public BasicAccountViewModel GetUserInfo(string username)
        {
            BasicAccountViewModel model=new BasicAccountViewModel();
            var data=context.tblUserActivations.Where(m=>m.UserName==username).Select(m=>m.UserId).FirstOrDefault();
            if (data != null)
            {
                model.BasicUserID = new Guid(data.ToString());
 
            }
           
            return model;
        }


        public List<SocialNetworkModel> reterive(Guid Aphid)
        {
            List<SocialNetworkModel> li = new List<SocialNetworkModel>();
            try
            {
                var data = context.tblSocialNetworks.Where(m => m.Aphid == Aphid && m.IsDeleted == true).ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var value = context.tblCategories.Where(m => m.CategoryID == item.Category).Single();
                        li.Add(new SocialNetworkModel()
                        {
                            category = value.CategoryName,
                            Expires = item.Expires.Value,
                            Access_Token = item.AccessToken,
                            RefereshToken = item.Refreshtoken
                        });

                    }
                }
                return li;
            }
            catch { throw; }
        }
        public void Delete(SocialNetworkModel ob)
        {
            try
            {
                context.sp_InsUpdDelSocialNetwork_tbl(null, ob.category, null, null, ob.Aphid_id, "DEL", null, ob.IsDeleted);
            }
            catch { throw; }
        }
        public int update(Guid Aphid, string category, DateTime Expire, string refreshtoken, string accesstoken = null)
        {
            try
            {
                if (accesstoken == null)
                    return context.sp_InsUpdDelSocialNetwork_tbl(null, category, null, Expire, Aphid, "UPD", refreshtoken, null);
                else
                    return context.sp_InsUpdDelSocialNetwork_tbl(null, category, accesstoken, Expire, Aphid, "UPD", refreshtoken, null);
            }
            catch { throw; }
        }

        public string Ret_accesstoken(Guid id, string catg)
        {
            try
            {
                var category = context.tblCategories.Where(m => m.CategoryName == catg).SingleOrDefault();
                var data = context.tblSocialNetworks.Where(m => m.Aphid == id && m.Category == category.CategoryID).SingleOrDefault();

                if (data != null)
                {
                    if (((catg == "Twitter") || (catg == "Flicker")) || (catg == "Scribd") || (catg == "YouTube"))
                    {
                        return data.AccessToken + ',' + data.Refreshtoken;
                    }
                    return data.AccessToken;
                }
                else
                    return "Invalid";
            }
            catch { throw; }
        }
        public bool Credit_Insert(Guid id, string category, string channel, string size, string path, string title, string track, bool active)
        {
            try
            {
                var value = context.tblCreditDetails.Where(m => ((m.Aphid == id) && (m.Path == path) && (m.Category == category) && (m.Channel == channel) && (m.IsActive == active))).SingleOrDefault();
                if (size == "")
                {
                    if (value == null)
                    {
                        return false;
                    }
                    return true;
                }
                else
                {
                    context.sp_CreditDetail(id, channel, category, size, path, title, DateTime.Now, DateTime.Now, track, active);
                    return false;
                }
            }
            catch { throw; }
        }

        public string Fetch_AccountType(Guid user)
        {
           
            var data = (from pd in context.tblUsers
                        join od in context.tblMasterAccountTypes on pd.AccountTypeID equals od.AccountTypeID
                        where pd.UserId == user
                        select new
                        {
                            od.AccountTypeName
                        }).SingleOrDefault();

            return data.AccountTypeName;
            
        }




        public bool InsertUrlLinks(UrlLinkModel model)
        {
           
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (model.Link1!=null)
                    {
                        context.sp_InsertUrlLinks(model.UserID, model.Link1,model.CreatedDate,model.Status,model.ModifiedDate);
                    }
                    if (model.Link2 != null)
                    {
                        context.sp_InsertUrlLinks(model.UserID, model.Link2, model.CreatedDate, model.Status, model.ModifiedDate);
                    }
                    if (model.Link3 != null)
                    {
                        context.sp_InsertUrlLinks(model.UserID, model.Link3, model.CreatedDate, model.Status, model.ModifiedDate);
                    }
                    if (model.Link4 != null)
                    {
                        context.sp_InsertUrlLinks(model.UserID, model.Link4, model.CreatedDate, model.Status, model.ModifiedDate);
                    }
                    if (model.Link5 != null)
                    {
                        context.sp_InsertUrlLinks(model.UserID, model.Link5, model.CreatedDate, model.Status, model.ModifiedDate);
                    }
                    scope.Complete();
                    return true;
                }
               
            }
            catch { return false;}
        }

        public string Modifysocialdata(Guid id, string value)
        {
            String[] str = value.Split(',');
            string value1 = "";
            try
            {
                for (int i = 0; i < str.Length; i++)
                {
                    value1 = str[i];
                    var data = context.tblCategories.Where(m => m.CategoryName == value1).SingleOrDefault();
                    var val = context.tbl_SocialNetworkStatus.Where(m => (m.Aphid == id && m.CategoryID == data.CategoryID)).SingleOrDefault();
                    if (val == null)
                    {
                        var userdata = new tbl_SocialNetworkStatus { Status = true, Aphid = id, CreatedDate = DateTime.Now, ModifiedDate = DateTime.Now, CategoryID = data.CategoryID };
                        context.tbl_SocialNetworkStatus.Add(userdata);
                        context.SaveChanges();
                        //var val1 = context.tblSocialNetworks.Where(m => (m.Aphid == id && m.Category == data.CategoryID)).SingleOrDefault();
                        //if (val1!= null)
                        //{
                        //    val1.IsDeleted= false;
                        //    context.SaveChanges();
                        //}
                    }
                }
                return "Succeess";
            }
            catch { throw; }
        }
        public string FindChannel(Guid id, string value)
        {
            try
            {
                var data = context.tblCategories.Where(m => m.CategoryName == value).SingleOrDefault();
                var val = context.tbl_SocialNetworkStatus.Where(m => (m.Aphid == id && m.CategoryID == data.CategoryID)).SingleOrDefault();
                if (val != null)
                {
                    val.Status = false;
                    val.ModifiedDate = DateTime.Now;
                    // context.tbl_SocialNetworkStatus.Add(userdata);
                    context.SaveChanges();
                    return "true";
                }
                return "false";
            }
            catch { throw; }
        }
        public List<SocialNetworkModel> Addchannel(Guid id)
        {
            List<SocialNetworkModel> list = new List<SocialNetworkModel>();
            try
            {
                var data = context.sp_AddChannelSite(id).ToList();
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {

                        if (item.status.ToString() == "")
                        {
                            item.status = false;

                        }
                        list.Add(new SocialNetworkModel()
                        {
                            categorytype = item.Channel,
                            category = item.CategoryName,
                            CurrentStatusSocial = item.status.ToString().ToLower()

                        });
                    }
                }
                else
                {
                    var data1 = context.tblCategories.ToList();
                    if (data.Count > 0)
                    {
                        foreach (var item in data1)
                        {
                            list.Add(new SocialNetworkModel()
                            {
                                category = item.CategoryName,
                                categorytype = item.Channel,
                                CurrentStatusSocial = "true"
                            });
                        }
                    }
                }
                return list;
            }
            catch { throw; }
        }
    }
}

