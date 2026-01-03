using Base.DTOs.FIN;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.PRM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
    public class TransferPriceListDTO
    {
        /// <summary>
        /// ราคาบ้านในสัญญา
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// ส่วนลด ณ​ วันโอน
        /// </summary>
        public decimal TransferDiscount { get; set; }
        /// <summary>
        /// ค่าเนื้อที่เพิ่ม-ลด
        /// </summary>
        public decimal IncreasingAreaPrice { get; set; }
        /// <summary>
        /// ราคาบ้านสุทธิ
        /// </summary>
        public decimal NetSellingPrice { get; set; }
        /// <summary>
        /// รวมเงินที่ชำระมาแล้ว
        /// </summary>
        public decimal TotalPaidAmount { get; set; }
        /// <summary>
        /// มี FreeDown หรือไม่
        /// </summary>
        public bool IsFreeDown { get; set; }
        /// <summary>
        /// ส่วนลด FreeDown
        /// </summary>
        public decimal FreeDownDiscount { get; set; }
        /// <summary>
        /// ค่าส่วนกลาง
        /// </summary>
        public decimal CommonFeeCharge { get; set; }
        /// <summary>
        /// ค่ามิเตอร์
        /// </summary>
        public decimal MeterAmount { get; set; }
        /// <summary>
        /// ค่่าจ่ายทีดิน
        /// </summary>
        public decimal LandAmount { get; set; }
        /// <summary>
        /// ค่าส่วนกลาง(ของบริษัทจ่าย)
        /// </summary>
        public decimal SellerCommonFeeCharge { get; set; }
        /// <summary>
        /// ค่ามิเตอร์(ของบริษัทจ่าย)
        /// </summary>
        public decimal SellerMeterAmount { get; set; }
        /// <summary>
        /// ค่่าจ่ายทีดิน(ของบริษัทจ่าย)
        /// </summary>
        public decimal SellerLandAmount { get; set; }
        /// <summary>
        /// รวมเงินที่เก็บจากลูกค้า
        /// </summary>
        public decimal TotalCustomerPayAmount { get; set; }
        /// <summary>
        /// ค่าใช้จ่ายที่เก็บจากลูกค้า
        /// </summary>
        public decimal CustomerPayAmount { get; set; }
        /// <summary>
        /// ค่าใช้จ่ายที่ไม่เก็บจากลูกค้า
        /// </summary>
        public decimal CustomerNoPayAmount { get; set; }
        /// <summary>
        /// ยอดจดจำนองจาก
        /// </summary>
        public decimal FromMortgageFee { get; set; }
        /// <summary>
        /// ยอดจดจำนองเป็น
        /// </summary>
        public decimal ToMortgageFee { get; set; }
        /// <summary>
        /// ยอดจดจำนอง
        /// </summary>
        public decimal MortgageFee { get; set; }

        /// <summary>
        /// ค่าดำเนินการเอกสาร
        /// </summary>
        [Description("ค่าดำเนินการเอกสาร")]
        public decimal? DocumentFeeCharge { get; set; }

        /// <summary>
        /// ค่าดำเนินการเอกสาร (ของบริษัทจ่าย)
        /// </summary>
        [Description("ค่าดำเนินการเอกสาร (ของบริษัทจ่าย)")]
        public decimal? SellerDocumentFeeCharge { get; set; }


        public async static Task<TransferPriceListDTO> CreateFromModelAsync(Guid bookingID, DatabaseContext DB)
        {

            var result = new TransferPriceListDTO();

            var model = await DB.Transfers
                        .Include(o => o.Agreement)
                        .Include(o => o.Agreement.Booking).Where(o => o.Agreement.BookingID == bookingID)
                        .Include(o => o.Unit)
                        .Include(o => o.Unit.TitledeedDetails)
                        .Include(o => o.Project)
                        .Include(o => o.Project.ProductType)
                        .FirstOrDefaultAsync();

            var ProductType = (model.Project?.ProductType?.Key ?? "");

            //พื้นที่ขาย
            double SaleArea = model.Agreement.Booking.SaleArea ?? 0;

            var unitPriceModel = await DB.UnitPrices
                .Include(o => o.Booking)
                .ThenInclude(o => o.ReferContact)
                .Include(o => o.UnitPriceStage)
                .Where(o => o.BookingID == bookingID
                            && o.IsActive == true
                            && o.UnitPriceStage.Key == UnitPriceStageKeys.Transfer
                        ).FirstOrDefaultAsync();

            if (unitPriceModel != null)
            {
                //ราคาบ้านในสัญญา
                decimal SellingPrice = unitPriceModel.AgreementPrice ?? 0;

                var transfer = await DB.Transfers
                            .Include(o => o.Agreement)
                            .Where(o => o.Agreement.BookingID == bookingID).FirstOrDefaultAsync();

                transfer = transfer ?? new Database.Models.SAL.Transfer();

                result.CommonFeeCharge = transfer.CommonFeeCharge;
                result.CustomerNoPayAmount = (transfer.CustomerNoPayAmount ?? 0);
                result.CustomerPayAmount = (transfer.CustomerPayAmount ?? 0);
                result.LandAmount = transfer.LandAmountCharge;
                result.MeterAmount = transfer.MeterAmountCharge;
                result.TotalCustomerPayAmount = (transfer.TotalCustomerPayAmount ?? 0);
                result.TotalPaidAmount = transfer.TotalPaidAmount;

                var transferFeeList = await DB.TransferExpenses
                                            .Include(o => o.MasterPriceItem)
                                            .Include(o => o.ExpenseReponsibleBy)
                                            .Include(o => o.PaymentReceiver)
                                            .Where(o => o.TransferID == model.ID)
                                            .ToListAsync() ?? new List<Database.Models.SAL.TransferExpense>();

                decimal SellerCommonFeeCharge = 0;
                if (ProductType == ProductTypeKeys.LowRise)
                {
                    SellerCommonFeeCharge = transferFeeList.Where(o =>
                            o.MasterPriceItem.Key == MasterPriceItemKeys.CommonFeeCharge
                        ).Select(o => o.SellerAmount).Sum();
                }
                else if (ProductType == ProductTypeKeys.HighRise)
                {
                    SellerCommonFeeCharge = transferFeeList.Where(o =>
                            o.MasterPriceItem.Key == MasterPriceItemKeys.CommonFeeCharge
                            || o.MasterPriceItem.Key == MasterPriceItemKeys.FirstSinkingFund
                        ).Select(o => o.SellerAmount).Sum();
                }
                result.SellerCommonFeeCharge = SellerCommonFeeCharge;

                decimal SellerMeterAmount = 0;
                SellerMeterAmount = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.ElectricMeter
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.WaterMeter
                    ).Select(o => o.SellerAmount).Sum();
                result.SellerMeterAmount = SellerMeterAmount;

                decimal SellerLandAmount = 0;
                SellerLandAmount = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.TransferFee
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                    ).Select(o => o.SellerAmount).Sum();
                result.SellerLandAmount = SellerLandAmount;

                result.FreeDownDiscount = unitPriceModel.FreedownDiscount ?? 0;
                result.TransferDiscount = unitPriceModel.TransferDiscount ?? 0;
                result.SellingPrice = SellingPrice;

                #region ราคาพื้นที่ต่อหน่วย

                //ราคาพื้นที่ต่อหน่วย
                decimal AreaPricePerUnit = 0;

                //ข้อมูลพื้นที่โฉนด
                var TitledeedDetail = model.Unit.TitledeedDetails
                                    .FirstOrDefault() ?? new Database.Models.PRJ.TitledeedDetail();

                //พื้นที่โฉนด
                decimal TitledeedArea = 0; decimal.TryParse((TitledeedDetail.TitledeedArea ?? 0).ToString(), out TitledeedArea);

                //แนวราบ
                if (ProductType == ProductTypeKeys.LowRise)
                {
                    var PriceListItem = await DB.PriceListItems
                        .Include(o => o.PriceList)
                        .Include(o => o.MasterPriceItem)
                        .Where(o =>
                            o.PriceList.UnitID == model.UnitID
                            && o.MasterPriceItem.Key == MasterPriceItemKeys.ExtraAreaPrice
                        )
                        .OrderByDescending(o => o.PriceList.ActiveDate)
                        .FirstOrDefaultAsync() ?? new Database.Models.PRJ.PriceListItem();

                    //Master Data > Price List
                    AreaPricePerUnit = (PriceListItem.PricePerUnitAmount ?? 0);
                }
                //แนวสูง
                else if (ProductType == ProductTypeKeys.HighRise)
                {
                    AreaPricePerUnit = (SellingPrice / (decimal)(SaleArea > 0 ? SaleArea : 1));
                }

                #endregion

                //พื้นที่ส่วนต่าง
                decimal AddOnArea = 0;
                if (TitledeedArea > 0 && SaleArea > 0)
                {
                    AddOnArea = TitledeedArea - decimal.Parse(SaleArea.ToString() ?? "0");
                }

                decimal IncreasingAreaPrice = 0;
                IncreasingAreaPrice = Math.Ceiling(AddOnArea * AreaPricePerUnit);
                result.IncreasingAreaPrice = IncreasingAreaPrice;

                decimal NetSellingPrice = 0;
                NetSellingPrice = Math.Ceiling(((result.SellingPrice - result.TransferDiscount) + result.IncreasingAreaPrice));
                result.NetSellingPrice = NetSellingPrice;

                decimal DocumentFeeCharge = 0;
                DocumentFeeCharge = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.DocumentFeeCharge
                    ).Select(o => o.BuyerAmount).Sum();
                result.DocumentFeeCharge = DocumentFeeCharge;

                decimal SellerDocumentFeeCharge = 0;
                SellerDocumentFeeCharge = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.DocumentFeeCharge
                    ).Select(o => o.SellerAmount).Sum();
                result.SellerDocumentFeeCharge = SellerDocumentFeeCharge;

                var LoanAmount = await DB.CreditBankings
                                .Include(o => o.LoanStatus)
                                .Where(o => o.BookingID == bookingID
                                            && (o.IsUseBank.HasValue && o.IsUseBank.Value)
                                            && o.LoanStatus.Key == "1")
                                .Select(o => o.ApprovedAmount).FirstOrDefaultAsync();

                if (result.SellingPrice > LoanAmount)
                {
                    result.FromMortgageFee = result.SellingPrice;
                    result.ToMortgageFee = LoanAmount;
                }
                else
                {
                    result.FromMortgageFee = LoanAmount;
                    result.ToMortgageFee = LoanAmount;
                }


                return result;
            }
            else
            {
                return null;
            }

        }

        public async static Task<TransferPriceListDTO> CreateFromModelDrafAsync(Guid bookingID, DatabaseContext DB, List<TransferExpenseListDTO> transferFeeList)
        {

            var result = new TransferPriceListDTO();

            transferFeeList = transferFeeList ?? new List<TransferExpenseListDTO>();

            var agreement = await DB.Agreements
                        .Include(o => o.Booking)
                        .Include(o => o.Unit)
                        .Include(o => o.Unit.TitledeedDetails)
                        .Include(o => o.Project)
                        .Include(o => o.Project.ProductType)
                        .Where(o => o.BookingID == bookingID).FirstOrDefaultAsync();

            var transferPromotion = await DB.TransferPromotions
                        .Where(o =>
                                o.BookingID == bookingID
                                && o.IsApprove == true
                            )
                        .FirstOrDefaultAsync() ?? new TransferPromotion();

            var ProductType = (agreement.Project?.ProductType?.Key ?? "");

            //พื้นที่ขาย
            double SaleArea = agreement.Booking.SaleArea ?? 0;

            var unitPriceModel_TP = await DB.UnitPrices
                        .Include(o => o.Booking)
                        .ThenInclude(o => o.ReferContact)
                        .Include(o => o.UnitPriceStage)
                        .Where(o => o.BookingID == bookingID
                                && o.UnitPriceStage.Key == UnitPriceStageKeys.TransferPromotion
                            //&& o.IsActive == true
                            )
                        .OrderByDescending(o => o.Created)
                        .FirstOrDefaultAsync();

            var unitPriceModel_AG = await DB.UnitPrices
                        .Include(o => o.Booking)
                        .ThenInclude(o => o.ReferContact)
                        .Include(o => o.UnitPriceStage)
                        .Where(o => o.BookingID == bookingID
                                && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                            //&& o.IsActive == true
                            )
                        .OrderByDescending(o => o.Created)
                        .FirstOrDefaultAsync();

            var unitPriceModel = unitPriceModel_TP;
            if (unitPriceModel == null)
            {
                unitPriceModel = unitPriceModel_AG;
            }

            //ข้อมูลพื้นที่โฉนด
            var TitledeedDetail = agreement.Unit.TitledeedDetails
                                .FirstOrDefault() ?? new Database.Models.PRJ.TitledeedDetail();

            //พื้นที่โฉนด
            decimal TitledeedArea = 0; decimal.TryParse((TitledeedDetail.TitledeedArea ?? 0).ToString(), out TitledeedArea);

            //ราคาพื้นที่ต่อหน่วย
            decimal AreaPricePerUnit = 0;

            //ราคาบ้านในสัญญา
            decimal SellingPrice = 0;

            if (unitPriceModel != null)
            {

                SellingPrice = unitPriceModel.AgreementPrice ?? 0;

                decimal CommonFeeCharge = 0;
                if (ProductType == ProductTypeKeys.LowRise)
                {
                    CommonFeeCharge = transferFeeList.Where(o =>
                            o.MasterPriceItem.Key == MasterPriceItemKeys.CommonFeeCharge
                        ).Select(o => o.BuyerAmount).Sum();
                }
                else if (ProductType == ProductTypeKeys.HighRise)
                {
                    CommonFeeCharge = transferFeeList.Where(o =>
                            o.MasterPriceItem.Key == MasterPriceItemKeys.CommonFeeCharge
                            || o.MasterPriceItem.Key == MasterPriceItemKeys.FirstSinkingFund
                        ).Select(o => o.BuyerAmount).Sum();
                }
                result.CommonFeeCharge = CommonFeeCharge;

                decimal MeterAmount = 0;
                MeterAmount = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.ElectricMeter
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.WaterMeter
                    ).Select(o => o.BuyerAmount).Sum();
                result.MeterAmount = MeterAmount;

                decimal SellerCommonFeeCharge = 0;
                if (ProductType == ProductTypeKeys.LowRise)
                {
                    SellerCommonFeeCharge = transferFeeList.Where(o =>
                            o.MasterPriceItem.Key == MasterPriceItemKeys.CommonFeeCharge
                        ).Select(o => o.SellerAmount).Sum();
                }
                else if (ProductType == ProductTypeKeys.HighRise)
                {
                    SellerCommonFeeCharge = transferFeeList.Where(o =>
                            o.MasterPriceItem.Key == MasterPriceItemKeys.CommonFeeCharge
                            || o.MasterPriceItem.Key == MasterPriceItemKeys.FirstSinkingFund
                        ).Select(o => o.SellerAmount).Sum();
                }
                result.SellerCommonFeeCharge = SellerCommonFeeCharge;

                decimal SellerMeterAmount = 0;
                SellerMeterAmount = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.ElectricMeter
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.WaterMeter
                    ).Select(o => o.SellerAmount).Sum();
                result.SellerMeterAmount = SellerMeterAmount;

                decimal SellerLandAmount = 0;
                SellerLandAmount = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.TransferFee
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                    ).Select(o => o.SellerAmount).Sum();
                result.SellerLandAmount = SellerLandAmount;

                result.FreeDownDiscount = unitPriceModel.FreedownDiscount ?? 0;

                result.TransferDiscount = (unitPriceModel_AG?.TransferDiscount ?? 0) + (unitPriceModel_TP?.TransferDiscount ?? 0);

                result.SellingPrice = SellingPrice;

                #region ราคาพื้นที่ต่อหน่วย

                //แนวราบ
                if (ProductType == ProductTypeKeys.LowRise)
                {
                    var PriceListItem = await DB.PriceListItems
                        .Include(o => o.PriceList)
                        .Include(o => o.MasterPriceItem)
                        .Where(o =>
                            o.PriceList.UnitID == agreement.UnitID
                            && o.MasterPriceItem.Key == MasterPriceItemKeys.ExtraAreaPrice
                        )
                        .OrderByDescending(o => o.PriceList.ActiveDate)
                        .FirstOrDefaultAsync() ?? new Database.Models.PRJ.PriceListItem();

                    //Master Data > Price List
                    AreaPricePerUnit = (PriceListItem.PricePerUnitAmount ?? 0);
                }
                //แนวสูง
                else if (ProductType == ProductTypeKeys.HighRise)
                {
                    AreaPricePerUnit = (SellingPrice / (decimal)(SaleArea > 0 ? SaleArea : 1));
                }

                #endregion

                //พื้นที่ส่วนต่าง
                decimal AddOnArea = 0;
                if (TitledeedArea > 0 && SaleArea > 0)
                {
                    AddOnArea = TitledeedArea - decimal.Parse(SaleArea.ToString() ?? "0");
                }

                decimal IncreasingAreaPrice = 0;
                IncreasingAreaPrice = Math.Ceiling(AddOnArea * AreaPricePerUnit);
                result.IncreasingAreaPrice = IncreasingAreaPrice;

                decimal NetSellingPrice = 0;
                NetSellingPrice = Math.Ceiling(((result.SellingPrice - result.TransferDiscount) + result.IncreasingAreaPrice));
                result.NetSellingPrice = NetSellingPrice;

                decimal CustomerPayAmount = 0;
                CustomerPayAmount = transferFeeList.Select(o => o.BuyerAmount).Sum();
                result.CustomerPayAmount = CustomerPayAmount;

                decimal CustomerNoPayAmount = 0;
                CustomerNoPayAmount = transferFeeList.Select(o => o.SellerAmount).Sum();
                result.CustomerNoPayAmount = CustomerNoPayAmount;

                decimal LandAmount = 0;
                LandAmount = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFee
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.MortgageFeeOverLoan
                        || o.MasterPriceItem.Key == MasterPriceItemKeys.TransferFee
                    ).Select(o => o.BuyerAmount).Sum();
                result.LandAmount = LandAmount;

                decimal DocumentFeeCharge = 0;
                DocumentFeeCharge = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.DocumentFeeCharge
                    ).Select(o => o.BuyerAmount).Sum();
                result.DocumentFeeCharge = DocumentFeeCharge;

                decimal SellerDocumentFeeCharge = 0;
                SellerDocumentFeeCharge = transferFeeList.Where(o =>
                        o.MasterPriceItem.Key == MasterPriceItemKeys.DocumentFeeCharge
                    ).Select(o => o.SellerAmount).Sum();
                result.SellerDocumentFeeCharge = SellerDocumentFeeCharge;

                var listMasterPriceItemKeys = new List<string>();
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.BookingAmount);
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.ContractAmount);
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.InstallmentAmount);
                listMasterPriceItemKeys.Add(MasterPriceItemKeys.TransferAmount);

                var paymentIDs = await DB.Payments.Where(o => o.BookingID == bookingID && o.IsCancel == false).OrderBy(o => o.Created).Select(o => o.ID).ToListAsync();
                var sumPayAmount = await DB.PaymentItems
                                        .Include(o => o.Payment)
                                        .Include(o => o.MasterPriceItem)
                                        .Where(o =>
                                            paymentIDs.Contains(o.PaymentID)
                                            && o.Payment.IsCancel == false
                                            && listMasterPriceItemKeys.Contains(o.MasterPriceItem.Key)
                                         ).SumAsync(o => o.PayAmount);

                result.TotalPaidAmount = sumPayAmount;
                result.TotalCustomerPayAmount = Math.Ceiling((((result.NetSellingPrice - sumPayAmount) + result.CommonFeeCharge + result.MeterAmount + result.LandAmount + (result.DocumentFeeCharge ?? 0)) - result.FreeDownDiscount));

                var LoanAmount = await DB.CreditBankings
                                .Include(o => o.LoanStatus)
                                .Where(o => o.BookingID == bookingID
                                            && (o.IsUseBank.HasValue && o.IsUseBank.Value)
                                            && o.LoanStatus.Key == "1")
                                .Select(o => o.ApprovedAmount).FirstOrDefaultAsync();

                if (result.SellingPrice > LoanAmount)
                {
                    result.FromMortgageFee = result.SellingPrice;
                    result.ToMortgageFee = LoanAmount;
                }
                else
                {
                    result.FromMortgageFee = LoanAmount;
                    result.ToMortgageFee = LoanAmount;
                }

                return result;
            }
            else
            {
                return null;
            }

        }

    }
}
