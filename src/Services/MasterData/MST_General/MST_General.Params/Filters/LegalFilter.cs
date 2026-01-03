using System;
using System.Collections.Generic;
using System.Text;

namespace MST_General.Params.Filters
{
    public class LegalFilter : BaseFilter
    {
        /// <summary>
        /// ชื่อ นิติบุคคลอาคารชุด ภาษาไทย (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อ นิติบุคคลอาคารชุด ภาษาอังกฤษ (TH)
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// Identity ธนาคาร
        /// </summary>
        public Guid? BankID { get; set; }
        /// <summary>
        /// ประเภทบัญชี
        /// </summary>
        public string BankAccountTypeKey { get; set; }
        /// <summary>
        /// เลขบัญชีธนาคาร
        /// </summary>
        public string BankAccountNo { get; set; }
        /// <summary>
        /// สถานะ Active/InActive
        /// </summary>
        public bool? IsActive { get; set; }
        /// <summary>
        /// รหัสนิติ
        /// </summary>
        public string VendorCode { get; set; }
   
        public Guid? ProjectID { get; set; }
    }
}
