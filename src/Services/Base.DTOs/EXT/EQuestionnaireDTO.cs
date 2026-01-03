using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class EQuestionnaireDTO : BaseDTO
    {
        /// <summary>
        /// รหัสโครงการ ex.40017
        /// </summary>
        [Description("รหัสโครงการ")]
        public string projectid { get; set; }
        /// <summary>
        /// ชื่อโครงการ
        /// </summary>
        [Description("ชื่อโครงการ")]
        public string project_name { get; set; }
        /// <summary>
        /// คำนำหน้าชื่อ
        /// </summary>
        [Description("คำนำหน้าชื่อ")]
        public string title_name { get; set; }
        /// <summary>
        /// ชื่อลูกค้า
        /// </summary>
        [Description("ชื่อลูกค้า")]
        public string first_name { get; set; }
        /// <summary>
        /// นามสกุลลูกค้า
        /// </summary>
        [Description("นามสกุลลูกค้า")]
        public string last_name { get; set; }
        /// <summary>
        /// email ลูกค้า
        /// </summary>
        [Description("email ลูกค้า")]
        public string email { get; set; }
        /// <summary>
        /// เบอร์ติดต่อลูกค้า
        /// </summary>
        [Description("เบอร์ติดต่อลูกค้า")]
        public string mobile_no { get; set; }
        /// <summary>
        /// flag เพื่อระบุว่าลูกค้าให้ความยินยอมในทำ direct (y = ให้ความยินยอมทำ direct marketing ทุกโครงการ, n = ยินยอมให้ทำ direct marketing เฉพาะโครงการที่ทำแบบสอบถาม)
        /// </summary>
        [Description("flag เพื่อระบุว่าลูกค้าให้ความยินยอมในทำ direct")]
        public string consent_ads_flag { get; set; }
        /// <summary>
        /// flag เพื่อระบุว่าลูกค้าให้ความยินยอมในทำในการเผยแพ่ข้อมูลให้ bangkok citismart (bc) - n (default)- y
        /// </summary>
        [Description("flag เพื่อระบุว่าลูกค้าให้ความยินยอมในทำในการเผยแพ่ข้อมูลให้ bangkok citismart (bc) ")]
        public string consent_bc_flag { get; set; }
        /// <summary>
        /// ช่องทางที่ลูกค้าสะดวกให้ติดต่อvalues:- e = email- m = mobile- t = tel.phone- o = other
        /// </summary>
        [Description("ช่องทางที่ลูกค้าสะดวกให้ติดต่อ")]
        public string channel_convenient { get; set; }
        /// <summary>
        /// สื่อที่พบเห็นโครงการ
        /// </summary>
        [Description("สื่อที่พบเห็นโครงการ")]
        public string csseen_media { get; set; }
        /// <summary>
        /// งบประมาณ
        /// </summary>
        [Description("งบประมาณ")]
        public string csbudget { get; set; }
        /// <summary>
        /// รายได้
        /// </summary>
        [Description("รายได้")]
        public string csincome { get; set; }
        /// <summary>
        /// รายได้ครอบครัว
        /// </summary>
        [Description("รายได้ครอบครัว")]
        public string family_income { get; set; }
        /// <summary>
        /// โครงการเปรียบเทียบ
        /// </summary>
        [Description("โครงการเปรียบเทียบ")]
        public string proj_compare { get; set; }
        /// <summary>
        /// ลักษณะสินค้าที่สนใจ
        /// </summary>
        [Description("ลักษณะสินค้าที่สนใจ")]
        public string products_interest { get; set; }
        /// <summary>
        /// ลักษณะเด่นของลูกค้า
        /// </summary>
        [Description("ลักษณะเด่นของลูกค้า")]
        public string cspersona { get; set; }
        /// <summary>
        /// เหตุผลที่แวะมาชมโครงการ ap
        /// </summary>
        [Description("เหตุผลที่แวะมาชมโครงการ ap")]
        public string reason_visit { get; set; }
        /// <summary>
        /// ข้อโต้แย้ง
        /// </summary>
        [Description("ข้อโต้แย้ง")]
        public string contradiction { get; set; }
        /// <summary>
        /// ลูกค้าจะซื้อโครงการเราหรือไม่
        /// </summary>
        [Description("ลูกค้าจะซื้อโครงการเราหรือไม่")]
        public string buyornot { get; set; }
        /// <summary>
        /// อายุ
        /// </summary>
        [Description("อายุ")]
        public int age { get; set; }
        /// <summary>
        /// อาชีพ
        /// </summary>
        [Description("อาชีพ")]
        public string occupation { get; set; }
        /// <summary>
        /// สถานภาพการสมรส
        /// </summary>
        [Description("สถานภาพการสมรส")]
        public string marital_status { get; set; }
        /// <summary>
        /// จำนวนบุตร
        /// </summary>
        [Description("จำนวนบุตร")]
        public int children_numb { get; set; }
        /// <summary>
        /// วัตถุประสงค์หลักที่ใช้ในการพิจารณาเลือกซื้อ
        /// </summary>
        [Description("วัตถุประสงค์หลักที่ใช้ในการพิจารณาเลือกซื้อ")]
        public string objective_considering { get; set; }
        /// <summary>
        /// ลักษณะที่อยู่อาศัยปัจจุบัน
        /// </summary>
        [Description("ลักษณะที่อยู่อาศัยปัจจุบัน")]
        public string housing_characteristics { get; set; }
        /// <summary>
        /// สื่ออื่นๆที่ท่านพบเห็น online
        /// </summary>
        [Description("สื่ออื่นๆที่ท่านพบเห็น online")]
        public string online_media { get; set; }
        /// <summary>
        /// สื่ออื่นๆที่ท่านพบเห็น (offline)
        /// </summary>
        [Description("สื่ออื่นๆที่ท่านพบเห็น (offline)")]
        public string offline_media { get; set; }
        /// <summary>
        /// วันนี้ท่านเดินทางมายังโครงการโดยใช้เส้นทางใด
        /// </summary>
        [Description("วันนี้ท่านเดินทางมายังโครงการโดยใช้เส้นทางใด")]
        public string visit_route { get; set; }
        /// <summary>
        /// url สำหรับ link ไปเปิดข้อมูลลูกค้าที่ระบบ e-questionnaire
        /// </summary>
        [Description("url สำหรับ link ไปเปิดข้อมูลลูกค้าที่ระบบ e-questionnaire")]
        public string deeplink_url { get; set; }
        /// <summary>
        /// lc ที่ต้อนรับลูกค้า (รหัสพนักงาน ap003910 เป็นต้น)
        /// </summary>
        [Description("lc ที่ต้อนรับลูกค้า")]
        public string lcowner { get; set; }
        /// <summary>
        /// อื่นๆ
        /// </summary>
        [Description("อื่นๆ")]
        public string other { get; set; }
        /// <summary>
        /// ความคิดเห็นพนักงาน
        /// </summary>
        [Description("ความคิดเห็นพนักงาน")]
        public string comment { get; set; }
        /// <summary>
        /// จำนวนข้อที่ลูกค้าตอบแบบสอบถาม
        /// </summary>
        [Description("จำนวนข้อที่ลูกค้าตอบแบบสอบถาม")]
        public int total_answer { get; set; }
        /// <summary>
        /// จำนวนข้อทั้งหมดของแบบสอบถาม
        /// </summary>
        [Description("จำนวนข้อทั้งหมดของแบบสอบถาม")]
        public int total_question { get; set; }
        /// <summary>
        /// วัน/เวลา ที่ submit รายการตอบแบบสอบถาม format yyyymmddhhmmss
        /// </summary>
        [Description("วัน/เวลา ที่ submit รายการตอบแบบสอบถาม")]
        public string submit_dttm { get; set; }

        /// <summary>
        /// a = add new contact/create opp./walk กรณีที่ lc ตัดสินใจเลือกเป็น new ลูกค้าใหม่ในระบบ crm u = existing contact/create opp./walk ในกรณีที่ lc เลือกรายการใดรายการหนึ่งใน list ลูกค้าที่แสดง    
        /// </summary>
        [Description("tran_type")]
        public string tran_type { get; set; }
        /// <summary>
        /// reference contact no. จากระบบ crm สำหรับกรณีที่เลือก trns_type = ‘u’ จะต้องส่งข้อมูล contact_ref_id มาด้วย
        /// </summary>
        [Description("reference contact no. จากระบบ crm สำหรับกรณีที่เลือก trns_type = ‘u’ จะต้องส่งข้อมูล contact_ref_id มาด้วย")]
        public string contact_ref_id { get; set; }
        /// <summary>
        /// reference contact id (guid) จากระบบ crm สำหรับกรณีที่เลือก trns_type = ‘u’ จะต้องส่งข้อมูล contact_ref_id มาด้วย
        /// </summary>
        [Description("reference contact id (guid) จากระบบ crm สำหรับกรณีที่เลือก trns_type = ‘u’ จะต้องส่งข้อมูล contact_ref_id มาด้วย")]
        public string contact_ref_guid { get; set; }
        /// <summary>
        /// flag เพื่อระบุว่าลูกค้าเคยเข้าโครงการนี้มาแล้วหรือไม่ values:- n(default) หมายถึงยังไม่เคยเข้าโครงการ first warlk- y หมายถึง revisit
        /// </summary>
        [Description("เพื่อระบุว่าลูกค้าเคยเข้าโครงการนี้มาแล้วหรือไม่ n(default) = ยังไม่เคยเข้าโครงการ first warlk, y = revisit")]
        public string revisit_flag { get; set; }

        public void ToModel(ref models.EQN.CustomerTransQAns model)
        {
            model.projectid = this.projectid;
            model.project_name = this.project_name;
            model.title_name = this.title_name;
            model.first_name = this.first_name;
            model.last_name = this.last_name;
            model.email = this.email;
            model.mobile_no = this.mobile_no;
            model.consent_ads_flag = this.consent_ads_flag;
            model.consent_bc_flag = this.consent_bc_flag;
            model.channel_convenient = this.channel_convenient;
            model.csseen_media = this.csseen_media;
            model.csbudget = this.csbudget;
            model.csincome = this.csincome;
            model.family_income = this.family_income;
            model.proj_compare = this.proj_compare;
            model.products_interest = this.products_interest;
            model.cspersona = this.cspersona;
            model.reason_visit = this.reason_visit;
            model.contradiction = this.contradiction;
            model.buyornot = this.buyornot;
            model.age = this.age;
            model.occupation = this.occupation;
            model.marital_status = this.marital_status;
            model.children_numb = this.children_numb;
            model.objective_considering = this.objective_considering;


            model.housing_characteristics = this.housing_characteristics;
            model.online_media = this.online_media;
            model.offline_media = this.offline_media;
            model.visit_route = this.visit_route;
            model.deeplink_url = this.deeplink_url;
            model.lcowner = this.lcowner;
            model.other = this.other;
            model.comment = this.comment;
            model.total_answer = this.total_answer;
            model.total_question = this.total_question;
            model.submit_dttm = this.submit_dttm;
            //model.tran_type = this.tran_type;
            model.contact_ref_id = this.contact_ref_id;
            //if (this.tran_type.Equals("U"))
            //{
            if (!string.IsNullOrEmpty(this.contact_ref_guid))
            {
                model.contact_ref_guid = Guid.Parse(this.contact_ref_guid);
            }
            //}
            //model.revisit_flag = this.revisit_flag;

        }

    }
}
