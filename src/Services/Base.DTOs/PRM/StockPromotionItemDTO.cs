using Database.Models;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRM
{
    /// <summary>
    /// รายการสิ่งของจากระบบสต๊อค
    /// </summary>
    public class StockPromotionItemDTO : BaseDTO
    {
        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public string PromotionName { get; set; }

        /// <summary>
        /// เลขที่ Serial Number
        /// </summary>
        public string SerialNo { get; set; }

        /// <summary>
        /// ชื่อผู้รับผิดชอบ
        /// </summary>
        public string RequestByName { get; set; }

        /// <summary>
        /// แปลง/ห้อง
        /// </summary>
        public string UnitNo { get; set; }

        /// <summary>
        /// Material Item (ของเทียบเท่า)
        /// </summary>
        public string MaterialItem_equal { get; set; }

        /// <summary>
        /// Material Name (ของเทียบเท่า)
        /// </summary>
        public string MaterialName_equal { get; set; }

        /// <summary>
        /// ราคาต่อหน่วย (ของเทียบเท่า)
        /// </summary>
        public string PricePerUnit_equal { get; set; }
    }
}
