using Base.DTOs.SAL;
using Database.Models;
using Database.Models.DbQueries.EQN;
using Database.Models.MasterKeys;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
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

namespace Base.DTOs.EXT
{
	public class TranferPromotionDetailDTO
    {

        public Guid? ID { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectNameTH { get; set; }
        public string ProjectDisplayName { get; set; }
        public string UnitNo { get; set; }
        public string MainOwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Staus { get; set; }
        public DateTime? TransferPromotionDate { get; set; }
        public decimal? NetSellingPrice { get; set; }
        public string TransferPromotionNo { get; set; }
        public string AgreementNo { get; set; }
        public DateTime? TransferDateBefore { get; set; }
        public decimal? TransferDiscount { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalBudgetPromotion { get; set; }
        public PromotionItems ItemPromotion { get; set; }
        public PromotionExpenses ItemExpense { get; set; }

        public string Created { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Updated { get; set; }
        public DateTime? UpdatedDate { get; set; }


        public class PromotionItems
        { 
        public string PromotionNo { get; set; }
        public List<PromotionItem> PromotionItem { get; set; }
        public decimal? totalPromotion { get; set; }

            public static PromotionItems CreateFromModel(TransferPromotionDTO tranferPromotion, DatabaseContext DB)
            {
                var masterExpenseReponsibleByAP = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == "0").Select(o => o.ID);
                var cleandata = tranferPromotion.Items.Where(o => o.IsSelected == true).ToList();
                var result = new PromotionItems();
                var promotion = new List<PromotionItem>();
                decimal total = 0;
                result.PromotionNo = tranferPromotion.PromotionNo;
                foreach (var i in cleandata)
                {
                    var item = new PromotionItem();
                    item.PromotionitemID = i.Id;
                    item.PromotionitemName = i.NameTH;
                    item.PromotionitemAmount = i.Quantity;
                    item.totalpromotion = i.TotalPrice;
                    item.PromotionitemUnit = i.UnitTH;
                    item.subitem = new List<PromotionItem>();
                    total += i.TotalPrice;
                    if(i.SubItems!=null)
                     {
                        foreach (var j in i.SubItems)
                        {
                            var item2 = new PromotionItem();
                            item2.PromotionitemID = j.Id;
                            item2.PromotionitemName = j.NameTH;
                            item2.PromotionitemAmount = j.Quantity;
                            item2.totalpromotion = j.TotalPrice;
                            item2.PromotionitemUnit = j.UnitTH;
                            item.subitem.Add(item2);
                        }
                    }
                

                    promotion.Add(item);
                }
                result.PromotionItem = promotion;
                result.totalPromotion = total;
                return result;
            }

        }

        public class PromotionItem
        { 
            public Guid? PromotionitemID { get; set; }
            public string PromotionitemName { get; set; }
            public int PromotionitemAmount { get; set; }
            public string PromotionitemUnit { get; set; }
            public decimal? totalpromotion { get; set; }
            public List<PromotionItem> subitem { get; set; }

      

        }


        public class PromotionExpenses
        { 
         public List<PromotionExpense> Promotion { get; set; }
         public decimal? TotalPromotionExpense { get; set; }

            public static PromotionExpenses CreateFromModel( List<TransferPromotionExpenseDTO> tranferPromotionExpense, DatabaseContext DB)
            {
                var masterExpenseReponsibleByAP = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == "0").Select(o=>o.ID).FirstOrDefault();
                var cleandata = tranferPromotionExpense.Where(o => o.ExpenseReponsibleBy.Id == masterExpenseReponsibleByAP).ToList();
                var Promotion = new List<PromotionExpense>();
                var result = new PromotionExpenses();
                decimal total = 0 ;
                foreach (var i in cleandata)
                {
                    var item = new PromotionExpense();
                    item.PromotionExpenseName =i.Name;
                    if (i.PriceUnitAmount != null)
                    { 
                        item.PromotionExpenseAmount =i.PriceUnitAmount;
                    }
                        item.TotalPromotionExpense = i.SellerPayAmount;
                    if (i.PriceUnit != null)
                    { 
                        item.PromotionExpenseUnit = i.PriceUnit.Name;
                    }
                    total += i.SellerPayAmount;
                    Promotion.Add(item);
                }

                result.Promotion = Promotion;
                result.TotalPromotionExpense = total;

                return result;
            }

        }

        public class PromotionExpense
        { 
            public string PromotionExpenseName { get; set; }
            public double? PromotionExpenseAmount { get; set; }
            public decimal TotalPromotionExpense { get; set; }
            public string PromotionExpenseUnit { get; set; }


        }

        public static TranferPromotionDetailDTO CreateFromModel(TransferPromotionDTO tranferPromotion, List<TransferPromotionExpenseDTO> tranferPromotionExpense, DatabaseContext DB)
        {
            if (tranferPromotion != null)
            {
                var result = new TranferPromotionDetailDTO()
                {
                    ID = tranferPromotion.Id,
                    ProjectNo=tranferPromotion.Project.ProjectNo,
                    ProjectNameTH = tranferPromotion.Project.ProjectNameTH,
                    ProjectDisplayName = tranferPromotion.Project.ProjectNo +"-"+ tranferPromotion.Project.ProjectNameTH,
                    UnitNo = tranferPromotion.Unit.UnitNo,
                    MainOwnerName = tranferPromotion.AgreementOwner.FullnameTH,
                    PhoneNumber = "",
                    Staus = tranferPromotion.TransferPromotionStatus.Name,
                    TransferPromotionDate= tranferPromotion.TransferPromotionDate,
                    NetSellingPrice= tranferPromotion.NetSellingPrice,
                    TransferPromotionNo = tranferPromotion.TransferPromotionNo,
                    AgreementNo = tranferPromotion.AgreementOwner.Agreement.AgreementNo,
                    TransferDateBefore = tranferPromotion.TransferDateBefore,
                    TransferDiscount = tranferPromotion.TransferDiscount,
                    TotalAmount= tranferPromotion.TotalAmount,
                    TotalBudgetPromotion = tranferPromotion.BudgetAmount,
                    ItemPromotion= PromotionItems.CreateFromModel(tranferPromotion, DB),
                    ItemExpense= PromotionExpenses.CreateFromModel(tranferPromotionExpense, DB),
                    Created= tranferPromotion.CreatedBy,
                    CreatedDate= tranferPromotion.Created,
                    Updated= tranferPromotion.UpdatedBy,
                    UpdatedDate= tranferPromotion.Updated,

                    //ProjectNo = model.Booking.Project.ProjectNo,
                    //ProjectNameTH = model.Booking.Project.ProjectNameTH,
                    //ProjectDisplayName = model.Booking.Project.ProjectNo + "-" + model.Booking.Project.ProjectNameTH,
                    //UnitNo = model.Booking.Unit.UnitNo,
                    //HouseNo = model.Booking.Unit.HouseNo,
                    //MainOwnerName = DB.Agreements.Where(o => o.BookingID == model.Booking.ID).Select(o => o.MainOwnerName).FirstOrDefault(),
                    //TranferPromotionNo = model.TransferPromotionNo
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
