using System;
using System.Collections.Generic;
using System.Linq;
using Database.Models.DbQueries.PRJ;
using Dapper.Contrib.Extensions;

namespace Database.Models.PRJ
{ 
    [Table("USR.vw_LCUserDetail")]
    public partial class USR_vwLCUserDetail
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string EmpCode { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string NickName { get; set; }
        public string Mobile { get; set; }
    }
     [Table("USR.vw_LCMUserDetail")]
    public partial class USR_vwLCMUserDetail
    {
        public Guid? UserID { get; set; }
        public string UserName { get; set; }
        public string EmpCode { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string TitleNameEng { get; set; }
        public string FirstNameEng { get; set; }
        public string LastNameEng { get; set; }
        public string FullNameEng { get; set; }
        public string NickName { get; set; }
        public string Mobile { get; set; }
    }
    [Table("USR.vw_PMUser")]
    public partial class USR_vwPMUser
    {
        public Guid ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string BG { get; set; }
        public string SubBG { get; set; }
        public string Department { get; set; }
        public string UserGUID { get; set; }
        public string EmpCode { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string NickName { get; set; }
        public string Mobile { get; set; }
    }
   
    public class DropdownListModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string FullName { get; set; }
        public string Key { get; set; }
        public int Order { get; set; }
    }
    public class UpdateProjectInfoModel
    {
        public Guid ProjectID { get; set; }
        public string AdminDescription { get; set; }

        //public string Input { get; set; }
        //public string Input { get; set; }
    }
    public class ProjectInformationModel
    {
        public class ProjectDetail
        {
            public string ProjectID { get; set; }
            public string ProjectNo { get; set; }
            public string ProjectName { get; set; }
            public string ProjectLocation { get; set; }
            public string ThumbnailURL { get; set; }
            public string ProjectShortDescription { get; set; }
            public string ProjectType { get; set; }
            public string RoomType { get; set; }
            public string TotalUnit { get; set; }
            public DateTime? StartSaleDate { get; set; }
            public string ProjectStatus { get; set; }
            public string GoogleMapUrl { get; set; }
            public string ProjectAddress { get; set; }
            public string SellingPriceDisplay { get; set; }
            public string AdminDescription { get; set; }
            public string PhoneNumber { get; set; }

            public List<ProjectUser> PMUser { get; set; } = new List<ProjectUser>();
            public List<ProjectUser> LCMUser { get; set; } = new List<ProjectUser>();
            public List<ProjectUser> LCUser { get; set; } = new List<ProjectUser>();

            public ProjectInfoDetail projectInfoDetail = new ProjectInfoDetail();
        }

        public class ProjectUser
        {
            public string EmpCode { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string NickName { get; set; }
            public string MobilePhone { get; set; }

        }

        public class HighlightLocation
        {
            public string IconKey { get; set; }
            public string IconURL { get; set; }
            public string Description { get; set; }
        }

        public class Promotion
        {
            public DateTime? StartDate { get; set; }
            public string PromotionDate { get; set; }
            public string PromotionDescription { get; set; }
        }

        public class Campaign
        {
            public DateTime? StartDate { get; set; }
            public string CampaignDate { get; set; }
            public string CampaignWebUrl { get; set; }
            public string CampaignImageUrl { get; set; }
            public string CampaignDescription { get; set; }
        }

        public class ResultProjectInformation
        {
            public ProjectDetail Project { get; set; } = new ProjectDetail();

            public List<HighlightLocation> HighlightLocations { get; set; } = new List<HighlightLocation>();
            public List<Promotion> Promotions { get; set; } = new List<Promotion>();
            public List<Campaign> Campaigns { get; set; } = new List<Campaign>();
        }

        [Table("PRJ.ProjectInfoPromotion")]
        public partial class PRJ_ProjectInfoPromotion
        {
            [ExplicitKey]
            public Guid ID { get; set; }
            public int? RefID { get; set; }
            public Guid ProjectID { get; set; }
            public string ProjectNo { get; set; }
            public string LanguageType { get; set; }
            public string Description { get; set; }
            public DateTime? StartedDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool IsDeleted { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? Updated { get; set; }
            public Guid? CreatedByUserID { get; set; }
            public Guid? UpdatedByUserID { get; set; }
        }
        [Table("PRJ.ProjectInfoCampaign")]
        public partial class PRJ_ProjectInfoCampaign
        {
            [ExplicitKey]
            public Guid ID { get; set; }
            public string CampiangID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string LinkUrl { get; set; }
            public string ImageUrl { get; set; }
            public string BUType { get; set; }
            public string CampaignType { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool IsActive { get; set; }
            public bool IsDeleted { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? Updated { get; set; }
            public Guid? CreatedByUserID { get; set; }
            public Guid? UpdatedByUserID { get; set; }
        }


        public static ResultProjectInformation ToModel(
            sql_ProjectInformation.Result Info
            , sql_ProjectInfoDetail.Result InfoDetail
            , List<USR_vwPMUser> PMUser
            , List<USR_vwLCMUserDetail> LCMUser
            , List<USR_vwLCUserDetail> LCUser
            , List<sql_ProjectInforLocationDetail.Result> LocationList
            , List<PRJ_ProjectInfoPromotion> PromotionList
            , List<PRJ_ProjectInfoCampaign> CampaignList)
        {
            var Result = new ResultProjectInformation();

            if (InfoDetail != null)
            {
                Result.Project.projectInfoDetail.ID = InfoDetail.ID;
                Result.Project.projectInfoDetail.ProjectID = InfoDetail.ProjectID;
                Result.Project.projectInfoDetail.AdminDescription = InfoDetail.AdminDescription;
                Result.Project.projectInfoDetail.IsDeleted = InfoDetail.IsDeleted;
                Result.Project.projectInfoDetail.Created = InfoDetail.Created;
                Result.Project.projectInfoDetail.Updated = InfoDetail.Updated;
                Result.Project.projectInfoDetail.CreatedByUserID = InfoDetail.CreatedByUserID;
                Result.Project.projectInfoDetail.UpdatedByUserID = InfoDetail.UpdatedByUserID;
            }

            if (Info != null)
            {
                Result.Project.ProjectNo = Info.ProjectNo;
                Result.Project.ProjectName = Info.ProjectNameTH;
                Result.Project.ProjectLocation = Info.ProjectLocation;
                Result.Project.ThumbnailURL = Info.ThumbnailURL;
                Result.Project.ProjectShortDescription = Info.ShortDescription;
                Result.Project.ProjectType = Info.ProjectType;
                Result.Project.RoomType = Info.UnitTypeDescription;
                Result.Project.TotalUnit = Info.TotalUnit;
                Result.Project.StartSaleDate = Info.ProjectStartDate;
                Result.Project.ProjectStatus = Info.ProjectStatus;
                Result.Project.SellingPriceDisplay = Info.SellingPriceDisplay;
                Result.Project.AdminDescription = Info.AdminDescription;
                Result.Project.GoogleMapUrl = Info.MapImageUrl;
                Result.Project.ProjectAddress = Info.AddressDisplay;
                Result.Project.PhoneNumber = Info.PhoneNumber;




                if (PMUser.Any())
                    Result.Project.PMUser = PMUser.Select(o => new ProjectUser { EmpCode = o.EmpCode, FirstName = o.FirstName, LastName = o.LastName, NickName = o.NickName, MobilePhone = o.Mobile }).OrderBy(o => o.FirstName).ToList();

                if (LCMUser.Any())
                    Result.Project.LCMUser = LCMUser.Select(o => new ProjectUser { EmpCode = o.EmpCode, FirstName = o.FirstName, LastName = o.LastName, NickName = o.NickName, MobilePhone = o.Mobile }).OrderBy(o => o.FirstName).ToList();

                if (LCUser.Any())
                    Result.Project.LCUser = LCUser.Select(o => new ProjectUser { EmpCode = o.EmpCode, FirstName = o.FirstName, LastName = o.LastName, NickName = o.NickName, MobilePhone = o.Mobile }).OrderBy(o => o.FirstName).ToList();

                if (LocationList.Any())
                    Result.HighlightLocations = LocationList.Select(o => new HighlightLocation { IconKey = o.IconKey, IconURL = o.IconURL, Description = o.Description }).OrderBy(o => o.IconKey).ThenBy(o => o.Description).ToList();

                if (PromotionList.Any())
                    Result.Promotions = PromotionList.Select(o => new Promotion { StartDate = o.StartedDate, PromotionDate = (o.StartedDate ?? DateTime.Now).ToString("MMM yyyy"), PromotionDescription = o.Description }).OrderByDescending(o => o.StartDate).ToList();

                if (CampaignList.Any())
                    Result.Campaigns = CampaignList.Select(o => new Campaign { StartDate = o.StartDate, CampaignDate = (o.StartDate ?? DateTime.Now).ToString("MMM yyyy"), CampaignDescription = o.Description, CampaignWebUrl = o.LinkUrl, CampaignImageUrl = o.ImageUrl }).OrderByDescending(o => o.StartDate).ToList();
            }

            return Result;
        }

    }
}
