using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class LetterOfGuaranteeSortByParam
    {
        public LetterOfGuaranteeSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum LetterOfGuaranteeSortBy
    {
        Project,
        IssueDate,
        ExpiredDate,
        MeterNumber,
        IsJuristicSetup,
        JuristicSetupDate,
        JuristicSetupBy,
        JuristicSetupRemark,
        Bank,
        Company,
        CostCenter,
        ProjectArea,
        LetterOfGuaranteeNo,
        LGGuarantor,
        LGType,
        IssueAmount,
        RefundAmount,
        RemainAmount,
        Status,
        LGGuaranteeConditions,
        Remark,
        EffectiveDate,
        ExpiredPeriodDate,
        ConditionCalFee,
        FeeRate,
        FeeRateAmountByPeriod,
        LGSubType
    }
}
