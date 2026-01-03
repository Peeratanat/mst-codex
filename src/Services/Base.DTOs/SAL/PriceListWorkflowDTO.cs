using Base.DTOs.SAL.Sortings;
using Base.DTOs.USR;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.SAL
{
    /// <summary>
    /// ข้อมูลขออนุมัติ PriceList
    /// Model: PriceListWorkflow
    /// </summary>
    public class PriceListWorkflowDTO : BaseDTO
    {
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDropdownDTO Unit { get; set; }
        /// <summary>
        /// Quotation
        /// </summary>
        public SAL.QuotationDTO Quotation { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public Guid? BookingID { get; set; }
        /// <summary>
        /// สถานะแปลง
        /// </summary>
        public MST.MasterCenterDropdownDTO UnitStatus { get; set; }
        /// <summary>
        /// ขั้นตอน
        /// </summary>
        public MST.MasterCenterDropdownDTO PriceListWorkflowStage { get; set; }
        /// <summary>
        /// ราคาขายเดิม
        /// </summary>
        public decimal? MasterSellingPrice { get; set; }
        /// <summary>
        /// เงินจองเดิม
        /// </summary>
        public decimal? MasterBookingAmount { get; set; }
        /// <summary>
        /// เงินสัญญาเดิม
        /// </summary>
        public decimal? MasterContractAmount { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์เดิม
        /// </summary>
        public int? MasterInstallment { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์ปกติเดิม
        /// </summary>
        public int? MasterNormalInstallment { get; set; }
        /// <summary>
        /// ราคางวดดาวน์ปกติเดิม
        /// </summary>
        public decimal? MasterInstallmentAmount { get; set; }
        /// <summary>
        /// งวดพิเศษเดิม
        /// </summary>
        public int? MasterSpecialInstallment { get; set; }

        /// <summary>
        /// รายละเอียด งวดดาวน์พิเศษ เดิม
        /// </summary>
        public List<SpecialInstallmentDTO> MasterSpecialInstallmentPeriods { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal? SellingPrice { get; set; }
        /// <summary>
        /// เงินจอง
        /// </summary>
        public decimal? BookingAmount { get; set; }
        /// <summary>
        /// เงินสัญญา
        /// </summary>
        public decimal? ContractAmount { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์
        /// </summary>
        public int? Installment { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์ปกติ
        /// </summary>
        public int? NormalInstallment { get; set; }
        /// <summary>
        /// ราคางวดดาวน์ปกติ
        /// </summary>
        public decimal? InstallmentAmount { get; set; }
        /// <summary>
        /// งวดพิเศษ
        /// </summary>
        public int? SpecialInstallment { get; set; }
        /// <summary>
        /// IsApproved
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// ApprovedBy
        /// </summary>
        public UserListDTO ApprovedBy { get; set; }
        /// <summary>
        /// RejectComment
        /// </summary>
        public string RejectComment { get; set; }
        /// <summary>
        /// รายละเอียด (งวดดาวน์พิเศษ) 
        /// </summary>
        public List<SpecialInstallmentDTO> SpecialInstallmentPeriods { get; set; }

        public static PriceListWorkflowDTO CreateFromModel(PriceListWorkflow model)
        {
            if (model != null)
            {
                var result = new PriceListWorkflowDTO()
                {
                    Id = model.ID,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Quotation = QuotationDTO.CreateFromModel(model.Quotation),
                    BookingID = model.BookingID,
                    UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus),
                    PriceListWorkflowStage = MST.MasterCenterDropdownDTO.CreateFromModel(model.PriceListWorkflowStage),
                    // Master
                    MasterSellingPrice = decimal.Round(model.MasterSellingPrice ?? 0, 2, MidpointRounding.AwayFromZero),
                    MasterBookingAmount = decimal.Round(model.MasterBookingAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    MasterContractAmount = decimal.Round(model.MasterContractAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    MasterInstallment = model.MasterInstallment,
                    MasterNormalInstallment = model.MasterNormalInstallment,
                    MasterInstallmentAmount = decimal.Round(model.MasterInstallmentAmount??0, 2, MidpointRounding.AwayFromZero),
                    MasterSpecialInstallment = !string.IsNullOrEmpty(model.MasterSpecialInstallments) ? model.MasterSpecialInstallments.Split(',').ToList().Count() : (int?)null,
                    // Quotataion
                    SellingPrice = model.SellingPrice,
                    BookingAmount = model.BookingAmount,
                    ContractAmount = model.ContractAmount,
                    Installment = model.Installment,
                    NormalInstallment = model.NormalInstallment,
                    InstallmentAmount = model.InstallmentAmount,
                    SpecialInstallment = !string.IsNullOrEmpty(model.SpecialInstallments) ? model.SpecialInstallments.Split(',').ToList().Count() : (int?)null,
                    // Approve Reject
                    IsApproved = model.IsApproved,
                    ApprovedBy = UserListDTO.CreateFromModel(model.ApprovedBy),
                    RejectComment = model.RejectComment,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                result.MasterSpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                var masterSpecialInstallment = !string.IsNullOrEmpty(model.MasterSpecialInstallments) ? model.MasterSpecialInstallments.Split(',').Select(o => Convert.ToInt32(o)).ToList() : null;
                var masterSpecialInstallmentAmount = !string.IsNullOrEmpty(model.MasterSpecialInstallmentAmounts) ? model.MasterSpecialInstallmentAmounts.Split(',').Select(o => Convert.ToDecimal(o)).ToList() : null;
                if (masterSpecialInstallment != null)
                {
                    for (int i = 0; i < masterSpecialInstallment.Count(); i++)
                    {
                        var masterSpecialInstallmentPeriod = new SpecialInstallmentDTO();
                        masterSpecialInstallmentPeriod.Amount = decimal.Round(masterSpecialInstallmentAmount[i], 2, MidpointRounding.AwayFromZero) ;
                        masterSpecialInstallmentPeriod.Period = masterSpecialInstallment[i];
                        result.MasterSpecialInstallmentPeriods.Add(masterSpecialInstallmentPeriod);
                    }
                }

                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                var specialInstallment = !string.IsNullOrEmpty(model.SpecialInstallments) ? model.SpecialInstallments.Split(',').Select(o => Convert.ToInt32(o)).ToList() : null;
                var specialInstallmentAmount = !string.IsNullOrEmpty(model.SpecialInstallmentAmounts) ? model.SpecialInstallmentAmounts.Split(',').Select(o => Convert.ToDecimal(o)).ToList() : null;
                if (specialInstallment != null)
                {
                    for (int i = 0; i < specialInstallment.Count(); i++)
                    {
                        var specialInstallmentPeriod = new SpecialInstallmentDTO();
                        specialInstallmentPeriod.Amount = decimal.Round(specialInstallmentAmount[i], 2, MidpointRounding.AwayFromZero);
                        specialInstallmentPeriod.Period = specialInstallment[i];
                        result.SpecialInstallmentPeriods.Add(specialInstallmentPeriod);
                    }
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static PriceListWorkflowDTO CreateFromQueryResult(PriceListWorkflowQueryResult model)
        {
            if (model != null)
            {
                var result = new PriceListWorkflowDTO()
                {
                    Id = model.PriceListWorkflow.ID,
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Unit = PRJ.UnitDropdownDTO.CreateFromModel(model.Unit),
                    Quotation = QuotationDTO.CreateFromModel(model.Quotation),
                    BookingID = model.PriceListWorkflow.BookingID,
                    UnitStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.UnitStatus),
                    PriceListWorkflowStage = MST.MasterCenterDropdownDTO.CreateFromModel(model.PriceListWorkflowStage),
                    // Master
                    MasterSellingPrice = decimal.Round(model.PriceListWorkflow.MasterSellingPrice ?? 0, 2, MidpointRounding.AwayFromZero),
                    MasterBookingAmount = decimal.Round(model.PriceListWorkflow.MasterBookingAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    MasterContractAmount = decimal.Round(model.PriceListWorkflow.MasterContractAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    MasterInstallment = model.PriceListWorkflow.MasterInstallment,
                    MasterNormalInstallment = model.PriceListWorkflow.MasterNormalInstallment,
                    MasterInstallmentAmount = decimal.Round(model.PriceListWorkflow.MasterInstallmentAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    MasterSpecialInstallment = !string.IsNullOrEmpty(model.PriceListWorkflow.MasterSpecialInstallments) ? model.PriceListWorkflow.MasterSpecialInstallments.Split(',').ToList().Count() : (int?)null,
                    // Quotataion
                    SellingPrice = decimal.Round(model.PriceListWorkflow.SellingPrice ?? 0, 2, MidpointRounding.AwayFromZero),
                    BookingAmount = decimal.Round(model.PriceListWorkflow.BookingAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    ContractAmount = decimal.Round(model.PriceListWorkflow.ContractAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    Installment = model.PriceListWorkflow.Installment,
                    NormalInstallment = model.PriceListWorkflow.NormalInstallment,
                    InstallmentAmount = decimal.Round(model.PriceListWorkflow.InstallmentAmount ?? 0, 2, MidpointRounding.AwayFromZero),
                    SpecialInstallment = !string.IsNullOrEmpty(model.PriceListWorkflow.SpecialInstallments) ? model.PriceListWorkflow.SpecialInstallments.Count() : (int?)null,
                    // Approve Reject
                    IsApproved = model.PriceListWorkflow.IsApproved,
                    ApprovedBy = UserListDTO.CreateFromModel(model.ApprovedBy),
                    RejectComment = model.PriceListWorkflow.RejectComment,
                    Updated = model.PriceListWorkflow.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };
                result.MasterSpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                var masterSpecialInstallment = !string.IsNullOrEmpty(model.PriceListWorkflow.MasterSpecialInstallments) ? model.PriceListWorkflow.MasterSpecialInstallments.Split(',').Select(o => Convert.ToInt32(o)).ToList() : null;
                var masterSpecialInstallmentAmount = !string.IsNullOrEmpty(model.PriceListWorkflow.MasterSpecialInstallmentAmounts) ? model.PriceListWorkflow.MasterSpecialInstallmentAmounts.Split(',').Select(o => Convert.ToDecimal(o)).ToList() : null;
                if (masterSpecialInstallment != null)
                {
                    for (int i = 0; i < masterSpecialInstallment.Count(); i++)
                    {
                        var masterSpecialInstallmentPeriod = new SpecialInstallmentDTO();
                        masterSpecialInstallmentPeriod.Amount = decimal.Round(masterSpecialInstallmentAmount[i], 2, MidpointRounding.AwayFromZero);
                        masterSpecialInstallmentPeriod.Period = masterSpecialInstallment[i];
                        result.MasterSpecialInstallmentPeriods.Add(masterSpecialInstallmentPeriod);
                    }
                }

                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                var specialInstallment = !string.IsNullOrEmpty(model.PriceListWorkflow.SpecialInstallments) ? model.PriceListWorkflow.SpecialInstallments.Split(',').Select(o => Convert.ToInt32(o)).ToList() : null;
                var specialInstallmentAmount = !string.IsNullOrEmpty(model.PriceListWorkflow.SpecialInstallmentAmounts) ? model.PriceListWorkflow.SpecialInstallmentAmounts.Split(',').Select(o => Convert.ToDecimal(o)).ToList() : null;

                if (specialInstallment != null)
                {
                    for (int i = 0; i < specialInstallment.Count(); i++)
                    {
                        var specialInstallmentPeriod = new SpecialInstallmentDTO();
                        specialInstallmentPeriod.Amount = decimal.Round(specialInstallmentAmount[i], 2, MidpointRounding.AwayFromZero);
                        specialInstallmentPeriod.Period = specialInstallment[i];
                        result.SpecialInstallmentPeriods.Add(specialInstallmentPeriod);
                    }
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(PriceListWorkflowSortByParam sortByParam, ref IQueryable<PriceListWorkflowQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case PriceListWorkflowSortBy.Project:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);
                        else query = query.OrderByDescending(o => o.Project.ProjectNo);
                        break;
                    case PriceListWorkflowSortBy.UnitNo:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
                        else query = query.OrderByDescending(o => o.Unit.UnitNo);
                        break;
                    case PriceListWorkflowSortBy.UnitStatus:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.UnitStatus.Name);
                        else query = query.OrderByDescending(o => o.UnitStatus.Name);
                        break;
                    case PriceListWorkflowSortBy.PriceListWorkflowStage:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.PriceListWorkflowStage.Name);
                        else query = query.OrderByDescending(o => o.PriceListWorkflowStage.Name);
                        break;
                    default:
                        query = query.OrderBy(o => o.Project.ProjectNo);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(o => o.Project.ProjectNo);
            }
        }

    }

    public class PriceListWorkflowQueryResult
    {
        public PriceListWorkflow PriceListWorkflow { get; set; }
        public MasterCenter UnitStatus { get; set; }
        public MasterCenter PriceListWorkflowStage { get; set; }
        public Unit Unit { get; set; }
        public Project Project { get; set; }
        public Quotation Quotation { get; set; }
        public User UpdatedBy { get; set; }
        public User ApprovedBy { get; set; }
    }
}
