using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
	public class dbqGetPerformanceTargetKpi
    {

		public Guid? Id { get; set; }

		public Guid ProjectID { get; set; }

		public decimal BOOKING_KPI { get; set; }

		public decimal REVENUE_KPI { get; set; }

		public decimal BOOKING_LL { get; set; }

		public decimal REVENUE_LL { get; set; }

		public decimal BOOKING_TOGO { get; set; }

		public decimal REVENUE_TOGO { get; set; }

		public string UpdateBy { get; set; }

		public DateTime? Updated { get; set; }

	}
}
