using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{

	public class QuotationPromotionsPrebookDTO
	{

		public List<QuotationSalePromotionPrebookItemsDTO> SaleItems { get; set; }
		public List<QuotationTransferPromotionPrebookItemsDTO> TransferItems { get; set; }
		public List<QuotationSalePromotionExpensePrebookItemDTO> SaleExpenseItems { get; set; }
		public Guid? PaymentPreBookId { get; set; }
		public string QuotationNo { get; set; }

		public async static Task<QuotationPromotionsPrebookDTO> CreateFromQuatationAsync(Guid quatationID, DatabaseContext db)
		{
			var result = new QuotationPromotionsPrebookDTO();
			if (quatationID != null)
			{
				var quotationSalePromotionPrebooks = await db.QuotationSalePromotionPrebooks.Where(o => o.QuotationID == quatationID).ToListAsync();
				var quotationTransferPromotionPrebooks = await db.QuotationTransferPromotionPrebooks.Where(o => o.QuotationID == quatationID).ToListAsync();

				var quotationSalePromotionExpensePrebooks = await db.QuotationSalePromotionExpensePrebooks.Where(o => o.QuotationID == quatationID)
																	.Include(o => o.MasterPriceItem)
																	.Include(o => o.ExpenseReponsibleBy).ToListAsync();


				if (quotationSalePromotionPrebooks.Count > 0)
				{
					result.SaleItems = new List<QuotationSalePromotionPrebookItemsDTO>();
					foreach (var item in quotationSalePromotionPrebooks)
					{
						var quotationPrebook = new QuotationSalePromotionPrebookItemsDTO();
						quotationPrebook.ID = item.ID;
						quotationPrebook.QuotationID = item.QuotationID;
						quotationPrebook.SeqNo = item.SeqNo;
						quotationPrebook.NameTH = item.PromotionDescription;
						quotationPrebook.Quantity = item.Quantity;

						result.SaleItems.Add(quotationPrebook);
					}

					result.SaleItems = result.SaleItems.OrderBy(o => o.SeqNo).ToList();
				}


				if (quotationTransferPromotionPrebooks.Count > 0)
				{
					result.TransferItems = new List<QuotationTransferPromotionPrebookItemsDTO>();
					foreach (var item in quotationTransferPromotionPrebooks)
					{
						var quotationPrebook = new QuotationTransferPromotionPrebookItemsDTO();
						quotationPrebook.ID = item.ID;
						quotationPrebook.QuotationID = item.QuotationID;
						quotationPrebook.UnitName = item.UnitName;
						quotationPrebook.PricePerUnit = item.PricePerUnit;
						quotationPrebook.TotalPrice = item.TotalPrice;
						quotationPrebook.Remark = item.Remark;

						result.TransferItems.Add(quotationPrebook);
					}
				}

				result.SaleExpenseItems = new List<QuotationSalePromotionExpensePrebookItemDTO>();
				if (quotationSalePromotionExpensePrebooks.Count > 0)
				{
					var unitPrice = await db.QuotationUnitPrices.Where(o => o.QuotationID == quatationID).Select(o => o.ID).FirstOrDefaultAsync();
					var results = quotationSalePromotionExpensePrebooks.Select(o => QuotationPromotionExpensePreBookDTO.CreateFromPreBookModel(o, unitPrice, db)).ToList();
					foreach (var itemResult in results)
					{
						result.SaleExpenseItems.Add(itemResult);
					}
					result.SaleExpenseItems = result.SaleExpenseItems.OrderBy(o => o.MasterPriceItemSeqNo).ToList();
				}

				return result;
			}
			else
			{
				return result;
			}
		}
	}
}