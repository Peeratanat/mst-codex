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
    /// รายละเอียดของโปรโมชั่นก่อนขาย
    /// Model = MasterSalePromotion
    /// </summary>
    public class MasterSalePromotionDTO : BaseDTO
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
        /// โครงการ
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
        /// ส่วนลดเงินสด
        /// </summary>
        public decimal? CashDiscount { get; set; }
        /// <summary>
        /// ส่วนลด FGF
        /// </summary>
        public decimal? FGFDiscount { get; set; }
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
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// IsPopUp
        /// </summary>
        public bool IsPopUp { get; set; }
        /// <summary>
        /// Message
        /// </summary>
        public string Message  { get; set; }

        public static MasterSalePromotionDTO CreateFromQueryResult(MasterSalePromotionQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionDTO()
                {
                    Id = model.MasterSalePromotion.ID,
                    PromotionNo = model.MasterSalePromotion.PromotionNo,
                    Name = model.MasterSalePromotion.Name,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StartDate = model.MasterSalePromotion.StartDate,
                    EndDate = model.MasterSalePromotion.EndDate,
                    CashDiscount = model.MasterSalePromotion.CashDiscount,
                    FGFDiscount = model.MasterSalePromotion.FGFDiscount,
                    TransferDiscount = model.MasterSalePromotion.TransferDiscount,
                    PromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionStatus),
                    IsUsed = model.MasterSalePromotion.IsUsed,
                    Updated = model.MasterSalePromotion.Updated,
                    UpdatedBy = model.MasterSalePromotion.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterSalePromotionDTO CreateFromModel(MasterSalePromotion model)
        {
            if (model != null)
            {
                var result = new MasterSalePromotionDTO()
                {
                    Id = model.ID,
                    PromotionNo = model.PromotionNo,
                    Name = model.Name,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    CashDiscount = model.CashDiscount,
                    FGFDiscount = model.FGFDiscount,
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

        public static void SortBy(MasterSalePromotionSortByParam sortByParam, ref IQueryable<MasterSalePromotionQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterSalePromotionSortBy.PromotionNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.PromotionNo);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.PromotionNo);
                        break;
                    case MasterSalePromotionSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.Name);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.Name);
                        break;
                    case MasterSalePromotionSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case MasterSalePromotionSortBy.StartDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.StartDate);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.StartDate);
                        break;
                    case MasterSalePromotionSortBy.EndDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.EndDate);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.EndDate);
                        break;
                    case MasterSalePromotionSortBy.CashDiscount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.CashDiscount);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.CashDiscount);
                        break;
                    case MasterSalePromotionSortBy.FGFDiscount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.FGFDiscount);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.FGFDiscount);
                        break;
                    case MasterSalePromotionSortBy.TransferDiscount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.TransferDiscount);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.TransferDiscount);
                        break;
                    case MasterSalePromotionSortBy.PromotionStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionStatus.Name);
                        break;
                    case MasterSalePromotionSortBy.IsUsed:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.IsUsed);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.IsUsed);
                        break;
                    case MasterSalePromotionSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterSalePromotion.Updated);
                        else query = query.OrderByDescending(o => o.MasterSalePromotion.Updated);
                        break;
                    case MasterSalePromotionSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterSalePromotion.PromotionNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterSalePromotion.PromotionNo);
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
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                //else
                //{
                //    if (!this.Name.CheckAllLang(true, false, false, null, " "))
                //    {
                //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
                //        string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                //        var msg = errMsg.Message.Replace("[field]", desc);
                //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //    }
                //}
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.StartDate == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.StartDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.EndDate == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.EndDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.PromotionStatus == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.PromotionStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            else
            {
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateasterSalePromotionsAsync(DatabaseContext db, bool isEdit = false)
        {
            ValidateException ex = new ValidateException();
            if (isEdit)
            {
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterSalePromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterSalePromotion model)
        {
            model.Name = this.Name;
            model.ProjectID = this.Project?.Id;
            model.StartDate = this.StartDate;
            model.EndDate = this.EndDate;
            model.CashDiscount = this.CashDiscount;
            model.FGFDiscount = this.FGFDiscount;
            model.TransferDiscount = this.TransferDiscount;
            model.PromotionStatusMasterCenterID = this.PromotionStatus?.Id;
        }
    }
    public class MasterSalePromotionQueryResult
    {
        public MasterSalePromotion MasterSalePromotion { get; set; }
        public Project Project { get; set; }
        public MasterCenter PromotionStatus { get; set; }
        public User UpdatedBy { get; set; }
    }
}
