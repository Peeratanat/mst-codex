using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Database.Models.DbQueries.EXT
{
    public static class sqlChangeUnitAgreementDropdownList
    {
        public static string QueryString = @"
               SELECT 'ProjectID' = prj.ID,
                     'ProjectNo' = prj.ProjectNo ,
                     'ProjectName' = prj.ProjectNameTH,
                     'SumByProject' = COUNT(cuw.ID)
              FROM USR.[User] us
                LEFT JOIN USR.UserRole ur
                    ON ur.UserID = us.ID AND ur.IsDeleted=0
                LEFT JOIN USR.Role r
                    ON r.ID = ur.RoleID AND r.IsDeleted=0
                LEFT JOIN USR.UserAuthorizeProject usp
                    ON usp.UserID = us.ID AND usp.IsDeleted=0
                LEFT JOIN PRJ.Project prj
                    ON prj.ID = usp.ProjectID AND prj.IsDeleted=0
                LEFT JOIN SAL.Agreement agr 
                    ON agr.ProjectID = prj.ID AND agr.IsDeleted = 0 and agr.IsCancel = 0

				LEFT JOIN SAL.ChangeUnitWorkflow cuw 
				ON cuw.ToAgreementID =agr.ID AND cuw.IsDeleted=0 
				LEFT JOIN MST.MasterCenter mc ON mc.ID = agr.AgreementStatusMasterCenterID
              where
				cuw.ID IS NOT NULL 
				AND
                 (r.Code='LCM') 
                AND us.EmployeeNo <> 'TEST_LCM'
				AND cuw.IsRequestApproved IS NULL
				AND cuw.IsDeleted = 0
				AND mc.[Key] ='5'
			#UserId
              group by prj.ID,prj.ProjectNo,prj.ProjectNameTH 
              order by prj.ProjectNo";
        //WHERE bk.ID = '66C96DFF-4747-4F9A-9128-0C14841775FC'";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid UserID)
        {
            var ParamList = new List<SqlParameter>() ?? new List<SqlParameter>();


            ParamList.Add(new SqlParameter($"@UserID", UserID));

            QueryString = QueryString.Replace("#UserId", string.Format(" AND us.id = @UserID"));

            return ParamList;
        }

        //public static List<SqlParameter> QueryFilterByList(ref string QueryString, List<Guid> BookingIDs)
        //{
        //    List<SqlParameter> ParamList = new List<SqlParameter>();

        //    for (var i = 1; i <= BookingIDs.Count; i++)
        //    {
        //        ParamList.Add(new SqlParameter($"@BookingID{i.ToString()}", BookingIDs[i - 1]));
        //    }

        //    QueryString += string.Format(" AND bk.ID IN ({0})", string.Join(",", ParamList.Select(o => o.ParameterName).ToList()));

        //    return ParamList;
        //}

        public class QueryResult
        {
            public Guid? ProjectID { get; set; }
            public string ProjectNo { get; set; }
            public string ProjectName { get; set; }
            public int SumByProject { get; set; }
        }

    }
}


