using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.Finance
{
    public static class sqlPaymentHistory
    {
        public static string QueryString = @"
        SELECT 'PaymentID' = p.ID
             , 'PaymentMethodID' = pm.ID
             , 'IsFET' = CONVERT(BIT, (CASE WHEN fet.ID IS NULL AND unFET.ID IS NULL THEN 0 ELSE 1 END))
             , 'hasFETAttachFile' = CONVERT(BIT, (CASE WHEN ISNULL(fet.AttachFileName, ISNULL(unFET.AttachFileName,'')) = '' THEN 0 ELSE 1 END))         
             , 'hasAttachFile' = CONVERT(BIT, (CASE WHEN ISNULL(p.AttachFileName,'') = '' THEN 0 ELSE 1 END))         
             , 'ReceiveAmount' = p.TotalAmount
             , 'ReceiveDate' = p.ReceiveDate
             , 'MasterPriceItemName' = (CASE WHEN ISNULL(mpi.[Key], '') = 'InstallmentAmount' THEN mpi.Detail + ' งวดที่ ' + CONVERT(VARCHAR(2), pi.Period) ELSE mpi.Detail END)
             , 'MasterPriceItemKey' = mpi.[Key]
             , 'Amount' = pmi.PayAmount
             , 'PaymentMethodTypeID' = mstPayType.ID
             , 'PaymentMethodTypeName' = CASE  WHEN ISNULL(unMethodType.[Name],'') = '' THEN mstPayType.[Name]
				ELSE unMethodType.[Name] + ' (' +mstPayType.[Name]+')' END 
             , 'PaymentMethodTypeKey' = mstPayType.[Key]
             , 'PaymentMethodTypeOrder' = mstPayType.[Order]
             , 'UnknownPaymentDate' = un.ReceiveDate
             , 'DepositNo' = p.DepositNo
             , 'DepositDate' = p.DepositDate
             , 'ReceiptTempHeaderID' = rh.ID
             --, 'ReceiptTempNo' = p.ReceiptTempNo
             , 'ReceiptTempNo' = p.ReceiptTempNo + (CASE WHEN mstStage.[Key] = 'Transfer' THEN ' (ฝ่ายโอน)' ELSE '' END)
             , 'ReceiptNo' = p.ReceiptNo
             , 'PrintingDate' = exPrinting.SendDate
             , 'SendMailDate' = exSendMail.SendDate
             , 'PostGLDocumentNo' = p.PostGLDocumentNo
             , 'PostGLDate' = p.PostGLDate
             , 'IsFromLC' = p.IsFromLC
             , 'IsLockCalendar' = ISNULL(car.IsLocked, 0)
             , 'PaymentCreateDate' = p.Created
             , 'IsPDFReceipt' = CONVERT(BIT, CASE WHEN rhl.ID IS null THEN 0 ELSE 1 END )
			 , 'PDFReceiptFile' = rhl.FilePath + '/' + rhl.[FileName]
             , 'IsOldFET' = CONVERT(BIT, (CASE WHEN fet.IsCancel = 1 OR unFET.IsCancel = 1 THEN 1 ELSE 0 END))
             , 'QuotationID' = p.QuotationID
         FROM FIN.PaymentMethodToItem pmi WITH (NOLOCK)
         LEFT JOIN FIN.PaymentMethod pm WITH (NOLOCK) ON pm.ID = pmi.PaymentMethodID AND pm.IsDeleted = 0
         LEFT JOIN FIN.Payment p WITH (NOLOCK) ON p.ID = pm.PaymentID AND p.IsDeleted = 0 AND p.IsCancel = 0
		 LEFT JOIN SAL.Booking bk WITH (NOLOCK) ON bk.ID = p.BookingID
		 LEFT JOIN PRJ.Project prj WITH (NOLOCK) ON prj.ID = bk.ProjectID
         LEFT JOIN USR.[User] pCreater WITH (NOLOCK) ON pCreater.ID = p.CreatedByUserID
         LEFT JOIN MST.MasterCenter mstPayType WITH (NOLOCK) ON mstPayType.ID = pm.PaymentMethodTypeMasterCenterID
         LEFT JOIN FIN.UnknownPayment un WITH (NOLOCK) ON un.ID = pm.UnknownPaymentID
         LEFT JOIN FIN.PaymentItem pi WITH (NOLOCK) ON pi.ID = pmi.PaymentItemID AND pi.IsDeleted = 0
         LEFT JOIN MST.MasterPriceItem mpi WITH (NOLOCK) ON mpi.ID = pi.MasterPriceItemID
         LEFT JOIN FIN.ReceiptTempHeader rh WITH (NOLOCK) ON rh.PaymentID = p.ID
         LEFT JOIN FIN.ReceiptHeader rhl WITH (NOLOCK) ON rhl.PaymentID = p.ID AND rhl.IsDeleted = 0
         LEFT JOIN ACC.PostGLHeader glh WITH (NOLOCK) ON glh.ReferentID = p.ID AND glh.IsDeleted = 0 and glh.IsCancel= 0
         LEFT JOIN FIN.FET fet WITH (NOLOCK) ON fet.PaymentMethodID = pm.ID AND fet.IsDeleted = 0
         LEFT JOIN FIN.FET unFET WITH (NOLOCK) ON unFET.UnknownPaymentID = un.ID AND unFET.IsDeleted = 0
         LEFT JOIN ACC.CalendarLock car WITH (NOLOCK) ON car.CompanyID = prj.CompanyID AND CONVERT(DATE, car.LockDate) = CONVERT(DATE, p.ReceiveDate) AND car.IsDeleted = 0
		 LEFT JOIN MST.MasterCenter mstStage WITH (NOLOCK) ON mstStage.ID = p.PaymentStateMasterCenterID
		 LEFT JOIN MST.MasterCenter unMethodType WITH (NOLOCK) ON unMethodType.ID = un.PaymentMethodTypeMasterCenterID
         OUTER APPLY (
						SELECT printdt.ReceiptHeaderID, 'SendDate' = MAX(printhd.SendDate)
						FROM FIN.ReceiptSendPrintingHistoryDetail printdt WITH (NOLOCK) 
						LEFT JOIN FIN.ReceiptSendPrintingHistory printhd WITH (NOLOCK) ON printhd.ID = printdt.ReceiptSendPrintingHistoryID
						WHERE printdt.ReceiptHeaderID = rhl.ID AND printhd.SendDate IS NOT NULL
						GROUP BY printdt.ReceiptHeaderID
                     ) AS exPrinting
		 OUTER APPLY (
						SELECT mail.ReceiptHeaderID, 'SendDate' = MAX(mail.SendDate)
						FROM FIN.ReceiptSendEmailHistory mail WITH (NOLOCK) 
						WHERE mail.ReceiptHeaderID = rhl.ID
						GROUP BY mail.ReceiptHeaderID
					 ) AS exSendMail
         WHERE 1=1
             AND pmi.IsDeleted = 0 
             AND ISNULL(rh.IsCancel, 0) = 0";

        public static string QueryStringOrder = @" ORDER BY p.ReceiveDate, p.Created, mstPayType.[Order], mpi.[Order], pi.Period";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid BookingID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (BookingID != Guid.Empty)
            {
                ParamList.Add(new SqlParameter("@prmBookingID", BookingID));
                QueryString += " AND p.BookingID = @prmBookingID";
            }

            return ParamList;
        }

        public static string QueryOrder(string QueryString)
        {
            QueryString += QueryStringOrder;

            return QueryString;
        }

        public class QueryResult
        {
            public Guid? PaymentID { get; set; }
            public Guid? PaymentMethodID { get; set; }
            public bool? IsFET { get; set; }
            public bool? hasFETAttachFile { get; set; }
            public bool? hasAttachFile { get; set; }
            public decimal? ReceiveAmount { get; set; }
            public DateTime? ReceiveDate { get; set; }

            public string MasterPriceItemName { get; set; }
            public string MasterPriceItemKey { get; set; }
            public decimal? Amount { get; set; }

            public Guid? PaymentMethodTypeID { get; set; }
            public string PaymentMethodTypeName { get; set; }
            public string PaymentMethodTypeKey { get; set; }
            public int? PaymentMethodTypeOrder { get; set; }

            public DateTime? UnknownPaymentDate { get; set; }

            public string DepositNo { get; set; }
            public DateTime? DepositDate { get; set; }

            public Guid? ReceiptTempHeaderID { get; set; }
            public string ReceiptTempNo { get; set; }
            public string ReceiptNo { get; set; }
            public DateTime? PrintingDate { get; set; }
            public DateTime? SendMailDate { get; set; }

            public string PostGLDocumentNo { get; set; }
            public DateTime? PostGLDate { get; set; }
            public bool? IsFromLC { get; set; }
            public bool? IsLockCalendar { get; set; }
            public DateTime? PaymentCreateDate { get; set; }
            public bool? IsPDFReceipt { get; set; }
            public string PDFReceiptFile { get; set; }
            public bool? IsOldFET { get; set; }
            public Guid? QuotationID { get; set; }
        }
    }
}


