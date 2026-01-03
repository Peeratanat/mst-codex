using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.OST
{
    [Description("ชนิดของประวัติลูกค้า")]
    [Table("ContactStoryType", Schema = Schema.OBJECT_STORY)]
    public class ContactStoryType : BaseEntity
    {
        [Description("ชื่อ")]
        public string Name { get; set; }
        //Describe reference key in Contact Story
        [Description("คำอธิบายตัวชี้ที่ 1 ของชนิดนี้")]
        public string Ref1 { get; set; }
        [Description("คำอธิบายตัวชี้ที่ 2 ของชนิดนี้")]
        public string Ref2 { get; set; }
        [Description("คำอธิบายตัวชี้ที่ 3 ของชนิดนี้")]
        public string Ref3 { get; set; }
        [Description("คำอธิบายตัวชี้ที่ 4 ของชนิดนี้")]
        public string Ref4 { get; set; }

    }
}
