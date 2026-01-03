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
    public class RateSettingFixTransferDTO : BaseDTO
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
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal Amount { get; set; }

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

        public static RateSettingFixTransferDTO CreateFromModel(RateSettingFixTransfer model)
        {
            if (model != null)
            {
                var result = new RateSettingFixTransferDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Amount = model.Amount,
                    IsActive = model.IsActive,
                    ActiveDate = model.ActiveDate
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static RateSettingFixTransferDTO CreateFromQueryResult(RateSettingFixTransferQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingFixTransferDTO()
                {
                    Id = model.RateSettingFixTransfer.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Amount = model.RateSettingFixTransfer.Amount,
                    IsActive = model.RateSettingFixTransfer.IsActive,
                    ActiveDate = model.RateSettingFixTransfer.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(RateSettingFixTransferSortByParam sortByParam, ref IQueryable<RateSettingFixTransferQueryResult> query)
        {
            IOrderedQueryable<RateSettingFixTransferQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingFixTransferSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransfer.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransfer.ActiveDate);
                        break;
                    case RateSettingFixTransferSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransfer.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransfer.ProjectID);
                        break;
                    case RateSettingFixTransferSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransfer.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransfer.Amount);
                        break;
                    case RateSettingFixTransferSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixTransfer.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixTransfer.IsActive);
                        break;                   
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingFixTransfer.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingFixTransfer.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingFixTransfer.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixTransferDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixTransferDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixTransferDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref RateSettingFixTransfer model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }
    }

    public class RateSettingFixTransferQueryResult
    {
        public RateSettingFixTransfer RateSettingFixTransfer { get; set; }
        public models.PRJ.Project Project { get; set; }
    }
}
