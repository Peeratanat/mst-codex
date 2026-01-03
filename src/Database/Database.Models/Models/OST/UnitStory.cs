using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.OST
{
    [Description("ประวัติแปลง")]
    [Table("UnitStory", Schema = Schema.OBJECT_STORY)]
    public class UnitStory : BaseEntity
    {
        [Description("ผูกแปลง")]
        public Guid UnitID { get; set; }
        [ForeignKey("UnitID")]
        public PRJ.Unit Unit { get; set; }

        [Description("ตัวชี้ที่ 1")]
        public string Ref1 { get; set; }
        [Description("ตัวชี้ที่ 2")]
        public string Ref2 { get; set; }
        [Description("ตัวชี้ที่ 3")]
        public string Ref3 { get; set; }
        [Description("ตัวชี้ที่ 4")]
        public string Ref4 { get; set; }

        [Description("ชนิดของประวัติแปลง")]
        public Guid UnitStoryTypeID { get; set; }
        [ForeignKey("UnitStoryTypeID")]
        public UnitStoryType UnitStoryType { get; set; }

        [Description("กลุ่มของประวัติแปลง")]
        public Guid UnitStoryGroupID { get; set; }
        [ForeignKey("UnitStoryGroupID")]
        public UnitStoryGroup UnitStoryGroup { get; set; }


    }
}
