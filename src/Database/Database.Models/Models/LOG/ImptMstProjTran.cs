using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.LOG
{
    [Table("ImptMstProjTran", Schema = Schema.LOG)]
    public class ImptMstProjTran : BaseEntity
    {
        [Description("โครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Description("ext. initialunits/generalunits/titledeed/minprice/pricelist/budgetprom/waveqc/receive")]
        [MaxLength(20)]
        public string Import_Type { get; set; }

        [Description("ext. I = Import Success/S = Process Success")]
        [MaxLength(2)]
        public string Import_Status { get; set; }
        
        // Modified by Suchat S. 2020-09-13 for workflow approved import โฉนด
        // การอนุมัติเอาโฉนดเข้า โดยฝ่ายโอนกรรมสิทธิ
        [Description("Flag เพื่อระบุว่ามีการอนุมัติเอาโฉนดเข้าหรือยัง")]
        public bool IsTitleDeedApproved { get; set; }
        [Description("วันที่ยกเลิกใบจอง")]
        public DateTime? TitleDeedApprovedDate { get; set; }
    }
}
