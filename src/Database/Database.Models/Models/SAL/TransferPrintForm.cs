using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("ข้อมูลในการเตรียมเอกสารโอนกรรมสิทธิ์")]
    [Table("TransferPrintForm", Schema = Schema.SALE)]
    public class TransferPrintForm : BaseEntity
    {
        [Description("โอนกรรมสิทธิ์")]
        public Guid? TransferID { get; set; }
        [ForeignKey("TransferID")]
        public SAL.Transfer Transfer { get; set; }

        [Description("ผู้รับมอบอำนาจโอนกรรมสิทธิ์")]
        public Guid? AttorneyNameTransferID { get; set; }
        [ForeignKey("AttorneyNameTransferID")]
        public MST.AttorneyTransfer AttorneyNameTransfer { get; set; }
        [Description("ผู้รับมอบอำนาจแทนบริษัท")]
        [MaxLength(200)]
        public string AttorneyNameCompany { get; set; }
        [Description("วันที่แทนตามมอบ ลว.(บริษัท)")]
        public DateTime? AttorneyDateCompany { get; set; }

        [Description("ผู้รับมอบอำนาจแทนลูกค้า")]
        [MaxLength(200)]
        public string AttorneyNameCustomer { get; set; }
        [Description("วันที่แทนตามมอบ ลว.(ลูกค้า)")]
        public DateTime? AttorneyDateCustomer { get; set; }

        [Description("ประเภท/แบบบ้าน")]
        [MaxLength(200)]
        public string ModelName { get; set; }

        [Description("วัตถุประสงค์ของการซื้อของนิติบุคคล")]
        public Guid? CorporateBuyingReasonMasterCenterID { get; set; }
        [ForeignKey("CorporateBuyingReasonMasterCenterID")]
        public MST.MasterCenter CorporateBuyingReason { get; set; }
        [Description("วัตถุประสงค์ของการซื้อของนิติบุคคลอื่นๆ")]
        [MaxLength(200)]
        public string CorporateBuyingReasonOther { get; set; }

        [Description("ประเภทรั้ว")]
        public Guid? FenceTypeMasterCenterID { get; set; }
        [ForeignKey("FenceTypeMasterCenterID")]
        public MST.MasterCenter FenceType { get; set; }

        [Description("ข้อ.3 อื่นๆ")]
        [MaxLength(200)]
        public string No3Other { get; set; }
    }
}
