using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class TitleDeedReportDTO
    {
        //public Guid? ProjectID { get; set; }
        public List<string> ProjectID { get; set; }
        public string ProjectNo { get; set; }
        public List<string> UnitNo { get; set; }
        [Description("วันที่สถานะโฉนด")]
        public DateTime? DateStart { get; set; }

    }
}
