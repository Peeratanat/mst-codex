using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Database.Models.DbQueries.Finance
{
    public static class sqlvwAgreementByUnitLetterInfo
    {
        public static string QueryString = @"SELECT * FROM dbo.vw_AgreementByUnitLetterInfo a 
                WHERE (a.LastDownPaymentLetterNo LIKE '%CTM2%' OR a.LastDownPaymentLetterNo LIKE '%CTM3%') 
                    AND IsProjectTransferLetter = 0 AND SumRemainDownTotalAmount > 0";
           
        // AND a.BookingID = '300FA30D-3CE1-4659-A35A-BA44FFC89813'

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? BookingID, Guid? AgreementID, Guid? ProjectID, Guid? UnitID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (BookingID.HasValue)
            {
                ParamList.Add(new SqlParameter("@prmBookingID", BookingID));
                QueryString += " AND a.BookingID = @prmBookingID";
            }

            if (AgreementID.HasValue)
            {
                ParamList.Add(new SqlParameter("@prmAgreementID", AgreementID));
                QueryString += " AND a.AgreementID = @prmAgreementID";
            }

            if (ProjectID.HasValue)
            {
                ParamList.Add(new SqlParameter("@prmProjectID", ProjectID));
                QueryString += " AND a.ProjectID = @prmProjectID";
            }

            if (UnitID.HasValue)
            {
                ParamList.Add(new SqlParameter("@prmUnitID", UnitID));
                QueryString += " AND a.UnitID = @prmUnitID";
            }

            return ParamList;
        }

        public static List<SqlParameter> QueryFilter(ref string QueryString, List<Guid?> BookingIDs)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            for (var i = 1; i <= BookingIDs.Count; i++)
            {
                ParamList.Add(new SqlParameter($"@BookingID{i}", BookingIDs[i - 1]));
            }

            QueryString += string.Format(" AND a.BookingID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

            return ParamList;
        }

        public class QueryResult
        {
            public Guid AgreementID { get; set; }
            public Guid? ProjectID { get; set; }
            public Guid? UnitID { get; set; }
            public Guid? BookingID { get; set; }
            public DateTime? SignAgreementDate { get; set; }
            public DateTime? SignContractApprovedDate { get; set; }
            public int IsDownPaymentLetter { get; set; }
            public Guid? LastDownPaymentLetterID { get; set; }
            public string LastDownPaymentLetterNo { get; set; }
            public DateTime? LastDownPaymentLetterDate { get; set; }
            public string LastDownPaymentLetterTypDesc { get; set; }
            public Guid? LastDownPaymentLetterTypeMasterCenterID { get; set; }
            public int? LastRemainDownPeriodEnd { get; set; }
            public decimal? LastRemainDownTotalAmount { get; set; }
            public DateTime? LastDownPaymentLetterResponseDate { get; set; }
            public int? TotalPeriodOverDue { get; set; }
            //public decimal? TotalAmountOverDue { get; set; }
            public int IsTransferLetter { get; set; }
            public Guid? LastTransferLetterID { get; set; }
            public string LastTransferLetterNo { get; set; }
            public DateTime? LastTransferLetterDate { get; set; }
            public string LastTransferLetterTypDesc { get; set; }
            public DateTime? LastTransferLetterResponseDate { get; set; }
            public decimal? UnknowPaymentWaitingAmount { get; set; }

            public int IsProjectTransferLetter { get; set; }
            public decimal? SumPaidAfterPaymentLetter { get; set; }
            public decimal? SumRemainDownTotalAmount { get; set; }
        }
    }
}



