using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    /// <summary>
    /// เครื่องรูดบัตร
    /// Model = EDC
    /// </summary>
    public class EDCDTO : BaseDTO
    {
        /// <summary>
        /// ธนาคาร
        /// Master/api/Banks/DropdownList
        /// </summary>
        [Description("ธนาคาร")]
        public BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// รหัสเครื่องรูดบัตร
        /// </summary>
        [Description("รหัสเครื่องรูดบัตร")]
        public string Code { get; set; }
        /// <summary>
        /// ประเภทเครื่อง
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CardMachineType
        /// </summary>
        [Description("ประเภทเครื่อง")]
        public MasterCenterDropdownDTO CardMachineType { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        [Description("เบอร์โทรศัพท์")]
        public string TelNo { get; set; }
        /// <summary>
        /// สถานะเครื่อง
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=CardMachineStatus
        /// </summary>
        [Description("สถานะเครื่อง")]
        public MasterCenterDropdownDTO CardMachineStatus { get; set; }
        /// <summary>
        /// โครงการ
        /// Project/api/Projects/DropdownList
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// บริษัท
        /// Master/api/Companies/DropdownList
        /// </summary>
        public CompanyDropdownDTO Company { get; set; }
        /// <summary>
        /// บัญชีธนาคาร
        /// Master/api/BankAccounts/DropdownList
        /// </summary>
        public BankAccountDropdownDTO BankAccount { get; set; }
        /// <summary>
        /// ผู้รับเครื่อง
        /// </summary>
        [Description("ผู้รับเครื่อง")]
        public string ReceiveBy { get; set; }
        /// <summary>
        /// วันที่มอบเครื่อง
        /// </summary>
        [Description("วันที่มอบเครื่อง")]
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        [Description("หมายเหตุ")]
        public string Remark { get; set; }

        public static EDCDTO CreateFromModel(EDC model)
        {
            if (model != null)
            {
                var result = new EDCDTO()
                {
                    Id = model.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    Code = model.Code,
                    CardMachineType = MasterCenterDropdownDTO.CreateFromModel(model.CardMachineType),
                    TelNo = model.TelNo,
                    CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(model.CardMachineStatus),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount),
                    ReceiveBy = model.ReceiveBy,
                    ReceiveDate = model.ReceiveDate,
                    Remark = model.Remark,
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
        public static EDCDTO CreateFromQueryResult(EDCQueryResult model)
        {
            if (model != null)
            {
                var result = new EDCDTO()
                {
                    Id = model.EDC.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    Code = model.EDC.Code,
                    CardMachineType = MasterCenterDropdownDTO.CreateFromModel(model.CardMachineType),
                    TelNo = model.EDC.TelNo,
                    CardMachineStatus = MasterCenterDropdownDTO.CreateFromModel(model.CardMachineStatus),
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    Company = CompanyDropdownDTO.CreateFromModel(model.Company),
                    BankAccount = BankAccountDropdownDTO.CreateFromModel(model.BankAccount), 
                    ReceiveBy = model.EDC.ReceiveBy,
                    ReceiveDate = model.EDC.ReceiveDate,
                    Remark = model.EDC.Remark,
                    Updated = model.EDC.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName, 
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(EDCSortByParam sortByParam, ref IQueryable<EDCQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case EDCSortBy.Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.Code);
                        else query = query.OrderByDescending(o => o.EDC.Code);
                        break;
                    case EDCSortBy.CardMachineType:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CardMachineType.Name);
                        else query = query.OrderByDescending(o => o.CardMachineType.Name);
                        break;
                    case EDCSortBy.BankAccountNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankAccount.Bank.Alias).ThenBy(o=>o.BankAccount.BankAccountNo);
                        else query = query.OrderByDescending(o => o.BankAccount.Bank.Alias).ThenBy(o => o.BankAccount.BankAccountNo);
                        break;
                    case EDCSortBy.Company:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Company.NameTH);
                        else query = query.OrderByDescending(o => o.Company.NameTH);
                        break;
                    case EDCSortBy.Project_ProjectNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case EDCSortBy.Project_Status:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectStatus.Name);
                        else query = query.OrderByDescending(o => o.Project.ProjectStatus.Name);
                        break;
                    case EDCSortBy.ReceiveBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.ReceiveBy);
                        else query = query.OrderByDescending(o => o.EDC.ReceiveBy);
                        break;
                    case EDCSortBy.ReceiveDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.ReceiveDate);
                        else query = query.OrderByDescending(o => o.EDC.ReceiveDate);
                        break;
                    case EDCSortBy.CardMachineStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CardMachineStatus.Name);
                        else query = query.OrderByDescending(o => o.CardMachineStatus.Name);
                        break;
                    case EDCSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.Updated);
                        else query = query.OrderByDescending(o => o.EDC.Updated);
                        break;
                    case EDCSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case EDCSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case EDCSortBy.TelNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.EDC.TelNo);
                        else query = query.OrderByDescending(o => o.EDC.TelNo);
                        break;
                    default:
                        query = query.OrderBy(o => o.EDC.Code);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.EDC.Code);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            // ธนาคาร
            if (this.Bank == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(EDCDTO.Bank)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // รหัสเครื่อง 
            if (string.IsNullOrEmpty(this.Code))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(EDCDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.Code.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0022").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(EDCDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            var checkUniqueCode = this.Id != (Guid?)null ? await db.EDCs.Where(o => o.Code == this.Code && o.ID != this.Id).CountAsync() > 0 : await db.EDCs.Where(o => o.Code == this.Code).CountAsync() > 0;
            if (checkUniqueCode)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(EDCDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.Code);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // ประเภทเครื่อง 
            if (this.CardMachineType == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(EDCDTO.CardMachineType)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                //// เบอร์โทรศัพท์
                //if (string.IsNullOrEmpty(this.TelNo) && this.CardMachineType.Key.Equals(CardMachineTypeKeys.Mobile))
                //{
                //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                //    string desc = this.GetType().GetProperty(nameof(EDCDTO.TelNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                //    var msg = errMsg.Message.Replace("[field]", desc);
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //}
            }
            // สถานะเครื่อง
            if (this.CardMachineStatus == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(EDCDTO.CardMachineStatus)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // โครงการ
            //if (this.Project == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(EDCDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            // ผู้รับเครื่อง
            if (string.IsNullOrEmpty(this.ReceiveBy))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(EDCDTO.ReceiveBy)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // วันที่มอบเครื่อง
            if (this.ReceiveDate == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(EDCDTO.ReceiveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // หมายเหตุ
            //if (this.Remark == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(EDCDTO.Remark)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref EDC model)
        {
            model.BankID = this.Bank?.Id;
            model.Code = this.Code;
            model.CardMachineTypeMasterCenterID = this.CardMachineType?.Id;
            model.TelNo = this.TelNo;
            model.CardMachineStatusMasterCenterID = this.CardMachineStatus?.Id;
            model.ProjectID = this.Project?.Id;
            model.CompanyID = this.Company?.Id;
            model.BankAccountID = this.BankAccount?.Id;
            model.ReceiveBy = this.ReceiveBy;
            model.ReceiveDate = this.ReceiveDate;
            model.Remark = this.Remark;
        }
    }

    public class EDCQueryResult
    {
        public EDC EDC { get; set; }
        public Company Company { get; set; }
        public Bank Bank { get; set; }
        public BankAccount BankAccount { get; set; }
        public MasterCenter CardMachineType { get; set; }
        public MasterCenter CardMachineStatus { get; set; }
        public Project Project { get; set; }
        public User UpdatedBy { get; set; }
    }
}
