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
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class BrandDTO : BaseDTO
    {
        /// <summary>
        /// รหัสแบรนด์
        /// </summary>
        [Description("รหัสแบรนด์")]
        public string BrandNo { get; set; }
        /// <summary>
        /// ชื่อแบรนด์
        /// </summary>
        [Description("ชื่อแบรนด์")]
        public string Name { get; set; }
        /// <summary>
        /// รูปแบบเลขที่แปลง
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=UnitNumberFormat
        /// </summary>
        [Description("รูปแบบเลขที่แปลง")]
        public MST.MasterCenterDropdownDTO UnitNumberFormat { get; set; }

        public static BrandDTO CreateFromModel(Brand model)
        {
            if (model != null)
            {
                var result = new BrandDTO()
                {
                    Id = model.ID,
                    BrandNo = model.BrandNo,
                    Name = model.Name,
                    UnitNumberFormat = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitNumberFormat),
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

        public static BrandDTO CreateFromQueryResult(BrandQueryResult model)
        {
            if (model != null)
            {
                var result = new BrandDTO()
                {
                    Id = model.Brand.ID,
                    BrandNo = model.Brand.BrandNo,
                    UnitNumberFormat = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitNumberFormat),
                    Name = model.Brand.Name,
                    Updated = model.Brand.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(BrandSortByParam sortByParam, ref IQueryable<BrandQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BrandSortBy.BrandNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Brand.BrandNo);
                        else query = query.OrderByDescending(o => o.Brand.BrandNo);
                        break;
                    case BrandSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Brand.Name);
                        else query = query.OrderByDescending(o => o.Brand.Name);
                        break;
                    case BrandSortBy.UnitNumberFormat:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitNumberFormat.Name);
                        else query = query.OrderByDescending(o => o.UnitNumberFormat.Name);
                        break;
                    case BrandSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Brand.Updated);
                        else query = query.OrderByDescending(o => o.Brand.Updated);
                        break;
                    case BrandSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Brand.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.Brand.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Brand.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Brand.Name);
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            // รหัสแบรนด์
            if (string.IsNullOrEmpty(this.BrandNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.BrandNo.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }

                var checkUniqueBrandNo = this.Id != (Guid?)null ? await db.Brands.Where(o => o.BrandNo == this.BrandNo && o.ID != this.Id).CountAsync() > 0 : await db.Brands.Where(o => o.BrandNo == this.BrandNo).CountAsync() > 0;
                if (checkUniqueBrandNo)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.BrandNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            // ชื่อ
            if (string.IsNullOrEmpty(this.Name))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BrandDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // รูปแบบเลขที่แปลง
            //if (this.UnitNumberFormat==null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BrandDTO.UnitNumberFormat)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public async Task ValidateUpdateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            // รหัสแบรนด์
            if (string.IsNullOrEmpty(this.BrandNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.BrandNo.CheckLang(false, true, false, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
                //แบรนด์ถูกนำไปใช้ และรหัสแบรนด์ไม่เหมือนกับในระบบ
                var projectUsed = db.Projects.Where(o => o.BrandID == this.Id).FirstOrDefault();
                if (projectUsed != null)
                {
                    var brandNo = db.Brands.Where(o => o.ID == this.Id && o.BrandNo == this.BrandNo).FirstOrDefault();
                    if (brandNo == null)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0140").FirstAsync();
                        string desc = typeof(BrandDTO).GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }

                var checkUniqueBrandNo = this.Id != (Guid?)null ? await db.Brands.Where(o => o.BrandNo == this.BrandNo && o.ID != this.Id).CountAsync() > 0 : await db.Brands.Where(o => o.BrandNo == this.BrandNo).CountAsync() > 0;
                if (checkUniqueBrandNo)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.BrandNo);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            // ชื่อ
            if (string.IsNullOrEmpty(this.Name))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(BrandDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // รูปแบบเลขที่แปลง
            //if (this.UnitNumberFormat==null)
            //{
            //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
            //    string desc = this.GetType().GetProperty(nameof(BrandDTO.UnitNumberFormat)).GetCustomAttribute<DescriptionAttribute>().Description;
            //    var msg = errMsg.Message.Replace("[field]", desc);
            //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //}
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref Brand model)
        {
            model.BrandNo = this.BrandNo;
            model.UnitNumberFormatMasterCenterID = this.UnitNumberFormat?.Id;
            model.Name = this.Name;
        }
    }

    public class BrandQueryResult
    {
        public Brand Brand { get; set; }
        public MasterCenter UnitNumberFormat { get; set; }
        public User UpdatedBy { get; set; }
    }
}
