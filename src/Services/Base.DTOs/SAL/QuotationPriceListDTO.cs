using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs.CTM;
using Database.Models;
using Database.Models.CTM;
using Database.Models.PRJ;
using Microsoft.EntityFrameworkCore;

namespace Base.DTOs.SAL
{
    public class QuotationPriceListDTO
    {
        /// <summary>
        /// ID ของ Price List
        /// </summary>
        public Guid? FromPriceListID { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
        /// <summary>
        /// โอนกรรมสิทธิ์ภายในวันที่
        /// </summary>
        public DateTime? TransferDateBefore { get; set; }
        /// <summary>
        /// ผู้แนะนำ
        /// </summary>
        public CTM.ContactListDTO ReferContact { get; set; }
        /// <summary>
        /// ราคาขาย
        /// </summary>
        public decimal SellingPrice { get; set; }
        /// <summary>
        /// ส่วนลดเงินสด
        /// </summary>
        public decimal CashDiscount { get; set; }
        /// <summary>
        /// ส่วนลด ณ​ วันโอน
        /// </summary>
        public decimal TransferDiscount { get; set; }
        /// <summary>
        /// มี FreeDown หรือไม่
        /// </summary>
        public bool IsFreeDown { get; set; }
        /// <summary>
        /// ส่วนลด FreeDown
        /// </summary>
        public decimal FreeDownDiscount { get; set; }
        /// <summary>
        /// ส่วนลด FGF (ในใบเสนอราคาจะไม่มี แต่จะมีค่าตอนที่ตั้งเรื่องย้ายแปลง)
        /// </summary>
        public decimal? FGFDiscount { get; set; }
        /// <summary>
        /// ราคาขายสุทธิ
        /// </summary>
        public decimal NetSellingPrice { get; set; }
        /// <summary>
        /// เงินจอง
        /// </summary>
        public decimal BookingAmount { get; set; }
        /// <summary>
        /// เงินสัญญา
        /// </summary>
        public decimal ContractAmount { get; set; }
        /// <summary>
        /// เงินดาวน์
        /// </summary>
        public decimal DownAmount { get; set; }
        /// <summary>
        /// เงินโอนกรรมสิทธิ์
        /// </summary>
        public decimal TransferAmount { get; set; }
        /// <summary>
        /// จำนวนผ่อนดาวน์รวม
        /// </summary>
        public int Installment { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์ปกติ
        /// </summary>
        public int NormalInstallment { get; set; }
        /// <summary>
        /// เงินงวดดาวน์ปกติ
        /// </summary>
        public decimal InstallmentAmount { get; set; }
        /// <summary>
        /// จำนวนงวดดาวน์พิเศษ
        /// </summary>
        public int SpecialInstallment { get; set; }
        /// <summary>
        /// งวดดาวน์พิเศษ
        /// </summary>
        public List<SpecialInstallmentDTO> SpecialInstallmentPeriods { get; set; }
        /// <summary>
        /// รวมมูลล่าโปรโมชั่น
        /// </summary>
        public decimal? TotalPromotionAmount { get; set; }
        /// <summary>
        /// รวมใช้ budgetPromotion
        /// </summary>
        public decimal? TotalBudgetPromotionAmount { get; set; }

        /// <summary>
        /// % เงินดาวน์
        /// </summary>
        public double? PercentDown { get; set; }

        /// <summary>
        /// วันที่โอนกรรมสิทธิ์จาก Floor
        /// </summary>
        public DateTime? DueTransferDate { get; set; }

        /// <summary>
        /// FGF Code
        /// </summary>
        public string FGFCode { get; set; }

        public async static Task<QuotationPriceListDTO> CreateDraftFromUnitAsync(Guid unitID, DatabaseContext db)
        {
            //var priceList = await db.PriceLists.GetActivePriceListAsync(unitID);

            var priceList = await db.GetActivePriceListAsync(unitID);

            var result = new QuotationPriceListDTO
            {
                SellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)?.Amount ?? 0,
                NetSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice)?.Amount ?? 0,
                BookingAmount = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount)?.Amount ?? 0,
                ContractAmount = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.ContractAmount)?.Amount ?? 0,
                DownAmount = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.Amount ?? 0
            };
            result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;
            result.Installment = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.Installment ?? 0;
            result.InstallmentAmount = decimal.Round(priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.InstallmentAmount ?? (decimal)0, 2, MidpointRounding.AwayFromZero);
            result.PercentDown = priceList.PriceListItems.Find(c => c.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.PriceUnitAmount == 0 ? null
            : priceList.PriceListItems.Find(c => c.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.PriceUnitAmount *100;
            if (result.PercentDown != null) result.PercentDown = (double)Math.Round(result.PercentDown.GetValueOrDefault(), 2);
            var specialDownInstallmentStrings = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.SpecialInstallments;
            var specialDownInstallmentAmountStrings = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.SpecialInstallmentAmounts;
            var specialDownInstallments = specialDownInstallmentStrings?.Split(',');
            var specialDownInstallmentAmounts = specialDownInstallmentAmountStrings?.Split(',');
            result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
            if (specialDownInstallmentAmountStrings != null && specialDownInstallmentStrings != "0")
            {
                for (int i = 0; i < specialDownInstallments.Length; i++)
                {
                    int periodNo;
                    if (int.TryParse(specialDownInstallments[i], out periodNo))
                    {
                        decimal amount = 0;
                        if (i < specialDownInstallmentAmounts.Length)
                        {
                            decimal.TryParse(specialDownInstallmentAmounts[i], out amount);
                        }

                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = periodNo,
                            Amount = amount
                        });
                    }
                }
            }

            result.SpecialInstallment = result.SpecialInstallmentPeriods.Count;
            result.NormalInstallment = result.Installment - result.SpecialInstallment;



            //result.ContractDate = DateTime.Today.AddDays(7);
            //result.TransferOwnershipDate = DateTime.Today.AddDays(37);
            //result.TransferDateBefore = DateTime.Today.AddDays(37);
            var project = await db.Units.Include(o => o.Project).Include(o => o.Project.ProductType).Where(o => o.ID == unitID).FirstOrDefaultAsync();

            if (project != null)
            {
                if (DateTime.Today < new DateTime(2023, 12, 01) && project?.Project?.ProjectNo == "AM0001")
                {
                    result.ContractDate = DateTime.Parse("2024-01-15");
                }
                else if (DateTime.Today <= new DateTime(2024, 05, 26) && project?.Project?.ProjectNo == "AP0001")
                {
                    result.ContractDate = DateTime.Parse("2024-07-15");
                }
                else if (DateTime.Today <= new DateTime(2024, 06, 30) && project?.Project?.ProjectNo == "40143")
                {
                    result.ContractDate = DateTime.Parse("2024-08-15");
                }
                else
                {
                    result.ContractDate = DateTime.Today.AddDays(7);
                }

                if (project.Project.ProductType.Key == "2")
                {
                    //var tfDate = await db.Floors.Where(o => o.ProjectID == project.ProjectID).Select(o => o.DueTransferDate).FirstOrDefaultAsync();

                    var tfDate = await db.Units.Include(o => o.Floor).Where(o => o.ID == unitID).Select(o => o.Floor.DueTransferDate).FirstOrDefaultAsync();
                    result.TransferOwnershipDate = tfDate ?? DateTime.Today.AddDays(37);
                    result.TransferDateBefore = tfDate ?? DateTime.Today.AddDays(37);
                }
                else
                {
                    result.TransferOwnershipDate = DateTime.Today.AddDays(37);
                    result.TransferDateBefore = DateTime.Today.AddDays(37);
                }

            }
            else
            {
                result.ContractDate = DateTime.Today.AddDays(7);
                result.TransferOwnershipDate = DateTime.Today.AddDays(37);
                result.TransferDateBefore = DateTime.Today.AddDays(37);
            }







            // result.TransferOwnershipDate = DateTime.Today.AddMonths(result.Installment + 1);
            // result.TransferDateBefore = DateTime.Today.AddMonths(result.Installment + 1);
            result.FromPriceListID = priceList.ID;
            result.CashDiscount = 0;
            result.TransferDiscount = 0;
            result.FGFDiscount = null;
            result.FreeDownDiscount = 0;


            return result;

        }



        public async static Task<QuotationPriceListDTO> CreateDraftFromUnitBookingEventAsync(Guid unitID, DatabaseContext db)
        {
            //var priceList = await db.PriceLists.GetActivePriceListAsync(unitID);

            var priceList = await db.GetActivePriceListAsync(unitID);

            var units = await db.Units.Where(o => o.ID == unitID).Include(o => o.Project).FirstOrDefaultAsync();

            var result = new QuotationPriceListDTO();
            result.SellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.SellPrice)?.Amount ?? 0;
            result.NetSellingPrice = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.NetSellPrice)?.Amount ?? 0;
            result.BookingAmount = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.BookingAmount)?.Amount ?? 0;
            result.ContractAmount = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.ContractAmount)?.Amount ?? 0;
            result.DownAmount = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.Amount ?? 0;
            result.TransferAmount = result.NetSellingPrice - result.BookingAmount - result.ContractAmount - result.DownAmount;
            result.Installment = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.Installment ?? 0;
            result.InstallmentAmount = decimal.Round(priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.InstallmentAmount ?? (decimal)0, 2, MidpointRounding.AwayFromZero);
            result.PercentDown = priceList.PriceListItems.Find(c => c.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.PriceUnitAmount == 0 ? null
            : priceList.PriceListItems.Find(c => c.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.PriceUnitAmount * 100;
            var specialDownInstallmentStrings = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.SpecialInstallments;
            var specialDownInstallmentAmountStrings = priceList.PriceListItems.Find(o => o.MasterPriceItemID == MasterPriceItemIDs.DownAmount)?.SpecialInstallmentAmounts;
            var specialDownInstallments = specialDownInstallmentStrings?.Split(',');
            var specialDownInstallmentAmounts = specialDownInstallmentAmountStrings?.Split(',');
            result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
            if (specialDownInstallmentAmountStrings != null && specialDownInstallmentStrings != "0")
            {
                for (int i = 0; i < specialDownInstallments.Length; i++)
                {
                    int periodNo;
                    if (int.TryParse(specialDownInstallments[i], out periodNo))
                    {
                        decimal amount = 0;
                        if (i < specialDownInstallmentAmounts.Length)
                        {
                            decimal.TryParse(specialDownInstallmentAmounts[i], out amount);
                        }

                        result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                        {
                            Period = periodNo,
                            Amount = amount
                        });
                    }
                }
            }

            result.SpecialInstallment = result.SpecialInstallmentPeriods.Count;
            result.NormalInstallment = result.Installment - result.SpecialInstallment;
            //17 or 18 
            if (units.Project.ProjectNo == "AI0001")
            {
                if ((DateTime.Now.Day == 16 || DateTime.Now.Day == 17 || DateTime.Now.Day == 18) && DateTime.Now.Month == 6 && DateTime.Now.Year == 2023)
                {
                    result.ContractDate = DateTime.Parse("2023-07-30");
                }
                else
                {
                    result.ContractDate = DateTime.Today.AddDays(7);
                }
            }
            if (units.Project.ProjectNo == "AP0001")
            {
                if (DateTime.Today <= new DateTime(2024, 05, 26))
                {
                    result.ContractDate = DateTime.Parse("2024-07-15");
                }
                else
                {
                    result.ContractDate = DateTime.Today.AddDays(7);
                }
            }
            if (units.Project.ProjectNo == "40143")
            {
                if (DateTime.Today <= new DateTime(2024, 06, 30))
                {
                    result.ContractDate = DateTime.Parse("2024-08-15");
                }
                else
                {
                    result.ContractDate = DateTime.Today.AddDays(7);
                }
            }
            else
            {
                result.ContractDate = DateTime.Today.AddDays(7);
            }


            result.TransferOwnershipDate = DateTime.Today.AddDays(37);
            result.TransferDateBefore = DateTime.Today.AddDays(37);
            // result.TransferOwnershipDate = DateTime.Today.AddMonths(result.Installment + 1);
            // result.TransferDateBefore = DateTime.Today.AddMonths(result.Installment + 1);
            result.FromPriceListID = priceList.ID;
            result.CashDiscount = 0;
            result.TransferDiscount = 0;
            result.FGFDiscount = null;
            result.FreeDownDiscount = 0;

            var unit = await db.Units.Include(o => o.Floor)
                        .Where(o => o.ID == unitID).FirstOrDefaultAsync();
            if (unit != null)
            {
                result.DueTransferDate = unit.Floor?.DueTransferDate;
            }

            return result;

        }

        public async static Task<QuotationPriceListDTO> CreateFromModelAsync(Guid quotationID, DatabaseContext db)
        {
            var unitPriceModel = await db.QuotationUnitPrices.Include(o => o.Quotation).Where(o => o.QuotationID == quotationID).FirstOrDefaultAsync();
            if (unitPriceModel != null)
            {
                //var unitPriceItemModel = await db.QuotationUnitPriceItems.Where(o => o.QuotationUnitPriceID == unitPriceModel.ID).ToListAsync();
                var newBooking = await db.Bookings.Where(o => o.QuotationID == quotationID).Include(o => o.ReferContact).FirstOrDefaultAsync();
                var result = new QuotationPriceListDTO();
                if (newBooking != null)
                {
                    if (newBooking.ReferContact != null)
                    {
                        var referContact = new ContactListDTO();
                        referContact.Id = newBooking.ReferContact.ID;
                        referContact.ContactNo = newBooking.ReferContact.ContactNo;
                        referContact.FirstNameTH = newBooking.ReferContact.FirstNameTH;
                        referContact.MiddleNameTH = newBooking.ReferContact.MiddleNameTH;
                        referContact.LastNameTH = newBooking.ReferContact.LastNameTH;
                        referContact.PhoneNumber = newBooking.ReferContact.PhoneNumber;
                        referContact.OpportunityCount = newBooking.ReferContact.OpportunityCount;
                        // referContact.LastOpportunityDate = newBooking.ReferContact.LastOpportunity.Updated;
                        referContact.CitizenIdentityNo = newBooking.ReferContact.CitizenIdentityNo;

                        result.ReferContact = referContact;
                    }
                }





                result.SellingPrice = unitPriceModel.SellingPrice.HasValue ? unitPriceModel.SellingPrice.Value : 0;
                result.NetSellingPrice = unitPriceModel.AgreementPrice.HasValue ? unitPriceModel.AgreementPrice.Value : 0;
                result.BookingAmount = unitPriceModel.BookingAmount.HasValue ? unitPriceModel.BookingAmount.Value : 0;
                result.ContractAmount = unitPriceModel.AgreementAmount.HasValue ? unitPriceModel.AgreementAmount.Value : 0;
                result.DownAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;
                result.TransferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                result.CashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                result.TransferAmount = unitPriceModel.TransferAmount.HasValue ? unitPriceModel.TransferAmount.Value : 0;
                result.Installment = unitPriceModel.Installment.HasValue ? unitPriceModel.Installment.Value : 0;
                result.InstallmentAmount = unitPriceModel.InstallmentAmount.HasValue ? unitPriceModel.InstallmentAmount.Value : 0;
                result.FGFDiscount = unitPriceModel.FGFDiscount.HasValue ? unitPriceModel.FGFDiscount.Value : 0;
                result.FreeDownDiscount = unitPriceModel.FreedownDiscount.HasValue ? unitPriceModel.FreedownDiscount.Value : 0;
                result.PercentDown = unitPriceModel.InstallmentPercent.HasValue ? unitPriceModel.InstallmentPercent.Value : 0;



                var specialDownInstallmentStrings = unitPriceModel.SpecialInstallments;
                var specialDownInstallmentAmountStrings = unitPriceModel.SpecialInstallmentAmounts;
                var specialDownInstallments = specialDownInstallmentStrings?.Split(',');
                var specialDownInstallmentAmounts = specialDownInstallmentAmountStrings?.Split(',');
                result.SpecialInstallmentPeriods = new List<SpecialInstallmentDTO>();
                if (specialDownInstallmentAmountStrings != null)
                {
                    for (int i = 0; i < specialDownInstallments.Length; i++)
                    {
                        int periodNo;
                        if (int.TryParse(specialDownInstallments[i], out periodNo))
                        {
                            decimal amount = 0;
                            if (i < specialDownInstallmentAmounts.Length)
                            {
                                decimal.TryParse(specialDownInstallmentAmounts[i], out amount);
                            }

                            result.SpecialInstallmentPeriods.Add(new SpecialInstallmentDTO()
                            {
                                Period = periodNo,
                                Amount = amount
                            });
                        }
                    }
                }

                result.SpecialInstallment = result.SpecialInstallmentPeriods.Count;
                result.NormalInstallment = result.Installment - result.SpecialInstallment;
                result.ContractDate = unitPriceModel.Quotation.ContractDate;
                result.TransferOwnershipDate = unitPriceModel.Quotation.TransferOwnershipDate;

                if (newBooking != null)
                {
                    result.FGFCode = newBooking.FGFCode;
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
