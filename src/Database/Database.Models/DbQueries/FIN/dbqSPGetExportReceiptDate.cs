using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.FIN
{
    public class dbqSPGetExportReceiptDate
    {
        public Guid? PaymentID { get; set; }

        public string ReceiptTempNo { get; set; }

        public Guid? ReceiptHeaderID { get; set; }
        public string ReceiptNo { get; set; }

        public string SendType { get; set; }
        public DateTime? SendDate { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}


