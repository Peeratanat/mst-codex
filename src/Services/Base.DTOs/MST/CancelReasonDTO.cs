using Database.Models;
using Database.Models.MST;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class CancelReasonDTO : BaseDTO
    {
        /// <summary>
        /// รหัส
        /// </summary>
        [Description("รหัส")]
        public string Key { get; set; }


        /// <summary>
        /// เหตุผล
        /// </summary>
        [Description("เหตุผล")]
        public string Description { get; set; }


        /// <summary>
        /// กลุ่มของเหตุผล
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=GroupOfCancelReason
        /// </summary>
        [Description("กลุ่มของเหตุผล")]
        public MST.MasterCenterDropdownDTO GroupOfCancelReason { get; set; }

        /// <summary>
        /// รูปแบบการอนุมัติ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CancelApproveFlow
        /// </summary>
        [Description("รูปแบบการอนุมัติ")]
        public MST.MasterCenterDropdownDTO CancelApproveFlow { get; set; }
        public static CancelReasonDTO CreateFromModel(CancelReason model)
        {
            if (model != null)
            {
                var result = new CancelReasonDTO()
                {
                    Id = model.ID,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    Key = model.Key,
                    Description = model.Description,
                    GroupOfCancelReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.GroupOfCancelReason),
                    CancelApproveFlow = MST.MasterCenterDropdownDTO.CreateFromModel(model.CancelApproveFlow),
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static CancelReasonDTO CreateFromQueryResult(CancelReasonQueryResult model)
        {
            if (model != null)
            {
                var result = new CancelReasonDTO()
                {
                    Id = model.CancelReason.ID,
                    Updated = model.CancelReason.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    Key = model.CancelReason.Key,
                    Description = model.CancelReason.Description,
                    GroupOfCancelReason = MST.MasterCenterDropdownDTO.CreateFromModel(model.GroupOfCancelReason),
                    CancelApproveFlow = MST.MasterCenterDropdownDTO.CreateFromModel(model.CancelApproveFlow),
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static void SortBy(CancelReasonSortByParam sortByParam, ref IQueryable<CancelReasonQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CancelReasonSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CancelReason.Updated);
                        else query = query.OrderByDescending(o => o.CancelReason.Updated);
                        break;
                    case CancelReasonSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case CancelReasonSortBy.Key:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CancelReason.Key);
                        else query = query.OrderByDescending(o => o.CancelReason.Key);
                        break;
                    case CancelReasonSortBy.Description:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CancelReason.Description);
                        else query = query.OrderByDescending(o => o.CancelReason.Description);
                        break;
                    case CancelReasonSortBy.GroupOfCancelReason:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.GroupOfCancelReason.Name);
                        else query = query.OrderByDescending(o => o.GroupOfCancelReason.Name);
                        break;
                    case CancelReasonSortBy.CancelApproveFlow:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CancelApproveFlow.Name);
                        else query = query.OrderByDescending(o => o.CancelApproveFlow.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.CancelReason.Key);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.Key))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstOrDefaultAsync();
                string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.Key.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0022").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueKey = this.Id != (Guid?)null ? await db.CancelReasons.Where(o => o.Key == this.Key && o.ID != this.Id).CountAsync() > 0 : await db.CancelReasons.Where(o => o.Key == this.Key).CountAsync() > 0;
                if (checkUniqueKey)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.Key)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Key);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (string.IsNullOrEmpty(this.Description))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.Description)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.Description.CheckAllLang(true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.Description)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueDesc = await db.CancelReasons.Where(o => o.Description == this.Description && o.ID != this.Id).AnyAsync();
                if (checkUniqueDesc)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.Description)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Description);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (this.GroupOfCancelReason == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.GroupOfCancelReason)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.CancelApproveFlow == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(CancelReasonDTO.CancelApproveFlow)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref CancelReason model)
        {
            model.Key = this.Key;
            model.Description = this.Description;
            model.GroupOfCancelReasonMasterCenterID = this.GroupOfCancelReason?.Id;
            model.CancelApproveFlowMasterCenterID = this.CancelApproveFlow?.Id;
        }
    }
    public class CancelReasonQueryResult
    {
        public CancelReason CancelReason { get; set; }
        public MasterCenter GroupOfCancelReason { get; set; }
        public MasterCenter CancelApproveFlow { get; set; }
        public User UpdatedBy { get; set; }
    }
}
