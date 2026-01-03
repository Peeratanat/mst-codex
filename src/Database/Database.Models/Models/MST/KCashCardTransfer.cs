using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("ผู้ถือบัตร K-Cash Card ในเอกสารโอน")]
    [Table("KCashCardTransfer", Schema = Schema.MASTER)]
    public class KCashCardTransfer : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public PRJ.Project Project { get; set; }
        
        [Description("ลำดับ")]
        public int Seqn_No { get; set; }
        
        [Description("ID Reference User")]
        public Guid? ProjectOwnerByUserID { get; set; }
        [ForeignKey("ProjectOwnerByUserID")]
        public USR.User ProjectOwnerByUser { get; set; }
        
        [Description("รหัสพนักงาน")]
        [MaxLength(20)]
        public string EmployeeNo { get; set; }
        
        [Description("ชื่อ - นามสกุล")]
        [MaxLength(250)]
        public string DisplayName { get; set; }
        
        [Description("สถานะการถือ K-Cash Card")]
        public bool? IsKCashCard { get; set; }
        
    }
}
