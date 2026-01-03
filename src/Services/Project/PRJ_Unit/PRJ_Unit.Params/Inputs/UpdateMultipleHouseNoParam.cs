using System;
using System.ComponentModel;
using Base.DTOs.PRJ;

namespace PRJ_Unit.Params.Inputs
{
    public class UpdateMultipleHouseNoParam
    {
        /// <summary>
        /// จากเลขที่แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public UnitDropdownDTO FromUnit { get; set; }
        /// <summary>
        /// ถึงเลขที่แปลง
        ///  Project/api/Projects/{projectID}/Units/DropdownList
        /// </summary>
        public UnitDropdownDTO ToUnit { get; set; }
        /// <summary>
        /// ตั้งแต่บ้านเลขที่
        /// หากข้อมูล แปลงที่ และ ถึงแปลงที่ มีมากกว่า 1 แปลง ระบบสามารถรันบ้านเลขที่ให้แต่ละแปลงจนครบ โดยรันเฉพาะเลขหลังเครื่องหมาย / เท่านั้น
        /// </summary>
        [Description("บ้านเลขที่")]
        public string FromHouseNo { get; set; }
        /// <summary>
        /// ปีที่ได้บ้านเลขที่
        /// </summary>
        public int HouseNoReceivedYear { get; set; }
    }
}
