using System;
namespace Base.DTOs.PRJ
{
    public class UnitListSortByParam
    {
        public UnitListSortBy? SortBy { get; set; }
        public bool Ascending { get; set; }
    }

    public enum UnitListSortBy
    {
        UnitNo,
        HouseNo,
        Model_Code,
        Model_TypeOfRealEstate,
        Model_NameTH,
        Tower,
        Floor,
        UnitDirection,
        UnitStatus,
        UnitType,
        SaleArea,
        TitleDeed_TitleDeedNo,
        TitleDeed_TitleDeedArea,
        NumberOfPrivilege,
        NumberOfParkingFix,
        NumberOfParkingUnFix

    }
}
