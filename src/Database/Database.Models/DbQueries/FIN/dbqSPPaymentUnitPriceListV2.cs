using System;
using System.Collections.Generic;

namespace Database.Models.DbQueries.FIN
{
    public class dbqSPPaymentUnitPriceListV2
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

        public bool? IsPaidAll { get; set; }

        public bool? IsPaidPartial { get; set; }

        public Guid? PaymentMethodTypeID { get; set; }

        public string PaymentMethodKey { get; set; }

        public string PaymentMethodName { get; set; }

    }

    public class PaymentUnitPriceListV2
    {
        public Guid BookingID { get; set; }

        public List<dbqSPPaymentUnitPriceListV2> UnitPrices { get; set; }
    }
}
