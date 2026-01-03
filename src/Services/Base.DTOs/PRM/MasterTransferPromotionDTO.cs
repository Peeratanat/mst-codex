using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// Master โปรโอน
    /// Model = MasterTransferPromotion
    /// </summary>
    public class MasterTransferPromotionDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น
        /// </summary>
        [Description("ชื่อโปรโมชั่น")]
        public string Name { get; set; }
        /// <summary>
        /// ผูกโครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// วันที่เริ่มต้น
        /// </summary>
        [Description("วันที่เริ่มใช้งาน")]
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// วันที่สิ้นสุด
        /// </summary>
        [Description("วันที่สิ้นสุดการใช้งาน")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// ส่วนลดวันโอน
        /// </summary>
        public decimal? TransferDiscount { get; set; }
        /// <summary>
        /// สถานะ Active
        /// Master/api/MasterCenters?masterCenterGroupKey=PromotionStatus
        /// </summary>
        [Description("สถานะโปรโมชั่น")]
        public MST.MasterCenterDropdownDTO PromotionStatus { get; set; }
        /// <summary>
        /// สถานะการนำไปใช้งาน
        /// </summary>
        public bool IsUsed { get; set; }
        public static MasterTransferPromotionDTO CreateFromQueryResult(MasterTransferPromotionQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterTransferPromotionDTO()
                {
                    Id = model.MasterTransferPromotion.ID,
                    PromotionNo = model.MasterTransferPromotion.PromotionNo,
                    Name = model.MasterTransferPromotion.Name,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StartDate = model.MasterTransferPromotion.StartDate,
                    EndDate = model.MasterTransferPromotion.EndDate,
                    TransferDiscount = model.MasterTransferPromotion.TransferDiscount,
                    PromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionStatus),
                    IsUsed = model.MasterTransferPromotion.IsUsed,
                    Updated = model.MasterTransferPromotion.Updated,
                    UpdatedBy = model.MasterTransferPromotion.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterTransferPromotionDTO CreateFromModel(MasterTransferPromotion model)
        {
            if (model != null)
            {
                var result = new MasterTransferPromotionDTO()
                {
                    Id = model.ID,
                    PromotionNo = model.PromotionNo,
                    Name = model.Name,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    TransferDiscount = model.TransferDiscount,
                    PromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionStatus),
                    IsUsed = model.IsUsed,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(MasterTransferPromotionSortByParam sortByParam, ref IQueryable<MasterTransferPromotionQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterTransferPromotionSortBy.PromotionNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotion.PromotionNo);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotion.PromotionNo);
                        break;
                    case MasterTransferPromotionSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotion.Name);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotion.Name);
                        break;
                    case MasterTransferPromotionSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case MasterTransferPromotionSortBy.StartDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotion.StartDate);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotion.StartDate);
                        break;
                    case MasterTransferPromotionSortBy.EndDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotion.EndDate);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotion.EndDate);
                        break;
                    case MasterTransferPromotionSortBy.TransferDiscount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotion.TransferDiscount);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotion.TransferDiscount);
                        break;
                    case MasterTransferPromotionSortBy.PromotionStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionStatus.Name);
                        break;
                    case MasterTransferPromotionSortBy.IsUsed:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotion.IsUsed);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotion.IsUsed);
                        break;
                    case MasterTransferPromotionSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterTransferPromotion.Updated);
                        else query = query.OrderByDescending(o => o.MasterTransferPromotion.Updated);
                        break;
                    case MasterTransferPromotionSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterTransferPromotion.PromotionNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterTransferPromotion.PromotionNo);
            }
        }

        public async Task ValidateAsync(DatabaseContext db, bool isEdit = false)
        {
            ValidateException ex = new ValidateException();
            if (isEdit)
            {
                if (string.IsNullOrEmpty(this.Name))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                //else
                //{
                //    if (!this.name.checkalllang(true, false, false, null, " "))
                //    {
                //        var errmsg = await db.errormessages.where(o => o.key == "err0018").firstasync();
                //        string desc = this.gettype().getproperty(nameof(mastertransferpromotiondto.name)).getcustomattribute<descriptionattribute>().description;
                //        var msg = errmsg.message.replace("[field]", desc);
                //        ex.adderror(errmsg.key, msg, (int)errmsg.type);
                //    }
                //}
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.StartDate == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionDTO.StartDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.EndDate == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionDTO.EndDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.PromotionStatus == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionDTO.PromotionStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            else
            {
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterTransferPromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterTransferPromotion model)
        {
            model.Name = this.Name;
            model.ProjectID = this.Project?.Id;
            model.StartDate = this.StartDate;
            model.EndDate = this.EndDate;
            model.TransferDiscount = this.TransferDiscount;
            model.PromotionStatusMasterCenterID = this.PromotionStatus?.Id;
        }
    }
    public class MasterTransferPromotionQueryResult
    {
        public MasterTransferPromotion MasterTransferPromotion { get; set; }
        public Project Project { get; set; }
        public MasterCenter PromotionStatus { get; set; }
        public User UpdatedBy { get; set; }

    }
}
