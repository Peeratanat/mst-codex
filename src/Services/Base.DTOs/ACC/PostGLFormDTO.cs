using Base.DTOs.MST;
using System.ComponentModel;

namespace Base.DTOs.ACC
{
    public class PostGLFormDTO : BaseDTO
    {
        /// <summary>
        /// Company
        /// </summary>
        [Description("Company")]
        public CompanyDropdownDTO Company { get; set; }

        /// <summary>
        /// จำนวนรายการที่ Post
        /// </summary>
        [Description("จำนวนรายการที่ Post")]
        public int SuccessRecord { get; set; }

   //รหัส Post  
          //รหัสยกเลิก
        //วันที่ยกเลิก


//บริษัท
        //โครงการ
        //แปลง


        //เลขที่ใบเสร็จ
        //เลขที่รับเงิน
        //วันที่ใบเสร็จ

        //จำนวนเงิน
        //ชำระโดย
        
        //รหัสนำฝาก
        //Total

        //วันที่จอง
        //ชื่อลูกค้า
        //ข้อมูลเดิม โครงการ
        //ข้อมูลเดิม แปลง
        //ข้อมูลใหม่ โครงการ
        //ข้อมูลใหม่ แปลง
        //รายละเอียด
         
        //Dr บัตรเครดิต
        //Cr เงินรับล่วงหน้าเงินดาวน์

    }
}
