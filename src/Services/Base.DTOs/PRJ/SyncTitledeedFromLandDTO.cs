using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{

    public class SyncTitledeedFromLandDTO
    {
        public List<ProjectSync> Data { get; set; } = [];
        public string RequestID { get; set; }
        public string Message { get; set; }
    }

    public class UnitSync
    {
        public string Wbs { get; set; }
        public string UnitNo { get; set; }
        public string HouseNo { get; set; }
        public string DeedNumber { get; set; }
        public string Portion { get; set; }
        public string LandNumber { get; set; }
        public string PlaceSurvey { get; set; }
        public string BookNumber { get; set; }
        public string PageNumber { get; set; }
        public double? SquareWa { get; set; }
        public string ModelName { get; set; }
        public string FloorNo { get; set; }
        public string HouseNoReceivedYear { get; set; }
        public double? BuildingPermitArea { get; set; }
        public double? UsedArea { get; set; }
        public double? FenceArea { get; set; }
        public double? FenceIronArea { get; set; }
        public double? BalconyArea { get; set; }
        public double? AirArea { get; set; }
        public double? PoolArea { get; set; }
        public double? ParkingArea { get; set; }
        public double? TotalArea { get; set; }
        public decimal? EstimatePrice { get; set; }
        public string Remark { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool? IsMortgage { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ProjectSync
    {
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public string BgNo { get; set; }
        public string ProjectTypeCode { get; set; }
        public string ProjectTypeName { get; set; }
        public string ProfitCenter { get; set; }
        public string ProfitCenterRemark { get; set; }
        public string ProjectWbs { get; set; }
        public string LandCode { get; set; }
        public List<UnitSync> Units { get; set; } = [];
    }

    public class SyncTitledeedFromLandResponse
    {
        public string Message { get; set; }
        public bool? IsSuccess { get; set; }
    }
}


