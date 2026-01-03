using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs
{
    public class MailAttachFileDTO
    { 
        public string AttachFileID { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set; }
        public byte[] FileObject { get; set; } 
    }
}
