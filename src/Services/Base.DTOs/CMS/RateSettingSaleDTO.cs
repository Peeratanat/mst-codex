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
    public class RateSettingSaleDTO : BaseDTO
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
        /// จำนวนเงินเริ่มต้น
        /// </summary>
        [Description("จำนวนเงินเริ่มต้น")]
        public decimal StartRange { get; set; }

        /// <summary>
        /// จำนวนเงินสิ้นสุด
        /// </summary>
        [Description("จำนวนเงินสิ้นสุด")]
        public decimal EndRange { get; set; }

        /// <summary>
        /// จำนวนเปอร์เซ็น
        /// </summary>
        [Description("จำนวนเปอร์เซ็น")]
        public double Amount { get; set; }

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

        public static RateSettingSaleDTO CreateFromModel(RateSettingSale model)
        {
            if (model != null)
            {
                var result = new RateSettingSaleDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    StartRange = model.StartRange,
                    EndRange = model.EndRange,
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

        public static RateSettingSaleDTO CreateFromQueryResult(RateSettingSaleQueryResult model)
        {
            if (model != null)
            {
                var result = new RateSettingSaleDTO()
                {
                    Id = model.RateSettingSale.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    StartRange = model.RateSettingSale.StartRange,
                    EndRange = model.RateSettingSale.EndRange,
                    Amount = model.RateSettingSale.Amount,
                    CreatedByUser = USR.UserListDTO.CreateFromModel(model.RateSettingSale.CreatedBy),
                    CreateDate = model.RateSettingSale.Created,
                    IsActive = model.RateSettingSale.IsActive,
                    ActiveDate = model.RateSettingSale.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(RateSettingSaleSortByParam sortByParam, ref IQueryable<RateSettingSaleQueryResult> query)
        {
            IOrderedQueryable<RateSettingSaleQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case RateSettingSaleSortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.ActiveDate);
                        break;
                    case RateSettingSaleSortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.ProjectID);
                        break;
                    case RateSettingSaleSortBy.StartRange:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.StartRange);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.StartRange);
                        break;
                    case RateSettingSaleSortBy.EndRange:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.EndRange);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.EndRange);
                        break;
                    case RateSettingSaleSortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.Amount);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.Amount);
                        break;
                    case RateSettingSaleSortBy.IsActive:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.RateSettingSale.IsActive);
                        else orderQuery = query.OrderByDescending(o => o.RateSettingSale.IsActive);
                        break;                   
                    default:
                        orderQuery = query.OrderBy(o => o.RateSettingSale.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.RateSettingSale.ActiveDate);
            }

            orderQuery.ThenBy(o => o.RateSettingSale.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.StartRange == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleDTO.StartRange)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.EndRange == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleDTO.EndRange)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == 0)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(RateSettingSaleDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref RateSettingSale model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.StartRange = this.StartRange;
            model.EndRange = this.EndRange;
            model.Amount = this.Amount;
            model.IsActive = this.IsActive;
        }
    }

    public class RateSettingSaleQueryResult
    {
        public RateSettingSale RateSettingSale { get; set; }
        public models.PRJ.Project Project { get; set; }
        public RateSale RateSale { get; set; }
    }
}
