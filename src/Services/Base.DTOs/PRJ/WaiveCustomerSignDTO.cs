using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class WaiveCustomerSignDTO : BaseDTO
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
        /// วันที่ Waive Sign
        /// </summary>
        public DateTime? WaiveSignDate { get; set; }
        /// <summary>
        /// เปิด-ปิดแก้ไข (รายการที่ตั้งโอน)
        /// </summary>
        public bool IsAction { get; set; }
        public static WaiveCustomerSignDTO CreateFromModel(WaiveQC model)
        {
            if (model != null)
            {
                var result = new WaiveCustomerSignDTO()
                {
                    Id = model.ID,
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    ActualTransferDate = model.ActualTransferDate,
                    WaiveSignDate = model.WaiveSignDate,
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

        public static WaiveCustomerSignDTO CreateFromQueryResult(WaiveCustomerSignQueryResult model, List<string> unitsNo)
        {
            if (model != null)
            {
                var result = new WaiveCustomerSignDTO()
                {
                    Id = model.WaiveQC?.ID,
                    Unit = UnitDropdownDTO.Create2ParameterFromModel(model.Unit, model.MasterCenter),
                    ActualTransferDate = model.WaiveQC?.ActualTransferDate,
                    WaiveSignDate = model.WaiveQC?.WaiveSignDate,
                    Updated = model.WaiveQC?.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    //IsAction = unitsNo.Contains(model.Unit?.UnitNo) ? false : true
                    //IsAction = model.MasterCenter?.Order > 4 ? false : true
                    //IsAction = model.WaiveQC.WaiveQCDate == null ? false : true
                    IsAction = model.MasterCenter?.Order > 4 ? false : true
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static WaiveCustomerSignDTO CreateFromQueryTempResult(WaiveCustomerSignQueryResult model)
        {
            if (model != null)
            {
                var result = new WaiveCustomerSignDTO();
                result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref WaiveQC model, bool isUpdate)
        {
            if (!isUpdate)
            {
                model.UnitID = this.Unit?.Id;
                model.ActualTransferDate = this.ActualTransferDate;
            }
            model.WaiveSignDate = this.WaiveSignDate;
        }

        public static void SortBy(WaiveCustomerSignSortByParam sortByParam, ref IQueryable<WaiveCustomerSignQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case WaiveCustomerSignSortBy.Unit:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case WaiveCustomerSignSortBy.Unit_UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name);
                        break;
                    case WaiveCustomerSignSortBy.ActualTransferDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WaiveQC.ActualTransferDate);
                        else query = query.OrderByDescending(o => o.WaiveQC.ActualTransferDate);
                        break;
                    case WaiveCustomerSignSortBy.WaiveSignDate:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WaiveQC.WaiveSignDate);
                        else query = query.OrderByDescending(o => o.WaiveQC.WaiveSignDate);
                        break;
                    case WaiveCustomerSignSortBy.UpdatedBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
                        else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
                        break;
                    case WaiveCustomerSignSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.WaiveQC.Updated);
                        else query = query.OrderByDescending(o => o.WaiveQC.Updated);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Unit.UnitNo);
            }
        }
    }
    public class WaiveCustomerSignQueryResult
    {
        public Model Model { get; set; }
        public Unit Unit { get; set; }
        public WaiveQC WaiveQC { get; set; }
        public User UpdatedBy { get; set; }
        public MasterCenter MasterCenter { get; set; }
    }
}
