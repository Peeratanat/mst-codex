using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Database.Models.DbQueries.Finance
{
    public static class sqlPaymentUnitPrice
    {
        #region Document
        public static string QueryString_Document = @"SELECT 'BookingID' = bk.ID
		        , bk.BookingNo
		        , bk.BookingDate
		        , 'AgreementID' = ag.ID
		        , ag.AgreementNo
		        , ag.ContractDate
		        ,'TransferID' = tf.ID 
		        , ag.TransferOwnershipDate
	        FROM SAL.Booking bk WITH (NOLOCK)
	        LEFT JOIN SAL.Agreement ag WITH (NOLOCK) ON ag.BookingID = bk.ID AND ag.IsDeleted = 0 AND ag.IsCancel = 0
	        LEFT JOIN SAL.Transfer tf WITH (NOLOCK) ON tf.AgreementID = ag.ID AND tf.IsDeleted = 0
	        WHERE bk.IsDeleted = 0 AND bk.IsCancelled = 0";

        public static List<SqlParameter> QueryFilter_Document(ref string QueryString, List<Guid> BookingIDs)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            for (var i = 1; i <= BookingIDs.Count; i++)
            {
                ParamList.Add(new SqlParameter($"@BookingID{i}", BookingIDs[i - 1]));
            }

            QueryString += string.Format(" AND bk.ID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

            return ParamList;
        }

        public class QueryResult_Document
        {
            public Guid? BookingID { get; set; }
            public string BookingNo { get; set; }
            public DateTime? BookingDate { get; set; }
            public Guid? AgreementID { get; set; }
            public string AgreementNo { get; set; }
            public DateTime? ContractDate { get; set; }
            public Guid? TransferID { get; set; }
            public DateTime? TransferOwnershipDate { get; set; }
        }
        #endregion


        #region UnitPrice
        public static string QueryString_UnitPrice = @"SELECT 'UnitPriceID' = up.ID
		        , up.BookingID
		        , up.AgreementAmount
		        , up.BookingAmount
		        , up.TransferAmount
		        , up.FreedownDiscount
		        , up.IsActive
		        , 'StageKey' = mstStage.[Key]
		        , 'StageOrder' = mstStage.[Order]
	        FROM SAL.UnitPrice up WITH (NOLOCK)
	        LEFT JOIN MST.MasterCenter mstStage WITH (NOLOCK) ON mstStage.ID = up.UnitPriceStageMasterCenterID
	        WHERE up.IsDeleted = 0";

        public static List<SqlParameter> QueryFilter_UnitPrice(ref string QueryString, List<Guid> BookingIDs)
        {
            var ParamList = new List<SqlParameter>() ?? new List<SqlParameter>();

            for (var i = 1; i <= BookingIDs.Count; i++)
            {
                ParamList.Add(new SqlParameter($"@BookingID{i.ToString()}", BookingIDs[i - 1]));
            }

            QueryString += string.Format(" AND up.BookingID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

            return ParamList;
        }

        public class QueryResult_UnitPrice
        {
            public Guid? UnitPriceID { get; set; }
            public Guid? BookingID { get; set; }
            public decimal? AgreementAmount { get; set; }
            public decimal? BookingAmount { get; set; }
            public decimal? TransferAmount { get; set; }
            public decimal? FreedownDiscount { get; set; }
            public bool? IsActive { get; set; }

            public string StageKey { get; set; }
            public int? StageOrder { get; set; }
        }
        #endregion


        #region Installment
        public static string QueryString_Installment = @"SELECT 'InstallmentID' = unitPriceInst.ID
             , 'UnitPriceID' = unitPriceInst.UnitPriceID
             , unitPriceInst.DueDate
             , unitPriceInst.IsSpecialInstallment
             , unitPriceInst.Amount
		     , unitPriceInst.Period 
	        FROM SAL.UnitPriceInstallment unitPriceInst WITH (NOLOCK)
	        WHERE unitPriceInst.IsDeleted = 0";

        public static List<SqlParameter> QueryFilter_Installment(ref string QueryString, List<Guid?> AgreementPriceIDs)
        {
            var ParamList = new List<SqlParameter>() ?? new List<SqlParameter>();

            for (var i = 1; i <= AgreementPriceIDs.Count; i++)
            {
                ParamList.Add(new SqlParameter($"@AgreementPriceID{i.ToString()}", AgreementPriceIDs[i - 1]));
            }

            QueryString += string.Format(" AND unitPriceInst.UnitPriceID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

            return ParamList;
        }

        public class QueryResult_Installment
        {
            public Guid? InstallmentID { get; set; }
            public Guid? UnitPriceID { get; set; }
            public DateTime? DueDate { get; set; }
            public bool? IsSpecialInstallment { get; set; }
            public decimal? Amount { get; set; }
            public int? Period { get; set; }
        }
        #endregion


        #region OtherPrice
  //      public static string QueryString_OtherPrice = @"SELECT 'UnitPriceItemID' = upi.ID
		//	, 'UnitPriceID' = upi.UnitPriceID
		//	, 'MasterPriceItemID' = mstPriceItem.ID
		//	, 'MasterPriceItemKey' = mstPriceItem.[Key]
		//	, 'MasterPriceItemDetail' = mstPriceItem.Detail
  //          , 'MasterPriceItemDetailEN' = mstPriceItem.DetailEN
		//	, 'ItemAmount' = ISNULL(transExp.BuyerAmount, upi.Amount)
		//	, 'MasterPriceItemOrder' = ISNULL(mstPriceItem.[Order], 0) + 60
		//FROM SAL.UnitPrice currentUnitPrice WITH (NOLOCK)
		//LEFT JOIN MST.MasterCenter mstCurrentUnitPrice WITH (NOLOCK) ON mstCurrentUnitPrice.ID = currentUnitPrice.UnitPriceStageMasterCenterID
		//LEFT JOIN SAL.UnitPriceItem upi WITH (NOLOCK) ON upi.UnitPriceID = currentUnitPrice.ID AND upi.IsDeleted = 0
		//LEFT JOIN MST.MasterPriceItem mstPriceItem WITH (NOLOCK) ON mstPriceItem.ID = upi.MasterPriceItemID
		//LEFT JOIN MST.MasterCenter mstUnitPriceStage WITH (NOLOCK) ON mstUnitPriceStage.ID = mstPriceItem.UnitPriceStageMasterCenterID
		//LEFT JOIN SAL.TransferExpense transExp WITH (NOLOCK) ON transExp.MasterPriceItemID = mstPriceItem.ID #TransferIDs
		//WHERE 1=1 
		//	AND currentUnitPrice.IsActive = 1 
		//	AND currentUnitPrice.IsDeleted = 0
		//	AND (ISNULL(mstUnitPriceStage.[Order], 0) <= ISNULL(mstCurrentUnitPrice.[Order], 0) OR mstPriceItem.UnitPriceStageMasterCenterID IS NULL)
		//	AND (ISNULL(transExp.BuyerAmount, upi.Amount) > 0) #BookingIDs";

  //      public static List<SqlParameter> QueryFilter_OtherPrice(ref string QueryString, List<Guid?> BookingIDs, List<Guid?> TransferIDs)
  //      {
  //          var ParamList_BookingID = new List<SqlParameter>() ?? new List<SqlParameter>();
  //          var ParamList_TransferID = new List<SqlParameter>() ?? new List<SqlParameter>();

  //          for (var i = 1; i <= BookingIDs.Count; i++)
  //              ParamList_BookingID.Add(new SqlParameter($"@BookingID{i.ToString()}", BookingIDs[i - 1]));

  //          QueryString = QueryString.Replace("#BookingIDs", string.Format(" AND currentUnitPrice.BookingID IN ({0})", string.Join(",", ParamList_BookingID.Select(o => o.ParameterName).ToList())));

  //          if (TransferIDs.Any())
  //          {
  //              for (var i = 1; i <= TransferIDs.Count; i++)
  //                  ParamList_TransferID.Add(new SqlParameter($"@TransferID{i.ToString()}", TransferIDs[i - 1]));

  //              QueryString = QueryString.Replace("#TransferIDs", string.Format(" AND transExp.TransferID IN ({0})", string.Join(",", ParamList_TransferID.Select(o => o.ParameterName).ToList())));
  //          }
  //          else 
  //          {
  //              QueryString = QueryString.Replace("#TransferIDs", string.Format(" AND 1=2"));
  //          }

  //          var ParamList = new List<SqlParameter>();

  //          ParamList.AddRange(ParamList_BookingID);
  //          ParamList.AddRange(ParamList_TransferID);

  //          return ParamList;
  //      }

  //      public class QueryResult_OtherPrice
  //      {
  //          public Guid? UnitPriceItemID { get; set; }
  //          public Guid? UnitPriceID { get; set; }
  //          public Guid? MasterPriceItemID { get; set; }
  //          public string MasterPriceItemKey { get; set; }
  //          public string MasterPriceItemDetail { get; set; }
  //          public string MasterPriceItemDetailEN { get; set; }
  //          public decimal? ItemAmount { get; set; }
  //          public int? MasterPriceItemOrder { get; set; }
  //      }
        #endregion


        #region UnitPayment
        public static string QueryString_UnitPayment = @"SELECT p.BookingID
		        , 'MstPriceItemID' = mstPriceItem.ID
		        , 'MstPriceItemKey' = mstPriceItem.[Key]
		        , 'MstPriceItemDetail' = mstPriceItem.Detail
		        , 'MstPriceItemDetailEN' = mstPriceItem.DetailEN
		        , 'Period' = pi.Period
		        , 'PayAmount' = SUM(pi.PayAmount)
		        , 'PaymentDate' = MAX(p.ReceiveDate)
	        FROM FIN.Payment p WITH (NOLOCK)
	        LEFT JOIN FIN.PaymentItem pi WITH (NOLOCK) ON pi.PaymentID = p.ID AND pi.IsDeleted = 0
	        LEFT JOIN MST.MasterPriceItem mstPriceItem WITH (NOLOCK) ON mstPriceItem.ID = pi.MasterPriceItemID
	        WHERE p.IsDeleted = 0 AND p.IsCancel = 0 #BookingIDs
	        GROUP BY p.BookingID
                   , mstPriceItem.ID
                   , mstPriceItem.[Key]
                   , mstPriceItem.Detail
                   , mstPriceItem.DetailEN
                   , pi.Period";

        public static List<SqlParameter> QueryFilter_UnitPayment(ref string QueryString, List<Guid> BookingIDs)
        {
            var ParamList = new List<SqlParameter>() ?? new List<SqlParameter>();

            for (var i = 1; i <= BookingIDs.Count; i++)
                ParamList.Add(new SqlParameter($"@BookingID{i.ToString()}", BookingIDs[i - 1]));

            QueryString = QueryString.Replace("#BookingIDs", string.Format(" AND p.BookingID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList())));

            return ParamList;
        }

        public class QueryResult_UnitPayment
        {
            public Guid? BookingID { get; set; }
            public Guid? MstPriceItemID { get; set; }
            public string MstPriceItemKey { get; set; }
            public string MstPriceItemDetail { get; set; }
            public string MstPriceItemDetailEN { get; set; }
            public int? Period { get; set; }
            public decimal? PayAmount { get; set; }
            public DateTime? PaymentDate { get; set; }
        }
        #endregion
    }
}


