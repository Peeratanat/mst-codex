using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CTM
{
	public class VisitorReportDTO
	{
        public Guid? ProjectID { get; set; }
        public DateTime? VisitDateInFrom { get; set; }
        public DateTime? VisitDateInTo { get; set; }

    }
}
