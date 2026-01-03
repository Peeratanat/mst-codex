using System;
using System.Collections.Generic;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.CTM
{

    public class ContactActivityListDTO
    {
        /// <summary>
        /// ID ของ Activity
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ID ของ ContactId
        /// </summary>
        public Guid? ContactId  { get; set; }
        /// <summary>
        /// วันที่ทำจริง
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// ชื่อโครงการ
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// เลขที่แปลง
        /// </summary>
        public string UnitCode { get; set; }
        /// <summary>
        /// ประเภท Activity
        /// </summary>
        public int TypeActivity { get; set; }
        /// <summary>
        /// ชื่อประเภท Activity
        /// </summary>
        public string TypeName { get; set; }

        public List<ContactActivityListDTO> ContactActivityList { get; set; }

        public class ContactActivityQueryResult
        {
            public models.CTM.Lead Lead { get; set; }
            public models.CTM.Contact Contact { get; set; }
            public models.CTM.Opportunity Opportunity { get; set; }
            public models.MST.MasterCenter MasterCenters { get; set; }
            public models.PRJ.Project Project { get; set; }
            public models.SAL.Booking Booking { get; set; }
            public models.PRJ.Unit Unit { get; set; }
            public models.SAL.Agreement Agreement { get; set; }
            public models.SAL.AgreementOwner AgreementOwner { get; set; }
            public models.SAL.ChangeAgreementOwnerWorkflowDetail ChangeAgreementOwnerWorkflowDetail { get; set; }
        }
    }
}
