using Database.Models.PRM;
using System;
namespace MST_Promotion.Params.Filters
{
    public class MasterTransferPromotionListFilter : BaseFilter
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ส่วนลดวันโอน จาก
        /// </summary>
        public decimal? TransferDiscountFrom { get; set; }
        /// <summary>
        /// ส่วนลดวันโอน ถึง
        /// </summary>
        public decimal? TransferDiscountTo { get; set; }
        /// <summary>
        /// วันที่เริ่ม (จาก)
        /// </summary>
        public DateTime? StartDateFrom { get; set; }
        /// <summary>
        /// วันที่เริ่ม (ถึง)
        /// </summary>
        public DateTime? StartDateTo { get; set; }
        /// <summary>
        /// วันที่สิ้นสุด (จาก)
        /// </summary>
        public DateTime? EndDateFrom { get; set; }
        /// <summary>
        /// วันที่สิ้นสุด (ถึง)
        /// </summary>
        public DateTime? EndDateTo { get; set; }
        ///// <summary>
        ///// สถานะ
        ///// </summary>
        public string PromotionStatusKey { get; set; }
        /// <summary>
        /// สถานะการนำไปใช้งาน
        /// </summary>
        public bool? IsUsed { get; set; }
    }
}
