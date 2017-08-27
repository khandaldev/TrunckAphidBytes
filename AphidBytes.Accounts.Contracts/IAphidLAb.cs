using AphidBytes.Accounts.Contracts.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AphidBytes.Accounts.Contracts
{
    public interface IAphidLAb
    {

        DataPlanDetail DataPlanDetailMethod(Guid userID);

        AphidLabAccountModel GetAphidLabAccountInfo(Guid userID);

        bool UpdateAphidLabAccountInfo(AphidLabAccountModel Aphid);

        bool InsertAphidLabVideo(AphidLabsUpload AphidLabVideoModel, InterruptedFileModel intModel, CreateLinkPostModel post);

        bool InsertAphidLabSoftware(AphidLabsUpload AphidlabSoftwaremodel, InterruptedFileModel intModel, CreateLinkPostModel post);

        List<AphidLabsUpload> fileprivew(string trackno);

        bool VerifyEmailAccount(Guid usid);
    }
}
