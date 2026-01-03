using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.RFN
{
	public class sqlDeleteRefundDocref
	{
		public static string QueryString = @"UPDATE RFN.crm_refund_docref 
											 SET isdelete = 1 , modifydate = GETDATE() , modifyby = 'flask_api'
											 WHERE 1=1 ";

		public static List<SqlParameter> QueryFilter(ref string QueryString ,int img_id)
		{
			List<SqlParameter> ParamList = new List<SqlParameter>();
			string imgId = img_id.ToString();
			if (!string.IsNullOrEmpty(imgId))
			{
				ParamList.Add(new SqlParameter("@img_id", imgId));
				QueryString += " AND img_id = @img_id";
			}


			return ParamList;
		}

		public class QueryResult
		{
			public bool IsSuccess { get; set; }
		}
	}
}
