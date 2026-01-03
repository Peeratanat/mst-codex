using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Base.DTOs
{
    public class ExportExcel
    {
        public byte[] FileContent { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
    }
}
