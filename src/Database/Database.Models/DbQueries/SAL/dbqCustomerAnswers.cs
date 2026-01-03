using Base.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
	public class dbqCustomerAnswers
	{
		public string ProjectCode { get; set; }
		public string ProjectName { get; set; }
		public string CustomerId { get; set; }
		public DateTime? AnswerDate { get; set; }
		public int TotalQuestion { get; set; }
		public int Answer { get; set; }

	}
}
