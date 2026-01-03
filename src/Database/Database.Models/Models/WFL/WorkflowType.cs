using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.WFL
{
    [Description("ชนิดของการอนุมัติ")]
    [Table("WorkflowType", Schema = Schema.WORKFLOW)]
    public class WorkflowType : BaseEntity
    {
        [Description("ชื่อชนิด")]
        public string Name { get; set; }
        //Describe reference key in Workflow
        [Description("อนธิบายความหมายของตัวชี้ที่ 1 ในการอนุมัติชนิดนี้")]
        public string Ref1 { get; set; }
        [Description("อนธิบายความหมายของตัวชี้ที่ 2 ในการอนุมัติชนิดนี้")]
        public string Ref2 { get; set; }
        [Description("อนธิบายความหมายของตัวชี้ที่ 3 ในการอนุมัติชนิดนี้")]
        public string Ref3 { get; set; }
        [Description("อนธิบายความหมายของตัวชี้ที่ 4 ในการอนุมัติชนิดนี้")]
        public string Ref4 { get; set; }
    }
}
