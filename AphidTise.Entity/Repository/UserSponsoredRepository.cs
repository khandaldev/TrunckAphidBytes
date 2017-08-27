using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AphidBytes.Accounts.Contracts.Model;
using System.Transactions;

namespace AphidTise.Entity.Repository
{
    public class UserSponsoredRepository : GenericRepository<tblUsersSponsored>
    {
        public bool InsertPhotoArt(SponsoredModel model,AllGenerateCloneModel allmodel)
        {
            try
            {
                context.sp_InserUserSpon(model.UserID, model.CloneId, model.Title, model.Tag, model.ArtistName, model.AlbumTitle, model.AudioFilePath, model.UploadFilePath, model.MatrixFilePath, model.ComposerName, model.Producer, model.Publisher, model.SelectedInteruptionFile, model.InteruptionStyle, model.AvailableForDownload, model.ExplicitContent, model.UploadImageFilePath, model.UploadPDFFilePath, model.PagePercentage, model.Type, model.PdfFilePath, model.FileNames, model.VideoFilePath, model.WaterMarkMatrixImagePath, model.WaterMarkMatrixImageText, model.VideoCategory, model.RARFilePath, model.MatrixImagePath, model.CreatotName, model.TrackingNumber, model.CatID, model.GenCloneID, System.DateTime.Now, System.DateTime.Now, model.IsActive, model.FileSize);
                context.sp_AllGenerateClones(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath, allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload, allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames, allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath, allmodel.CreatotName, allmodel.TrackingNumber, allmodel.CatID, allmodel.GenCloneID, System.DateTime.Now, System.DateTime.Now, allmodel.IsActive);
                return true;
            }
            catch { throw; }

        }
        public bool InsertSingleMusic(SponsoredModel model,AllGenerateCloneModel allmodel)
        {
            try
            {
                context.sp_InserUserSpon(model.UserID, model.CloneId, model.Title, model.Tag, model.ArtistName, model.AlbumTitle, model.AudioFilePath, model.UploadFilePath, model.MatrixFilePath, model.ComposerName, model.Producer, model.Publisher, model.SelectedInteruptionFile, model.InteruptionStyle, model.AvailableForDownload, model.ExplicitContent, model.UploadImageFilePath, model.UploadPDFFilePath, model.PagePercentage, model.Type, model.PdfFilePath, model.FileNames, model.VideoFilePath, model.WaterMarkMatrixImagePath, model.WaterMarkMatrixImageText, model.VideoCategory, model.RARFilePath, model.MatrixImagePath, model.CreatotName, model.TrackingNumber, model.CatID, model.GenCloneID, System.DateTime.Now, System.DateTime.Now, model.IsActive, model.FileSize);
                context.sp_AllGenerateClones(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath, allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload, allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames, allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath, allmodel.CreatotName, allmodel.TrackingNumber, allmodel.CatID, allmodel.GenCloneID,System.DateTime.Now,System.DateTime.Now, allmodel.IsActive);
                return true;
            }
            catch { throw; }
        }
        public bool InsertByteYourFile(SponsoredModel model,AllGenerateCloneModel allmodel)
        {
            try
            {
                context.sp_InserUserSpon(model.UserID, model.CloneId, model.Title, model.Tag, model.ArtistName, model.AlbumTitle, model.AudioFilePath, model.UploadFilePath, model.MatrixFilePath, model.ComposerName, model.Producer, model.Publisher, model.SelectedInteruptionFile, model.InteruptionStyle, model.AvailableForDownload, model.ExplicitContent, model.UploadImageFilePath, model.UploadPDFFilePath, model.PagePercentage, model.Type, model.PdfFilePath, model.FileNames, model.VideoFilePath, model.WaterMarkMatrixImagePath, model.WaterMarkMatrixImageText, model.VideoCategory, model.RARFilePath, model.MatrixImagePath, model.CreatotName, model.TrackingNumber, model.CatID, model.GenCloneID, System.DateTime.Now, System.DateTime.Now, model.IsActive, model.FileSize);
                context.sp_AllGenerateClones(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath, allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload, allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames, allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath, allmodel.CreatotName, allmodel.TrackingNumber, allmodel.CatID, allmodel.GenCloneID, System.DateTime.Now, System.DateTime.Now, allmodel.IsActive);
                return true;
            }
            catch { throw; }
        }
        public bool InsertByteyourVideo(SponsoredModel model,AllGenerateCloneModel allmodel)
        {
            try
            {
                context.sp_InserUserSpon(model.UserID, model.CloneId, model.Title, model.Tag, model.ArtistName, model.AlbumTitle, model.AudioFilePath, model.UploadFilePath, model.MatrixFilePath, model.ComposerName, model.Producer, model.Publisher, model.SelectedInteruptionFile, model.InteruptionStyle, model.AvailableForDownload, model.ExplicitContent, model.UploadImageFilePath, model.UploadPDFFilePath, model.PagePercentage, model.Type, model.PdfFilePath, model.FileNames, model.VideoFilePath, model.WaterMarkMatrixImagePath, model.WaterMarkMatrixImageText, model.VideoCategory, model.RARFilePath, model.MatrixImagePath, model.CreatotName, model.TrackingNumber, model.CatID, model.GenCloneID, System.DateTime.Now, System.DateTime.Now, model.IsActive, model.FileSize);
                context.sp_AllGenerateClones(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath, allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload, allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames, allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath, allmodel.CreatotName, allmodel.TrackingNumber, allmodel.CatID, allmodel.GenCloneID, System.DateTime.Now, System.DateTime.Now, allmodel.IsActive);
                return true;
            }
            catch { throw; }
 
        }
        public bool InsertByteYourEbook(SponsoredModel model,AllGenerateCloneModel allmodel)
        {
            try
            {
                context.sp_InserUserSpon(model.UserID, model.CloneId, model.Title, model.Tag, model.ArtistName, model.AlbumTitle, model.AudioFilePath, model.UploadFilePath, model.MatrixFilePath, model.ComposerName, model.Producer, model.Publisher, model.SelectedInteruptionFile, model.InteruptionStyle, model.AvailableForDownload, model.ExplicitContent, model.UploadImageFilePath, model.UploadPDFFilePath, model.PagePercentage, model.Type, model.PdfFilePath, model.FileNames, model.VideoFilePath, model.WaterMarkMatrixImagePath, model.WaterMarkMatrixImageText, model.VideoCategory, model.RARFilePath, model.MatrixImagePath, model.CreatotName, model.TrackingNumber, model.CatID, model.GenCloneID, System.DateTime.Now, System.DateTime.Now, model.IsActive, model.FileSize);
                context.sp_AllGenerateClones(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath, allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload, allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames, allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath, allmodel.CreatotName, allmodel.TrackingNumber, allmodel.CatID, allmodel.GenCloneID, System.DateTime.Now, System.DateTime.Now, allmodel.IsActive);
                return true;
            }
            catch { throw; }
        }
        public bool InsertAlbum(SponsoredModel model,AllGenerateCloneModel allmodel)
        {
            try
            {
                context.sp_InserUserSpon(model.UserID, model.CloneId, model.Title, model.Tag, model.ArtistName, model.AlbumTitle, model.AudioFilePath, model.UploadFilePath, model.MatrixFilePath, model.ComposerName, model.Producer, model.Publisher, model.SelectedInteruptionFile, model.InteruptionStyle, model.AvailableForDownload, model.ExplicitContent, model.UploadImageFilePath, model.UploadPDFFilePath, model.PagePercentage, model.Type, model.PdfFilePath, model.FileNames, model.VideoFilePath, model.WaterMarkMatrixImagePath, model.WaterMarkMatrixImageText, model.VideoCategory, model.RARFilePath, model.MatrixImagePath, model.CreatotName, model.TrackingNumber, model.CatID, model.GenCloneID, System.DateTime.Now, System.DateTime.Now, model.IsActive, model.FileSize);
                context.sp_AllGenerateClones(allmodel.UserID, allmodel.CloneId, allmodel.Title, allmodel.Tag, allmodel.ArtistName, allmodel.AlbumTitle, allmodel.AudioFilePath, allmodel.UploadFilePath, allmodel.MatrixFilePath, allmodel.ComposerName, allmodel.Producer, allmodel.Publisher, allmodel.SelectedInteruptionFile, allmodel.InteruptionStyle, allmodel.AvailableForDownload, allmodel.ExplicitContent, allmodel.UploadImageFilePath, allmodel.UploadPDFFilePath, allmodel.PagePercentage, allmodel.Type, allmodel.PdfFilePath, allmodel.FileNames, allmodel.VideoFilePath, allmodel.WaterMarkMatrixImagePath, allmodel.WaterMarkMatrixImageText, allmodel.VideoCategory, allmodel.RARFilePath, allmodel.MatrixImagePath, allmodel.CreatotName, allmodel.TrackingNumber, allmodel.CatID, allmodel.GenCloneID, System.DateTime.Now, System.DateTime.Now, allmodel.IsActive);
                return true;
            }
            catch { throw; }
        }
        
    }
}
