using AphidBytes.Accounts.Contracts;
using AphidBytes.Accounts.Contracts.Model;
using AphidTise.Entity;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Processor
{
    public class AphidTise : IAphidTise
    {
        RepositoryAphidTise repository = new RepositoryAphidTise();

        public AphidTiseAccountViewModel GetAphidTiseInfo(Guid userID)
        {
            sp_GetAphidTiseAccountInfo_Result aphidTiseObjectData = repository.GetAphidTiseInfo(userID);
            AphidTiseAccountViewModel aphidTiseData = new AphidTiseAccountViewModel();
            if (aphidTiseObjectData != null)
            {
                aphidTiseData.AddressLine1 = aphidTiseObjectData.AddressLine1;
                aphidTiseData.AddressLine2 = aphidTiseObjectData.AddressLine2;
                aphidTiseData.Answer1 = aphidTiseObjectData.Answer1;
                aphidTiseData.Answer2 = aphidTiseObjectData.Answer2;
                aphidTiseData.City = aphidTiseObjectData.City;
                aphidTiseData.ProfilePictureServerId = aphidTiseObjectData.PictureServerId ?? 0;
                aphidTiseData.ProfilePicturePath = aphidTiseObjectData.PicturePath;
                aphidTiseData.UserName = aphidTiseObjectData.CompanyName;
                aphidTiseData.DOB = aphidTiseObjectData.DOB;
                aphidTiseData.EmailAddress = aphidTiseObjectData.EmailAddress;
                aphidTiseData.FirstName = aphidTiseObjectData.FirstName;
                aphidTiseData.Informations = aphidTiseObjectData.Informations;
                aphidTiseData.LastName = aphidTiseObjectData.LastName;
                aphidTiseData.Phone = aphidTiseObjectData.Phone;
                aphidTiseData.PostalCode = aphidTiseObjectData.PostalCode;
                aphidTiseData.ProductService = aphidTiseObjectData.ProductService;
                aphidTiseData.Region = aphidTiseObjectData.Region;
                aphidTiseData.SecurityQuestion1 = aphidTiseObjectData.SecurityQuestion1;
                aphidTiseData.SecurityQuestion2 = aphidTiseObjectData.SecurityQuestion2;
                aphidTiseData.Website = aphidTiseObjectData.Website;
                aphidTiseData.AccountTypeID = aphidTiseObjectData.AccountTypeID;
                aphidTiseData.AddressID = aphidTiseObjectData.AddressID;
                aphidTiseData.SecurityQuestionID = aphidTiseObjectData.SecurityQuestionID;
            }
            return aphidTiseData;
        }
        public bool UpdateAphidTiseAccountInfo(AphidTiseAccountViewModel aphidtisemodel)
        {
            return repository.UpdateAphidTiseAccountInfo(aphidtisemodel);
        }

        public bool InsertAphidAds(AphidTiseGenerateAds AphidAds)
        {
            return repository.InsertAphidAds(AphidAds);
        }

        public int GetAdId(string name)
        {
            return repository.GetAdId(name);
        }
        public List<BindDropDown> BindDrop()
        {
            return repository.BindDropDown1();
        }

        public List<DateTime> fetch_booked_dates(string ad_type_no)
        {
            return repository.fetch_booked_dates(ad_type_no);
        }

        public AdvertisementModel fetch_record(string TrackNo) 
        {
            return repository.fetch_record(TrackNo);
        }

        public bool modify_searchbaradd(AdvertisementModel obj)
        {
            return repository.modify_searchbaradd(obj);
        }

        public bool insideSearch(AdvertisementModel obj)
        {
            return repository.insideSearch(obj);
        }

        public bool entermatrixadd(AdvertisementModel obj)
        {
            return repository.entermatrixadd(obj);
        }

        public bool beforeplayadd(AdvertisementModel obj)
        {
            return repository.beforeplayadd(obj);
        }
        public List<AdvertisementModel> GetPostedDataResult(Guid userid)
        {
            return repository.GetPostedDataResult(userid);
        }
        public List<AdvertisementModel> SearchAdv(Guid user, string Text)
        {
            return repository.SearchAdv(user, Text);
        }
        public List<AdvertisementModel> SortingOrd(Guid user, string Sort)
        {
            return repository.SortingOrd(user, Sort);
        }


    }
}
