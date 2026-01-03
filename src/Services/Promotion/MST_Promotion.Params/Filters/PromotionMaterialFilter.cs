using System;
namespace MST_Promotion.Params.Filters
{
    public class PromotionMaterialFilter : BaseFilter
    {
        /// <summary>
        /// Agreement No.
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// เลขที่สิ่งของ
        /// </summary>
        public string ItemNo { get; set; }
        /// <summary>
        /// Plant
        /// </summary>
        public string Plant { get; set; }
        /// <summary>
        /// ชื่อBrand (ภาษาไทย)
        /// </summary>
        public string BrandTH { get; set; }
        /// <summary>
        /// ชื่อBrand (ภาษาอังกฤษ)
        /// </summary>
        public string BrandEN { get; set; }
        /// <summary>
        /// ชื่อสิ่งของ (ภาษาไทย)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อสิ่งของ (ภาษาอังกฤษ)
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// Material Code
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// ราคา (จาก)
        /// </summary>
        public decimal? PriceFrom { get; set; }
        /// <summary>
        /// ราคา (ถึง)
        /// </summary>
        public decimal? PriceTo { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string Unit { get; set; }
        /// <summary>
        /// วันหมดอายุ (จาก)
        /// </summary>
        public DateTime? ExpireDateFrom { get; set; }
        /// <summary>
        /// วันหมดอายุ (ถึง)
        /// </summary>
        public DateTime? ExpireDateTo { get; set; }
        /// <summary>
        /// ProjectID
        /// </summary>
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        //public string ProjectNo { get; set; }
    }
}
