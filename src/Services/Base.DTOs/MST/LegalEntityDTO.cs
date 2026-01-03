using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
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
    public class LegalEntityDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อ นิติบุคคลอาคารชุด ภาษาไทย (TH)
        /// </summary>
        [Description("ชื่อ นิติบุคคลอาคารชุด ภาษาไทย (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อ นิติบุคคลอาคารชุด อังกฤษ (EN)
        /// </summary>
        [Description("ชื่อ นิติบุคคลอาคารชุด อังกฤษ (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        /// ธนาคาร
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// ประเภทบัญชี
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=BankAccountType
        /// </summary>
        [Description("ประเภทบัญชี")]
        public MST.MasterCenterDropdownDTO BankAccountType { get; set; }
        /// <summary>
        /// เลขบัญชีธนาคาร
        /// </summary>
        [Description("เลขบัญชีธนาคาร")]
        public string BankAccountNo { get; set; }
        /// <summary>
        /// สถานะ Active/InActive
        /// </summary>
        [Description("สถานะ Active/InActive")]
        public bool IsActive { get; set; }
        /// <summary>
        /// รหัสนิติ
        /// </summary>
        [Description("VendorCode")]
        public string VendorCode { get; set; }
        /// <summary>
        /// Project
        /// </summary>
        [Description("Project")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// ProjectColumn
        /// </summary>
        [Description("ProjectColumn")]
        public PRJ.ProjectDTO ProjectColumn { get; set; }

        public static LegalEntityDTO CreateFromModel(LegalEntity model)
        {
            if (model != null)
            {
                var result = new LegalEntityDTO()
                {
                    Id = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccountType = MasterCenterDropdownDTO.CreateFromModel(model.BankAccountType),
                    BankAccountNo = model.BankAccountNo,
                    IsActive = model.IsActive,
                    VendorCode = model.AgentCode,
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

        public static LegalEntityDTO CreateFromQueryResult(LegalEntityQueryResult model )
        {
            if (model != null)
            {
                var result = new LegalEntityDTO()
                {
                    Id = model.LegalEntity.ID,
                    NameTH = model.LegalEntity.NameTH,
                    NameEN = model.LegalEntity.NameEN,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankAccountType = MasterCenterDropdownDTO.CreateFromModel(model.BankAccountType),
                    BankAccountNo = model.LegalEntity.BankAccountNo,
                    IsActive = model.LegalEntity.IsActive,
                    VendorCode = model.LegalEntity.AgentCode,
                    Updated = model.LegalEntity.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    ProjectColumn = ProjectDTO.CreateFromModel(model.Project)
                }; 
                //result.Project = ProjectDropdownDTO.CreateFromModel(modelProject.Project);
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(LegalEntitySortByParam sortByParam, ref IQueryable<LegalEntityQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LegalEntitySortBy.NameTH:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LegalEntity.NameTH);
                        else query = query.OrderByDescending(o => o.LegalEntity.NameTH);
                        break;
                    case LegalEntitySortBy.NameEN:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LegalEntity.NameEN);
                        else query = query.OrderByDescending(o => o.LegalEntity.NameEN);
                        break;
                    case LegalEntitySortBy.IsActive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LegalEntity.IsActive);
                        else query = query.OrderByDescending(o => o.LegalEntity.IsActive);
                        break;
                    case LegalEntitySortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case LegalEntitySortBy.BankAccountNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LegalEntity.BankAccountNo);
                        else query = query.OrderByDescending(o => o.LegalEntity.BankAccountNo);
                        break;
                    case LegalEntitySortBy.BankAccountType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LegalEntity.BankAccountType);
                        else query = query.OrderByDescending(o => o.LegalEntity.BankAccountType);
                        break;
                    case LegalEntitySortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LegalEntity.Updated);
                        else query = query.OrderByDescending(o => o.LegalEntity.Updated);
                        break;
                    case LegalEntitySortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case LegalEntitySortBy.VendorCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LegalEntity.AgentCode);
                        else query = query.OrderByDescending(o => o.LegalEntity.AgentCode);
                        break;
                    default:
                        query = query.OrderBy(o => o.LegalEntity.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.LegalEntity.NameTH);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.NameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //else
            //{
            //    if (!this.NameTH.CheckLang(true, false, true, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0022").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}

            var checkUniqueNameTH = await db.LegalEntities.Where(o => o.NameTH == this.NameTH && o.ID != this.Id).AnyAsync();
            if (checkUniqueNameTH)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstOrDefaultAsync();
                string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.NameTH);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            //if (!string.IsNullOrEmpty(this.NameEN))
            //{
            //    if (!this.NameEN.CheckLang(false, false, true, false))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0005").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            //if (this.Bank == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //if (this.BankAccountType == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.BankAccountType)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //if (string.IsNullOrEmpty(BankAccountNo))
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.BankAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //else
            //{
            //    if (!this.BankAccountNo.IsOnlyNumberWithMaxLength(10))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0023").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(LegalEntityDTO.BankAccountNo)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref LegalEntity model)
        {
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.BankID = this.Bank?.Id;
            model.BankAccountTypeMasterCenterID = this.BankAccountType?.Id;
            model.BankAccountNo = this.BankAccountNo;
            model.IsActive = this.IsActive;
            model.AgentCode = this.VendorCode;
        }
    }

    public class LegalEntityQueryResult
    {
        public LegalEntity LegalEntity { get; set; }
        public Bank Bank { get; set; }
        public User UpdatedBy { get; set; }
        public MasterCenter BankAccountType { get; set; }
        public Project Project { get; set; }
    }
}
