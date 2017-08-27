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
    public class Home:IHome
    {
        RepositoryHome repository = new RepositoryHome();

        public bool ReleaseInsert(AdminModel model)
        {
           return repository.InsertRelease(model);
        }
        public List<AdminModel> GetNewRelease()
        {
            return repository.GetNewRelease();
        }
        public List<AdminModel> GetaDMINRelease()
        {
            return repository.GetaDMINRelease();
        }
        public bool DeleteRecord(string id)
        {
            return repository.deleteRecord(id);
        }

        public bool UpdateRelease(string id, string path, string text)
        {
            return repository.UpdateRelease(path, id, text);
        }
        public AdvertisementModel Fetch_Ad_Video_Data(string ad_type_id)
        {
            return repository.Fetch_Ad_Video_Data(ad_type_id);
        }
        public List<searchmodel> outsearchmethod(string text, string category,string trackingnumber)
        {
            return repository.outsearchmethod(text, category,trackingnumber );
        }

        public AllGenerateCloneModel GetTrack(string trackingnumber)
        {
            return repository.GetTrack(trackingnumber);
        }
        public BasicGenerateCloneModel fileprivew(string trackingNumber)
        {
            return repository.Fileprivew(trackingNumber);
        }

        public ChannelModel UserLogin(ChannelModel obj)
        {
            return repository.UserLogin(obj);
        }
        public string CheckAccountType(Guid UserId)
        {
            return repository.CheckAccountType(UserId);
        }
        public AdvertisementModel survey(Guid suyveyid)
        {
            return repository.survey(suyveyid);
        }
        public bool SubmitFeedback(Guid user, string Feedbacktxt, string TrackId, int Credits, string Path,string val)
        {
            return repository.SubmitFeedback(user, Feedbacktxt,TrackId,Credits, Path,val);
        }
        public string FetchEmail(Guid user)
        {
            return repository.FetchEmail(user);
        }
        public bool AddSongtoFav(string TrackingID, Guid UserID)
        {
            return repository.AddSongtoFav(TrackingID, UserID);
        }
    }
}
