using Database.Models;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.ComponentModel;
using ErrorHandling;
using Database.Models.USR;

namespace Base.DTOs.MST
{
    public class BankBranchDTO : BaseDTO
    {
        /// <summary>
        /// ธนาคาร
        /// Master/Banks/DropdownList
        /// </summary>
        public BankDropdownDTO Bank { get; set; }
        /// <summary>
        /// ชื่อสาขา
        /// </summary>
        [Description("ชื่อสาขา")]
        public string Name { get; set; }
        /// <summary>
        /// ที่อยู่
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ตึก
        /// </summary>
        [Description("ตึก")]
        public string Building { get; set; }
        /// <summary>
        /// ซอย
        /// </summary>
        public string Soi { get; set; }
        /// <summary>
        /// ถนน
        /// </summary>
        public string Road { get; set; }
        /// <summary>
        /// Master/SubDistricts/DropdownList
        /// ตำบล
        /// </summary>
        [Description("ตำบล")]
        public SubDistrictListDTO SubDistrict { get; set; }
        /// <summary>
        /// Master/Districts/DropdownList
        /// อำเภอ
        /// </summary>
        [Description("อำเภอ")]
        public DistrictListDTO District { get; set; }
        /// <summary>
        /// Master/Provinces/DropdownList
        /// </summary>
        [Description("จังหวัด")]
        public ProvinceListDTO Province { get; set; }
        /// <summary>
        /// รหัสไปรษ์ณีย์
        /// </summary>
        [Description("รหัสไปรษ์ณีย์")]
        public string PostalCode { get; set; }
        /// <summary>
        /// เบอร์โทร
        /// </summary>
        [Description("เบอร์โทร")]
        public string Telephone { get; set; }
        /// <summary>
        /// เบอร์แฟ๊กซ์
        /// </summary>
        [Description("เบอร์แฟ๊กซ์")]
        public string Fax { get; set; }
        /// <summary>
        /// บัญชี Credit
        /// </summary>
        public bool IsCreditBank { get; set; }
        /// <summary>
        /// บัญชี Direct Debit
        /// </summary>
        public bool IsDirectDebit { get; set; }
        /// <summary>
        /// บัญชี Direct Credit
        /// </summary>
        public bool IsDirectCredit { get; set; }
        /// <summary>
        /// รหัสสาขา
        /// </summary>
        [Description("รหัสสาขา")]
        public string AreaCode { get; set; }
        /// <summary>
        /// รหัส ID แบงค์อันเก่า
        /// </summary>
        public string OldBankID { get; set; }
        /// <summary>
        /// รหัสสาขาอันเก่า
        /// </summary>
        public string OldBranchID { get; set; }
        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive { get; set; }

        public static BankBranchDTO CreateFromModel(BankBranch model)
        {
            if (model != null)
            {
                var result = new BankBranchDTO()
                {
                    Id = model.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    Name = model.Name,
                    Address = model.Address,
                    Building = model.Building,
                    Soi = model.Soi,
                    Road = model.Road,
                    District = DistrictListDTO.CreateFromModel(model.District),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    PostalCode = model.PostalCode,
                    Telephone = model.Telephone,
                    Fax = model.Fax,
                    IsCreditBank = model.IsCreditBank,
                    IsDirectCredit = model.IsDirectCredit,
                    IsDirectDebit = model.IsDirectDebit,
                    AreaCode = model.AreaCode,
                    OldBankID = model.OldBankID,
                    OldBranchID = model.OldBranchID,
                    IsActive = model.IsActive,
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

        public static BankBranchDTO CreateFromQueryResult(BankBranchQueryResult model)
        {
            if (model != null)
            {
                var result = new BankBranchDTO()
                {
                    Id = model.BankBranch.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    Name = model.BankBranch.Name,
                    Address = model.BankBranch.Address,
                    Building = model.BankBranch.Building,
                    Soi = model.BankBranch.Soi,
                    Road = model.BankBranch.Road,
                    District = DistrictListDTO.CreateFromModel(model.District),
                    SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
                    Province = ProvinceListDTO.CreateFromModel(model.Province),
                    PostalCode = model.BankBranch.PostalCode,
                    Telephone = model.BankBranch.Telephone,
                    Fax = model.BankBranch.Fax,
                    IsCreditBank = model.BankBranch.IsCreditBank,
                    IsDirectCredit = model.BankBranch.IsDirectCredit,
                    IsDirectDebit = model.BankBranch.IsDirectDebit,
                    AreaCode = model.BankBranch.AreaCode,
                    OldBankID = model.BankBranch.OldBankID,
                    OldBranchID = model.BankBranch.OldBranchID,
                    IsActive = model.BankBranch.IsActive,
                    Updated = model.BankBranch.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.Name))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BankBranchDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.Name.CheckLang(true, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankBranchDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                var checkUniqueName = await db.BankBranches.Where(o => o.BankID == this.Bank.Id && o.Name == this.Name && o.ID != this.Id).AnyAsync();
                if (checkUniqueName)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BankBranchDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Name);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            //if (string.IsNullOrEmpty(this.AreaCode))
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BankBranchDTO.AreaCode)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            //else
            //{
            //    if (!this.AreaCode.IsOnlyNumberWithMaxLength(3))
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(BankBranchDTO.AreaCode)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //    var checkUniqueAreaCode = db.BankBranches.Where(o => o.BankID == this.Bank.Id && o.AreaCode == this.AreaCode && o.ID != this.Id).Any();
            //    if (checkUniqueAreaCode)
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(BankBranchDTO.AreaCode)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        msg = msg.Replace("[value]", this.AreaCode);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}
            

            //if (this.Province == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BankBranchDTO.Province)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            //if (this.District == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BankBranchDTO.District)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            //if (this.SubDistrict == null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BankBranchDTO.SubDistrict)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            //if (string.IsNullOrEmpty(this.PostalCode))
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BankBranchDTO.PostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}

            //if (!string.IsNullOrEmpty(this.Telephone))
            //{
            //    if (!this.Telephone.IsOnlyNumber())
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0025").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(BankBranchDTO.Telephone)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}

            //if (!string.IsNullOrEmpty(this.Fax))
            //{
            //    if (!this.Fax.IsOnlyNumber())
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0025").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(BankBranchDTO.Fax)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref BankBranch model)
        {
            model.BankID = this.Bank.Id;
            model.Name = this.Name;
            model.Address = this.Address;
            model.Building = this.Building;
            model.Soi = this.Soi;
            model.Road = this.Road;
            model.SubDistrictID = this.SubDistrict?.Id;
            model.DistrictID = this.District?.Id;
            model.ProvinceID = this.Province?.Id;
            model.PostalCode = this.PostalCode;
            model.Telephone = this.Telephone;
            model.Fax = this.Fax;
            model.IsCreditBank = this.IsCreditBank;
            model.IsDirectCredit = this.IsDirectCredit;
            model.IsDirectDebit = this.IsDirectDebit;
            model.AreaCode = this.AreaCode;
            model.OldBankID = this.OldBankID;
            model.OldBranchID = this.OldBranchID;
            model.IsActive = this.IsActive;
        }

        public static void SortBy(BankBranchSortByParam sortByParam, ref IQueryable<BankBranchQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BankBranchSortBy.Bank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Bank.NameTH);
                        else query = query.OrderByDescending(o => o.Bank.NameTH);
                        break;
                    case BankBranchSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Name);
                        else query = query.OrderByDescending(o => o.BankBranch.Name);
                        break;
                    case BankBranchSortBy.Address:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Address);
                        else query = query.OrderByDescending(o => o.BankBranch.Address);
                        break;
                    case BankBranchSortBy.Building:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Building);
                        else query = query.OrderByDescending(o => o.BankBranch.Building);
                        break;
                    case BankBranchSortBy.Soi:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Soi);
                        else query = query.OrderByDescending(o => o.BankBranch.Soi);
                        break;
                    case BankBranchSortBy.Road:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Road);
                        else query = query.OrderByDescending(o => o.BankBranch.Road);
                        break;
                    case BankBranchSortBy.SubDistrict:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.SubDistrict.NameTH);
                        else query = query.OrderByDescending(o => o.SubDistrict.NameTH);
                        break;
                    case BankBranchSortBy.District:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.District.NameTH);
                        else query = query.OrderByDescending(o => o.District.NameTH);
                        break;
                    case BankBranchSortBy.Province:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Province.NameTH);
                        else query = query.OrderByDescending(o => o.Province.NameTH);
                        break;
                    case BankBranchSortBy.PostalCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.PostalCode);
                        else query = query.OrderByDescending(o => o.BankBranch.PostalCode);
                        break;
                    case BankBranchSortBy.Telephone:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Telephone);
                        else query = query.OrderByDescending(o => o.BankBranch.Telephone);
                        break;
                    case BankBranchSortBy.Fax:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Fax);
                        else query = query.OrderByDescending(o => o.BankBranch.Fax);
                        break;
                    case BankBranchSortBy.IsCreditBank:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.IsCreditBank);
                        else query = query.OrderByDescending(o => o.BankBranch.IsCreditBank);
                        break;
                    case BankBranchSortBy.IsDirectCredit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.IsDirectCredit);
                        else query = query.OrderByDescending(o => o.BankBranch.IsDirectCredit);
                        break;
                    case BankBranchSortBy.IsDirectDebit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.IsDirectDebit);
                        else query = query.OrderByDescending(o => o.BankBranch.IsDirectDebit);
                        break;
                    case BankBranchSortBy.AreaCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.AreaCode);
                        else query = query.OrderByDescending(o => o.BankBranch.AreaCode);
                        break;
                    case BankBranchSortBy.OldBankID:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.OldBankID);
                        else query = query.OrderByDescending(o => o.BankBranch.OldBankID);
                        break;
                    case BankBranchSortBy.OldBranchID:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.OldBranchID);
                        else query = query.OrderByDescending(o => o.BankBranch.OldBranchID);
                        break;
                    case BankBranchSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.Updated);
                        else query = query.OrderByDescending(o => o.BankBranch.Updated);
                        break;
                    case BankBranchSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case BankBranchSortBy.IsActive:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranch.IsActive);
                        else query = query.OrderByDescending(o => o.BankBranch.IsActive);
                        break;
                    default:
                        query = query.OrderBy(o => o.Bank.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Bank.NameTH);
            }
        }
    }

    public class BankBranchQueryResult
    {
        public BankBranch BankBranch { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public SubDistrict SubDistrict { get; set; }
        public Bank Bank { get; set; }
        public User UpdatedBy { get; set; }
    }

}
