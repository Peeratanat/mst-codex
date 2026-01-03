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
    public class CalculatePerMonthHighRiseTransferDetailDTO : BaseDTO
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
        /// ค่า commission โอน
        /// </summary>
        [Description("ค่า commission โอน")]
        public decimal? CommissionTransferAmount { get; set; }

        /// <summary>
        /// ค่า commission ณ วันที่จ่าย
        /// </summary>
        [Description("ค่า commission ณ วันที่จ่าย")]
        public decimal? CommissionPayAmount { get; set; }

        public static CalculatePerMonthHighRiseTransferDetailDTO CreateFromQueryResult(CalculatePerMonthHighRiseTransferDetailQueryResult model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthHighRiseTransferDetailDTO()
                {
                    Id = model.ID,
                    UnitNo = model.UnitNo,
                    LCCloseBy = model.LCCloseBy,
                    LCProjectBy = model.LCProjectBy,
                    PerRate = model.PerRate,
                    CommissionType = model.CommissionType,
                    AgreementAmount = model.AgreementAmount,
                    ActualTransferDate = model.ActualTransferDate,
                    CommissionTransferAmount = model.CommissionTransferAmount,
                    CommissionPayAmount = model.CommissionPayAmount
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CalculatePerMonthHighRiseTransferDetailSortByParam sortByParam, ref IQueryable<CalculatePerMonthHighRiseTransferDetailQueryResult> query)
        {
            IOrderedQueryable<CalculatePerMonthHighRiseTransferDetailQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {

                    case CalculatePerMonthHighRiseTransferDetailSortBy.UnitNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.UnitNo);
                        else orderQuery = query.OrderByDescending(o => o.UnitNo);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.LCCloseBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LCCloseBy);
                        else orderQuery = query.OrderByDescending(o => o.LCCloseBy);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.LCProjectBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.LCProjectBy);
                        else orderQuery = query.OrderByDescending(o => o.LCProjectBy);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.PerRate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.PerRate);
                        else orderQuery = query.OrderByDescending(o => o.PerRate);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.CommissionType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionType);
                        else orderQuery = query.OrderByDescending(o => o.CommissionType);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.AgreementAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.AgreementAmount);
                        else orderQuery = query.OrderByDescending(o => o.AgreementAmount);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.ActualTransferDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ActualTransferDate);
                        else orderQuery = query.OrderByDescending(o => o.ActualTransferDate);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.CommissionTransferAmount:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CommissionTransferAmount);
                        else orderQuery = query.OrderByDescending(o => o.CommissionTransferAmount);
                        break;

                    case CalculatePerMonthHighRiseTransferDetailSortBy.CommissionPayAmount:
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

    public class CalculatePerMonthHighRiseTransferDetailQueryResult
    {
        public Guid ID { get; set; }
        public string UnitNo { get; set; }
        public string LCCloseBy { get; set; }
        public string LCProjectBy { get; set; }
        public decimal? PerRate { get; set; }
        public string CommissionType { get; set; }
        public decimal? AgreementAmount { get; set; }
        public DateTime? ActualTransferDate { get; set; }
        public decimal? CommissionTransferAmount { get; set; }
        public decimal? CommissionPayAmount { get; set; }

    }
}
