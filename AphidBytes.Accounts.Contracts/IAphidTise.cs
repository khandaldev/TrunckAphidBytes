using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface IAphidTise
    {

        AphidTiseAccountViewModel GetAphidTiseInfo(Guid userID);

        bool UpdateAphidTiseAccountInfo(AphidTiseAccountViewModel aphidtisemodel);
        bool InsertAphidAds(AphidTiseGenerateAds aphidtiseAds);
        int GetAdId(string name);
        List<BindDropDown> BindDrop();
        List<DateTime> fetch_booked_dates(string ad_type_no);
        AdvertisementModel fetch_record(string TrackNo);
        bool modify_searchbaradd(AdvertisementModel obj);
        bool insideSearch(AdvertisementModel objet);
        bool entermatrixadd(AdvertisementModel objet);
        bool beforeplayadd(AdvertisementModel objet);
        List<AdvertisementModel> GetPostedDataResult(Guid userID);
        List<AdvertisementModel> SearchAdv(Guid user, string Text);
        List<AdvertisementModel> SortingOrd(Guid user, string Sort);
    }
}
