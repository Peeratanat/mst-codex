using Base.DbQueries;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EXT
{
    public class dbqPrePareDownPaymentLetterExecData
    {
        public Guid? AgreementID { get; set; }
        public Guid? ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public Guid? UnitID { get; set; }
        public string UnitNo { get; set; }
        public string DisplayName { get; set; }
        public int CountDueAmount { get; set; }
        public decimal? SumDueAmount { get; set; }
        public DateTime? LastReceiveDate { get; set; }
        public bool? IsOverTwelvePointFivePercent { get; set; }
        public string NowDownPaymentLetterType { get; set; }
        public Guid? NowDownPaymentLetterID { get; set; }
        public string NowDownPaymentLetterName { get; set; }
        public Guid? LastDownPaymentLetterID { get; set; }
        public int LastDownPaymentLetterType { get; set; }
        public Guid? LastDownPaymentLetterTypeID { get; set; }
        public string LastDownPaymentLetterTypeName { get; set; }
        public DateTime? LastDownPaymentLetterDate { get; set; }
        public DateTime? LastResponseDate { get; set; }
        public string LastLetterStatusName { get; set; }
        public int? TotalPeriodOverDue { get; set; }
        public decimal? TotalAmountOverDue { get; set; }

    }
}


