using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
    public class HeaderQuarterlyExcelModel
    {
        public string ProjectNo { get; set; }
        public decimal QuarterlyTotalAmount { get; set; }
        public int Year { get; set; }
        public int Quarter { get; set; }
    }
}
