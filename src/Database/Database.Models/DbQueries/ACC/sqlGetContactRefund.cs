using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.ACC
{
	public class sqlGetContactRefund
	{
		public static string QueryString = @"SELECT cr.transfernumber, cr.transferdateapprove, cr.remainingtotalamount, 
													cr.welcomehome_amount, cr.welcomehome_flag,  cp.NameTH, cp.NameEN 
						FROM ACC.PostGLHeader ph 
							LEFT OUTER JOIN acc.PostGLDetail pd ON ph.ID = pd.PostGLHeaderID
							LEFT OUTER JOIN RFN.crm_contact_refund cr ON pd.ProjectNo = cr.productid AND pd.UnitNo = cr.unitnumber
							LEFT OUTER JOIN mst.Company cp ON cr.companyid = cp.Code
						WHERE 1=1 AND cp.IsDeleted = 0 ";

		public static List<SqlParameter> QueryFilter(ref string QueryString, Guid? postGLHeaderID)
		{
			List<SqlParameter> ParamList = new List<SqlParameter>();
			if (postGLHeaderID != null)
			{
				ParamList.Add(new SqlParameter("@postGLHeaderID", postGLHeaderID));
				QueryString += " AND ph.ID = @postGLHeaderID";
			}


			return ParamList;
		}

		public class QueryResult
		{
			public string transfernumber { get; set; }
			public DateTime? transferdateapprove { get; set; }
			public decimal? remainingtotalamount { get; set; }
			public decimal? welcomehome_amount { get; set; }
			public string welcomehome_flag { get; set; }
			public string NameTH { get; set; }
			public string NameEN { get; set; }
		}

		public static string GetURLQueryString = @"SELECT ISNULL(doc_merge_url, '') DocMergeUrl FROM RFN.crm_contact_refund WHERE 1=1  ";


		public static List<SqlParameter> QueryURLFilter(ref string QueryString, string sap_ref_doc)
		{
			List<SqlParameter> ParamList = new List<SqlParameter>();
			if (!string.IsNullOrEmpty(sap_ref_doc))
			{
				ParamList.Add(new SqlParameter("@sap_ref_doc", sap_ref_doc));
				QueryString += " AND sap_ref_doc = @sap_ref_doc";
			}

			return ParamList;
		}

		public class QueryURLResult
		{
			public string DocMergeUrl { get; set; }
		}

	}
}
