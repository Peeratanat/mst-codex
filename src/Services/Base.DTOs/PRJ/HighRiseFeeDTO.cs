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
    public class HighRiseFeeDTO : BaseDTO
    {
        /// <summary>
        ///  ตึก
        ///  Project/api/Projects/{projectID}/Towers/DropdownList
        /// </summary>
        public TowerDropdownDTO Tower { get; set; }
        /// <summary>
        ///  ชั้น
        ///  Project/api/Projects/{projectID}/Towers/{towerID}/Floors/DropdownList
        /// </summary>
        public FloorDropdownDTO Floor { get; set; }
        /// <summary>
        ///  แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        [Description("เลขที่แปลง")]
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        ///  คำนวณที่จอดรถตามพื้นที่
        ///  Master/api/MasterCenters?masterCenterGroupKey=CalculateParkArea
        /// </summary>
        public MST.MasterCenterDropdownDTO CalculateParkArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่จอดรถ
        /// </summary>
        public decimal? EstimatePriceArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่ใช้สอย
        /// </summary>
        public decimal? EstimatePriceUsageArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่ใช้สอยระเบียง
        /// </summary>
        public decimal? EstimatePriceBalconyArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่วางแอร์
        /// </summary>
        public decimal? EstimatePriceAirArea { get; set; }
        /// <summary>
        ///  ราคาประเมิณพื้นที่สระว่ายน้ำ
        /// </summary>
        public decimal? EstimatePricePoolArea { get; set; }

        public static HighRiseFeeDTO CreateFromModel(HighRiseFee model)
        {
            if (model != null)
            {
                var result = new HighRiseFeeDTO()
                {
                    Id = model.ID,
                    Tower = TowerDropdownDTO.CreateFromModel(model?.Tower),
                    Floor = FloorDropdownDTO.CreateFromModel(model?.Floor),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    CalculateParkArea = MST.MasterCenterDropdownDTO.CreateFromModel(model.CalculateParkArea),
                    EstimatePriceArea = model.EstimatePriceArea,
                    EstimatePriceUsageArea = model.EstimatePriceUsageArea,
                    EstimatePriceBalconyArea = model.EstimatePriceBalconyArea,
                    EstimatePriceAirArea = model.EstimatePriceAirArea,
                    EstimatePricePoolArea = model.EstimatePricePoolArea,
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

        public static HighRiseFeeDTO CreateFromQueryResult(HighRiseFeeQueryResult model)
        {
            if (model != null)
            {
                var result = new HighRiseFeeDTO()
                {
                    Id = model.HighRiseFee?.ID,
                    Tower = TowerDropdownDTO.CreateFromModel(model.Tower),
                    Floor = FloorDropdownDTO.CreateFromModel(model.Floor),
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    CalculateParkArea = MST.MasterCenterDropdownDTO.CreateFromModel(model.CalculateParkArea),
                    EstimatePriceArea = model.HighRiseFee?.EstimatePriceArea,
                    EstimatePriceUsageArea = model.HighRiseFee?.EstimatePriceUsageArea,
                    EstimatePriceBalconyArea = model.HighRiseFee?.EstimatePriceBalconyArea,
                    EstimatePriceAirArea = model.HighRiseFee?.EstimatePriceAirArea,
                    EstimatePricePoolArea = model.HighRiseFee?.EstimatePricePoolArea,
                    Updated = model.HighRiseFee?.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(HighRiseFeeSortByParam sortByParam, ref IQueryable<HighRiseFeeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case HighRiseFeeSortBy.Tower:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Tower.TowerCode);
                        else query = query.OrderByDescending(o => o.Tower.TowerCode);
                        break;
                    case HighRiseFeeSortBy.Floor:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Floor.NameTH);
                        else query = query.OrderByDescending(o => o.Floor.NameTH);
                        break;
                    case HighRiseFeeSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case HighRiseFeeSortBy.CalculateParkArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.CalculateParkArea.Name);
                        else query = query.OrderByDescending(o => o.CalculateParkArea.Name);
                        break;
                    case HighRiseFeeSortBy.EstimatePriceArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.HighRiseFee.EstimatePriceArea);
                        else query = query.OrderByDescending(o => o.HighRiseFee.EstimatePriceArea);
                        break;
                    case HighRiseFeeSortBy.EstimatePriceUsageArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.HighRiseFee.EstimatePriceUsageArea);
                        else query = query.OrderByDescending(o => o.HighRiseFee.EstimatePriceUsageArea);
                        break;
                    case HighRiseFeeSortBy.EstimatePriceBalconyArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.HighRiseFee.EstimatePriceBalconyArea);
                        else query = query.OrderByDescending(o => o.HighRiseFee.EstimatePriceBalconyArea);
                        break;
                    case HighRiseFeeSortBy.EstimatePriceAirArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.HighRiseFee.EstimatePriceAirArea);
                        else query = query.OrderByDescending(o => o.HighRiseFee.EstimatePriceAirArea);
                        break;
                    case HighRiseFeeSortBy.EstimatePricePoolArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.HighRiseFee.EstimatePricePoolArea);
                        else query = query.OrderByDescending(o => o.HighRiseFee.EstimatePricePoolArea);
                        break;
                    case HighRiseFeeSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.HighRiseFee.Updated);
                        else query = query.OrderByDescending(o => o.HighRiseFee.Updated);
                        break;
                    case HighRiseFeeSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Tower.TowerNoTH);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Tower.TowerNoTH);
            }
        }

        public void ToModel(ref HighRiseFee model)
        {
            model.TowerID = this.Tower?.Id;
            model.FloorID = this.Floor?.Id;
            model.UnitID = this.Unit?.Id;
            model.CalculateParkAreaMasterCenterID = this.CalculateParkArea?.Id;
            model.EstimatePriceArea = this.EstimatePriceArea ?? 0;
            model.EstimatePriceUsageArea = this.EstimatePriceUsageArea ?? 0;
            model.EstimatePriceBalconyArea = this.EstimatePriceBalconyArea ?? 0;
            model.EstimatePriceAirArea = this.EstimatePriceAirArea ?? 0;
            model.EstimatePricePoolArea = this.EstimatePricePoolArea ?? 0;
        }
    }

    public class HighRiseFeeQueryResult
    {
        public Tower Tower { get; set; }
        public Floor Floor { get; set; }
        public Unit Unit { get; set; }
        public MasterCenter CalculateParkArea { get; set; }
        public HighRiseFee HighRiseFee { get; set; }
        public User UpdatedBy { get; set; }
    }
}
