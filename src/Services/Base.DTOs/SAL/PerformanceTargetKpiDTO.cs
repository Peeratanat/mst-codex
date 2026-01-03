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
    public class PerformanceTargetKpiDTO : BaseDTO
	{
		//pk.ID, pk.ProjectID, pk.BOOKING_KPI( doble), pk.REVENUE_KPI, pk.BOOKING_LL, pk.REVENUE_LL, pk.BOOKING_TOGO, pk.REVENUE_TOGO,
		//CONCAT(ISNULL(u.Title,''), u.DisplayName) UpdateBy, pk.Updated
		/// <summary>
		/// 
		/// </summary>
		public Guid ProjectID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal BOOKING_KPI { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal REVENUE_KPI { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal BOOKING_LL { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal REVENUE_LL { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal BOOKING_TOGO { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public decimal REVENUE_TOGO { get; set; }


		public static PerformanceTargetKpiDTO CreateFromQuery(dbqGetPerformanceTargetKpi model, DatabaseContext db)
		{
			if (model != null)
			{
				var result = new PerformanceTargetKpiDTO();

				result.Id = model.Id;
				result.ProjectID = model.ProjectID;
				result.BOOKING_KPI = model.BOOKING_KPI;
				result.REVENUE_KPI = model.REVENUE_KPI;
				result.BOOKING_LL = model.BOOKING_LL;
				result.REVENUE_LL = model.REVENUE_LL;
				result.BOOKING_TOGO = model.BOOKING_TOGO;
				result.REVENUE_TOGO = model.REVENUE_TOGO;
				result.UpdatedBy = model.UpdateBy;
				result.Updated = model.Updated;
				return result;
			}
			else
			{
				return null;
			}
		}

	}
}
