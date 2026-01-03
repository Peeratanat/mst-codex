using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
	public class InputQuestionnaireContactDTO
	{

        /// <summary>
        /// AppCode
        /// </summary>
        public string AppCode { get; set; }
        /// <summary>
        /// ProjectID
        /// </summary>
        public Guid? ProjectID{ get; set; }
        /// <summary>
        /// ID Contact
        /// </summary>
        public Guid? ContactID { get; set; }
        /// <summary>
        /// วันที่เยี่ยมชมโครงการ
        /// </summary>
        public DateTime? VisitDate { get; set; }
    }

}
