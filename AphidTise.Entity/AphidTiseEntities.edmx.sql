
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/21/2017 16:28:45
-- Generated from EDMX file: C:\Work\AphidByte\Code\AphidTise.Entity\AphidTiseEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [stag_abyt2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tbl_SocialNetworkStatus_tblCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbl_SocialNetworkStatus] DROP CONSTRAINT [FK_tbl_SocialNetworkStatus_tblCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAds_tblMasterAdsType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAds] DROP CONSTRAINT [FK_tblAds_tblMasterAdsType];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAds_tblMasterCredits]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAds] DROP CONSTRAINT [FK_tblAds_tblMasterCredits];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAds_tblSurveyQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAds] DROP CONSTRAINT [FK_tblAds_tblSurveyQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAphidlabAccount_tblPersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAphidlabAccount] DROP CONSTRAINT [FK_tblAphidlabAccount_tblPersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAphidlabAccount_tblSecurityQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAphidlabAccount] DROP CONSTRAINT [FK_tblAphidlabAccount_tblSecurityQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAphidTiseAccount_tblMasterAccountType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAphidTiseAccount] DROP CONSTRAINT [FK_tblAphidTiseAccount_tblMasterAccountType];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAphidTiseAccount_tblPersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAphidTiseAccount] DROP CONSTRAINT [FK_tblAphidTiseAccount_tblPersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAphidTiseAccount_tblSecurityQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAphidTiseAccount] DROP CONSTRAINT [FK_tblAphidTiseAccount_tblSecurityQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_tblAudioInterruption_tblUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblAudioInterruption] DROP CONSTRAINT [FK_tblAudioInterruption_tblUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_tblBasicAccount_tblSecurityQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblBasicAccount] DROP CONSTRAINT [FK_tblBasicAccount_tblSecurityQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_tblByterAccount_tblMasterAccountType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblByterAccount] DROP CONSTRAINT [FK_tblByterAccount_tblMasterAccountType];
GO
IF OBJECT_ID(N'[dbo].[FK_tblByterAccount_tblPersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblByterAccount] DROP CONSTRAINT [FK_tblByterAccount_tblPersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_tblLoginTokens_tblLoginTokens]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblLoginTokens] DROP CONSTRAINT [FK_tblLoginTokens_tblLoginTokens];
GO
IF OBJECT_ID(N'[dbo].[FK_tblLoginTokens_tblMasterAccountType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblLoginTokens] DROP CONSTRAINT [FK_tblLoginTokens_tblMasterAccountType];
GO
IF OBJECT_ID(N'[dbo].[FK_tblLoginTokens_tblUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblLoginTokens] DROP CONSTRAINT [FK_tblLoginTokens_tblUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_tblMerchantAccount_tblMasterAccountType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblMerchantAccount] DROP CONSTRAINT [FK_tblMerchantAccount_tblMasterAccountType];
GO
IF OBJECT_ID(N'[dbo].[FK_tblMerchantAccount_tblPersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblMerchantAccount] DROP CONSTRAINT [FK_tblMerchantAccount_tblPersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_tblMerchantAccount_tblSecurityQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblMerchantAccount] DROP CONSTRAINT [FK_tblMerchantAccount_tblSecurityQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_tblPremiumAccount_tblAudioInterruption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblPremiumAccount] DROP CONSTRAINT [FK_tblPremiumAccount_tblAudioInterruption];
GO
IF OBJECT_ID(N'[dbo].[FK_tblPremiumAccount_tblMasterAccountType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblPremiumAccount] DROP CONSTRAINT [FK_tblPremiumAccount_tblMasterAccountType];
GO
IF OBJECT_ID(N'[dbo].[FK_tblPremiumAccount_tblPersonAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblPremiumAccount] DROP CONSTRAINT [FK_tblPremiumAccount_tblPersonAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_tblPremiumAccount_tblSecurityQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblPremiumAccount] DROP CONSTRAINT [FK_tblPremiumAccount_tblSecurityQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_tblPremiumAccount_tblWaterMarkUpInterruption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblPremiumAccount] DROP CONSTRAINT [FK_tblPremiumAccount_tblWaterMarkUpInterruption];
GO
IF OBJECT_ID(N'[dbo].[FK_tblSocialNetwork_tblCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblSocialNetwork] DROP CONSTRAINT [FK_tblSocialNetwork_tblCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_tblToolsInfo_tblToolsInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblToolsInfo] DROP CONSTRAINT [FK_tblToolsInfo_tblToolsInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_tblUsers_tblUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [FK_tblUsers_tblUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_tblVideoInterruption_tblUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblVideoInterruption] DROP CONSTRAINT [FK_tblVideoInterruption_tblUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_tblWaterMarkUpInterruption_tblUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblWaterMarkUpInterruption] DROP CONSTRAINT [FK_tblWaterMarkUpInterruption_tblUsers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[aphidbyte].[tbl_Messages]', 'U') IS NOT NULL
    DROP TABLE [aphidbyte].[tbl_Messages];
GO
IF OBJECT_ID(N'[dbo].[Feedback]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feedback];
GO
IF OBJECT_ID(N'[dbo].[SocialNetworkLogin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialNetworkLogin];
GO
IF OBJECT_ID(N'[dbo].[tbl_AphidLabSoftware]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AphidLabSoftware];
GO
IF OBJECT_ID(N'[dbo].[tbl_AphidLabVideoUpload]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_AphidLabVideoUpload];
GO
IF OBJECT_ID(N'[dbo].[tbl_BCMSErrorLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_BCMSErrorLog];
GO
IF OBJECT_ID(N'[dbo].[tbl_ByterMessage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ByterMessage];
GO
IF OBJECT_ID(N'[dbo].[tbl_ChannelSubscription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_ChannelSubscription];
GO
IF OBJECT_ID(N'[dbo].[tbl_FeedBack]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_FeedBack];
GO
IF OBJECT_ID(N'[dbo].[tbl_Message]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Message];
GO
IF OBJECT_ID(N'[dbo].[tbl_MTUserSubscription]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_MTUserSubscription];
GO
IF OBJECT_ID(N'[dbo].[tbl_PlayList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_PlayList];
GO
IF OBJECT_ID(N'[dbo].[tbl_SendLinkToMT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SendLinkToMT];
GO
IF OBJECT_ID(N'[dbo].[tbl_SocialNetworkStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_SocialNetworkStatus];
GO
IF OBJECT_ID(N'[dbo].[tbl_Surveys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_Surveys];
GO
IF OBJECT_ID(N'[dbo].[tbl_TotalCredits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_TotalCredits];
GO
IF OBJECT_ID(N'[dbo].[tbl_UrlLinkSites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_UrlLinkSites];
GO
IF OBJECT_ID(N'[dbo].[tbl_UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbl_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[tblAccountTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAccountTypes];
GO
IF OBJECT_ID(N'[dbo].[tblAds]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAds];
GO
IF OBJECT_ID(N'[dbo].[tblAllGenerateClones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAllGenerateClones];
GO
IF OBJECT_ID(N'[dbo].[tblAphidlabAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAphidlabAccount];
GO
IF OBJECT_ID(N'[dbo].[tblAphidTiseAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAphidTiseAccount];
GO
IF OBJECT_ID(N'[dbo].[tblAudioInterruption]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAudioInterruption];
GO
IF OBJECT_ID(N'[dbo].[tblBasicAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblBasicAccount];
GO
IF OBJECT_ID(N'[dbo].[tblBasicGenerateClone]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblBasicGenerateClone];
GO
IF OBJECT_ID(N'[dbo].[tblByterAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblByterAccount];
GO
IF OBJECT_ID(N'[dbo].[tblCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblCategory];
GO
IF OBJECT_ID(N'[dbo].[tblChannelPage]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblChannelPage];
GO
IF OBJECT_ID(N'[dbo].[tblCreateLinkPost]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblCreateLinkPost];
GO
IF OBJECT_ID(N'[dbo].[tblCreditDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblCreditDetail];
GO
IF OBJECT_ID(N'[dbo].[tblDataStoragePlan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblDataStoragePlan];
GO
IF OBJECT_ID(N'[dbo].[tblFavourites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblFavourites];
GO
IF OBJECT_ID(N'[dbo].[tblForgetPassword]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblForgetPassword];
GO
IF OBJECT_ID(N'[dbo].[tblInterruptedBasicAudioFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblInterruptedBasicAudioFiles];
GO
IF OBJECT_ID(N'[dbo].[tblInterruptedFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblInterruptedFiles];
GO
IF OBJECT_ID(N'[dbo].[tblLoginTokens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblLoginTokens];
GO
IF OBJECT_ID(N'[dbo].[tblMasGenClonesType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMasGenClonesType];
GO
IF OBJECT_ID(N'[dbo].[tblMasterAccountType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMasterAccountType];
GO
IF OBJECT_ID(N'[dbo].[tblMasterAdsType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMasterAdsType];
GO
IF OBJECT_ID(N'[dbo].[tblMasterCatType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMasterCatType];
GO
IF OBJECT_ID(N'[dbo].[tblMasterCredits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMasterCredits];
GO
IF OBJECT_ID(N'[dbo].[tblMerchantAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMerchantAccount];
GO
IF OBJECT_ID(N'[dbo].[tblPersonAddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblPersonAddress];
GO
IF OBJECT_ID(N'[dbo].[tblPostingStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblPostingStatus];
GO
IF OBJECT_ID(N'[dbo].[tblPremiumAccount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblPremiumAccount];
GO
IF OBJECT_ID(N'[dbo].[tblPremiumGeterateClone]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblPremiumGeterateClone];
GO
IF OBJECT_ID(N'[dbo].[tblRelease]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblRelease];
GO
IF OBJECT_ID(N'[dbo].[tblReleaseUpdate]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblReleaseUpdate];
GO
IF OBJECT_ID(N'[dbo].[tblSecurityQuestions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblSecurityQuestions];
GO
IF OBJECT_ID(N'[dbo].[tblSocialNetwork]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblSocialNetwork];
GO
IF OBJECT_ID(N'[dbo].[tblSurveyQuestion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblSurveyQuestion];
GO
IF OBJECT_ID(N'[dbo].[tblTools]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblTools];
GO
IF OBJECT_ID(N'[dbo].[tblToolsInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblToolsInfo];
GO
IF OBJECT_ID(N'[dbo].[tblUserActivation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUserActivation];
GO
IF OBJECT_ID(N'[dbo].[tblUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUsers];
GO
IF OBJECT_ID(N'[dbo].[tblUsersSponsored]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblUsersSponsored];
GO
IF OBJECT_ID(N'[dbo].[tblVideoInterruption]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblVideoInterruption];
GO
IF OBJECT_ID(N'[dbo].[tblWaterMarkUpInterruption]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblWaterMarkUpInterruption];
GO
IF OBJECT_ID(N'[dbo].[TrainingBatch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TrainingBatch];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[jobseeders_com_aphidbyteModelStoreContainer].[tblAdmin]', 'U') IS NOT NULL
    DROP TABLE [jobseeders_com_aphidbyteModelStoreContainer].[tblAdmin];
GO
IF OBJECT_ID(N'[jobseeders_com_aphidbyteModelStoreContainer].[tblToolFile]', 'U') IS NOT NULL
    DROP TABLE [jobseeders_com_aphidbyteModelStoreContainer].[tblToolFile];
GO
IF OBJECT_ID(N'[jobseeders_com_aphidbyteModelStoreContainer].[Demo]', 'U') IS NOT NULL
    DROP TABLE [jobseeders_com_aphidbyteModelStoreContainer].[Demo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SocialNetworkLogins'
CREATE TABLE [dbo].[SocialNetworkLogins] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(75)  NULL,
    [Password] nvarchar(75)  NULL,
    [Userid] uniqueidentifier  NULL,
    [Category] int  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'tbl_BCMSErrorLog'
CREATE TABLE [dbo].[tbl_BCMSErrorLog] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Controller] nvarchar(50)  NULL,
    [Action] nvarchar(50)  NULL,
    [Exception] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_ByterMessage'
CREATE TABLE [dbo].[tbl_ByterMessage] (
    [id] int IDENTITY(1,1) NOT NULL,
    [ChannelId] uniqueidentifier  NULL,
    [ByterId] uniqueidentifier  NULL,
    [MsgStatus] bit  NULL
);
GO

-- Creating table 'tbl_ChannelSubscription'
CREATE TABLE [dbo].[tbl_ChannelSubscription] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ByterUserId] uniqueidentifier  NULL,
    [PremiumUserid] uniqueidentifier  NULL,
    [ChannelImage] nvarchar(max)  NULL,
    [Tillte] nvarchar(100)  NULL,
    [Status] bit  NULL,
    [SubscribeDate] datetime  NULL,
    [ChannelID] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_FeedBack'
CREATE TABLE [dbo].[tbl_FeedBack] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserEmail] nvarchar(50)  NULL,
    [Text] nvarchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [Subject] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_Message'
CREATE TABLE [dbo].[tbl_Message] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [CreditPoint] nvarchar(50)  NULL,
    [IsNew] int  NULL,
    [Message] nvarchar(max)  NULL,
    [ByterMessage] bit  NULL,
    [ByterLink] bit  NULL,
    [ChannelId] uniqueidentifier  NULL
);
GO

-- Creating table 'tbl_MTUserSubscription'
CREATE TABLE [dbo].[tbl_MTUserSubscription] (
    [ComposerName] nvarchar(50)  NULL,
    [ChannelId] uniqueidentifier  NULL,
    [Credits] int  NULL,
    [Category] nvarchar(50)  NULL,
    [UserId] uniqueidentifier  NULL,
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NULL,
    [Path] nvarchar(max)  NULL,
    [LinkToPostM] bit  NULL,
    [PremiumProfilePic] nvarchar(max)  NULL,
    [TrackingNumber] nvarchar(max)  NULL,
    [MessageStatus] bit  NULL,
    [ByterUsrId] uniqueidentifier  NULL,
    [ByterUserName] nvarchar(50)  NULL,
    [IsDelete] bit  NULL,
    [IsPostDelete] bit  NULL
);
GO

-- Creating table 'tbl_PlayList'
CREATE TABLE [dbo].[tbl_PlayList] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [PlaylistName] nvarchar(50)  NOT NULL,
    [FileName] nvarchar(100)  NULL,
    [TrackingID] nvarchar(50)  NOT NULL,
    [Composer] nchar(50)  NOT NULL,
    [CatId] int  NOT NULL
);
GO

-- Creating table 'tbl_SendLinkToMT'
CREATE TABLE [dbo].[tbl_SendLinkToMT] (
    [ComposerName] nvarchar(50)  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [ChannelId] uniqueidentifier  NOT NULL,
    [Category] nvarchar(50)  NOT NULL,
    [Credits] int  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [ID] int IDENTITY(1,1) NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [TrackingNumber] nvarchar(150)  NOT NULL,
    [LinkToPostM] bit  NULL,
    [PremiumProfilePic] nvarchar(max)  NULL,
    [MessageStatus] bit  NULL,
    [ByterUsrId] uniqueidentifier  NULL,
    [IsDelete] bit  NULL
);
GO

-- Creating table 'tbl_SocialNetworkStatus'
CREATE TABLE [dbo].[tbl_SocialNetworkStatus] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Aphid] uniqueidentifier  NULL,
    [Status] bit  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [CategoryID] int  NULL
);
GO

-- Creating table 'tbl_Surveys'
CREATE TABLE [dbo].[tbl_Surveys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ByterUserId] uniqueidentifier  NOT NULL,
    [TrackingNo] nvarchar(50)  NOT NULL,
    [Imagepath] nvarchar(100)  NOT NULL,
    [Credits] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifyDate] datetime  NOT NULL,
    [FeedbackText] nvarchar(50)  NULL,
    [IsNew] int  NULL
);
GO

-- Creating table 'tbl_TotalCredits'
CREATE TABLE [dbo].[tbl_TotalCredits] (
    [UserId] uniqueidentifier  NOT NULL,
    [Credits] int  NULL
);
GO

-- Creating table 'tbl_UrlLinkSites'
CREATE TABLE [dbo].[tbl_UrlLinkSites] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [UrlSites] nvarchar(max)  NULL,
    [Date] datetime  NOT NULL,
    [Status] bit  NOT NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'tbl_UserInfo'
CREATE TABLE [dbo].[tbl_UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MobileNo] nvarchar(50)  NULL,
    [BloodGroup] nvarchar(50)  NULL,
    [UserId] uniqueidentifier  NULL
);
GO

-- Creating table 'tblAds'
CREATE TABLE [dbo].[tblAds] (
    [AdID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [Title] nvarchar(200)  NULL,
    [AdInformation] nvarchar(max)  NULL,
    [AdCycleFromDate] datetime  NULL,
    [AdCycleToDate] datetime  NULL,
    [AdTypeID] int  NULL,
    [AdVideo] nvarchar(max)  NULL,
    [AdHyperLinkUrl] nvarchar(200)  NULL,
    [PriceToDisplay] decimal(19,4)  NULL,
    [CreditsID] int  NULL,
    [SurveyID] uniqueidentifier  NULL,
    [CreateDate] datetime  NULL,
    [ModifyDate] datetime  NULL,
    [IsDelete] bit  NULL,
    [CompanyLogo] nvarchar(100)  NULL,
    [AdPicture] nvarchar(100)  NULL,
    [TrackingNo] nvarchar(10)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'tblAllGenerateClones'
CREATE TABLE [dbo].[tblAllGenerateClones] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [CloneId] uniqueidentifier  NULL,
    [Title] nvarchar(100)  NULL,
    [Tag] nvarchar(100)  NULL,
    [ArtistName] nvarchar(100)  NULL,
    [AlbumTitle] nvarchar(100)  NULL,
    [AudioFilePath] nvarchar(500)  NULL,
    [UploadFilePath] nvarchar(500)  NULL,
    [MatrixFilePath] nvarchar(500)  NULL,
    [ComposerName] nvarchar(100)  NULL,
    [Producer] nvarchar(100)  NULL,
    [Publisher] nvarchar(100)  NULL,
    [SelectedInteruptionFile] nvarchar(500)  NULL,
    [InteruptionStyle] nvarchar(500)  NULL,
    [AvailableForDownload] char(1)  NULL,
    [ExplicitContent] nvarchar(100)  NULL,
    [UploadImageFilePath] nvarchar(500)  NULL,
    [UploadPDFFilePath] nvarchar(500)  NULL,
    [PagePercentage] nvarchar(100)  NULL,
    [Type] nvarchar(100)  NULL,
    [PdfFilePath] nvarchar(500)  NULL,
    [FileNames] nvarchar(500)  NULL,
    [VideoFilePath] nvarchar(500)  NULL,
    [WaterMarkMatrixImagePath] nvarchar(500)  NULL,
    [WaterMarkMatrixImageText] nvarchar(500)  NULL,
    [VideoCategory] nvarchar(100)  NULL,
    [RARFilePath] nvarchar(500)  NULL,
    [MatrixImagePath] nvarchar(500)  NULL,
    [CreatotName] nvarchar(100)  NULL,
    [TrackingNumber] nvarchar(100)  NULL,
    [CatID] int  NULL,
    [GenCloneID] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifyDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'tblAudioInterruptions'
CREATE TABLE [dbo].[tblAudioInterruptions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [AudioInterruptionFileName] nvarchar(max)  NULL,
    [IsActive] bit  NULL,
    [FileName] nvarchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifyDate] datetime  NOT NULL
);
GO

-- Creating table 'tblBasicGenerateClones'
CREATE TABLE [dbo].[tblBasicGenerateClones] (
    [id] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [CloneID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(50)  NULL,
    [Tags] nvarchar(50)  NULL,
    [ArtistName] nvarchar(50)  NULL,
    [AlbumTitle] nvarchar(50)  NULL,
    [UploadFileAudioPath] nvarchar(max)  NULL,
    [MatrixImagePath] nvarchar(max)  NULL,
    [Composer] nvarchar(50)  NULL,
    [Publisher] nvarchar(50)  NULL,
    [Producer] nvarchar(50)  NULL,
    [SelectIntFile] nvarchar(50)  NULL,
    [InterruptionStyle] nvarchar(50)  NULL,
    [AvailableForDownload] nvarchar(50)  NULL,
    [ExplicitContent] nvarchar(50)  NULL,
    [UploadFileImagePath] nvarchar(max)  NULL,
    [UploadFilePDFPath] nvarchar(max)  NULL,
    [PagePercentage] nvarchar(50)  NULL,
    [WatermarkMatrixImagePath] nvarchar(max)  NULL,
    [WatermarkMatrixImageText] nvarchar(50)  NULL,
    [VideoCategory] nvarchar(50)  NULL,
    [TrackingNumber] nvarchar(50)  NULL,
    [CatID] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifyDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [VideoPath] nvarchar(max)  NULL,
    [RarPath] nvarchar(max)  NULL,
    [TotalLength] nvarchar(10)  NULL,
    [UploadFileAudio2Path] nvarchar(max)  NULL
);
GO

-- Creating table 'tblCategories'
CREATE TABLE [dbo].[tblCategories] (
    [CategoryID] int  NOT NULL,
    [CategoryName] nvarchar(50)  NOT NULL,
    [Credit] int  NULL,
    [Channel] nvarchar(50)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'tblChannelPages'
CREATE TABLE [dbo].[tblChannelPages] (
    [ChannelID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [ChannelImagePath] nvarchar(max)  NULL,
    [ChannelBiography] nvarchar(max)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [UserName] nvarchar(100)  NULL
);
GO

-- Creating table 'tblCreateLinkPosts'
CREATE TABLE [dbo].[tblCreateLinkPosts] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(100)  NULL,
    [Channel] nvarchar(100)  NULL,
    [NoOfClones] int  NULL,
    [Views] int  NULL,
    [Downloads] int  NULL,
    [FileSize] nvarchar(50)  NULL,
    [TrackingNo] nvarchar(50)  NULL,
    [PostedDate] datetime  NULL,
    [Category] nvarchar(100)  NULL,
    [UserId] uniqueidentifier  NULL,
    [ChannelStatus] bit  NULL,
    [IsDelete] bit  NULL,
    [MatrixImagePath] nvarchar(max)  NULL
);
GO

-- Creating table 'tblCreditDetails'
CREATE TABLE [dbo].[tblCreditDetails] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Aphid] uniqueidentifier  NULL,
    [Channel] nvarchar(50)  NULL,
    [Credit] int  NULL,
    [Category] nvarchar(50)  NULL,
    [File_Size] nvarchar(50)  NULL,
    [Path] nvarchar(255)  NULL,
    [Title] nvarchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifyDate] datetime  NULL,
    [IsActive] bit  NOT NULL,
    [TrackingID] nvarchar(8)  NULL,
    [LinktoPost] bit  NULL
);
GO

-- Creating table 'tblDataStoragePlans'
CREATE TABLE [dbo].[tblDataStoragePlans] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [StoragePlan] nvarchar(50)  NULL,
    [UsedSpace] nvarchar(max)  NULL,
    [FreeSpace] nvarchar(max)  NULL,
    [AccountTypeId] int  NULL,
    [ExpireDate] datetime  NULL
);
GO

-- Creating table 'tblFavourites'
CREATE TABLE [dbo].[tblFavourites] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [FileName] nvarchar(500)  NULL,
    [TrackingNumber] nvarchar(10)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [IsActive] int  NULL
);
GO

-- Creating table 'tblForgetPasswords'
CREATE TABLE [dbo].[tblForgetPasswords] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [EmailId] nvarchar(50)  NULL,
    [Token] uniqueidentifier  NULL,
    [Status] bit  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifyDate] datetime  NOT NULL
);
GO

-- Creating table 'tblInterruptedBasicAudioFiles'
CREATE TABLE [dbo].[tblInterruptedBasicAudioFiles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CloneID] uniqueidentifier  NOT NULL,
    [SongName] nvarchar(100)  NULL,
    [InterruptedByte] varbinary(max)  NULL,
    [Type] nvarchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'tblInterruptedFiles'
CREATE TABLE [dbo].[tblInterruptedFiles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [CloneId] uniqueidentifier  NULL,
    [InterruptFilePath] nvarchar(max)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL,
    [IsActive] bit  NULL,
    [FileName] nvarchar(100)  NULL,
    [VideoPath] nvarchar(max)  NULL,
    [TrackingNumber] nvarchar(50)  NULL,
    [CatID] int  NULL
);
GO

-- Creating table 'tblMasGenClonesTypes'
CREATE TABLE [dbo].[tblMasGenClonesTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GenCloneName] nvarchar(100)  NULL
);
GO

-- Creating table 'tblMasterAccountTypes'
CREATE TABLE [dbo].[tblMasterAccountTypes] (
    [AccountTypeID] int IDENTITY(1,1) NOT NULL,
    [AccountTypeName] nvarchar(100)  NULL
);
GO

-- Creating table 'tblMasterAdsTypes'
CREATE TABLE [dbo].[tblMasterAdsTypes] (
    [AdTypeID] int IDENTITY(1,1) NOT NULL,
    [AdTypeName] nvarchar(50)  NULL,
    [AdFeature] smallint  NULL
);
GO

-- Creating table 'tblMasterCatTypes'
CREATE TABLE [dbo].[tblMasterCatTypes] (
    [CatID] int  NOT NULL,
    [CatName] nvarchar(30)  NULL
);
GO

-- Creating table 'tblMasterCredits'
CREATE TABLE [dbo].[tblMasterCredits] (
    [CreditsID] int IDENTITY(1,1) NOT NULL,
    [CreditsPoint] nvarchar(50)  NULL
);
GO

-- Creating table 'tblMerchantAccounts'
CREATE TABLE [dbo].[tblMerchantAccounts] (
    [MerchantUserID] uniqueidentifier  NOT NULL,
    [CompanyLogo] varbinary(max)  NULL,
    [CompanyName] nvarchar(50)  NULL,
    [Informations] nvarchar(max)  NULL,
    [Websites] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [Phone] nvarchar(20)  NULL,
    [ProductServices] nvarchar(200)  NULL,
    [AddressID] uniqueidentifier  NULL,
    [SecurityQuestionID] uniqueidentifier  NULL,
    [AccountTypeID] int  NULL,
    [CreateDate] datetime  NULL,
    [ModifyDate] datetime  NULL,
    [IsDelete] bit  NULL
);
GO

-- Creating table 'tblPersonAddresses'
CREATE TABLE [dbo].[tblPersonAddresses] (
    [AddressID] uniqueidentifier  NOT NULL,
    [AddressLine1] nvarchar(500)  NULL,
    [AddressLine2] nvarchar(500)  NULL,
    [City] nvarchar(50)  NULL,
    [Region] nvarchar(200)  NULL,
    [PostalCode] nvarchar(50)  NULL
);
GO

-- Creating table 'tblPostingStatus'
CREATE TABLE [dbo].[tblPostingStatus] (
    [Title] nvarchar(50)  NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Status] nvarchar(50)  NOT NULL,
    [CategoryName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'tblPremiumGeterateClones'
CREATE TABLE [dbo].[tblPremiumGeterateClones] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [CloneID] uniqueidentifier  NULL,
    [Title] nvarchar(50)  NULL,
    [Tags] nvarchar(50)  NULL,
    [ArtistName] nvarchar(50)  NULL,
    [AlbumTitle] nvarchar(50)  NULL,
    [AudioFilePath] nvarchar(100)  NULL,
    [ComposerName] nvarchar(50)  NULL,
    [Producer] nvarchar(50)  NULL,
    [Publisher] nvarchar(50)  NULL,
    [SelectedInterruptionFile] nvarchar(50)  NULL,
    [InterruptionStyle] nvarchar(50)  NULL,
    [AvailableForDownload] nvarchar(50)  NULL,
    [ExplicitContent] nvarchar(50)  NULL,
    [Type] nvarchar(50)  NULL,
    [PDFFilePath] nvarchar(max)  NULL,
    [VideoFilePath] nvarchar(max)  NULL,
    [PagePercentage] nvarchar(50)  NULL,
    [RARFilePAth] nvarchar(max)  NULL,
    [MatrixImagePath] nvarchar(max)  NULL,
    [CreatorName] nvarchar(50)  NULL,
    [TrackingNumber] nvarchar(50)  NULL,
    [ImageFile] nvarchar(max)  NULL,
    [CatID] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifyDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Total_Length] nvarchar(10)  NULL
);
GO

-- Creating table 'tblReleases'
CREATE TABLE [dbo].[tblReleases] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ReleaseName] nvarchar(max)  NULL
);
GO

-- Creating table 'tblReleaseUpdates'
CREATE TABLE [dbo].[tblReleaseUpdates] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ReleaseId] int  NULL,
    [Message] nvarchar(max)  NULL,
    [ImagePath] nvarchar(50)  NULL
);
GO

-- Creating table 'tblSecurityQuestions'
CREATE TABLE [dbo].[tblSecurityQuestions] (
    [SecurityQuestionID] uniqueidentifier  NOT NULL,
    [SecurityQuestion1] nvarchar(500)  NULL,
    [Answer1] nvarchar(200)  NULL,
    [SecurityQuestion2] nvarchar(500)  NULL,
    [Answer2] nvarchar(200)  NULL
);
GO

-- Creating table 'tblSocialNetworks'
CREATE TABLE [dbo].[tblSocialNetworks] (
    [ID] uniqueidentifier  NOT NULL,
    [Aphid] uniqueidentifier  NULL,
    [AccessToken] nvarchar(max)  NULL,
    [Category] int  NULL,
    [IsDeleted] bit  NULL,
    [Expires] datetime  NULL,
    [Refreshtoken] nvarchar(255)  NULL
);
GO

-- Creating table 'tblSurveyQuestions'
CREATE TABLE [dbo].[tblSurveyQuestions] (
    [SurveyID] uniqueidentifier  NOT NULL,
    [Question] nvarchar(max)  NULL,
    [Option1] nvarchar(300)  NULL,
    [Option2] nvarchar(300)  NULL,
    [Option3] nvarchar(300)  NULL,
    [Option4] nvarchar(300)  NULL,
    [Option5] nvarchar(300)  NULL,
    [Option6] nvarchar(300)  NULL,
    [Option7] nvarchar(300)  NULL,
    [Option8] nvarchar(300)  NULL
);
GO

-- Creating table 'tblTools'
CREATE TABLE [dbo].[tblTools] (
    [ToolID] int IDENTITY(1,1) NOT NULL,
    [ToolName] nvarchar(50)  NOT NULL,
    [Images] nvarchar(max)  NOT NULL,
    [IsActive] bit  NULL,
    [ToolInfo] nvarchar(max)  NULL
);
GO

-- Creating table 'tblToolsInfoes'
CREATE TABLE [dbo].[tblToolsInfoes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ToolID] int  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [CreatedOn] datetime  NOT NULL,
    [Modify] datetime  NOT NULL
);
GO

-- Creating table 'tblUserActivations'
CREATE TABLE [dbo].[tblUserActivations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [TokenId] uniqueidentifier  NULL,
    [UserName] nvarchar(50)  NULL,
    [Status] bit  NULL
);
GO

-- Creating table 'tblUsersSponsoreds'
CREATE TABLE [dbo].[tblUsersSponsoreds] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] uniqueidentifier  NULL,
    [CloneId] uniqueidentifier  NULL,
    [Title] nvarchar(100)  NULL,
    [Tag] nvarchar(100)  NULL,
    [ArtistName] nvarchar(100)  NULL,
    [AlbumTitle] nvarchar(100)  NULL,
    [AudioFilePath] nvarchar(500)  NULL,
    [UploadFilePath] nvarchar(500)  NULL,
    [MatrixFilePath] nvarchar(500)  NULL,
    [ComposerName] nvarchar(100)  NULL,
    [Producer] nvarchar(100)  NULL,
    [Publisher] nvarchar(100)  NULL,
    [SelectedInteruptionFile] nvarchar(500)  NULL,
    [InteruptionStyle] nvarchar(500)  NULL,
    [AvailableForDownload] char(1)  NULL,
    [ExplicitContent] nvarchar(100)  NULL,
    [UploadImageFilePath] nvarchar(500)  NULL,
    [UploadPDFFilePath] nvarchar(500)  NULL,
    [PagePercentage] nvarchar(100)  NULL,
    [Type] nvarchar(100)  NULL,
    [PdfFilePath] nvarchar(500)  NULL,
    [FileNames] nvarchar(500)  NULL,
    [VideoFilePath] nvarchar(500)  NULL,
    [WaterMarkMatrixImagePath] nvarchar(500)  NULL,
    [WaterMarkMatrixImageText] nvarchar(500)  NULL,
    [VideoCategory] nvarchar(100)  NULL,
    [RARFilePath] nvarchar(500)  NULL,
    [MatrixImagePath] nvarchar(500)  NULL,
    [CreatotName] nvarchar(100)  NULL,
    [TrackingNumber] nvarchar(100)  NULL,
    [CatID] int  NULL,
    [GenCloneID] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [ModifyDate] datetime  NOT NULL,
    [IsActive] bit  NOT NULL,
    [FileSize] nvarchar(10)  NULL
);
GO

-- Creating table 'tblVideoInterruptions'
CREATE TABLE [dbo].[tblVideoInterruptions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [VideoInterruption] varbinary(max)  NULL
);
GO

-- Creating table 'tblWaterMarkUpInterruptions'
CREATE TABLE [dbo].[tblWaterMarkUpInterruptions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserId] uniqueidentifier  NULL,
    [IsActive] bit  NULL,
    [WatermarkImageName] nvarchar(max)  NULL,
    [ImageInterruption] nvarchar(max)  NULL
);
GO

-- Creating table 'tblAdmins'
CREATE TABLE [dbo].[tblAdmins] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [AdminID] uniqueidentifier  NULL,
    [Password] nvarchar(10)  NULL,
    [AdminName] nvarchar(50)  NULL,
    [IsFree] bit  NULL,
    [GroupId] uniqueidentifier  NULL
);
GO

-- Creating table 'tblToolFiles'
CREATE TABLE [dbo].[tblToolFiles] (
    [ToolID] int  NOT NULL,
    [FileName] nvarchar(50)  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Toolcontent] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'tblUsers'
CREATE TABLE [dbo].[tblUsers] (
    [UserId] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserPassword] nvarchar(50)  NULL,
    [UserStatus] bit  NULL,
    [AccountTypeID] int  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [RecoveryEmail] nvarchar(50)  NULL,
    [PictureServerId] int  NULL,
    [PicturePath] nvarchar(max)  NULL,
    [StripeCustomerID] nvarchar(max)  NULL
);
GO

-- Creating table 'tbl_AphidLabSoftware'
CREATE TABLE [dbo].[tbl_AphidLabSoftware] (
    [ID] int  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [SoftwareID] uniqueidentifier  NOT NULL,
    [SoftwareTitle] nvarchar(100)  NULL,
    [MatrixImagePath] nvarchar(200)  NULL,
    [AvalibaleForDownload] nvarchar(50)  NULL,
    [DownloadPassword] nvarchar(50)  NULL,
    [SoftwareDeiscription] nvarchar(max)  NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'tbl_AphidLabVideoUpload'
CREATE TABLE [dbo].[tbl_AphidLabVideoUpload] (
    [ID] int  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [VideoID] uniqueidentifier  NOT NULL,
    [VideoTitle] nvarchar(100)  NULL,
    [MatrixImagePath] nvarchar(200)  NULL,
    [InteruptionFilePath] nvarchar(200)  NULL,
    [InteruptionStyle] nvarchar(50)  NULL,
    [AvalibaleForDownload] nvarchar(50)  NULL,
    [ExcplictContent] nvarchar(50)  NULL,
    [DownloadPassword] nvarchar(50)  NULL,
    [VideoDeiscription] nvarchar(max)  NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'tblAphidlabAccounts'
CREATE TABLE [dbo].[tblAphidlabAccounts] (
    [UserId] uniqueidentifier  NOT NULL,
    [DeveloperName] nvarchar(50)  NOT NULL,
    [UserEmail] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NULL,
    [Firstname] nvarchar(50)  NULL,
    [Lastname] nvarchar(50)  NULL,
    [DOB] nvarchar(50)  NULL,
    [Phonenumber] nvarchar(50)  NULL,
    [WebsiteUrl] nvarchar(100)  NULL,
    [ChannelUrl] nvarchar(100)  NULL,
    [AddressId] uniqueidentifier  NOT NULL,
    [SecurityQuestionID] uniqueidentifier  NOT NULL,
    [Accountid] int  NULL,
    [CreatedDate] datetime  NULL,
    [UpdateDate] datetime  NULL,
    [IsActive] bit  NULL,
    [RecoveryEmail] nvarchar(50)  NULL
);
GO

-- Creating table 'tblAphidTiseAccounts'
CREATE TABLE [dbo].[tblAphidTiseAccounts] (
    [AphidTiseUserID] uniqueidentifier  NOT NULL,
    [CompanyName] nvarchar(100)  NULL,
    [Password] nvarchar(50)  NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [DOB] nvarchar(50)  NULL,
    [Phone] nvarchar(20)  NULL,
    [AddressID] uniqueidentifier  NULL,
    [SecurityQuestionID] uniqueidentifier  NULL,
    [AccountTypeID] int  NULL,
    [CreateDate] datetime  NULL,
    [ModifyDate] datetime  NULL,
    [IsDelete] bit  NULL,
    [Informations] nvarchar(max)  NULL,
    [Website] nvarchar(max)  NULL,
    [ProductService] nvarchar(500)  NULL
);
GO

-- Creating table 'tblPremiumAccounts'
CREATE TABLE [dbo].[tblPremiumAccounts] (
    [PremiumUserID] uniqueidentifier  NOT NULL,
    [ComposerName] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [DOB] nvarchar(50)  NULL,
    [Biography] nvarchar(max)  NULL,
    [Website] nvarchar(200)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [RecoveryEmail] nvarchar(50)  NULL,
    [Phone] nvarchar(20)  NULL,
    [AddressID] uniqueidentifier  NULL,
    [SecurityQuestionID] uniqueidentifier  NULL,
    [AccountTypeID] int  NULL,
    [CreateDate] datetime  NULL,
    [ModifyDate] datetime  NULL,
    [IsDelete] bit  NULL,
    [AudioInterruptionID] int  NULL,
    [WatermarksInterruptionID] int  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'tblByterAccounts'
CREATE TABLE [dbo].[tblByterAccounts] (
    [ByterUserID] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [DOB] nvarchar(50)  NULL,
    [Phone] nvarchar(20)  NULL,
    [RecoveryEmail] nvarchar(50)  NULL,
    [AddressID] uniqueidentifier  NULL,
    [SecurityQuestionID] uniqueidentifier  NULL,
    [AccountTypeID] int  NULL,
    [CreateDate] datetime  NULL,
    [ModifyDate] datetime  NULL,
    [IsDelete] bit  NULL
);
GO

-- Creating table 'tbl_Messages'
CREATE TABLE [dbo].[tbl_Messages] (
    [id] int IDENTITY(1,1) NOT NULL,
    [emailid_sender] nvarchar(50)  NULL,
    [emailid_receiver] nvarchar(50)  NULL,
    [message_subject] nvarchar(30)  NULL,
    [message_body] nvarchar(max)  NULL,
    [message_status] bit  NULL,
    [sender_name] nvarchar(30)  NULL,
    [receiver_name] nvarchar(30)  NULL,
    [sending_date] datetime  NULL,
    [Is_Read] bit  NULL
);
GO

-- Creating table 'tblBasicAccounts'
CREATE TABLE [dbo].[tblBasicAccounts] (
    [BasicUserID] uniqueidentifier  NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [EmailAddress] nvarchar(50)  NULL,
    [DOB] nvarchar(50)  NULL,
    [Phone] nvarchar(20)  NULL,
    [AudioInterruptionFile] nvarchar(max)  NULL,
    [WebSiteUrl] nvarchar(200)  NULL,
    [AddressID] uniqueidentifier  NULL,
    [SecurityQuestionID] uniqueidentifier  NULL,
    [AccountTypeID] int  NULL,
    [CreateDate] datetime  NULL,
    [ModifyDate] datetime  NULL,
    [IsDelete] bit  NULL,
    [RecoveryEmail] nvarchar(50)  NULL,
    [IsActive] bit  NULL,
    [WaterMarkImage] nvarchar(max)  NULL
);
GO

-- Creating table 'Feedback'
CREATE TABLE [dbo].[Feedback] (
    [FeedbackId] int IDENTITY(1,1) NOT NULL,
    [BatchId] int  NULL,
    [UserId] int  NULL,
    [FeedbackComment] nvarchar(max)  NULL,
    [PostedDate] datetime  NULL,
    [IsActive] bit  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'TrainingBatch'
CREATE TABLE [dbo].[TrainingBatch] (
    [BatchId] int IDENTITY(1,1) NOT NULL,
    [BatchName] nvarchar(250)  NULL,
    [BatchDescription] nvarchar(max)  NULL,
    [BatchStartDate] datetime  NULL,
    [BatchTimings] nvarchar(100)  NULL,
    [BatchDuration] int  NULL,
    [BatchLanguage] varchar(50)  NULL,
    [IsActive] bit  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [UserEmail] nvarchar(100)  NULL,
    [UserPicture] nvarchar(max)  NULL,
    [UserContact] bigint  NULL,
    [UserAddress] nvarchar(max)  NULL,
    [SocialWebsite] nvarchar(50)  NULL,
    [UserDesgination] nvarchar(50)  NULL,
    [UserCompany] nvarchar(50)  NULL,
    [CreatedDate] datetime  NULL,
    [ModifiedDate] datetime  NULL
);
GO

-- Creating table 'Demo'
CREATE TABLE [dbo].[Demo] (
    [EmailAddress] nvarchar(50)  NULL,
    [UserName] nvarchar(100)  NULL,
    [BasicUserID] uniqueidentifier  NOT NULL,
    [SecurityQuestionID] uniqueidentifier  NULL,
    [AccType] nvarchar(100)  NULL
);
GO

-- Creating table 'tblLoginTokens'
CREATE TABLE [dbo].[tblLoginTokens] (
    [UserId] uniqueidentifier  NOT NULL,
    [TokenId] uniqueidentifier  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [AccountTypeId] int  NOT NULL
);
GO

-- Creating table 'tblPremiumCodes'
CREATE TABLE [dbo].[tblPremiumCodes] (
    [PremiumCode] nvarchar(50)  NOT NULL,
    [AlreadyRedeemed] bit  NOT NULL,
    [ExpirationDate] datetime  NOT NULL
);
GO

-- Creating table 'tblAccountTypes'
CREATE TABLE [dbo].[tblAccountTypes] (
    [AphidAccountID] int IDENTITY(1,1) NOT NULL,
    [AphidAccountName] varchar(200)  NULL,
    [Active] bit  NULL,
    [Description] varchar(200)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'SocialNetworkLogins'
ALTER TABLE [dbo].[SocialNetworkLogins]
ADD CONSTRAINT [PK_SocialNetworkLogins]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_BCMSErrorLog'
ALTER TABLE [dbo].[tbl_BCMSErrorLog]
ADD CONSTRAINT [PK_tbl_BCMSErrorLog]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [id] in table 'tbl_ByterMessage'
ALTER TABLE [dbo].[tbl_ByterMessage]
ADD CONSTRAINT [PK_tbl_ByterMessage]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_ChannelSubscription'
ALTER TABLE [dbo].[tbl_ChannelSubscription]
ADD CONSTRAINT [PK_tbl_ChannelSubscription]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_FeedBack'
ALTER TABLE [dbo].[tbl_FeedBack]
ADD CONSTRAINT [PK_tbl_FeedBack]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_Message'
ALTER TABLE [dbo].[tbl_Message]
ADD CONSTRAINT [PK_tbl_Message]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_MTUserSubscription'
ALTER TABLE [dbo].[tbl_MTUserSubscription]
ADD CONSTRAINT [PK_tbl_MTUserSubscription]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_PlayList'
ALTER TABLE [dbo].[tbl_PlayList]
ADD CONSTRAINT [PK_tbl_PlayList]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SendLinkToMT'
ALTER TABLE [dbo].[tbl_SendLinkToMT]
ADD CONSTRAINT [PK_tbl_SendLinkToMT]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_SocialNetworkStatus'
ALTER TABLE [dbo].[tbl_SocialNetworkStatus]
ADD CONSTRAINT [PK_tbl_SocialNetworkStatus]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'tbl_Surveys'
ALTER TABLE [dbo].[tbl_Surveys]
ADD CONSTRAINT [PK_tbl_Surveys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'tbl_TotalCredits'
ALTER TABLE [dbo].[tbl_TotalCredits]
ADD CONSTRAINT [PK_tbl_TotalCredits]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_UrlLinkSites'
ALTER TABLE [dbo].[tbl_UrlLinkSites]
ADD CONSTRAINT [PK_tbl_UrlLinkSites]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_UserInfo'
ALTER TABLE [dbo].[tbl_UserInfo]
ADD CONSTRAINT [PK_tbl_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [AdID] in table 'tblAds'
ALTER TABLE [dbo].[tblAds]
ADD CONSTRAINT [PK_tblAds]
    PRIMARY KEY CLUSTERED ([AdID] ASC);
GO

-- Creating primary key on [ID] in table 'tblAllGenerateClones'
ALTER TABLE [dbo].[tblAllGenerateClones]
ADD CONSTRAINT [PK_tblAllGenerateClones]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblAudioInterruptions'
ALTER TABLE [dbo].[tblAudioInterruptions]
ADD CONSTRAINT [PK_tblAudioInterruptions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [id] in table 'tblBasicGenerateClones'
ALTER TABLE [dbo].[tblBasicGenerateClones]
ADD CONSTRAINT [PK_tblBasicGenerateClones]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [CategoryID] in table 'tblCategories'
ALTER TABLE [dbo].[tblCategories]
ADD CONSTRAINT [PK_tblCategories]
    PRIMARY KEY CLUSTERED ([CategoryID] ASC);
GO

-- Creating primary key on [ChannelID] in table 'tblChannelPages'
ALTER TABLE [dbo].[tblChannelPages]
ADD CONSTRAINT [PK_tblChannelPages]
    PRIMARY KEY CLUSTERED ([ChannelID] ASC);
GO

-- Creating primary key on [ID] in table 'tblCreateLinkPosts'
ALTER TABLE [dbo].[tblCreateLinkPosts]
ADD CONSTRAINT [PK_tblCreateLinkPosts]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblCreditDetails'
ALTER TABLE [dbo].[tblCreditDetails]
ADD CONSTRAINT [PK_tblCreditDetails]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblDataStoragePlans'
ALTER TABLE [dbo].[tblDataStoragePlans]
ADD CONSTRAINT [PK_tblDataStoragePlans]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblFavourites'
ALTER TABLE [dbo].[tblFavourites]
ADD CONSTRAINT [PK_tblFavourites]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblForgetPasswords'
ALTER TABLE [dbo].[tblForgetPasswords]
ADD CONSTRAINT [PK_tblForgetPasswords]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblInterruptedBasicAudioFiles'
ALTER TABLE [dbo].[tblInterruptedBasicAudioFiles]
ADD CONSTRAINT [PK_tblInterruptedBasicAudioFiles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblInterruptedFiles'
ALTER TABLE [dbo].[tblInterruptedFiles]
ADD CONSTRAINT [PK_tblInterruptedFiles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblMasGenClonesTypes'
ALTER TABLE [dbo].[tblMasGenClonesTypes]
ADD CONSTRAINT [PK_tblMasGenClonesTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [AccountTypeID] in table 'tblMasterAccountTypes'
ALTER TABLE [dbo].[tblMasterAccountTypes]
ADD CONSTRAINT [PK_tblMasterAccountTypes]
    PRIMARY KEY CLUSTERED ([AccountTypeID] ASC);
GO

-- Creating primary key on [AdTypeID] in table 'tblMasterAdsTypes'
ALTER TABLE [dbo].[tblMasterAdsTypes]
ADD CONSTRAINT [PK_tblMasterAdsTypes]
    PRIMARY KEY CLUSTERED ([AdTypeID] ASC);
GO

-- Creating primary key on [CatID] in table 'tblMasterCatTypes'
ALTER TABLE [dbo].[tblMasterCatTypes]
ADD CONSTRAINT [PK_tblMasterCatTypes]
    PRIMARY KEY CLUSTERED ([CatID] ASC);
GO

-- Creating primary key on [CreditsID] in table 'tblMasterCredits'
ALTER TABLE [dbo].[tblMasterCredits]
ADD CONSTRAINT [PK_tblMasterCredits]
    PRIMARY KEY CLUSTERED ([CreditsID] ASC);
GO

-- Creating primary key on [MerchantUserID] in table 'tblMerchantAccounts'
ALTER TABLE [dbo].[tblMerchantAccounts]
ADD CONSTRAINT [PK_tblMerchantAccounts]
    PRIMARY KEY CLUSTERED ([MerchantUserID] ASC);
GO

-- Creating primary key on [AddressID] in table 'tblPersonAddresses'
ALTER TABLE [dbo].[tblPersonAddresses]
ADD CONSTRAINT [PK_tblPersonAddresses]
    PRIMARY KEY CLUSTERED ([AddressID] ASC);
GO

-- Creating primary key on [Username] in table 'tblPostingStatus'
ALTER TABLE [dbo].[tblPostingStatus]
ADD CONSTRAINT [PK_tblPostingStatus]
    PRIMARY KEY CLUSTERED ([Username] ASC);
GO

-- Creating primary key on [ID] in table 'tblPremiumGeterateClones'
ALTER TABLE [dbo].[tblPremiumGeterateClones]
ADD CONSTRAINT [PK_tblPremiumGeterateClones]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblReleases'
ALTER TABLE [dbo].[tblReleases]
ADD CONSTRAINT [PK_tblReleases]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblReleaseUpdates'
ALTER TABLE [dbo].[tblReleaseUpdates]
ADD CONSTRAINT [PK_tblReleaseUpdates]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [SecurityQuestionID] in table 'tblSecurityQuestions'
ALTER TABLE [dbo].[tblSecurityQuestions]
ADD CONSTRAINT [PK_tblSecurityQuestions]
    PRIMARY KEY CLUSTERED ([SecurityQuestionID] ASC);
GO

-- Creating primary key on [ID] in table 'tblSocialNetworks'
ALTER TABLE [dbo].[tblSocialNetworks]
ADD CONSTRAINT [PK_tblSocialNetworks]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [SurveyID] in table 'tblSurveyQuestions'
ALTER TABLE [dbo].[tblSurveyQuestions]
ADD CONSTRAINT [PK_tblSurveyQuestions]
    PRIMARY KEY CLUSTERED ([SurveyID] ASC);
GO

-- Creating primary key on [ToolID] in table 'tblTools'
ALTER TABLE [dbo].[tblTools]
ADD CONSTRAINT [PK_tblTools]
    PRIMARY KEY CLUSTERED ([ToolID] ASC);
GO

-- Creating primary key on [ID] in table 'tblToolsInfoes'
ALTER TABLE [dbo].[tblToolsInfoes]
ADD CONSTRAINT [PK_tblToolsInfoes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblUserActivations'
ALTER TABLE [dbo].[tblUserActivations]
ADD CONSTRAINT [PK_tblUserActivations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblUsersSponsoreds'
ALTER TABLE [dbo].[tblUsersSponsoreds]
ADD CONSTRAINT [PK_tblUsersSponsoreds]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblVideoInterruptions'
ALTER TABLE [dbo].[tblVideoInterruptions]
ADD CONSTRAINT [PK_tblVideoInterruptions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblWaterMarkUpInterruptions'
ALTER TABLE [dbo].[tblWaterMarkUpInterruptions]
ADD CONSTRAINT [PK_tblWaterMarkUpInterruptions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tblAdmins'
ALTER TABLE [dbo].[tblAdmins]
ADD CONSTRAINT [PK_tblAdmins]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ToolID], [FileName], [UserID], [Toolcontent] in table 'tblToolFiles'
ALTER TABLE [dbo].[tblToolFiles]
ADD CONSTRAINT [PK_tblToolFiles]
    PRIMARY KEY CLUSTERED ([ToolID], [FileName], [UserID], [Toolcontent] ASC);
GO

-- Creating primary key on [UserId] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [PK_tblUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AphidLabSoftware'
ALTER TABLE [dbo].[tbl_AphidLabSoftware]
ADD CONSTRAINT [PK_tbl_AphidLabSoftware]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'tbl_AphidLabVideoUpload'
ALTER TABLE [dbo].[tbl_AphidLabVideoUpload]
ADD CONSTRAINT [PK_tbl_AphidLabVideoUpload]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserId] in table 'tblAphidlabAccounts'
ALTER TABLE [dbo].[tblAphidlabAccounts]
ADD CONSTRAINT [PK_tblAphidlabAccounts]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [AphidTiseUserID] in table 'tblAphidTiseAccounts'
ALTER TABLE [dbo].[tblAphidTiseAccounts]
ADD CONSTRAINT [PK_tblAphidTiseAccounts]
    PRIMARY KEY CLUSTERED ([AphidTiseUserID] ASC);
GO

-- Creating primary key on [PremiumUserID] in table 'tblPremiumAccounts'
ALTER TABLE [dbo].[tblPremiumAccounts]
ADD CONSTRAINT [PK_tblPremiumAccounts]
    PRIMARY KEY CLUSTERED ([PremiumUserID] ASC);
GO

-- Creating primary key on [ByterUserID] in table 'tblByterAccounts'
ALTER TABLE [dbo].[tblByterAccounts]
ADD CONSTRAINT [PK_tblByterAccounts]
    PRIMARY KEY CLUSTERED ([ByterUserID] ASC);
GO

-- Creating primary key on [id] in table 'tbl_Messages'
ALTER TABLE [dbo].[tbl_Messages]
ADD CONSTRAINT [PK_tbl_Messages]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [BasicUserID] in table 'tblBasicAccounts'
ALTER TABLE [dbo].[tblBasicAccounts]
ADD CONSTRAINT [PK_tblBasicAccounts]
    PRIMARY KEY CLUSTERED ([BasicUserID] ASC);
GO

-- Creating primary key on [FeedbackId] in table 'Feedback'
ALTER TABLE [dbo].[Feedback]
ADD CONSTRAINT [PK_Feedback]
    PRIMARY KEY CLUSTERED ([FeedbackId] ASC);
GO

-- Creating primary key on [BatchId] in table 'TrainingBatch'
ALTER TABLE [dbo].[TrainingBatch]
ADD CONSTRAINT [PK_TrainingBatch]
    PRIMARY KEY CLUSTERED ([BatchId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [BasicUserID] in table 'Demo'
ALTER TABLE [dbo].[Demo]
ADD CONSTRAINT [PK_Demo]
    PRIMARY KEY CLUSTERED ([BasicUserID] ASC);
GO

-- Creating primary key on [UserId], [TokenId] in table 'tblLoginTokens'
ALTER TABLE [dbo].[tblLoginTokens]
ADD CONSTRAINT [PK_tblLoginTokens]
    PRIMARY KEY CLUSTERED ([UserId], [TokenId] ASC);
GO

-- Creating primary key on [PremiumCode] in table 'tblPremiumCodes'
ALTER TABLE [dbo].[tblPremiumCodes]
ADD CONSTRAINT [PK_tblPremiumCodes]
    PRIMARY KEY CLUSTERED ([PremiumCode] ASC);
GO

-- Creating primary key on [AphidAccountID] in table 'tblAccountTypes'
ALTER TABLE [dbo].[tblAccountTypes]
ADD CONSTRAINT [PK_tblAccountTypes]
    PRIMARY KEY CLUSTERED ([AphidAccountID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CategoryID] in table 'tbl_SocialNetworkStatus'
ALTER TABLE [dbo].[tbl_SocialNetworkStatus]
ADD CONSTRAINT [FK_tbl_SocialNetworkStatus_tblCategory]
    FOREIGN KEY ([CategoryID])
    REFERENCES [dbo].[tblCategories]
        ([CategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbl_SocialNetworkStatus_tblCategory'
CREATE INDEX [IX_FK_tbl_SocialNetworkStatus_tblCategory]
ON [dbo].[tbl_SocialNetworkStatus]
    ([CategoryID]);
GO

-- Creating foreign key on [AdTypeID] in table 'tblAds'
ALTER TABLE [dbo].[tblAds]
ADD CONSTRAINT [FK_tblAds_tblMasterAdsType]
    FOREIGN KEY ([AdTypeID])
    REFERENCES [dbo].[tblMasterAdsTypes]
        ([AdTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAds_tblMasterAdsType'
CREATE INDEX [IX_FK_tblAds_tblMasterAdsType]
ON [dbo].[tblAds]
    ([AdTypeID]);
GO

-- Creating foreign key on [CreditsID] in table 'tblAds'
ALTER TABLE [dbo].[tblAds]
ADD CONSTRAINT [FK_tblAds_tblMasterCredits]
    FOREIGN KEY ([CreditsID])
    REFERENCES [dbo].[tblMasterCredits]
        ([CreditsID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAds_tblMasterCredits'
CREATE INDEX [IX_FK_tblAds_tblMasterCredits]
ON [dbo].[tblAds]
    ([CreditsID]);
GO

-- Creating foreign key on [SurveyID] in table 'tblAds'
ALTER TABLE [dbo].[tblAds]
ADD CONSTRAINT [FK_tblAds_tblSurveyQuestion]
    FOREIGN KEY ([SurveyID])
    REFERENCES [dbo].[tblSurveyQuestions]
        ([SurveyID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAds_tblSurveyQuestion'
CREATE INDEX [IX_FK_tblAds_tblSurveyQuestion]
ON [dbo].[tblAds]
    ([SurveyID]);
GO

-- Creating foreign key on [Category] in table 'tblSocialNetworks'
ALTER TABLE [dbo].[tblSocialNetworks]
ADD CONSTRAINT [FK_tblSocialNetwork_tblCategory]
    FOREIGN KEY ([Category])
    REFERENCES [dbo].[tblCategories]
        ([CategoryID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblSocialNetwork_tblCategory'
CREATE INDEX [IX_FK_tblSocialNetwork_tblCategory]
ON [dbo].[tblSocialNetworks]
    ([Category]);
GO

-- Creating foreign key on [AccountTypeID] in table 'tblMerchantAccounts'
ALTER TABLE [dbo].[tblMerchantAccounts]
ADD CONSTRAINT [FK_tblMerchantAccount_tblMasterAccountType]
    FOREIGN KEY ([AccountTypeID])
    REFERENCES [dbo].[tblMasterAccountTypes]
        ([AccountTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblMerchantAccount_tblMasterAccountType'
CREATE INDEX [IX_FK_tblMerchantAccount_tblMasterAccountType]
ON [dbo].[tblMerchantAccounts]
    ([AccountTypeID]);
GO

-- Creating foreign key on [AddressID] in table 'tblMerchantAccounts'
ALTER TABLE [dbo].[tblMerchantAccounts]
ADD CONSTRAINT [FK_tblMerchantAccount_tblPersonAddress]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[tblPersonAddresses]
        ([AddressID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblMerchantAccount_tblPersonAddress'
CREATE INDEX [IX_FK_tblMerchantAccount_tblPersonAddress]
ON [dbo].[tblMerchantAccounts]
    ([AddressID]);
GO

-- Creating foreign key on [SecurityQuestionID] in table 'tblMerchantAccounts'
ALTER TABLE [dbo].[tblMerchantAccounts]
ADD CONSTRAINT [FK_tblMerchantAccount_tblSecurityQuestions]
    FOREIGN KEY ([SecurityQuestionID])
    REFERENCES [dbo].[tblSecurityQuestions]
        ([SecurityQuestionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblMerchantAccount_tblSecurityQuestions'
CREATE INDEX [IX_FK_tblMerchantAccount_tblSecurityQuestions]
ON [dbo].[tblMerchantAccounts]
    ([SecurityQuestionID]);
GO

-- Creating foreign key on [ID] in table 'tblToolsInfoes'
ALTER TABLE [dbo].[tblToolsInfoes]
ADD CONSTRAINT [FK_tblToolsInfo_tblToolsInfo]
    FOREIGN KEY ([ID])
    REFERENCES [dbo].[tblToolsInfoes]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'tblAudioInterruptions'
ALTER TABLE [dbo].[tblAudioInterruptions]
ADD CONSTRAINT [FK_tblAudioInterruption_tblUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAudioInterruption_tblUsers'
CREATE INDEX [IX_FK_tblAudioInterruption_tblUsers]
ON [dbo].[tblAudioInterruptions]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'tblVideoInterruptions'
ALTER TABLE [dbo].[tblVideoInterruptions]
ADD CONSTRAINT [FK_tblVideoInterruption_tblUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblVideoInterruption_tblUsers'
CREATE INDEX [IX_FK_tblVideoInterruption_tblUsers]
ON [dbo].[tblVideoInterruptions]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'tblWaterMarkUpInterruptions'
ALTER TABLE [dbo].[tblWaterMarkUpInterruptions]
ADD CONSTRAINT [FK_tblWaterMarkUpInterruption_tblUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblWaterMarkUpInterruption_tblUsers'
CREATE INDEX [IX_FK_tblWaterMarkUpInterruption_tblUsers]
ON [dbo].[tblWaterMarkUpInterruptions]
    ([UserId]);
GO

-- Creating foreign key on [AddressId] in table 'tblAphidlabAccounts'
ALTER TABLE [dbo].[tblAphidlabAccounts]
ADD CONSTRAINT [FK_tblAphidlabAccount_tblPersonAddress]
    FOREIGN KEY ([AddressId])
    REFERENCES [dbo].[tblPersonAddresses]
        ([AddressID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAphidlabAccount_tblPersonAddress'
CREATE INDEX [IX_FK_tblAphidlabAccount_tblPersonAddress]
ON [dbo].[tblAphidlabAccounts]
    ([AddressId]);
GO

-- Creating foreign key on [SecurityQuestionID] in table 'tblAphidlabAccounts'
ALTER TABLE [dbo].[tblAphidlabAccounts]
ADD CONSTRAINT [FK_tblAphidlabAccount_tblSecurityQuestions]
    FOREIGN KEY ([SecurityQuestionID])
    REFERENCES [dbo].[tblSecurityQuestions]
        ([SecurityQuestionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAphidlabAccount_tblSecurityQuestions'
CREATE INDEX [IX_FK_tblAphidlabAccount_tblSecurityQuestions]
ON [dbo].[tblAphidlabAccounts]
    ([SecurityQuestionID]);
GO

-- Creating foreign key on [AccountTypeID] in table 'tblAphidTiseAccounts'
ALTER TABLE [dbo].[tblAphidTiseAccounts]
ADD CONSTRAINT [FK_tblAphidTiseAccount_tblMasterAccountType]
    FOREIGN KEY ([AccountTypeID])
    REFERENCES [dbo].[tblMasterAccountTypes]
        ([AccountTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAphidTiseAccount_tblMasterAccountType'
CREATE INDEX [IX_FK_tblAphidTiseAccount_tblMasterAccountType]
ON [dbo].[tblAphidTiseAccounts]
    ([AccountTypeID]);
GO

-- Creating foreign key on [AddressID] in table 'tblAphidTiseAccounts'
ALTER TABLE [dbo].[tblAphidTiseAccounts]
ADD CONSTRAINT [FK_tblAphidTiseAccount_tblPersonAddress]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[tblPersonAddresses]
        ([AddressID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAphidTiseAccount_tblPersonAddress'
CREATE INDEX [IX_FK_tblAphidTiseAccount_tblPersonAddress]
ON [dbo].[tblAphidTiseAccounts]
    ([AddressID]);
GO

-- Creating foreign key on [SecurityQuestionID] in table 'tblAphidTiseAccounts'
ALTER TABLE [dbo].[tblAphidTiseAccounts]
ADD CONSTRAINT [FK_tblAphidTiseAccount_tblSecurityQuestions]
    FOREIGN KEY ([SecurityQuestionID])
    REFERENCES [dbo].[tblSecurityQuestions]
        ([SecurityQuestionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblAphidTiseAccount_tblSecurityQuestions'
CREATE INDEX [IX_FK_tblAphidTiseAccount_tblSecurityQuestions]
ON [dbo].[tblAphidTiseAccounts]
    ([SecurityQuestionID]);
GO

-- Creating foreign key on [AudioInterruptionID] in table 'tblPremiumAccounts'
ALTER TABLE [dbo].[tblPremiumAccounts]
ADD CONSTRAINT [FK_tblPremiumAccount_tblAudioInterruption]
    FOREIGN KEY ([AudioInterruptionID])
    REFERENCES [dbo].[tblAudioInterruptions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblPremiumAccount_tblAudioInterruption'
CREATE INDEX [IX_FK_tblPremiumAccount_tblAudioInterruption]
ON [dbo].[tblPremiumAccounts]
    ([AudioInterruptionID]);
GO

-- Creating foreign key on [AccountTypeID] in table 'tblPremiumAccounts'
ALTER TABLE [dbo].[tblPremiumAccounts]
ADD CONSTRAINT [FK_tblPremiumAccount_tblMasterAccountType]
    FOREIGN KEY ([AccountTypeID])
    REFERENCES [dbo].[tblMasterAccountTypes]
        ([AccountTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblPremiumAccount_tblMasterAccountType'
CREATE INDEX [IX_FK_tblPremiumAccount_tblMasterAccountType]
ON [dbo].[tblPremiumAccounts]
    ([AccountTypeID]);
GO

-- Creating foreign key on [AddressID] in table 'tblPremiumAccounts'
ALTER TABLE [dbo].[tblPremiumAccounts]
ADD CONSTRAINT [FK_tblPremiumAccount_tblPersonAddress]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[tblPersonAddresses]
        ([AddressID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblPremiumAccount_tblPersonAddress'
CREATE INDEX [IX_FK_tblPremiumAccount_tblPersonAddress]
ON [dbo].[tblPremiumAccounts]
    ([AddressID]);
GO

-- Creating foreign key on [SecurityQuestionID] in table 'tblPremiumAccounts'
ALTER TABLE [dbo].[tblPremiumAccounts]
ADD CONSTRAINT [FK_tblPremiumAccount_tblSecurityQuestions]
    FOREIGN KEY ([SecurityQuestionID])
    REFERENCES [dbo].[tblSecurityQuestions]
        ([SecurityQuestionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblPremiumAccount_tblSecurityQuestions'
CREATE INDEX [IX_FK_tblPremiumAccount_tblSecurityQuestions]
ON [dbo].[tblPremiumAccounts]
    ([SecurityQuestionID]);
GO

-- Creating foreign key on [WatermarksInterruptionID] in table 'tblPremiumAccounts'
ALTER TABLE [dbo].[tblPremiumAccounts]
ADD CONSTRAINT [FK_tblPremiumAccount_tblWaterMarkUpInterruption]
    FOREIGN KEY ([WatermarksInterruptionID])
    REFERENCES [dbo].[tblWaterMarkUpInterruptions]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblPremiumAccount_tblWaterMarkUpInterruption'
CREATE INDEX [IX_FK_tblPremiumAccount_tblWaterMarkUpInterruption]
ON [dbo].[tblPremiumAccounts]
    ([WatermarksInterruptionID]);
GO

-- Creating foreign key on [AccountTypeID] in table 'tblByterAccounts'
ALTER TABLE [dbo].[tblByterAccounts]
ADD CONSTRAINT [FK_tblByterAccount_tblMasterAccountType]
    FOREIGN KEY ([AccountTypeID])
    REFERENCES [dbo].[tblMasterAccountTypes]
        ([AccountTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblByterAccount_tblMasterAccountType'
CREATE INDEX [IX_FK_tblByterAccount_tblMasterAccountType]
ON [dbo].[tblByterAccounts]
    ([AccountTypeID]);
GO

-- Creating foreign key on [AddressID] in table 'tblByterAccounts'
ALTER TABLE [dbo].[tblByterAccounts]
ADD CONSTRAINT [FK_tblByterAccount_tblPersonAddress]
    FOREIGN KEY ([AddressID])
    REFERENCES [dbo].[tblPersonAddresses]
        ([AddressID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblByterAccount_tblPersonAddress'
CREATE INDEX [IX_FK_tblByterAccount_tblPersonAddress]
ON [dbo].[tblByterAccounts]
    ([AddressID]);
GO

-- Creating foreign key on [SecurityQuestionID] in table 'tblByterAccounts'
ALTER TABLE [dbo].[tblByterAccounts]
ADD CONSTRAINT [FK_tblByterAccount_tblSecurityQuestions]
    FOREIGN KEY ([SecurityQuestionID])
    REFERENCES [dbo].[tblSecurityQuestions]
        ([SecurityQuestionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblByterAccount_tblSecurityQuestions'
CREATE INDEX [IX_FK_tblByterAccount_tblSecurityQuestions]
ON [dbo].[tblByterAccounts]
    ([SecurityQuestionID]);
GO

-- Creating foreign key on [SecurityQuestionID] in table 'tblBasicAccounts'
ALTER TABLE [dbo].[tblBasicAccounts]
ADD CONSTRAINT [FK_tblBasicAccount_tblSecurityQuestions]
    FOREIGN KEY ([SecurityQuestionID])
    REFERENCES [dbo].[tblSecurityQuestions]
        ([SecurityQuestionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblBasicAccount_tblSecurityQuestions'
CREATE INDEX [IX_FK_tblBasicAccount_tblSecurityQuestions]
ON [dbo].[tblBasicAccounts]
    ([SecurityQuestionID]);
GO

-- Creating foreign key on [UserId], [TokenId] in table 'tblLoginTokens'
ALTER TABLE [dbo].[tblLoginTokens]
ADD CONSTRAINT [FK_tblLoginTokens_tblLoginTokens]
    FOREIGN KEY ([UserId], [TokenId])
    REFERENCES [dbo].[tblLoginTokens]
        ([UserId], [TokenId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AccountTypeId] in table 'tblLoginTokens'
ALTER TABLE [dbo].[tblLoginTokens]
ADD CONSTRAINT [FK_tblLoginTokens_tblMasterAccountType]
    FOREIGN KEY ([AccountTypeId])
    REFERENCES [dbo].[tblMasterAccountTypes]
        ([AccountTypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblLoginTokens_tblMasterAccountType'
CREATE INDEX [IX_FK_tblLoginTokens_tblMasterAccountType]
ON [dbo].[tblLoginTokens]
    ([AccountTypeId]);
GO

-- Creating foreign key on [UserId] in table 'tblLoginTokens'
ALTER TABLE [dbo].[tblLoginTokens]
ADD CONSTRAINT [FK_tblLoginTokens_tblUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserId] in table 'tblUsers'
ALTER TABLE [dbo].[tblUsers]
ADD CONSTRAINT [FK_tblUsers_tblUsers]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[tblUsers]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------