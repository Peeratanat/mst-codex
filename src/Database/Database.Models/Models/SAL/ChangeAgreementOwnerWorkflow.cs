using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("การอนุมัติเปลี่ยนแปลงชื่อผู้ทำสัญญา")]
    [Table("ChangeAgreementOwnerWorkflow", Schema = Schema.SALE)]
    public class ChangeAgreementOwnerWorkflow : BaseEntity
    {
        //LC ตั้งเรื่อง > LCM อนุมัติตั้งเรื่อง > นิติกรรมอนุมัติ

        //เพิ่มชื่อ
        //นิติกรรมอนุมัติ Sign Contact แล้ว 
        //คำนวนสัดส่วนต่างชาติ ต้องไม่มากกว่า 49%  

        //ลดชื่อ
        //นิติกรรมอนุมัติ Sign Contact แล้ว 
        //ค้างงวดดาวน์หรือไม่

        //โอนสิทธิ์
        //นิติกรรมอนุมัติ Sign Contact แล้ว 
        //คำนวนสัดส่วนต่างชาติ ต้องไม่มากกว่า 49%  
        //ค้างงวดดาวน์หรือไม่

        [Description("เลขที่สํญญา")]
        public Guid? AgreementID { get; set; }
        [ForeignKey("AgreementID")]
        public SAL.Agreement Agreement { get; set; }

        [Description("วันที่นัดทำรายการ")]
        public DateTime? AppointmentDate { get; set; }
        [Description("วันที่นัดโอนใหม่")]
        public DateTime? NewTransferOwnershipDate { get; set; }

        [Description("เบี้ยปรับ/ค่าธรรมเนียม")]
        [Column(TypeName = "Money")]
        public decimal Fee { get; set; }
        [Description("เหตุผลที่ไม่เก็บเบี้ยปรับ/ค่าธรรมเนียม")]
        [MaxLength(5000)]
        public string NoFeeComment { get; set; }

        [Description("รหัสพนักงานตั้งเรื่อง")]
        public Guid? SaleUserID { get; set; }
        [ForeignKey("SaleUserID")]
        public USR.User SaleUser { get; set; }

        [Description("ชนิดของการเปลี่ยนแปลงชื่อผู้ทำสัญญา")]
        public Guid? ChangeAgreementOwnerTypeMasterCenterID { get; set; }
        [ForeignKey("ChangeAgreementOwnerTypeMasterCenterID")]
        public MST.MasterCenter ChangeAgreementOwnerType { get; set; }


        [Description("ตำแหน่งของผู้อนุมัติตั้งเรื่อง")]
        public Guid? RequestApproverRoleID { get; set; }
        [ForeignKey("RequestApproverRoleID")]
        public USR.Role RequestApproverRole { get; set; }
        [Description("ผู้อนุมัติตั้งเรื่อง")]
        public Guid? RequestApproverUserID { get; set; }
        [ForeignKey("RequestApproverUserID")]
        public USR.User RequestApproverUser { get; set; }
        [Description("วันที่อนุมัติตั้งเรื่อง")]
        public DateTime? RequestApprovedDate { get; set; }
        [Description("สถานะอนุมัติตั้งเรื่อง")]
        public bool? IsRequestApproved { get; set; }
        [Description("เหตุผลที่ไม่อนุมัติตั้งเรื่อง")]
        [MaxLength(5000)]
        public string RequestRejectComment { get; set; }


        [Description("ตำแหน่งของผู้อนุมัติ")]
        public Guid? ApproverRoleID { get; set; }
        [ForeignKey("ApproverRoleID")]
        public USR.Role ApproverRole { get; set; }
        [Description("ผู้อนุมัติ")]
        public Guid? ApproverUserID { get; set; }
        [ForeignKey("ApproverUserID")]
        public USR.User ApproverUser { get; set; }
        [Description("วันที่อนุมัติ")]
        public DateTime? ApprovedDate { get; set; }
        [Description("สถานะอนุมัติ")]
        public bool? IsApproved { get; set; }
        [Description("เหตุผลที่ไม่อนุมัติ")]
        [MaxLength(5000)]
        public string RejectComment { get; set; }


        [Description("สถานะอนุมัติพิมพ์สัญญา")]
        public bool? IsPrintApproved { get; set; }
        [Description("วัน-เวลาที่อนุมัติพิมพ์สัญญา")]
        public DateTime? PrintApprovedDate { get; set; }
        [Description("ผู้อนุมัติพิมพ์สัญญา")]
        public Guid? PrintApprovedByUserID { get; set; }
        [ForeignKey("PrintApprovedByUserID")]
        public USR.User PrintApprovedBy { get; set; }

        [Description("สถานะของการเปลี่ยนแปลงชื่อผู้ทำสัญญา")]
        public Guid? ChangeAgreementOwnerStatusMasterCenterID { get; set; }
        [ForeignKey("ChangeAgreementOwnerStatusMasterCenterID")]
        public MST.MasterCenter ChangeAgreementOwnerStatus { get; set; }

        [Description("Agency")]
        public Guid? AgentID { get; set; }
        [ForeignKey("AgentID")]
        public MST.Agent Agent { get; set; }

        [Description("สถานะโอนสิทธิ์ทั้งหมด")]
        public bool? IsTransferAll { get; set; }

    }
}
