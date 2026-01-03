using Base.DTOs.MST;
using Database.Models;
using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class PriceListItemDTO : BaseDTO
    {
        /// <summary>
        /// รหัสของราคาโครงการ
        /// </summary>
        public Guid PriceListID { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// ชื่อ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวนหน่วย
        /// </summary>
        public double? PriceUnitAmount { get; set; }
        /// <summary>
        /// หน่วย
        /// </summary>
        public MasterCenterDropdownDTO PriceUnit { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย
        /// </summary>
        public decimal? PricePerUnitAmount { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// ราคาที่ต้องจ่าย
        /// </summary>
        public bool IsToBePay { get; set; }
        /// <summary>
        /// จำนวนงวดถ้ามี
        /// </summary>
        public int? Installment { get; set; }
        /// <summary>
        /// งวดพิเศษ (eg. 1,2,10)
        /// </summary>
        public string SpecialInstallments { get; set; }
        /// <summary>
        /// ราคางวดพิเศษ (eg. 1000.00,2000.00,3000)
        /// </summary>
        public string SpecialInstallmentAmounts { get; set; }
        /// <summary>
        /// ราคาต่องวด
        /// </summary>
        public decimal? InstallmentAmount { get; set; }
        /// <summary>
        /// ItemKey
        /// </summary>
        public MasterPriceItemDTO MasterPriceItem { get; set; }
        /// <summary>
        /// ประเภทของราคา
        /// </summary>
        public MasterCenterDropdownDTO PriceType { get; set; }

        public static PriceListItemDTO CreateFromModel(PriceListItem model)
        {
            if (model != null)
            {
                var result = new PriceListItemDTO()
                {
                    Id = model.ID,
                    PriceListID = model.PriceListID,
                    Order = model.Order,
                    Name = model.Name,
                    PriceUnitAmount = model.PriceUnitAmount,
                    PriceUnit = MasterCenterDropdownDTO.CreateFromModel(model.PriceUnit),
                    PricePerUnitAmount = model.PricePerUnitAmount,
                    InstallmentAmount = model.InstallmentAmount,
                    Amount = model.Amount,
                    IsToBePay = model.IsToBePay,
                    Installment = model.Installment,
                    PriceType = MasterCenterDropdownDTO.CreateFromModel(model.PriceType),
                    MasterPriceItem = MasterPriceItemDTO.CreateFromModel(model.MasterPriceItem),
                    SpecialInstallments = model.SpecialInstallments,
                    SpecialInstallmentAmounts = model.SpecialInstallmentAmounts
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
