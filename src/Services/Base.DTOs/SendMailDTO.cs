using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs
{
    public class ParamMail
    {
        public partial class Param_Mail
        {
            public DateTime? MailTime { get; set; }
            public string MailFrom { get; set; }
            public string MailTo { get; set; }
            public string MailCC { get; set; }
            public string Topic { get; set; }
            public string Detail { get; set; }
            public string MailType { get; set; }
            public string Key1 { get; set; }
            public string Key2 { get; set; }
            public string Key3 { get; set; }
            public string Key4 { get; set; }
            public string MailFromName { get; set; }
            public string MailBCC { get; set; }
            public string SendStatus { get; set; }
            public string Result { get; set; }
            public List<MailAttachFileDTO> AttachFiles { get; set; }
        }
        public partial class Param_Mail2
        {
            public string TO { get; set; }
            public string CC { get; set; }
            public string BCC { get; set; }
            public string SenderMail { get; set; }
            public string SenderName { get; set; }
            public string SenderSystem { get; set; }
            public string Subject { get; set; }
            public string Content { get; set; }
            public string Ref1 { get; set; }
            public string Ref2 { get; set; }
            public string Ref3 { get; set; }
            public List<MailAttachFileDTO> AttachFiles { get; set; }
        }
    }
}
