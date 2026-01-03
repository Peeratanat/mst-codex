using Database.Models;
using Database.Models.CMS;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CMS
{
    public class CalculatePerMonthLowRiseDetailDTO : BaseDTO
    {
        /// <summary>
        /// แปลง
        /// </summary>
        [Description("แปลง")]
        public string UnitNo { get; set; }

        /// <summary>
        /// LC ปิดการขาย
        /// </summary>
        [Description("LC ปิดการขาย")]
        public string LCCloseBy { get; set; }

        /// <summary>
        /// LC ประจำโครงการ
        /// </summary>
        [Description("LC ประจำโครงการ")]
        public string LCProjectBy { get; set; }

        /// <summary>
        /// % Rate
        /// </summary>
        [Description("% Rate")]
        public decimal? PerRate { get; set; }

        /// <summary>
        /// Commission Type
        /// </summary>
        [Description("Commission Type")]
        public string CommissionType { get; set; }

        /// <summary>
        /// มูลค่าสัญญา
        /// </summary>
        [Description("มูลค่าสัญญา")]
        public decimal? AgreementAmount { get; set; }

        /// <summary>
        /// วันที่โอนจริง
        /// </summary>
        [Description("วันที่โอนจริง")]
        public DateTime? ActualTransferDate { get; set; }

        /// <summary>
        /// ค่า com ปิดขาย
        /// </summary>
        [Description("ค่า com ปิดขาย")]
        public decimal? TransferCommissionCloseAmount { get; set; }

        /// <summary>
        /// ค่า com ประจำ คก.
        /// </summary>
        [Description("ค่า com ประจำ คก.")]
        public decimal? TransferCommissionProjectAmount { get; set; }

        /// <summary>
        /// ค่า com รวม
        /// </summary>
        [Description("ค่า com รวม")]
        public decimal? TransferCommissionSumAmount { get; set; }

        public static CalculatePerMonthLowRiseDetailDTO CreateFromQueryResult(CalculatePerMonthLowRiseDetailQueryResult model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthLowRiseDetailDTO()
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo,
                    LCCloseBy = model.LCCloseBy,
                    LCProjectBy = model.LCProjectBy,
                    PerRate = model.PerRate,
                    CommissionType = model.CommissionType,
                    AgreementAmount = model.AgreementAmount,
                    ActualTransferDate = model.ActualTransferDate,
                    TransferCommissionCloseAmount = model.TransferCommissionCloseAmount,
                    TransferCommissionProjectAmount = model.TransferCommissionProjectAmount,
                    TransferCommissionSumAmount = model.TransferCommissionSumAmount
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CalculatePerMonthLowRiseDetailSortByParam sortByParam, ref IQueryable<CalculatePerMonthLowRiseDetailQueryResult> query)
        {
            IOrderedQueryable<CalculatePerMonthLowRiseDetailQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {

                    case CalculatePerMonthLowRiseDetailSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.UnitNo);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.LCCloseBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LCCloseBy);
                        else orderQuery = query.OrderByDescending(o => o.LCCloseBy);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.LCProjectBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LCProjectBy);
                        else orderQuery = query.OrderByDescending(o => o.LCProjectBy);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.PerRate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PerRate);
                        else orderQuery = query.OrderByDescending(o => o.PerRate);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.CommissionType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionType);
                        else orderQuery = query.OrderByDescending(o => o.CommissionType);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.AgreementAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.AgreementAmount);
                        else orderQuery = query.OrderByDescending(o => o.AgreementAmount);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.ActualTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ActualTransferDate);
                        else orderQuery = query.OrderByDescending(o => o.ActualTransferDate);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.TransferCommissionCloseAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferCommissionCloseAmount);
                        else orderQuery = query.OrderByDescending(o => o.TransferCommissionCloseAmount);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.TransferCommissionProjectAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferCommissionProjectAmount);
                        else orderQuery = query.OrderByDescending(o => o.TransferCommissionProjectAmount);
                        break;

                    case CalculatePerMonthLowRiseDetailSortBy.TransferCommissionSumAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferCommissionSumAmount);
                        else orderQuery = query.OrderByDescending(o => o.TransferCommissionSumAmount);
                        break;

                    default:
                        orderQuery = query.OrderByDescending(o => o.UnitNo);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.UnitNo);
            }

            orderQuery.ThenBy(o => o.UnitNo);
            query = orderQuery;
        }
    }

    public class CalculatePerMonthLowRiseDetailQueryResult
    {

        public Guid ID { get; set; }
        public string UnitNo { get; set; }
        public string LCCloseBy { get; set; }
        public string LCProjectBy { get; set; }
        public decimal? PerRate { get; set; }
        public string CommissionType { get; set; }
        public decimal? AgreementAmount { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public decimal? TransferCommissionCloseAmount { get; set; }
        public decimal? TransferCommissionProjectAmount { get; set; }
        public decimal? TransferCommissionSumAmount { get; set; }

    }
}
