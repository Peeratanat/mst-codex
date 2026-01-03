using System;
using System.Linq;

namespace Base.DTOs.CTM
{
	public class VisitorPurchaseHistoryDTO
	{
		/// <summary>
		/// วันที่
		/// </summary>
		public DateTime? PurchaseDate { get; set; }
		/// <summary>
		/// โครงการ
		/// </summary>
		public PRJ.ProjectDTO Project { get; set; }
		/// <summary>
		/// แปลง
		/// </summary>
		public PRJ.UnitDTO Unit { get; set; }
		/// <summary>
		/// มูลค่า (ราคาขายสุทธิ)
		/// </summary>
		public decimal? NetSellingPrice { get; set; }

		public string UnitStatus { get; set; }

		public static void VisitorSortBy(VisitorPurchaseHistoryListSortByParam sortByParam, ref IQueryable<VisitorPurchaseHistoryQueryResult> query)
		{
			if (sortByParam.SortBy != null)
			{
				//switch (sortByParam.SortBy.Value)
				//{
				//case VisitorLeadListSortBy.Advertisement:
				//    if (sortByParam.Ascending) query = query.OrderBy(o => o.Lead.Advertisement.Name);
				//    else query = query.OrderByDescending(o => o.Lead.Advertisement.Name);
				//    break;
				//case VisitorLeadListSortBy.Project:
				//    if (sortByParam.Ascending) query = query.OrderBy(o => o.Project.ProjectNo);W
				//    else query = query.OrderByDescending(o => o.Project.ProjectNo);
				//    break;
				//case VisitorLeadListSortBy.CreatedDate:
				//    if (sortByParam.Ascending) query = query.OrderBy(o => o.Lead.Created);
				//    else query = query.OrderByDescending(o => o.Lead.Created);
				//    break;
				//case VisitorLeadListSortBy.Remark:
				//    if (sortByParam.Ascending) query = query.OrderBy(o => o.Lead.Remark);
				//    else query = query.OrderByDescending(o => o.Lead.Remark);
				//    break;
				//default:
				//    query = query.OrderBy(o => o.Lead.Created);
				//    break;
				// }
			}
			else
			{
				// query = query.OrderBy(o => o.Project);
			}
		}

		public static VisitorPurchaseHistoryDTO CreateFromQueryResult(VisitorPurchaseHistoryQueryResult model)
		{
			if (model != null)
			{
				VisitorPurchaseHistoryDTO result = new VisitorPurchaseHistoryDTO()
				{
					Unit = model.Unit,
					Project = model.Project,
					PurchaseDate = model.PurchaseDate,
					NetSellingPrice = model.NetSellingPrice,
					UnitStatus = model.UnitStatus
				};

				return result;
			}
			else
			{
				return null;
			}
		}
		public class VisitorPurchaseHistoryQueryResult
		{
			public PRJ.ProjectDTO Project { get; set; }
			public PRJ.UnitDTO Unit { get; set; }
			public DateTime? PurchaseDate { get; set; }
			public decimal? NetSellingPrice { get; set; }
			public string UnitStatus { get; set; }

		}
	}
}
