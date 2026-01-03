using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class UnitMeterListSortByParam
    {
        public UnitMeterListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitMeterListSortBy
    {
        Project,
        Unit_UnitNo,
        Unit_HouseNo,
        Unit_Model_NameTH,
        Unit_UnitStatus,
        TransferOwnerShipDate,
        ElectricMeter,
        WaterMeter,
        CompletedDocumentDate,
        ElectricMeterStatus,
        WaterMeterStatus,
        UpdatedBy,
        Updated
    }
}
