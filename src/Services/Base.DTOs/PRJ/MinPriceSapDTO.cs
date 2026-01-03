using Database.Models;
using Database.Models.DbQueries.PRJ;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
	public class MinPriceSapDTO : BaseDTO
	{
		public Guid? ProjectID { get; set; }
		public Guid? UnitID { get; set; }
		public string ProjectNo { get; set; }
		public string SapCode { get; set; }
		public string UnitNo { get; set; }
		public double? SaleArea { get; set; }
		public string SAPWBSNo { get; set; }
		public string SAPWBSObject { get; set; }
		public string SAPWBSStatus { get; set; }
		public string BOQStyle { get; set; }
		public string HomeStyle { get; set; }
		public string CompanyCode { get; set; }
		public string MinPriceTypeKey { get; set; }
		public string MinPriceTypeName { get; set; }
		public decimal? Minprice { get; set; }
		public decimal? ROIMinprice { get; set; }

		public static MinPriceSapDTO CreateFromQuery(dbqMinPriceSAPList model)
		{
			if (model != null)
			{
				var result = new MinPriceSapDTO()
				{
					ProjectID = model.ProjectID,
					UnitID = model.UnitID,
					ProjectNo = model.ProjectNo,
					SapCode = model.SapCode,
					UnitNo = model.UnitNo,
					SaleArea = model.SaleArea,
					SAPWBSNo = model.SAPWBSNo,
					SAPWBSObject = model.SAPWBSObject,
					SAPWBSStatus = model.SAPWBSStatus,
					BOQStyle = model.BOQStyle,
					HomeStyle = model.HomeStyle,
					CompanyCode = model.CompanyCode,
					MinPriceTypeKey = model.MinPriceTypeKey,
					MinPriceTypeName = model.MinPriceTypeName,
					Minprice = model.Minprice,
					ROIMinprice = model.ROIMinprice,
				};
				return result;
			}
			else
			{
				return null;
			}
		}
	}

}
