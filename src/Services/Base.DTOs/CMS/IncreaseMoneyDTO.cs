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
    public class IncreaseMoneyDTO : BaseDTO
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
        /// พนักงานขาย
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs={projectID}
        /// </summary>
        [Description("พนักงานขาย")]
        public USR.UserListDTO SaleUser { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        public static IncreaseMoneyDTO CreateFromModel(IncreaseMoney model)
        {
            if (model != null)
            {
                var result = new IncreaseMoneyDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    Amount = model.Amount,
                    Remark = model.Remark,
                    ActiveDate = model.ActiveDate
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static IncreaseMoneyDTO CreateFromQueryResult(IncreaseMoneyQueryResult model)
        {
            if (model != null)
            {
                var result = new IncreaseMoneyDTO()
                {
                    Id = model.IncreaseMoney.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    Amount = model.IncreaseMoney.Amount,
                    Remark = model.IncreaseMoney.Remark,
                    ActiveDate = model.IncreaseMoney.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(IncreaseMoneySortByParam sortByParam, ref IQueryable<IncreaseMoneyQueryResult> query)
        {
            IOrderedQueryable<IncreaseMoneyQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case IncreaseMoneySortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.ActiveDate);
                        break;
                    case IncreaseMoneySortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.ProjectID);
                        break;
                    case IncreaseMoneySortBy.SaleUserID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.SaleUserID);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.SaleUserID);
                        break;
                    case IncreaseMoneySortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.Amount);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.Amount);
                        break;
                    case IncreaseMoneySortBy.Remark:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.Remark);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.Remark);
                        break;                   
                    default:
                        orderQuery = query.OrderBy(o => o.IncreaseMoney.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.IncreaseMoney.ActiveDate);
            }

            orderQuery.ThenBy(o => o.IncreaseMoney.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseMoneyDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseMoneyDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.SaleUser == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseMoneyDTO.SaleUser)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseMoneyDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref IncreaseMoney model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.SaleUserID = this.SaleUser.Id;
            model.Amount = this.Amount;
            model.Remark = this.Remark;
        }
    }

    public class IncreaseMoneyQueryResult
    {
        public IncreaseMoney IncreaseMoney { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User SaleUser { get; set; }
    }
}
