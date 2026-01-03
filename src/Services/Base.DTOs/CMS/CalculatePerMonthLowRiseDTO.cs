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
    public class CalculatePerMonthLowRiseDTO : BaseDTO
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
        public List<CommissionLowRiseVeiwDTO> CommissionLowRiseVeiws { get; set; }

        public static CalculatePerMonthLowRiseDTO CreateFromModel(CalculatePerMonthLowRise model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthLowRiseDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.Project),
                    PeriodMonth = new DateTime(model.PeriodYear, model.PeriodMonth, 1),
                    CalculateUserName = USR.UserListDTO.CreateFromModel(model.CreatedBy),
                    CalculateDate = model.Created,
                    Status = ((model.IsApprove.HasValue && model.IsApprove.Value) ? "อนุมัติแล้ว" : "ยังไม่อนุมัติ"),
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

        public static CalculatePerMonthLowRiseDTO CreateFromQueryResult(CalculatePerMonthLowRiseQueryResult model)
        {
            if (model != null)
            {
                var result = new CalculatePerMonthLowRiseDTO()
                {
                    Id = model.CalculatePerMonthLowRise?.ID,
                    Project = PRJ.ProjectDropdownDTO.CreateFromModel(model.CalculatePerMonthLowRise.Project),
                    PeriodMonth = new DateTime(model.CalculatePerMonthLowRise.PeriodYear, model.CalculatePerMonthLowRise.PeriodMonth, 1),
                    CalculateUserName = USR.UserListDTO.CreateFromModel(model.CalculatePerMonthLowRise.CreatedBy),
                    CalculateDate = model.CalculatePerMonthLowRise.Created,
                    Status = ((model.CalculatePerMonthLowRise.IsApprove.HasValue && model.CalculatePerMonthLowRise.IsApprove.Value) ? "อนุมัติแล้ว" : "ยังไม่อนุมัติ"),
                    TotalContractNetAmount = model.CalculatePerMonthLowRise.TotalContractNetAmount,
                    CommissionPercentRate = model.CalculatePerMonthLowRise.CommissionPercentRate
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(CalculatePerMonthLowRiseSortByParam sortByParam, ref IQueryable<CalculatePerMonthLowRiseQueryResult> query)
        {
            IOrderedQueryable<CalculatePerMonthLowRiseQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case CalculatePerMonthLowRiseSortBy.PeriodMonth:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthLowRise.PeriodYear).ThenBy(o => o.CalculatePerMonthLowRise.PeriodMonth);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthLowRise.PeriodYear).ThenByDescending(o => o.CalculatePerMonthLowRise.PeriodMonth);
                        break;
                    case CalculatePerMonthLowRiseSortBy.CalculateUserName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthLowRise.CreatedBy.DisplayName);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthLowRise.CreatedBy.DisplayName);
                        break;
                    case CalculatePerMonthLowRiseSortBy.CalculateDate:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthLowRise.Created);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthLowRise.Created);
                        break;
                    case CalculatePerMonthLowRiseSortBy.Status:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthLowRise.IsApprove);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthLowRise.IsApprove);
                        break;
                    case CalculatePerMonthLowRiseSortBy.CommissionType:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.CalculatePerMonthLowRise.CommissionPercentType);
                        else orderQuery = query.OrderByDescending(o => o.CalculatePerMonthLowRise.CommissionPercentType);
                        break;
                    default:
                        orderQuery = query.OrderByDescending(o => o.CalculatePerMonthLowRise.PeriodYear).ThenByDescending(o => o.CalculatePerMonthLowRise.PeriodMonth);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderByDescending(o => o.CalculatePerMonthLowRise.PeriodYear).ThenByDescending(o => o.CalculatePerMonthLowRise.PeriodMonth);
            }

            orderQuery.ThenBy(o => o.CalculatePerMonthLowRise.ID);
            query = orderQuery;
        }
    }

    public class CalculatePerMonthLowRiseQueryResult
    {
        public CalculatePerMonthLowRise CalculatePerMonthLowRise { get; set; }
        public models.PRJ.Project Project { get; set; }
        public User CalculateUserName { get; set; }
    }
}
