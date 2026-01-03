using Base.DTOs.PRM;
using Base.DTOs.CTM;
using Database.Models;
using Database.Models.PRJ;
using Database.Models.PRM;
using Database.Models.SAL;
using Database.Models.USR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.DTOs.USR;
using Base.DTOs.PRJ;
using Database.Models.MasterKeys;
using Database.Models.MST;
using ErrorHandling;
using System.ComponentModel;
using System.Reflection;
using static Base.DTOs.EXT.TranferPromotionDetailDTO;
using Minio.DataModel;

namespace Base.DTOs.SAL
{
    public class TransferPromotionDTO : BaseDTO
    {
        #region project other


        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDropdownDTO Project { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }
        #endregion

        #region Contact
        public AgreementOwnerDTO AgreementOwner { get; set; }

        #endregion

        #region TransferPromotion
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public string PromotionNo { get; set; }
        /// <summary>
        /// วันที่โปรโมชั่นโอน
        /// </summary>
        public DateTime? TransferPromotionDate { get; set; }
        /// <summary>
        /// เลขที่โปรโมชั่นโอน
        /// </summary>
        public string TransferPromotionNo { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }
        /// <summary>
        /// ผลรวมใช้ budget
        /// </summary>
        public decimal BudgetAmount { get; set; }
        /// <summary>
        /// ผลรวมโปรโมชั่น
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// กำหนดวันที่โอน
        /// </summary>
        public DateTime? TransferDateBefore { get; set; }
        /// <summary>
        /// รหัสโปรโมชั่น
        /// </summary>
        public MasterTransferPromotionDTO MasterTransferPromotion { get; set; }
        /// <summary>
        /// ส่วนลด ณ วันโอน
        /// </summary>
        public decimal TransferDiscount { get; set; }
        /// <summary>
        /// ฟรีค่าจดจำนองกรณีกู้เกิน
        /// </summary>
        public bool IsFreeMortgageCharge { get; set; }
        /// <summary>
        /// Min Price Workflow
        /// </summary>
        public MinPriceBudgetWorkflowDTO MinPriceBudgetWorkflow { get; set; }
        /// <summary>
        /// Min Price Approval Workflow
        /// </summary>
        public List<MinPriceBudgetApprovalDTO> MinPriceBudgetApprovalWorkflow { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price (กรณีติด Workflow)
        /// </summary>
        public MST.MasterCenterDropdownDTO MinPriceRequestReason { get; set; }
        /// <summary>
        /// เหตุผลขออนุมัติ Min Price อื่นๆ (กรณีติด Workflow)
        /// </summary>
        public string OtherMinPriceRequestReason { get; set; }
        /// <summary>
        /// ปลดล็อคส่วนลด ณ วันโอนมากกว่า  3%
        /// </summary>
        public bool IsUnlocked3PercentTransferDiscount { get; set; }
        /// <summary>
        /// ปลดล็อคส่วนลด ณ วันโอน
        /// </summary>
        public bool IsUnlockedTransferDiscount { get; set; }
        /// <summary>
        /// ราขาขายสุทธิ
        /// </summary>
        public decimal? NetSellingPrice { get; set; }
        /// <summary>
        /// สถานะอนุมติ
        /// </summary>
        public bool IsApprove { get; set; }
        /// <summary>
        /// วันที่อนุมติ
        /// </summary>
        public DateTime? ApproveDate { get; set; }

        /// <summary>
        /// จำนวนเดือนที่เก็บค่าส่วนกลาง
        /// </summary>
        public int? publicFundMonths { get; set; }

        /// <summary>
        /// ค่าจดจำนอง กรณีกู้เกิน
        /// </summary>
        public decimal? MortgageFee { get; set; }

        /// <summary>
        /// คืนเงิน ในกรณีกู้เกิน
        /// </summary>
        public decimal? OverMortgageFee { get; set; }
        #endregion

        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<TransferPromotionItemDTO> Items { get; set; }

        /// <summary>
        /// รายการโปรโมชั่น
        /// </summary>
        public List<MasterTransferPromotionItemDTO> MasterPromotionItems { get; set; }

        /// <summary>
        /// Master Budget Promotion
        /// </summary>
        public BudgetPromotionDTO BudgetPromotion { get; set; }

        /// <summary>
        /// check CreditBank is use bank
        /// </summary>
        public CreditBankingDTO CreditBanking { get; set; }


        /// <summary>
        /// check AgreementPriceList 
        /// </summary>
        public AgreementPriceListDTO AgreementPriceList { get; set; }

        /// <summary>
        /// check MinPrice
        /// </summary>
        public MinPriceDTO MinPrice { get; set; }

        /// <summary>
        /// สร้างโปรโมชั่นโอน
        /// </summary>
        public bool IsCreate { get; set; }

        /// <summary>
        /// ค่าจดจำนอง
        /// </summary>
        public decimal DiffMortgageFree { get; set; }

        /// <summary>
        /// โอนหน้าสัญญา
        /// </summary>
        public decimal TransferDiscountOfAgreement { get; set; }

        /// <summary>
        /// สถานะโปรโมชั่นโอน
        /// </summary>
        public MST.MasterCenterDropdownDTO TransferPromotionStatus { get; set; }

        /// <summary>
        /// โอนโปรแล้ว 
        /// </summary>
        public bool IsTransfer { get; set; }
        /// <summary>
        /// เชคจาก transfer ไม่สามารถแก้ไขขยายวันที่ได้ 
        /// </summary>
        public bool IsTransferConfirmed { get; set; }
        /// <summary>
        /// Created 
        /// </summary>
        public DateTime? Created { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Reject
        /// </summary>
        public bool? IsReject { get; set; }

        /// <summary>
        /// มีการเบิกโปร
        /// </summary>
        public bool IsTransferRequest { get; set; }


        public async static Task<TransferPromotionDTO> CreateFromModelAsync(TransferPromotion model, DatabaseContext db)
        {

            if (model != null)
            {

                model.Booking = db.Bookings
                    .Include(o => o.Project)
                    .Include(o => o.Unit)
                    .Where(o => o.ID == model.BookingID).FirstOrDefault();

                var result = new TransferPromotionDTO();

                result.Id = model.ID;
                result.PromotionNo = model.MasterPromotion?.PromotionNo;
                result.TransferPromotionDate = model.TransferPromotionDate;
                result.Remark = model.Remark;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy == null ? model.UpdatedBy?.DisplayName : model.UpdatedBy?.DisplayName;
                result.Project = ProjectDropdownDTO.CreateFromModel(model.Booking?.Project);
                result.Unit = UnitDTO.CreateFromModel(model.Booking?.Unit);
                result.Booking = await BookingDTO.CreateFromModelAsync(model.Booking, db);
                result.BudgetAmount = model.BudgetAmount;
                result.TotalAmount = model.TotalAmount;
                result.TransferDateBefore = model.TransferDateBefore;
                result.MasterTransferPromotion = MasterTransferPromotionDTO.CreateFromModel(model.MasterPromotion);
                result.TransferDiscount = model.TransferDiscount.HasValue ? model.TransferDiscount.Value : 0;
                result.IsFreeMortgageCharge = model.IsFreeMortgageCharge;
                result.TransferPromotionNo = model.TransferPromotionNo;
                result.IsUnlockedTransferDiscount = model.Booking.IsUnlockedTransferDiscount;
                result.IsUnlocked3PercentTransferDiscount = model.Booking.IsUnlocked3PercentTransferDiscount;
                result.IsApprove = model.IsApprove;
                result.ApproveDate = model.ApproveDate;
                result.IsFreeMortgageCharge = model.IsFreeMortgageCharge;

                result.Created = model.Created;
                result.CreatedBy = model.CreatedBy == null ? model.CreatedBy?.DisplayName : model.CreatedBy?.DisplayName;
                //var budgetMinPrice = await db.BudgetMinPrices.Where(o=>o.ProjectID == model.Booking.ProjectID).FirstOrDefaultAsync();
                //result.BudgetMinPrice = BudgetMinPriceDTO.CreateFromQueryResult(budgetMinPrice,budgetMinPrice.Year,budgetMinPrice.Quarter);


                var publicFundMonth = await db.AgreementConfigs.Where(o => o.ProjectID == model.Booking.ProjectID).FirstOrDefaultAsync();
                result.publicFundMonths = publicFundMonth.PublicFundMonths;

                var query = from b in db.BudgetPromotions
                                .Include(o => o.BudgetPromotionType)
                            where (b.BudgetPromotionType.Key == BudgetPromotionTypeKeys.Transfer
                                    && (b == null
                                        || b.ActiveDate == (db.BudgetPromotions.Include(o => o.BudgetPromotionType).Where(n => n.UnitID == b.UnitID && n.BudgetPromotionType.Key == BudgetPromotionTypeKeys.Transfer).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))
                            select b;
                var budgetPromotion = await query.FirstOrDefaultAsync();
                result.BudgetPromotion = BudgetPromotionDTO.CreateFromModel(budgetPromotion);
                var CheckCreditBank = await db.CreditBankings
                                            .Include(o => o.Booking)
                                            .Include(o => o.FinancialInstitution)
                                            .Where(o => o.IsUseBank == true).FirstOrDefaultAsync();
                result.CreditBanking = CreditBankingDTO.CreateFromModel(CheckCreditBank, db);

                var titledeel = await db.TitledeedDetails
                        .Where(o => o.ProjectID == model.Booking.ProjectID && o.UnitID == model.Booking.UnitID)
                        .FirstOrDefaultAsync();

                var CheckMinprice = await db.MinPrices
                        .Include(o => o.Project)
                        .Where(o => o.ProjectID == model.Booking.ProjectID && o.UnitID == model.Booking.UnitID)
                        .OrderByDescending(o => o.ActiveDate)
                        .FirstOrDefaultAsync();

                result.MinPrice = MinPriceDTO.CreateFromModel(CheckMinprice, titledeel);

                var Agreement = await db.Agreements
                    .Where(o => o.BookingID == model.BookingID).FirstOrDefaultAsync();
                if (Agreement != null)
                {
                    var AgreementOwner = await db.AgreementOwners
                                                    .Include(o => o.Agreement)
                                                    .Include(o => o.Agreement.UpdatedBy)
                                                    .Include(o => o.ContactTitleTH)
                                                    .Include(o => o.ContactTitleEN)
                                                    .Include(o => o.ContactType)
                                                    .Include(o => o.Gender)
                                                    .Include(o => o.National)
                                                    .Where(o => o.AgreementID == Agreement.ID && o.IsMainOwner == true).FirstOrDefaultAsync() ?? new Database.Models.SAL.AgreementOwner();
                    result.AgreementOwner = await AgreementOwnerDTO.CreateFromModelAsync(AgreementOwner, db);
                    result.AgreementPriceList = await AgreementPriceListDTO.CreateFromModelAsync(Agreement.ID, db);


                }
                var unitPrice = await db.UnitPrices
                                    .Include(o => o.UnitPriceStage)
                                    .Where(o =>
                                            o.BookingID == model.BookingID
                                            //&& o.IsActive == true
                                            && o.UnitPriceStage.Key == UnitPriceStageKeys.TransferPromotion)
                                    .OrderByDescending(o => o.Created)
                                    .FirstOrDefaultAsync();
                if (unitPrice != null)
                {
                    result.NetSellingPrice = unitPrice.AgreementPrice;
                    result.TransferDiscountOfAgreement = unitPrice.TransferDiscount.HasValue ? unitPrice.TransferDiscount.Value : 0;
                }

                #region "TransferPromotionItem"
                result.Items = new List<TransferPromotionItemDTO>();

                var projectID = await db.Units.Where(o => o.ID == Agreement.UnitID).FirstOrDefaultAsync();
                var promotionStatusActive2 = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();


                var query2 = from a in db.MasterTransferHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                            join ms in db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == model.MasterTransferPromotionID 
                            && o.MainPromotionItemID == null)
                            //&& o.ExpireDate >= DateTime.Now 
                            //&& o.PromotionItemStatusMasterCenterID == promotionStatusActive2)
                            on a.MasterTransferPromotionItemID equals ms.ID
                            select ms;
                var itemMaster = query2.ToList();

                //var itemMaster = await db.MasterTransferPromotionItems
                //           .Where(o => o.MasterTransferPromotionID == model.MasterTransferPromotionID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now)
                //           .OrderBy(o => o.Order).ToListAsync();

                var itemModels = await db.TransferPromotionItems
                    .Include(o => o.MasterPromotionItem)
                    .Include(o => o.QuotationTransferPromotionItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.TransferPromotionID == model.ID && o.MainTransferPromotionItemID == null).ToListAsync();

                if (itemMaster.Count > 0)
                {
                    if (itemModels.Count > 0)
                    {
                        //Modified By Peeratanat.k
                        //Original
                        foreach (var master in itemMaster)
                        {
                            foreach (var item in itemModels)
                            {
                                if (item.MasterTransferPromotionItemID == master.ID)
                                {
                                    result.Items.Add(TransferPromotionItemDTO.CreateFromModel(item, null, null, db));
                                    break;
                                }
                                else
                                {
                                    if (master.ExpireDate > DateTime.Now && master.PromotionItemStatusMasterCenterID == promotionStatusActive2)
                                    {
                                        result.Items.Add(TransferPromotionItemDTO.CreateFromMasterModel(master, null, null, db));
                                        break;
                                    }
                                }
                            }
                        }
                        //Original

                        //New
                        //var promotionItems = new List<TransferPromotionItemDTO>();
                        //var items = itemModels.Select(o => TransferPromotionItemDTO.CreateFromModel(o, null, null, db)).ToList();
                        //promotionItems.AddRange(items);
                        //result.Items.AddRange(promotionItems);

                        //foreach (var master in itemMaster)
                        //{
                        //    if (master.ExpireDate > DateTime.Now && master.PromotionItemStatusMasterCenterID == promotionStatusActive2)
                        //    {
                        //        result.Items.Add(TransferPromotionItemDTO.CreateFromMasterModel(master, null, null, db));
                        //    }
                        //}
                        //New
                        //End Modified By Peeratanat.k
                    }
                    else
                    {
                        var items = itemMaster.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                        result.Items.AddRange(items);
                    }
                }



                var query3 = from a in db.MasterTransferHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                             join ms in db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == model.MasterTransferPromotionID)
                             on a.MasterTransferPromotionFreeItemID equals ms.ID
                             select ms;
                var freeItemMaster = query3.ToList();

                //var freeItemMaster = await db.MasterTransferPromotionFreeItems
                // .Where(o => o.MasterTransferPromotionID == model.MasterTransferPromotionID)
                // .OrderBy(o => o.Order).ToListAsync();

                var freeModels = await db.TransferPromotionFreeItems
                    .Include(o => o.MasterTransferPromotionFreeItem)
                    .Include(o => o.QuotationTransferPromotionFreeItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.TransferPromotionID == model.ID).ToListAsync();

                if (freeItemMaster.Count > 0)
                {
                    if (freeModels.Count > 0)
                    {
                        //var items = freeModels.Select(o => TransferPromotionItemDTO.CreateFromModel(null, o, null, db)).ToList();
                        //result.Items.AddRange(items);

                        foreach (var master in freeItemMaster)
                        {
                            foreach (var item in freeModels)
                            {
                                if (item.MasterTransferPromotionFreeItemID == master.ID)
                                {
                                    result.Items.Add(TransferPromotionItemDTO.CreateFromModel(null, item, null, db));
                                    break;
                                }
                                else
                                {
                                    result.Items.Add(TransferPromotionItemDTO.CreateFromMasterModel(null, master, null, db));
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        var items = freeItemMaster.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                        result.Items.AddRange(items);
                    }
                }


                var creditMaster = await db.MasterTransferPromotionCreditCardItems
                     .Where(o => o.MasterTransferPromotionID == model.MasterTransferPromotionID)
                     .OrderBy(o => o.Order).ToListAsync();

                var creditModels = await db.TransferPromotionCreditCardItems
                    .Include(o => o.MasterTransferPromotionCreditCardItem)
                    .Include(o => o.QuotationTransferPromotionCreditCardItem)
                    .Include(o => o.UpdatedBy)
                    .Where(o => o.TransferPromotionID == model.ID).ToListAsync();

                if (creditMaster.Count > 0)
                {
                    if (creditModels.Count > 0)
                    {
                        //var items = creditModels.Select(o => TransferPromotionItemDTO.CreateFromModel(null, null, o, db)).ToList();
                        //result.Items.AddRange(items);

                        foreach (var master in creditMaster)
                        {
                            foreach (var item in creditModels)
                            {
                                if (item.MasterTransferPromotionCreditCardItemID == master.ID)
                                {
                                    result.Items.Add(TransferPromotionItemDTO.CreateFromModel(null, null, item, db));
                                    break;
                                }
                                else
                                {
                                    result.Items.Add(TransferPromotionItemDTO.CreateFromMasterModel(null, null, master, db));
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        var items = creditMaster.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                        result.Items.AddRange(items);
                    }
                }
                #endregion

                var workflow = await db.MinPriceBudgetWorkflows
                   .Include(o => o.MinPriceBudgetWorkflowStage)
                   .Include(o => o.MinPriceWorkflowType)
                   .Include(o => o.BudgetPromotionType)
                   .Where(o => o.TransferPromotionID == model.ID &&
                            o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.PromotionTransfer)
                   .OrderByDescending(o => o.Created).FirstOrDefaultAsync();



                if (workflow != null)
                {
                    result.MinPriceBudgetWorkflow = MinPriceBudgetWorkflowDTO.CreateFromModel(workflow);

                    var approveList = new List<MinPriceBudgetApprovalDTO>();


                    if (workflow.CreatedByUserID != null)
                    {
                        //var user = await db.UserRoles
                        //    .Include(o => o.Role)
                        //    .Include(o => o.User)
                        //    .Where(o => o.UserID == workflow.CreatedByUserID).FirstAsync();

                        var user = await db.Users.IgnoreQueryFilters().Where(o => o.ID == workflow.CreatedByUserID).FirstOrDefaultAsync();

                        var requester = new MinPriceBudgetApprovalDTO
                        {
                            MinPriceBudgetWorkflowID = workflow.ID,
                            Order = 0,
                            RoleName = "LC",
                            User = UserListDTO.CreateFromModel(user),
                            ApprovedDate = workflow.Created,
                            IsRequest = true
                        };

                        approveList.Add(requester);
                        approveList.OrderByDescending(o => o.Order);
                    }

                    //if (workflow.CreatedByUserID != null)
                    //{
                    //    var user = await db.UserRoles
                    //        .Include(o => o.Role)
                    //        .Include(o => o.User)
                    //        .Where(o => o.UserID == workflow.CreatedByUserID).FirstAsync();

                    //    var requester = new MinPriceBudgetApprovalDTO
                    //    {
                    //        MinPriceBudgetWorkflowID = workflow.ID,
                    //        Order = 0,
                    //        RoleName = user.Role?.Code,
                    //        User = UserListDTO.CreateFromModel(user.User),
                    //        ApprovedDate = workflow.Created,
                    //        IsRequest = true
                    //    };

                    //    approveList.Add(requester);
                    //    approveList.OrderByDescending(o => o.Order);
                    //}

                    var minPriceBudgetApprovals = await db.MinPriceBudgetApprovals.Include(o => o.Role)
                                                                    .Include(o => o.User)
                                                                    .Include(o => o.UpdatedBy)
                                                                    .Where(o => o.MinPriceBudgetWorkflowID == workflow.ID)
                                                                    .OrderBy(o => o.Order)
                                                                    .ToListAsync();

                    var minPriceBudgetApprovalsList = minPriceBudgetApprovals.Select(o => MinPriceBudgetApprovalDTO.CreateFromModel(o)).ToList();
                    if (minPriceBudgetApprovalsList.Count > 0)
                    {
                        for (var i = 0; i < minPriceBudgetApprovalsList.Count; i++)
                        {
                            approveList.Add(minPriceBudgetApprovalsList[i]);
                        }
                    }
                    foreach (var item in minPriceBudgetApprovals.Where(o => o.MinPriceBudgetWorkflowID == workflow.ID))
                    {
                        if (item?.IsApproved == false)
                        {
                            result.IsReject = true;
                        }
                    }
                    result.MinPriceBudgetApprovalWorkflow = approveList;


                }



                result.IsCreate = true;
                result.TransferPromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.TransferPromotionStatus);
                result.IsTransfer = false;
                result.IsTransferConfirmed = false;
                var transfer = await db.Transfers.Where(o => o.AgreementID == Agreement.ID).FirstOrDefaultAsync();
                if (transfer != null)
                {
                    if (!string.IsNullOrEmpty(transfer.TransferNo))
                    {
                        result.IsTransfer = true;
                    }
                    if (transfer.IsTransferConfirmed)
                    {
                        result.IsTransferConfirmed = true;
                    }

                }

                var IsTransferRequest = false;

                var itemIds = itemModels.Select(o => o.ID).ToList() ?? new List<Guid>();

                var reqItems = db.TransferPromotionRequestItems
                        .Include(o => o.TransferPromotionRequest)
                        .Include(o => o.TransferPromotionRequest.TransferPromotion)
                        //.Include(o => o.TransferPromotionRequest.TransferPromotion.Booking)
                        .Include(o => o.TransferPromotionRequest.PromotionRequestPRStatus)
                        .Where(o =>
                                itemIds.Contains((o.TransferPromotionItemID ?? Guid.Empty))
                                && (
                                    o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.Approve
                                    || o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.ApproveSomeUnit
                                    || o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.WaitApprove
                                    || o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.Reject
                                    || o.TransferPromotionRequest.PromotionRequestPRStatus.Key == PromotionRequestPRStatusKeys.WaitCancel
                                )
                                && o.TransferPromotionRequest.TransferPromotion.ID == model.ID
                                //&& !string.IsNullOrEmpty(o.PRNo)
                            )
                        .ToList() ?? new List<TransferPromotionRequestItem>();

                IsTransferRequest = reqItems.Any() ? true : false;

                result.IsTransferRequest = IsTransferRequest;
                
                foreach(var itemA in result.Items)
                {
                    itemA.IsDisabled = CreateDisabled(itemA.MaterialGroupKey, itemA.IsSelected, itemA.ItemType.ToString());
                    // 
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public static TransferPromotionDTO CreateFromModel(TransferPromotion model, DatabaseContext db)
        {
            if (model != null)
            {
                model.Booking = db.Bookings
                    .Include(o => o.Project)
                    .Include(o => o.Unit)
                    .Include(o => o.CreditBankingByUser)
                    .Where(o => o.ID == model.BookingID).FirstOrDefault();

                var result = new TransferPromotionDTO();

                result.Id = model.ID;
                result.PromotionNo = model.MasterPromotion?.PromotionNo;
                result.TransferPromotionDate = model.TransferPromotionDate;
                result.Remark = model.Remark;
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy == null ? model.UpdatedBy?.DisplayName : model.UpdatedBy?.DisplayName;
                result.Project = ProjectDropdownDTO.CreateFromModel(model.Booking?.Project);
                result.Unit = UnitDTO.CreateFromModel(model.Booking?.Unit);
                result.Booking = BookingDTO.CreateFromModel(model.Booking, db);
                result.BudgetAmount = model.BudgetAmount;
                result.TotalAmount = model.TotalAmount;
                result.TransferDateBefore = model.TransferDateBefore;
                result.MasterTransferPromotion = MasterTransferPromotionDTO.CreateFromModel(model.MasterPromotion);
                result.TransferDiscount = model.TransferDiscount.HasValue ? model.TransferDiscount.Value : 0;
                result.IsFreeMortgageCharge = model.IsFreeMortgageCharge;
                result.TransferPromotionNo = model.TransferPromotionNo;
                result.IsUnlockedTransferDiscount = model.Booking.IsUnlockedTransferDiscount;
                result.IsUnlocked3PercentTransferDiscount = model.Booking.IsUnlocked3PercentTransferDiscount;
                result.IsApprove = model.IsApprove;
                result.ApproveDate = model.ApproveDate;
                result.Created = model.Created;
                result.CreatedBy = model.CreatedBy == null ? model.CreatedBy?.DisplayName : model.CreatedBy?.DisplayName;
                result.IsCreate = true;
                result.TransferPromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.TransferPromotionStatus);

                result.IsTransferRequest = false;

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<TransferPromotionDTO> CreateFromModelDrafAsync(Booking model, DatabaseContext db)
        {
            if (model != null)
            {
                var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
                var promotion = await db.MasterTransferPromotions
                    .Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == model.ProjectID && o.StartDate <= DateTime.Now && o.EndDate >= DateTime.Now)
                    .FirstOrDefaultAsync() ?? new Database.Models.PRM.MasterTransferPromotion();
                var Agreement = await db.Agreements.Include(o => o.UpdatedBy).Where(o => o.BookingID == model.ID).FirstOrDefaultAsync() ?? new Database.Models.SAL.Agreement();

                var AgreementOwner = await db.AgreementOwners
                                .Include(o => o.Agreement)
                                .ThenInclude(o => o.UpdatedBy)
                                .Include(o => o.ContactTitleTH)
                                .Include(o => o.ContactTitleEN)
                                .Include(o => o.ContactType)
                                .Include(o => o.Gender)
                                .Include(o => o.National)
                                .Where(o => o.AgreementID == Agreement.ID && o.IsMainOwner == true)
                                .FirstOrDefaultAsync() ?? new Database.Models.SAL.AgreementOwner();

                var TransferPromotion = await db.TransferPromotions.Where(o => o.BookingID == model.ID).FirstOrDefaultAsync() ?? new Database.Models.PRM.TransferPromotion();
                var MinPriceBudgetWorkflow = await db.MinPriceBudgetWorkflows.Where(o => o.TransferPromotionID == TransferPromotion.ID).FirstOrDefaultAsync();


                var results = new List<MinPriceBudgetApprovalDTO>();
                if (MinPriceBudgetWorkflow != null)
                {

                    if (MinPriceBudgetWorkflow.CreatedByUserID != null)
                    {
                        var user = await db.UserRoles
                            .Include(o => o.Role)
                            .Include(o => o.User)
                            .Where(o => o.UserID == MinPriceBudgetWorkflow.CreatedByUserID).FirstAsync();

                        var requester = new MinPriceBudgetApprovalDTO
                        {
                            MinPriceBudgetWorkflowID = MinPriceBudgetWorkflow.ID,
                            Order = 0,
                            RoleName = user.Role?.Code,
                            User = UserListDTO.CreateFromModel(user.User),
                            ApprovedDate = MinPriceBudgetWorkflow.Created,
                            IsRequest = true
                        };

                        results.Add(requester);
                        results.OrderByDescending(o => o.Order);
                    }

                    var approvals = await db.MinPriceBudgetApprovals.Include(o => o.Role)
                                                                    .Include(o => o.User)
                                                                    .Include(o => o.UpdatedBy)
                                                                    .Where(o => o.MinPriceBudgetWorkflowID == MinPriceBudgetWorkflow.ID)
                                                                    .OrderBy(o => o.Order)
                                                                    .ToListAsync();

                    var results2 = approvals.Select(o => MinPriceBudgetApprovalDTO.CreateFromModel(o)).ToList();
                    if (results2.Count > 0)
                    {
                        for (var i = 0; i < results2.Count; i++)
                        {
                            results.Add(results2[i]);
                        }
                    }

                }

                var result = new TransferPromotionDTO();
                //Id = model.ID;
                result.PromotionNo = promotion.PromotionNo;
                result.TransferPromotionDate = DateTime.Now.Date;
                result.Remark = "";
                result.Updated = model.Updated;
                result.UpdatedBy = model.UpdatedBy == null ? model.UpdatedBy?.DisplayName : model.UpdatedBy?.DisplayName;
                result.Project = ProjectDropdownDTO.CreateFromModel(model.Project);
                result.Unit = UnitDTO.CreateFromModel(model.Unit);
                result.Booking = await BookingDTO.CreateFromModelAsync(model, db);
                result.BudgetAmount = 0;
                result.TotalAmount = 0;
                //TransferDateBefore = null;
                result.MasterTransferPromotion = MasterTransferPromotionDTO.CreateFromModel(promotion);
                result.TransferDiscount = 0;
                result.IsFreeMortgageCharge = false;
                result.AgreementOwner = await AgreementOwnerDTO.CreateFromModelAsync(AgreementOwner, db);
                result.TransferPromotionNo = TransferPromotion.TransferPromotionNo;
                result.MinPriceBudgetWorkflow = MinPriceBudgetWorkflowDTO.CreateFromModel(MinPriceBudgetWorkflow);
                result.MinPriceBudgetApprovalWorkflow = results;
                result.IsApprove = false;
                result.IsUnlockedTransferDiscount = model.IsUnlockedTransferDiscount;
                result.IsUnlocked3PercentTransferDiscount = model.IsUnlocked3PercentTransferDiscount;
                result.AgreementPriceList = await AgreementPriceListDTO.CreateFromModelAsync(Agreement.ID, db);
                result.Created = model.Created;
                result.CreatedBy = model.CreatedBy == null ? model.CreatedBy?.DisplayName : model.CreatedBy?.DisplayName;
                var query = from b in db.BudgetPromotions
                              .Include(o => o.BudgetPromotionType)
                            where (b.BudgetPromotionType.Key == BudgetPromotionTypeKeys.Transfer
                                    && (b == null
                                        || b.ActiveDate == (db.BudgetPromotions.Include(o => o.BudgetPromotionType).Where(n => n.UnitID == b.UnitID && n.BudgetPromotionType.Key == BudgetPromotionTypeKeys.Transfer).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))
                            select b;
                var budgetPromotion = await query.FirstOrDefaultAsync();
                result.BudgetPromotion = BudgetPromotionDTO.CreateFromModel(budgetPromotion);
                var CheckCreditBank = await db.CreditBankings
                                          .Include(o => o.Booking)
                                          .Include(o => o.FinancialInstitution)
                                          .Where(o => o.IsUseBank == true).FirstOrDefaultAsync();
                result.CreditBanking = CreditBankingDTO.CreateFromModel(CheckCreditBank, db);

                var titledeel = await db.TitledeedDetails
                        .Where(o => o.ProjectID == model.ProjectID && o.UnitID == model.UnitID)
                        .FirstOrDefaultAsync();

                var CheckMinprice = await db.MinPrices
                        .Include(o => o.Project)
                        .Where(o => o.ProjectID == model.ProjectID && o.UnitID == model.UnitID)
                        .OrderByDescending(o => o.ActiveDate)
                        .FirstOrDefaultAsync();

                result.MinPrice = MinPriceDTO.CreateFromModel(CheckMinprice, titledeel);

                var unitPrice = await db.UnitPrices
                                   .Include(o => o.UnitPriceStage)
                                   .Where(o =>
                                        o.BookingID == model.ID
                                        && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                                    )
                                   .OrderByDescending(o => o.Created)
                                   .FirstOrDefaultAsync();
                if (unitPrice != null)
                {
                    result.NetSellingPrice = unitPrice.AgreementPrice;
                    result.TransferDiscountOfAgreement = unitPrice.TransferDiscount.HasValue ? unitPrice.TransferDiscount.Value : 0;
                }

                result.Items = new List<TransferPromotionItemDTO>();

                var itemModels = await db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == promotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(o, null, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var freeModels = await db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == promotion.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(null, o, null, db)).ToList();
                    result.Items.AddRange(items);
                }

                var creditModels = await db.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == promotion.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => TransferPromotionItemDTO.CreateFromMasterModel(null, null, o, db)).ToList();
                    result.Items.AddRange(items);
                }

                var workflow = await db.MinPriceBudgetWorkflows
                .Include(o => o.Project)
                .Include(o => o.MinPriceBudgetWorkflowStage)
                .Include(o => o.MinPriceWorkflowType)
                .Include(o => o.BudgetPromotionType)
                .Include(o => o.Booking)
                .ThenInclude(o => o.Unit)
                .Where(o => o.BookingID == model.ID)
                .OrderByDescending(o => o.Created).FirstOrDefaultAsync();

                if (workflow != null)
                {
                    result.MinPriceBudgetWorkflow = MinPriceBudgetWorkflowDTO.CreateFromModel(workflow);
                }

                result.IsTransfer = false;
                var transfer = await db.Transfers.Where(o => o.AgreementID == Agreement.ID).FirstOrDefaultAsync();
                if (transfer != null)
                {
                    if (!string.IsNullOrEmpty(transfer.TransferNo))
                    {
                        result.IsTransfer = true;
                    }
                }

                result.IsTransferRequest = false;

                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<TransferPromotionDTO> CreateFromModelMasterAsync(Booking model, DatabaseContext db)
        {
            var result = new TransferPromotionDTO();

            if (model != null)
            {
                var Booking = db.Bookings
                   .Include(o => o.CreditBankingType)
                   .Include(o => o.Project)
                   .Include(o => o.Unit)
                   .Where(o => o.ID == model.ID).FirstOrDefault();

                //var MasterTransferPromotion = await db.MasterTransferPromotions.Where(o => o.ProjectID == model.ProjectID && o.IsActive == true).FirstOrDefaultAsync();

                var promotionStatusActive = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();
                var MasterTransferPromotion = await db.MasterTransferPromotions
                    .Where(o => o.PromotionStatusMasterCenterID == promotionStatusActive && o.ProjectID == model.ProjectID && o.StartDate <= DateTime.Now && o.EndDate >= DateTime.Now)
                    .FirstOrDefaultAsync() ?? new Database.Models.PRM.MasterTransferPromotion();

                //result.Id = model.ID;
                result.PromotionNo = MasterTransferPromotion?.PromotionNo;
                //result.Updated = model.Updated;
                //result.UpdatedBy = model.UpdatedBy == null ? model.UpdatedBy?.DisplayName : model.UpdatedBy?.DisplayName;
                result.Project = ProjectDropdownDTO.CreateFromModel(Booking?.Project);
                result.Unit = UnitDTO.CreateFromModel(Booking?.Unit);
                result.Booking = await BookingDTO.CreateFromModelAsync(Booking, db);
                result.MasterTransferPromotion = MasterTransferPromotionDTO.CreateFromModel(MasterTransferPromotion);
                var publicFundMonth = await db.AgreementConfigs.Where(o => o.ProjectID == Booking.ProjectID).FirstOrDefaultAsync();
                result.publicFundMonths = publicFundMonth.PublicFundMonths;
                //result.Created = model.Created;
                //result.CreatedBy = model.CreatedBy == null ? model.CreatedBy?.DisplayName : model.CreatedBy?.DisplayName;

                //if (result.IsCreate != true)
                //{
                //    result.Id = null;
                //}
                var query = from b in db.BudgetPromotions
                                .Include(o => o.BudgetPromotionType)
                            where (b.BudgetPromotionType.Key == BudgetPromotionTypeKeys.Transfer
                                    && (b == null
                                        || b.ActiveDate == (db.BudgetPromotions.Include(o => o.BudgetPromotionType).Where(n => n.UnitID == b.UnitID && n.BudgetPromotionType.Key == BudgetPromotionTypeKeys.Transfer).OrderByDescending(n => n.ActiveDate).Max(n => n.ActiveDate))))
                            select b;
                var budgetPromotion = await query.FirstOrDefaultAsync();
                result.BudgetPromotion = BudgetPromotionDTO.CreateFromModel(budgetPromotion);

                var titledeel = await db.TitledeedDetails
                        .Where(o => o.ProjectID == model.ProjectID && o.UnitID == model.UnitID)
                        .FirstOrDefaultAsync();

                var CheckMinprice = await db.MinPrices
                        .Include(o => o.Project)
                        .Where(o => o.ProjectID == model.ProjectID && o.UnitID == model.UnitID)
                        .OrderByDescending(o => o.ActiveDate)
                        .FirstOrDefaultAsync();

                result.MinPrice = MinPriceDTO.CreateFromModel(CheckMinprice, titledeel);

                var Agreement = await db.Agreements.Where(o => o.BookingID == Booking.ID).FirstOrDefaultAsync();
                if (Agreement != null)
                {
                    var AgreementOwner = await db.AgreementOwners
                                                    .Include(o => o.Agreement)
                                                    .Include(o => o.Agreement.UpdatedBy)
                                                    .Include(o => o.ContactTitleTH)
                                                    .Include(o => o.ContactTitleEN)
                                                    .Include(o => o.ContactType)
                                                    .Include(o => o.Gender)
                                                    .Include(o => o.National)
                                                    .Where(o => o.AgreementID == Agreement.ID && o.IsMainOwner == true).FirstOrDefaultAsync();

                    result.AgreementOwner = await AgreementOwnerDTO.CreateFromModelAsync(AgreementOwner, db);
                    result.AgreementPriceList = await AgreementPriceListDTO.CreateFromModelAsync(Agreement.ID, db);

                }

                var CheckCreditBank = await db.CreditBankings
                                           .Include(o => o.Booking)
                                            .ThenInclude(o => o.CreditBankingType)
                                           .Include(o => o.LoanStatus)
                                           .Include(o => o.FinancialInstitution)
                                           .Where(o => o.IsUseBank == true && o.BookingID == model.ID).FirstOrDefaultAsync();

                result.CreditBanking = CreditBankingDTO.CreateFromModel(CheckCreditBank, db);

                var unitPrice = await db.UnitPrices
                                    .Include(o => o.UnitPriceStage)
                                    .Where(o =>
                                        o.BookingID == Booking.ID
                                        //&& o.IsActive == true
                                        && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                                    )
                                    .OrderByDescending(o => o.Created)
                                    .FirstOrDefaultAsync();

                if (unitPrice != null)
                {
                    result.NetSellingPrice = unitPrice.AgreementPrice;
                    result.TransferDiscountOfAgreement = unitPrice.TransferDiscount.HasValue ? unitPrice.TransferDiscount.Value : 0;
                    if (CheckCreditBank != null)
                    {
                        //กรณีส่วนต่างของค่าจดจำนอง
                        result.DiffMortgageFree = (result.CreditBanking?.ApprovedAmount ?? 0) - (result?.NetSellingPrice ?? 0);
                    }
                }


                result.MasterPromotionItems = new List<MasterTransferPromotionItemDTO>();
                var projectID = await db.Units.Where(o => o.ID == Agreement.UnitID).FirstOrDefaultAsync();
                var promotionStatusActive2 = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == "PromotionItemStatus" && o.Key == PromotionStatusKeys.Active).Select(o => o.ID).FirstOrDefaultAsync();

                var query2 = from a in db.MasterTransferHouseModelItems.Where(o => o.ModelID == projectID.ModelID)
                            join ms in db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == MasterTransferPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now && o.PromotionItemStatusMasterCenterID == promotionStatusActive2)
                            on a.MasterTransferPromotionItemID equals ms.ID
                            select ms;
                var itemModels = query2.ToList();

               // var itemModels = await db.MasterTransferPromotionItems.Where(o => o.MasterTransferPromotionID == MasterTransferPromotion.ID && o.MainPromotionItemID == null && o.ExpireDate >= DateTime.Now).ToListAsync();
                if (itemModels.Count > 0)
                {
                    var items = itemModels.Select(o => MasterTransferPromotionItemDTO.CreateTransferPromotionFromModel(o, db)).ToList();
                    result.MasterPromotionItems.AddRange(items);
                }

                var query3 = from a in db.MasterTransferHouseModelFreeItems.Where(o => o.ModelID == projectID.ModelID)
                             join ms in db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == MasterTransferPromotion.ID)
                             on a.MasterTransferPromotionFreeItemID equals ms.ID
                             select ms;
                var freeModels = query3.ToList();
                //var freeModels = await db.MasterTransferPromotionFreeItems.Where(o => o.MasterTransferPromotionID == MasterTransferPromotion.ID).ToListAsync();
                if (freeModels.Count > 0)
                {
                    var items = freeModels.Select(o => MasterTransferPromotionFreeItemDTO.CreateFromDraftModel(o)).ToList();
                    result.MasterPromotionItems.AddRange(items);
                }

                var creditModels = await db.MasterTransferPromotionCreditCardItems.Where(o => o.MasterTransferPromotionID == MasterTransferPromotion.ID).ToListAsync();
                if (creditModels.Count > 0)
                {
                    var items = creditModels.Select(o => MasterTransferCreditCardItemDTO.CreateFromDraftModel(o)).ToList();
                    result.MasterPromotionItems.AddRange(items);
                }

                result.IsCreate = false;


                /*-- Default สถานะ Transfer Promotion --*/
                var TransferPromotionStatus = await db.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.TransferPromotionStatus && o.Key == TransferPromotionStatusTypeKeys.WaitTransferPromotion).FirstAsync();
                result.TransferPromotionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(TransferPromotionStatus);

                if (default(Guid) == result.Id)
                {
                    result.Id = null;
                }

                result.IsUnlockedTransferDiscount = model.IsUnlockedTransferDiscount;
                result.IsUnlocked3PercentTransferDiscount = model.IsUnlocked3PercentTransferDiscount;

                result.IsTransferRequest = false;

                return result;

            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref TransferPromotion model)
        {
            model = model ?? new TransferPromotion();

            model.BookingID = this.Booking.Id.Value;
            model.BudgetAmount = this.BudgetAmount;
            model.TotalAmount = this.TotalAmount;
            model.TransferDateBefore = this.TransferDateBefore.HasValue ? this.TransferDateBefore.Value.Date : this.TransferDateBefore;
            model.MasterTransferPromotionID = this.MasterTransferPromotion?.Id;
            model.TransferDiscount = this.TransferDiscount;
            model.TransferPromotionNo = this.TransferPromotionNo;
            model.IsFreeMortgageCharge = this.IsFreeMortgageCharge;
            model.Remark = this.Remark;
            model.IsActive = true;

            //model.IsApprove = this.IsApprove;
            model.TransferPromotionDate = this.TransferPromotionDate.HasValue ? this.TransferPromotionDate.Value.Date : this.TransferPromotionDate;
        }
        public class MasterTransferPromotionItemQueryResult
        {
            public MasterTransferPromotionItem MasterTransferPromotionItem { get; set; }
            //public PromotionMaterialItem PromotionMaterialItem { get; set; }
            //public MasterCenter PromotionItemStatus { get; set; }
            //public MasterCenter WhenPromotionReceive { get; set; }
            //public User UpdatedBy { get; set; }
        }
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();

            var master = await db.MasterTransferPromotions.Include(o => o.PromotionStatus).Where(
                o => o.ProjectID == this.Project.Id
            && o.PromotionNo == this.PromotionNo
            && o.PromotionStatus.Key == PromotionStatusKeys.Active
            ).FirstOrDefaultAsync();

            var agreement = await db.Agreements.Where(o => o.BookingID == this.Booking.Id && o.IsDeleted == false).FirstOrDefaultAsync();
            var unitPriceResult = await db.UnitPrices
                               .Include(o => o.UnitPriceStage)
                               .Where(o =>
                                    o.BookingID == this.Booking.Id.Value
                                    //&& o.IsActive == true
                                    && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement
                                )
                                .OrderByDescending(o => o.Created)
                                .FirstOrDefaultAsync();

            if (master != null)
            {
                if (this.IsUnlockedTransferDiscount && !this.IsUnlocked3PercentTransferDiscount)
                {
                    if (this.TransferDiscount > master.TransferDiscount)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0157").FirstAsync(); //กรุณาระบุส่วนลดเกินราคาใน Master ที่กำหนด
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                    else if ((unitPriceResult.TransferDiscount + this.TransferDiscount) > (unitPriceResult.AgreementPrice * Convert.ToDecimal(0.1)))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0159").FirstAsync(); //กรุณาระบุส่วนลดไม่เกิน 10% ของราคาขายสุทธิ
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                    else if (this.TransferDiscount > (unitPriceResult.AgreementPrice * Convert.ToDecimal(0.03)))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0158").FirstAsync();//กรุณาระบุส่วนลด ไม่เกิน 3% ของราคาขายสุทธิ
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                }
                if (this.IsUnlocked3PercentTransferDiscount && this.IsUnlockedTransferDiscount)
                {
                    if (this.TransferDiscount > master.TransferDiscount)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0157").FirstAsync(); //กรุณาระบุส่วนลดเกินราคาใน Master ที่กำหนด
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                    else if ((unitPriceResult.TransferDiscount + this.TransferDiscount) > (unitPriceResult.AgreementPrice * Convert.ToDecimal(0.1)))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0159").FirstAsync(); //กรุณาระบุส่วนลดไม่เกิน 10% ของราคาขายสุทธิ
                        ex.AddError(errMsg.Key, errMsg.Message, (int)errMsg.Type);
                    }
                }
            }



            if (ex.HasError)
            {
                throw ex;
            }
        }

        public static bool CreateDisabled(string MaterialGroupKey, bool IsSelect, string creditCardType)
        {
            //
            List<string> key = new List<string>();
            key.Add("PLV100");
            key.Add("7V700");
            key.Add("EST100");
            key.Add("HST100");
            key.Add("NST100");
            bool isCreditCard = false;
            if (creditCardType.Equals("CreditCard"))
            {
                isCreditCard = true;
            }
            // 
            if (!key.Contains(MaterialGroupKey) && !isCreditCard)
            {
                return true;
            }
            //else if (key.Contains(MaterialGroupKey) && IsSelect)
            //{
            //    return false;
            //}
            else
            {
                return false;
            }
        }
    }



}