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
    public class TypeOfRealEstateDTO : BaseDTO
    {
        /// <summary>
        /// รหัสประเภทบ้าน
        /// </summary>
        [Description("รหัสประเภทบ้าน")]
        public string Code { get; set; }
        /// <summary>
        /// ชื่อประเภทบ้าน
        /// </summary>
        [Description("ชื่อประเภทบ้าน")]
        public string Name { get; set; }
        /// <summary>
        /// ลักษณะ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=RealEstateCategory
        /// </summary>
        [Description("ลักษณะ แบบบ้าน")]
        public MasterCenterDropdownDTO RealEstateCategory { get; set; }
        /// <summary>
        /// Standard Cost
        /// </summary>
        public decimal StandardCost { get; set; }
        /// <summary>
        /// Standard Price
        /// </summary>
        public decimal StandardPrice { get; set; }

        public static TypeOfRealEstateDTO CreateFromModel(TypeOfRealEstate model)
        {
            if (model != null)
            {
                var result = new TypeOfRealEstateDTO()
                {
                    Id = model.ID,
                    Code = model.Code,
                    Name = model.Name,
                    RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(model.RealEstateCategory),
                    StandardCost = model.StandardCost,
                    StandardPrice = model.StandardPrice,
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

        public static TypeOfRealEstateDTO CreateFromQueryResult(TypeOfRealEstateQueryResult model)
        {
            if (model != null)
            {
                var result = new TypeOfRealEstateDTO()
                {
                    Id = model.TypeOfRealEstate.ID,
                    Code = model.TypeOfRealEstate.Code,
                    Name = model.TypeOfRealEstate.Name,
                    RealEstateCategory = MasterCenterDropdownDTO.CreateFromModel(model.RealEstateCategory),
                    StandardCost = model.TypeOfRealEstate.StandardCost,
                    StandardPrice = model.TypeOfRealEstate.StandardPrice,
                    Updated = model.TypeOfRealEstate.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(TypeOfRealEstateSortByParam sortByParam, ref IQueryable<TypeOfRealEstateQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case TypeOfRealEstateSortBy.Code:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TypeOfRealEstate.Code);
                        else query = query.OrderByDescending(o => o.TypeOfRealEstate.Code);
                        break;
                    case TypeOfRealEstateSortBy.Name:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TypeOfRealEstate.Name);
                        else query = query.OrderByDescending(o => o.TypeOfRealEstate.Name);
                        break;
                    case TypeOfRealEstateSortBy.RealEstateCategory:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.RealEstateCategory.Name);
                        else query = query.OrderByDescending(o => o.RealEstateCategory.Name);
                        break;
                    case TypeOfRealEstateSortBy.StandardCost:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TypeOfRealEstate.StandardCost);
                        else query = query.OrderByDescending(o => o.TypeOfRealEstate.StandardCost);
                        break;
                    case TypeOfRealEstateSortBy.StandardPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TypeOfRealEstate.StandardPrice);
                        else query = query.OrderByDescending(o => o.TypeOfRealEstate.StandardPrice);
                        break;
                    case TypeOfRealEstateSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TypeOfRealEstate.Updated);
                        else query = query.OrderByDescending(o => o.TypeOfRealEstate.Updated);
                        break;
                    case TypeOfRealEstateSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.TypeOfRealEstate.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.TypeOfRealEstate.Name);
            }
        }
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (string.IsNullOrEmpty(this.Code))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TypeOfRealEstateDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                //if (!this.Code.CheckLang(false, true, false, false))
                //{
                //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0022").FirstAsync();
                //    string desc = this.GetType().GetProperty(nameof(TypeOfRealEstateDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                //    var msg = errMsg.Message.Replace("[field]", desc);
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //}
                var checkUniqueCode = this.Id != (Guid?)null ? await db.TypeOfRealEstates.Where(o => o.Code == this.Code && o.ID != this.Id).CountAsync() > 0 : await db.TypeOfRealEstates.Where(o => o.Code == this.Code).CountAsync() > 0;
                if (checkUniqueCode)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(TypeOfRealEstateDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", this.Code);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
           
            if (!string.IsNullOrEmpty(this.Name))
            {
                //if (!this.Name.CheckLang(false, true, true, false))
                //{
                //    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0005").FirstAsync();
                //    string desc = this.GetType().GetProperty(nameof(TypeOfRealEstateDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                //    var msg = errMsg.Message.Replace("[field]", desc);
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //}
            }
            else
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TypeOfRealEstateDTO.Name)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.RealEstateCategory == null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(TypeOfRealEstateDTO.Code)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref TypeOfRealEstate model)
        {
            model.Code = this.Code;
            model.Name = this.Name;
            model.RealEstateCategoryMasterCenterID = this.RealEstateCategory?.Id;
            model.StandardCost = this.StandardCost;
            model.StandardPrice = this.StandardPrice;
        }
    }
    public class TypeOfRealEstateQueryResult
    {
        public TypeOfRealEstate TypeOfRealEstate { get; set; }
        public MasterCenter RealEstateCategory { get; set; }
        public User UpdatedBy { get; set; }
    }
}
