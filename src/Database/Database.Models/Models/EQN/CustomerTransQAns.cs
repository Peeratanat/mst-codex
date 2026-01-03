using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.EQN
{
    [Table("CustomerTransQAns", Schema = Schema.EQN)]
    public class CustomerTransQAns
    {
        [Key]
        public Guid tran_id { get; set; }
        public string contact_ref_id { get; set; }
        public Guid? contact_ref_guid { get; set; }
        public string eqn_ref_id { get; set; }
        public string projectid { get; set; }
        public string project_name { get; set; }
        public string title_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string mobile_no { get; set; }
        public string consent_ads_flag { get; set; }
        public string consent_bc_flag { get; set; }
        public string channel_convenient { get; set; }
        public string csseen_media { get; set; }
        public string csbudget { get; set; }
        public string csincome { get; set; }
        public string family_income { get; set; }
        public string proj_compare { get; set; }
        public string products_interest { get; set; }
        public string cspersona { get; set; }
        public string reason_visit { get; set; }
        public string contradiction { get; set; }
        public string buyornot { get; set; }
        public int? age { get; set; }
        public string occupation { get; set; }
        public string marital_status { get; set; }
        public int? children_numb { get; set; }
        public string objective_considering { get; set; }
        public string housing_characteristics { get; set; }
        public string online_media { get; set; }
        public string offline_media { get; set; }
        public string visit_route { get; set; }
        public string deeplink_url { get; set; }
        public string other { get; set; }
        public string comment { get; set; }
        public int? total_answer { get; set; }
        public int? total_question { get; set; }
        public string submit_dttm { get; set; }
        public string lcowner { get; set; }
        public string process_flag { get; set; }
        public DateTime? created { get; set; }
        public string createdby { get; set; }
        public DateTime? updated { get; set; }
        public string updatedby { get; set; }
        public string tran_type { get; set; }
        public string revisit_flag { get; set; }
        public Guid? OpportunityID { get; set; }
        public int? total_required_answer { get; set; }
        public bool? is_bc_recommended { get; set; }
    }
}
