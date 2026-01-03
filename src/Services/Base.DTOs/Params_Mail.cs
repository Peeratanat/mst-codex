using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs
{
    public partial class Params_Mail
    {
        public Params_Mail(string _MailFrom, string _MailTo, string _Topic, string _Detail, string _MailType, string _Key1 = "")
        {
            MailFrom = _MailFrom;
            MailTo = _MailTo;
            Topic = _Topic;
            Detail = _Detail;
            MailType = _MailType;
            MailFrom = _MailFrom;
            Key1 = _Key1;
        }

        public Params_Mail(string _MailFrom, string _MailTo, string _MailCc, string _Topic, string _Detail, string _MailType, string _Key1 = "")
        {
            MailFrom = _MailFrom;
            MailTo = _MailTo;
            Topic = _Topic;
            Detail = _Detail;
            MailType = _MailType;
            MailFrom = _MailFrom;
            Key1 = _Key1;
            MailCC = _MailCc;
        }

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
        public List<Params_Mail_AttachFile> AttachFiles { get; set; }
    }

    public partial class Params_Mail_AttachFile
    {
        public string AttachFileID { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set; }
        public byte[] FileObject { get; set; }
    }
}
