using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.DBO
{
    [Table("JobTransaction", Schema = Schema.DATA_MIGRATION)]
    public class JobTransaction : BaseEntity
    {

        [Description("ชื่อ")]
        public string Name { get; set; }

        [Description("สถานะ")]
        public string Status { get; set; }

        [Description("หมายเหตุ")]
        public string Note { get; set; }

        [Description("วันเวลาเริ่ม")]
        public DateTime StartDate { get; set; }

        [Description("วันเวลาเสร็จสิ้น")]
        public DateTime EndDate { get; set; }

    }

    public enum JobStatus
    {
        INPROGRESS,
        SUCCESS,
        FAILED
    }
}
