using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.EXT
{
	public class CustomerTransQAnsDTO
	{
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

		public async static Task<CustomerTransQAnsDTO> CreateFromModelAsync(models.EQN.CustomerTransQAns model)
		{
			if (model != null)
			{
				CustomerTransQAnsDTO result = new CustomerTransQAnsDTO()
				{
                    tran_id = model.tran_id,
                    contact_ref_id = model.contact_ref_id,
                    contact_ref_guid = model.contact_ref_guid,
                    eqn_ref_id = model.eqn_ref_id,
                    projectid = model.projectid,
                    project_name = model.project_name,
                    title_name = model.title_name,
                    first_name = model.first_name,
                    last_name = model.last_name,
                    email = model.email,
                    mobile_no = model.mobile_no,
                    consent_ads_flag = model.consent_ads_flag,
                    consent_bc_flag = model.consent_bc_flag,
                    channel_convenient = model.channel_convenient,
                    csseen_media = model.csseen_media,
                    csbudget = model.csbudget,
                    csincome = model.csincome,
                    family_income = model.family_income,
                    proj_compare = model.proj_compare,
                    products_interest = model.products_interest,
                    cspersona = model.cspersona,
                    reason_visit = model.reason_visit,
                    contradiction = model.contradiction,
                    buyornot = model.buyornot,
                     age = model.age,
                    occupation = model.occupation,
                    marital_status = model.marital_status,
                    children_numb = model.children_numb,
                    objective_considering = model.objective_considering,
                    housing_characteristics = model.housing_characteristics,
                    online_media = model.online_media,
                    offline_media = model.offline_media,
                    visit_route = model.visit_route,
                    deeplink_url = model.deeplink_url,
                    other = model.other,
                    comment = model.comment,
                    total_answer = model.total_answer,
                    total_question = model.total_question,
                    submit_dttm = model.submit_dttm,
                    lcowner = model.lcowner,
                    process_flag = model.process_flag,
                    created = model.created,
                    createdby = model.createdby,
                    updated = model.updated,
                    updatedby = model.updatedby,
                    tran_type = model.tran_type,
                    revisit_flag = model.revisit_flag,
                    OpportunityID = model.OpportunityID,
                    total_required_answer = model.total_required_answer
                };

				return result;
			}
			else
			{
                CustomerTransQAnsDTO result = new CustomerTransQAnsDTO();
                return result;
			}
		}
	}
}
