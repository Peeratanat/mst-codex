using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.DTOs.FIN
{
    public class ReceiptListDTO
    {
        [Description("เลขที่ใบเสร็จ")]
        public string ReceiptNo { get; set; }
    }
}
