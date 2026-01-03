using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.EQN
{
    public static class sqlCustomerTransQAns
    {
        public static string QueryString = @"
                                            SELECT 'ContactNo' = contact_ref_id
                                                 , 'ContactID' = contact_ref_guid
                                                 , 'ProjectNo' = projectid
                                                 , 'DeeplinkUrl' = deeplink_url 
                                                 , 'LcOwner' = lcowner 
                                                 , 'TotalAnswer' = total_answer 
                                                 , 'TotalQuestion' = total_question 
                                            FROM EQN.CustomerTransQAns WHERE 1=1 ";

        public static List<SqlParameter> QueryFilter(ref string QueryString, string ContactNo, string ProjectNo)  //string contact_ref_id, string projectid
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(ContactNo))
            {
                ParamList.Add(new SqlParameter("@prmcontactNo", ContactNo));
                QueryString += " AND contact_ref_id = @prmcontactNo";
            }
            if (!string.IsNullOrEmpty(ProjectNo))
            {
                ParamList.Add(new SqlParameter("@prmprojectNo", ProjectNo));
                QueryString += " AND projectid = @prmprojectNo";
            }
            //if (!string.IsNullOrEmpty(LcOwner))
            //{
            //    ParamList.Add(new SqlParameter("@prmLcOwner", LcOwner));
            //    QueryString += " AND lcowner = @prmLcOwner";
            //}

            return ParamList;
        }

        public class QueryResult
        {  
            public string ContactNo { get; set; }
            public Guid? ContactID { get; set; }
            public string ProjectNo { get; set; }
            public string DeeplinkUrl { get; set; }
            public string LcOwner { get; set; }
            public int? TotalAnswer { get; set; }
            public int? TotalQuestion { get; set; }
        }
    }
}


