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
    public class IncreaseDeductMoneyDTO : BaseDTO
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

        /// <summary>
        /// ประเภทคอมมิสชั่น
        /// </summary>
        [Description("ประเภทคอมมิสชั่น")]
        public MST.MasterCenterDropdownDTO CommissionType { get; set; }


        public static IncreaseDeductMoneyDTO CreateFromIncreaseMoneyModel(IncreaseMoney model)
        {
            if (model != null)
            {
                var result = new IncreaseDeductMoneyDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    CommissionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CommissionType),
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

        public static IncreaseDeductMoneyDTO CreateFromDeductMoneyModel(DeductMoney model)
        {
            if (model != null)
            {
                var result = new IncreaseDeductMoneyDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    CommissionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CommissionType),
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

        public static IncreaseDeductMoneyDTO CreateFromQueryResultIncreaseMoney(IncreaseMoneyQueryResult model)
        {
            if (model != null)
            {
                var result = new IncreaseDeductMoneyDTO()
                {
                    Id = model.IncreaseMoney.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    CommissionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CommissionType),
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

        public static IncreaseDeductMoneyDTO CreateFromQueryResultDeductMoney(DeductMoneyQueryResult model)
        {
            if (model != null)
            {
                var result = new IncreaseDeductMoneyDTO()
                {
                    Id = model.DeductMoney.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.SaleUser),
                    CommissionType = MST.MasterCenterDropdownDTO.CreateFromModel(model.CommissionType),
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

        public static void SortByIncreaseMoney(IncreaseMoneySortByParam sortByParam, ref IQueryable<IncreaseMoneyQueryResult> query)
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
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.Project.ProjectNo);
                        break;
                    case IncreaseMoneySortBy.SaleUserID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.SaleUser.EmployeeNo);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.SaleUser.EmployeeNo);
                        break;
                    case IncreaseMoneySortBy.SaleUserName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.SaleUser.DisplayName);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.SaleUser.DisplayName);
                        break;
                    case IncreaseMoneySortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.Amount);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.Amount);
                        break;
                    case IncreaseMoneySortBy.Remark:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.IncreaseMoney.Remark);
                        else orderQuery = query.OrderByDescending(o => o.IncreaseMoney.Remark);
                        break;
                    case IncreaseMoneySortBy.CommissionType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionType);
                        else orderQuery = query.OrderByDescending(o => o.CommissionType);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.IncreaseMoney.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.IncreaseMoney.ActiveDate);
            }

            orderQuery = orderQuery.ThenByDescending(x => x.IncreaseMoney.Created);
            query = orderQuery;
        }
        public static void SortByDeductMoney(DeductMoneySortByParam sortByParam, ref IQueryable<DeductMoneyQueryResult> query)
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
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.Project.ProjectNo);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.Project.ProjectNo);
                        break;
                    case DeductMoneySortBy.SaleUserID:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.SaleUser.EmployeeNo);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.SaleUser.EmployeeNo);
                        break;
                    case DeductMoneySortBy.SaleUserName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.SaleUser.DisplayName);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.SaleUser.DisplayName);
                        break;
                    case DeductMoneySortBy.Amount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.Amount);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.Amount);
                        break;
                    case DeductMoneySortBy.Remark:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.DeductMoney.Remark);
                        else orderQuery = query.OrderByDescending(o => o.DeductMoney.Remark);
                        break;
                    case DeductMoneySortBy.CommissionType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionType);
                        else orderQuery = query.OrderByDescending(o => o.CommissionType);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.DeductMoney.ActiveDate);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.DeductMoney.ActiveDate);
            }

            orderQuery = orderQuery.ThenByDescending(o => o.DeductMoney.Created);
            query = orderQuery;
        }

        public async Task IncreaseMoneyValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.SaleUser == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.SaleUser)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.CommissionType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.CommissionType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }
        public async Task DeductMoneyValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!this.ActiveDate.HasValue)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.ActiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Project == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.SaleUser == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.SaleUser)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.Amount == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.Amount)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.CommissionType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(IncreaseDeductMoneyDTO.CommissionType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToIncreaseMoneyModel(ref IncreaseMoney model)
        {
            model.ActiveDate = Extensions.DateStartMonth(this.ActiveDate);
            model.ProjectID = this.Project.Id;
            model.SaleUserID = this.SaleUser.Id;
            model.Amount = this.Amount;
            model.Remark = this.Remark;
            model.CommissionTypeMasterCenterID = this.CommissionType.Id;
        }
        public void ToDeductMoneyModel(ref DeductMoney model)
        {
            model.ActiveDate = Extensions.DateStartMonth(this.ActiveDate);
            model.ProjectID = this.Project.Id;
            model.SaleUserID = this.SaleUser.Id;
            model.Amount = this.Amount;
            model.Remark = this.Remark;
            model.CommissionTypeMasterCenterID = this.CommissionType.Id;
        }
    }

    public class IncreaseMoneyQueryResult
    {
        public IncreaseMoney IncreaseMoney { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User SaleUser { get; set; }
        public User CreatedByUser { get; set; }
        public models.MST.MasterCenter CommissionType { get; set; }
    }

    public class DeductMoneyQueryResult
    {
        public DeductMoney DeductMoney { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User SaleUser { get; set; }
        public User CreatedByUser { get; set; }
        public models.MST.MasterCenter CommissionType { get; set; }
    }
}
