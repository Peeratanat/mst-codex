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
    public class CalculatePerMonthHighRiseSaleDTO : BaseDTO
    {
        /// <summary>
        /// Commission For
        /// </summary>
        [Description("Commission For")]
        public DateTime? PeriodMonth { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        [Description("โครงการ")]
        public PRJ.ProjectDropdownDTO Project { get; set; }

        /// <summary>
        /// คำนวณโดย
        /// </summary>
        [Description("คำนวณโดย")]
        public USR.UserListDTO CalculateUserName { get; set; }

        /// <summary>
        /// วันที่คำนวณ
        /// </summary>
        [Description("วันที่คำนวณ")]
        public DateTime? CalculateDate { get; set; }

        /// <summary>
        /// Commission Type
        /// </summary>
        [Description("Commission Type")]
        public string CommissionType { get; set; }

        /// <summary>
        /// สถานะ
        /// </summary>
        [Description("สถานะ")]
        public string Status { get; set; }

        /// <summary>
        /// ผลรวมมูลค่าสัญญาในเดือนนี้
        /// </summary>
        [Description("ผลรวมมูลค่าสัญญาในเดือนนี้")]
        public decimal? TotalContractAmount { get; set; }

        /// <summary>
        /// ผลรวมมูลค่าสัญญาที่ยกเลิกในเดือนนี้
        /// </summary>
        [Description("ผลรวมมูลค่าสัญญาที่ยกเลิกในเดือนนี้")]
        public decimal? TotalContractCancelAmount { get; set; }

        /// <summary>
        /// ผลรวมมูลค่าสัญญาสุทธิในเดือนนี้
        /// </summary>
        [Description("ผลรวมมูลค่าสัญญาสุทธิในเดือนนี้")]
        public decimal? TotalContractNetAmount { get; set; }

        /// <summary>
        /// % Commission
        /// </summary>
        [Description("% Commission")]
        public decimal? CommissionPercentRate { get; set; }

        /// <summary>
        /// รายละเอียดผลการคำนวณ
        /// </summary>
        [Description("รายละเอียดผลการคำนวณ")]
        public List<CommissionHighRiseSaleVeiwDTO> CommissionHighRiseSaleVeiws { get; set; }

        public static CalculatePerMonthHighRiseSaleDTO CreateFromModel(CalculatePerMonthHighRiseSale model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthHighRiseSaleDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    PeriodMonth = new DateTime(model.PeriodYear, model.PeriodMonth, 1),
                    CalculateUserName = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CalculateDate = model.Created,
                    Status = ((model.IsApprove.HasValue && model.IsApprove.Value) ? "อนุมัติแล้ว" : "ยังไม่อนุมัติ"),
                    TotalContractAmount = model.TotalContractAmount,
                    TotalContractCancelAmount = model.TotalContractCancelAmount,
                    TotalContractNetAmount = model.TotalContractNetAmount,
                    CommissionPercentRate = model.CommissionPercentRate
                };
                return result;
            }
            else
            {
                return null;
            }
        }

        public static CalculatePerMonthHighRiseSaleDTO CreateFromQueryResult(CalculatePerMonthHighRiseSaleQueryResult model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthHighRiseSaleDTO()
                {
                    Id = model.CalculatePerMonthHighRiseSale?.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.CalculatePerMonthHighRiseSale.Project),
                    PeriodMonth = new DateTime(model.CalculatePerMonthHighRiseSale.PeriodYear, model.CalculatePerMonthHighRiseSale.PeriodMonth, 1),
                    CalculateUserName = USR.UserListDTO.CreateFromModel(model.CalculatePerMonthHighRiseSale.CreatedBy),
                    CalculateDate = model.CalculatePerMonthHighRiseSale.Created,
                    Status = ((model.CalculatePerMonthHighRiseSale.IsApprove.HasValue && model.CalculatePerMonthHighRiseSale.IsApprove.Value) ? "อนุมัติแล้ว" : "ยังไม่อนุมัติ"),
                    TotalContractAmount = model.CalculatePerMonthHighRiseSale.TotalContractAmount,
                    TotalContractCancelAmount = model.CalculatePerMonthHighRiseSale.TotalContractCancelAmount,
                    TotalContractNetAmount = model.CalculatePerMonthHighRiseSale.TotalContractNetAmount,
                    CommissionPercentRate = model.CalculatePerMonthHighRiseSale.CommissionPercentRate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CalculatePerMonthHighRiseSaleSortByParam sortByParam, ref IQueryable<CalculatePerMonthHighRiseSaleQueryResult> query)
        {
            IOrderedQueryable<CalculatePerMonthHighRiseSaleQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CalculatePerMonthHighRiseSaleSortBy.PeriodMonth:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseSale.PeriodYear).ThenBy(o => o.CalculatePerMonthHighRiseSale.PeriodMonth);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseSale.PeriodYear).ThenByDescending(o => o.CalculatePerMonthHighRiseSale.PeriodMonth);
                        break;
                    case CalculatePerMonthHighRiseSaleSortBy.CalculateUserName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseSale.CreatedBy.DisplayName);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseSale.CreatedBy.DisplayName);
                        break;
                    case CalculatePerMonthHighRiseSaleSortBy.CalculateDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseSale.Created);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseSale.Created);
                        break;
                    case CalculatePerMonthHighRiseSaleSortBy.Status:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseSale.IsApprove);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseSale.IsApprove);
                        break;
                    case CalculatePerMonthHighRiseSaleSortBy.CommissionType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthHighRiseSale.CommissionPercentType);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseSale.CommissionPercentType);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseSale.PeriodYear).ThenByDescending(o => o.CalculatePerMonthHighRiseSale.PeriodMonth);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculatePerMonthHighRiseSale.PeriodYear).ThenByDescending(o => o.CalculatePerMonthHighRiseSale.PeriodMonth);
            }

            orderQuery.ThenBy(o => o.CalculatePerMonthHighRiseSale.ID);
            query = orderQuery;
        }
    }

    public class CalculatePerMonthHighRiseSaleQueryResult
    {
        public CalculatePerMonthHighRiseSale CalculatePerMonthHighRiseSale { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User CalculateUserName { get; set; }
    }
}
