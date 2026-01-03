using Base.DTOs.PRM;
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
namespace Base.DTOs.EXT
{
    public class ChangeUnitAgreementDetailDTO
    {

        public Guid? ChangeUnitID { get; set; }
        public string project { get; set; }
        public string BookingNo { get; set; }
        public string AgreementNo { get; set; }
        public UnitDetailDTO UnitDetails { get; set; }
        public UnitprieDetailDTO UnitpriceDetails { get; set; }
        public Installment Installments { get; set; }
        public SpecialInstallments SpecialInstallmentdatas { get; set; }
        public SumPromotions Salepromotion { get; set; }
        public SumPresalePromotions PreSalepromotion { get; set; }
        public SumPromotionExpenses SalepromotionExprnse { get; set; }
        public decimal TotalReceivedAmount { get; set; }
        public string Created { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Updated { get; set; }
        public DateTime? UpdateDate { get; set; }





        public static ChangeUnitAgreementDetailDTO CreateFromModel(ChangeUnitWorkflow model, decimal totalReceivedAmount, SalePromotionDTO salePromotionDataOld, QuotationSalePromotionDTO salePromotionDatanew, List<SalePromotionExpenseDTO> salePromotionExpenseDataOld, List<QuotationPromotionExpenseDTO> salePromotionExpenseDataNew, DatabaseContext DB, BookingPreSalePromotionDTO presalepromotionDataold, PreSalePromotionDTO presalepromotionDatanew)
        {
            if (model != null)
            {
                var result = new ChangeUnitAgreementDetailDTO()
                {
                    ChangeUnitID = model.ID,
                    project = DB.Projects.Where(o => o.ID == model.FromAgreement.ProjectID).Select(o => o.ProjectNo + '-' + o.ProjectNameTH).FirstOrDefault(),
                    BookingNo = model.FromAgreement.Booking.BookingNo,
                    AgreementNo = model.FromAgreement.AgreementNo,
                    TotalReceivedAmount = totalReceivedAmount,
                    UnitDetails= UnitDetailDTO.CreateFromModel(model.FromAgreement.Unit,model.ToAgreement.Unit,DB),
                    UnitpriceDetails = UnitprieDetailDTO.CreateFromModel(model.FromAgreement,model.ToAgreement,DB),
                    Created=DB.Users.Where(o=>o.ID==model.CreatedByUserID).Select(o=>o.DisplayName).FirstOrDefault(),
                    CreatedDate= model.Created,
                    Updated= DB.Users.Where(o => o.ID == model.UpdatedByUserID).Select(o => o.DisplayName).FirstOrDefault(),
                    UpdateDate=model.Updated,
                };
                result.Installments = Installment.CreateFromModel(result.UnitpriceDetails.Oldinstallment, result.UnitpriceDetails.Newinstallment, DB);
                result.SpecialInstallmentdatas = SpecialInstallments.CreateFromModel(result.UnitpriceDetails.Oldinstallment.SpecialInstallmentPeriods, result.UnitpriceDetails.Newinstallment.SpecialInstallmentPeriods,DB);

                result.PreSalepromotion = new SumPresalePromotions();
                if (presalepromotionDataold != null)
                { result.PreSalepromotion.PreSalePromotionOld = PromotionItems2.CreateFromModelPresale(presalepromotionDataold, null, DB); }
                if (presalepromotionDatanew != null)
                { result.PreSalepromotion.PreSalePromotionNew = PromotionItems2.CreateFromModelPresale(null, presalepromotionDatanew, DB); }


                result.Salepromotion = new SumPromotions();
                result.Salepromotion.SalePromotionOld = PromotionItems2.CreateFromModel(salePromotionDataOld, null, DB);
                result.Salepromotion.SalePromotionNew = PromotionItems2.CreateFromModel(null, salePromotionDatanew, DB);
               
                result.SalepromotionExprnse = new SumPromotionExpenses();
                result.SalepromotionExprnse.SalePromotionExpenseOld = PromotionExpenses2.CreateFromModel(salePromotionExpenseDataOld, null, DB);
                result.SalepromotionExprnse.SalePromotionExpenseNew = PromotionExpenses2.CreateFromModel(null, salePromotionExpenseDataNew, DB);

                return result;
            }
            else
            {
                return null;
            }
        }

        public class UnitDetailDTO
        {
            public string UnitNoOld { get; set; }
            public string UnitNoNew { get; set; }

            public string HouseNoOld { get; set; }
            public string HouseNoNew { get; set; }

            public string TowerOld { get; set; }
            public string TowerNew { get; set; }

            public string ModelOld { get; set; }
            public string ModelNew { get; set; }


            public double? TitledeedOld { get; set; }
            public double? TitledeedNew { get; set; }

            public double? SaleOld { get; set; }
            public double? SaleNew { get; set; }

            public static UnitDetailDTO CreateFromModel(Unit oldunit, Unit newunit, DatabaseContext DB)
            {
                if (oldunit != null)
                {
                    var result = new UnitDetailDTO()
                    {
                        UnitNoOld = oldunit.UnitNo,
                        UnitNoNew = newunit.UnitNo,
                        HouseNoOld = oldunit.HouseNo,
                        HouseNoNew = newunit.HouseNo,
                        TowerOld = oldunit.Tower != null? oldunit.Tower.TowerCode+'/'+ oldunit.Floor.NameTH:"-",
                        TowerNew = newunit.Tower != null? newunit.Tower.TowerCode+'/'+ newunit.Floor.NameTH:"-",
                        ModelOld= oldunit.Model.NameTH,
                        ModelNew= newunit.Model.NameTH,
                        TitledeedOld = PRJ.TitleDeedDTO.CreateFromModel(oldunit.TitledeedDetails?.FirstOrDefault()).TitledeedArea,
                        TitledeedNew = PRJ.TitleDeedDTO.CreateFromModel(newunit.TitledeedDetails?.FirstOrDefault()).TitledeedArea,
                       
                    };

                    result.SaleOld = result.TitledeedOld != null ? result.TitledeedOld : oldunit.SaleArea;
                    result.SaleNew = result.TitledeedNew != null ? result.TitledeedNew : newunit.SaleArea;

                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public class UnitprieDetailDTO
        {
            public DateTime? ContractDateOld { get; set; }
            public DateTime? ContractDateNew { get; set; }
            public DateTime? TransferDateBeforeOld { get; set; }
            public DateTime? TransferDateBeforeNew { get; set; }

            public decimal SellingPriceOld { get; set; }
            public decimal SellingPriceNew { get; set; }
            public decimal CashDiscountOld { get; set; }
            public decimal CashDiscountNew { get; set; }
            public decimal TransferDiscountOld { get; set; }
            public decimal TransferDiscountNew { get; set; }
            public decimal NetSellingPriceOld { get; set; }
            public decimal NetSellingPriceNew { get; set; }
            public decimal BookingAmountOld { get; set; }
            public decimal BookingAmountNew { get; set; }
            public decimal ContractAmountOld { get; set; }
            public decimal ContractAmountNew { get; set; }
            public decimal FreeDownDiscountOld { get; set; }
            public decimal FreeDownDiscountNew { get; set; }
            public decimal FgfDiscountOld { get; set; }
            public decimal FgfDiscountNew { get; set; }

            public string ReferContactOld { get; set; }
            public string ReferContactNew { get; set; }
            public decimal? TotalPromotionAmountOld { get; set; }
            public decimal? TotalPromotionAmountNew { get; set; }
            public decimal? TotalBudgetPromotionAmountOld { get; set; }
            public decimal? TotalBudgetPromotionAmountNew { get; set; }
            /// <summary>
            /// ไม่ต้องใช้
            /// </summary>
            public AgreementPriceListDTO Oldinstallment { get; set; }
            /// <summary>
            /// ไม่ต้องใช้
            /// </summary>
            public QuotationPriceListDTO Newinstallment { get; set; }
            

            public  static UnitprieDetailDTO CreateFromModel(Agreement oldagreement, Agreement newagreement, DatabaseContext DB)
            {
                if (oldagreement != null)
                {
                    var datapricelist = AgreementPriceListDTO.CreateFromModelPriceListAsync(oldagreement.ID, DB).Result;
                    # region newdata
                    //
                    var newdatapricelist = QuotationPriceListDTO.CreateFromModelAsync(newagreement.Booking.QuotationID??new Guid(), DB).Result;
                    var newpresale = PreSalePromotionDTO.CreateFromUnitAsync(newagreement.Booking.UnitID, DB).Result;
                    decimal totalpresale = 0;
                    if (newpresale != null)
                    foreach (var i in newpresale.Items)
                    {
                        totalpresale += i.TotalPrice;
                    }

                    var model =  DB.QuotationSalePromotions
                          .Include(o => o.MasterPromotion)
                          .Include(o => o.UpdatedBy)
                          .Where(o => o.QuotationID == newagreement.Booking.QuotationID).FirstOrDefault();
                    var newsale =  QuotationSalePromotionDTO.CreateFromQuotationAsync(model, DB).Result;
                    decimal totalnewsale = 0;
                    if(newsale!=null)
                    { 
                        foreach (var i in newsale.Items)
                        {
                            if(i.IsSelected)
                                totalnewsale += i.Quantity*i.MasterTotalprice;
                        }
                    }
                    var promotion =  DB.QuotationSalePromotions.Where(o => o.QuotationID == newagreement.Booking.QuotationID).First();
                    var unitPrice =  DB.QuotationUnitPrices.Where(o => o.QuotationID == newagreement.Booking.QuotationID).Select(o => o.ID).First();

                    var models =  DB.QuotationSalePromotionExpenses
                        .Include(o => o.MasterPriceItem)
                        .Include(o => o.ExpenseReponsibleBy)
                        .Where(o => o.QuotationSalePromotionID == promotion.ID).ToList();
                    var newexpense = models.Select(o => QuotationPromotionExpenseDTO.CreateFromModel(o, unitPrice, DB)).ToList();
                    decimal totalnewExpense = 0;
                    if (newexpense != null)
                    {
                    foreach (var i in newexpense)
                    {

                        totalnewExpense += i.SellerPayAmount;
                    }
                    }
                    #endregion

                    var result = new UnitprieDetailDTO()
                    {
                        ContractDateOld = oldagreement.ContractDate,
                        ContractDateNew = newagreement.ContractDate,
                        TransferDateBeforeOld = DB.SalePromotions.Where(o => o.BookingID == oldagreement.BookingID).Select(o => o.TransferDateBefore).FirstOrDefault(),
                        TransferDateBeforeNew = DB.SalePromotions.Where(o => o.BookingID == newagreement.BookingID).Select(o => o.TransferDateBefore).FirstOrDefault(),
                        SellingPriceOld = datapricelist.SellingPrice,
                        SellingPriceNew = newdatapricelist.SellingPrice,

                        CashDiscountOld = datapricelist.CashDiscount,
                        CashDiscountNew = newdatapricelist.CashDiscount,

                        TransferDiscountOld = datapricelist.TransferDiscount,
                        TransferDiscountNew = newdatapricelist.TransferDiscount,

                        NetSellingPriceOld = datapricelist.NetSellingPrice,
                        NetSellingPriceNew = newdatapricelist.NetSellingPrice,

                        BookingAmountOld = datapricelist.BookingAmount,
                        BookingAmountNew = newdatapricelist.BookingAmount,

                        ContractAmountOld = datapricelist.ContractAmount,
                        ContractAmountNew = newdatapricelist.ContractAmount,

                        FreeDownDiscountOld = datapricelist.FreeDownDiscount,
                        FreeDownDiscountNew = newdatapricelist.FreeDownDiscount,

                        FgfDiscountOld = datapricelist.FGFDiscount,
                        FgfDiscountNew = newdatapricelist.FGFDiscount ?? 0,

                        ReferContactOld = datapricelist.ReferContact != null? (datapricelist.ReferContact.ContactTitleTH+" " + datapricelist.ReferContact.FirstNameTH+" " + (datapricelist.ReferContact.MiddleNameTH!=null? datapricelist.ReferContact.MiddleNameTH+" ":"")+datapricelist.ReferContact.LastNameTH):null,
                        ReferContactNew = newdatapricelist.ReferContact != null ? (newdatapricelist.ReferContact.ContactTitleTH + " " + newdatapricelist.ReferContact.FirstNameTH + " " + (newdatapricelist.ReferContact.MiddleNameTH != null ? newdatapricelist.ReferContact.MiddleNameTH + " " : "") + newdatapricelist.ReferContact.LastNameTH):null,

                        TotalBudgetPromotionAmountOld =datapricelist.TotalBudgetPromotionAmount,

                        TotalPromotionAmountOld = datapricelist.TotalPromotionAmount,

                        Oldinstallment = datapricelist,
                        Newinstallment = newdatapricelist

                        //HouseNoOld = oldunit.HouseNo,
                        //HouseNoNew = newunit.HouseNo,
                        //TowerOld = oldunit.Tower != null ? oldunit.Tower.TowerCode + '/' + oldunit.Floor.NameTH : "-",
                        //TowerNew = newunit.Tower != null ? oldunit.Tower.TowerCode + '/' + newunit.Floor.NameTH : "-",
                        //ModelOld = oldunit.Model.NameTH,
                        //ModelNew = newunit.Model.NameTH,
                        //TitledeedOld = PRJ.TitleDeedDTO.CreateFromModel(oldunit.TitledeedDetails?.FirstOrDefault()).TitledeedArea,
                        //TitledeedNew = PRJ.TitleDeedDTO.CreateFromModel(newunit.TitledeedDetails?.FirstOrDefault()).TitledeedArea,

                    };
                    result.TotalBudgetPromotionAmountNew = result.FgfDiscountNew + totalpresale + totalnewsale + totalnewExpense + result.FreeDownDiscountNew;
                    result.TotalPromotionAmountNew = result.TotalBudgetPromotionAmountNew + result.CashDiscountNew + result.TransferDiscountNew;
                    //result.SaleOld = result.TitledeedOld != null ? result.TitledeedOld : oldunit.SaleArea;
                    //result.SaleNew = result.TitledeedNew != null ? result.TitledeedNew : newunit.SaleArea;

                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public class Installment
        { 
        public decimal TotalinstallmentAmountOld { get; set; }
        public decimal TotalinstallmentAmountNew { get; set; }
        public int TotalinstallmentOld { get; set; }
        public int TotalinstallmentNew { get; set; }
        public int NormalInstallmentOld { get; set; }
        public int NormalInstallmentNew { get; set; }
        public decimal NormalInstallmentAmountOld { get; set; }
        public decimal NormaInstallmentAmountNew { get; set; }
        public int SpecialInstallmentOld { get; set; }
        public int SpecialInstallmentNew { get; set; }

            public static Installment CreateFromModel(AgreementPriceListDTO olddata, QuotationPriceListDTO newdata, DatabaseContext DB)
            {
                if (olddata != null|| newdata!=null)
                {
                    var result = new Installment()
                    {
                        NormalInstallmentOld= olddata.NormalInstallment,
                        NormalInstallmentNew= newdata.NormalInstallment,
                        NormalInstallmentAmountOld= olddata.InstallmentAmount,
                        NormaInstallmentAmountNew= newdata.InstallmentAmount,
                        SpecialInstallmentOld= olddata.SpecialInstallment,
                        SpecialInstallmentNew=newdata.SpecialInstallment
                    };
                    decimal totalspecialold = 0;
                    if (olddata.SpecialInstallmentPeriods != null)
                    {
                        foreach (var i in olddata.SpecialInstallmentPeriods)
                        {
                            totalspecialold += i.Amount;
                        }
                    }

                    result.TotalinstallmentAmountOld = (olddata.NormalInstallment * olddata.InstallmentAmount) + totalspecialold;
                    result.TotalinstallmentOld = result.NormalInstallmentOld + result.SpecialInstallmentOld;

                    decimal totalspecialnew = 0;
                    if (newdata.SpecialInstallmentPeriods != null)
                    {
                        foreach (var i in newdata.SpecialInstallmentPeriods)
                        {
                            totalspecialnew += i.Amount;
                        }
                    }

                    result.TotalinstallmentAmountNew = (newdata.NormalInstallment * newdata.InstallmentAmount) + totalspecialnew;
                    result.TotalinstallmentNew = result.NormalInstallmentNew + result.SpecialInstallmentNew;

                    return result;
                }
                else
                {
                    return null;
                }
            }

        }

        public class SpecialInstallments
        {
            public List<SpecialInstallment> InstallmentOld { get; set; }
            public List<SpecialInstallment> InstallmentNew { get; set; }

            public static SpecialInstallments CreateFromModel(List<SpecialInstallmentDTO> olddata, List<SpecialInstallmentDTO> newdata, DatabaseContext DB)
            {
                if (olddata != null || newdata != null)
                {
                    var result = new SpecialInstallments();
                    var olddatainstall = new List<SpecialInstallment>();
                    foreach (var i in olddata)
                    {
                        var data = new SpecialInstallment
                        {
                            Period=i.Period,
                            Amount=i.Amount
                        };
                        olddatainstall.Add(data);
                    }
                    var newdatainstall = new List<SpecialInstallment>();
                    foreach (var i in newdata)
                    {
                        var data = new SpecialInstallment
                        {
                            Period = i.Period,
                            Amount = i.Amount
                        };
                        newdatainstall.Add(data);
                    }
                    result.InstallmentOld = olddatainstall;
                    result.InstallmentNew = newdatainstall;

                    return result;
                }
                else
                {
                    return null;
                }
            }


        }

        public class SpecialInstallment
        {
            public int Period { get; set; }
            public decimal Amount { get; set; }
        }

        public class SumPromotions
        {
            public PromotionItems2 SalePromotionOld { get; set; }
            public PromotionItems2 SalePromotionNew { get; set; }
        }

        public class PromotionItems2
        {
            public string PromotionNo { get; set; }
            public List<PromotionItem2> PromotionItem { get; set; }
            public decimal? totalPromotion { get; set; }

            public static PromotionItems2 CreateFromModel(SalePromotionDTO tranferPromotionOld, QuotationSalePromotionDTO tranferPromotionNew, DatabaseContext DB)
            {
                var result = new PromotionItems2();

                if (tranferPromotionOld != null)
                {
                    var cleandata = tranferPromotionOld.Items.Where(o => o.IsSelected == true).ToList();
                    var promotion = new List<PromotionItem2>();
                    decimal total = 0;
                    result.PromotionNo = tranferPromotionOld.PromotionNo;
                    foreach (var i in cleandata)
                    {
                        var item = new PromotionItem2();
                        item.PromotionitemID = i.Id;
                        item.PromotionitemName = i.NameTH;
                        item.PromotionitemAmount = i.Quantity;
                        item.totalpromotion = i.TotalPrice;
                        item.PromotionitemUnit = i.UnitTH;
                        item.subitem = new List<PromotionItem2>();
                        total += i.TotalPrice;
                        if (i.SubItems != null)
                        {
                            foreach (var j in i.SubItems)
                            {
                                var item2 = new PromotionItem2();
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
                else
                {
                    var cleandata = tranferPromotionNew.Items.Where(o => o.IsSelected == true).ToList();
                    var promotion = new List<PromotionItem2>();
                    decimal total = 0;
                    result.PromotionNo = tranferPromotionNew.PromotionNo;
                    foreach (var i in cleandata)
                    {
                        var item = new PromotionItem2();
                        item.PromotionitemID = i.Id;
                        item.PromotionitemName = i.NameTH;
                        item.PromotionitemAmount = i.Quantity;
                        item.totalpromotion = i.TotalPrice;
                        item.PromotionitemUnit = i.UnitTH;
                        item.subitem = new List<PromotionItem2>();
                        total += i.TotalPrice;
                        if (i.SubItems != null)
                        {
                            foreach (var j in i.SubItems)
                            {
                                var item2 = new PromotionItem2();
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

            public static PromotionItems2 CreateFromModelPresale(BookingPreSalePromotionDTO presalePromotionOld, PreSalePromotionDTO presalePromotionNew, DatabaseContext DB)
            {
                var result = new PromotionItems2();

                if (presalePromotionOld != null)
                {
                   // var cleandata = tranferPromotionOld.Items.Where(o => o.IsSelected == true).ToList();
                    var promotion = new List<PromotionItem2>();
                    decimal total = 0;
                    result.PromotionNo = presalePromotionOld.PromotionNo;
                    foreach (var i in presalePromotionOld.Items)
                    {
                        var item = new PromotionItem2();
                        item.PromotionitemID = i.Id;
                        item.PromotionitemName = i.NameTH;
                        item.PromotionitemAmount = i.Quantity;
                        item.totalpromotion = i.TotalPrice;
                        item.PromotionitemUnit = i.UnitTH;
                        item.subitem = new List<PromotionItem2>();
                        total += i.TotalPrice;
                        //if (i.SubItems != null)
                        //{
                        //    foreach (var j in i.SubItems)
                        //    {
                        //        var item2 = new PromotionItem2();
                        //        item2.PromotionitemID = j.Id;
                        //        item2.PromotionitemName = j.NameTH;
                        //        item2.PromotionitemAmount = j.Quantity;
                        //        item2.totalpromotion = j.TotalPrice;
                        //        item2.PromotionitemUnit = j.UnitTH;
                        //        item.subitem.Add(item2);
                        //    }
                        //}


                        promotion.Add(item);
                    }
                    result.PromotionItem = promotion;
                    result.totalPromotion = total;
                    return result;
                }
                else
                {
                    //var cleandata = tranferPromotionNew.Items.Where(o => o.IsSelected == true).ToList();
                    var promotion = new List<PromotionItem2>();
                    decimal total = 0;
                    result.PromotionNo = presalePromotionNew.PromotionNo;
                    foreach (var i in presalePromotionNew.Items)
                    {
                        var item = new PromotionItem2();
                        item.PromotionitemID = i.Id;
                        item.PromotionitemName = i.NameTH;
                        item.PromotionitemAmount = i.Quantity;
                        item.totalpromotion = i.TotalPrice;
                        item.PromotionitemUnit = i.UnitTH;
                        item.subitem = new List<PromotionItem2>();
                        total += i.TotalPrice;
                        //if (i.SubItems != null)
                        //{
                        //    foreach (var j in i.SubItems)
                        //    {
                        //        var item2 = new PromotionItem2();
                        //        item2.PromotionitemID = j.Id;
                        //        item2.PromotionitemName = j.NameTH;
                        //        item2.PromotionitemAmount = j.Quantity;
                        //        item2.totalpromotion = j.TotalPrice;
                        //        item2.PromotionitemUnit = j.UnitTH;
                        //        item.subitem.Add(item2);
                        //    }
                        //}


                        promotion.Add(item);
                    }
                    result.PromotionItem = promotion;
                    result.totalPromotion = total;
                    return result;
                }
            }

        }

        public class PromotionItem2
        {
            public Guid? PromotionitemID { get; set; }
            public string PromotionitemName { get; set; }
            public int PromotionitemAmount { get; set; }
            public string PromotionitemUnit { get; set; }
            public decimal? totalpromotion { get; set; }
            public List<PromotionItem2> subitem { get; set; }

        }

        public class SumPromotionExpenses
        {
            public PromotionExpenses2 SalePromotionExpenseOld { get; set; }
            public PromotionExpenses2 SalePromotionExpenseNew { get; set; }
        }
        public class PromotionExpenses2
        {
            public List<PromotionExpense2> Promotion { get; set; }
            public decimal? TotalPromotionExpense { get; set; }

            public static PromotionExpenses2 CreateFromModel(List<SalePromotionExpenseDTO> tranferPromotionExpenseOld , List<QuotationPromotionExpenseDTO> tranferPromotionExpenseNew, DatabaseContext DB)
            {
                if (tranferPromotionExpenseOld != null)
                {
                    var masterExpenseReponsibleByAP = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == "0").Select(o => o.ID).FirstOrDefault();
                    var cleandata = tranferPromotionExpenseOld.Where(o => o.ExpenseReponsibleBy.Id == masterExpenseReponsibleByAP).ToList();
                    var Promotion = new List<PromotionExpense2>();
                    var result = new PromotionExpenses2();
                    decimal total = 0;
                    foreach (var i in cleandata)
                    {
                        var item = new PromotionExpense2();
                        item.PromotionExpenseName = i.Name;
                        item.PromotionExpenseAmount = i.PriceUnitAmount;
                        item.TotalPromotionExpense = i.SellerPayAmount;
                        item.PromotionExpenseUnit = i.PriceUnit.Name;
                        total += i.SellerPayAmount;
                        Promotion.Add(item);
                    }

                    result.Promotion = Promotion;
                    result.TotalPromotionExpense = total;

                    return result;
                }
                else
                {
                    var masterExpenseReponsibleByAP = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ExpenseReponsibleBy && o.Key == "0").Select(o => o.ID).FirstOrDefault();
                    var cleandata = tranferPromotionExpenseNew.Where(o => o.ExpenseReponsibleBy.Id == masterExpenseReponsibleByAP).ToList();
                    var Promotion = new List<PromotionExpense2>();
                    var result = new PromotionExpenses2();
                    decimal total = 0;
                    foreach (var i in cleandata)
                    {
                        var item = new PromotionExpense2();
                        item.PromotionExpenseName = i.Name;
                        item.PromotionExpenseAmount = i.PriceUnitAmount;
                        item.TotalPromotionExpense = i.SellerPayAmount;
                        item.PromotionExpenseUnit = i.PriceUnit.Name;
                        total += i.SellerPayAmount;
                        Promotion.Add(item);
                    }

                    result.Promotion = Promotion;
                    result.TotalPromotionExpense = total;

                    return result;
                }
               
            }

        }

        public class PromotionExpense2
        {
            public string PromotionExpenseName { get; set; }
            public double? PromotionExpenseAmount { get; set; }
            public decimal TotalPromotionExpense { get; set; }
            public string PromotionExpenseUnit { get; set; }


        }



        public class SumPresalePromotions
        {
            public PromotionItems2 PreSalePromotionOld { get; set; }
            public PromotionItems2 PreSalePromotionNew { get; set; }
        }
    }
}
