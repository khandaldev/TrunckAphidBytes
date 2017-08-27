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
    public class Common : ICommon
    {
        RepositoryCommon repository = new RepositoryCommon();
        /// <summary>
        /// User Guid Get From Session and Flag=1 is return FileName And Status, Flag=2 is return AudioFile, FileName And Status
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="Flag"></param>
        /// <returns></returns>
        public IEnumerable<AudioFileModel> GetAudioFiles(Guid userid,int Flag)
        {

            List<AudioFileModel> lstAudioFile = new List<AudioFileModel>();
            if (Flag == 1)
            {
                var audioLst = repository.GetAudioFiles(userid , Flag);
                if (audioLst.Count() > 0)
                {
                    for (int i = 0; i < audioLst.Count(); i++)
                    {
                        lstAudioFile.Add(new AudioFileModel()
                        {
                            AudioFile = audioLst[i].AudioInterruptionFileName,
                            AudioFileName = audioLst[i].FileName,
                            IsActive = audioLst[i].IsActive
                        });
                    }
                }
            }
            if (Flag == 2)
            {
                var audioLst = repository.GetAudioFiles(userid, Flag);
                if (audioLst.Count() > 0)
                {
                    for (int i = 0; i < audioLst.Count(); i++)
                    {
                        lstAudioFile.Add(new AudioFileModel()
                        {
                           // AudioFile = audioLst[i].AudioInterruption,
                            AudioFileName = audioLst[i].FileName,
                            IsActive = audioLst[i].IsActive,
                            AudioFile=audioLst[i].AudioInterruptionFileName
                        });
                    }
                }
            }

           
            return lstAudioFile;
        }
        public IEnumerable<ImageFileModel> GetImageFile(Guid userid)
        {
            List<ImageFileModel> li = new List<ImageFileModel>();
            var ImgList = repository.GetWaterMarkFiles(userid);
            if (ImgList.Count() > 0)
            {
                for (int i = 0; i < ImgList.Count(); i++)
                {
                    li.Add(new ImageFileModel()
                    {
                        ImageFile = ImgList[i].ImageInterruption,
                        ImageFileName = ImgList[i].WatermarkImageName,
                        IsActive = ImgList[i].IsActive
                    });

                }

            }
            return li;
        }

        public bool UpdateDataMemory(Guid UserId, long Length)
        {
           return repository.UpdateDataMemory(UserId, Length);
        }

        public bool CheckSpace(Guid UserId, long Length)
        {
            return repository.CheckSpace(UserId, Length);
        }
        public bool Deleteitem(Guid user, string Track)
        {
            return repository.Deleteitem(user, Track);
        }

        public bool ChangeDataPlan(string plan, Guid userid)
        {
            return repository.ChangeDataPlan(plan, userid);
        }

        public string GetMessageCount(Guid UserId)
        {
            return repository.GetMessageCount(UserId);
        }
        public string GetNewCount(Guid UserId)
        {
            return repository.GetNewCount(UserId);
        }
        public bool CheckEditStatus(Guid id, long length, string oldlength)
        {
            return repository.CheckEditStatus(id,length,oldlength);
        }
        public bool InsertMessageDetails(MessageModel messagemodel)
        {
            return repository.InsertMessageDetails(messagemodel);
        }
        public List<MessageModel> GetMessageData(string emailid)
        {
            return repository.GetMessageData(emailid);
        }
        public string GetReadMessage(int email)
        {
            return repository.GetReadMessage(email);
        }
        public bool MessageDeleteCommon(int MessageID)
        {
            return repository.MessageDeleteCommon(MessageID);
        }
        public List<LinkShareHistory> GetDataForHistory(Guid userid)
        {
            return repository.GetDataForHistory(userid);
        }

        public bool UpdatePassword(Guid userid, string passwordEnc)
        {
            return repository.UpdatePassword(userid, passwordEnc);
        }

        public bool UpdateStripeCard(Guid userid, string stripeToken)
        {
            return repository.UpdateStripeCard(userid, stripeToken);
        }

    }
}
