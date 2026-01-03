using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqCHECKTitledeedRequestFlowChangeDue
    {
        public int? StatusCode { get; set; }
        public string StatusMsg { get; set; }
        public int? ConfigNumberOfDays { get; set; }
        public int? ConfigIsFullDay { get; set; }
        public int? IsPassHalfDay { get; set; }
        public DateTime? ScheduleDate { get; set; }

        public DateTime? RequestDate { get; set; }
        public DateTime? AddNumbFullDayDate { get; set; }
        public DateTime? MinusNumbDayDate { get; set; }
        public TimeSpan RequestTime { get; set; }
        public string RequestAllDayFlag { get; set; }
    }
}
