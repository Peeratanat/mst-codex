using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.PRJ
{
    [Description("ข้อมูลโฉนด")]
    [Table("TitledeedDetail", Schema = Schema.PROJECT)]
    public class TitledeedDetail : BaseEntity
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
        public string TitledeedNo { get; set; } ="";

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
        public MST.MasterCenter LandStatus { get; set; }
        [Description("วันที่เปลี่ยนสถานะโฉนด")]
        public DateTime? LandStatusDate { get; set; }
        [Description("หมายเหตุสถานะโฉนด")]
        [MaxLength(5000)]
        public string LandStatusNote { get; set; }
        [Description("สถานะขอปลอด")]
        public Guid? PreferStatusMasterCenterID { get; set; }
        [ForeignKey("PreferStatusMasterCenterID")]
        public MST.MasterCenter PreferStatus { get; set; }

        public Guid? TitledeedRequestFlowID { get; set; }
        [ForeignKey("TitledeedRequestFlowID")]
        public SAL.TitledeedRequestFlow TitledeedRequestFlow { get; set; }


        public Guid? TitledeedTFVsAcctTypeMasterCenterID { get; set; }
        [ForeignKey("TitledeedTFVsAcctTypeMasterCenterID")]
        public MST.MasterCenter TitledeedTFVsAcctType { get; set; }


        //public Guid? TitledeedTFVsAcctFlowMasterCenterID { get; set; }
        //[ForeignKey("TitledeedTFVsAcctFlowMasterCenterID")]
        //public MST.MasterCenter TitledeedTFVsAcctFlow { get; set; }


        public Guid? TransferByUserID { get; set; }
        [ForeignKey("TransferByUserID")]
        public USR.User TransferByUser { get; set; }


        public DateTime? TransferUpdated { get; set; }

        public bool? IsAccountApproved { get; set; }


        public Guid? AccountByUserID { get; set; }
        [ForeignKey("AccountByUserID")]
        public USR.User AccountByUser { get; set; }


        public DateTime? AccountUpdated { get; set; }


        //public Guid? TitledeedTFVsAcctFlowACMasterCenterID { get; set; }
        //[ForeignKey("TitledeedTFVsAcctFlowACMasterCenterID")]
        //public MST.MasterCenter TitledeedTFVsAcctFlowAC { get; set; }




        public TitledeedDetailHistory CloneToHistoryItem()
        {
            var model = new TitledeedDetailHistory()
            {
                ProjectID = this.ProjectID,
                UnitID = this.UnitID,
                TitledeedNo = this.TitledeedNo,
                TitledeedArea = this.TitledeedArea,
                AddressID = this.AddressID,
                LandNo = this.LandNo,
                BookNo = this.BookNo,
                PageNo = this.PageNo,
                EstimatePrice = this.EstimatePrice,
                Remark = this.Remark,
                LandSurveyArea = this.LandSurveyArea,
                LandPortionNo = this.LandPortionNo,
                LandStatusMasterCenterID = this.LandStatusMasterCenterID,
                LandStatusDate = this.LandStatusDate,
                LandStatusNote = this.LandStatusNote,
                TitledeedDetailID = this.ID,
                PreferStatusMasterCenterID = this.PreferStatusMasterCenterID
            };
            return model;
        }

    }
}
