using System;
using Database.Models.PRM;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการโปรก่อนขาย
    /// </summary>
    public class PreSalePromotinItemDTO : BaseDTO
    {
        /// <summary>
        /// ชื่อผลิตภัณฑ์ (TH)
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// จำนวน
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย (บาท)
        /// </summary>
        public decimal PricePerUnit { get; set; }
        /// <summary>
        /// ราคารวม (บาท)
        /// </summary>
        public decimal TotalPrice { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public string UnitTH { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// เลขที่ PR
        /// </summary>
        public string PRNo { get; set; }

        public static PreSalePromotinItemDTO CreateFromRequestItemAsync(PreSalePromotionRequestItem model)
        {
            if (model != null)
            {
                var result = new PreSalePromotinItemDTO()
                {
                    NameTH = model.NameTH,
                    Quantity = model.Quantity,
                    PricePerUnit = model.PricePerUnit,
                    TotalPrice = model.TotalPrice,
                    UnitTH = model.UnitTH,
                    Remark = model.Remark,
                    PRNo = model.PRNo
                };
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
