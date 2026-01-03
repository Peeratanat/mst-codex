using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Base.DTOs.CTM
{
    public class UpdateCustomerConsentDTO
    {
        public List<ReferentData> ReferentDataList { get; set; }

        public MST.MasterCenterDTO ConsentType { get; set; }

        [Description("รหัสพนักงงานที่แก้ไข")]
        public string EmpoyeeNo { get; set; }

        [Description("ชื่อ-สุกล พนักงานที่แก้ไข")]
        public string EmpoyeeName { get; set; }

        [Description("แก้ไขจากระบบ eg. CRM3, E-Questionnaire")]
        public string CreateBySystem { get; set; }


        public class ReferentData 
        {
            [Description("ID Lead/Contact/Opp")]
            public Guid ReferentID { get; set; }

            [Description("ประเภทข้อมูล Lead/Contact/Opp")]
            public MST.MasterCenterDTO ReferentType { get; set; }
        }

        public class LastOpportunitieByContact
        {
            public Guid ContactID { get; set; }

            public DateTime? UpdateDate { get; set; }
        }
    }
}
