using Database.Models;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.RFN
{
    public class RefundContactDTO
    {
        public Int64 hyrf_id { get; set; }

        public string productid { get; set; }

        public string project { get; set; }

        public string companyid { get; set; }

        public string unitnumber { get; set; }

        public string addressnumber { get; set; }
        
        public string wbsnumber { get; set; }

        public string contractnumber { get; set; }

        public string transfernumber { get; set; }
        
        public DateTime? transferdateapprove { get; set; }

        public decimal? remainingtotalamount { get; set; }
        
        public string contactid { get; set; }

        public bool header { get; set; }

        public string personcardid { get; set; }
        
        public string namestitle { get; set; }
        
        public string firstname { get; set; }
        
        public string lastname { get; set; }
        
        public string fullname { get; set; }
        
        public string coownername { get; set; }
        
        public string nationality { get; set; }
        
        public string foreigner { get; set; }
        
        public string mobile { get; set; }

        public string email { get; set; }

        public string bankcode { get; set; }
        
        public string bankaccountno { get; set; }

        public string bankaccountname { get; set; }

        public string legalentityid { get; set; }

        public string legalentiryname { get; set; }

        public string legalbankcode { get; set; }
        
        public string legalbankaccountno { get; set; }

        public string tf01_appv_flag { get; set; }

        public DateTime? tf01_appv_date { get; set; }

        public string tf01_appv_by { get; set; }

        public string tf01_remarks { get; set; }

        public string tf02_appv_flag { get; set; }

        public DateTime? tf02_appv_date { get; set; }

        public string tf02_appv_by { get; set; }
        
        public string tf02_remarks { get; set; }
        
        public string ac01_appv_flag { get; set; }

        public DateTime? ac01_appv_date { get; set; }
        
        public string ac01_appv_by { get; set; }
        
        public string ac01_remarks { get; set; }

        public string ac02_appv_flag { get; set; }

        public DateTime? ac02_appv_date { get; set; }

        public string ac02_appv_by { get; set; }

        public string ac02_remarks { get; set; }
        
        public DateTime? ac02_due_date { get; set; }
        
        public string ac03_reject_doc_flag { get; set; }
        
        public string ac03_reject_reason { get; set; }

        public string ac03_change_due_flag { get; set; }

        public DateTime? ac03_change_due_date { get; set; }

        public string email_sent_status { get; set; }

        public DateTime? email_sent_date { get; set; }

        public string email_thx_sent_status { get; set; }

        public DateTime? email_thx_sent_date { get; set; }
        
        public string email_reject_doc_status { get; set; }
        
        public DateTime? email_reject_doc_date { get; set; }

        public string email_change_due { get; set; }

        public DateTime? email_change_due_date { get; set; }

        public string sms_sent_status { get; set; }
        
        public DateTime? sms_sent_date { get; set; }

        public string sms_thx_sent_status { get; set; }
        
        public DateTime? sms_thx_sent_date { get; set; }
        
        public string sms_reject_doc_status { get; set; }
        
        public DateTime? sms_reject_doc_date { get; set; }
        
        public string sms_change_due { get; set; }
        
        public DateTime? sms_change_due_date { get; set; }

        public string line_sent_status { get; set; }
        
        public DateTime? line_sent_date { get; set; }

        public string doc_sent_status { get; set; }

        public DateTime? doc_sent_date { get; set; }

        public string doc_merge_url { get; set; }
        
        public string tran_status { get; set; }
        
        public string createby { get; set; }
        
        public DateTime? createdate { get; set; }
        
        public string modifyby { get; set; }
        
        public DateTime? modifydate { get; set; }

        public string ac_appv_by { get; set; }

        public string ac_appv_type { get; set; }

        public string ac_remarks { get; set; }
        public string gl_txt_file_name { get; set; }
        public string gl_txt_street { get; set; }
        public string gl_txt_city { get; set; }

        public decimal? bringtolegalentity_amount { get; set; }
        public string bringtolegalentity_flag { get; set; }
        public decimal? welcomehome_amount { get; set; }
        public string welcomehome_flag { get; set; }
        public string nameth { get; set; }
        public string nameen { get; set; }

        public string bank_key { get; set; }
        public string bot_bank_code { get; set; }
        public string bot_bank_branch_code { get; set; }
        public string bot_bank_branch_name { get; set; }


        //public static RefundContactDTO CreateFromModel(crm_contact_refund model)
        //{
        //    if (model != null)
        //    {
        //        var result = new RefundContactDTO()
        //        {
        //            //
        //        };

        //        return result;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //public void ToRefundContactApproveModel(ref crm_contact_refund model)
        //{
        //    model.hyrf_id = this.hyrf_id;

        //    model.ac01_appv_by = this.ac01_appv_by;
        //    model.ac01_remarks = this.ac01_remarks;
        //    model.ac01_appv_date = this.ac01_appv_date;

        //    model.ac02_appv_by = this.ac02_appv_by;
        //    model.ac02_remarks = this.ac02_remarks;
        //    model.ac02_appv_date = this.ac02_appv_date;

        //}
    }
}
