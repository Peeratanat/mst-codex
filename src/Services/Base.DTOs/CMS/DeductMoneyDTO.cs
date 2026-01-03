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
    public class DeductMoneyDTO : BaseDTO
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

        public static DeductMoneyDTO CreateFromModel(DeductMoney model)
        {
            if (model != null)
            {
                var result = new DeductMoneyDTO()
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

        public static DeductMoneyDTO CreateFromQueryResult(DeductMoneyQueryResult model)
        {
            if (model != null)
            {
                var result = new DeductMoneyDTO()
                {
                    Id = model.DeductMoney.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    Amount = model.DeductMoney.Amount,
                    Remark = model.DeductMoney.Remark,
                    ActiveDate = model.DeductMoney.ActiveDate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(DeductMoneySortByParam sortByParam, ref IQueryable<DeductMoneyQueryResult> query)
        {
            IOrderedQueryable<DeductMoneyQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case DeductMoneySortBy.ActiveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.ActiveDate);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.ActiveDate);
                        break;
                    case DeductMoneySortBy.ProjectID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.ProjectID);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.ProjectID);
                        break;
                    case DeductMoneySortBy.SaleUserID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.SaleUserID);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.SaleUserID);
                        break;
                    case DeductMoneySortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.Amount);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.Amount);
                        break;
                    case DeductMoneySortBy.Remark:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.Remark);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.Remark);
                        break;                   
                    default:
                        orderQuery = query.OrderBy(o => o.DeductMoney.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.DeductMoney.ActiveDate);
            }

            orderQuery.ThenBy(o => o.DeductMoney.ID);
            query = orderQuery;
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DeductMoneyDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DeductMoneyDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.SaleUser == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DeductMoneyDTO.SaleUser)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(DeductMoneyDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref DeductMoney model)
        {
            model.ActiveDate = this.ActiveDate;
            model.ProjectID = this.Project.Id;
            model.SaleUserID = this.SaleUser.Id;
            model.Amount = this.Amount;
            model.Remark = this.Remark;
        }
    }

    public class DeductMoneyQueryResult
    {
        public DeductMoney DeductMoney { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User SaleUser { get; set; }
    }
}
