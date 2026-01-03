using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.CMS
{
    public class RateSettingTransferExcelDTO
    {
        public int Success { get; set; }
        public int Error { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
