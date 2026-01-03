using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_UnitInfos.Params.Filters
{
    public class PublicAppointmentFilter
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public string ProjectCode { get; set; }
        public string appointmentDate { get; set; }

    }
}
