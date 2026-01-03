using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Database.Models.DbQueries.RFN
{
	public class sqlContactRefundUpdate
	{
		public static string QueryString = @" UPDATE RFN.crm_contact_refund SET ";

		public static List<SqlParameter> QueryUpdate(ref Int64 Hyrf_id, String Ac01_appv_flag, String Ac02_appv_flag, String Ac01_appv_by, String Ac02_appv_by,
													DateTime? Ac01_appv_date, DateTime? Ac02_appv_date)
		{
			List<SqlParameter> ParamList = new List<SqlParameter>();

			if (Ac01_appv_flag != null)
			{
				ParamList.Add(new SqlParameter("@Ac01_appv_flag", Ac01_appv_flag));
				QueryString += " ac01_appv_flag = @Ac01_appv_flag, ";
			}

			if (Ac02_appv_flag != null)
			{
				ParamList.Add(new SqlParameter("@Ac02_appv_flag", Ac02_appv_flag));
				QueryString += " ac02_appv_flag = @Ac02_appv_flag, ";
			}

			if (Ac01_appv_by != null)
			{
				ParamList.Add(new SqlParameter("@Ac01_appv_by", Ac01_appv_by));
				QueryString += " ac01_appv_by = @Ac01_appv_by, ";
			}

			if (Ac02_appv_by != null)
			{
				ParamList.Add(new SqlParameter("@Ac02_appv_by", Ac02_appv_by));
				QueryString += " ac02_appv_by = @Ac02_appv_by, ";
			}

			if (Ac01_appv_date != null)
			{
				ParamList.Add(new SqlParameter("@Ac01_appv_date", Ac01_appv_date));
				QueryString += " ac01_appv_date = @ac01_appv_date, ";
			}

			if (Ac02_appv_date != null)
			{
				ParamList.Add(new SqlParameter("@Ac02_appv_date", Ac02_appv_date));
				QueryString += " ac02_appv_date = @Ac02_appv_date ";
			}

			if (Hyrf_id != null)
			{
				ParamList.Add(new SqlParameter("@hyrf_id", Hyrf_id));
				QueryString += " Where hyrf_id = @hyrf_id ";
			}

			return ParamList;
		}

		public class QueryResult
		{
			public bool IsSuccess { get; set; }
		}
	}
}
