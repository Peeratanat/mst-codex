using System;
using System.Collections.Generic;

namespace Database.Models.DbQueries.FIN
{
    public class dbqSPPaymentUnitPriceList
    {
        public Guid? ID { get; set; }
        public string Name { get; set; }

        public decimal? ItemAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public DateTime? PaymentDate { get; set; }

        public decimal? RemainAmount { get; set; }
        public decimal? PayAmount { get; set; }
        public int? Orders { get; set; }
        public Guid? MasterPriceItemID { get; set; }
        public string MasterPriceItemKey { get; set; }

        public string MasterPriceItemDetail { get; set; }

        public string MasterPriceItemDetailEN { get; set; }

        public int? Period { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid? UnitPriceID { get; set; }

    }

    public class PaymentUnitPriceList
    {
        public Guid BookingID { get; set; }

        public List<dbqSPPaymentUnitPriceList> UnitPrices { get; set; }
    }
}
