using Base.DTOs.MST;
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
    public class LowRiseBuildingPriceFeeDTO : BaseDTO
    {
        /// <summary>
        ///  แบบบ้าน
        ///  Project/api/Projects/{projectID}/Models/DropdownList
        /// </summary>
        [Description("แบบบ้าน")]
        public ModelDropdownDTO Model { get; set; }
        /// <summary>
        ///  แปลง/ห้อง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        [Description("แปลง")]
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        ///  ราคา
        /// </summary>
        public decimal? Price { get; set; }
        public TypeOfRealEstateDropdownDTO TypeOfRealEstate { get; set; }
        public decimal? BuildingPermitPricePerArea { get; set; }

        public static LowRiseBuildingPriceFeeDTO CreateFromModel(LowRiseBuildingPriceFee model)
        {
            if (model != null)
            {
                var result = new LowRiseBuildingPriceFeeDTO()
                {
                    Id = model.ID,
                    Model = ModelDropdownDTO.CreateFromModel(model.Model),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    Price= model.Price,
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

        public static LowRiseBuildingPriceFeeDTO CreateFromQueryResult(LowRiseBuildingPriceFeeQueryResult model)
        {
            if (model != null)
            {
                var result = new LowRiseBuildingPriceFeeDTO()
                {
                    Id = model.LowRiseBuildingPriceFee.ID,
                    Model = ModelDropdownDTO.CreateFromModel(model.Model),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    Price = model.LowRiseBuildingPriceFee.Price,
                    Updated = model.LowRiseBuildingPriceFee.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    TypeOfRealEstate = TypeOfRealEstateDropdownDTO.CreateFromModel(model.Model?.TypeOfRealEstate),
                    BuildingPermitPricePerArea = model.LowRiseBuildingPriceFee?.BuildingPermitPricePerArea
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(LowRiseBuildingPriceFeeSortByParam sortByParam, ref IQueryable<LowRiseBuildingPriceFeeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LowRiseBuildingPriceFeeSortBy.Model:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.NameTH);
                        else query = query.OrderByDescending(o => o.Model.NameTH);
                        break;
                    case LowRiseBuildingPriceFeeSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case LowRiseBuildingPriceFeeSortBy.Price:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseBuildingPriceFee.Price);
                        else query = query.OrderByDescending(o => o.LowRiseBuildingPriceFee.Price);
                        break;
                    case LowRiseBuildingPriceFeeSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseBuildingPriceFee.Updated);
                        else query = query.OrderByDescending(o => o.LowRiseBuildingPriceFee.Updated);
                        break;
                    case LowRiseBuildingPriceFeeSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case LowRiseBuildingPriceFeeSortBy.TypeOfRealEstate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Model.TypeOfRealEstate.Name);
                        else query = query.OrderByDescending(o => o.Model.TypeOfRealEstate.Name);
                        break;
                    default:
                        query = query.OrderBy(o => o.Model.NameTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Model.NameTH);
            }
        }

        public void ToModel(ref LowRiseBuildingPriceFee model)
        {
            model.ModelID = this.Model?.Id;
            model.UnitID = this.Unit?.Id;
            model.Price = this.Price;
        }
    }
    public class LowRiseBuildingPriceFeeQueryResult
    {
        public LowRiseBuildingPriceFee LowRiseBuildingPriceFee { get; set; }
        public Model Model { get; set; }
        public Unit Unit { get; set; }
        public User UpdatedBy { get; set; }
        public TypeOfRealEstate TypeOfRealEstate { get; set;}
    }
}
