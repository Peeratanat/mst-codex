using System;
using System.Collections.Generic;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
    public class ContactListDTO
    {
        /// <summary>
        /// ID ของ Contact
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// รหัสของ Contact
        /// </summary>
        public string ContactNo { get; set; }
        /// <summary>
        /// ชื่อจริง/ชื่อบริษัท (ภาษาไทย)
        /// </summary>
        public string FirstNameTH { get; set; }
        /// <summary>
        /// ชื่อกลาง (ภาษาไทย)
        /// </summary>
        public string MiddleNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        public string LastNameTH { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์บ้าน
        /// </summary>
        public string HomeNumber { get; set; }
        /// <summary>
        /// จำนวน Opportunity
        /// </summary>
        public int OpportunityCount { get; set; }
        /// <summary>
        /// Last Opportunity
        /// </summary>
        public DateTime? LastOpportunityDate { get; set; }
        /// <summary>
        /// วันที่สร้าง
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// วันที่แก้ไข
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        /// <summary>
        /// เลขที่บัตรประชาชน
        /// </summary>
        public string CitizenIdentityNo { get; set; }
        /// <summary>
        /// เลขประจำตัวผู้เสียภาษี
        /// </summary>
        public string TaxID { get; set; }

        /// <summary>
        ///  คำนำหน้าชื่อ
        /// </summary>
        public string ContactTitleTH { get; set; }

        public string StatusQuestionaire { get; set; }

        public DateTime? VisitDate { get; set; }

        public static ContactListDTO CreateFromQueryResult(ContactQueryResult model)
        {
            if (model != null)
            {
                var result = new ContactListDTO();

                result.Id = model.Contact.ID;
                result.ContactNo = model.Contact.ContactNo;

                result.FirstNameTH = !string.IsNullOrEmpty(model.Contact.FirstNameTH) ? model.Contact.FirstNameTH : "";
                result.LastNameTH = !string.IsNullOrEmpty(model.Contact.LastNameTH) ? model.Contact.LastNameTH : "";
                result.PhoneNumber = !string.IsNullOrEmpty(model.ContactPhone?.PhoneNumber) ? model.ContactPhone?.PhoneNumber : "";
                result.CreatedDate = model.Contact.Created;
                result.UpdatedDate = model.Contact.Updated;
                result.CitizenIdentityNo = !string.IsNullOrEmpty(model.Contact.CitizenIdentityNo) ? model.Contact.CitizenIdentityNo : "";
                result.LastOpportunityDate = model.Contact.LastOpportunity?.Created;
                result.OpportunityCount = model.Contact.OpportunityCount;

                if (model.Contact.TitleExtTH != null)
                {
                    result.ContactTitleTH = model.Contact.TitleExtTH;
                }
                else
                {
                    result.ContactTitleTH = model.Contact.ContactTitleTH != null ? model.Contact.ContactTitleTH.Name : "";
                }
                return result;
            }
            else
            {
                return null;
            }
        }


    }

    public class ContactQueryResult
    {
        public models.CTM.Contact Contact { get; set; }
        public models.CTM.ContactPhone ContactPhone { get; set; }
        public models.CTM.Opportunity Opportunity { get; set; }
        public models.CTM.ContactEmail ContactEmail { get; set; }
    }
}
