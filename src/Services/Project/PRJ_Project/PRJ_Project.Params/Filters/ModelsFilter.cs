using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRJ_Project.Params.Filters
{
    public class ModelsFilter : BaseFilter
    {
        /// <summary>
        /// รหัสแบบบ้าน
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// ชื่อแบบบ้านภาษาไทย
        /// </summary>
        public string NameTH { get; set; }
        /// <summary>
        /// ชื่อแบบบ้านภาษาอังกฤษ
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// MastercentergroupKey = ModelShortName
        /// ชื่อย่อ
        /// </summary>
        public string ModelShortNameKey { get; set; }
        /// <summary>
        /// MastercentergroupKey = ModelUnitType
        /// ลักษณะบ้าน
        /// </summary>
        public string ModelUnitTypeKey { get; set; }
        /// <summary>
        /// ประเภทบ้าน Indentity
        /// </summary>
        public Guid? TypeOfRealEstateID { get; set; }
        /// <summary>
        /// MastercentergroupKey = ModelType
        /// Type
        /// </summary>
        public string ModelTypeKey { get; set; }
        /// <summary>
        /// ราคามิเตอร์ไฟ จาก
        /// </summary>
        public decimal? ElectricMeterPriceFrom { get; set; }
        /// <summary>
        /// ราคามิเตอร์ไฟ ถึง
        /// </summary>
        public decimal? ElectricMeterPriceTo { get; set; }
        /// <summary>
        /// ราคามิเตอร์น้ำ จาก
        /// </summary>
        public decimal? WaterMeterPriceFrom { get; set; }
        /// <summary>
        /// ราคามิเตอร์น้ำ ถึง
        /// </summary>
        public decimal? WaterMeterPriceTo { get; set; }
    }
}
