using System;
namespace PRJ_Project.Params.Filters
{
    public class TowerFilter : BaseFilter
    {
        /// <summary>
        /// รหัสอาคาร
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// อาคารเลขที่ (TH)
        /// </summary>
        public string NoTH { get; set; }
        /// <summary>
        /// อาการเลขที่ (EN)
        /// </summary>
        public string NoEN { get; set; }
        /// <summary>
        /// ทะเบียนอาคารชุดเลขที่
        /// </summary>
        public string CondominiumName { get; set; }
        /// <summary>
        /// ชื่ออาคารชุด
        /// </summary>
        public string CondominiumNo { get; set; }
        /// <summary>
        /// คำอธิบาย
        /// </summary>
        public string Description { get; set; }
    }
}
