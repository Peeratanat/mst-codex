using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
	public class dbqGetQuotation
    {
		public string QuotationID { get; set; }
		public string QuotationNo { get; set; }
		public string ProjectID { get; set; }
		public string UnitID { get; set; }
		public string QuotationSalePromotionID { get; set; }
		public string QuotationUnitPriceID { get; set; }

	}
}
