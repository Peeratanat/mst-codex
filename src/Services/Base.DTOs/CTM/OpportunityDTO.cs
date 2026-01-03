using Database.Models;
using Database.Models.DbQueries.EQN;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
	public class OpportunityDTO
	{
		/// <summary>
		/// ID ของ Opportunity
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// ข้อมูล Contact
		/// </summary>
		public ContactListDTO Contact { get; set; }
		/// <summary>
		/// โครงการ
		/// project/api/Projects/DropdownList
		/// </summary>
		[Description("โครงการ")]
		public PRJ.ProjectDTO Project { get; set; }
		/// <summary>
		/// ประเมินโอกาสการขาย
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=EstimateSalesOpportunity
		/// </summary>
		[Description("ประเมินโอกาสการขาย")]
		public MST.MasterCenterDropdownDTO EstimateSalesOpportunity { get; set; }
		/// <summary>
		/// โอกาสการขาย
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=SalesOpportunity
		/// </summary>
		public MST.MasterCenterDropdownDTO SalesOpportunity { get; set; }
		/// <summary>
		/// สินค้าที่สนใจ 1
		/// </summary>
		public string InterestedProduct1 { get; set; }
		/// <summary>
		/// สินค้าที่สนใจ 2
		/// </summary>
		public string InterestedProduct2 { get; set; }
		/// <summary>
		/// สินค้าที่สนใจ 3
		/// </summary>
		public string InterestedProduct3 { get; set; }
		/// <summary>
		/// สถานะทำแบบสอบถาม
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
		/// </summary>
		public MST.MasterCenterDropdownDTO StatusQuestionaire { get; set; }
		/// <summary>
		/// จำนวนแปลงที่สนใจ
		/// </summary>
		public int ProductQTY { get; set; }
		/// <summary>
		/// โครงการเปรียบเทียบ
		/// </summary>
		public string ProjectCompare { get; set; }
		/// <summary>
		/// Remark
		/// </summary>
		public string Remark { get; set; }
		/// <summary>
		/// เหตุผลที่ซื้อ
		/// </summary>
		public string BuyReason { get; set; }
		/// <summary>
		/// วันที่เยี่ยมชม
		/// </summary>
		[Description("วันที่เยี่ยมชม")]
		public DateTime? ArriveDate { get; set; }
		/// <summary>
		/// จำนวนคำถามที่ตอบคำถามแล้ว
		/// </summary>
		public int? AnsweredQuestionaire { get; set; }
		/// <summary>
		/// จำนวนคำถามทั้งหมด
		/// </summary>
		public int? TotalQuestionaire { get; set; }
		/// <summary>
		/// ผู้ดูแล
		/// </summary>
		public USR.UserListDTO Owner { get; set; }
		/// <summary>
		/// ConsentType PPDA
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ConsentType
		/// </summary>
		public MST.MasterCenterDropdownDTO ConsentTypeStatus { get; set; }
		/// <summary>
		/// IsSendToBC
		/// </summary>
		public bool? IsSendToBC { get; set; }
		//public static OpportunityDTO CreateFromModel(models.CTM.Opportunity model, models.DatabaseContext DB)
		//{
		//	if (model != null)
		//	{
		//		OpportunityDTO result = new OpportunityDTO()
		//		{
		//			Id = model.ID,
		//			EstimateSalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.EstimateSalesOpportunity),
		//			SalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.SalesOpportunity),
		//			InterestedProduct1 = model.InterestedProduct1,
		//			InterestedProduct2 = model.InterestedProduct2,
		//			InterestedProduct3 = model.InterestedProduct3,
		//			StatusQuestionaire = model.StatusQuestionaire != null ? MST.MasterCenterDropdownDTO.CreateFromModel(model.StatusQuestionaire) :
		//																	MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseEmptyFromModel(DB),
		//			ProductQTY = model.ProductQTY,
		//			ProjectCompare = model.ProjectCompare,
		//			Remark = model.Remark,
		//			BuyReason = model.BuyReason,
		//			ArriveDate = model.ArriveDate,
		//			Owner = USR.UserListDTO.CreateFromModel(model.Owner),
		//		};

		//		if (model.Contact != null)
		//			result.Contact = ContactListDTO.CreateFromModel(model.Contact, DB);
		//		if (model.Project != null)
		//			result.Project = PRJ.ProjectDTO.CreateFromModel(model.Project);

		//		return result;
		//	}
		//	else
		//	{
		//		return null;
		//	}
		//}
		public static OpportunityDTO CreateFromModel(models.CTM.Opportunity model, DatabaseContext DB, DbQueryContext dbQuery)
		{
			if (model != null)
			{
				OpportunityDTO result = new OpportunityDTO()
				{
					Id = model.ID,
					EstimateSalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.EstimateSalesOpportunity),
					SalesOpportunity = MST.MasterCenterDropdownDTO.CreateFromModel(model.SalesOpportunity),
					StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateFromModel(model.StatusQuestionaire),
					InterestedProduct1 = model.InterestedProduct1,
					InterestedProduct2 = model.InterestedProduct2,
					InterestedProduct3 = model.InterestedProduct3,
					ProductQTY = model.ProductQTY,
					ProjectCompare = model.ProjectCompare,
					Remark = model.Remark,
					BuyReason = model.BuyReason,
					ArriveDate = model.ArriveDate,
					Owner = USR.UserListDTO.CreateFromModel(model.Owner),
					ConsentTypeStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ConsentType),
					IsSendToBC = model.IsSendToBC
				};

				if (model.Contact != null)
				{
					result.Contact = ContactListDTO.CreateFromModel(model.Contact, DB);
				}
				if (model.Project != null)
				{
					result.Project = PRJ.ProjectDTO.CreateFromModel(model.Project);
				}

				//if (model.Contact != null && model.Project != null)
				//{
				//	var query = dbQuery.dbqCustomerAnswers.FromSql(" SELECT ProjectCode, ProjectName, CustomerId, AnswerDate, TotalQuestion, Answer FROM DBLINK_SVR_EQN.[APQuestionnaire].dbo.vw_CustomerAnswer WHERE CustomerID = '" + model.Contact.ContactNo + "' AND  ProjectCode = '" + model.Project.ProjectNo + "'").ToArray();
				//	//var query = dbQuery.dbqCustomerAnswers.FromSql(" SELECT TotalQuestion, Answer FROM DBLINK_SVR_QN_TEST.[APQuestionnaire].dbo.vw_CustomerAnswer WHERE CustomerID = '90143992' AND  ProjectCode = '10104'").ToArray(); //ไม่ครบ
				//	//var query = dbQuery.dbqCustomerAnswers.FromSql(" SELECT TotalQuestion, Answer FROM DBLINK_SVR_QN_TEST.[APQuestionnaire].dbo.vw_CustomerAnswer WHERE CustomerID = '90109948' AND  ProjectCode = '10092'").ToArray(); //ครบ
				//	var querylist = query.ToList();
				//	int totalQ = querylist.Select(o => o.TotalQuestion).FirstOrDefault();
				//	int answer = querylist.Select(o => o.Answer).FirstOrDefault();

				//	if (totalQ > 0)
				//	{
				//		result.StatusQuestionaire = totalQ == answer ? MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "2") : MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
				//	}
				//	else
				//	{
				//		result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
				//	}
				//}
				//else
				//{
				//	result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
				//}

				return result;
			}
			else
			{
				return null;
			}
		}

		public async Task ValidateAsync(models.DatabaseContext DB)
		{
			ValidateException ex = new ValidateException();
			if (this.Project == null)
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
				string desc = this.GetType().GetProperty(nameof(OpportunityDTO.Project)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (this.EstimateSalesOpportunity == null)
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
				string desc = this.GetType().GetProperty(nameof(OpportunityDTO.EstimateSalesOpportunity)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			if (this.ArriveDate == null)
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
				string desc = this.GetType().GetProperty(nameof(OpportunityDTO.ArriveDate)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}

			if (ex.HasError)
			{
				throw ex;
			}
		}

		public static OpportunityDTO CreateCustomerAnswerModelAsync(OpportunityDTO opp, Guid? userID, DatabaseContext DB, DbQueryContext dbQuery)
		{
			OpportunityDTO result = new OpportunityDTO();
			if (!string.IsNullOrEmpty(opp.Contact.ContactNo) && !string.IsNullOrEmpty(opp.Project?.ProjectNo))
			{
				//var LcOwnerNo = await DB.Users.Where(o => o.ID == userID).Select(o => o.EmployeeNo).FirstOrDefaultAsync();
				string sqlQuery = sqlCustomerTransQAns.QueryString;
				List<SqlParameter> ParamList = sqlCustomerTransQAns.QueryFilter(ref sqlQuery, opp.Contact.ContactNo, opp.Project.ProjectNo);
				//if (ParamList.Count > 0)
				//{
				var SqlCustomerTransQAnsQuery =  dbQuery.sqlCustomerTransQAnss.FromSqlRaw(sqlQuery, ParamList.ToArray()).FirstOrDefault() ?? new sqlCustomerTransQAns.QueryResult();
				if (SqlCustomerTransQAnsQuery != null) //New EQN
				{
					int totalQ = SqlCustomerTransQAnsQuery.TotalQuestion != null ? SqlCustomerTransQAnsQuery.TotalQuestion.Value : 0;
					int answer = SqlCustomerTransQAnsQuery.TotalAnswer != null ? SqlCustomerTransQAnsQuery.TotalAnswer.Value : 0;

					if (totalQ > 0)
					{
						//ทำ
						if (totalQ == answer)
						{
							result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "2");
						}
						else if (answer > 0) //ทั้งหมด Y, ทำไปแล้ว x
						{
							//X=ตอบแล้ว, Y=คำถามทั้งหมด
							result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "3");
							//if (result.StatusQuestionaire != null)
							//{
							//	result.StatusQuestionaire.Name = result.StatusQuestionaire.Name.Replace("X", answer.ToString());
							//	result.StatusQuestionaire.Name = result.StatusQuestionaire.Name.Replace("Y", totalQ.ToString());
							//}
						}
						else
						{
							result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
						}
					}
					else
					{
						//ไม่ทำ
						result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
					}
				}
				else //Old EQN
				{
					var query = dbQuery.dbqCustomerAnswers.FromSqlRaw(" SELECT TotalQuestion, Answer FROM DBLINK_SVR_EQN.[APQuestionnaire].dbo.vw_CustomerAnswer WHERE CustomerID = '" + opp.Contact.ContactNo + "' AND  ProjectCode = '" + opp.Project.ProjectNo + "'").ToArray();
					var querylist = query.ToList();
					if (querylist.Count > 0)
					{
						int totalQ = querylist.Select(o => o.TotalQuestion).FirstOrDefault();
						int answer = querylist.Select(o => o.Answer).FirstOrDefault();

						if (totalQ > 0)
						{
							//ทำ
							if (totalQ == answer)
							{
								result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "2");
							}
							else if (answer > 0) //ทั้งหมด Y, ทำไปแล้ว x
							{
								//X=ตอบแล้ว, Y=คำถามทั้งหมด
								result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "3");
								//if (result.StatusQuestionaire != null)
								//{
								//	result.StatusQuestionaire.Name = result.StatusQuestionaire.Name.Replace("X", answer.ToString());
								//	result.StatusQuestionaire.Name = result.StatusQuestionaire.Name.Replace("Y", totalQ.ToString());
								//}
							}
							else
							{
								result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
							}
						}
						else
						{
							//ไม่ทำ
							result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
						}
					}
					else
					{
						//ไม่ทำ
						result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
					}
				}

				//}
				//else
				//{
				//	result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1");
				//}
			}
			else
			{
				result.StatusQuestionaire = MST.MasterCenterDropdownDTO.CreateStatusQuestionaireCaseSelectFromDBLink(DB, "1"); //ยังไม่ได้ทำ
			}

			return result;

		}

		public void ToModel(ref models.CTM.Opportunity model)
		{
			model.InterestedProduct1 = this.InterestedProduct1;
			model.InterestedProduct2 = this.InterestedProduct2;
			model.InterestedProduct3 = this.InterestedProduct3;
			model.EstimateSalesOpportunityMasterCenterID = this.EstimateSalesOpportunity?.Id;
			model.SalesOpportunityMasterCenterID = this.SalesOpportunity?.Id;
			model.ArriveDate = this.ArriveDate;
		}
	}
}
