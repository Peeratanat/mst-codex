using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
	public class dbqGetPerformanceTargetLCs
	{
		public Guid? ID { get; set; }
		public string LCEmpCode { get; set; }

		public string LCFullName { get; set; }

		public string LCNickName { get; set; }

		public string BG { get; set; }
		public Guid? ProjectID { get; set; }
		public string ProjectNo { get; set; }
		public string ProjectNameTH { get; set; }
		public int Trans_Year { get; set; }
		public int Trans_Quarter { get; set; }
		public double TOTAL_BOOK_ToGO { get; set; }
		public double TOTAL_REVENUE_ToGO { get; set; }
		public double BOOK_W1 { get; set; }
		public double BOOK_W2 { get; set; }
		public double BOOK_W3 { get; set; }
		public double BOOK_W4 { get; set; }
		public double BOOK_W5 { get; set; }
		public double BOOK_W6 { get; set; }
		public double BOOK_W7 { get; set; }
		public double BOOK_W8 { get; set; }
		public double BOOK_W9 { get; set; }
		public double BOOK_W10 { get; set; }
		public double BOOK_W11 { get; set; }
		public double BOOK_W12 { get; set; }
		public double BOOK_W13 { get; set; }
		public double BOOK_W14 { get; set; }
		public double BOOK_W15 { get; set; }
		public double REVENUE_W1 { get; set; }
		public double REVENUE_W2 { get; set; }
		public double REVENUE_W3 { get; set; }
		public double REVENUE_W4 { get; set; }
		public double REVENUE_W5 { get; set; }
		public double REVENUE_W6 { get; set; }
		public double REVENUE_W7 { get; set; }
		public double REVENUE_W8 { get; set; }
		public double REVENUE_W9 { get; set; }
		public double REVENUE_W10 { get; set; }
		public double REVENUE_W11 { get; set; }
		public double REVENUE_W12 { get; set; }
		public double REVENUE_W13 { get; set; }
		public double REVENUE_W14 { get; set; }
		public double REVENUE_W15 { get; set; }
		public string Remark { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }



	}
}
