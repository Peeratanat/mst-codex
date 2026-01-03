using System;
using System.Collections.Generic;
using System.Text;

namespace MST_Event.Params.Filters
{
    public class LetterOfGuaranteeFilter : BaseFilter
    {
        public DateTime? IssueDateFrom { get; set; }
        public DateTime? IssueDateTo { get; set; }
        public DateTime? ExpireDateFrom { get; set; }
        public DateTime? ExpireDateTo { get; set; }
        public string MeterNumber { get; set; }
        public bool? IsJuristicSetup { get; set; }
        public DateTime? JuristicSetupDateFrom { get; set; }
        public DateTime? JuristicSetupDateTo { get; set; }
        public string JuristicSetupBy { get; set; }
        public string JuristicSetupRemarks { get; set; }
        public Guid? BankID { get; set; }
        public Guid? CompanyID { get; set; }
        public string CostCenter { get; set; }
        public Guid? ProjectID { get; set; }
        public double? ProjectAreaFrom { get; set; }
        public double? ProjectAreaTo { get; set; }
        public string LetterOfGuaranteeNo { get; set; }
        public Guid? LGGuarantorMasterCenterID { get; set; }
        public Guid? LGTypeMasterCenterID { get; set; }
        public decimal? IssueAmountFrom { get; set; }
        public decimal? IssueAmountTo { get; set; }
        public decimal? RefundAmountFrom { get; set; }
        public decimal? RefundAmountTo { get; set; }
        public decimal? RemainAmountFrom { get; set; }
        public decimal? RemainAmountTo { get; set; }
        public Guid? LGGuaranteeConditionsMasterCenterID { get; set; }
        public string Remark { get; set; }
        public DateTime? EffectiveDateFrom { get; set; }
        public DateTime? EffectiveDateTo { get; set; }
        public int? ConditionCalFeeFrom { get; set; }
        public int? ConditionCalFeeTo { get; set; }
        public double? FeeRateFrom { get; set; }
        public double? FeeRateTo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
        public DateTime? UpdatedFrom { get; set; }
        public DateTime? UpdatedTo { get; set; }
        public decimal? FeeRateAmountByPeriodFrom {get; set;}
        public decimal? FeeRateAmountByPeriodTo { get; set; }
        public bool? IsCancel { get; set; }
    }
}
