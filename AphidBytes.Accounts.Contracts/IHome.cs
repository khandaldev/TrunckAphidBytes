using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface IHome
    {
        bool ReleaseInsert(AdminModel model);
        List<AdminModel> GetNewRelease();
        List<AdminModel> GetaDMINRelease();
        bool DeleteRecord(string id);
        bool UpdateRelease(string id, string path, string text);
        AdvertisementModel Fetch_Ad_Video_Data(string ad_type_id);
        List<searchmodel> outsearchmethod(string searchtext, string category,string trackingnumber);
        AllGenerateCloneModel GetTrack(string trackingnumber);
        string CheckAccountType(Guid UserId);
        BasicGenerateCloneModel fileprivew(string trackno);
        ChannelModel UserLogin(ChannelModel mpdel);
        AdvertisementModel survey(Guid suyveyid);
        bool SubmitFeedback(Guid user, string Feedbacktxt, string TrackId, int Credits, string Path,string val);
        string FetchEmail(Guid user);
        bool AddSongtoFav(string TrackingID, Guid UserID);
    }
}
