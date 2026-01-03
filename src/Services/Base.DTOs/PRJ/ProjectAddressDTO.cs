using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.MST;
using Database.Models;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Base.DTOs.PRJ
{
	public class ProjectAddressDTO : BaseDTO
	{
		/// <summary>
		/// ประเภทที่ตั้ง
		/// Master/api/MasterCenters?masterCenterGroupKey=ProjectAddressType
		/// </summary>
		public MasterCenterDropdownDTO ProjectAddressType { get; set; }
		/// <summary>
		/// ชื่อที่ตั้ง (TH)
		/// </summary>
		/// <value>The address name th.</value>
		public string AddressNameTH { get; set; }
		/// <summary>
		/// ชื่อที่ตั้ง (EN)
		/// </summary>
		/// <value>The address name en.</value>
		[Description("ชื่อที่ตั้ง (EN)")]
		public string AddressNameEN { get; set; }
		/// <summary>
		/// เลขที่โฉนด (comma separated)
		/// </summary>
		/// <value>The title deed no.</value>
		[Description("เลขที่โฉนด")]
		public string TitleDeedNo { get; set; }
		/// <summary>
		/// เลขที่ดิน (comma separated)
		/// </summary>
		/// <value>The land no.</value>
		[Description("เลขที่ดิน")]
		public string LandNo { get; set; }
		/// <summary>
		/// หน้าสำรวจ (comma separated)
		/// </summary>
		/// <value>The inspection no.</value>
		[Description("หน้าสำรวจ")]
		public string InspectionNo { get; set; }
		/// <summary>
		/// รหัสไปรณีย์
		/// </summary>
		[Description("รหัสไปรณีย์")]
		public string PostalCode { get; set; }
		/// <summary>
		/// จังหวัด
		/// Master/api/Provinces/DropdownList
		/// </summary>
		public ProvinceListDTO Province { get; set; }
		/// <summary>
		/// อำเภอ
		/// Master/api/Districts/DropdownList
		/// </summary>
		public DistrictListDTO District { get; set; }
		/// <summary>
		/// ตำบล
		/// Master/api/SubDistricts/DropdownList
		/// </summary>
		public SubDistrictListDTO SubDistrict { get; set; }
		/// <summary>
		/// หมู่ที่
		/// </summary>
		[Description("หมู่ที่")]
		public string Moo { get; set; }
		/// <summary>
		/// ซอย (TH)
		/// </summary>
		public string SoiTH { get; set; }
		/// <summary>
		/// ซอย (EN)
		/// </summary>
		public string SoiEN { get; set; }
		/// <summary>
		/// ถนน (TH)
		/// </summary>
		public string RoadTH { get; set; }
		/// <summary>
		/// ถนน (EN)
		/// </summary>
		public string RoadEN { get; set; }
		//ที่อยู่ตามทะเบียนบ้าน
		/// <summary>
		/// ตำบลข้อมูลตามทะเบียนบ้าน
		/// Master/api/SubDistricts/DropdownList
		/// </summary>
		public SubDistrictListDTO HouseSubDistrict { get; set; }
		/// <summary>
		/// หมู่ ข้อมูลตามทะเบียนบ้าน
		/// </summary>
		[Description("หมู่ ข้อมูลตามทะเบียนบ้าน")]
		public string HouseMoo { get; set; }
		/// <summary>
		/// ซอย ข้อมูลตามทะเบียนบ้าน (TH)
		/// </summary>
		public string HouseSoiTH { get; set; }
		/// <summary>
		/// ซอย ข้อมูลตามทะเบียนบ้าน (EN)
		/// </summary>
		public string HouseSoiEN { get; set; }
		/// <summary>
		/// ถนน ข้อมูลตามทะเบียนบ้าน (TH)
		/// </summary>
		public string HouseRoadTH { get; set; }
		/// <summary>
		/// ถนน ข้อมูลตามทะเบียนบ้าน (EN)
		/// </summary>
		public string HouseRoadEN { get; set; }

		//ที่อยู่ตามโฉนด
		/// <summary>
		/// ตำบล/แขวง ข้อมูลที่อยู่ตามโฉนด
		/// Master/api/SubDistricts/DropdownList
		/// </summary>
		public SubDistrictListDTO TitledeedSubDistrict { get; set; }
		/// <summary>
		/// หมู่ ข้อมูลที่อยู่ตามโฉนด
		/// </summary>
		[Description("หมู่ ข้อมูลที่อยู่ตามโฉนด")]
		public string TitledeedMoo { get; set; }
		/// <summary>
		/// ซอย ข้อมูลที่อยู่ตามโฉนด (TH)
		/// </summary>
		public string TitledeedSoiTH { get; set; }
		/// <summary>
		/// ซอย ข้อมูลที่อยู่ตามโฉนด (EN)
		/// </summary>
		public string TitledeedSoiEN { get; set; }
		/// <summary>
		/// ถนน ข้อมูลที่อยู่ตามโฉนด (TH)
		/// </summary>
		public string TitledeedRoadTH { get; set; }
		/// <summary>
		/// ถนน ข้อมูลที่อยู่ตามโฉนด (EN)
		/// </summary>
		public string TitledeedRoadEN { get; set; }
		/// <summary>
		/// สำนักงานที่ดิน 
		/// Master/api/LandOffice/DropdownList
		/// </summary>
		public LandOfficeListDTO LandOffice { get; set; }
		[Description("จังหวัดอื่นๆ (จากข้อมูล crm เก่า)")]
		public string OtherProvinceEN { get; set; }
		[Description("จังหวัดอื่นๆ (จากข้อมูล crm เก่า)")]
		public string OtherProvinceTH { get; set; }
		[Description("อำเภออื่นๆ (จากข้อมูล crm เก่า)")]
		public string OtherDistrictEN { get; set; }
		[Description("อำเภออื่นๆ (จากข้อมูล crm เก่า)")]
		public string OtherDistrictTH { get; set; }
		[Description("ตำบลอื่นๆ (จากข้อมูล crm เก่า)")]
		public string OtherSubDistrictEN { get; set; }
		[Description("ตำบลอื่นๆ (จากข้อมูล crm เก่า)")]
		public string OtherSubDistrictTH { get; set; }

		public static ProjectAddressDTO CreateFromModel(Address model)
		{
			if (model != null)
			{
				var result = new ProjectAddressDTO()
				{
					Id = model.ID,
					Updated = model.Updated,
					UpdatedBy = model.UpdatedBy?.DisplayName,
					AddressNameEN = model.AddressNameEN,
					AddressNameTH = model.AddressNameTH,
					TitleDeedNo = model.TitleDeedNo,
					LandNo = model.LandNo,
					InspectionNo = model.InspectionNo,
					PostalCode = model.PostalCode,
					HouseMoo = model.HouseMoo,
					HouseSoiEN = model.HouseSoiEN,
					HouseSoiTH = model.HouseSoiTH,
					HouseRoadEN = model.HouseRoadEN,
					HouseRoadTH = model.HouseRoadTH,
					TitledeedMoo = model.TitledeedMoo,
					TitledeedSoiEN = model.TitledeedSoiEN,
					TitledeedSoiTH = model.TitledeedSoiTH,
					TitledeedRoadEN = model.TitledeedRoadEN,
					TitledeedRoadTH = model.TitledeedRoadTH,
					ProjectAddressType = MasterCenterDropdownDTO.CreateFromModel(model.ProjectAddressType),
					District = DistrictListDTO.CreateFromModel(model.District),
					Province = ProvinceListDTO.CreateFromModel(model.Province),
					SubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
					Moo = model.Moo,
					SoiEN = model.SoiEN,
					SoiTH = model.SoiTH,
					RoadEN = model.RoadEN,
					RoadTH = model.RoadTH,
					HouseSubDistrict = SubDistrictListDTO.CreateFromModel(model.HouseSubDistrict),
					TitledeedSubDistrict = SubDistrictListDTO.CreateFromModel(model.TitledeedSubDistrict),
					LandOffice = LandOfficeListDTO.CreateFromModel(model.LandOffice),
					OtherProvinceEN = model.OtherProvinceEN,
					OtherProvinceTH = model.OtherProvinceTH,
					OtherDistrictEN = model.OtherDistrictEN,
					OtherDistrictTH = model.OtherDistrictTH,
					OtherSubDistrictEN = model.OtherSubDistrictEN,
					OtherSubDistrictTH = model.OtherSubDistrictTH
				};

				return result;
			}
			else
			{
				return null;
			}
		}

		public static ProjectAddressDTO CreateAddressWithUnitTitledeedFromModel(Address model, List<Unit> unitList)
		{
			if (model != null)
			{
				var result = new ProjectAddressDTO()
				{
					Id = model.ID,
					Updated = model.Updated,
					UpdatedBy = model.UpdatedBy?.DisplayName,
					AddressNameEN = model.AddressNameEN,
					AddressNameTH = model.AddressNameTH,
					TitleDeedNo = model.TitleDeedNo,
					LandNo = model.LandNo,
					InspectionNo = model.InspectionNo,
					PostalCode = model.PostalCode,
					HouseMoo = model.Moo,
					HouseSoiEN = model.SoiEN,
					HouseSoiTH = model.SoiTH,
					HouseRoadEN = model.RoadEN,
					HouseRoadTH = model.RoadTH,
					TitledeedMoo = model.TitledeedMoo,
					TitledeedSoiEN = model.TitledeedSoiEN,
					TitledeedSoiTH = model.TitledeedSoiTH,
					TitledeedRoadEN = model.TitledeedRoadEN,
					TitledeedRoadTH = model.TitledeedRoadTH,
					ProjectAddressType = MasterCenterDropdownDTO.CreateFromModel(model.ProjectAddressType),
					District = DistrictListDTO.CreateFromModel(model.District),
					Province = ProvinceListDTO.CreateFromModel(model.Province),
					SubDistrict = model.ProjectAddressType?.Key == "3" ? SubDistrictListDTO.CreateFromModel(model.TitledeedSubDistrict) : SubDistrictListDTO.CreateFromModel(model.SubDistrict),
					Moo = model.Moo,
					SoiEN = model.SoiEN,
					SoiTH = model.SoiTH,
					RoadEN = model.RoadEN,
					RoadTH = model.RoadTH,
					HouseSubDistrict = SubDistrictListDTO.CreateFromModel(model.SubDistrict),
					TitledeedSubDistrict = SubDistrictListDTO.CreateFromModel(model.TitledeedSubDistrict),
					LandOffice = LandOfficeListDTO.CreateFromModel(model.LandOffice),
					OtherProvinceEN = model.OtherProvinceEN,
					OtherProvinceTH = model.OtherProvinceTH,
					OtherDistrictEN = model.OtherDistrictEN,
					OtherDistrictTH = model.OtherDistrictTH,
					OtherSubDistrictEN = model.OtherSubDistrictEN,
					OtherSubDistrictTH = model.OtherSubDistrictTH,
					//TitleDeedList = TitleDeedDTO.CreateListFromModel(titledeedDetailList),
					UnitList = UnitDTO.CreateListFromModel(unitList)
				};

				return result;
			}
			else
			{
				return null;
			}
		}

		public async Task ValidateAsync(DatabaseContext db)
		{
			ValidateException ex = new ValidateException();
			if (!string.IsNullOrEmpty(this.AddressNameEN))
			{
				if (!this.AddressNameEN.CheckLang(false, true, true, false))
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.AddressNameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (!string.IsNullOrEmpty(this.TitleDeedNo))
			{
				var isValid = this.TitleDeedNo.IsOnlyNumberWithSpecialCharacter(true);
				if (!isValid)
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.TitleDeedNo)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (!string.IsNullOrEmpty(this.LandNo))
			{
				var isValid = this.LandNo.IsOnlyNumberWithSpecialCharacter(true);
				if (!isValid)
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.LandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (!string.IsNullOrEmpty(this.InspectionNo))
			{
				var isValid = this.InspectionNo.IsOnlyNumberWithSpecialCharacter(true);
				if (!isValid)
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0016").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.InspectionNo)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (!string.IsNullOrEmpty(this.PostalCode))
			{
				if (!this.PostalCode.IsOnlyNumberWithMaxLength(5))
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0024").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.PostalCode)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (!string.IsNullOrEmpty(this.Moo))
			{
				if (!this.Moo.IsOnlyNumber())
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.Moo)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (!string.IsNullOrEmpty(this.HouseMoo))
			{
				if (!this.HouseMoo.IsOnlyNumber())
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.HouseMoo)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (!string.IsNullOrEmpty(this.TitledeedMoo))
			{
				if (!this.TitledeedMoo.IsOnlyNumber())
				{
					var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
					string desc = this.GetType().GetProperty(nameof(ProjectAddressDTO.TitledeedMoo)).GetCustomAttribute<DescriptionAttribute>().Description;
					var msg = errMsg.Message.Replace("[field]", desc);
					ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
				}
			}

			if (ex.HasError)
			{
				throw ex;
			}
		}

		public void ToModel(ref Address model)
		{
			model.AddressNameEN = this.AddressNameEN;
			model.AddressNameTH = this.AddressNameTH;
			model.TitleDeedNo = this.TitleDeedNo;
			model.LandNo = this.LandNo;
			model.InspectionNo = this.InspectionNo;
			model.PostalCode = this.PostalCode;
			model.HouseMoo = this.HouseMoo;
			model.HouseSoiEN = this.HouseSoiEN;
			model.HouseSoiTH = this.HouseSoiTH;
			model.HouseRoadEN = this.HouseRoadEN;
			model.HouseRoadTH = this.HouseRoadTH;
			model.TitledeedMoo = this.TitledeedMoo;
			model.TitledeedSoiEN = this.TitledeedSoiEN;
			model.TitledeedSoiTH = this.TitledeedSoiTH;
			model.TitledeedRoadTH = this.TitledeedRoadTH;
			model.TitledeedRoadEN = this.TitledeedRoadEN;
			model.ProjectAddressTypeMasterCenterID = this.ProjectAddressType?.Id;
			model.DistrictID = this.District?.Id;
			model.ProvinceID = this.Province?.Id;
			model.SubDistrictID = this.ProjectAddressType?.Key == "3" ? this.HouseSubDistrict?.Id : this.SubDistrict?.Id;
			model.Moo = this.ProjectAddressType?.Key == "3" ? this.HouseMoo :this.Moo;
			model.RoadTH = this.ProjectAddressType?.Key == "3" ? this.HouseRoadTH : this.RoadTH;
			model.RoadEN = this.ProjectAddressType?.Key == "3" ? this.HouseRoadEN : this.RoadEN;
			model.SoiTH = this.ProjectAddressType?.Key == "3" ? this.HouseSoiTH : this.SoiTH;
			model.SoiEN = this.ProjectAddressType?.Key == "3" ? this.HouseSoiEN : this.SoiEN;
			model.HouseSubDistrictID = this.HouseSubDistrict?.Id;
			model.TitledeedSubDistrictID = this.ProjectAddressType?.Key == "3" ? this.SubDistrict?.Id : this.TitledeedSubDistrict?.Id;
			model.LandOfficeID = this.LandOffice?.Id;
			//model.OtherProvinceEN = this.OtherProvinceEN;
			//model.OtherProvinceTH = this.OtherProvinceTH;
			//model.OtherDistrictEN = this.OtherDistrictEN;
			//model.OtherDistrictTH = this.OtherDistrictTH;
			//model.OtherSubDistrictEN = this.OtherSubDistrictEN;
			//model.OtherSubDistrictTH = this.OtherSubDistrictTH;
		}

		public List<TitleDeedDTO> TitleDeedList { get; set; }
		public List<UnitDTO> UnitList { get; set; }

	}
}
