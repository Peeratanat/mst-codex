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
	public class CustomerDTO
	{
		/// <summary>
		/// ชื่อจริง/ชื่อบริษัท (ภาษาไทย)
		/// </summary>
		[Description("ชื่อ (ภาษาไทย)")]
		public string FirstNameTH { get; set; }
		/// <summary>
		/// นามสกุล (ภาษาไทย)
		/// </summary>
		[Description("นามสกุล (ภาษาไทย)")]
		public string LastNameTH { get; set; }
		/// <summary>
		/// Email
		/// </summary>
		[Description("Email")]
		public string Email { get; set; }
		/// <summary>
		/// เบอร์โทรศัพท์
		/// </summary>
		[Description("เบอร์โทรศัพท์")]
		public string PhoneNumber { get; set; }
		/// <summary>
		/// รหัสโครงการ
		/// </summary>
		[Description("รหัสโครงการ")]
		public string ProjectNo { get; set; }
		/// <summary>
		/// EmployeeNo
		/// </summary>
		[Description("EmployeeNo")]
		public string EmployeeNo { get; set; }

		//public async Task ValidateAsync(models.DatabaseContext DB)
		//{
		//	ValidateException ex = new ValidateException();
		//	if (this.National != null)
		//	{
		//		if (string.IsNullOrEmpty(this.CitizenIdentityNo))
		//		{
		//			var nationThaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstAsync();
		//			if (this.National.Id == nationThaiID)
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
		//				var msg = errMsg.Message;
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//		}
		//	}
		//	if (this.ContactEmails.Count > 0)
		//	{
		//		var isCheckNull = false;
		//		if (this.National != null)
		//		{
		//			var nationThaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstAsync();
		//			if (this.National.Id == nationThaiID)
		//			{
		//				isCheckNull = false;
		//			}
		//			else
		//			{
		//				isCheckNull = true;
		//			}
		//		}

		//		var isEmailNull = this.ContactEmails.Any(o => string.IsNullOrEmpty(o.Email));
		//		if (isEmailNull)
		//		{
		//			if (isCheckNull)
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//				string desc = typeof(ContactEmailDTO).GetProperty(nameof(ContactEmailDTO.Email)).GetCustomAttribute<DescriptionAttribute>().Description;
		//				var msg = errMsg.Message.Replace("[field]", desc);
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//		}
		//		else
		//		{
		//			var isEmailInvalid = this.ContactEmails.Any(o => o.Email.IsValidEmail() == false);
		//			if (isEmailInvalid)
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0015").FirstAsync();
		//				var msg = errMsg.Message;
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//		}

		//		List<string> emailList = new List<string>();
		//		foreach (var email in this.ContactEmails)
		//		{
		//			if (!string.IsNullOrEmpty(email.Email))
		//			{
		//				if (emailList.Contains(email.Email))
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactEmails)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

		//					break;
		//				}
		//				else
		//				{
		//					emailList.Add(email.Email);
		//				}
		//			}
		//		}
		//	}

		//	if (this.ContactPhones.Count > 0)
		//	{
		//		var isPhoneTypeNull = this.ContactPhones.Where(o => o.PhoneType == null).Any();
		//		if (isPhoneTypeNull)
		//		{
		//			var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//			string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.PhoneType)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			var msg = errMsg.Message.Replace("[field]", desc);
		//			ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//		}

		//		var isPhoneNumberNull = this.ContactPhones.Any(o => string.IsNullOrEmpty(o.PhoneNumber));
		//		if (isPhoneNumberNull)
		//		{
		//			var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//			string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			var msg = errMsg.Message.Replace("[field]", desc);
		//			ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//		}
		//		else
		//		{
		//			var isNumberNull = this.ContactPhones.Any(o => !o.PhoneNumber.IsOnlyNumber());
		//			if (isNumberNull)
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
		//				string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
		//				var msg = errMsg.Message.Replace("[field]", desc);
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//		}

		//		if (!isPhoneTypeNull)
		//		{
		//			var foreignID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "PhoneType" && o.Key == "3").Select(o => o.ID).FirstAsync();
		//			var foreignList = this.ContactPhones.Where(o => o.PhoneType.Id == foreignID).ToList();

		//			if (foreignList.Count > 0)
		//			{
		//				var isCountryCodeNull = foreignList.Any(o => string.IsNullOrEmpty(o.CountryCode));
		//				if (isCountryCodeNull)
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//					string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//				else
		//				{
		//					var isInvalidCode = foreignList.Any(o => !o.CountryCode.IsOnlyNumberWithSpecialCharacter(false, "+"));
		//					if (isInvalidCode)
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
		//						string desc = typeof(ContactPhoneDTO).GetProperty(nameof(ContactPhoneDTO.CountryCode)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}
		//		}

		//		List<ContactPhoneList> phoneList = new List<ContactPhoneList>();
		//		foreach (var phone in this.ContactPhones)
		//		{
		//			if (!string.IsNullOrEmpty(phone.PhoneNumber) && (phone.PhoneType != null))
		//			{
		//				var isPhoneExits = phoneList.Where(o => o.PhoneNumber == phone.PhoneNumber && o.PhoneTypeMastercenterID == phone.PhoneType.Id).Any();

		//				if (isPhoneExits)
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0033").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactPhones)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

		//					break;
		//				}
		//				else
		//				{
		//					phoneList.Add(new ContactPhoneList
		//					{
		//						PhoneNumber = phone.PhoneNumber,
		//						PhoneTypeMastercenterID = phone.PhoneType.Id
		//					});
		//				}
		//			}
		//		}
		//	}

		//	if (!string.IsNullOrEmpty(this.WhatsApp))
		//	{
		//		if (!this.WhatsApp.CheckLang(false, true, true, false))
		//		{
		//			var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
		//			string desc = this.GetType().GetProperty(nameof(ContactDTO.WhatsApp)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			var msg = errMsg.Message.Replace("[field]", desc);
		//			ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//		}
		//	}
		//	if (!string.IsNullOrEmpty(this.WeChat))
		//	{
		//		if (!this.WeChat.CheckLang(false, true, true, false))
		//		{
		//			var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
		//			string desc = this.GetType().GetProperty(nameof(ContactDTO.WeChat)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			var msg = errMsg.Message.Replace("[field]", desc);
		//			ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//		}
		//	}
		//	if (!string.IsNullOrEmpty(this.LineID))
		//	{
		//		if (!this.LineID.CheckLang(false, true, true, false))
		//		{
		//			var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
		//			string desc = this.GetType().GetProperty(nameof(ContactDTO.LineID)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			var msg = errMsg.Message.Replace("[field]", desc);
		//			ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//		}
		//	}

		//	if (this.ContactType == null)
		//	{
		//		var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//		string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactType)).GetCustomAttribute<DescriptionAttribute>().Description;
		//		var msg = errMsg.Message.Replace("[field]", desc);
		//		ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//	}
		//	else
		//	{
		//		var legalID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactType" && o.Key == "1").Select(o => o.ID).FirstAsync();
		//		if (this.ContactType.Id == legalID)
		//		{
		//			//if (string.IsNullOrEmpty(this.TaxID))
		//			//{
		//			//    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//			//    string desc = this.GetType().GetProperty(nameof(ContactDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			//    var msg = errMsg.Message.Replace("[field]", desc);
		//			//    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			//}
		//			//else if (!this.TaxID.IsOnlyNumber())
		//			//{
		//			//    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
		//			//    string desc = this.GetType().GetProperty(nameof(ContactDTO.TaxID)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			//    var msg = errMsg.Message.Replace("[field]", desc);
		//			//    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			//}

		//			if (!string.IsNullOrEmpty(this.PhoneNumber))
		//			{
		//				if (!this.PhoneNumber.IsOnlyNumber())
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//			}
		//			if (!string.IsNullOrEmpty(this.PhoneNumberExt))
		//			{
		//				if (!this.PhoneNumberExt.IsOnlyNumber())
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0004").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.PhoneNumberExt)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//			}
		//			if (!string.IsNullOrEmpty(this.ContactFirstName))
		//			{
		//				if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//			}
		//			if (!string.IsNullOrEmpty(this.ContactLastname))
		//			{
		//				if (!this.ContactFirstName.CheckAllLang(false, false, false, null, " "))
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.ContactFirstName)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//			}

		//			if (string.IsNullOrEmpty(this.FirstNameTH))
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//				string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//				var msg = errMsg.Message.Replace("[field]", desc);
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//			//else
		//			//{
		//			//    if (!this.FirstNameTH.CheckLang(true, true, true, false))
		//			//    {
		//			//        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
		//			//        string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			//        var msg = errMsg.Message.Replace("[field]", desc);
		//			//        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			//    }
		//			//}

		//			if (string.IsNullOrEmpty(this.FirstNameEN))
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//				string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
		//				var msg = errMsg.Message.Replace("[field]", desc);
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//			else
		//			{
		//				if (!this.FirstNameEN.CheckLang(false, true, true, false))
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//			}
		//		}
		//		else
		//		{
		//			//var isCheckNation = false;
		//			var isCheckNationThai = false;
		//			#region Citizen
		//			var thaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstOrDefaultAsync();
		//			if (this.National != null)
		//			{
		//				if (this.National.Id == thaiID)
		//				{
		//					isCheckNationThai = true;
		//				}
		//			}
		//			if (!string.IsNullOrEmpty(this.CitizenIdentityNo))
		//			{
		//				if (this.National == null)
		//				{
		//					//21052020
		//					//var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//					//string desc = this.GetType().GetProperty(nameof(ContactDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					//var msg = errMsg.Message.Replace("[field]", desc);
		//					//ex.AddError(errMsg.Key, msg, (int)errMsg.Type);

		//					//isCheckNation = true;
		//				}
		//				else
		//				{
		//					//var thaiID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.National && o.Key == NationalKeys.Thai).Select(o => o.ID).FirstOrDefaultAsync();
		//					if (this.National.Id == thaiID)
		//					{
		//						if (!this.CitizenIdentityNo.IsOnlyNumber())
		//						{
		//							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
		//							var msg = errMsg.Message;
		//							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//						}
		//						else if (this.CitizenIdentityNo.Length != 13)
		//						{
		//							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0014").FirstAsync();
		//							var msg = errMsg.Message;
		//							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//						}
		//					}
		//					else
		//					{
		//						if (!this.CitizenIdentityNo.CheckLang(false, true, false, false))
		//						{
		//							var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0002").FirstAsync();
		//							string desc = this.GetType().GetProperty(nameof(ContactDTO.CitizenIdentityNo)).GetCustomAttribute<DescriptionAttribute>().Description;
		//							var msg = errMsg.Message.Replace("[field]", desc);
		//							ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//						}
		//					}
		//				}
		//			}
		//			#endregion

		//			#region TH
		//			if (this.TitleTH == null)
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//				string desc = this.GetType().GetProperty(nameof(ContactDTO.TitleTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//				var msg = errMsg.Message.Replace("[field]", desc);
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//			else
		//			{
		//				var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleTH" && o.Key == "-1").Select(o => o.ID).FirstAsync();
		//				if (this.TitleTH.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtTH))
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.TitleExtTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//			}

		//			if (string.IsNullOrEmpty(this.FirstNameTH))
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//				string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//				var msg = errMsg.Message.Replace("[field]", desc);
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//			else
		//			{	
		//				if (isCheckNationThai)
		//				{
		//					if (!this.FirstNameTH.CheckAllLang(false, false, false, null, " "))
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
		//						string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}

		//			if (string.IsNullOrEmpty(this.LastNameTH))
		//			{
		//				var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//				string desc = this.GetType().GetProperty(nameof(ContactDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//				var msg = errMsg.Message.Replace("[field]", desc);
		//				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			}
		//			else
		//			{
		//				if (isCheckNationThai)
		//				{
		//					if (!this.LastNameTH.CheckAllLang(false, false, false, null, " "))
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
		//						string desc = this.GetType().GetProperty(nameof(ContactDTO.LastNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}

		//			if (!string.IsNullOrEmpty(this.MiddleNameTH))
		//			{
		//				if (isCheckNationThai)
		//				{
		//					if (!this.MiddleNameTH.CheckAllLang(false, false, false, null, " "))
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
		//						string desc = this.GetType().GetProperty(nameof(ContactDTO.MiddleNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}

		//			if (!string.IsNullOrEmpty(this.Nickname))
		//			{
		//				if (isCheckNationThai)
		//				{
		//					if (!this.Nickname.CheckAllLang(false, false, false, null, " "))
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0012").FirstAsync();
		//						string desc = this.GetType().GetProperty(nameof(ContactDTO.Nickname)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}
		//			#endregion

		//			#region EN
		//			if (this.TitleEN != null)
		//			{
		//				var externalTitleID = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == "ContactTitleEN" && o.Key == "-1").Select(o => o.ID).FirstAsync();
		//				if (this.TitleEN.Id == externalTitleID && string.IsNullOrEmpty(this.TitleExtEN))
		//				{
		//					var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//					string desc = this.GetType().GetProperty(nameof(ContactDTO.TitleExtEN)).GetCustomAttribute<DescriptionAttribute>().Description;
		//					var msg = errMsg.Message.Replace("[field]", desc);
		//					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//				}
		//			}

		//			if (!string.IsNullOrEmpty(this.FirstNameEN))
		//			{
		//				if (isCheckNationThai)
		//				{
		//					if (!this.FirstNameEN.CheckLang(false, false, false, false, null, " "))
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
		//						string desc = this.GetType().GetProperty(nameof(ContactDTO.FirstNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}

		//			if (!string.IsNullOrEmpty(this.LastNameEN))
		//			{
		//				if (isCheckNationThai)
		//				{
		//					if (!this.LastNameEN.CheckLang(false, false, false, false, null, " "))
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
		//						string desc = this.GetType().GetProperty(nameof(ContactDTO.LastNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}

		//			if (!string.IsNullOrEmpty(this.MiddleNameEN))
		//			{
		//				if (isCheckNationThai)
		//				{
		//					if (!this.MiddleNameEN.CheckLang(false, false, false, false, null, " "))
		//					{
		//						var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
		//						string desc = this.GetType().GetProperty(nameof(ContactDTO.MiddleNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
		//						var msg = errMsg.Message.Replace("[field]", desc);
		//						ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//					}
		//				}
		//			}
		//			#endregion

		//			//if (!isCheckNation) //21022020
		//			//{
		//			//    if (this.National == null)
		//			//    {
		//			//        var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//			//        string desc = this.GetType().GetProperty(nameof(ContactDTO.National)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			//        var msg = errMsg.Message.Replace("[field]", desc);
		//			//        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			//    }
		//			//}

		//			//if (this.Gender == null) //21022020
		//			//{
		//			//    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
		//			//    string desc = this.GetType().GetProperty(nameof(ContactDTO.Gender)).GetCustomAttribute<DescriptionAttribute>().Description;
		//			//    var msg = errMsg.Message.Replace("[field]", desc);
		//			//    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
		//			//}
		//		}
		//	}

		//	if (ex.HasError)
		//	{
		//		throw ex;
		//	}
		//}

		//public void ToModel(ref models.CTM.Contact model)
		//{
		//	model.TitleExtTH = this.TitleExtTH;
		//	model.FirstNameTH = this.FirstNameTH;
		//	model.MiddleNameTH = this.MiddleNameTH;
		//	model.LastNameTH = this.LastNameTH;
		//	if (this.TitleTH != null)
		//	{
		//		if (this.TitleTH.Key.Equals("-1"))
		//		{
		//			model.FullnameTH = this.TitleExtTH + this.FirstNameTH + " " + this.LastNameTH;
		//		}
		//		else
		//		{
		//			model.FullnameTH = this.TitleTH.FullName + this.FirstNameTH + " " + this.LastNameTH;
		//		}
		//	}
		//	model.Nickname = this.Nickname;
		//	model.TitleExtEN = this.TitleExtEN;
		//	model.FirstNameEN = this.FirstNameEN;

		//	if (this.TitleEN != null)
		//	{
		//		if (this.TitleEN.Key.Equals("-1"))
		//		{
		//			model.FullnameEN = this.TitleExtEN + this.FirstNameEN + " " + this.LastNameEN;
		//		}
		//		else
		//		{
		//			model.FullnameEN = this.TitleEN.FullName + this.FirstNameEN + " " + this.LastNameEN;
		//		}
		//	}

		//	model.MiddleNameEN = this.MiddleNameEN;
		//	model.LastNameEN = this.LastNameEN;
		//	model.CitizenIdentityNo = this.CitizenIdentityNo;
		//	model.BirthDate = this.BirthDate;
		//	model.TaxID = this.TaxID;
		//	model.PhoneNumber = this.PhoneNumber;
		//	model.PhoneNumberExt = this.PhoneNumberExt;
		//	model.ContactFirstName = this.ContactFirstName;
		//	model.ContactLastname = this.ContactLastname;
		//	model.WeChatID = this.WeChat;
		//	model.WhatsAppID = this.WhatsApp;
		//	model.LineID = this.LineID;
		//	model.ContactTypeMasterCenterID = this.ContactType?.Id;
		//	model.GenderMasterCenterID = this.Gender?.Id;
		//	model.ContactTitleTHMasterCenterID = this.TitleTH?.Id;
		//	model.ContactTitleENMasterCenterID = this.TitleEN?.Id;
		//	model.NationalMasterCenterID = this.National?.Id;
		//	model.CitizenExpireDate = this.CitizenExpireDate;
		//}

		//private class ContactPhoneList
		//{
		//	public string PhoneNumber { get; set; }
		//	public Guid? PhoneTypeMastercenterID { get; set; }
		//}
	}
}
