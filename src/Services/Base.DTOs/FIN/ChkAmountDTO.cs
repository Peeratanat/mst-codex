using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Database.Models.FIN;

namespace Base.DTOs.FIN
{
    public class ChkAmountDTO
    {
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptNo { get; set; }
        [Description("เลขที่ใบเสร็จชั่วคราว")]
        public string ReceiptTempNo { get; set; }
        [Description("BookingID")]
        public Guid? BookingId { get; set; }


        public static ChkAmountDTO CreateFromModel(Payment model)
        {
            if (model != null)
            {
                var result = new ChkAmountDTO()
                {
                    ReceiptNo = model.ReceiptNo,
                    ReceiptTempNo = model.ReceiptTempNo,
                    BookingId = model.BookingID,
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
