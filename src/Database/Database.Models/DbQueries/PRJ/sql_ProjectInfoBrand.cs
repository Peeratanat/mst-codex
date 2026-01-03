using System;

namespace Database.Models.DbQueries.PRJ
{
    public static class sql_ProjectInfoBrand
	{
        public static string Query = @"
							SELECT a.BrandName
					FROM (
					SELECT CASE WHEN ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') IN ('Apitown', 'อภิทาวน์') THEN 'อภิทาวน์' 
					WHEN ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') IN ('City', 'The City') THEN 'The City'
					WHEN ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') IN ('Baan Klang Krung', 'บ้านกลางกรุง') THEN 'Baan Klang Krung'
					ELSE ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') END AS BrandName
					FROM PRJ.Project p
					LEFT JOIN prj.ProjectInfo pi WITH(NOLOCK) ON pi.ProjectID = p.ID
					LEFT JOIN MST.Brand b WITH(NOLOCK) ON p.BrandID = b.ID AND b.IsDeleted = 0
					WHERE p.IsDeleted = 0
					AND p.ProjectNameTH NOT LIKE '%ระงับ%'
					AND p.ProjectNameTH NOT LIKE '%ไม่ใช้%'
					AND p.ProjectNo NOT LIKE '999%'
					AND p.ProjectNo NOT LIKE '11111'
					AND b.Name <> 'MIGRATE'
					GROUP BY CASE WHEN ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') IN ('Apitown', 'อภิทาวน์') THEN 'อภิทาวน์' 
					WHEN ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') IN ('City', 'The City') THEN 'The City'
					WHEN ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') IN ('Baan Klang Krung', 'บ้านกลางกรุง') THEN 'Baan Klang Krung'
					ELSE ISNULL(ISNULL(pi.BrandName, b.Name),'Unknown') END
					) AS a  WHERE 1=1 ";

        public class Result
        {
			public string BrandName { get; set; }
		}

		//public class DataResult
		//{
		//	public List<Result> DataResult { get; set; }
		//}
	}
}
