using System;
namespace Base.DTOs.PRJ
{

    public class TitleDeedListSortByParam
    {
        public TitleDeedListSortBy? SortBy { get; set; }  
        public bool Ascending { get; set; }
    }

    public enum TitleDeedListSortBy
    {
        Unit,
        Project, 
        TitledeedNo, 
        Model, 
        LandOffice, 
        TitledeedArea, 
        UsedArea, 
        Address,
        LandStatus,
        LandStatusDate,
        LandPortionNo,
        HouseNo,
        PreferStatus,
        UnitStatus,
        LandNo,
        LandSurveyArea,
        BuildingPermitArea
    }
}
