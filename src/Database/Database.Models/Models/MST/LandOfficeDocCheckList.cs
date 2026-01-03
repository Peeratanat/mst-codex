using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.MST
{
    [Description("การกำหนด Default Document CheckList สำหรับแต่ละสำนักงานที่ดิน")]
    [Table("LandOfficeDocCheckList", Schema = Schema.MASTER)]
    public class LandOfficeDocCheckList : BaseEntity
    {
        [Description("สำนักงานที่ดิน")]
        public Guid LandOfficeID { get; set; }
        [ForeignKey("LandOfficeID")]
        public LandOffice LandOffice { get; set; }

        [Description("ประเภทของโครงการ (แนวสูง แนวราบ)")]
        public Guid ProductTypeMasterCenterID { get; set; }
        [ForeignKey("ProductTypeMasterCenterID")]
        public MasterCenter ProductType { get; set; }

        [Description("แบบแสดงรายการภาษีธุรกิจเฉพาะ (ภ.ธ.40)")]
        public bool? HR_PT_40 { get; set; }
        [Description("คำขอจดทะเบียนสิทธิและนิติกรรม (ท.ด.1)")]
        public bool? R_TD_01 { get; set; }
        [Description("คำขอจดทะเบียนสิทธิและนิติกรรม (อ.ช.15)")]
        public bool? H_AH_15 { get; set; }
        [Description("บันทึกการประเมินราคาทรัพย์สิน (ท.ด.86)")]
        public bool? HR_TD_86 { get; set; }
        [Description("บันทึกถ้อยคำการชำระภาษีอากร (ท.ด.16)")]
        public bool? HR_TD_16 { get; set; }
        [Description("หนังสือสัญญาขายที่ดิน (ท.ด.13-1)สำนักงานที่ดิน")]
        public bool? R_TD_13_1 { get; set; }
        [Description("หนังสือสัญญาขายที่ดิน (อ.ช.23-1)สำนักงานที่ดิน")]
        public bool? H_AH_23_1 { get; set; }
        [Description("หนังสือสัญญาขายที่ดิน (ท.ด.13-2)ผู้ซื้อ,กรมสรรพากร")]
        public bool? R_TD_13_2 { get; set; }
        [Description("หนังสือสัญญาขายที่ดิน (อ.ช.23-2)ผู้ซื้อ,กรมสรรพากร")]
        public bool? H_AH_23_2 { get; set; }
        [Description("หนังสือสัญญาขายที่ดิน (ท.ด.13-3)ใบเปล่า")]
        public bool? R_TD_13_3 { get; set; }
        [Description("หนังสือสัญญาขายที่ดิน (อ.ช.23-3)ผู้ขาย")]
        public bool? H_AH_23_3 { get; set; }
        [Description("หนังสือสัญญาขายที่ดิน (อ.ช.23-4)ใบเปล่า")]
        public bool? H_AH_23_4 { get; set; }
        [Description("ใบรับค่าใช้จ่าย (A4)")]
        public bool? HR_Expenses_A4 { get; set; }
        [Description("ใบรับค่าใช้จ่าย (Print Dot)")]
        public bool? HR_Recv_Exps_PDOT { get; set; }
        [Description("รายงานการ์ดลูกค้า")]
        public bool? HR_CS_CARD { get; set; }
        [Description("ค่าใช้จ่าย ณ วันโอน (ภาษาไทย)")]
        public bool? HR_Exps_Transfer_TH { get; set; }
        [Description("ค่าใช้จ่าย ณ วันโอน (ภาษาอังกฤษ)")]
        public bool? HR_Exps_Transfer_EN { get; set; }
    }
}
