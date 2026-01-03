using Database.Models;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Linq;
namespace Base.DTOs.PRJ
{
    /// <summary>
    /// Model: BudgetMinPriceUnit
    /// </summary>
    public class BudgetMinPriceUnitDTO
    {
        /// <summary>
        /// แปลง
        /// </summary>
        public UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal UnitAmount { get; set; }
        /// <summary>
        /// OldAmount
        /// </summary>
        public decimal OldUnitAmount { get; set; }
        /// <summary>
        /// ข้อมูลผิด
        /// </summary>
        public bool isCorrected { get; set; }
        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string UpdatedBy { get; set; }
        public Guid? UpdatedByUserID { get; set; }

        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// แก้ไขโดย
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// สามารถแก้ไขได้
        /// </summary>
        public bool? isCanEdit { get; set; }

        public static BudgetMinPriceUnitDTO CreateFromQueryResult(BudgetMinPriceUnitQueryResult model)
        {
            if (model != null)
            {
                var result = new BudgetMinPriceUnitDTO()
                {
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    UnitAmount = model.BudgetMinPriceUnit?.Amount ?? 0.000m,
                    Updated = model.BudgetMinPriceUnit?.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                if (result.Unit.UnitStatus.Key.Equals(UnitStatusKeys.WaitingForConfirmBooking) || result.Unit.UnitStatus.Key.Equals(UnitStatusKeys.Available))
                {
                    result.Unit.UnitStatus.Name = "ว่าง";
                }
                else if (result.Unit.UnitStatus.Key.Equals(UnitStatusKeys.WaitingForAgreement))
                {
                    result.Unit.UnitStatus.Name = "จอง";
                }
                else if (result.Unit.UnitStatus.Key.Equals(UnitStatusKeys.WaitingForTransfer))
                {
                    result.Unit.UnitStatus.Name = "ทำสัญญา";
                }
                else if (result.Unit.UnitStatus.Key.Equals(UnitStatusKeys.Transfer))
                {
                    result.Unit.UnitStatus.Name = "โอนกรรมสิทธิ์";
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static BudgetMinPriceUnitDTO CreateFromModel(BudgetMinPriceUnit model)
        {
            if (model != null)
            {
                var result = new BudgetMinPriceUnitDTO()
                {
                    Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
                    UnitAmount = model.Amount ?? 0,
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

        public void ToModel(ref BudgetMinPriceUnit model)
        {
            model.Amount = this.UnitAmount;
            model.UnitID = this.Unit.Id;
            model.UpdatedByUserID = this.UpdatedByUserID;

        }
        public static void SortBy(BudgetMinPriceSortByParam sortByParam, ref IQueryable<BudgetMinPriceUnitQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case BudgetMinPriceSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case BudgetMinPriceSortBy.Amount:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BudgetMinPriceUnit.Amount);
                        else query = query.OrderByDescending(o => o.BudgetMinPriceUnit.Amount);
                        break;
                    case BudgetMinPriceSortBy.Status:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name);
                        break;
                    case BudgetMinPriceSortBy.Updated:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.BudgetMinPriceUnit.Updated);
                        else query = query.OrderByDescending(o => o.BudgetMinPriceUnit.Updated);
                        break;
                    case BudgetMinPriceSortBy.UpdatedBy:
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



    }

    public class BudgetMinPriceUnitQueryResult
    {
        public Unit Unit { get; set; }
        public BudgetMinPriceUnit BudgetMinPriceUnit { get; set; }
        public User UpdatedBy { get; set; }
    }

}

