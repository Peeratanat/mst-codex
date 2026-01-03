using System;
using System.Collections.Generic;

namespace Base.DTOs.PRJ
{
    public class UnitInitialExcelDTO
    {
        public int Success { get; set; }
        public int Error { get; set; }
        public int Delete { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<string> Messages { get; set; }
    }
}
