using Database.Models;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.USR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Base.DTOs.PRM
{
	/// <summary>
	/// Item ที่มาจาก SAP สำหรับเลือกลงรายการโปร
	/// </summary>
	public class PromotionMaterialDTO : BaseDTO
	{
		/// <summary>
		/// Agreement No.
		/// </summary>
		[Description("Agreement No.")]
		public string AgreementNo { get; set; }
		/// <summary>
		/// เลขที่สิ่งของ
		/// </summary>
		[Description("Item No.")]
		public string ItemNo { get; set; }
		/// <summary>
		/// Plant
		/// </summary>
		public string Plant { get; set; }
		/// <summary>
		/// ชื่อBrand (ภาษาไทย)
		/// </summary>
		public string BrandTH { get; set; }
		/// <summary>
		/// ชื่อBrand (ภาษาอังกฤษ)
		/// </summary>
		public string BrandEN { get; set; }
		/// <summary>
		/// ชื่อสิ่งของ (ภาษาไทย)
		/// </summary>
		public string NameTH { get; set; }
		/// <summary>
		/// ชื่อสิ่งของ (ภาษาอังกฤษ)
		/// </summary>
		public string NameEN { get; set; }
		/// <summary>
		/// Material Code
		/// </summary>
		public string MaterialCode { get; set; }
		/// <summary>
		/// ราคา
		/// </summary>
		public decimal Price { get; set; }
		/// <summary>
		/// หน่วย
		/// </summary>
		public string Unit { get; set; }
		/// <summary>
		/// หน่วยไทย
		/// </summary>
		public string UnitTH { get; set; }
		/// <summary>
		/// หน่วยEN
		/// </summary>
		public string UnitEN { get; set; }
		/// <summary>
		/// Fee
		/// </summary>
		public double Fee { get; set; }
		/// <summary>
		/// วันหมดอายุ
		/// </summary>public double Fee { get; set; }
		public DateTime? ExpireDate { get; set; }

		public DateTime? AgreementExpireDate { get; set; }
		public decimal? BasePrice { get; set; }
		public string SAPSaleTaxCode { get; set; }
		public string MaterialGroupKey { get; set; }
		public string SAPDeleteIndicator { get; set; }
		public string SAPVarKey { get; set; }
		public string PromotionMaterialPlant { get; set; }
		public string PromotionMaterialCode { get; set; }
		public string PromotionMaterialGroup { get; set; }
		public string PromotionMaterialGLAAccount { get; set; }
		public string PromotionMaterialMaterial_MaterialGroupKey { get; set; }
		public string PromotionMaterialMaterialGroup_Key { get; set; }
		public Guid? MaterialItemStatusMasterCenterID { get; set; }

		public static PromotionMaterialDTO CreateFromModel(PromotionMaterialItem model)
		{
			if (model != null)
			{
				var result = new PromotionMaterialDTO()
				{
					Id = model.ID,
					AgreementNo = model.AgreementNo,
					ItemNo = model.ItemNo,
					Plant = model.Plant,
					NameTH = model.NameTH,
					NameEN = model.NameEN,
					MaterialCode = model.MaterialCode,
					Price = model.Price,
					Unit = model.UnitTH,
					ExpireDate = model.ExpireDate,
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
		public static PromotionMaterialDTO CreateFromQueryResult(PromotionMaterialQueryResult model
																 , List<PromotionMaterialAddPrice> promotionmaterialAddPrices
																 , List<PromotionMaterialGroup> PromotionMaterialGroups
																 , Project project)
		{
			if (model != null)
			{
				var result = new PromotionMaterialDTO()
				{
					Id = model.PromotionMaterialItem.ID,
					AgreementNo = model.PromotionMaterialItem.AgreementNo,
					ItemNo = model.PromotionMaterialItem.ItemNo,
					Plant = model.PromotionMaterialItem.Plant,
					NameTH = model.PromotionMaterialItem.NameTH,
					NameEN = model.PromotionMaterialItem.NameEN,
					MaterialCode = model.PromotionMaterialItem.MaterialCode,
					//Price = model.PromotionMaterialItem.Price,
					Unit = model.PromotionMaterialItem.UnitTH,
					//ExpireDate = model.PromotionMaterialItem.ExpireDate,
					ExpireDate = model.PromotionMaterialItem.AgreementExpireDate <= model.PromotionMaterialItem.ExpireDate ?
					             model.PromotionMaterialItem.AgreementExpireDate : model.PromotionMaterialItem.ExpireDate,
					Updated = model.PromotionMaterialItem.Updated,
					UpdatedBy = model.PromotionMaterialItem.UpdatedBy?.DisplayName
				};

				double percentMarkUP = 0;
				var promotionmaterialGroup = PromotionMaterialGroups.Where(o => o.Key == model.PromotionMaterialItem.MaterialGroupKey).FirstOrDefault();
				var promotionmaterialAddPrice = promotionmaterialGroup != null ? promotionmaterialAddPrices.Where(o => o.PromotionMaterialGroupID == promotionmaterialGroup.ID).FirstOrDefault() : null;

				if (project.ProductType.Key == ProductTypeKeys.LowRise)
				{
					percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.LowRisePercent;
				}
				else if (project.ProductType.Key == ProductTypeKeys.HighRise)
				{
					percentMarkUP = promotionmaterialAddPrice == null ? 0 : promotionmaterialAddPrice.HighRisePercent;
				}

				var vat7Pct = model.PromotionMaterialItem.SAPSaleTaxCode == "VX"
							 || string.IsNullOrEmpty(model.PromotionMaterialItem.SAPSaleTaxCode) ? 0 : Convert.ToDecimal(1.07);

				//case vat7 = 0 : (BasePrice) + (BasePrice * (Low/HighRisePercent / 100))
				//case vat7 = 7 : (BasePrice * 1.07) + ((BasePrice * 1.07) * (Low/HighRisePercent / 100))
				result.Price = vat7Pct == 0 ?
							   (model.PromotionMaterialItem.BasePrice) +
							   (((model.PromotionMaterialItem.BasePrice) * Convert.ToDecimal(percentMarkUP)) / 100) :
							   (model.PromotionMaterialItem.BasePrice * Convert.ToDecimal(1.07)) +
							   (((model.PromotionMaterialItem.BasePrice * Convert.ToDecimal(1.07)) * Convert.ToDecimal(percentMarkUP)) / 100);

				return result;
			}
			else
			{
				return null;
			}
		}
		public static PromotionMaterialDTO CreateFromQueryWithBrandResult(PromotionMaterialQueryResult model)
		{
			if (model != null)
			{
				var result = new PromotionMaterialDTO()
				{
					Id = model.PromotionMaterialItem.ID,
					AgreementNo = model.PromotionMaterialItem.AgreementNo,
					ItemNo = model.PromotionMaterialItem.ItemNo,
					Plant = model.PromotionMaterialItem.Plant,
					BrandTH = model.PromotionMaterialItem.BrandTH,
					BrandEN = model.PromotionMaterialItem.BrandEN,
					NameTH = model.PromotionMaterialItem.NameTH,
					NameEN = model.PromotionMaterialItem.NameEN,
					MaterialCode = model.PromotionMaterialItem.MaterialCode,
					Price = model.PromotionMaterialItem.Price,
					Unit = model.PromotionMaterialItem.UnitTH,
					ExpireDate = model.PromotionMaterialItem.ExpireDate,
					Updated = model.PromotionMaterialItem.Updated,
					UpdatedBy = model.PromotionMaterialItem.UpdatedBy?.DisplayName
				};

				return result;
			}
			else
			{
				return null;
			}
		}

		public static PromotionMaterialDTO CreatePromotionMaterialItemFromModel(PromotionMaterialQueryResult model)
		{
			if (model != null)
			{
				var result = new PromotionMaterialDTO()
				{
					Id = model.PromotionMaterialItem.ID,
					AgreementNo = model.PromotionMaterialItem.AgreementNo,
					ItemNo = model.PromotionMaterialItem.ItemNo,
					Plant = model.PromotionMaterialItem.Plant,
					NameTH = model.PromotionMaterialItem.NameTH,
					NameEN = model.PromotionMaterialItem.NameEN,
					MaterialCode = model.PromotionMaterialItem.MaterialCode,
					Price = model.PromotionMaterialItem.Price,				
					Unit = model.PromotionMaterialItem.UnitTH,
					ExpireDate = model.PromotionMaterialItem.ExpireDate,
					AgreementExpireDate = model.PromotionMaterialItem.AgreementExpireDate,
					BasePrice = model.PromotionMaterialItem.BasePrice,
					SAPSaleTaxCode = model.PromotionMaterialItem.SAPSaleTaxCode,
					SAPDeleteIndicator = model.PromotionMaterialItem.SAPDeleteIndicator,
					MaterialGroupKey = model.PromotionMaterialItem.MaterialGroupKey,
					SAPVarKey = model.PromotionMaterialItem.SAPVarKey,
					MaterialItemStatusMasterCenterID = model.PromotionMaterialItem.MaterialItemStatusMasterCenterID,
					PromotionMaterialPlant = model.PromotionMaterialItem.PromotionMaterial?.Plant,
					PromotionMaterialCode = model.PromotionMaterialItem.PromotionMaterial?.Code,
					PromotionMaterialGroup = model.PromotionMaterialItem.PromotionMaterial?.MaterialGroupKey,
					PromotionMaterialGLAAccount = model.PromotionMaterialItem.PromotionMaterial?.GLAccountNo,
					PromotionMaterialMaterial_MaterialGroupKey = model.PromotionMaterialItem.PromotionMaterial?.MaterialGroupKey,
					PromotionMaterialMaterialGroup_Key = model.PromotionMaterialItem.PromotionMaterial?.PromotionMaterialGroup?.Key,

				};

				return result;
			}
			else
			{
				return null;
			}
		}
		public static void SortBy(PromotionMaterialSortByParam sortByParam, ref IQueryable<PromotionMaterialQueryResult> query)
		{
			if (sortByParam.SortBy != null)
			{
				switch (sortByParam.SortBy.Value)
				{
					case PromotionMaterialSortBy.AgreementNo:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.AgreementNo);
						break;
					case PromotionMaterialSortBy.ItemNo:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.ItemNo);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.ItemNo);
						break;
					case PromotionMaterialSortBy.Plant:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.Plant);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.Plant);
						break;
					case PromotionMaterialSortBy.NameTH:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.NameTH);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.NameTH);
						break;
					case PromotionMaterialSortBy.NameEN:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.NameEN);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.NameEN);
						break;
					case PromotionMaterialSortBy.MaterialCode:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.MaterialCode);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.MaterialCode);
						break;
					case PromotionMaterialSortBy.Price:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.Price);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.Price);
						break;
					case PromotionMaterialSortBy.Unit:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.UnitTH);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.UnitTH);
						break;
					case PromotionMaterialSortBy.ExpireDate:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.ExpireDate);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.ExpireDate);
						break;
					case PromotionMaterialSortBy.Updated:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.PromotionMaterialItem.Updated);
						else query = query.OrderByDescending(o => o.PromotionMaterialItem.Updated);
						break;
					case PromotionMaterialSortBy.UpdatedBy:
						if (sortByParam.Ascending) query = query.OrderBy(o => o.UpdatedBy.DisplayName);
						else query = query.OrderByDescending(o => o.UpdatedBy.DisplayName);
						break;
					default:
						query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
						break;
				}
			}
			else
			{
				query = query.OrderBy(o => o.PromotionMaterialItem.AgreementNo);
			}
		}
		public void ToMasterSalePromotionItemModel(ref MasterSalePromotionItem model)
		{
			model.PromotionMaterialItemID = this.Id;
			model.NameTH = this.NameTH;
			model.NameEN = this.NameEN;
			model.Quantity = 1;
			model.UnitTH = this.Unit;
			model.PricePerUnit = (int)Math.Ceiling(this.Price);
			model.TotalPrice = (int)Math.Ceiling(this.Price);
			model.ReceiveDays = 45;

			//IsPurchasing
			if (this.AgreementNo.ToLower().Contains("welcomehome"))
			{
				model.IsPurchasing = false;
			}
			else if (this.NameTH.Trim().Replace(" ", "").ToLower().Contains("welcomehome")
					|| this.NameTH.Trim().Replace(" ", "").ToLower().Contains("secretdeal")
					|| this.NameTH.Trim().Replace(" ", "").ToLower().Contains("commonfee"))
			{
				model.IsPurchasing = false;
			}
			else
			{
				model.IsPurchasing = true;
			}

			model.IsShowInContract = true;
			model.ExpireDate = this.ExpireDate;
			model.IsUsed = false;
		}
		public void ToMasterTransferPromotionItemModel(ref MasterTransferPromotionItem model)
		{
			model.PromotionMaterialItemID = this.Id;
			model.NameTH = this.NameTH;
			model.NameEN = this.NameEN;
			model.Quantity = 1;
			model.UnitTH = this.Unit;
			model.PricePerUnit = (int)Math.Ceiling(this.Price);
			model.TotalPrice = (int)Math.Ceiling(this.Price);
			model.ReceiveDays = 45;

			//IsPurchasing
			if (this.AgreementNo.ToLower().Contains("welcomehome"))
			{
				model.IsPurchasing = false;
			}
			else if (this.NameTH.Trim().Replace(" ", "").ToLower().Contains("welcomehome")
					|| this.NameTH.Trim().Replace(" ", "").ToLower().Contains("secretdeal")
					|| this.NameTH.Trim().Replace(" ", "").ToLower().Contains("commonfee"))
			{
				model.IsPurchasing = false;
			}
			else
			{
				model.IsPurchasing = true;
			}

			model.IsShowInContract = true;
			model.ExpireDate = this.ExpireDate;
			model.IsUsed = false;
		}
		public void ToMasterPreSalePromotionItemModel(ref MasterPreSalePromotionItem model)
		{
			model.PromotionMaterialItemID = this.Id;
			model.NameTH = this.NameTH;
			model.NameEN = this.NameEN;
			model.Quantity = 1;
			model.UnitTH = this.Unit;
			model.PricePerUnit = (int)Math.Ceiling(this.Price);
			model.TotalPrice = (int)Math.Ceiling(this.Price);
			model.ReceiveDays = 45;

			//IsPurchasing
			if (this.AgreementNo.ToLower().Contains("welcomehome"))
			{
				model.IsPurchasing = false;
			}
			else if (this.NameTH.Trim().Replace(" ", "").ToLower().Contains("welcomehome")
					|| this.NameTH.Trim().Replace(" ", "").ToLower().Contains("secretdeal")
					|| this.NameTH.Trim().Replace(" ", "").ToLower().Contains("commonfee"))
			{
				model.IsPurchasing = false;
			}
			else
			{
				model.IsPurchasing = true;
			}

			model.IsShowInContract = true;
			model.ExpireDate = this.ExpireDate;
			model.IsUsed = false;
		}
		public void ToMasterSalePromotionCreditCardItemModel(ref MasterSalePromotionCreditCardItem model)
		{
			//model.BankID = this.Bank?.Id;
			//model.EDCFeeID = this.Id;
			model.Quantity = 1;
			model.UnitTH = this.UnitTH;
			model.UnitEN = this.UnitEN;
			model.Fee = Convert.ToDouble(this.Price);
			model.TotalPrice = Convert.ToDecimal(Convert.ToDouble(this.Price));

			model.NameTH = this.NameTH;
			model.NameEN = this.NameEN;
			model.BankName = this.BrandTH;
		}
		public void ToMasterTransferPromotionCreditCardItemModel(ref MasterTransferPromotionCreditCardItem model)
		{
			//model.BankID = this.Bank?.Id;
			//model.EDCFeeID = this.Id;
			model.Quantity = 1;
			model.UnitTH = "บาท";
			model.UnitEN = "บาท";
			model.Fee = Convert.ToDouble(this.Price);
			model.TotalPrice = Convert.ToDecimal(Convert.ToDouble(this.Price));

			model.NameTH = this.NameTH;
			model.NameEN = this.NameEN;
			model.BankName = this.BrandTH;
		}
	}
	public class PromotionMaterialQueryResult
	{
		public PromotionMaterialItem PromotionMaterialItem { get; set; }
		public PromotionMaterialGroup PromotionMaterialGroup { get; set; }
		public PromotionMaterial PromotionMaterial { get; set; }
		public User UpdatedBy { get; set; }
	}
}
