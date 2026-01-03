using Database.Models;
using Database.Models.CMS;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CMS
{
    public class GeneralSettingDTO : BaseDTO
    {
        /// <summary>
        /// Effective Month
        /// </summary>
        [Description("Effective Month")]
        public DateTime? ActiveDate { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// เมื่อ Launch โครงการจะได้รับเงิน
        /// </summary>
        [Description("เมื่อ Launch โครงการจะได้รับเงิน")]
        public decimal AfterLaunchAmount { get; set; }

        /// <summary>
        /// โครงการตั้งแต่วันที่
        /// </summary>
        [Description("โครงการตั้งแต่วันที่")]
        public DateTime? LaunchStartDate { get; set; }

        /// <summary>
        /// โครงการสิ้นสุดวันที่
        /// </summary>
        [Description("โครงการสิ้นสุดวันที่")]
        public DateTime? LaunchEndDate { get; set; }

        /// <summary>
        /// ผู้สร้าง
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("ผู้สร้าง")]
        public USR.UserListDTO CreatedByUser { get; set; }

        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        [Description("วันที่สร้าง")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public bool IsActive { get; set; }

        /// <summary>
        /// List of ProjectID สำหรับบันทึกข้อมูล
        /// </summary>
        [Description("List of ProjectID สำหรับบันทึกข้อมูล")]
        public List<Guid> ListProjectId { get; set; }

        public static GeneralSettingDTO CreateFromModel(GeneralSetting model)
        {
            if (model != null)
            {
                var result = new GeneralSettingDTO()
                {
                    Id = model.ID,
                    ActiveDate = model.ActiveDate,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    AfterLaunchAmount = model.AfterLaunchAmount,
                    LaunchStartDate = model.LaunchStartDate,
                    LaunchEndDate = model.LaunchEndDate,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CreateDate = model.Created,
                    IsActive = model.IsActive
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static GeneralSettingDTO CreateFromQueryResult(GeneralSettingQueryResult model)
        {
            if (model != null)
            {
                var result = new GeneralSettingDTO()
                {
                    Id = model.GeneralSetting.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    AfterLaunchAmount = model.GeneralSetting.AfterLaunchAmount,
                    LaunchStartDate = model.GeneralSetting.LaunchStartDate,
                    LaunchEndDate = model.GeneralSetting.LaunchEndDate,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.GeneralSetting.CreatedBy),
                    CreateDate = model.GeneralSetting.Created,
                    IsActive = model.GeneralSetting.IsActive,
                    ActiveDate = model.GeneralSetting.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(GeneralSettingSortByParam sortByParam, ref IQueryable<GeneralSettingQueryResult> query)
        {
            IOrderedQueryable<GeneralSettingQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case GeneralSettingSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderByDescending(x=>x.GeneralSetting.IsActive).ThenBy(o => o.GeneralSetting.ActiveDate);
                        else orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.ActiveDate);
                        break;
                    case GeneralSettingSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenBy(o => o.GeneralSetting.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.Project.ProjectNo);
                        break;
                    case GeneralSettingSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.GeneralSetting.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.GeneralSetting.IsActive);
                        break;
                    case GeneralSettingSortBy.CreatedByName:
                        if (sortByParam.Ascending) orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenBy(o => o.GeneralSetting.CreatedBy.DisplayName);
                        else orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.CreatedBy.DisplayName);
                        break;
                    case GeneralSettingSortBy.CreateDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenBy(o => o.GeneralSetting.Created);
                        else orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.Created);
                        break;
                    case GeneralSettingSortBy.LaunchStartDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenBy(o => o.GeneralSetting.LaunchStartDate);
                        else orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.LaunchStartDate);
                        break;
                    case GeneralSettingSortBy.LaunchEndDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenBy(o => o.GeneralSetting.LaunchEndDate);
                        else orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.LaunchEndDate);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(x => x.GeneralSetting.IsActive).ThenByDescending(o => o.GeneralSetting.ActiveDate);
            }

            orderQuery.ThenBy(o => o.GeneralSetting.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(GeneralSettingDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(GeneralSettingDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.AfterLaunchAmount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(GeneralSettingDTO.AfterLaunchAmount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.LaunchStartDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(GeneralSettingDTO.LaunchStartDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.LaunchEndDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(GeneralSettingDTO.LaunchEndDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref GeneralSetting model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.AfterLaunchAmount = this.AfterLaunchAmount;
            model.LaunchStartDate = this.LaunchStartDate;
            model.LaunchEndDate = this.LaunchEndDate;
            model.IsActive = this.IsActive;
        }
    }

    public class GeneralSettingQueryResult
    {
        public GeneralSetting GeneralSetting { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User CreatedByUser { get; set; }
    }
}
