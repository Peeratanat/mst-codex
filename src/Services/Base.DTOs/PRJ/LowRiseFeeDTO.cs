using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class LowRiseFeeDTO : BaseDTO
    {
        /// <summary>
        ///  แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        [Description("เลขที่แปลง")]
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        ///  ราคาประเมิณที่ดิน
        /// </summary>
        public decimal? EstimatePriceArea { get; set; }

        public static LowRiseFeeDTO CreateFromModel(LowRiseFee model)
        {
            if (model != null)
            {
                var result = new LowRiseFeeDTO()
                {
                    Id = model.ID,
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    EstimatePriceArea = model.EstimatePriceArea,
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

        public static LowRiseFeeDTO CreateFromQueryResult(LowRiseFeeQueryResult model)
        {
            if (model != null)
            {
                var result = new LowRiseFeeDTO()
                {
                    Id = model.LowRiseFee.ID,
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    EstimatePriceArea = model.LowRiseFee.EstimatePriceArea,
                    Updated = model.LowRiseFee.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(LowRiseFeeSortByParam sortByParam, ref IQueryable<LowRiseFeeQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case LowRiseFeeSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case LowRiseFeeSortBy.EstimatePriceArea:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFee.EstimatePriceArea);
                        else query = query.OrderByDescending(o => o.LowRiseFee.EstimatePriceArea);
                        break;
                    case LowRiseFeeSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.LowRiseFee.Updated);
                        else query = query.OrderByDescending(o => o.LowRiseFee.Updated);
                        break;
                    case LowRiseFeeSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    default:
                        query = query.OrderBy(o => o.Unit.UnitNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit.UnitNo);
            }
        }

        public void ToModel(ref LowRiseFee model)
        { 
            model.UnitID = this.Unit?.Id;
            model.EstimatePriceArea = this.EstimatePriceArea;      
        }
    }
    public class LowRiseFeeQueryResult
    {
        public LowRiseFee LowRiseFee { get; set; }
        public Unit Unit { get; set; }
        public User UpdatedBy { get; set; }
    }
}
