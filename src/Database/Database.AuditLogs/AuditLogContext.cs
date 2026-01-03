using System;
using Microsoft.EntityFrameworkCore;

namespace Database.AuditLogs
{
    public class AuditLogContext : DbContext
    {
        public AuditLogContext(DbContextOptions options) : base(options)
        {

        }

        #region PRJ
        public DbSet<PRJ.UnitLog> UnitLogs { get; set; }
        #endregion

        # region PRM
        public DbSet<PRM.MasterBookingPromotionItemLog> MasterBookingPromotionItemLogs { get; set; }
        public DbSet<PRM.MasterBookingPromotionFreeItemLog> MasterBookingPromotionFreeItemLogs { get; set; }
        public DbSet<PRM.MasterBookingCreditCardItemLog> MasterBookingCreditCardItemLogs { get; set; }
        public DbSet<PRM.MasterTransferPromotionItemLog> MasterTransferPromotionItemLogs { get; set; }
        public DbSet<PRM.MasterTransferPromotionFreeItemLog> MasterTransferPromotionFreeItemLogs { get; set; }
        public DbSet<PRM.MasterTransferCreditCardItemLog> MasterTransferCreditCardItemLogs { get; set; }
        public DbSet<PRM.MasterPreSalePromotionItemLog> MasterPreSalePromotionItemLogs { get; set; }
        #endregion

        #region FIN
        public DbSet<FIN.PaymentBankTransferLog> PaymentBankTransferLogs { get; set; }
        public DbSet<FIN.PaymentCashierChequeLog> PaymentCashierChequeLogs { get; set; }
        public DbSet<FIN.PaymentCashLog> PaymentCashLogs { get; set; }
        public DbSet<FIN.PaymentCreditCardLog> PaymentCreditCardLogs { get; set; }
        public DbSet<FIN.PaymentDebitCardLog> PaymentDebitCardLogs { get; set; }
        public DbSet<FIN.PaymentForeignBankTransferLog> PaymentForeignBankTransferLogs { get; set; }
        public DbSet<FIN.PaymentPersonalChequeLog> PaymentPersonalChequeLogs { get; set; }
        public DbSet<FIN.PaymentQRCodeLog> PaymentQRCodeLogs { get; set; }
        public DbSet<FIN.FETLog> FETLogs { get; set; }
        public DbSet<FIN.DirectCreditDebitExportHeaderLog> DirectCreditDebitExportHeaderLogs { get; set; }
        public DbSet<FIN.DirectCreditDebitExportDetailLog> DirectCreditDebitExportDetailLogs { get; set; }
        public DbSet<FIN.BillPaymentHeaderLog> BillPaymentHeaderLogs { get; set; }
        public DbSet<FIN.BillPaymentDetailLog> BillPaymentDetailLogs { get; set; }
        public DbSet<FIN.OfflinePaymentHeaderLog> OfflinePaymentHeaderLogs { get; set; }
        public DbSet<FIN.OfflinePaymentDetailLog> OfflinePaymentDetailLogs { get; set; }
       //public DbSet<FIN.MemoMoveMoneyLog> MemoMoveMoneyLogs { get; set; }
        public DbSet<FIN.ReceiptHeaderLog> ReceiptHeaderLogs { get; set; }
        public DbSet<FIN.ReceiptDetailLog> ReceiptDetailLogs { get; set; }
        public DbSet<FIN.ReceiptTempHeaderLog> ReceiptTempHeaderLogs { get; set; }
        public DbSet<FIN.ReceiptTempDetailLog> ReceiptTempDetailLogs { get; set; }
        #endregion

        #region ACC
        public DbSet<ACC.CalendarLockLog> CalendarLockLogs { get; set; }
        public DbSet<ACC.PostGLFormatTextFileHeaderLog> PostGLFormatTextFileHeaderLogs { get; set; }
        public DbSet<ACC.PostGLFormatTextFileDetailLog> PostGLFormatTextFileDetailLogs { get; set; }
        #endregion
    }
}
