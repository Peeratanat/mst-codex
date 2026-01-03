using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class ModelListSortByParam
    {
        public ModelListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum ModelListSortBy
    {
        Code,
        NameTH,
        NameEN,
        ModelShortName,
        ModelUnitType,
        TypeOfRealEstate,
        ModelType,
        WaterElectricMeterPrice_WaterMeterPrice,
        WaterElectricMeterPrice_ElectricMeterPrice,
        Updated,
        UpdatedBy
    }
}
