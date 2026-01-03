using System;
using Database.Models.DbQueries.FIN;

namespace Base.DTOs.FIN
{
    public class ReceiptInfoDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่ใบเสร็จ
        /// </summary>
        public string ReceiptTempNo { get; set; }

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// โครงการ
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// แปลง
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// เลขที่บัญชี
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// ประเภทการชำระ
        /// </summary>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// ค่าใช้จ่าย
        /// </summary>
        public string ReceiptDescription { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// เลขที่นำฝาก
        /// </summary>
        public string DepositNo { get; set; }

        /// <summary>
        /// เลขที่ RV
        /// </summary>
        public string RVNumber { get; set; }

        /// <summary>
        /// สถานะใบเสร็จ
        /// </summary>
        public bool? ReceiptStatus { get; set; }

        public bool? IsCancel { get; set; }
        ////public CancelRemark CancelRemark { get; set; }

        /// <summary>
        /// ชื่อ
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// รหัสจอง
        /// </summary>
        public Guid? BookingID { get; set; }

        /// <summary>
        /// ReceiverID
        /// </summary>
        public Guid? PaymentStateID { get; set; }

        /// <summary>
        /// ReceiverKey
        /// </summary>
        public string PaymentStateKey { get; set; }

        /// <summary>
        /// ReceiverName
        /// </summary>
        public string PaymentStateDetail { get; set; }

        ////----------------------------------------------- ReceiptInfoQueryResult ----------------------------------------------------------------------------------------------------
        public static ReceiptInfoDTO CreateFromQueryList(dbqSPReceiptInfo model)
        {
            if (model != null)
            {
                ReceiptInfoDTO result = new ReceiptInfoDTO()
                {
                    Id = model.ID,
                    CustomerName = model.CustomerName,
                    Amount = model.Amount ?? 0,
                    BankAccount = model.BankAccount,
                    DepositNo = model.DepositNo,
                    PaymentMethod = model.PaymentMethod,
                    Project = model.Project,
                    ReceiptDescription = model.ReceiptDescription,
                    ReceiptStatus = model.ReceiptStatus,
                    ReceiptTempNo = model.ReceiptTempNo,
                    ReceiveDate = model.ReceiveDate ?? null,
                    RVNumber = model.RVNumber,
                    Unit = model.Unit,
                    IsCancel = model.IsCancel,
                    BookingID = model.BookingID,
                            
                    PaymentStateID = model.PaymentStateID,
                    PaymentStateKey = model.PaymentStateKey,
                    PaymentStateDetail = model.PaymentStateDetail
                };

                return result;
            }
            else
            {
                return null;
            }
        }     

    }
}
