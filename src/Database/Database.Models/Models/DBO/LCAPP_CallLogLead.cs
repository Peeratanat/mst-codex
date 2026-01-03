using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.DBO
{
    [Table("LCAPP_CallLogLead", Schema = Schema.DBO)]
    public class LCAPP_CallLogLead
    {
        public Guid ID { get; set; }
        public Guid ActivityID { get; set; }
        public string PhoneNumber { get; set; }
        public string CallDuration { get; set; }
        public int CallTotalSecond { get; set; }
        public DateTime StartCallTime { get; set; }
        public DateTime EndCallTime { get; set; }
        public string DeviceType { get; set; }
        public bool IsMissCall { get; set; }
        public bool IsFinished { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public Guid CreatedByUserID { get; set; }
        public Guid? UpdatedByUserID { get; set; }
    }
}
