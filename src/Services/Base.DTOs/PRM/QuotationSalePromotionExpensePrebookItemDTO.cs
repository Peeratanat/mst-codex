using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Database.Models.PRM;
using Database.Models.SAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// โปรก่อนขาย
    /// </summary>
    public class QuotationSalePromotionExpensePrebookItemDTO : BaseDTO
    {
        //public Guid ID { get; set; }

        //public Guid? QuotationID { get; set; }

        //public decimal Amount { get; set; }

        //public decimal SellerAmount { get; set; }

        //public decimal BuyerAmount { get; set; }

        public Guid? QuotationID { get; set; }

        /// <summary>
        /// ผู้รับผิดชอบ คชจ.
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ExpenseReponsibleBy
        /// </summary>
        public MST.MasterCenterDropdownDTO ExpenseReponsibleBy { get; set; }
        /// <summary>
        /// รายการ
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// จำนวน (หน่วย)
        /// </summary>
        public double? PriceUnitAmount { get; set; }
        /// <summary>
        /// หน่วย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=PriceUnit
        /// </summary>
        public MST.MasterCenterDropdownDTO PriceUnit { get; set; }
        /// <summary>
        /// ราคาต่อหน่วย
        /// </summary>
        public decimal? PricePerUnitAmount { get; set; }
        /// <summary>
        /// ราคารวม
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// ลูกค้าจ่าย
        /// </summary>
        public decimal BuyerAmount { get; set; }
        /// <summary>
        /// บริษัทจ่าย
        /// </summary>
        public decimal SellerAmount { get; set; }
        /// <summary>
        /// รายการชำระเงิน
        /// </summary>
        public MST.MasterPriceItemDTO MasterPriceItem { get; set; }
        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }

    }
}