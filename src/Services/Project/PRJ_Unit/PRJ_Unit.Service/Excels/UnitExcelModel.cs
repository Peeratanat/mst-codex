using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PRJ_Unit.Services.Excels
{
	public class UnitExcelModel
	{
		public const int _projectNoIndex = 0;
		public const int _projectCodeIndex = 1;
		public const int _wbsCodeIndex = 2;
		public const int _wbsobjectCodeIndex = 3;
		public const int _unitNoIndex = 4;
		public const int _modelNameIndex = 5;//3
		public const int _unitHighRiseLocationIndex = 6;//4
		public const int _towerIndex = 7;//1
		public const int _floorIndex = 8;//2
		public const int _unitDirectionIndex = 9;
		public const int _floorplanNameIndex = 10;
		public const int _roomplanNameIndex = 11;
		public const int _assetTypeIndex = 12;
		public const int _saleAreaIndex = 13;
		public const int _titledeedArea = 14;
		public const int _numberOfPrivilegeIndex = 15;
		public const int _numberOfParkingFixIndex = 16;
		public const int _numberOfParkingUnFixIndex = 17;
		public const int _quotaForeignUnit = 18;

		public string ProjectNo { get; set; }
		public string ProjectCode { get; set; }
		public string WBSNo { get; set; }
		public string WBSObjectCode { get; set; }
		public string UnitNo { get; set; }
		public string ModelName { get; set; }
		public string HighRiseLocation { get; set; }
		public string TowerCode { get; set; }
		public string FloorName { get; set; }
		public string UnitDirection { get; set; }
		public string FloorPlan { get; set; }
		public string RoomPlan { get; set; }
		public string AssetType { get; set; }
		public double SaleArea { get; set; }
		public double TitedeedArea { get; set; }
		public double NumberOfPrivilege { get; set; }
		public decimal? UnitLoanAmount { get; set; }
		public double NumberOfParkingFix { get; set; }
		public double NumberOfParkingUnFix { get; set; }
		public string QuotaForeignUnit { get; set; }
		public static UnitExcelModel CreateFromDataRow(DataRow dr)
		{
			var result = new UnitExcelModel();
			result.ProjectNo = dr[_projectNoIndex]?.ToString();
			result.ProjectCode = dr[_projectCodeIndex]?.ToString();
			result.WBSNo = dr[_wbsCodeIndex]?.ToString();
			result.WBSObjectCode = dr[_wbsobjectCodeIndex]?.ToString();
			result.UnitNo = dr[_unitNoIndex]?.ToString();
			result.ModelName = dr[_modelNameIndex]?.ToString();
			result.HighRiseLocation = dr[_unitHighRiseLocationIndex]?.ToString();
			result.TowerCode = dr[_towerIndex]?.ToString();
			result.FloorName = dr[_floorIndex]?.ToString();
			result.UnitDirection = dr[_unitDirectionIndex]?.ToString();
			result.FloorPlan = dr[_floorplanNameIndex]?.ToString();
			result.RoomPlan = dr[_roomplanNameIndex]?.ToString();
			result.AssetType = dr[_assetTypeIndex]?.ToString();
			double titedeedArea;
			if (double.TryParse(dr[_titledeedArea]?.ToString(), out titedeedArea))
			{
				result.TitedeedArea = titedeedArea;
			}
			double saleArea;
			if (double.TryParse(dr[_saleAreaIndex]?.ToString(), out saleArea))
			{
				result.SaleArea = saleArea;
			}
			//double numberOfPrivilege;
			//if (double.TryParse(dr[_numberOfPrivilegeIndex]?.ToString(), out numberOfPrivilege))
			//{
			//    result.NumberOfPrivilege = numberOfPrivilege;
			//}
			decimal unitLoanAmount;
			if (decimal.TryParse(dr[_numberOfPrivilegeIndex]?.ToString(), out unitLoanAmount))
			{
				result.UnitLoanAmount = unitLoanAmount;
			}
			double numberOfParkingFix;
			if (double.TryParse(dr[_numberOfParkingFixIndex]?.ToString(), out numberOfParkingFix))
			{
				result.NumberOfParkingFix = numberOfParkingFix;
			}
			double numberOfParkingUnFix;
			if (double.TryParse(dr[_numberOfParkingUnFixIndex]?.ToString(), out numberOfParkingUnFix))
			{
				result.NumberOfParkingUnFix = numberOfParkingUnFix;
			}
			result.QuotaForeignUnit = dr[_quotaForeignUnit]?.ToString();
			return result;
		}
		public void ToModel(ref Unit model, bool isLowRise = false)
		{
			model.SAPWBSNo = this.WBSNo;
			model.SAPWBSObject = this.WBSObjectCode;
			model.FloorPlanFileName = this.FloorPlan;
			model.RoomPlanFileName = this.RoomPlan;
			model.UnitNo = this.UnitNo;
			if (isLowRise)
			{
				// model.NumberOfPrivilege = this.NumberOfPrivilege;
				model.UnitLoanAmount = this.UnitLoanAmount;
			}
			model.NumberOfParkingFix = this.NumberOfParkingFix;
			model.NumberOfParkingUnFix = this.NumberOfParkingUnFix;
			if (!isLowRise)
			{
				model.IsForeignUnit = this.QuotaForeignUnit.Equals("1") ? true : false;
			}
		}
	}
}
