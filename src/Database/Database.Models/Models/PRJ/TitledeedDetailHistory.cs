using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ประวัติข้อมูลโฉนด")]
    [Table("TitledeedDetailHistory", Schema = Schema.PROJECT)]
    public class TitledeedDetailHistory : BaseEntity
    {
        [Description("รหัสโครงการ")]
        public Guid? ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }
        [Description("แปลง")]
        public Guid? UnitID { get; set; }
        [ForeignKey("UnitID")]
        public Unit Unit { get; set; }
        [Description("เลขที่โฉนด")]
        [MaxLength(50)]
        public string TitledeedNo { get; set; }

        [Description("ที่ตั้งโฉนด")]
        public Guid? AddressID { get; set; }
        [ForeignKey("AddressID")]
        public Address Address { get; set; }

        [Description("เลขที่ดิน")]
        [MaxLength(100)]
        public string LandNo { get; set; }
        [Description("เล่ม")]
        [MaxLength(100)]
        public string BookNo { get; set; }
        [Description("หน้า")]
        [MaxLength(100)]
        public string PageNo { get; set; }
        [Description("ราคาประเมิณ")]
        [Column(TypeName = "Money")]
        public decimal? EstimatePrice { get; set; }
        [Description("หมายเหตุ")]
        [MaxLength(5000)]
        public string Remark { get; set; }

        [Description("พื้นที่โฉนด")]
        public double? TitledeedArea { get; set; }
        [Description("หน้าสำรวจ")]
        [MaxLength(100)]
        public string LandSurveyArea { get; set; }
        [Description("เลขระวาง")]
        [MaxLength(50)]
        public string LandPortionNo { get; set; }
        [Description("สถานะโฉนด")]
        public Guid? LandStatusMasterCenterID { get; set; }
        [ForeignKey("LandStatusMasterCenterID")]
        public MST.MasterCenter LandStatusMasterCenter { get; set; }
        [Description("วันที่เปลี่ยนสถานะโฉนด")]
        public DateTime? LandStatusDate { get; set; }
        [Description("หมายเหตุสถานะโฉนด")]
        [MaxLength(1000)]
        public string LandStatusNote { get; set; }
        [Description("สถานะขอปลอด")]
        public Guid? PreferStatusMasterCenterID { get; set; }
        [ForeignKey("PreferStatusMasterCenterID")]
        public MST.MasterCenter PreferStatus { get; set; }

        [Description("ประวัติของโฉนดไหน?")]
        public Guid? TitledeedDetailID { get; set; }
        [ForeignKey("TitledeedDetailID")]
        public TitledeedDetail TitledeedDetail { get; set; }



        public Guid? TitledeedTFVsAcctTypeMasterCenterID { get; set; }


        //public Guid? TitledeedTFVsAcctFlowMasterCenterID { get; set; }



        public Guid? TransferByUserID { get; set; }



        public DateTime? TransferUpdated { get; set; }

        public bool? IsAccountApproved { get; set; }


        public Guid? AccountByUserID { get; set; }


        public DateTime? AccountUpdated { get; set; }


        //public Guid? TitledeedTFVsAcctFlowACMasterCenterID { get; set; }
        //[ForeignKey("TitledeedTFVsAcctFlowACMasterCenterID")]
        //public MST.MasterCenter TitledeedTFVsAcctFlowAC { get; set; }



    }
}
