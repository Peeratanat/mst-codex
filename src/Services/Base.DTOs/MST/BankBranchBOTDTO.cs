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
    public class BankBranchBOTDTO : BaseDTO
    {
        /// <summary>
        /// ธนาคาร
        /// Master/Banks/DropdownList
        /// </summary>
        public BankDropdownDTO Bank { get; set; }

        public bool IsDeleted { get; set; }

        [Description("รหัสสถาบันการเงิน")]
        public string BankCode { get; set; }
        [Description("ชื่อสถาบันการเงิน")]
        public string BankName { get; set; }

        [Description("รหัสจุดให้บริการ")]
        public string BankBranchCode { get; set; }

        [Description("รหัสสาขา")]
        public string BankServiceCode { get; set; }

        [Description("ชื่อสาขา")]
        public string BankBranchName { get; set; }

        [Description("เลขที่")]
        public string AddressNo { get; set; }

        [Description("อาคาร")]
        public string AddressBuilding { get; set; }

        [Description("หมู่")]
        public string AddressMoo { get; set; }

        [Description("ซอย")]
        public string AddressSoi { get; set; }

        [Description("ถนน")]
        public string AddressRoad { get; set; }

        [Description("ตำบล/แขวง")]
        public string AddressSubDistrict { get; set; }

        [Description("อำเภอ/เขต")]
        public string AddressDistrict { get; set; }

        [Description("จังหวัด")]
        public string AddressProvince { get; set; }

        [Description("รหัสไปรษณีย์")]
        public string AddressPostCode { get; set; }

        [Description("เบอร์โทรศัพท์")]
        public string AddressTelNo { get; set; }

        [Description("วันที่เปิดสาขา/จุดให้บริการ")]
        public string BankBranchStartDate { get; set; }

        [Description("วันทำการ")]
        public string BankBranchWorkingDate { get; set; }

        [Description("วันทำการอื่น ๆ")]
        public string BankBranchWorkingOtherDate { get; set; }

        [Description("เวลาทำการ")]
        public string BankBranchWorkingTime { get; set; }

        [Description("เวลาทำการอื่น ๆ")]
        public string BankBranchWorkingOtherTime { get; set; }

        public static BankBranchBOTDTO CreateFromModel(BankBranchBOT model)
        {
            if (model != null)
            {
                var result = new BankBranchBOTDTO()
                {
                    Id = model.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankName = model.BankName,
                    BankBranchName = model.BankBranchName,
                    BankBranchCode = model.BankBranchCode,
                    BankServiceCode = model.BankServiceCode,

                    AddressBuilding = model.AddressBuilding,
                    AddressMoo = model.AddressMoo,
                    AddressSoi = model.AddressSoi,
                    AddressRoad = model.AddressRoad,
                    AddressSubDistrict = model.AddressSubDistrict,
                    AddressDistrict = model.AddressDistrict,

                    AddressProvince = model.AddressProvince,
                    AddressPostCode = model.AddressPostCode,
                    AddressTelNo = model.AddressTelNo,
                    BankBranchStartDate = model.BankBranchStartDate,
                    BankBranchWorkingDate = model.BankBranchWorkingDate,
                    BankBranchWorkingOtherDate = model.BankBranchWorkingOtherDate,
                    BankBranchWorkingTime = model.BankBranchWorkingTime,
                    BankBranchWorkingOtherTime = model.BankBranchWorkingOtherTime,

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

        public static BankBranchBOTDTO CreateFromQueryResult(BankBranchBOTQueryResult model)
        {
            if (model != null)
            {
                var result = new BankBranchBOTDTO()
                {
                    Id = model.BankBranchBOT.ID,
                    Bank = BankDropdownDTO.CreateFromModel(model.Bank),
                    BankName = model.BankBranchBOT.BankName,
                    BankBranchName = model.BankBranchBOT.BankBranchName,
                    BankBranchCode = model.BankBranchBOT.BankBranchCode,
                    BankServiceCode = model.BankBranchBOT.BankServiceCode,

                    AddressBuilding = model.BankBranchBOT.AddressBuilding,
                    AddressMoo = model.BankBranchBOT.AddressMoo,
                    AddressSoi = model.BankBranchBOT.AddressSoi,
                    AddressRoad = model.BankBranchBOT.AddressRoad,
                    AddressSubDistrict = model.BankBranchBOT.AddressSubDistrict,
                    AddressDistrict = model.BankBranchBOT.AddressDistrict,

                    AddressProvince = model.BankBranchBOT.AddressProvince,
                    AddressPostCode = model.BankBranchBOT.AddressPostCode,
                    AddressTelNo = model.BankBranchBOT.AddressTelNo,
                    BankBranchStartDate = model.BankBranchBOT.BankBranchStartDate,
                    BankBranchWorkingDate = model.BankBranchBOT.BankBranchWorkingDate,
                    BankBranchWorkingOtherDate = model.BankBranchBOT.BankBranchWorkingOtherDate,
                    BankBranchWorkingTime = model.BankBranchBOT.BankBranchWorkingTime,
                    BankBranchWorkingOtherTime = model.BankBranchBOT.BankBranchWorkingOtherTime,

                    Updated = model.BankBranchBOT.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db,bool isAdd)
        {
            ValidateException ex = new ValidateException();
            if (!string.IsNullOrEmpty(this.BankBranchCode))
            {
                var bbCode = await db.BankBranchBOTs.Where(o => o.BankID == this.Bank.Id && o.BankBranchCode == this.BankBranchCode).AnyAsync();
                if (bbCode && isAdd)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    var msg = errMsg.Message.Replace("[field]", "รหัสสาขาธนาคาร");
                    msg = msg.Replace("[value]", "รหัสสาขาธนาคารนี้");
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                } else
                {
                    if (string.IsNullOrEmpty(this.BankBranchName))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(BankBranchBOTDTO.BankBranchName)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                    else
                    {
                        if (!this.BankBranchName.CheckLang(true, true, true, false))
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BankBranchBOTDTO.BankBranchName)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                        var checkUniqueName = await db.BankBranchBOTs.Where(o => o.BankID == this.Bank.Id && o.BankBranchName == this.BankBranchName && o.ID != this.Id).AnyAsync();
                        if (checkUniqueName)
                        {
                            var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                            string desc = this.GetType().GetProperty(nameof(BankBranchBOTDTO.BankBranchName)).GetCustomAttribute<DescriptionAttribute>().Description;
                            var msg = errMsg.Message.Replace("[field]", desc);
                            msg = msg.Replace("[value]", this.BankBranchName);
                            ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                        }
                    }
                }
            }
            else
            {
                ex.AddError("", "กรุณาระบุ รหัสสาขาธนาคาร", 0);
            }
            
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref BankBranchBOT model)
        {
            model.BankID = this.Bank.Id;
            model.BankName = this.BankName;
            model.BankBranchName = this.BankBranchName;
            model.BankBranchCode = this.BankBranchCode;
            model.BankServiceCode = this.BankServiceCode;

            model.AddressBuilding = this.AddressBuilding;
            model.AddressMoo = this.AddressMoo;
            model.AddressSoi = this.AddressSoi;
            model.AddressRoad = this.AddressRoad;
            model.AddressSubDistrict = this.AddressSubDistrict;
            model.AddressDistrict = this.AddressDistrict;

            model.AddressProvince = this.AddressProvince;
            model.AddressPostCode = this.AddressPostCode;
            model.AddressTelNo = this.AddressTelNo;
            model.BankBranchStartDate = this.BankBranchStartDate;
            model.BankBranchWorkingDate = this.BankBranchWorkingDate;
            model.BankBranchWorkingOtherDate = this.BankBranchWorkingOtherDate;
            model.BankBranchWorkingTime = this.BankBranchWorkingTime;
            model.BankBranchWorkingOtherTime = this.BankBranchWorkingOtherTime;
        }

        public static void SortBy(BankBranchBOTSortByParam sortByParam, ref IQueryable<BankBranchBOTQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BankBranchBOTSortBy.BankBranchName:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranchBOT.BankBranchName);
                        else query = query.OrderByDescending(o => o.BankBranchBOT.BankBranchName);
                        break;
                    case BankBranchBOTSortBy.BankBranchCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BankBranchBOT.BankBranchName);
                        else query = query.OrderByDescending(o => o.BankBranchBOT.BankBranchCode);
                        break;
                    default:
                        query = query.OrderBy(o => o.BankBranchBOT.BankBranchName);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.BankBranchBOT.BankBranchName);
            }
        }
    }

    public class BankBranchBOTQueryResult
    {
        public BankBranchBOT BankBranchBOT { get; set; }
        public Bank Bank { get; set; }
        public User UpdatedBy { get; set; }
    }

}
