using System;

namespace Database.Models.DbQueries.PRJ
{
    public static class sql_ProjectInforLocationDetail
    {
        public static string Query = @"
            SELECT pil.ID
                 , pil.RefID
                 , pil.RefID2
                 , pil.ProjectID
                 , pil.ProjectNo
                 , pil.LanguageType
                 , pil.Description
                 , pil.LocationType
                 , pil.LocationTitle
                 , pil.Created
                 , pil.Updated
                 , pil.CreatedByUserID
                 , pil.UpdatedByUserID
	             , 'IconKey' = c.[Key]
	             , 'IconURL' = c.Name
            FROM PRJ.ProjectInfoLocation pil
            LEFT JOIN MST.MasterCenter c ON c.MasterCenterGroupKey = 'ProjectInfoLocationIconURL' AND c.[Key] = pil.LocationTitle
            WHERE pil.IsDeleted = 0
                AND c.IsDeleted = 0
                AND pil.LocationType = 'highlight'";

        public class Result
        {
            public Guid? ID { get; set; }
            public int? RefID { get; set; }
            public int? RefID2 { get; set; }
            public Guid? ProjectID { get; set; }
            public string ProjectNo { get; set; }
            public string LanguageType { get; set; }
            public string Description { get; set; }
            public string LocationType { get; set; }
            public string LocationTitle { get; set; }
            public DateTime? Created { get; set; }
            public DateTime? Updated { get; set; }
            public Guid? CreatedByUserID { get; set; }
            public Guid? UpdatedByUserID { get; set; }
            public string IconKey { get; set; }
            public string IconURL { get; set; }
        }
    }
}
