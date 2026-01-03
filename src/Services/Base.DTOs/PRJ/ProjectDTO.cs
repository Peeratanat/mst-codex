using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class ProjectDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        [Description("รหัสโครงการ")]
        public string ProjectNo { get; set; }
        /// <summary>
        /// ชื่อโครงการ (TH)
        /// </summary>
        [Description("ชื่อโครงการ (TH)")]
        public string ProjectNameTH { get; set; }
        /// <summary>
        /// ชื่อโครงการ (EN)
        /// </summary>
        [Description("ชื่อโครงการ (EN)")]
        public string ProjectNameEN { get; set; }

        /// <summary>
        /// แบรนด์
        /// Master/api/Brands/DropdownList
        /// </summary>
        public MST.BrandDropdownDTO Brand { get; set; }
        /// <summary>
        /// บริษัท
        /// Master/api/Companies/DropdownList
        /// </summary>
        public MST.CompanyDropdownDTO Company { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProductType
        /// ประเภทของโครงการ  (แนวราบ, แนวสูง)
        /// </summary>
        [Description("ประเภทของโครงการ  (แนวราบ, แนวสูง)")]
        public MST.MasterCenterDropdownDTO ProductType { get; set; }
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectStatus
        /// สถานะโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ProjectStatus { get; set; }
        /// <summary>
        /// สถานะ Active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// SapCode
        /// </summary>
        [Description("SapCode")]
        public string SapCode { get; set; }
        /// <summary>
        /// Plant
        /// </summary>
        public string Plant { get; set; }

        /// <summary>
        /// จำนวน Unit
        /// </summary>
        public ProjectUnitCountDTO UnitCount { get; set; }

        /// <summary>
        /// IsForeignProject
        /// </summary>
        public bool? IsForeignProject { get; set; }

        public Guid? TitledeedConfigID { get; set; }
        public int? NumberOfDays { get; set; }
        public bool? IsFullDay { get; set; }
        public int? CompNumberOfDays { get; set; }
        public bool? IsCompFullDay { get; set; }
        public int? ShiftNumberOfDays { get; set; }
        public bool? IsShiftFullDay { get; set; }
        public DateTime? TitledeedConfigUpdated { get; set; }
        public string TitledeedConfigUpdatedBy { get; set; }
        public string TitledeedConfigRemark { get; set; }
        public string URLMasterPlan { get; set; }
        public string CMSMasterPlan { get; set; }
        public BGDropdownDTO BG { get; set; }
        public bool IsEventUse { get; set; }

        public static ProjectDTO CreateFromModel(Project model)
        {
            if (model != null)
            {
                var result = new ProjectDTO()
                {
                    Id = model.ID,
                    ProjectNo = model.ProjectNo,
                    ProjectNameTH = model.ProjectNameTH,
                    ProjectNameEN = model.ProjectNameEN,
                    SapCode = model.SapCode,
                    Plant = model.Plant,
                    Brand = MST.BrandDropdownDTO.CreateFromModel(model.Brand),
                    Company = MST.CompanyDropdownDTO.CreateFromModel(model.Company),
                    ProductType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    ProjectStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectStatus),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    IsActive = model.IsActive,
                    IsForeignProject = model.IsForeignProject,
                    BG = BGDropdownDTO.CreateFromModel(model.BG)


                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static ProjectDTO CreateFromQueryResult(ProjectQueryResult model)
        {
            if (model != null)
            {

                var result = new ProjectDTO()
                {
                    Id = model.Project.ID,
                    ProjectNo = model.Project.ProjectNo,
                    ProjectNameTH = model.Project.ProjectNameTH,
                    ProjectNameEN = model.Project.ProjectNameEN,
                    SapCode = model.Project.SapCode,
                    Plant = model.Project.Plant,
                    Brand = MST.BrandDropdownDTO.CreateFromModel(model.Brand),
                    Company = MST.CompanyDropdownDTO.CreateFromModel(model.Company),
                    ProductType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    ProjectStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectStatus),
                    Updated = model.Project.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    IsActive = model.Project.IsActive,
                    IsForeignProject = model.Project.IsForeignProject,
                    TitledeedConfigID = model.TitledeedConfig == null ? Guid.Parse("00000000-0000-0000-0000-000000000000") : model.TitledeedConfig.ID,
                    NumberOfDays = model.TitledeedConfig == null ? 0 : model.TitledeedConfig.NumberOfDays,
                    IsFullDay = model.TitledeedConfig == null ? null : model.TitledeedConfig.IsFullDay,
                    CompNumberOfDays = model.TitledeedConfig == null ? 0 : model.TitledeedConfig.CompNumberOfDays,
                    IsCompFullDay = model.TitledeedConfig == null ? null : model.TitledeedConfig.IsCompFullDay,

                    ShiftNumberOfDays = model.TitledeedConfig == null ? 0 : model.TitledeedConfig.ShiftNumberOfDays,
                    IsShiftFullDay = model.TitledeedConfig == null ? null : model.TitledeedConfig.IsShiftFullDay,

                    TitledeedConfigUpdated = model.TitledeedConfig == null ? null : model.TitledeedConfig.Updated,
                    TitledeedConfigUpdatedBy = model.TitledeedConfig == null ? null : model.titledeedConfigUpdatedBy.DisplayName,
                    TitledeedConfigRemark = model.TitledeedConfig == null ? null : model.TitledeedConfig.Remark,
                    URLMasterPlan = model.Project.MasterPlanFilePath == null ? null : model.Project.MasterPlanFilePath
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(ProjectSortByParam sortByParam, ref IQueryable<ProjectQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case ProjectSortBy.ProjectNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case ProjectSortBy.ProjectNameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNameTH);
                        else query = query.OrderByDescending(o => o.Project.ProjectNameTH);
                        break;
                    case ProjectSortBy.ProjectNameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNameEN);
                        else query = query.OrderByDescending(o => o.Project.ProjectNameEN);
                        break;
                    case ProjectSortBy.Brand:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Brand.Name);
                        else query = query.OrderByDescending(o => o.Brand.Name);
                        break;
                    case ProjectSortBy.Company:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.NameTH);
                        else query = query.OrderByDescending(o => o.Company.NameTH);
                        break;
                    case ProjectSortBy.ProductType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ProductType.Name);
                        else query = query.OrderByDescending(o => o.ProductType.Name);
                        break;
                    case ProjectSortBy.ProjectStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.ProjectStatus.Name);
                        else query = query.OrderByDescending(o => o.ProjectStatus.Name);
                        break;
                    case ProjectSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.Updated);
                        else query = query.OrderByDescending(o => o.Project.Updated);
                        break;
                    case ProjectSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case ProjectSortBy.IsActive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.IsActive);
                        else query = query.OrderByDescending(o => o.Project.IsActive);
                        break;
                    default:
                        query = query.OrderBy(o => o.Project.ProjectNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Project.ProjectNo);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.ProjectNo))
            {
                var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0006");
                string desc = this.GetType().GetProperty(nameof(ProjectDTO.ProjectNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.ProjectNo.CheckLang(false, true, false, true, 10, "-/"))
                {
                    var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0036");
                    string desc = this.GetType().GetProperty(nameof(ProjectDTO.ProjectNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueProjectNo = this.Id != (Guid?)null
                    ? db.Projects.Any(o => o.ProjectNo == this.ProjectNo && o.ID != this.Id)
                    : db.Projects.Any(o => o.ProjectNo == this.ProjectNo);
                if (checkUniqueProjectNo)
                {
                    var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0042");
                    string desc = this.GetType().GetProperty(nameof(ProjectDTO.ProjectNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.ProjectNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (string.IsNullOrEmpty(this.SapCode))
            {
                var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0006");
                string desc = this.GetType().GetProperty(nameof(ProjectDTO.SapCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.SapCode.CheckLang(false, true, false, true, 10, "-/"))
                {
                    var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0036");
                    string desc = this.GetType().GetProperty(nameof(ProjectDTO.SapCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            var checkUniqueSabCode = this.Id != (Guid?)null
                ? db.Projects.Any(o => o.SapCode == this.SapCode && o.ID != this.Id)
                : db.Projects.Any(o => o.SapCode == this.SapCode);
            if (checkUniqueSabCode)
            {
                var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0042");
                string desc = this.GetType().GetProperty(nameof(ProjectDTO.SapCode)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.SapCode);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (string.IsNullOrEmpty(this.ProjectNameTH))
            {
                var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0006");
                string desc = this.GetType().GetProperty(nameof(ProjectDTO.ProjectNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!string.IsNullOrEmpty(this.ProjectNameEN))
            {
                if (!this.ProjectNameEN.CheckLang(false, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0036");
                    string desc = this.GetType().GetProperty(nameof(ProjectDTO.ProjectNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (this.ProductType == null)
            {
                var errMsg = await db.ErrorMessages.FirstAsync(o => o.Key == "ERR0006");
                string desc = this.GetType().GetProperty(nameof(ProjectDTO.ProductType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref Project model)
        {
            model.ProjectNo = this.ProjectNo;
            model.ProjectNameTH = this.ProjectNameTH;
            model.SapCode = this.SapCode;
            model.ProductTypeMasterCenterID = this.ProductType?.Id;
        }
    }

    public class ProjectQueryResult
    {
        public Project Project { get; set; }
        public Brand Brand { get; set; }
        public Company Company { get; set; }
        public MasterCenter ProductType { get; set; }
        public MasterCenter ProjectStatus { get; set; }
        public User UpdatedBy { get; set; }
        public TitledeedConfig TitledeedConfig { get; set; }
        public User titledeedConfigUpdatedBy { get; set; }
    }
}
