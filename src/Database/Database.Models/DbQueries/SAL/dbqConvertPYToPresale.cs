using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
	public class dbqConvertPYToPresale
    {
		public string BookingID { get; set; }
		public string QuotationID { get; set; }
		public string QuotationID_FromPrebook { get; set; }
		public string PaymentID { get; set; }
		public string ReceiptTempHeaderID { get; set; }

	}
}
