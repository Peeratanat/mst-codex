using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.SAL
{
    [Description("ไฟล์แนบMemo การยกเลิกใบจองหรือสัญญา")]
    [Table("CancelMemoFile", Schema = Schema.SALE)]
    public class CancelMemoFile : BaseEntity
    {
        [Description("Memo การยกเลิก")]
        public Guid? CancelMemoID { get; set; }
        public CancelMemo CancelMemo { get; set; }


        [Description("ชื่อ")]
        [MaxLength(1000)]
        public string Name { get; set; }


        [Description("ไฟล์แนบ")]
        [MaxLength(1000)]
        public string File { get; set; }

        //BankRejectDocument
        //ReturnBookBankFile
        [Description("ประเภทไฟล์แนบ 1=หลักฐานการกู้เงินไม่ผ่าน/2=สำเนา Book Bank")]
        public int FileType { get; set; }
    }
}
