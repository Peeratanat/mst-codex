using System;
using Base.DTOs.MST;
using Base.DTOs.PRJ;

namespace MST_Titledeeds.Params.Inputs
{
    public class UpdateMultipleLandOfficeParam
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
        /// สำนักงานที่ดิน
        ///  MasterData/api/LandOffices/DropdownList
        /// </summary>
        public LandOfficeListDTO LandOffice { get; set; }
    }
}
