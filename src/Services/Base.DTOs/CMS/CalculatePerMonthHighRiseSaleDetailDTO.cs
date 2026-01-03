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
    public class CalculatePerMonthHighRiseSaleDetailDTO : BaseDTO
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
        [Description("วันที่อนุมัติสัญญา")]
        public DateTime? AgreementApproveDate { get; set; }

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

        /// <summary>
        /// ค่า commission ณ วันที่จ่าย
        /// </summary>
        [Description("ค่า commission ณ วันที่จ่าย")]
        public decimal? CommissionPayAmount { get; set; }

        public static CalculatePerMonthHighRiseSaleDetailDTO CreateFromQueryResult(CalculatePerMonthHighRiseSaleDetailQueryResult model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthHighRiseSaleDetailDTO()
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo,
                    LCCloseBy = model.LCCloseBy,
                    LCProjectBy = model.LCProjectBy,
                    PerRate = model.PerRate,
                    CommissionType = model.CommissionType,
                    AgreementAmount = model.AgreementAmount,
                    AgreementApproveDate = model.AgreementApproveDate,
                    TransferCommissionCloseAmount = model.TransferCommissionCloseAmount,
                    TransferCommissionProjectAmount = model.TransferCommissionProjectAmount,
                    TransferCommissionSumAmount = model.TransferCommissionSumAmount,
                    CommissionPayAmount = model.CommissionPayAmount

                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CalculatePerMonthHighRiseSaleDetailSortByParam sortByParam, ref IQueryable<CalculatePerMonthHighRiseSaleDetailQueryResult> query)
        {
            IOrderedQueryable<CalculatePerMonthHighRiseSaleDetailQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {

                    case CalculatePerMonthHighRiseSaleDetailSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.UnitNo);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.LCCloseBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LCCloseBy);
                        else orderQuery = query.OrderByDescending(o => o.LCCloseBy);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.LCProjectBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LCProjectBy);
                        else orderQuery = query.OrderByDescending(o => o.LCProjectBy);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.PerRate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PerRate);
                        else orderQuery = query.OrderByDescending(o => o.PerRate);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.CommissionType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionType);
                        else orderQuery = query.OrderByDescending(o => o.CommissionType);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.AgreementAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.AgreementAmount);
                        else orderQuery = query.OrderByDescending(o => o.AgreementAmount);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.AgreementApproveDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.AgreementApproveDate);
                        else orderQuery = query.OrderByDescending(o => o.AgreementApproveDate);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.TransferCommissionCloseAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferCommissionCloseAmount);
                        else orderQuery = query.OrderByDescending(o => o.TransferCommissionCloseAmount);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.TransferCommissionProjectAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferCommissionProjectAmount);
                        else orderQuery = query.OrderByDescending(o => o.TransferCommissionProjectAmount);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.TransferCommissionSumAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.TransferCommissionSumAmount);
                        else orderQuery = query.OrderByDescending(o => o.TransferCommissionSumAmount);
                        break;

                    case CalculatePerMonthHighRiseSaleDetailSortBy.CommissionPayAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionPayAmount);
                        else orderQuery = query.OrderByDescending(o => o.CommissionPayAmount);
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

    public class CalculatePerMonthHighRiseSaleDetailQueryResult
    {
        public Guid ID { get; set; }
        public string UnitNo { get; set; }
        public string LCCloseBy { get; set; }
        public string LCProjectBy { get; set; }
        public decimal? PerRate { get; set; }
        public string CommissionType { get; set; }
        public decimal? AgreementAmount { get; set; }
        public DateTime? AgreementApproveDate { get; set; }
        public decimal? TransferCommissionCloseAmount { get; set; }
        public decimal? TransferCommissionProjectAmount { get; set; }
        public decimal? TransferCommissionSumAmount { get; set; }
        public decimal? CommissionPayAmount { get; set; }

    }
}
