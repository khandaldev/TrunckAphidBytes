using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.IO;
using System.Text;

namespace AphidTise.Entity.Repository
{
    public class RepositoryAphidTise : GenericRepository<tblAphidTiseAccount>
    {
        public sp_GetAphidTiseAccountInfo_Result GetAphidTiseInfo(Guid userID)
        {
            try
            {
                return context.sp_GetAphidTiseAccountInfo(userID).SingleOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public bool UpdateAphidTiseAccountInfo(AphidTiseAccountViewModel aphidtisemodel)
        {
            try
            {
                using (TransactionScope tranScope = new TransactionScope())
                {

                    //Update data in Bank Account Details
                    if (aphidtisemodel.BankAccountID.HasValue)
                    {
                        context.sp_UpdateBankAccountDetails(aphidtisemodel.BankAccountID, aphidtisemodel.CardNumber, aphidtisemodel.ExpiryMonth, aphidtisemodel.ExpiryYear, aphidtisemodel.CSV, aphidtisemodel.NameOnCard);
                    }
                    //Update data in Personal Address
                    if (aphidtisemodel.AddressID.HasValue)
                    {
                        context.sp_UpdatePersonAddress(aphidtisemodel.AddressID, aphidtisemodel.AddressLine1, aphidtisemodel.AddressLine2, aphidtisemodel.City, aphidtisemodel.Region, aphidtisemodel.PostalCode);
                    }
                    //Update data in Security Queastion Answer
                    if (aphidtisemodel.SecurityQuestionID.HasValue)
                    {
                        context.sp_UpdateSecurityQuestions(aphidtisemodel.SecurityQuestionID, aphidtisemodel.SecurityQuestion1, aphidtisemodel.Answer1, aphidtisemodel.SecurityQuestion2, aphidtisemodel.Answer2);
                    }
                    context.sp_UpdateUsers(aphidtisemodel.ProfilePicturePath, aphidtisemodel.ProfilePictureServerId, aphidtisemodel.AphidTiseUserID);

                    //Update data in AphidTise Table
                    context.sp_UpdateAphidTiseAccount(aphidtisemodel.UserName, aphidtisemodel.FirstName, aphidtisemodel.LastName, aphidtisemodel.EmailAddress, aphidtisemodel.DOB, aphidtisemodel.Phone, aphidtisemodel.Informations, aphidtisemodel.Website, aphidtisemodel.ProductService, aphidtisemodel.AphidTiseUserID);

                    tranScope.Complete();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        public bool InsertAphidAds(AphidTiseGenerateAds AphidAds)
        {
            try
            {
                using (TransactionScope transScope = new TransactionScope())
                {
                    Guid AphidAdID = Guid.NewGuid();
                    Guid ServeyID = Guid.NewGuid();
                    if (AphidAds.Question != null)
                    {
                        context.sp_InsertSurveyQuestion(ServeyID, AphidAds.Question, AphidAds.Option1, AphidAds.Option2, AphidAds.Option3, AphidAds.Option4, AphidAds.Option5, AphidAds.Option6, AphidAds.Option7, AphidAds.Option8);
                    }

                    //if (AphidAds.AdCycleFromDate <= DateTime.Now  && AphidAds.AdCycleToDate<=DateTime.Now)
                    //{
                    //    AphidAds.IsActive = true;
                    //    context.sp_InsertAds(AphidAdID, AphidAds.UserID, AphidAds.CompanyLogoByte, AphidAds.Title, AphidAds.AdInformation, AphidAds.AdCycleFromDate, AphidAds.AdCycleToDate, AphidAds.AdTypeID, AphidAds.AdPictureByte, AphidAds.AdVideoByte, AphidAds.AdHyperLinkUrl, AphidAds.PriceToDisplay, AphidAds.CreditsID, ServeyID, AphidAds.CreateDate, AphidAds.ModifyDate, AphidAds.IsDelete, AphidAds.IsActive, AphidAds.TrackingNumber);
                    //}
                    AphidAds.IsActive = false;
                    context.sp_InsertAds(AphidAdID, AphidAds.UserID, AphidAds.CompanyLogoByte, AphidAds.Title, AphidAds.AdInformation, AphidAds.AdCycleFromDate, AphidAds.AdCycleToDate, AphidAds.AdTypeID, AphidAds.AdPictureByte, AphidAds.AdVideoByte, AphidAds.AdHyperLinkUrl, AphidAds.PriceToDisplay, AphidAds.CreditsID, ServeyID, AphidAds.CreateDate, AphidAds.ModifyDate, AphidAds.IsDelete, AphidAds.IsActive, AphidAds.TrackingNumber);



                    transScope.Complete();
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }

        public int GetAdId(string name)
        {
            try
            {
                int id = 0;
                if (name != null)
                {
                    tblMasterAdsType ad = context.tblMasterAdsTypes.Where(m => m.AdTypeName == name).SingleOrDefault();
                    id = ad.AdTypeID;
                }
                return id;
            }
            catch
            {
                throw;
            }
        }

        public List<BindDropDown> BindDropDown1()
        {
            //BindDropDown bd = new BindDropDown();
            List<BindDropDown> ad = new List<BindDropDown>();
            try
            {
                var Da = context.tblMasterCredits.ToList();
                foreach (var item in Da)
                {
                    ad.Add(new BindDropDown()
                    {
                        id = item.CreditsID,
                        Value = item.CreditsPoint

                    });
                }
                return ad;
            }
            catch
            {
                throw;
            }
        }

        public List<DateTime> fetch_booked_dates(string ad_type_no)
        {
            
            //BindDropDown bd = new BindDropDown();
            try
            {
                List<DateTime> dates_book = new List<DateTime>();
                var ad_type = context.tblMasterAdsTypes.Where(m => m.AdTypeName == ad_type_no).SingleOrDefault();
                if (ad_type!=null)
                {
                      var Da = context.tblAds.Where(m => (m.AdTypeID == ad_type.AdTypeID)).Select(m => new { m.AdCycleFromDate, m.AdCycleToDate }).ToList();
                     if (Da != null)
                     {
                         foreach (var item in Da)
                         {
                             DateTime from = DateTime.Parse(item.AdCycleFromDate.ToString());
                             DateTime to = DateTime.Parse(item.AdCycleToDate.ToString());
                             while (from < to)
                             {
                                 //if(dates_book.)
                                 dates_book.Add(from.Date);
                                 from = from.AddDays(1);
                             }
                         }

                     }
                }
             
                
                return dates_book;
            }
            catch
            {
                throw;
            }
        }

        public AdvertisementModel fetch_record(string TrackNo)
        {
            AdvertisementModel rec = new AdvertisementModel();
            try
            {
                var data = context.tblAds.Where(m=>(m.TrackingNo==TrackNo)).SingleOrDefault();
                if (data != null)
                {
                    rec.AdVideo = data.AdVideo;
                    rec.CreditsID = data.CreditsID.Value;
                    rec.AdPicture = data.AdPicture;
                    rec.PriceToDisplay = data.PriceToDisplay.ToString();
                    rec.TrackingNumber = data.TrackingNo;
                    rec.AdInformation = data.AdInformation;
                    rec.AdHyperLinkUrl = data.AdHyperLinkUrl;
                    rec.Title = data.Title;

                }
            }
            catch
            {
                throw;
            }
            return rec;
        }

        public bool modify_searchbaradd(AdvertisementModel obj)
        {
            bool result = false;
            AphidTiseGenerateAds rec = new AphidTiseGenerateAds();
            try
            {
                var data = context.tblAds.Where(m => (m.TrackingNo == obj.TrackingNumber)).SingleOrDefault();
                if (data != null)
                {
                    data.AdHyperLinkUrl = obj.AdHyperLinkUrl;
                    data.AdInformation = obj.AdInformation;
                    data.Title = obj.Title;
                    data.AdPicture = obj.AdPicture;
                }
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool insideSearch(AdvertisementModel obj)
        {
            bool result = false;
            AphidTiseGenerateAds rec = new AphidTiseGenerateAds();
            try
            {
                var data = context.tblAds.Where(m => (m.TrackingNo == obj.TrackingNumber)).SingleOrDefault();
                if (data != null)
                {
                    data.AdHyperLinkUrl = obj.AdHyperLinkUrl;
                    data.AdInformation = obj.AdInformation;
                    data.Title = obj.Title;
                    data.AdVideo = obj.AdVideo;
                }
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool entermatrixadd(AdvertisementModel obj)
        {
            bool result = false;
            AphidTiseGenerateAds rec = new AphidTiseGenerateAds();
            try
            {
                var data = context.tblAds.Where(m => (m.TrackingNo == obj.TrackingNumber)).SingleOrDefault();
                if (data != null)
                {
                    data.AdHyperLinkUrl = obj.AdHyperLinkUrl;
                    data.AdInformation = obj.AdInformation;
                    data.Title = obj.Title;
                    data.AdVideo = obj.AdVideo;
                }
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }

        public bool beforeplayadd(AdvertisementModel obj)
        {
            bool result = false;
            AphidTiseGenerateAds rec = new AphidTiseGenerateAds();
            try
            {
                var data = context.tblAds.Where(m => (m.TrackingNo == obj.TrackingNumber)).SingleOrDefault();
                if (data != null)
                {
                    data.AdVideo = obj.AdVideo;
                }
                context.SaveChanges();
                result = true;
            }
            catch
            {
                throw;
            }
            return result;
        }
        public List<AdvertisementModel> SearchAdv(Guid user, string Text)
        {
            List<AdvertisementModel> li = new List<AdvertisementModel>();
            try
            {
                var data = (from pd in context.tblMasterAdsTypes
                            join od in context.tblAds on pd.AdTypeID equals od.AdTypeID
                            where od.UserID == user 
                            select new
                            {
                                od.Title,
                                od.TrackingNo,
                                pd.AdTypeName,
                                od.IsActive,
                                od.AdTypeID,
                                od.AdCycleToDate,
                                od.AdCycleFromDate
                            }).ToList();

                if (data != null)
                {


                    foreach (var item in data)
                    {
                        if (item.Title.Contains(Text))
                        {

                            li.Add(new AdvertisementModel()
                            {
                                Title = item.Title,
                                TrackingNumber = item.TrackingNo,
                                Adtypename = item.AdTypeName,
                                IsActive = item.IsActive,
                                AdCycleFromDate = item.AdCycleFromDate,
                                AdCycleToDate = item.AdCycleToDate

                            });
                        }
                    }
                }
                return li;
            }
            catch
            {
                throw;
            }
        }


        public List<AdvertisementModel> SortingOrd(Guid user, string Sort)
        {
            try
            {
                var data = (from pd in context.tblMasterAdsTypes
                            join od in context.tblAds on pd.AdTypeID equals od.AdTypeID
                            where od.UserID == user
                            orderby od.Title
                            select new
                            {
                                od.Title,
                                od.TrackingNo,
                                pd.AdTypeName,
                                od.IsActive,
                                od.AdTypeID,
                                od.AdCycleFromDate,
                                od.AdCycleToDate



                            }).ToList();
                List<AdvertisementModel> li = new List<AdvertisementModel>();


                if (Sort == "AtoZ")
                {

                    data = (from pd in context.tblMasterAdsTypes
                            join od in context.tblAds on pd.AdTypeID equals od.AdTypeID
                            where od.UserID == user
                            orderby od.Title
                            select new
                            {
                                od.Title,
                                od.TrackingNo,
                                pd.AdTypeName,
                                od.IsActive,
                                od.AdTypeID,
                                od.AdCycleFromDate,
                                od.AdCycleToDate



                            }).ToList();

                }
                else
                {
                    data = (from pd in context.tblMasterAdsTypes
                            join od in context.tblAds on pd.AdTypeID equals od.AdTypeID
                            where od.UserID == user
                            orderby od.Title descending
                            select new
                            {
                                od.Title,
                                od.TrackingNo,
                                pd.AdTypeName,
                                od.IsActive,
                                od.AdTypeID,
                                od.AdCycleFromDate,
                                od.AdCycleToDate



                            }).ToList();

                }
                if (data != null)
                {
                    foreach (var item in data)
                    {

                        li.Add(new AdvertisementModel()
                        {
                            Title = item.Title,
                            TrackingNumber = item.TrackingNo,
                            Adtypename = item.AdTypeName,
                            IsActive = item.IsActive,
                            AdCycleToDate = item.AdCycleToDate,
                            AdCycleFromDate = item.AdCycleFromDate

                        });
                    }

                }


                return li;

            }
            catch
            {
                throw;
            }
        }

        public List<AdvertisementModel> GetPostedDataResult(Guid userid)
        {
            List<AdvertisementModel> li = new List<AdvertisementModel>();
            //var Data = context.tblAds.Where(m => m.UserID == userid).ToList();
            try
            {
                var data = (from pd in context.tblMasterAdsTypes
                            join od in context.tblAds on pd.AdTypeID equals od.AdTypeID
                            where od.UserID == userid
                            select new
                            {
                                od.Title,
                                od.TrackingNo,
                                pd.AdTypeName,
                                od.IsActive,
                                od.AdTypeID,
                                od.AdCycleToDate,
                                od.AdCycleFromDate

                            }).ToList();

                if (data != null)
                {
                    foreach (var item in data)
                    {

                        li.Add(new AdvertisementModel()
                        {
                            Title = item.Title,
                            TrackingNumber = item.TrackingNo,
                            Adtypename = item.AdTypeName,
                            IsActive = item.IsActive,
                            AdCycleToDate = item.AdCycleToDate,
                            AdCycleFromDate = item.AdCycleFromDate

                        });
                    }


                }


                return li;
            }
            catch
            {
                throw;
            }
        }
       
    }
}
