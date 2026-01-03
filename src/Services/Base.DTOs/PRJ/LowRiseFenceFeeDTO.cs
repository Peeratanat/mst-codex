using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class LowRiseFenceFeeDTO : BaseDTO
    {
        /// <summary>
        ///  สำนักงานที่ดิน
        ///  Master/api/LandOffices/DropdownList
        /// </summary>
        [Description("สำนักงานที่ดิน")]
        public MST.LandOfficeListDTO LandOffice { get; set; }
        /// <summary>
        ///  ประเภทบ้าน
        ///  Master/api/TypeOfRealEstates/DropdownList
        /// </summary>
        [Description("ประเภทบ้าน")]
        public MST.TypeOfRealEstateDropdownDTO TypeOfRealEstate { get; set; }
        /// <summary>
        ///  อัตราค่ารั้วคอนกรีต
        /// </summary>
        public double? ConcreteRate { get; set; }
        /// <summary>
        ///  อัตราค่ารั้วเหล็ก
        /// </summary>
        public double? IronRate { get; set; }
        /// <summary>
        ///  ราคารั้วเหล็ก
        /// </summary>
        public decimal? IronPrice { get; set; }
        /// <summary>
        /// ราคารั้วคอนกรีต
        /// </summary>
        public decimal? ConcretePrice { get; set; }
        /// <summary>
        ///  ค่าเสื่อมราคาต่อปี
        /// </summary>
        public decimal? DepreciationPerYear { get; set; }
        /// <summary>
        ///  คำนวนค่าเสื่อมรั้ว
        /// </summary>
        public bool IsCalculateDepreciation { get; set; }

        public static LowRiseFenceFeeDTO CreateFromModel(LowRiseFenceFee model)
        {
            if (model != null)
            {
                var result = new LowRiseFenceFeeDTO()
                {
                    Id = model.ID,
                    LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.LandOffice),
                    TypeOfRealEstate = MST.TypeOfRealEstateDropdownDTO.CreateFromModel(model.TypeOfRealEstate),
                    ConcreteRate = model.ConcreteRate,
                    IronRate=model.IronRate,
                    ConcretePrice = model.ConcretePrice,
                    IronPrice = model.IronPrice,
                    DepreciationPerYear = model.DepreciationPerYear,
                    IsCalculateDepreciation = model.IsCalculateDepreciation,
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

        public static LowRiseFenceFeeDTO CreateFromQueryResult(LowRiseFenceFeeQueryResult model)
        {
            if (model != null)
            {
                var result = new LowRiseFenceFeeDTO()
                {
                    Id = model.LowRiseFenceFee.ID,
                    LandOffice = MST.LandOfficeListDTO.CreateFromModel(model.LandOffice),
                    TypeOfRealEstate = MST.TypeOfRealEstateDropdownDTO.CreateFromModel(model.TypeOfRealEstate),
                    ConcreteRate = model.LowRiseFenceFee.ConcreteRate,
                    ConcretePrice = model.LowRiseFenceFee.ConcretePrice,
                    IronRate=model.LowRiseFenceFee.IronRate,
                    IronPrice = model.LowRiseFenceFee.IronPrice,
                    DepreciationPerYear = model.LowRiseFenceFee.DepreciationPerYear,
                    IsCalculateDepreciation = model.LowRiseFenceFee.IsCalculateDepreciation,
                    Updated = model.LowRiseFenceFee.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(LowRiseFenceFeeSortByParam sortByParam, ref IQueryable<LowRiseFenceFeeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LowRiseFenceFeeSortBy.LandOffice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LandOffice.NameTH);
                        else query = query.OrderByDescending(o => o.LandOffice.NameTH);
                        break;
                    case LowRiseFenceFeeSortBy.TypeOfRealEstate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.TypeOfRealEstate.Name);
                        else query = query.OrderByDescending(o => o.TypeOfRealEstate.Name);
                        break;
                    case LowRiseFenceFeeSortBy.ConcreteRate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFenceFee.ConcreteRate);
                        else query = query.OrderByDescending(o => o.LowRiseFenceFee.ConcreteRate);
                        break;
                    case LowRiseFenceFeeSortBy.ConcretePrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFenceFee.ConcretePrice);
                        else query = query.OrderByDescending(o => o.LowRiseFenceFee.ConcretePrice);
                        break;
                    case LowRiseFenceFeeSortBy.IronRate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFenceFee.IronRate);
                        else query = query.OrderByDescending(o => o.LowRiseFenceFee.IronRate);
                        break;
                    case LowRiseFenceFeeSortBy.IronPrice:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFenceFee.IronPrice);
                        else query = query.OrderByDescending(o => o.LowRiseFenceFee.IronPrice);
                        break;
                    case LowRiseFenceFeeSortBy.DepreciationPerYear:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFenceFee.DepreciationPerYear);
                        else query = query.OrderByDescending(o => o.LowRiseFenceFee.DepreciationPerYear);
                        break;
                    case LowRiseFenceFeeSortBy.IsCalculateDepreciation:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFenceFee.IsCalculateDepreciation);
                        else query = query.OrderByDescending(o => o.LowRiseFenceFee.IsCalculateDepreciation);
                        break;
                    case LowRiseFenceFeeSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFenceFee.Updated);
                        else query = query.OrderByDescending(o => o.LowRiseFenceFee.Updated);
                        break;
                    case LowRiseFenceFeeSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.LandOffice.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.LandOffice.NameTH);
            }
        }

        public void ToModel(ref LowRiseFenceFee model)
        {
            model.LandOfficeID = this.LandOffice?.Id;
            model.TypeOfRealEstateID = this.TypeOfRealEstate?.Id;
            model.ConcreteRate = this.ConcreteRate;
            model.ConcretePrice = this.ConcretePrice;
            model.IronPrice = this.IronPrice;
            model.IronRate = this.IronRate;
            model.DepreciationPerYear = this.DepreciationPerYear;
            model.IsCalculateDepreciation = this.IsCalculateDepreciation;
        }
    }
    public class LowRiseFenceFeeQueryResult
    {
        public LowRiseFenceFee LowRiseFenceFee { get; set; }
        public LandOffice LandOffice { get; set; }
        public TypeOfRealEstate TypeOfRealEstate { get; set; }
        public User UpdatedBy { get; set; }
    }
}
