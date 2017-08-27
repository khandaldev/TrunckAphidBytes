using AphidBytes.Accounts.Contracts;
using AphidTise.Entity.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Accounts.Contracts.Model;

namespace AphidBytes.Accounts.Processor
{
   public class UserSponsored : IUserSponsored
    {
      UserSponsoredRepository repository = new UserSponsoredRepository();
      public bool InsertPhotoArt(SponsoredModel model,AllGenerateCloneModel allmodel)
      {
          return repository.InsertPhotoArt(model,allmodel);
      }
      public bool InsertSingleMusic(SponsoredModel model,AllGenerateCloneModel allmodel)
      {
          return repository.InsertSingleMusic(model,allmodel);
      }
      public bool InsertByteYourFile(SponsoredModel model,AllGenerateCloneModel allmodel)
      {
          return repository.InsertByteYourFile(model,allmodel);
      }
      public bool InsertByteyourVideo(SponsoredModel model,AllGenerateCloneModel allmodel)
      {
          return repository.InsertByteyourVideo(model,allmodel);
      }
      public bool InsertByteYourEbook(SponsoredModel model,AllGenerateCloneModel allmodel)
      {
          return repository.InsertByteYourEbook(model,allmodel);
      }
      public bool InsertAlbum(SponsoredModel model,AllGenerateCloneModel allmodel)
      {
          return repository.InsertAlbum(model,allmodel);
      }
       
    }
   
}
