using System;
using System.Collections.Generic;

namespace Database.Models.DbQueries.PRJ
{
    public static class sql_ProjectInformation
    {
        public static string Query = @"
        SELECT * 
		FROM (
			SELECT prj.ID
				, prj.ProjectNo 
				, prj.ProjectNameTH
				, pif.ThumbnailURL
				, pif.WebURL
				, pif.ProjectType
				, pif.SellingPriceDisplay
				, pif.SellingPriceMin
				, pif.SellingPriceMax
				, psts.Name AS ProjectStatus
				, prj.ProjectStartDate
				, STUFF(
						 (SELECT '-' + plo.Description
						  FROM PRJ.ProjectInfoLocation plo
						  WHERE plo.ProjectID = prj.ID AND plo.LocationType = 'normal'
						  FOR XML PATH ('')
					  ), 1, 1, '') ProjectLocation
				, pif.MapImageUrl
				, ISNULL(ISNULL(CONVERT(VARCHAR(40),CAST(prj.Latitude AS decimal(18,7))), pif.Lat), '13.73026')  Lat   
				, ISNULL(ISNULL(CONVERT(VARCHAR(40),CAST(prj.Longitude AS decimal(18,7))), pif.Lng), '100.56016') Lng  
				, pin.Name AS PinImageURL
				, pif.BrandName
				, pif.UnitTypeDescription
				, pif.AddressDisplay
				, pif.TotalUnit
				, pif.TotalParking
				, pif.UnitTypeDisplay
				, pif.ShortDescription
				, pif.AdminDescription			
				, bg.BGNo
				, prj.ProjectStatusMasterCenterID AS ProjectStatusID
				, psts.[Key] AS ProjectStatusKey
				, prj.BrandID
				, prj.PhoneNumber
				, prj.IsActive
				, prj.LasttUnitTransferDate
			FROM PRJ.Project prj
			LEFT JOIN PRJ.ProjectInfo pif ON pif.ProjectID = prj.ID
			LEFT JOIN MST.BG bg ON bg.ID = prj.BGID
			LEFT JOIN MST.MasterCenter pin ON pin.MasterCenterGroupKey = 'ProjectInfoMapPinImageURL' AND pin.[Key] = (CASE WHEN bg.BGNo = '1' THEN 'SDH' WHEN bg.BGNo IN ('2', '5') THEN 'TH' WHEN bg.BGNo IN ('3', '4') THEN 'CD' END)
			LEFT JOIN MST.MasterCenter psts ON psts.ID = prj.ProjectStatusMasterCenterID
			WHERE prj.IsDeleted = 0 AND ISNULL(pif.IsDeleted, 0) = 0 AND prj.ProjectNameTH NOT LIKE '%ระงับ%' 
		) AS prj
		WHERE 1=1 ";

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
			public string BGNo { get; set; }
            public string PhoneNumber { get; set; }
        }

		public class PageResult
		{
			public List<Result> DataResult { get; set; }

			public int Page { get; set; }
			public int PageSize { get; set; }
			public int PageCount { get; set; }
			public int RecordCount { get; set; }
		}
	}
}
