using System;
using System.Collections.Generic;

namespace Database.Models.DbQueries.PRJ
{
    public static class sql_ProjectInfoDetail
	{
        public static string Query = @"
        SELECT * 
		FROM (
			SELECT prj.ID
				, prj.ProjectID 
				, prj.AdminDescription
				, prj.IsDeleted
				, prj.Created
				, prj.Updated
                , prj.CreatedByUserID
                , prj.UpdatedByUserID

			FROM PRJ.ProjectInfoDetail prj
			WHERE prj.IsDeleted = 0 
		) AS prj
		WHERE 1=1 ";

        public class Result
        {
			public Guid ID { get; set; }
			public Guid ProjectID { get; set; }
			public string AdminDescription { get; set; }
			public bool IsDeleted { get; set; }
			public DateTime? Created { get; set; }
			public DateTime? Updated { get; set; }
			public Guid? CreatedByUserID { get; set; }
			public Guid? UpdatedByUserID { get; set; }
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
