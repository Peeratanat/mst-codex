using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.OST
{
    [Description("ประวัติของลูกค้า")]
    [Table("ContactStory", Schema = Schema.OBJECT_STORY)]
    public class ContactStory : BaseEntity
    {
        [Description("ผูกลูกค้า")]
        public Guid ContactID { get; set; }
        [ForeignKey("ContactID")]
        public CTM.Contact Contact { get; set; }

        [Description("เลขที่โครงการ")]
        public string ProjectNo { get; set; }
        [Description("เลขที่แปลง")]
        public string UnitNo { get; set; }

        [Description("ตัวชี้ที่ 1")]
        public string Ref1 { get; set; }
        [Description("ตัวชี้ที่ 1")]
        public string Ref2 { get; set; }
        [Description("ตัวชี้ที่ 1")]
        public string Ref3 { get; set; }
        [Description("ตัวชี้ที่ 1")]
        public string Ref4 { get; set; }

        [Description("ชนิดของประวัติลูกค้า")]
        public Guid ContactStoryTypeID { get; set; }
        [ForeignKey("ContactStoryTypeID")]
        public ContactStoryType ContactStoryType { get; set; }

        [Description("กลุ่มของประวัติลูกค้า")]
        public Guid ContactStoryGroupID { get; set; }
        [ForeignKey("ContactStoryGroupID")]
        public ContactStoryGroup ContactStoryGroup { get; set; }

    }
}
