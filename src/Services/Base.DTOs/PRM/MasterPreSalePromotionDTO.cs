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
    /// รายละเอียก Master โปรก่อนขาย
    /// Model = MasterPreSalePromotion
    /// </summary>
    public class MasterPreSalePromotionDTO : BaseDTO
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
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// รหัสบริษัท
        /// </summary>
        public string CompanyCode { get; set; }
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
        /// อนุมัติแล้ว
        /// </summary>
        public bool IsApproved { get; set; }
        /// <summary>
        /// วันที่อนุมัติ
        /// </summary>
        public DateTime? ApprovedDate { get; set; }
        /// <summary>
        /// ราคารวมโปรโมชั่น
        /// </summary>
        public decimal? TotalItemPrice { get; set; }
        public static MasterPreSalePromotionDTO CreateFromQueryResult(MasterPreSalePromotionQueryResult model)
        {
            if (model != null)
            {
                var result = new MasterPreSalePromotionDTO()
                {
                    Id = model.MasterPreSalePromotion.ID,
                    PromotionNo = model.MasterPreSalePromotion.PromotionNo,
                    Name = model.MasterPreSalePromotion.Name,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    CompanyCode = model.MasterPreSalePromotion.CompanyCode,
                    PromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionStatus),
                    IsUsed = model.MasterPreSalePromotion.IsUsed,
                    IsApproved = model.MasterPreSalePromotion.IsApproved,
                    ApprovedDate = model.MasterPreSalePromotion.ApprovedDate,
                    Updated = model.MasterPreSalePromotion.Updated,
                    UpdatedBy = model.MasterPreSalePromotion.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public async static Task<MasterPreSalePromotionDTO> CreateFromModelAsync(MasterPreSalePromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                var result = new MasterPreSalePromotionDTO();

                result.Id = model.ID;
                result.PromotionNo = model.PromotionNo;
                result.Name = model.Name;
                result.Project = PRJ.ProjectDTO.CreateFromModel(model.Project);
                result.CompanyCode = model.CompanyCode;
                result.PromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.PromotionStatus);
                result.IsUsed = model.IsUsed;
                result.IsApproved = model.IsApproved;
                result.ApprovedDate = model.ApprovedDate;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy?.DisplayName;


                result.TotalItemPrice = await db.MasterPreSalePromotionItems.Where(o => o.MasterPreSalePromotionID == model.ID).SumAsync(o => o.TotalPrice);

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(MasterPreSalePromotionSortByParam sortByParam, ref IQueryable<MasterPreSalePromotionQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MasterPreSalePromotionSortBy.PromotionNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPreSalePromotion.PromotionNo);
                        else query = query.OrderByDescending(o => o.MasterPreSalePromotion.PromotionNo);
                        break;
                    case MasterPreSalePromotionSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPreSalePromotion.Name);
                        else query = query.OrderByDescending(o => o.MasterPreSalePromotion.Name);
                        break;
                    case MasterPreSalePromotionSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case MasterPreSalePromotionSortBy.CompanyCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPreSalePromotion.CompanyCode);
                        else query = query.OrderByDescending(o => o.MasterPreSalePromotion.CompanyCode);
                        break;
                    case MasterPreSalePromotionSortBy.PromotionStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionStatus.Name);
                        else query = query.OrderByDescending(o => o.PromotionStatus.Name);
                        break;
                    case MasterPreSalePromotionSortBy.IsUsed:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPreSalePromotion.IsUsed);
                        else query = query.OrderByDescending(o => o.MasterPreSalePromotion.IsUsed);
                        break;
                    case MasterPreSalePromotionSortBy.IsApproved:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPreSalePromotion.IsApproved);
                        else query = query.OrderByDescending(o => o.MasterPreSalePromotion.IsApproved);
                        break;
                    case MasterPreSalePromotionSortBy.ApprovedDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPreSalePromotion.ApprovedDate);
                        else query = query.OrderByDescending(o => o.MasterPreSalePromotion.ApprovedDate);
                        break;
                    case MasterPreSalePromotionSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MasterPreSalePromotion.Updated);
                        else query = query.OrderByDescending(o => o.MasterPreSalePromotion.Updated);
                        break;
                    case MasterPreSalePromotionSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.MasterPreSalePromotion.PromotionNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.MasterPreSalePromotion.PromotionNo);
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
                    string desc = this.GetType().GetProperty(nameof(MasterPreSalePromotionDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                //else
                //{
                    //if (!this.Name.CheckAllLang(true, false, false, null, " "))
                    //{
                    //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0018").FirstAsync();
                    //    string desc = this.GetType().GetProperty(nameof(MasterPreSalePromotionDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    //    var msg = errMsg.Message.Replace("[field]", desc);
                    //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    //}
                //}
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterPreSalePromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                if (this.PromotionStatus == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterPreSalePromotionDTO.PromotionStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            else
            {
                if (this.Project == null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(MasterPreSalePromotionDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref MasterPreSalePromotion model)
        {
            model.Name = this.Name;
            model.ProjectID = this.Project?.Id;
            model.PromotionStatusMasterCenterID = this.PromotionStatus?.Id;
            model.CompanyCode = this.CompanyCode;
        }
    }
    public class MasterPreSalePromotionQueryResult
    {
        public MasterPreSalePromotion MasterPreSalePromotion { get; set; }
        public Project Project { get; set; }
        public MasterCenter PromotionStatus { get; set; }
        public User UpdatedBy { get; set; }
    }
}
