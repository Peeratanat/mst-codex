using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Base.DTOs.PRJ
{
	public class WaiveQCDTO : BaseDTO
	{
		/// <summary>
		/// เลขที่แปลง
		///  Project/api/Projects/{projectID}/Units/DropdownList
		/// </summary>
		public UnitDropdownDTO Unit { get; set; }
		/// <summary>
		/// วันที่โอนจริง
		/// </summary>
		public DateTime? ActualTransferDate { get; set; }
		/// <summary>
		/// วันที่ Wavie QC
		/// </summary>
		public DateTime? WaiveQCDate { get; set; }
		/// <summary>
		/// เปิด-ปิดแก้ไข (รายการที่ตั้งโอน)
		/// </summary>
		public bool IsAction { get; set; }
		public static WaiveQCDTO CreateFromModel(WaiveQC model)
		{
			if (model != null)
			{
				var result = new WaiveQCDTO()
				{
					Id = model.ID,
					Unit = UnitDropdownDTO.CreateFromModel(model.Unit),
					ActualTransferDate = model.ActualTransferDate,
					WaiveQCDate = model.WaiveQCDate,
					Updated = model.Updated,
					UpdatedBy = model.UpdatedBy?.DisplayName
				};
				return result;
			}
			else
			{
				return null;
			}
		}

		public static WaiveQCDTO CreateFromQueryResult(WaiveQCQueryResult model, List<string> unitsNoTrf)
		{
			if (model != null)
			{
				var result = new WaiveQCDTO()
				{
					Id = model.WaiveQC?.ID,
					Unit = UnitDropdownDTO.Create2ParameterFromModel(model.Unit, model.MasterCenter),
					ActualTransferDate = model.WaiveQC?.ActualTransferDate,
					WaiveQCDate = model.WaiveQC?.WaiveQCDate,
					Updated = model.WaiveQC?.Updated,
					UpdatedBy = model.UpdatedBy?.DisplayName,
					//IsAction = unitsNoTrf.Contains(model.Unit?.UnitNo) ? false : true
					IsAction = model.MasterCenter?.Order > 4 ? false : true
				};

				//if (unitsNoTrf.Contains(model.Unit?.UnitNo) || !waiveQCsProjects.Contains(model.WaiveQC.ID))
				//{
				//	result.IsAction = false;
				//}
				//else
				//{
				//	result.IsAction = true;
				//}
				return result;
			}
			else
			{
				return null;
			}
		}

		public static WaiveQCDTO CreateFromQueryTempResult(WaiveQCQueryResult model)
		{
			if (model != null)
			{
				var result = new WaiveQCDTO();
				result.Unit = UnitDropdownDTO.CreateFromModel(model.Unit);

				return result;
			}
			else
			{
				return null;
			}
		}

		public void ToModel(ref WaiveQC model, bool isUpdate)
		{
			if (!isUpdate)
			{
				model.UnitID = this.Unit?.Id;
				model.ActualTransferDate = this.ActualTransferDate;
			}
			model.WaiveQCDate = this.WaiveQCDate;
		}

		public static void SortBy(WaiveQCSortByParam sortByParam, ref IQueryable<WaiveQCQueryResult> query)
		{
			if (sortByParam.SortBy != null)
			{
				switch (sortByParam.SortBy.Value)
				{
					case WaiveQCSortBy.Unit:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitNo);
						else query = query.OrderByDescending(o => o.Unit.UnitNo);
						break;
					case WaiveQCSortBy.Unit_UnitStatus:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.Unit.UnitStatus.Name);
						else query = query.OrderByDescending(o => o.Unit.UnitStatus.Name);
						break;
					case WaiveQCSortBy.ActualTransferDate:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.WaiveQC.ActualTransferDate);
						else query = query.OrderByDescending(o => o.WaiveQC.ActualTransferDate);
						break;
					case WaiveQCSortBy.WaiveQCDate:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.WaiveQC.WaiveQCDate);
						else query = query.OrderByDescending(o => o.WaiveQC.WaiveQCDate);
						break;
					case WaiveQCSortBy.UpdatedBy:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.WaiveQC.UpdatedBy.DisplayName);
						else query = query.OrderByDescending(o => o.WaiveQC.UpdatedBy.DisplayName);
						break;
					case WaiveQCSortBy.Updated:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.WaiveQC.Updated);
						else query = query.OrderByDescending(o => o.WaiveQC.Updated);
						break;
					default:
						query = query.OrderBy(o => o.Unit.UnitNo);
						break;
				}
			}
			else
			{
				query = query.OrderBy(o => o.Unit.UnitNo);
			}
		}
	}
	public class WaiveQCQueryResult
	{
		public Model Model { get; set; }
		public Unit Unit { get; set; }
		public WaiveQC WaiveQC { get; set; }
		public User UpdatedBy { get; set; }
		public MasterCenter MasterCenter { get; set; }
	}
}
