using System;
using Base.DTOs.MST;
using Base.DTOs.PRJ;

namespace MST_Titledeeds.Params.Inputs
{
    public class UpdateMultipleHouseNoReceivedYearParam
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
        /// ปีที่ได้บ้านเลขที่
        /// </summary>
        public int HouseNoReceivedYear { get; set; }
    }
}
