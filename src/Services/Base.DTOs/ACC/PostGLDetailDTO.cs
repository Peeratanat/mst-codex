using Base.DTOs.MST;
using System.ComponentModel;

namespace Base.DTOs.ACC
{
    public class PostGLDetailDTO 
    {
        /// <summary>
        /// ประเภทรายการ Credit/Debit
        /// </summary>
        [Description("ประเภทรายการ Credit/Debit")]
        public string PostGLType { get; set; }

        /// <summary>
        /// เลขที่ GL และ ชื่อ GL
        /// </summary>
        [Description("เลขที่ GL")]
        public string GLAccount { get; set; }

        /// <summary>
        /// เลขที่ GL และ ชื่อ GL
        /// </summary>
        [Description("ชื่อ GL")]
        public string GLAccountName { get; set; }

        /// <summary>
        /// จำนวนเงิน
        /// </summary>
        [Description("จำนวนเงิน")]
        public decimal Amount { get; set; }


        public string AccountCode { get; set; }
        public string WBSNumber { get; set; }
        public string ProfitCenter { get; set; }
        public string CostCenter { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
        public string Assignment { get; set; }
        public string TaxCode { get; set; }
        public string ObjectNumber { get; set; }
        public string CustomerName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
