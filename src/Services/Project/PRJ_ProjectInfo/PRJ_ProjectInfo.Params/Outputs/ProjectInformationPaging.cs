using System;
using System.Collections.Generic;
using Database.Models.DbQueries.PRJ;
using PagingExtensions;

namespace PRJ_ProjectInfo.Params.Outputs
{
    public class ProjectInformationPaging
    {
        public class Filter
        {
            public Guid? ID { get; set; }
            public string Zone { get; set; }
            public string BrandName { get; set; }
            public string ProjectTypeID { get; set; }
            public string UnitType { get; set; }
            public decimal? SellingPriceMin { get; set; }
            public decimal? SellingPriceMax { get; set; }
            public string ProjectStatusKey { get; set; }
            public string SearchText { get; set; }
        }

        public class SortByParam
        {
            public SortBy? SortBy { get; set; }
            public Ascending? Ascending { get; set; } = ProjectInformationPaging.Ascending.ASC;
        }

        public enum SortBy //*** ตั้งชื่อเดียวกับฟิล ใน DB
        {
            ProjectStartDate = 1,
            ProjectLocation = 2,
            BrandName = 3,
            ProjectType = 4
        }

        public enum Ascending //***
        {
            ASC = 1,
            DESC = 2,
        }

        public class Result
        {
            public Guid? ID { get; set; }
            public string ProjectNo { get; set; }
            public string ProjectNameTH { get; set; }
            public string ThumbnailURL { get; set; }
            public string WebURL { get; set; }
            public string ProjectType { get; set; }
            public string SellingPriceDisplay { get; set; }
            public decimal? SellingPriceMin { get; set; }
            public decimal? SellingPriceMax { get; set; }
            public string ProjectStatus { get; set; }
            public DateTime? ProjectStartDate { get; set; }
            public string ProjectLocation { get; set; }
            public string MapImageUrl { get; set; }
            public string Lat { get; set; }
            public string Lng { get; set; }
            public string PinImageURL { get; set; }
            public string BrandName { get; set; }
            public string UnitTypeDescription { get; set; }
            public string AddressDisplay { get; set; }
            public string TotalUnit { get; set; }
            public string TotalParking { get; set; }
            public string UnitTypeDisplay { get; set; }
            public string ShortDescription { get; set; }
            public string AdminDescription { get; set; }
            public string PhoneNumber { get; set; }

            public static Result ToModel(sql_ProjectInformation.Result data)
            {
                if (data != null)
                {
                    var result = new Result();

                    result.ID = data.ID;
                    result.ProjectNo = data.ProjectNo;
                    result.ProjectNameTH = data.ProjectNameTH;
                    result.ThumbnailURL = data.ThumbnailURL;
                    result.WebURL = data.WebURL;
                    result.ProjectType = data.ProjectType;
                    result.SellingPriceDisplay = data.SellingPriceDisplay;
                    result.SellingPriceMin = data.SellingPriceMin;
                    result.SellingPriceMax = data.SellingPriceMax;
                    result.ProjectStatus = data.ProjectStatus;
                    result.ProjectStartDate = data.ProjectStartDate;
                    result.ProjectLocation = data.ProjectLocation;
                    result.MapImageUrl = data.MapImageUrl;
                    result.Lat = data.Lat;
                    result.Lng = data.Lng;
                    result.PinImageURL = data.PinImageURL;
                    result.BrandName = data.BrandName;
                    result.UnitTypeDescription = data.UnitTypeDescription;
                    result.AddressDisplay = data.AddressDisplay;
                    result.TotalUnit = data.TotalUnit;
                    result.TotalParking = data.TotalParking;
                    result.UnitTypeDisplay = data.UnitTypeDisplay;
                    result.ShortDescription = data.ShortDescription;
                    result.AdminDescription = data.AdminDescription;
                    result.PhoneNumber = data.PhoneNumber;

                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public class APIResult
        {
            public List<Result> DataResult { get; set; }
            public PageOutput PageResult { get; set; }
        }

    }
}
