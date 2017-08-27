using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Accounts.Contracts.Model;

namespace AphidBytes.Accounts.Contracts
{
    public interface IUserSponsored
    {
         bool InsertPhotoArt(SponsoredModel model,AllGenerateCloneModel allmodel);
         bool InsertSingleMusic(SponsoredModel model,AllGenerateCloneModel allmodel);
         bool InsertByteYourFile(SponsoredModel model,AllGenerateCloneModel allmodel);
         bool InsertByteyourVideo(SponsoredModel model,AllGenerateCloneModel allmodel);
         bool InsertByteYourEbook(SponsoredModel model,AllGenerateCloneModel allmodel);
         bool InsertAlbum(SponsoredModel model,AllGenerateCloneModel allmodel);
    }
    
}
