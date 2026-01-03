using Database.Models;
using Database.Models.DbQueries.SAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.DTOs.CTM
{
	public class VisitorQuestionnaireHistoryDTO
	{
		/// <summary>
		/// โครงการ
		/// </summary>
		public PRJ.ProjectDropdownDTO Project { get; set; }
		/// <summary>
		/// โครงการ
		/// </summary>
		public string ProjectName { get; set; }
		/// <summary>
		/// สถานะทำแบบสอบถาม
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
		/// </summary>
		public MST.MasterCenterDropdownDTO StatusQuestionaire { get; set; }
		/// <summary>
		/// จำนวนคำถามที่ตอบคำถามแล้ว
		/// </summary>
		public int? AnsweredQuestionaire { get; set; }
		/// <summary>
		/// จำนวนคำถามทั้งหมด
		/// </summary>
		public int? TotalQuestionaire { get; set; }
		/// <summary>
		/// วันที่ทำรายการ
		/// </summary>
		public DateTime? QuestionaireDate { get; set; }

		public static List<VisitorQuestionnaireHistoryDTO> CreateModel(ContactDTO contact, DatabaseContext DB, DbQueryContext dbQuery)
		{
			List<VisitorQuestionnaireHistoryDTO> results = new List<VisitorQuestionnaireHistoryDTO>();
			if (!string.IsNullOrEmpty(contact.ContactNo))
			{
				var querylist = dbQuery.dbqCustomerAnswers.FromSqlRaw(" SELECT ProjectCode, ProjectName, CustomerId, AnswerDate, TotalQuestion, Answer FROM DBLINK_SVR_EQN.[APQuestionnaire].dbo.vw_CustomerAnswer WHERE CustomerID = '" + contact.ContactNo + "' ").ToArray();

				foreach (dbqCustomerAnswers ca in querylist)
				{
					VisitorQuestionnaireHistoryDTO result = new VisitorQuestionnaireHistoryDTO();
					result.ProjectName = ca.ProjectCode + " : " + ca.ProjectName;

					int totalQ = querylist.Select(o => o.TotalQuestion).FirstOrDefault();
					int answer = querylist.Select(o => o.Answer).FirstOrDefault();
					if (totalQ > 0)
					{
						result.StatusQuestionaire = totalQ == answer ? MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "2") : MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
					}
					else
					{
						result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
					}
					result.QuestionaireDate = ca.AnswerDate;
					results.Add(result);
				}

			}

			return results;

		}
		public class VisitorQuestionnaireHistoryQueryResult
		{
			public PRJ.ProjectDTO Project { get; set; }
			public PRJ.UnitDTO Unit { get; set; }
			public DateTime? PurchaseDate { get; set; }
			public decimal? NetSellingPrice { get; set; }
			public string UnitStatus { get; set; }

		}
	}
}
