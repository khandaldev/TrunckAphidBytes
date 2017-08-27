using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Accounts.Contracts.Model;
using AphidBytes.Core.PaymentServices;
using static AphidBytes.Core.PaymentServices.StripePackages;
using AphidBytes.Core.Extensions;

namespace AphidTise.Entity.Repository
{
    public class RepositoryCommon : GenericRepository<tblAudioInterruption>
    {
        public const long BytesPerGigabyte = 1073741824;
        public bool UpdateStripeCard(Guid userid, string stripeToken)
        {
            try
            {
                var userToUpdate = context.tblUsers.Where(u => u.UserId == userid).SingleOrDefault(); 
                if (userToUpdate == null)
                {
                    return false; 
                }

                if (string.IsNullOrWhiteSpace(userToUpdate.StripeCustomerID))
                {
                    return false;
                }

                StripeClient.UpdateStripeCustomer(userToUpdate.StripeCustomerID, stripeToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdatePassword(Guid userId, string passwordEnc)
        {
            try
            {
                context.sp_ChangePassword(userId.ToString(), passwordEnc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<tblAudioInterruption> GetAudioFiles(Guid userid, int Flag)
        {
            List<tblAudioInterruption> val = new List<tblAudioInterruption>();
            try
            {
                if (Flag == 1)
                {

                    var data = context.tblAudioInterruptions.Where(m => m.UserId == userid).Select(d => new { d.FileName, d.IsActive, d.AudioInterruptionFileName }).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        val.Add(new tblAudioInterruption() { FileName = data[i].FileName, IsActive = data[i].IsActive, AudioInterruptionFileName = data[i].AudioInterruptionFileName });
                    }
                }
                else if (Flag == 2)
                {
                    //List<tblAudioInterruption> val = new List<tblAudioInterruption>();
                    var data = context.tblAudioInterruptions.Where(m => m.UserId == userid).Select(d => new { d.FileName, d.IsActive, d.AudioInterruptionFileName }).ToList();
                    for (int i = 0; i < data.Count(); i++)
                    {
                        val.Add(new tblAudioInterruption() { FileName = data[i].FileName, IsActive = data[i].IsActive, AudioInterruptionFileName = data[i].AudioInterruptionFileName });
                    }
                }
                return val;
                //  var data = context.tblAudioInterruptions.Where(m => m.UserId == userid).ToList();
                // return context.tblAudioInterruptions.Where(m => m.UserId == userid) as IEnumerable<AphidBytes.Accounts.Contracts.Model.AudioFileModel>;
                //  return data;
            }
            catch { throw; }
        }

        public List<tblWaterMarkUpInterruption> GetWaterMarkFiles(Guid userId)
        {
            // var data = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == userId).ToList();
            List<tblWaterMarkUpInterruption> li = new List<tblWaterMarkUpInterruption>();
            try
            {
                var data = context.tblWaterMarkUpInterruptions.Where(m => m.UserId == userId).Select(d => new { d.WatermarkImageName, d.IsActive, d.ImageInterruption }).ToList();
                for (int i = 0; i < data.Count; i++)
                {
                    li.Add(new tblWaterMarkUpInterruption() { WatermarkImageName = data[i].WatermarkImageName, IsActive = data[i].IsActive, ImageInterruption = data[i].ImageInterruption });
                }
                return li;
            }
            catch { throw; }
        }

        public bool UpdateDataMemory(Guid UserId, long Length)
        {
            bool IsSuccess = false;
            try
            {
                var data = context.tblDataStoragePlans.Where(m => m.UserID == UserId).SingleOrDefault();
                if (data != null)
                {
                    var freespace = Convert.ToInt64(data.FreeSpace);
                    if (freespace < Length)
                    {
                        IsSuccess = false;
                    }
                    else
                    {
                     
                        var cal = freespace - Length;
                        var used = Convert.ToInt64(data.UsedSpace) + Length;
                        data.FreeSpace = cal.ToString();
                        data.UsedSpace = used.ToString();
                        context.SaveChanges();
                        IsSuccess = true;
                    }
                }
                return IsSuccess;
            }
            catch (Exception)
            {

                return IsSuccess;
            }
        }
        public bool Deleteitem(Guid user, string Track)
        {
            bool val = false;
            var data = context.tblCreateLinkPosts.Where(m => m.UserId == user && m.TrackingNo == Track).SingleOrDefault();
            var data1 = context.tbl_MTUserSubscription.Where(m => m.TrackingNumber == Track).SingleOrDefault();
            var Msgdata = context.tbl_SendLinkToMT.Where(m => m.TrackingNumber == Track).SingleOrDefault();
            if (data != null)
            {
                data.IsDelete = true;
                context.SaveChanges();
                val = true;

            }
            if (data1 != null)
            {
                data1.IsPostDelete = true;
                context.SaveChanges();
                val = true;
            }
            if (Msgdata != null)
            {
                Msgdata.IsDelete = true;
                context.SaveChanges();
                val = true;
 
            }
            return val;
        }




        public bool CheckSpace(Guid UserID, long length)
        {
            bool IsSuccess = false;
            try
            {
                var data = context.tblDataStoragePlans.Where(m => m.UserID == UserID).SingleOrDefault();
                if (data != null)
                {
                    var space = Convert.ToInt64(data.FreeSpace);
                    if (space > length)
                    {
                        IsSuccess = true;
                    }
                }
                return IsSuccess;
            }
            catch (Exception)
            {

                return IsSuccess;
            }
        }
        public bool InsertMessageDetails(MessageModel messagemodel)
        {
            bool InsertStatus = false;
            var data = context.sp_Messages(messagemodel.sender_Email, messagemodel.receiver_Email, messagemodel.message_subject, messagemodel.message_body, true, messagemodel.sender_username, messagemodel.receiver_username, System.DateTime.Now, false);
            InsertStatus = true;
             return InsertStatus;
          
        }
        public List<MessageModel> GetMessageData(string emailid)
        {
            var messagedata = context.sp_GetMessages(emailid).ToList();
             List<MessageModel> listmessage=new List<MessageModel>();
            if (messagedata != null)
            {
                foreach (var item in messagedata)
                {
                    listmessage.Add(new MessageModel() { 
                        message_body=item.message_body,
                        sender_username=item.sender_name,
                        message_subject=item.message_subject,
                        id=item.id
                    });
                    
                }
            }
            return listmessage;
        }

        public string GetReadMessage(int id)
        { 
             try
             {
                 var data = context.tbl_Messages.FirstOrDefault(m => m.id == id);
                 if (data != null)
                 {
                     data.Is_Read = true;
                     context.SaveChanges();
                     return "Success";
                 }
                 else
                 {
                     return "Failed";
                 }
             }
             catch { throw; }
        }

        public bool ChangeDataPlan(string plan, Guid userid)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Today;
            try
            {
                var data = context.tblDataStoragePlans.Where(m => m.UserID == userid).SingleOrDefault();
                if (data == null)
                {
                    return false;
                }

                var userToUpdate = context.tblUsers.Where(u => u.UserId == userid).SingleOrDefault();
                if (userToUpdate == null)
                {
                    return false;
                }

                if (string.IsNullOrWhiteSpace(userToUpdate.StripeCustomerID))
                {
                    return false;
                }

                var storagePlan = DataPlans.AvailablePlans().FirstOrDefault(s => s.PlanId == plan) ?? DataPlans.Gb2Free;
                StripeClient.UpdateDataPlanSubscription(userToUpdate.StripeCustomerID, storagePlan?.PlanId);

                data.ExpireDate = dt.AddYears(1);
                data.StoragePlan = storagePlan?.PlanId;
                var oldspace = Convert.ToInt64(data.FreeSpace).Bytes();
                var newspace = Convert.ToInt64(storagePlan.StorageAmount).Gigabytes();
                data.FreeSpace = newspace.Units.ToString();
                context.SaveChanges();
                return true;
            }
            catch { throw; }
        }

        public string GetMessageCount(Guid userId)
        {
            try
            {
                List<int> li = new List<int>();
                int newCount = 0;
                int count = 0;
                var data = context.tbl_Message.Where(m => m.UserID == userId).ToList();
                if (data.Count!= 0)
                {
                    foreach (var item in data)
                    {
                        li.Add(Convert.ToInt32(item.CreditPoint));
                        item.IsNew = 1;
                    }
                    context.SaveChanges();

                    for (int i = 0; i < li.Count; i++)
                    {
                        count = count + li[i];
                    }
                    foreach (var item in data)
                    {
                        if (item.IsNew == 0)
                        {
                            newCount++;
                        }
                    }

                    return count.ToString() + "+" + newCount.ToString();
                }
                else
                    return "0";
            }
            catch (Exception)
            {

                return null;
            }
          
        }

        public string GetNewCount(Guid UserId)
        {
            try
            {
                int cont = 0;
                var data = context.tbl_Message.Where(m => m.UserID == UserId).ToList();
                if (data.Count != 0)
                {
                    foreach (var item in data)
                    {
                        if (item.IsNew==0)
                        {
                            cont++;
                        }
                    }
                    return cont.ToString();
                }
                return "0";
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        public bool CheckEditStatus(Guid id, long length, string oldlength)
        {
            bool IsSuccess = false;
            try
            {
                var data = context.tblDataStoragePlans.Where(m => m.UserID == id).SingleOrDefault();
                if (data != null)
                {
                    long space = Convert.ToInt64(data.FreeSpace);
                    long tttlen = space + (Convert.ToInt64(oldlength));
                    if (length < tttlen)
                    {
                        var cal = tttlen - length;
                        var used = Convert.ToInt64(data.UsedSpace) + length;
                        data.FreeSpace = cal.ToString();
                        data.UsedSpace = used.ToString();
                        context.SaveChanges();
                        IsSuccess = true;

                    }
                }
                return IsSuccess;
            }
            catch (Exception)
            {

                return IsSuccess;
            }
        }
        public bool MessageDeleteCommon(int MessageID)
        {
           try
           {
               var data = context.tbl_Messages.FirstOrDefault(m => m.id == MessageID);
               if(data!=null)
               {
                   data.message_status = false;
                   if (context.SaveChanges() > 0)
                   {
                       return true;
                   }
               }
           }
           catch
           {
               throw;
           }
           return false;
        }
        public List<LinkShareHistory> GetDataForHistory(Guid userid)
        {
            List<LinkShareHistory> listPurchaseHistoryModel = new List<LinkShareHistory>();
            try
            {
              
                //var datarecord = (from p in context.tblCreateLinkPost orderby p.UserId descending select p).Take(10);
                var data = context.tblCreateLinkPosts.Where(m=>m.UserId==userid).Take(5).ToList();
                var datarecords = context.tbl_Messages.Take(5).ToList();

                
               
               
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        listPurchaseHistoryModel.Add(new LinkShareHistory()
                        {
                           Title=item.Title,
                           Category=item.Category,
                           Channel=item.Channel,
                           Track=item.TrackingNo,
                           DateShow=item.PostedDate.ToString()
                        });
                    
                        
                    }
                   
                }
                return listPurchaseHistoryModel;
            }
            catch
            {
                throw;
            }

        }
      
    }
}
