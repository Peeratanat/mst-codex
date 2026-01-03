using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections.Generic;
using Database.Models.USR;
using Database.Models.DbQueries.FIN;

namespace Base.DTOs.FIN
{
    public class ReceiptDetailDTO : BaseDTO
    {

        /// <summary>
        /// วันที่ใบเสร็จ
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// งวดชำระ
        /// </summary>
        public string PriceItemName { get; set; }

        /// <summary>
        /// ชำระโดย
        /// </summary>
        public string PaymentMethodName { get; set; }
        public Guid? PaymentMethodID{ get; set; }
        /// <summary>
        /// เลขที่ใบนำฝาก
        /// </summary>
        public string DepositNo { get; set; }

        /// <summary>
        /// ธนาคาร
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        public decimal Amount { get; set; }

        public static ReceiptDetailDTO CreateFromModel(dbqReceiptHeaderSP model)
        {
            if (model != null)
            {
                ReceiptDetailDTO result = new ReceiptDetailDTO()
                {
                    ReceiveDate = model.ReceiveDate,
                    PriceItemName = model.MasterPriceItemName,
                    PaymentMethodName = model.MethodName,
                    DepositNo = model.DepositNo,
                    BankName = model.BankName,
                    Amount = model.Amount,
                    PaymentMethodID = model.MethodID
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
