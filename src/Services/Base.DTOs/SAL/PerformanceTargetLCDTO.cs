using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Database.Models;
using Base.DTOs.MST;
using Base.DTOs.SAL.Sortings;
using System.Linq;
using Base.DTOs.PRJ;
using Database.Models.DbQueries.SAL;

namespace Base.DTOs.SAL
{
    public class PerformanceTargetLcDTO
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

		public static PerformanceTargetLcDTO CreateFromQuery(dbqGetPerformanceTargetLCs model, DatabaseContext db)
		{
			if (model != null)
			{
				var result = new PerformanceTargetLcDTO();
				result.ID = model.ID;
				result.BG = model.BG;
				result.LCEmpCode = model.LCEmpCode;
				result.LCFullName = model.LCFullName;
				result.LCNickName = model.LCNickName;
				result.ProjectID = model.ProjectID;
				result.ProjectNo = model.ProjectNo;
				result.ProjectNameTH = model.ProjectNameTH;
				result.Trans_Year = model.Trans_Year;
				result.Trans_Quarter = model.Trans_Quarter;
				result.TOTAL_BOOK_ToGO = model.TOTAL_BOOK_ToGO;
				result.TOTAL_REVENUE_ToGO = model.TOTAL_REVENUE_ToGO;
				result.BOOK_W1 = model.BOOK_W1;
				result.BOOK_W2 = model.BOOK_W2;
				result.BOOK_W3 = model.BOOK_W3;
				result.BOOK_W4 = model.BOOK_W4;
				result.BOOK_W5 = model.BOOK_W5;
				result.BOOK_W6 = model.BOOK_W6;
				result.BOOK_W7 = model.BOOK_W7;
				result.BOOK_W8 = model.BOOK_W8;
				result.BOOK_W9 = model.BOOK_W9;
				result.BOOK_W10 = model.BOOK_W10;
				result.BOOK_W11 = model.BOOK_W11;
				result.BOOK_W12 = model.BOOK_W12;
				result.BOOK_W13 = model.BOOK_W13;
				result.BOOK_W14 = model.BOOK_W14;
				result.BOOK_W15 = model.BOOK_W15;
				result.REVENUE_W1 = model.REVENUE_W1;
				result.REVENUE_W2 = model.REVENUE_W2;
				result.REVENUE_W3 = model.REVENUE_W3;
				result.REVENUE_W4 = model.REVENUE_W4;
				result.REVENUE_W5 = model.REVENUE_W5;
				result.REVENUE_W6 = model.REVENUE_W6;
				result.REVENUE_W7 = model.REVENUE_W7;
				result.REVENUE_W8 = model.REVENUE_W8;
				result.REVENUE_W9 = model.REVENUE_W9;
				result.REVENUE_W10 = model.REVENUE_W10;
				result.REVENUE_W11 = model.REVENUE_W11;
				result.REVENUE_W12 = model.REVENUE_W12;
				result.REVENUE_W13 = model.REVENUE_W13;
				result.REVENUE_W14 = model.REVENUE_W14;
				result.REVENUE_W15 = model.REVENUE_W15;
				result.Remark = model.Remark;
				result.IsDeleted = model.IsDeleted;
				result.Created = model.Created;
				result.CreatedBy = model.CreatedBy;
				result.Updated = model.Updated;
				result.UpdatedBy = model.UpdatedBy;
			

				return result;
			}
			else
			{
				return null;
			}
		}


	}
}
