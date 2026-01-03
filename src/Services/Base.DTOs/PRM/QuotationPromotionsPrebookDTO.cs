using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.SAL;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.PRM
{

    public class QuotationPromotionsPrebookDTO
    {

        public List<QuotationSalePromotionPrebookItemsDTO> SaleItems { get; set; }
        public List<QuotationTransferPromotionPrebookItemsDTO> TransferItems { get; set; }
        public List<QuotationSalePromotionExpensePrebookItemDTO> SaleExpenseItems { get; set; }

        public async static Task<QuotationPromotionsPrebookDTO> CreateFromQuatationAsync(Guid quatationID, DatabaseContext db)
        {
            var result = new QuotationPromotionsPrebookDTO();
            if (quatationID != null)
            {
                var quotationSalePromotionPrebooks  = await db.QuotationSalePromotionPrebooks.Where(o => o.QuotationID == quatationID).ToListAsync();
                var quotationTransferPromotionPrebooks = await db.QuotationTransferPromotionPrebooks.Where(o => o.QuotationID == quatationID).ToListAsync();

                var quotationSalePromotionExpensePrebooksOld = await db.QuotationSalePromotionExpensePrebooks.Where(o => o.QuotationID == quatationID && o.MasterPriceItemID != null)
                                                                    .Include(o => o.MasterPriceItem)
                                                                    .Include(o => o.ExpenseReponsibleBy).ToListAsync();

                var quotationSalePromotionExpensePrebooksNew = await db.QuotationSalePromotionExpensePrebooks.Where(o => o.QuotationID == quatationID && o.MasterPriceItemID == null)
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

                    result.SaleItems = result.SaleItems.OrderByDescending(o => o.SeqNo).ToList();
                }
                else
                {
                    result.SaleItems = new List<QuotationSalePromotionPrebookItemsDTO>();
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
                else
                {
                    result.TransferItems = new List<QuotationTransferPromotionPrebookItemsDTO>();
                }

                result.SaleExpenseItems = new List<QuotationSalePromotionExpensePrebookItemDTO>();

                if (quotationSalePromotionExpensePrebooksOld.Count > 0)
                {
                    var unitPrice = await db.QuotationUnitPrices.Where(o => o.QuotationID == quatationID).Select(o => o.ID).FirstAsync();
                    var results = quotationSalePromotionExpensePrebooksOld.Select(o => QuotationPromotionExpensePreBookDTO.CreateFromPreBookModel(o, unitPrice, db)).FirstOrDefault();

                    //result.SaleExpenseItems.Add(results);
                    ////ส่วน FreeText
                    //foreach (var item in quotationSalePromotionExpensePrebooksNew)
                    //{
                    //    var quotationPrebook = new QuotationSalePromotionExpensePrebookItemDTO();
                    //    quotationPrebook.Id = item.ID;
                    //    quotationPrebook.QuotationID = item.QuotationID;
                    //    quotationPrebook.ExpenseReponsibleByMasterCenterID = item.ExpenseReponsibleBy.Id; //Column1 ผู้รับผิดชอบค่าฝช้จ่าย
                    //    quotationPrebook.PriceName = item.PriceName;  //รายการ
                    //    quotationPrebook.PriceUnitAmount = item.PriceUnitAmount; //จำนวน
                    //    quotationPrebook.UnitName = item.UnitName; //หน่วย
                    //    quotationPrebook.Amount = item.Amount; //ราคา/หน่วย(บาท)
                    //    quotationPrebook.BuyerAmount = item.BuyerAmount;  //ผู้ค้าจ่าย
                    //    quotationPrebook.SellerAmount = item.SellerAmount;   //บริษัทจ่าย

                    //    result.SaleExpenseItems.Add(quotationPrebook);
                    //}
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
