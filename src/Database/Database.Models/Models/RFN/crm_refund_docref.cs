using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.RFN
{
    [Description("ข้อมูลการคืนเงินลูกค้าหลังโอนกรรมสิทธิ เก็บข้อมูลเอกสาร")]
    [Table("crm_refund_docref", Schema = Schema.REFUND)]

    public class crm_refund_docref
    {
        [Description("")]
        [Key]
        public Guid? img_id { get; set; }
        //////////
        
        [Description("ประเภทของลูกค้า (บุคคลทั่วไป/นิติบุคคล)")]
        public Int64 refund_hyrf_id { get; set; }
        [ForeignKey("refund_hyrf_id")]
        public RFN.crm_contact_refund hyrf_id { get; set; }
        
        //[Description("test2)")]
        //public Guid? img_ref_contact_refund { get; set; }
        //////////

        public int img_seqn { get; set; }
        //////////

        [MaxLength(100)]
        public string img_type { get; set; }
        //////////

        [MaxLength(255)]
        public string img_name { get; set; }
        //////////

        [MaxLength(255)]
        public string minio_bucket_name { get; set; }
        //////////

        [MaxLength(255)]
        public string minio_img_file_name { get; set; }
        //////////

        [MaxLength(2000)]
        public string minio_img_url { get; set; }
        //////////

        public Guid? createby { get; set; }
        //////////

        public DateTime? createdate { get; set; }
        //////////

        public Guid? modifyby { get; set; }
        //////////

        public DateTime? modifydate { get; set; }
        //////////
    }
}
