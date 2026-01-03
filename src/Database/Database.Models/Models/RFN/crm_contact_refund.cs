using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.RFN
{
    [Description("ข้อมูลการคืนเงินลูกค้าหลังโอนกรรมสิทธิ")]
    [Table("crm_contact_refund", Schema = Schema.REFUND)]

    public class crm_contact_refund
    {
        [Key]
        public Int64 hyrf_id { get; set; }
        //////////

        [MaxLength(15)]
        public string productid { get; set; }
        //////////

        [MaxLength(255)]
        public string project { get; set; }
        //////////

        [MaxLength(15)]
        public string companyid { get; set; }
        //////////

        [MaxLength(10)]
        public string unitnumber { get; set; }
        //////////

        [MaxLength(10)]
        public string addressnumber { get; set; }
        //////////

        [MaxLength(30)]
        public string wbsnumber { get; set; }
        //////////

        [MaxLength(20)]
        public string contractnumber { get; set; }
        //////////

        [MaxLength(50)]
        public string transfernumber { get; set; }
        //////////

        public DateTime? transferdateapprove { get; set; }
        //////////

        [Column(TypeName = "Money")]
        public decimal? remainingtotalamount { get; set; }
        //////////

        [MaxLength(50)]
        public string contactid { get; set; }
        //////////

        public bool header { get; set; }
        //////////

        [MaxLength(20)]
        public string personcardid { get; set; }
        //////////

        [MaxLength(50)]
        public string namestitle { get; set; }
        //////////

        [MaxLength(100)]
        public string firstname { get; set; }
        //////////

        [MaxLength(100)]
        public string lastname { get; set; }
        //////////

        [MaxLength(255)]
        public string fullname { get; set; }
        //////////

        [MaxLength(500)]
        public string coownername { get; set; }
        //////////

        [MaxLength(50)]
        public string nationality { get; set; }
        //////////

        [MaxLength(2)]
        public string foreigner { get; set; }
        //////////

        [MaxLength(100)]
        public string mobile { get; set; }
        //////////

        [MaxLength(255)]
        public string email { get; set; }
        //////////

        [MaxLength(15)]
        public string bankcode { get; set; }
        //////////

        [MaxLength(20)]
        public string bankaccountno { get; set; }
        //////////

        [MaxLength(500)]
        public string bankaccountname { get; set; }
        //////////

        [MaxLength(10)]
        public string legalentityid { get; set; }
        //////////

        [MaxLength(1000)]
        public string legalentiryname { get; set; }
        //////////

        [MaxLength(15)]
        public string legalbankcode { get; set; }
        //////////

        [MaxLength(20)]
        public string legalbankaccountno { get; set; }
        //////////

        [MaxLength(2)]
        public string tf01_appv_flag { get; set; }
        //////////

        public DateTime? tf01_appv_date { get; set; }
        //////////

        [MaxLength(50)]
        public string tf01_appv_by { get; set; }
        //////////

        [MaxLength(2000)]
        public string tf01_remarks { get; set; }
        //////////

        [MaxLength(2)]
        public string tf02_appv_flag { get; set; }
        //////////

        public DateTime? tf02_appv_date { get; set; }
        //////////

        [MaxLength(50)]
        public string tf02_appv_by { get; set; }
        //////////

        [MaxLength(2000)]
        public string tf02_remarks { get; set; }
        //////////

        [MaxLength(2)]
        public string ac01_appv_flag { get; set; }
        //////////


        public DateTime? ac01_appv_date { get; set; }
        //////////

        [MaxLength(50)]
        public string ac01_appv_by { get; set; }
        //////////

        [MaxLength(2000)]
        public string ac01_remarks { get; set; }
        //////////

        [MaxLength(2)]
        public string ac02_appv_flag { get; set; }
        //////////

        public DateTime? ac02_appv_date { get; set; }
        //////////

        [MaxLength(50)]
        public string ac02_appv_by { get; set; }
        //////////

        [MaxLength(2000)]
        public string ac02_remarks { get; set; }
        //////////

        public DateTime? ac02_due_date { get; set; }
        //////////

        [MaxLength(2)]
        public string ac03_reject_doc_flag { get; set; }
        //////////

        [MaxLength(500)]
        public string ac03_reject_reason { get; set; }
        //////////

        [MaxLength(2)]
        public string ac03_change_due_flag { get; set; }
        //////////

        public DateTime? ac03_change_due_date { get; set; }
        //////////

        [MaxLength(2)]
        public string email_sent_status { get; set; }
        //////////

        public DateTime? email_sent_date { get; set; }
        //////////

        [MaxLength(2)]
        public string email_thx_sent_status { get; set; }
        //////////

        public DateTime? email_thx_sent_date { get; set; }
        //////////

        [MaxLength(2)]
        public string email_reject_doc_status { get; set; }
        //////////

        public DateTime? email_reject_doc_date { get; set; }
        //////////

        [MaxLength(2)]
        public string email_change_due { get; set; }
        //////////

        public DateTime? email_change_due_date { get; set; }
        //////////

        [MaxLength(2)]
        public string sms_sent_status { get; set; }
        //////////

        public DateTime? sms_sent_date { get; set; }
        //////////

        [MaxLength(2)]
        public string sms_thx_sent_status { get; set; }
        //////////

        public DateTime? sms_thx_sent_date { get; set; }
        //////////

        [MaxLength(2)]
        public string sms_reject_doc_status { get; set; }
        //////////

        public DateTime? sms_reject_doc_date { get; set; }
        //////////

        [MaxLength(2)]
        public string sms_change_due { get; set; }
        //////////

        public DateTime? sms_change_due_date { get; set; }
        //////////

        [MaxLength(2)]
        public string line_sent_status { get; set; }
        //////////

        public DateTime? line_sent_date { get; set; }
        //////////

        [MaxLength(2)]
        public string doc_sent_status { get; set; }
        //////////

        public DateTime? doc_sent_date { get; set; }
        //////////

        [MaxLength(255)]
        public string doc_merge_url { get; set; }
        //////////

        [MaxLength(2)]
        public string tran_status { get; set; }
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
