using Base.DbQueries;
using System;

namespace Database.Models.DbQueries.Finance
{
    public class fn_BillPayment_Validate_ReferentKey
    {
        public Guid? BookingID { get; set; }
        public bool? BookingIsDeleted { get; set; }
        public bool? BookingCancelled { get; set; }
        public bool? BookingIsPaid { get; set; }
        public bool? AgreementIsDeleted { get; set; }
        public bool? AgreementCancelled { get; set; }
        
        public Guid? TransferAgreementID { get; set; }
        public bool? TransferIsDeleted { get; set; }
        public string ContactNo { get; set; }
        
    }
}
