using Database.Models.MST;
using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class WaiveDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        public DateTime? ActualTransferDate { get; set; }
        /// <summary>
        /// วันที่ Wavie QC
        /// </summary>
        public DateTime? WaiveQCeDate { get; set; }
        /// <summary>
        /// วันที่ผ่าน End Major Product
        /// </summary>
        public DateTime? EndMajoreDate { get; set; }
        /// <summary>
        /// วันที่ผ่าน End Full Product
        /// </summary>
        public DateTime? EndFulleDate { get; set; }
        /// <summary>
        /// วันที่ Waive Sign
        /// </summary>
        public DateTime? WaiveSigneDate { get; set; }
        /// <summary>
        /// วันที่ลูกค้าเข้าอยู่
        /// </summary>
        public DateTime? ArriveDate { get; set; }

        public static WaiveDTO CreateFromModel(WaiveQC model)
        {
            if (model != null)
            {
                var result = new WaiveDTO()
                {
                    Id = model.ID,
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    ActualTransferDate=model.ActualTransferDate,
                    WaiveQCeDate=model.WaiveQCDate,
                    EndMajoreDate=model.EndMajorDate,
                    EndFulleDate=model.EndFullDate,
                    WaiveSigneDate=model.WaiveSignDate,
                    ArriveDate=model.ArriveDate,
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

        public static WaiveDTO CreateFromQueryResult(WaiveQueryResult model)
        {
            if (model != null)
            {
                var result = new WaiveDTO()
                {
                    Id = model.Waive.ID,
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    ActualTransferDate = model.Waive.ActualTransferDate,
                    WaiveQCeDate = model.Waive.WaiveQCDate,
                    EndMajoreDate = model.Waive.EndMajorDate,
                    EndFulleDate = model.Waive.EndFullDate,
                    WaiveSigneDate = model.Waive.WaiveSignDate,
                    ArriveDate = model.Waive.ArriveDate,
                    Updated = model.Waive.Updated,
                    UpdatedBy = model.Waive.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(SortByParam sortByParam, ref IQueryable<WaiveQueryResult> query)
        {
            if (!string.IsNullOrEmpty(sortByParam.SortBy))
            {
                if (sortByParam.SortBy.Equals("Unit", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                    else query = query.OrderByDescending(o => o.Unit.UnitNo);
                }
                else if (sortByParam.SortBy.Equals("ActualTransferDate", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Waive.ActualTransferDate);
                    else query = query.OrderByDescending(o => o.Waive.ActualTransferDate);
                }
                else if (sortByParam.SortBy.Equals("WaiveQCeDate", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Waive.WaiveQCDate);
                    else query = query.OrderByDescending(o => o.Waive.WaiveQCDate);
                }
                else if (sortByParam.SortBy.Equals("EndMajoreDate", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Waive.EndMajorDate);
                    else query = query.OrderByDescending(o => o.Waive.EndMajorDate);
                }
                else if (sortByParam.SortBy.Equals("EndFulleDate", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Waive.EndFullDate);
                    else query = query.OrderByDescending(o => o.Waive.EndFullDate);
                }
                else if (sortByParam.SortBy.Equals("WaiveSigneDate", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Waive.WaiveSignDate);
                    else query = query.OrderByDescending(o => o.Waive.WaiveSignDate);
                }
                else if (sortByParam.SortBy.Equals("ArriveDate", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.Waive.ArriveDate);
                    else query = query.OrderByDescending(o => o.Waive.ArriveDate);
                }
                else if (sortByParam.SortBy.Equals("UnitStatus", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitStatus.Key);
                    else query = query.OrderByDescending(o => o.UnitStatus.Key);
                }
                else
                {
                    query = query.OrderBy(o => o.Unit.UnitNo);
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit.UnitNo);
            }
        }

        public void ToModel(ref WaiveQC model)
        {
            model.UnitID = this.Unit?.Id;
            model.ActualTransferDate = this.ActualTransferDate;
            model.WaiveQCDate = this.WaiveQCeDate;
            model.EndMajorDate = this.EndMajoreDate;
            model.EndFullDate = this.EndFulleDate;
            model.WaiveQCDate = this.WaiveSigneDate;
            model.ArriveDate = this.ArriveDate;
        }
    }
    public class WaiveQueryResult
    {
        public WaiveQC Waive { get; set; }
        public Unit Unit { get; set; }
        public MasterCenter UnitStatus { get; set; }
        
    }
}
