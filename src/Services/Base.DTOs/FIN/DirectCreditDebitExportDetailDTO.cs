using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.FIN;
using Database.Models.MST;
using Database.Models.SAL;
using System;
using System.ComponentModel;
using System.Linq;

namespace Base.DTOs.FIN
{
    public class DirectCreditDebitExportDetailDTO : BaseDTO
    {
        /// <summary>
        /// Code ที่ใช้ Referent ใน Textfile
        /// </summary>
        public string BatchID { get; set; }

        /// <summary>
        /// ลำดับรายการ
        /// </summary>
        public int Seq { get; set; }

        ///// <summary>
        ///// Booking
        ///// </summary>
        //public SAL.BookingDropdownDTO Booking { get; set; }

        /// <summary>
        /// UnitPriceInstallment งวดที่จะตัดเงินลูกค้า
        /// </summary>
        public PaymentUnitPriceItemDTO UnitPriceInstallment { get; set; }

        /// <summary>
        /// จำนวนเงินที่จะตัดเงินลูกค้า
        /// </summary>
        public decimal? Amount { get; set; }

        /// <summary>
        /// Code ผลลัพธ์จากธนาคาร
        /// </summary>
        public string TransCode { get; set; }
        public string TransCodeRemark { get; set; }

        /// <summary>
        /// เลขที่ใบเสร็จชั่วคราว
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// สถานะตัดเงิน รอ Import Text File,ข้อมูลถูกต้อง,ไม่สามารถตัดเงินได้,ห้องโอนกรรมสิทธิ์แล้ว
        /// </summary>
        public MST.MasterCenterDropdownDTO DirectCreditDebitExportDetailStatus { get; set; }

        public ProjectDropdownDTO Project { get; set; }
        public string Unit { get; set; }
        public string AccountNO { get; set; }
        public int? CreditCardExpireYear { get; set; }
        public int? CreditCardExpireMonth { get; set; }

        public DateTime? dueDate { get; set; }
        public string Agreement { get; set; }

        public string OwnerName { get; set; }
        public int? DirectPeriod { get; set; }

        /// <summary>
        /// งวดที่จะตัดตอนสร้าง Text File
        /// </summary>
        public string OldPeriod { get; set; }

        /// <summary>
        /// งวดที่ตัด
        /// </summary>
        public string Period { get; set; }

        public Guid? DirectCreditDebitApprovalFormID { get; set; }

        public bool IsAGLetter { get; set; } = false;

        public string AGLetterStatusText { get; set; }
        public decimal UnknowPaymentWaitingAmount { get; set; } = 0;

        public static DirectCreditDebitExportDetailDTO CreateFromModel(DirectCreditDebitExportDetail model, Agreement Agreement, string ReceiptTempNo = null, string PaymentItemName = null, string TranCodeRemark = null , UnitPriceInstallment unitPriceInstallment = null)
        {
            if (model != null)
            {
                var result = new DirectCreditDebitExportDetailDTO()
                {
                    BatchID = model.BatchID,
                    Project = ProjectDropdownDTO.CreateFromModel(model.DirectCreditDebitApprovalForm.Booking.Unit.Project),
                    Unit = model.DirectCreditDebitApprovalForm.Booking.Unit.UnitNo,
                    AccountNO = model.DirectCreditDebitApprovalForm.AccountNO,
                    CreditCardExpireYear = model.DirectCreditDebitApprovalForm.CreditCardExpireYear,
                    CreditCardExpireMonth = model.DirectCreditDebitApprovalForm.CreditCardExpireMonth,
                    DirectPeriod = model.DirectCreditDebitApprovalForm.DirectPeriod,
                    dueDate = unitPriceInstallment.DueDate,
                    Agreement = Agreement.AgreementNo,
                    OwnerName = Agreement.MainOwnerName,
                    DirectCreditDebitApprovalFormID = model.DirectCreditDebitApprovalFormID,
                    //Amount = unitPriceInstallment.Amount - unitPriceInstallment.PaidAmount, 
                    Amount = model.Amount,
                    DirectCreditDebitExportDetailStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.DirectCreditDebitExportDetailStatus),
                    Seq = model.Seq,
                    TransCode = model.TransCode,
                    ReceiptTempNo = ReceiptTempNo,
                    Id = model.ID,
                    OldPeriod = "เงินดาวน์ งวดที่ " + unitPriceInstallment.Period.ToString(),//model.Period,
                    Period  = PaymentItemName ,
                    TransCodeRemark = TranCodeRemark, 
                    UnitPriceInstallment  =new PaymentUnitPriceItemDTO {Id = model.UnitPriceInstallmentID,
                        Period = unitPriceInstallment.Period,
                        DueDate = unitPriceInstallment.DueDate,
                        ItemAmount = unitPriceInstallment.Amount,
                        PaidAmount = unitPriceInstallment.PaidAmount
                    }
                };

                switch (result.DirectCreditDebitExportDetailStatus.Key.ToString())
                {
                    case "Fail":         //Status : Fail
                        result.DirectCreditDebitExportDetailStatus.Key = "0" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                    case "TransferUnit": // Status : FaTransferUnitil
                        result.DirectCreditDebitExportDetailStatus.Key = "1" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                    case "Complete":     // Status : Complete
                        result.DirectCreditDebitExportDetailStatus.Key = "2" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                    case "Wait":         // Status : Wait
                        result.DirectCreditDebitExportDetailStatus.Key = "3" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                    case "Cancel":         // Status : Cancel
                        result.DirectCreditDebitExportDetailStatus.Key = "4" + result.DirectCreditDebitExportDetailStatus.Key;
                        break;
                }
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
