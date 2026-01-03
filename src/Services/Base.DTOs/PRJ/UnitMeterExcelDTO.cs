using System;
using System.Collections.Generic;

namespace Base.DTOs.PRJ
{
    public class UnitMeterExcelDTO
    {
        public int Success { get; set; }
        public int Error { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
