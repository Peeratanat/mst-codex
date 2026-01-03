using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Database.Models.DbQueries.Finance
{
    public static class sqlGetActiveAgreementOwner
    {
        public static string QueryString = @"
        SELECT DISTINCT ago.ID AS AgreementOwnerID
        FROM SAL.Agreement ag WITH (NOLOCK)
        LEFT JOIN SAL.AgreementOwner ago WITH (NOLOCK) ON ago.AgreementID = ag.ID AND ago.IsDeleted = 0
        LEFT JOIN SAL.ChangeAgreementOwnerWorkflowDetail changeOwnerDetail  WITH (NOLOCK) ON changeOwnerDetail.AgreementOwnerID = ago.ID AND changeOwnerDetail.IsDeleted = 0
        LEFT JOIN SAL.ChangeAgreementOwnerWorkflow changeOwner  WITH (NOLOCK) ON changeOwner.ID = changeOwnerDetail.ChangeAgreementOwnerWorkflowID AND changeOwner.IsDeleted = 0
        LEFT JOIN MST.MasterCenter mstChangeOwnerType  WITH (NOLOCK) ON mstChangeOwnerType.ID = changeOwner.ChangeAgreementOwnerTypeMasterCenterID
        WHERE ag.IsDeleted = 0 
	        AND ag.IsCancel = 0
	        AND ag.ID IS NOT NULL
	        AND (changeOwnerDetail.ID IS NULL OR ISNULL(changeOwner.IsApproved, 0) = (CASE WHEN changeOwnerDetail.ChangeAgreementOwnerInType = 1 THEN 1 ELSE 0 END) OR mstChangeOwnerType.[Key] = '4') ";

        public static List<SqlParameter> QueryFilter(ref string QueryString, Guid AgreementID)
        {
            List<SqlParameter> ParamList = new List<SqlParameter>();

            if (AgreementID != Guid.Empty)
            {                
                ParamList.Add(new SqlParameter("@prmAgreementID", AgreementID));
                QueryString += " AND ago.AgreementID = @prmAgreementID";
            }

            return ParamList;
        }

        public class QueryResult
        {
            public Guid? AgreementOwnerID { get; set; }
        }
    }
}


