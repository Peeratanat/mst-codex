using Base.DTOs.CTM;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTM_CustomerConsent.Params.Filters
{
    public class ConsentFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identity { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string ConsentText { get; set; }
        public Guid? ReferentType { get; set; }
        public string ReferentSubType { get; set; }
        public Guid? ProjectID { get; set; }
        public string ContactNumber { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public DateTime? UpdateDateFrom { get; set; }
        public DateTime? UpdateDateTo { get; set; }
        public string EditBy { get; set; }



    }
}
