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
    public class RateSettingFixSaleDTO : BaseDTO
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

        public static RateSettingFixSaleDTO CreateFromModel(RateSettingFixSale model)
        {
            if (model != null)
            {
                var result = new RateSettingFixSaleDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Amount = model.Amount,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CreateDate = model.Created,
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

        public static RateSettingFixSaleDTO CreateFromQueryResult(RateSettingFixSaleQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingFixSaleDTO()
                {
                    Id = model.RateSettingFixSale.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Amount = model.RateSettingFixSale.Amount,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.RateSettingFixSale.CreatedBy),
                    CreateDate = model.RateSettingFixSale.Created,
                    IsActive = model.RateSettingFixSale.IsActive,
                    ActiveDate = model.RateSettingFixSale.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(RateSettingFixSaleSortByParam sortByParam, ref IQueryable<RateSettingFixSaleQueryResult> query)
        {
            IOrderedQueryable<RateSettingFixSaleQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingFixSaleSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSale.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSale.ActiveDate);
                        break;
                    case RateSettingFixSaleSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSale.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSale.ProjectID);
                        break;
                    case RateSettingFixSaleSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSale.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSale.Amount);
                        break;
                    case RateSettingFixSaleSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingFixSale.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingFixSale.IsActive);
                        break;                   
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingFixSale.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingFixSale.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingFixSale.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingFixSaleDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref RateSettingFixSale model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }
    }

    public class RateSettingFixSaleQueryResult
    {
        public RateSettingFixSale RateSettingFixSale { get; set; }
        public models.PRJ.Project Project { get; set; }
    }
}
