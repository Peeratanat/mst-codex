using System;
using System.ComponentModel;
using System.Linq;
using Base.DTOs.FIN;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;
using static Base.DTOs.FIN.BillPaymentDetailDTO;

namespace Base.DTOs.FIN
{
    public class SplitForBillPaymentDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบจอง
        /// </summary>
        public Guid BookingID { get; set; }

        /// <summary>
        /// ลบ
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// เลขที่ Agreement
        /// </summary>
        public string AgreementNo { get; set; }


        /// <summary>
        /// Project
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// Project
        /// </summary>
        public string Unit { get; set; }


        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        public MasterCenterDropdownDTO DeleteReason { get; set; }

        /// <summary>
        /// Remark Delete
        /// </summary>
        public string RemarkDelete { get; set; }

        /// <summary>
        /// เลขที่ใบรับเงินชั่วคราว
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// ชื่อรายการรับชำระเงินของ PaymentMethod นี้ ถ้ามีมากกว่า 1 รายการจะ , ต่อกัน
        /// </summary>
        public string PaymentItemName { get; set; }

        public static SplitForBillPaymentDTO CreateFromSplitModel(QueryResult model , DatabaseContext DB)
        {
            if (model != null)
            {
                var PaymentItem = DB.PaymentItems.Include(x => x.MasterPriceItem).Where(x=>x.PaymentID == model.PaymentBillPayment.PaymentID).ToList();
                var PayInstallmentAmount = PaymentItem.Where(x => x.MasterPriceItem?.Key == MasterPriceItemKeys.InstallmentAmount).ToList();
                string Installmentstr = (PayInstallmentAmount.Where(x => x.MasterPriceItem != null).FirstOrDefault() ?? new PaymentItem { MasterPriceItem = new MasterPriceItem()  }).MasterPriceItem.Detail ?? "";
                string Installment = "";
                if (!string.IsNullOrEmpty(Installmentstr))
                {
                    Installment = Installmentstr + " " + PayInstallmentAmount.Min(x => x.Period) + (PayInstallmentAmount.Min(x => x.Period) != PayInstallmentAmount.Max(x => x.Period) ? " - " + PayInstallmentAmount.Max(x => x.Period) : "");//o.PaymentItem.Period
                }
                var PayAmountDetail = PaymentItem.Where(x => x.MasterPriceItem?.Key != MasterPriceItemKeys.InstallmentAmount).Select(o => o.MasterPriceItem?.Detail).ToList();
                
                SplitForBillPaymentDTO result = new SplitForBillPaymentDTO()
                {
                    Id = model.PaymentBillPayment.ID,
                    AgreementNo = model.Agreement.AgreementNo ?? model.PaymentBillPayment.Payment.Booking.BookingNo,
                    BookingID = model.PaymentBillPayment.Payment.Booking.ID,
                    Unit = model.PaymentBillPayment.Payment.Booking.Unit.UnitNo,
                    Project = model.PaymentBillPayment.Payment.Booking.Unit.Project.ProjectNo + "-" + model.PaymentBillPayment.Payment.Booking.Unit.Project.ProjectNameTH,
                    Amount = model.PaymentBillPayment.PayAmount, 
                    IsDelete = false, 
                    ReceiptTempNo = model.PaymentBillPayment.Payment?.ReceiptTempNo, 
                };
                result.PaymentItemName = Installment + (!string.IsNullOrEmpty(Installment) && PayAmountDetail.Any() ? "," : "") + (PayAmountDetail.Any() ? string.Join(",", PayAmountDetail) : ""); 
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
