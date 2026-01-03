using System;
using Database.Models.PRM;

namespace MST_Promotion.Params.Filters
{
    public class MasterPreSalePromotionListFilter : BaseFilter
    {
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        /// <value>The promotion no.</value>
        public string PromotionNo { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public Guid? ProjectID { get; set; }
        /// <summary>
        /// ชื่อโปรโมชั่น
        /// </summary>
        /// <value>The name th.</value>
        public string Name { get; set; }
        /// <summary>
        /// รหัสบริษัท
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// สถานะการนำไปใช้งาน
        /// </summary>
        public bool? IsUsed { get; set; }
        /// <summary>
        /// อนุมัติแล้ว
        /// </summary>
        public bool? IsApproved { get; set; }
        /// <summary>
        /// วันที่อนุมัติ จาก
        /// </summary>
        public DateTime? ApprovedDateFrom { get; set; }
        /// <summary>
        /// วันที่อนุมัติ ถึง
        /// </summary>
        public DateTime? ApprovedDateTo { get; set; }
        ///// <summary>
        ///// สถานะ
        ///// </summary>
        public string PromotionStatusKey { get; set; }
    }
}
