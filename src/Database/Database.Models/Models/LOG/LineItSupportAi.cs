using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Description("เก็บ log การ")]
    [Table("line_it_support_ai", Schema = Schema.LOG)]
    public class LineItSupportAi 
    {
        [Key]
        public Guid id { get; set; }
        public string reply_token { get; set; }
        public string source_userId { get; set; }
        public string message_type { get; set; }
        public string message_text { get; set; }
        public string message_reply { get; set; }
        public string createby { get; set; }
        public string modifyby { get; set; }
        public DateTime? createdate { get; set; }
        public DateTime? modifydate { get; set; }

    }
}
