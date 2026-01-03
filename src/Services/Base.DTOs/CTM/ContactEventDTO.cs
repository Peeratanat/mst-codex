using Database.Models;
using Database.Models.DbQueries.EQN;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
	public class ContactEventDTO
	{
		/// <summary>
		///  ID ของ Contact
		/// </summary>
		public Guid? Id { get; set; }
		/// <summary>
		/// รหัสของ Contact
		/// </summary>
		public string ContactNo { get; set; }
		/// <summary>
		/// ประเภท Contact
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactType
		/// </summary>
		[Description("ประเภท Contact")]
		public MST.MasterCenterDropdownDTO ContactType { get; set; }
		/// <summary>
		/// คำนำหน้าชื่อ (ภาษาไทย)
		/// </summary>
		[Description("คำนำหน้าชื่อ (ภาษาไทย)")]
		public MST.MasterCenterDropdownDTO TitleTH { get; set; }
		/// <summary>
		/// คำนำหน้าชื่อเพิ่มเติม
		/// </summary>
		[Description("คำนำหน้าชื่อเพิ่มเติม")]
		public string TitleExtTH { get; set; }
		/// <summary>
		/// ชื่อจริง/ชื่อบริษัท (ภาษาไทย)
		/// </summary>
		[Description("ชื่อจริง/ชื่อบริษัท (ภาษาไทย)")]
		public string FirstNameTH { get; set; }
		/// <summary>
		/// ชื่อกลาง (ภาษาไทย)
		/// </summary>
		[Description("ชื่อกลาง (ภาษาาไทย)")]
		public string MiddleNameTH { get; set; }
		/// <summary>
		/// นามสกุล (ภาษาไทย)
		/// </summary>
		[Description("นามสกุล (ภาษาไทย)")]
		public string LastNameTH { get; set; }
		/// <summary>
		/// ชื่อเล่น (ภาษาไทย)
		/// </summary>
		[Description("ชื่อเล่น")]
		public string Nickname { get; set; }
		/// <summary>
		/// คำนำหน้าชื่อ (ภาษาอังกฤษ)
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactTitleEN
		/// </summary>
		[Description("คำนำหน้าชื่อภาษาอังกฤษ")]
		public MST.MasterCenterDropdownDTO TitleEN { get; set; }
		/// <summary>
		/// คำนำหน้าชื่อเพิ่มเติม (ภาษาอังกฤษ)
		/// </summary>
		[Description("คำนำหน้าชื่อเพิ่มเติม (ภาษาอังกฤษ)")]
		public string TitleExtEN { get; set; }
		/// <summary>
		/// ชื่อจริง (ภาษาอังกฤษ)
		/// </summary>
		[Description("ชื่อจริง/ชื่อบริษัท (ภาษาอังกฤษ)")]
		public string FirstNameEN { get; set; }
		/// <summary>
		/// ชื่อกลาง (ภาษาอังกฤษ)
		/// </summary>
		[Description("ชื่อกลาง (ภาษาอังกฤษ)")]
		public string MiddleNameEN { get; set; }
		/// <summary>
		/// นามสกุล (ภาษาอังกฤษ)
		/// </summary>
		[Description("นามสกุล (ภาษาอังกฤษ)")]
		public string LastNameEN { get; set; }
		/// <summary>
		/// สัญชาติ
		/// </summary>
		[Description("สัญชาติ")]
		public MST.MasterCenterDropdownDTO National { get; set; }
		/// <summary>
		/// เลขที่บัตรประชาชน/Passport
		/// </summary>
		[Description("เลขที่บัตรประชาชน/Passport")]
		public string CitizenIdentityNo { get; set; }
		/// <summary>
		/// วันหมดอายุบัตรประชาชน
		/// </summary>
		[Description("วันหมดอายุบัตรประชาชน")]
		public DateTime? CitizenExpireDate { get; set; }
		/// <summary>
		/// วันเกิด
		/// </summary>
		[Description("วันเกิด")]
		public DateTime? BirthDate { get; set; }
		/// <summary>
		/// เพศ
		/// </summary>
		[Description("เพศ")]
		public MST.MasterCenterDropdownDTO Gender { get; set; }
		/// <summary>
		/// เลขที่ภาษี
		/// </summary>
		[Description("เลขที่ภาษี")]
		public string TaxID { get; set; }
		/// <summary>
		/// เบอร์โทรศัพท์ของบริษัท (กรณีนิติบุคคล)
		/// </summary>
		[Description("เบอร์โทรศัพท์")]
		public string PhoneNumber { get; set; }
		/// <summary>
		/// หมายเลขต่อของบริษัท (กรณีนิติบุคคล)
		/// </summary>
		[Description("เบอร์ต่อ")]
		public string PhoneNumberExt { get; set; }
		/// <summary>
		/// ชื่อจริงของ Contact
		/// </summary>
		[Description("ชื่อจริงผู้ติดต่อ")]
		public string ContactFirstName { get; set; }
		/// <summary>
		/// นางสกุลของ Contact
		/// </summary>
		[Description("นามสกุลผู้ติดต่อ")]
		public string ContactLastname { get; set; }
		/// <summary>
		/// ID Wechat
		/// </summary>
		[Description("Wechat")]
		public string WeChat { get; set; }
		/// <summary>
		/// ID Whatapp
		/// </summary>
		[Description("WhatsApps")]
		public string WhatsApp { get; set; }
		/// <summary>
		/// ID Line
		/// </summary>
		[Description("Line ID")]
		public string LineID { get; set; }
		/// <summary>
		/// ลูกค้า VIP (true = เป็น/false = ไม่เป็น)
		/// </summary>
		public bool? IsVIP { get; set; }
		/// <summary>
		/// ลำดับของ Contact
		/// </summary>
		public int? Order { get; set; }
		/// <summary>
		/// เป็นคนไทยหรือไม่ (true = เป็น/false = ไม่เป็น)
		/// </summary>
		public bool? IsThaiNationality { get; set; }
		/// <summary>
		/// รายการอีเมลของ Contact
		/// </summary>
		[Description("รายการอีเมล")]
		public List<ContactEmailDTO> ContactEmails { get; set; }
		/// <summary>
		/// รายการโทรศัพท์ของ Contact
		/// </summary>
		[Description("รายการโทรศัพท์")]
		public List<ContactPhoneDTO> ContactPhones { get; set; }
		/// <summary>
		/// ID ของ Lead สำหรับการ Qualify กรณีสร้าง Contact ใหม่
		/// </summary>
		public Guid? LeadID { get; set; }

		public Project Project { get; set; }

		/// <summary>
		/// สถานะทำแบบสอบถาม
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=StatusQuestionaire
		/// </summary>
		public MST.MasterCenterDropdownDTO StatusQuestionaire { get; set; }

		public DateTime? VisitDate { get; set; }
		public USR.UserDTO UpdateBy { get; set; }
		public DateTime? UpdateDate { get; set; }
		/// <summary>
		/// ConsentType PPDA
		/// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ConsentType
		/// </summary>
		public MST.MasterCenterDropdownDTO ConsentTypeStatus { get; set; }
		/// <summary>
		/// IsSendToBC
		/// </summary>
		public bool? IsSendToBC { get; set; }

		/// <summary>
		/// ลูกค้า IsVVIP
		/// </summary>
		public bool IsVVIP { get; set; }

		/// <summary>
		/// VVIPExcomApprover
		/// </summary>
		public string VVIPExcomApprover { get; set; }

		public bool? Success { get; set; }
        public string Message { get; set; }


		public string Queue { get; set; }
		public MST.EventDTO Event { get; set; }

        public async static Task<ContactEventDTO> CreateFromModelAsync(models.CTM.Contact model, models.DatabaseContext DB)
		{
			if (model != null)
			{
                ContactEventDTO result = new ContactEventDTO()
				{
					Id = model.ID,
					ContactNo = model.ContactNo,
					TitleExtTH = model.TitleExtTH,
					FirstNameTH = model.FirstNameTH,
					MiddleNameTH = model.MiddleNameTH,
					LastNameTH = model.LastNameTH,
					Nickname = model.Nickname,
					TitleExtEN = model.TitleExtEN,
					FirstNameEN = model.FirstNameEN,
					MiddleNameEN = model.MiddleNameEN,
					LastNameEN = model.LastNameEN,
					CitizenIdentityNo = model.CitizenIdentityNo,
					BirthDate = model.BirthDate,
					TaxID = model.TaxID,
					PhoneNumber = model.PhoneNumber,
					PhoneNumberExt = model.PhoneNumberExt,
					ContactFirstName = model.ContactFirstName,
					ContactLastname = model.ContactLastname,
					WeChat = model.WeChatID,
					WhatsApp = model.WhatsAppID,
					LineID = model.LineID,
					ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
					Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
					TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
					TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
					National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
					CitizenExpireDate = model.CitizenExpireDate,
					IsVIP = model.IsVIP,
					IsThaiNationality = model.IsThaiNationality,
					UpdateBy = USR.UserDTO.CreateFromModel(model.UpdatedBy),
					UpdateDate = model.Updated,
					ConsentTypeStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ConsentType),
                    IsSendToBC = model.IsSendToBC,
					IsVVIP = model.IsVVIP,
					VVIPExcomApprover = model.VVIPExcomApprover
				};

				result.ContactEmails = await DB.ContactEmails
					.Where(e => e.ContactID == model.ID)
					.Select(x => new ContactEmailDTO
					{
						Id = x.ID,
						Email = x.Email,
						IsMain = x.IsMain
					}).OrderByDescending(o => o.IsMain).ToListAsync();

				result.ContactPhones = await DB.ContactPhones
					.Include(o => o.PhoneType)
					.Where(e => e.ContactID == model.ID)
					.Select(o => new ContactPhoneDTO
					{
						Id = o.ID,
						PhoneType = MST.MasterCenterDropdownDTO.CreateFromModel(o.PhoneType),
						PhoneNumber = o.PhoneNumber,
						PhoneNumberExt = o.PhoneNumberExt,
						CountryCode = o.CountryCode,
						IsMain = o.IsMain
					}).OrderByDescending(o => o.IsMain).ToListAsync();

				
				return result;
			}
			else
			{
				return null;
			}
		}
		public async static Task<ContactEventDTO> CreateFormatCitizenFromModelAsync(models.CTM.Contact model, models.DatabaseContext DB)
		{
			if (model != null)
			{
                ContactEventDTO result = new ContactEventDTO()
				{
					Id = model.ID,
					ContactNo = model.ContactNo,
					TitleExtTH = model.TitleExtTH,
					FirstNameTH = model.FirstNameTH,
					MiddleNameTH = model.MiddleNameTH,
					LastNameTH = model.LastNameTH,
					Nickname = model.Nickname,
					TitleExtEN = model.TitleExtEN,
					FirstNameEN = model.FirstNameEN,
					MiddleNameEN = model.MiddleNameEN,
					LastNameEN = model.LastNameEN,
					CitizenIdentityNo = model.IsThaiNationality ? getCitizenIdThaiFormate(model) : model.CitizenIdentityNo,
					BirthDate = model.BirthDate,
					TaxID = model.TaxID,
					PhoneNumber = model.PhoneNumber,
					PhoneNumberExt = model.PhoneNumberExt,
					ContactFirstName = model.ContactFirstName,
					ContactLastname = model.ContactLastname,
					WeChat = model.WeChatID,
					WhatsApp = model.WhatsAppID,
					LineID = model.LineID,
					ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
					Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
					TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
					TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
					National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
					CitizenExpireDate = model.CitizenExpireDate,
					IsVIP = model.IsVIP,
					IsThaiNationality = model.IsThaiNationality
				};

				result.ContactEmails = await DB.ContactEmails
					.Where(e => e.ContactID == model.ID)
					.Select(x => new ContactEmailDTO
					{
						Id = x.ID,
						Email = x.Email,
						IsMain = x.IsMain
					}).ToListAsync();

				result.ContactPhones = await DB.ContactPhones
					.Include(o => o.PhoneType)
					.Where(e => e.ContactID == model.ID)
					.Select(o => new ContactPhoneDTO
					{
						Id = o.ID,
						PhoneType = MST.MasterCenterDropdownDTO.CreateFromModel(o.PhoneType),
						PhoneNumber = o.PhoneNumber,
						PhoneNumberExt = o.PhoneNumberExt,
						CountryCode = o.CountryCode,
						IsMain = o.IsMain
					}).ToListAsync();

				return result;
			}
			else
			{
				return null;
			}
		}
		public static async Task<ContactEventDTO> CreateCustomerAnswerModelAsync(ContactDTO contact, Guid? userID, DatabaseContext DB, DbQueryContext dbQuery)
		{
            ContactEventDTO result = new ContactEventDTO();
			if(!string.IsNullOrEmpty(contact.ContactNo) && !string.IsNullOrEmpty(contact.Project?.ProjectNo))
			{
				//var LcOwnerNo = await DB.Users.Where(o => o.ID == userID).Select(o => o.EmployeeNo).FirstOrDefaultAsync();
				string sqlQuery = sqlCustomerTransQAns.QueryString;
				List<SqlParameter> ParamList = sqlCustomerTransQAns.QueryFilter(ref sqlQuery, contact.ContactNo, contact.Project.ProjectNo);
				//if (ParamList.Count > 0)
				//{
				var SqlCustomerTransQAnsQuery = await dbQuery.sqlCustomerTransQAnss.FromSqlRaw(sqlQuery, ParamList.ToArray()).FirstOrDefaultAsync() ?? new sqlCustomerTransQAns.QueryResult();
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
					var query = dbQuery.dbqCustomerAnswers.FromSqlRaw(" SELECT TotalQuestion, Answer FROM DBLINK_SVR_EQN.[APQuestionnaire].dbo.vw_CustomerAnswer WHERE CustomerID = '" + contact.ContactNo + "' AND  ProjectCode = '" + contact.Project.ProjectNo + "'").ToArray();
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
		public static ContactEventDTO CreateFromModel(models.CTM.Contact model)
		{
			if (model != null)
			{
                ContactEventDTO result = new ContactEventDTO()
				{
					Id = model.ID,
					ContactNo = model.ContactNo,
					TitleExtTH = model.TitleExtTH,
					FirstNameTH = model.FirstNameTH,
					MiddleNameTH = model.MiddleNameTH,
					LastNameTH = model.LastNameTH,
					Nickname = model.Nickname,
					TitleExtEN = model.TitleExtEN,
					FirstNameEN = model.FirstNameEN,
					MiddleNameEN = model.MiddleNameEN,
					LastNameEN = model.LastNameEN,
					CitizenIdentityNo = model.CitizenIdentityNo,
					BirthDate = model.BirthDate,
					TaxID = model.TaxID,
					PhoneNumber = model.PhoneNumber,
					PhoneNumberExt = model.PhoneNumberExt,
					ContactFirstName = model.ContactFirstName,
					ContactLastname = model.ContactLastname,
					WeChat = model.WeChatID,
					WhatsApp = model.WhatsAppID,
					LineID = model.LineID,
					ContactType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactType),
					Gender = MST.MasterCenterDropdownDTO.CreateFromModel(model.Gender),
					TitleEN = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleEN),
					TitleTH = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactTitleTH),
					National = MST.MasterCenterDropdownDTO.CreateFromModel(model.National),
					CitizenExpireDate = model.CitizenExpireDate,
					IsVIP = model.IsVIP,
					IsThaiNationality = model.IsThaiNationality,
					UpdateBy = USR.UserDTO.CreateFromModel(model.UpdatedBy),
					UpdateDate = model.Updated,
					ConsentTypeStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ConsentType),
					IsSendToBC = model.IsSendToBC,
					IsVVIP = model.IsVVIP,
					VVIPExcomApprover = model.VVIPExcomApprover
				};
				return result;
			}
            else
            {
				return null;
            }

		}
		public static string getCitizenIdThaiFormate(models.CTM.Contact model)
		{
			string citizenID = "";

			if (!string.IsNullOrEmpty(model.CitizenIdentityNo))
			{
				var sb = new StringBuilder();
				sb.Append(model.CitizenIdentityNo);
				sb.Insert(1, "-");
				sb.Insert(6, "-");
				sb.Insert(12, "-");
				sb.Insert(15, "-");

				citizenID = sb.ToString();
			}

			return citizenID;
		}
		public async Task ValidateAsync(models.DatabaseContext DB)
		{
			ValidateException ex = new ValidateException();
			if (this.National != null)
			{
				if (string.IsNullOrEmpty(this.CitizenIdentityNo))
				{
					var nationThaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstAsync();
					if (this.National.Id == nationThaiID)
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
						var msg = errMsg.Message;
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
				}
			}
			if (this.ContactEmails.Count > 0)
			{
				var isCheckNull = false;
				if (this.National != null)
				{
					var nationThaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstAsync();
					if (this.National.Id == nationThaiID)
					{
						isCheckNull = false;
					}
					else
					{
						isCheckNull = true;
					}
				}

				var isEmailNull = this.ContactEmails.Any(o => string.IsNullOrEmpty(o.Email));
				if (isEmailNull)
				{
					if (isCheckNull)
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
						string desc = typeof(ContactEmailDTO).GetProperty(nameof(ContactEmailDTO.Email)).GetCustomAttribute<DescriptionAttribute>().Description;
						var msg = errMsg.Message.Replace("[field]", desc);
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
				}
				else
				{
					if (this.National?.Key == NationalKeys.Thai)
					{ }
					else
					{
						var isEmailInvalid = this.ContactEmails.Any(o => o.Email.IsValidEmail() == false);
						if (isEmailInvalid)
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0015").FirstAsync();
							var msg = errMsg.Message;
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}

				
				}

				List<string> emailList = new List<string>();
				foreach (var email in this.ContactEmails)
				{
					if (!string.IsNullOrEmpty(email.Email))
					{
						if (emailList.Contains(email.Email))
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactEmails)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

							break;
						}
						else
						{
							emailList.Add(email.Email);
						}
					}
				}
			}

			if (this.ContactPhones.Count > 0)
			{
				var isPhoneTypeNull = this.ContactPhones.Where(o => o.PhoneType == null).Any();
				if (isPhoneTypeNull)
				{
					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
					string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.PhoneType)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}

				var isPhoneNumberNull = this.ContactPhones.Any(o => string.IsNullOrEmpty(o.PhoneNumber));
				if (isPhoneNumberNull)
				{
					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
					string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
				else
				{
					var isNumberNull = this.ContactPhones.Any(o => !o.PhoneNumber.IsOnlyNumber());
					if (isNumberNull)
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
						string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
						var msg = errMsg.Message.Replace("[field]", desc);
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
				}

				if (!isPhoneTypeNull)
				{
					var foreignID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType" && o.Key == "3").Select(o => o.ID).FirstAsync();
					var foreignList = this.ContactPhones.Where(o => o.PhoneType.Id == foreignID).ToList();

					if (foreignList.Count > 0)
					{
						var isCountryCodeNull = foreignList.Any(o => string.IsNullOrEmpty(o.CountryCode));
						if (isCountryCodeNull)
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
							string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
						else
						{
							var isInvalidCode = foreignList.Any(o => !o.CountryCode.IsOnlyNumberWithSpecialCharacter(false, "+"));
							if (isInvalidCode)
							{
								var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
								string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
								var msg = errMsg.Message.Replace("[field]", desc);
								ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
							}
						}
					}
				}

				List<ContactPhoneList> phoneList = new List<ContactPhoneList>();
				foreach (var phone in this.ContactPhones)
				{
					if (!string.IsNullOrEmpty(phone.PhoneNumber) && (phone.PhoneType != null))
					{
						var isPhoneExits = phoneList.Where(o => o.PhoneNumber == phone.PhoneNumber && o.PhoneTypeMastercenterID == phone.PhoneType.Id).Any();

						if (isPhoneExits)
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactPhones)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

							break;
						}
						else
						{
							phoneList.Add(new ContactPhoneList
							{
								PhoneNumber = phone.PhoneNumber,
								PhoneTypeMastercenterID = phone.PhoneType.Id
							});
						}
					}
				}
			}

			if (!string.IsNullOrEmpty(this.WhatsApp))
			{
				if (!this.WhatsApp.CheckLang(false, true, true, false))
				{
					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ContactDTO.WhatsApp)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.WeChat))
			{
				if (!this.WeChat.CheckLang(false, true, true, false))
				{
					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ContactDTO.WeChat)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}
			if (!string.IsNullOrEmpty(this.LineID))
			{
				if (!this.LineID.CheckLang(false, true, true, false))
				{
					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ContactDTO.LineID)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (this.ContactType == null)
			{
				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
				string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactType)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}
			else
			{
				var legalID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "1").Select(o => o.ID).FirstAsync();
				if (this.ContactType.Id == legalID)
				{
					//if (string.IsNullOrEmpty(this.TaxID))
					//{
					//    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
					//    string desc = this.GetType().GetProperty(nameof(ContactDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
					//    var msg = errMsg.Message.Replace("[field]", desc);
					//    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					//}
					//else if (!this.TaxID.IsOnlyNumber())
					//{
					//    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
					//    string desc = this.GetType().GetProperty(nameof(ContactDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
					//    var msg = errMsg.Message.Replace("[field]", desc);
					//    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					//}

					if (!string.IsNullOrEmpty(this.PhoneNumber))
					{
						if (!this.PhoneNumber.IsOnlyNumber())
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}
					if (!string.IsNullOrEmpty(this.PhoneNumberExt))
					{
						if (!this.PhoneNumberExt.IsOnlyNumber())
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.PhoneNumberExt)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}
					if (!string.IsNullOrEmpty(this.ContactFirstName))
					{
						if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}
					if (!string.IsNullOrEmpty(this.ContactLastname))
					{
						if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}

					if (string.IsNullOrEmpty(this.FirstNameTH))
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
						string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
						var msg = errMsg.Message.Replace("[field]", desc);
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
					//else
					//{
					//    if (!this.FirstNameTH.CheckLang(true, true, true, false))
					//    {
					//        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
					//        string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
					//        var msg = errMsg.Message.Replace("[field]", desc);
					//        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					//    }
					//}

					if (string.IsNullOrEmpty(this.FirstNameEN))
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
						string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
						var msg = errMsg.Message.Replace("[field]", desc);
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
					else
					{
						if (!this.FirstNameEN.CheckLang(false, true, true, false))
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}
				}
				else
				{
					//var isCheckNation = false;
					var isCheckNationThai = false;
					#region Citizen
					var thaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstOrDefaultAsync();
					if (this.National != null)
					{
						if (this.National.Id == thaiID)
						{
							isCheckNationThai = true;
						}
					}
					if (!string.IsNullOrEmpty(this.CitizenIdentityNo))
					{
						if (this.National == null)
						{
							//21052020
							//var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
							//string desc = this.GetType().GetProperty(nameof(ContactDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
							//var msg = errMsg.Message.Replace("[field]", desc);
							//ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

							//isCheckNation = true;
						}
						else
						{
							//var thaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstOrDefaultAsync();
							if (this.National.Id == thaiID)
							{
								if (!this.CitizenIdentityNo.IsOnlyNumber())
								{
									var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
									var msg = errMsg.Message;
									ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
								}
								else if (this.CitizenIdentityNo.Length != 13)
								{
									var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
									var msg = errMsg.Message;
									ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
								}
							}
							else
							{
								if (!this.CitizenIdentityNo.CheckLang(false, true, false, false))
								{
									var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
									string desc = this.GetType().GetProperty(nameof(ContactDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
									var msg = errMsg.Message.Replace("[field]", desc);
									ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
								}
							}
						}
					}
					#endregion

					#region TH
					if (this.TitleTH == null)
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
						string desc = this.GetType().GetProperty(nameof(ContactDTO.TitleTH)).GetCustomAttribute<DescriptionAttribute>().Description;
						var msg = errMsg.Message.Replace("[field]", desc);
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
					else
					{
						var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH" && o.Key == "-1").Select(o => o.ID).FirstAsync();
						if (this.TitleTH.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtTH))
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.TitleExtTH)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}

					if (string.IsNullOrEmpty(this.FirstNameTH))
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
						string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
						var msg = errMsg.Message.Replace("[field]", desc);
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
					//else
					//{	
					//	if (isCheckNationThai)
					//	{
					//		if (!this.FirstNameTH.CheckAllLang(false, false, false, null, " "))
					//		{
					//			var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
					//			string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
					//			var msg = errMsg.Message.Replace("[field]", desc);
					//			ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					//		}
					//	}
					//}

					if (string.IsNullOrEmpty(this.LastNameTH))
					{
						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
						string desc = this.GetType().GetProperty(nameof(ContactDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
						var msg = errMsg.Message.Replace("[field]", desc);
						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					}
					//else
					//{
					//	if (isCheckNationThai)
					//	{
					//		if (!this.LastNameTH.CheckAllLang(false, false, false, null, " "))
					//		{
					//			var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
					//			string desc = this.GetType().GetProperty(nameof(ContactDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
					//			var msg = errMsg.Message.Replace("[field]", desc);
					//			ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					//		}
					//	}
					//}

					if (!string.IsNullOrEmpty(this.MiddleNameTH))
					{
						//if (isCheckNationThai)
						//{
						//	if (!this.MiddleNameTH.CheckAllLang(false, false, false, null, " "))
						//	{
						//		var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
						//		string desc = this.GetType().GetProperty(nameof(ContactDTO.MiddleNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
						//		var msg = errMsg.Message.Replace("[field]", desc);
						//		ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						//	}
						//}
					}

					if (!string.IsNullOrEmpty(this.Nickname))
					{
						if (isCheckNationThai)
						{
							if (!this.Nickname.CheckAllLang(false, false, false, null, " "))
							{
								var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
								string desc = this.GetType().GetProperty(nameof(ContactDTO.Nickname)).GetCustomAttribute<DescriptionAttribute>().Description;
								var msg = errMsg.Message.Replace("[field]", desc);
								ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
							}
						}
					}
					#endregion

					#region EN
					if (this.TitleEN != null)
					{
						var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).FirstAsync();
						if (this.TitleEN.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtEN))
						{
							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
							string desc = this.GetType().GetProperty(nameof(ContactDTO.TitleExtEN)).GetCustomAttribute<DescriptionAttribute>().Description;
							var msg = errMsg.Message.Replace("[field]", desc);
							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
						}
					}

					if (!string.IsNullOrEmpty(this.FirstNameEN))
					{
						if (isCheckNationThai)
						{
							if (!this.FirstNameEN.CheckLang(false, true, true, false, null, " "))
							{
								var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
								string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
								var msg = errMsg.Message.Replace("[field]", desc);
								ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
							}
						}
					}

					if (!string.IsNullOrEmpty(this.LastNameEN))
					{
						if (isCheckNationThai)
						{
							if (!this.LastNameEN.CheckLang(false, true, true, false, null, " "))
							{
								var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
								string desc = this.GetType().GetProperty(nameof(ContactDTO.LastNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
								var msg = errMsg.Message.Replace("[field]", desc);
								ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
							}
						}
					}

					if (!string.IsNullOrEmpty(this.MiddleNameEN))
					{
						if (isCheckNationThai)
						{
							if (!this.MiddleNameEN.CheckLang(false, true, true, false, null, " "))
							{
								var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
								string desc = this.GetType().GetProperty(nameof(ContactDTO.MiddleNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
								var msg = errMsg.Message.Replace("[field]", desc);
								ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
							}
						}
					}
					#endregion

					//if (!isCheckNation) //21022020
					//{
					//    if (this.National == null)
					//    {
					//        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
					//        string desc = this.GetType().GetProperty(nameof(ContactDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
					//        var msg = errMsg.Message.Replace("[field]", desc);
					//        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					//    }
					//}

					//if (this.Gender == null) //21022020
					//{
					//    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
					//    string desc = this.GetType().GetProperty(nameof(ContactDTO.Gender)).GetCustomAttribute<DescriptionAttribute>().Description;
					//    var msg = errMsg.Message.Replace("[field]", desc);
					//    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
					//}
				}
			}

			if (ex.HasError)
			{
				throw ex;
			}
		}

		public void ToModel(ref models.CTM.Contact model)
		{
			model.TitleExtTH = this.TitleExtTH;
			model.FirstNameTH = this.FirstNameTH;
			model.MiddleNameTH = this.MiddleNameTH;
			model.LastNameTH = this.LastNameTH;
			if (this.TitleTH != null)
			{
				if (this.TitleTH.Key.Equals("-1"))
				{
					model.FullnameTH = this.TitleExtTH + this.FirstNameTH + " " + this.LastNameTH;
				}
				else
				{
					model.FullnameTH = this.TitleTH.FullName + this.FirstNameTH + " " + this.LastNameTH;
				}
			}
			model.Nickname = this.Nickname;
			model.TitleExtEN = this.TitleExtEN;
			model.FirstNameEN = this.FirstNameEN;

			if (this.TitleEN != null)
			{
				if (this.TitleEN.Key.Equals("-1"))
				{
					model.FullnameEN = this.TitleExtEN + this.FirstNameEN + " " + this.LastNameEN;
				}
				else
				{
					model.FullnameEN = this.TitleEN.FullName + this.FirstNameEN + " " + this.LastNameEN;
				}
			}

			model.MiddleNameEN = this.MiddleNameEN;
			model.LastNameEN = this.LastNameEN;
			model.CitizenIdentityNo = this.CitizenIdentityNo;
			model.BirthDate = this.BirthDate;
			model.TaxID = this.TaxID;
			model.PhoneNumber = this.PhoneNumber;
			model.PhoneNumberExt = this.PhoneNumberExt;
			model.ContactFirstName = this.ContactFirstName;
			model.ContactLastname = this.ContactLastname;
			model.WeChatID = this.WeChat;
			model.WhatsAppID = this.WhatsApp;
			model.LineID = this.LineID;
			model.ContactTypeMasterCenterID = this.ContactType?.Id;
			model.GenderMasterCenterID = this.Gender?.Id;
			model.ContactTitleTHMasterCenterID = this.TitleTH?.Id;
			model.ContactTitleENMasterCenterID = this.TitleEN?.Id;
			model.NationalMasterCenterID = this.National?.Id;
			model.CitizenExpireDate = this.CitizenExpireDate;

			model.IsVVIP = this.IsVVIP;
			model.VVIPExcomApprover = this.VVIPExcomApprover;

		}

		private class ContactPhoneList
		{
			public string PhoneNumber { get; set; }
			public Guid? PhoneTypeMastercenterID { get; set; }
		}
	}
}
