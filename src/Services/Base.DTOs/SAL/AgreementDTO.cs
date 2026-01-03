using Base.DTOs.ACC;
using Base.DTOs.PRJ;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.SAL;
using FileStorage;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DTOs.SAL

{
    public class AgreementDTO : BaseDTO
    {
        /// <summary>
        /// เลขที่สัญญา
        /// </summary>
        public string AgreementNo { get; set; }
        /// <summary>
        /// แปลง
        /// </summary>
        public PRJ.UnitDTO Unit { get; set; }
        /// <summary>
        /// โครงการ
        /// </summary>
        public PRJ.ProjectDTO Project { get; set; }
        /// <summary>
        /// ใบจอง
        /// </summary>
        public BookingDTO Booking { get; set; }
        /// <summary>
        /// สถานะสัญญา
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=AgreementStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO AgreementStatus { get; set; }
        /// <summary>
        /// วันที่ทำสัญญา
        /// </summary>
        public DateTime? ContractDate { get; set; }
        /// <summary>
        /// วันที่โอนกรรมสิทธิ์
        /// </summary>
        public DateTime? TransferOwnershipDate { get; set; }
        /// <summary>
        /// วันที่ลงนามสัญญา
        /// </summary>
        public DateTime? SignAgreementDate { get; set; }
        /// <summary>
        /// ผู้ขออนุมัติ
        /// </summary>
        public USR.UserListDTO SignContractRequestUser { get; set; }
        /// <summary>
        /// วันที่ขออนุมัติ Sign Contract ล่าสุด
        /// </summary>
        public DateTime? SignContractRequestDate { get; set; }
        /// <summary>
        /// วันที่อนุมัติ Sign Contract
        /// </summary>
        public DateTime? SignContractApprovedDate { get; set; }
        /// <summary>
        /// สถานะอนุมัติ Sign Contract (true = อนุมัติ/false = ไม่อนุมัติ)
        /// </summary>
        public bool? IsSignContractApproved { get; set; }
        /// <summary>
        /// สถานะอนุมัติพิมพ์สัญญา (true = อนุมัติ/false = ไม่อนุมัติ)
        /// </summary>
        public bool? IsPrintApproved { get; set; }
        /// <summary>
        /// วัน-เวลาที่อนุมัติพิมพ์สัญญา
        /// </summary>
        public DateTime? PrintApprovedDate { get; set; }
        /// <summary>
        /// ผู้อนุมัติพิมพ์สัญญา
        /// </summary>
        public USR.UserListDTO PrintApprovedBy { get; set; }
        /// <summary>
        /// สถานะยกเลิกอนุมัติพิมพ์สัญญา
        /// </summary>
        public bool? IsCancelPrintApproved { get; set; }
        /// <summary>
        /// ราคาพื้นที่ต่อหน่วย
        /// </summary>
        public decimal? AreaPricePerUnit { get; set; }
        /// <summary>
        /// พื้นที่เพิ่มลด ค่าบวก = พื้นที่โฉนด > พื้นที่ขาย
        /// </summary>
        public double? OffsetArea { get; set; }
        /// <summary>
        /// ราคาพื้นที่เพิ่มลด
        /// </summary>
        public decimal? OffsetAreaPrice { get; set; }
        /// <summary>
        /// สถานะการก่อสร้าง (สำหรับโครงการแนวสูง)
        /// </summary>
        public MST.MasterCenterDropdownDTO HighRiseConstructionStatus { get; set; }
        /// <summary>
        /// บริษัทจ่ายงวดสุดท้าย (true = จ่าย)
        /// </summary>
        public bool IsSellerPayLastDownInstallment { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO Files { get; set; }
        /// <summary>
        /// ประเภทพนักงานปิดการขาย
        /// GET Master/api/MasterCenters?MasterCenterGroupKey=SaleOfficerType
        /// </summary>
        public MST.MasterCenterDropdownDTO SaleOfficerType { get; set; }
        /// <summary>
        /// รหัส Sale
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO SaleUser { get; set; }
        /// <summary>
        /// รหัส Agent
        /// GET Master/api/Agents/DropdownList
        /// </summary>
        public MST.AgentDropdownDTO Agent { get; set; }
        /// <summary>
        /// รหัสพนักงาน Agent
        /// GET Master/api/AgentEmployees/DropdownList?agentID={agentID}
        /// </summary>
        public MST.AgentEmployeeDropdownDTO AgentEmployee { get; set; }
        /// <summary>
        /// รหัส Sale ประจำโครงการ
        /// GET Identity/api/Users?roleCodes=LC&authorizeProjectIDs=7{projectID}
        /// </summary>
        public USR.UserListDTO ProjectSaleUser { get; set; }
        /// <summary>
        /// สถานะตั้งเรื่องเปลี่ยนแปลงชื่ออยู่หรือไม่ (true = ตั้งเรื่อง)
        /// </summary>
        public bool IsChangeAgreementOwnerWorkflow { get; set; }
        /// <summary>
        /// ประเภทของการตั้งเรื่องเปลี่ยนแปลงชื่อ
        /// </summary>
        public string ChangeAgreementOwnerName { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// แสดงปุ่มบันทึกเลขที่สัญญา
        /// </summary>
        public bool IsSaveContract { get; set; }
        /// <summary>
        /// แสดงปุ่มยกเลิกเลขที่สัญญา
        /// </summary>
        public bool IsCancelContract { get; set; }
        /// <summary>
        /// แสดงปุ่มอนุมัติพิมพ์สัญญา
        /// </summary>
        public bool IsPrintContract { get; set; }
        /// <summary>
        /// แสดงปุ่มเปลี่ยนแปลงชื่อ
        /// </summary>
        public bool IsChangeOwner { get; set; }
        /// <summary>
        /// แสดงปุ่มย้ายแปลง
        /// </summary>
        public bool IsChangeUnit { get; set; }
        /// <summary>
        /// แสดงปุ่มยกเลิก
        /// </summary>
        public bool IsCancelButton { get; set; }
        /// <summary>
        /// แสดงปุ่มเอกสาร
        /// </summary>
        public bool IsViewFile { get; set; }
        /// <summary>
        /// แสดงปุ่มแนบเอกสาร
        /// </summary>
        public bool IsAddFile { get; set; }
        /// <summary>
        /// แสดงปุ่มการอนุมัติสัญญา
        /// </summary>
        public bool IsApproveContract { get; set; }
        /// <summary>
        /// แสดงปุ่มชำระเงิน
        /// </summary>
        public bool IsPayment { get; set; }
        /// <summary>
        /// SAL.UnitPrice
        /// </summary>
        public AgreementPriceListDTO UnitPrice { get; set; }
        /// <summary>
        /// Min Price Workflow
        /// </summary>
        public MinPriceBudgetWorkflowDTO MinPriceBudgetWorkflow { get; set; }
        /// <summary>
        /// สถานะตั้งเรื่องย้ายแปลงอยู่หรือไม่ (true = ตั้งเรื่อง)
        /// </summary>
        public bool IsChangeUnitWorkflow { get; set; }
        /// <summary>
        /// แสดงปุ่มตั้งเรื่องเปลี่ยนแปลงโปร
        /// </summary>
        public bool IsChangePromotionButton { get; set; }
        /// <summary>
        /// สถานะตั้งเรื่องเปลี่ยนแปลงโปรหรือไม่ (true = ตั้งเรื่อง)
        /// </summary>
        public bool IsChangePromotionWorkflow { get; set; }
        /// <summary>
        /// พื้นที่ขาย
        /// </summary>
        public double? SaleArea { get; set; }
        /// <summary>
        /// ข้อมูล Booking Promotion
        /// </summary>
        public SalePromotionDTO salePromotion { get; set; }
        /// <summary>
        /// รายชื่อ Main Owner
        /// </summary>
        public string MainOwnerName { get; set; }
        /// <summary>
        /// รายชื่อ Agreement Owner ทั้งหมด
        /// </summary>
        public string AllOwnerName { get; set; }
        /// <summary>
        /// ยืนยันโอนจริง
        /// </summary>
        public bool IsTransferConfirmed { get; set; }
        /// <summary>
        /// วันสิ้นสุดโปรโมชั่นที่เสนอ
        /// </summary>
        public DateTime? TransferDateBefore { get; set; }
        /// <summary>
        /// แสดงปุ่มยกเลิกสัญญา
        /// </summary>
        public bool IsCancelContract2 { get; set; }
        /// <summary>
        /// สถานะตั้งเรื่องยกเลิกสัญญาอยู่หรือไม่ (true = ตั้งเรื่อง)
        /// </summary>
        public bool IsCancelMemo { get; set; }
        /// <summary>
        /// สถานะลงนามสัญญา Popup 
        /// </summary>
        public bool IsSignAgreementPopup { get; set; }
        /// <summary>
        /// สถานะขออนุมัติลงนาม Popup 
        /// </summary>
        public bool IsSignContractRequestPopup { get; set; }
        /// <summary>
        /// สถานะอนุมัติลงนาม Popup 
        /// </summary>
        public bool IsSignContractApprovedPopup { get; set; }
        /// <summary>
        /// ค่าธรรมเนียม/เบี้ยปรับ(บาท) กรณีตั้งเรื่องเปลี่ยนแปลงชื่อ
        /// </summary>
        public decimal? ChangeNameFee { get; set; }
        /// <summary>
        /// จ่ายค่าธรรมเนียม/เบี้ยปรับ(บาท)ครบแล้ว กรณีตั้งเรื่องเปลี่ยนแปลงชื่อ
        /// </summary>
        public bool? IsChangeNameFeePaid { get; set; }
        /// <summary>
        /// ค่าเงินคงเหลือจากแปลงเดิม (ใช้ที่หน้าย้ายแปลง)
        /// </summary>
        public decimal? TotalReceivedAmount { get; set; }
        /// <summary>
        /// Due วันที่โอนกรรมสิทธิ์จาก Master Data
        /// </summary>
        public DateTime? DueTransferDate { get; set; }
        /// <summary>
        /// แสดงลักษณะโครงการ True = แนวสูง / False = แนวราบ
        /// </summary>
        public bool IsBuilding { get; set; }
        /// <summary>
        /// ตรวจสอบกรณีดิวโอนมี Range ต่างจากวันที่ปัจจุบัน 30 วันหรือไม่ สำหรับการปลดล็อคดิวโอน
        /// </summary>
        public bool IsUnLockDueTransDate { get; set; }
        /// <summary>
        /// ตรวจสอบว่าชำระเงินทำสัญญาครบหรือยัง เพื่อแสดงปุ่ม ลงนามสัญญา
        /// </summary>
        public bool IsCompletePayContractAmount { get; set; }
        /// <summary>
        /// Id ของ การตั้งเรื่องเปลี่ยนแปลงชื่อ
        /// </summary>
        public Guid ChangeAgreementOwnerWorkflowId { get; set; }
        /// <summary>
        /// Id ของ การตั้งเรื่องย้ายแปลง
        /// </summary>
        public Guid ChangeUnitWorkflowId { get; set; }
        /// <summary>
        /// Id ของ การตั้งเรื่องเปลี่ยนแปลงโอน
        /// </summary>
        public Guid ChangePromotionWorkflowId { get; set; }
        /// <summary>
        /// ตรวจสอบว่าโปรเจ็คเปิดขายไม่ถึง 6 เดือน
        /// </summary>
        public bool IsProjectLessThanSixMonth { get; set; }
        /// <summary>
        /// ตรวจสอบว่าให้ Disable ปุ่ม อนุมัติพิมพ์เอกสาร
        /// </summary>
        public bool IsDisableApvPrintBtn { get; set; }

        /// <summary>
        /// Id ของ การตั้งเรื่องย้ายแปลงของแปลงต้นทาง
        /// </summary>
        public Guid? FromChangeUnitWorkflowId { get; set; }
        /// <summary>
        /// มาจากการย้ายแปลง
        /// </summary>
        public bool? IsFormChangeUnitWorkflow { get; set; }
        /// <summary>
        /// สำหรับตรวจสอบว่ามีการตั้งเรื่องโปรโอน
        /// </summary>
        public bool? IsTransferPromotion { get; set; }
        /// <summary>
        /// สำหรับตรวจสอบว่ามีการติดminprice
        /// </summary>
        public bool? Isminprice { get; set; }
        /// <summary>
        /// แสดงปุ่มตั้งเรื่องใบบันทึกเลื่อนโอน
        /// </summary>
        public bool IsPostponeTransferButton { get; set; }
        /// <summary>
        /// เลขที่ใบบันทึกเลื่อนโอน
        /// </summary>
        public Guid? PostponeTransferId { get; set; }
        /// <summary>
        /// สถานะใบบันทึกเลื่อนโอน Popup >> ต้องทำบันทึกเลื่อนนัดโอน
        /// </summary>
        public bool IsPostponeTransferPopup { get; set; }
        /// <summary>
        /// สถานะใบบันทึกเลื่อนโอน Popup >> ให้มี Pop-up แจ้งว่า ”แปลงนี้ไม่มีการออกจดหมายนัดโอน” และไม่ต้องทำบันทึกเลื่อนนัดโอน
        /// </summary>
        public bool IsPostponeTransferPopup2 { get; set; }

        public bool IsChangeUnitWorkflow2 { get; set; }
        public UnitDropdownDTO UniCombine { get; set; }


        public static async Task<AgreementDTO> CreateFromModelAsync(Agreement model, FileHelper fileHelper, DatabaseContext DB)
        {
            if (model != null)
            {
                if (model.Unit == null)
                {
                    model.Unit = DB.Units.Where(e => e.ID == model.UnitID).FirstOrDefault() ?? new Database.Models.PRJ.Unit();
                }

                model.Unit.TitledeedDetails = await DB.TitledeedDetails.Where(o => o.UnitID == model.UnitID).ToListAsync() ?? new List<Database.Models.PRJ.TitledeedDetail>();
                model.Unit.Tower = DB.Towers.Where(o => o.ID == model.Unit.TowerID).FirstOrDefault();
                model.Unit.Floor = DB.Floors.Where(o => o.ID == model.Unit.FloorID).FirstOrDefault();

                var unitPriceModel = await DB.UnitPrices
               .Include(o => o.Booking)
               .ThenInclude(o => o.ReferContact)
               .Include(o => o.UnitPriceStage)
               .Where(o => o.BookingID == model.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement)
               .FirstOrDefaultAsync();

                AgreementDTO result = new AgreementDTO()
                {
                    Id = model.ID,
                    AgreementNo = model.AgreementNo,
                    Unit = PRJ.UnitDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Booking = await BookingDTO.CreateFromModelAsync(model.Booking, DB),
                    AgreementStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.AgreementStatus),
                    ContractDate = model.ContractDate,
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    SignAgreementDate = model.SignAgreementDate,
                    SignContractRequestUser = USR.UserListDTO.CreateFromModel(model.SignContractRequestUser),
                    SignContractRequestDate = model.SignContractRequestDate,
                    SignContractApprovedDate = model.SignContractApprovedDate,
                    IsSignContractApproved = model.IsSignContractApproved,
                    IsPrintApproved = model.IsPrintApproved,
                    PrintApprovedDate = model.PrintApprovedDate,
                    PrintApprovedBy = USR.UserListDTO.CreateFromModel(model.PrintApprovedBy),
                    AreaPricePerUnit = model.AreaPricePerUnit,
                    OffsetArea = model.OffsetArea,
                    HighRiseConstructionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.HighRiseConstructionStatus),
                    IsSellerPayLastDownInstallment = model.IsSellerPayLastDownInstallment,
                    SaleOfficerType = MST.MasterCenterDropdownDTO.CreateFromModel(model.Booking?.SaleOfficerType),
                    SaleUser = USR.UserListDTO.CreateFromModel(model.Booking?.SaleUser),
                    Agent = MST.AgentDropdownDTO.CreateFromModel(model.Booking?.Agent),
                    AgentEmployee = MST.AgentEmployeeDropdownDTO.CreateFromModel(model.Booking?.AgentEmployee),
                    ProjectSaleUser = USR.UserListDTO.CreateFromModel(model.Booking?.ProjectSaleUser),
                    Remark = model.Remark,
                    //UnitPrice = await AgreementPriceListDTO.CreateFromModelAsync(model.ID, DB),
                };

                #region ตรวจสอบว่าโปรเจ็คเปิดขายถึง 6 เดือนรึยัง
                if (model.Project?.ProductType?.Key == ProductTypeKeys.HighRise)
                {
                    //var test = model.Project.ProjectStartDate.Value.AddMonths(6);
                    //if (DateTime.Now.Date <= model.Project?.ProjectStartDate.Value.AddMonths(6).Date)
                    if (DateTime.Now.Date <= model.Project?.ProjectStartDate.Value.AddMonths(3).Date) //นิติกรรม && พี่ไก่ Confirm
                    {
                        if (result.Project.ProjectNo == "20042")
                        {
                            result.IsProjectLessThanSixMonth = true;
                        }
                        else
                        { 
                        result.IsProjectLessThanSixMonth = false;

                        }
                    }
                    else
                    {
                        result.IsProjectLessThanSixMonth = true;
                    }

                 
                }
                else
                {
                    result.IsProjectLessThanSixMonth = true;
                }

                #endregion

                #region Set Data Unit

                //var unitPrice = await DB.UnitPrices.Where(o => o.BookingID == model.BookingID && o.IsActive == true).FirstOrDefaultAsync();
                //result.UnitPrice = await DB.UnitPrices.Where(o => o.BookingID == model.BookingID && o.IsActive == true).FirstOrDefaultAsync();
                result.Unit.TitleDeed = PRJ.TitleDeedDTO.CreateFromModel(model.Unit.TitledeedDetails?.FirstOrDefault());
                result.Unit.AddOnArea = result.Unit.TitleDeed?.TitledeedArea != null ? (result.Unit.TitleDeed?.TitledeedArea ?? 0.00) - ((result.Unit?.TitleDeed?.TitledeedArea != null ? result.Unit?.TitleDeed?.TitledeedArea : result?.Unit?.SaleArea) ?? 0.00) : 0.00;
                //result.Unit.TitleDeed = PRJ.TitleDeedDTO.CreateFromModel(model.Unit.TitledeedDetails?.FirstOrDefault());
                //result.Unit.AddOnArea = (result.Unit.TitleDeed?.TitledeedArea ?? 0.00) - (result.Unit.SaleArea ?? 0.00);
                //try
                //{
                //    var unitPrices = await DB.UnitPrices.Where(o => o.BookingID == model.BookingID && o.UnitPriceStage.Key == UnitPriceStageKeys.Agreement).FirstOrDefaultAsync();
                //    //result.UnitPrice.InstallmentStartDate = unitPrices.InstallmentStartDate ?? null;
                //    result.UnitPrice = await AgreementPriceListDTO.CreateFromModelAsyncs(model.ID, DB);
                //    //result.UnitPrice.InstallmentEndDate = unitPrices.InstallmentEndDate ?? null;
                //    //result.UnitPrice.Installment = unitPrices.Installment ?? 0;
                //}
                //catch (Exception ex)
                //{
                //    var x = ex.Message;
                //    return null;
                //}
                if (result.Unit?.TitleDeed?.TitledeedArea != null)
                {
                    result.Unit.UsedArea = model.Unit.TitledeedDetails.Select(o => o.TitledeedArea).FirstOrDefault();
                }
                else
                {
                    result.Unit.UsedArea = model.Unit.SaleArea;
                }

                var productTypeId = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ProductType && o.Key == ProductTypeKeys.LowRise).Select(o => o.ID).FirstOrDefaultAsync();
                //if (result.Project != null)
                //{
                //    if (result.Project.ProductType?.Id == productTypeId)
                //    {
                //        // Case แนวราบ
                //        var pricelistId = await DB.PriceLists.Where(o => o.UnitID == model.UnitID).OrderByDescending(o => o.ActiveDate).Select(o => o.ID).FirstOrDefaultAsync();
                //        var pricelistItem = await DB.PriceListItems.Where(o => o.PriceListID == pricelistId && o.Order == 6).Select(o => o.PricePerUnitAmount).FirstOrDefaultAsync();
                //        result.AreaPricePerUnit = pricelistItem ?? 0;
                //    }
                //    else
                //    {
                //        // Case แนวสูง
                //        result.AreaPricePerUnit = ((unitPrice == null ? 0 : unitPrice.SellingPrice) / (decimal)(model.Unit.UsedArea == null ? 1 : model.Unit.UsedArea));
                //    }
                //}



                // Set สถานะโครงการ

                var productType = await DB.Projects.Where(o => o.ID == model.ProjectID).Select(o => o.ProductType).FirstOrDefaultAsync();
                if (productType != null && productType.Key == ProductTypeKeys.HighRise)
                {
                    //กรณีแนวสูง
                    var isTransferConfirmed = await DB.Transfers.Where(o => o.ProjectID == model.ProjectID && o.TransferConfirmedDate != null).FirstOrDefaultAsync();
                    if (isTransferConfirmed != null)
                    {
                        result.IsTransferConfirmed = true;
                    }
                    else
                    {
                        result.IsTransferConfirmed = false;
                    }

                    // เซ็ต Due วันที่โอนกรรมสิทธิ์จาก Master
                    if (model.Unit.Floor != null)
                    {
                        result.DueTransferDate = model.Unit.Floor.DueTransferDate;

                        // ตรวจสอบว่าวันที่ปัจจุบันน้อยกว่าวันที่โอนจาก Master 30 วันหรือไม่
                        var chkDate = DateTime.Now.AddDays(30);
                        if (chkDate >= result.DueTransferDate) result.IsUnLockDueTransDate = true;
                        else result.IsUnLockDueTransDate = false;
                    }


                }

                #endregion

                #region ตรวจสอบการตั้งเรื่องเปลี่ยนแปลงชื่อ

                var changeAgreementOwnerData = DB.ChangeAgreementOwnerWorkflows.Include(o => o.ChangeAgreementOwnerType).Where(e => e.AgreementID == model.ID && (e.IsApproved == false || e.IsApproved == null)).FirstOrDefault();
                if (changeAgreementOwnerData != null)
                {
                    result.ChangeAgreementOwnerWorkflowId = changeAgreementOwnerData.ID;
                    result.IsChangeAgreementOwnerWorkflow = true;
                    result.ChangeAgreementOwnerName = changeAgreementOwnerData.ChangeAgreementOwnerType.Name;
                    result.ChangeNameFee = changeAgreementOwnerData.Fee;

                    var sumOwnershipTransferFee = await DB.PaymentItems
                                                    .Include(o => o.MasterPriceItem)
                                                    .Include(o => o.Payment)
                                                    .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.OwnershipTransferFee
                                                      && o.Payment.BookingID == model.BookingID
                                                      && o.Payment.IsCancel == false)
                                                    .SumAsync(o => o.PayAmount);

                    if (sumOwnershipTransferFee < result.ChangeNameFee)
                        result.IsChangeNameFeePaid = true;
                    else
                        result.IsChangeNameFeePaid = false;
                }
                else
                {
                    result.IsChangeAgreementOwnerWorkflow = false;
                }

                #endregion

                #region ตรวจสอบตั้งเรื่องย้ายแปลง

                var changeUnitData2 = DB.ChangeUnitWorkflows.Where(o => o.ToAgreementID == model.ID && (o.IsRequestApproved == true || o.IsRequestApproved == null)).FirstOrDefault();
                if (changeUnitData2 != null)
                {
                    result.IsChangeUnitWorkflow2 = true;
                } else
                {
                    result.IsChangeUnitWorkflow2 = false;
                }
                var changeUnitData = DB.ChangeUnitWorkflows.Where(o => o.FromAgreementID == model.ID && (o.IsApproved == null && (o.IsRequestApproved == true || o.IsRequestApproved == null))).FirstOrDefault();
                if (changeUnitData != null)
                {
                    result.ChangeUnitWorkflowId = changeUnitData.ID;
                    result.IsChangeUnitWorkflow = true;

                }
                else
                {
                    result.IsChangeUnitWorkflow = false;
                }

                #endregion

                #region ตรวจสอบตั้งเรื่องยกเลิกสัญญา

                var cancelMemoData = DB.CancelMemos.Where(o => o.AgreementID == model.ID && o.ApproveDate == null).FirstOrDefault();
                if (cancelMemoData != null)
                {
                    result.IsCancelMemo = true;
                }
                else
                {
                    result.IsCancelMemo = false;
                }

                #endregion               

                #region ตรวจสอบตั้งเรื่องโปรโอน

                var transferPrmData = DB.TransferPromotions.Where(o => o.BookingID == model.BookingID).FirstOrDefault();
                if (transferPrmData != null)
                {
                    result.IsTransferPromotion = true;
                }
                else
                {
                    result.IsTransferPromotion = false;
                }

                #endregion               

                #region ค่าเงินคงเหลือจากแปลงเดิม (ใช้ที่หน้าย้ายแปลง)

                result.TotalReceivedAmount = await DB.PaymentItems
                                             .Include(o => o.MasterPriceItem)
                                             .Include(o => o.Payment)
                                             .Where(o => (o.MasterPriceItem.Key == MasterPriceItemKeys.BookingAmount
                                                               || o.MasterPriceItem.Key == MasterPriceItemKeys.AdvanceContractPayment
                                                               || o.MasterPriceItem.Key == MasterPriceItemKeys.ContractAmount
                                                               || o.MasterPriceItem.Key == MasterPriceItemKeys.InstallmentAmount
                                                               || o.MasterPriceItem.Key == MasterPriceItemKeys.TransferAmount)
                                                           && o.Payment.BookingID == model.BookingID
                                                           && o.Payment.IsCancel == false).SumAsync(o => o.PayAmount);

                #endregion

                #region ตรวจสอบตั้งเรื่องเปลี่ยนแปลงโปรโมชั่น

                var changePromotionWorkflow = DB.SalePromotions
                                       .Include(o => o.ChangePromotionWorkflow)
                                       .Include(o => o.SalePromotionStage)
                                       .Where(o => o.BookingID == model.BookingID
                                                && o.IsActive == false
                                                && o.SalePromotionStage.Key == SalePromotionStageKeys.Agreement
                                                && ((o.ChangePromotionWorkflow.IsApproved == null || o.ChangePromotionWorkflow.IsApproved != false) && (o.ChangePromotionWorkflow.ContractIsApproved == null || o.ChangePromotionWorkflow.ContractIsApproved != false))).FirstOrDefault();
                if (changePromotionWorkflow != null && changePromotionWorkflow.ChangePromotionWorkflow != null)
                {
                    result.ChangePromotionWorkflowId = changePromotionWorkflow.ChangePromotionWorkflow.ID;
                    result.IsChangePromotionWorkflow = true;
                }
                else
                {
                    result.IsChangePromotionWorkflow = false;
                }

                #endregion

                #region สร้างmin
                var workflow = await DB.MinPriceBudgetWorkflows
              .Include(o => o.Project)
              .Include(o => o.MinPriceBudgetWorkflowStage)
              .Include(o => o.MinPriceWorkflowType)
              .Include(o => o.BudgetPromotionType)
              .Include(o => o.Booking)
              .ThenInclude(o => o.Unit)
              .Where(o => o.BookingID == model.Booking.ID && o.IsCancelled == false && (o.MinPriceBudgetWorkflowStage.Key == "3" || o.MinPriceBudgetWorkflowStage.Key == "4"))
              .OrderByDescending(o => o.Created).FirstOrDefaultAsync();
                if (workflow != null)
                {
                    result.MinPriceBudgetWorkflow = MinPriceBudgetWorkflowDTO.CreateFromModel(workflow);
                }
                #endregion

                result.SaleArea = DB.Bookings.Where(o => o.ID == model.BookingID).Select(o => o.SaleArea).FirstOrDefault();
                if (unitPriceModel != null)
                {
                    var results = new AgreementPriceListDTO();
                    results.CashDiscount = unitPriceModel.CashDiscount.HasValue ? unitPriceModel.CashDiscount.Value : 0;
                    results.TransferDiscount = unitPriceModel.TransferDiscount.HasValue ? unitPriceModel.TransferDiscount.Value : 0;
                    results.SellingPrice = unitPriceModel.SellingPrice.HasValue ? unitPriceModel.SellingPrice.Value : 0;
                    results.NetSellingPrice = results.SellingPrice - results.CashDiscount;
                    if (productType != null && productType.Key == "1")
                    {
                        // Case แนวราบ
                        var pricelistId = await DB.PriceLists.Where(o => o.UnitID == model.UnitID).OrderByDescending(o => o.ActiveDate).Select(o => o.ID).FirstOrDefaultAsync();
                        var masterPriceItemId = await DB.MasterPriceItems.Where(o => o.Key == MasterPriceItemKeys.ExtraAreaPrice).Select(o => o.ID).FirstOrDefaultAsync();
                        var pricelistItem = await DB.PriceListItems.Where(o => o.PriceListID == pricelistId && o.MasterPriceItemID == masterPriceItemId).Select(o => o.PricePerUnitAmount).FirstOrDefaultAsync();
                        result.AreaPricePerUnit = pricelistItem ?? 0;
                        result.IsBuilding = false;
                    }
                    else
                    {
                        // Case แนวสูง
                        //result.AreaPricePerUnit = results.NetSellingPrice / Convert.ToDecimal(result.Unit?.TitleDeed?.TitledeedArea != null ? result.Unit?.TitleDeed?.TitledeedArea : result?.Unit?.SaleArea);
                        result.AreaPricePerUnit = Math.Round((results.NetSellingPrice / (decimal)(result.Booking.SaleArea != null ? result.Booking.SaleArea : 1)), 2);
                        result.IsBuilding = true;
                    }

                    // result.OffsetAreaPrice = Math.Ceiling(((result.AreaPricePerUnit ?? 0) * (decimal)(result.Unit?.AddOnArea ?? 1)));

                    result.OffsetAreaPrice = Math.Round(((result.AreaPricePerUnit ?? 0) * (decimal)(result.Unit?.TitleDeed?.TitledeedArea - result.Booking.SaleArea ?? 0)), 2);

                    // ตรวจสอบว่าจ่ายค่าทำสัญญาครบแล้วหรือไม่
                    var sumPayAgreementPrice = await DB.PaymentItems
                                                     .Include(o => o.MasterPriceItem)
                                                     .Include(o => o.Payment)
                                                     .Where(o => o.MasterPriceItem.Key == MasterPriceItemKeys.ContractAmount
                                                       && o.Payment.BookingID == model.BookingID
                                                       && o.Payment.IsCancel == false)
                                                     .SumAsync(o => o.PayAmount);

                    if (sumPayAgreementPrice >= (unitPriceModel.AgreementAmount.HasValue ? unitPriceModel.AgreementAmount.Value : 0))
                        result.IsCompletePayContractAmount = true;
                    else
                        result.IsCompletePayContractAmount = false;

                    //result.UnitPrice = await AgreementPriceListDTO.CreateFromUnitPricesModelAsync(model.ID, unitPriceModel, DB);
                }

                //Set Transfer Promotion Date
                result.TransferDateBefore = await DB.SalePromotions.Where(o => o.BookingID == model.BookingID).Select(o => o.TransferDateBefore).FirstOrDefaultAsync();

                // กรณี LC ลงนามแล้ว แต่ นิติกรรม ยังไม่ได้อนุมัติ
                #region กรณี LC ลงนามแล้ว แต่ นิติกรรม ยังไม่ได้อนุมัติ
                //if (result.SignContractRequestDate != null && result.IsSignContractApproved == false)
                //{
                //    result.IsDisableApvPrintBtn = true;
                //}
                //else
                //{
                //    result.IsDisableApvPrintBtn = false;
                //}
                #endregion

                #region Set Button

                if (result.AgreementStatus != null)
                {
                    #region Default Button

                    result.IsSaveContract = false;
                    result.IsCancelContract = false;
                    result.IsPrintContract = false;
                    result.IsChangeOwner = false;
                    result.IsChangeUnit = false;
                    result.IsViewFile = false;
                    result.IsAddFile = false;
                    result.IsApproveContract = false;
                    result.IsPayment = false;
                    result.IsCancelContract2 = false;
                    result.IsSignContractRequestPopup = false;
                    result.IsSignAgreementPopup = false;
                    result.IsSignContractApprovedPopup = false;
                    result.IsCancelPrintApproved = false;
                    result.IsCancelButton = false;
                    result.IsChangePromotionButton = false;
                    result.Isminprice = false;
                    result.IsPostponeTransferButton = false;

                    #endregion

                    if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForContract)
                    {
                        #region checkminprice

                        var appovemin = await DB.MinPriceBudgetWorkflows.Include(o => o.MinPriceBudgetWorkflowStage).Where(o => o.BookingID == result.Booking.Id && o.MinPriceBudgetWorkflowStage.Key == MinPriceBudgetWorkflowStageKeys.Booking && o.IsCancelled == false).FirstOrDefaultAsync();
                        if (appovemin != null)
                        {
                            var appveval = await DB.MinPriceBudgetApprovals.Where(o => o.MinPriceBudgetWorkflowID == appovemin.ID).ToListAsync();
                            if (appveval.TrueForAll(o => o.IsApproved == true))
                            {
                                result.IsSaveContract = true;
                            }
                            else
                            {
                                result.IsSaveContract = false;

                            }
                        }
                        else
                        {
                            result.IsSaveContract = true;
                        }
                        #endregion

                        // Status รอทำสัญญา
                        result.IsViewFile = true;
                        result.IsPrintApproved = false;

                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForSignContract)
                    {
                        //result.IsPayment = true;
                        result.IsPayment = string.IsNullOrEmpty(await DB.IsCanPayAsync(model.BookingID.Value));

                        // Status รอลงนามสัญญา
                        if (result.PrintApprovedDate == null)
                        {
                            result.IsSaveContract = true;
                            result.IsCancelContract = true;
                            result.IsViewFile = true;
                            //result.IsPayment = true;
                            result.IsPrintApproved = false;
                        }
                        else
                        {
                            result.IsCancelContract = true;
                            result.IsPrintContract = true;
                            result.IsViewFile = true;
                            //result.IsPayment = true;
                            //result.IsApproveContract = result.IsCompletePayContractAmount;
                            result.IsApproveContract = true;
                            result.IsCancelPrintApproved = true;
                            result.IsPrintApproved = true;
                            if (model.SignAgreementDate == null)
                            {
                                if (result.IsCompletePayContractAmount) //ชำระเงินทำสัญญาครบ
                                {
                                    result.IsSignAgreementPopup = true;
                                }
                            }
                        }
                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForTransfer)
                    {
                        //result.IsPayment = true;
                        result.IsPayment = string.IsNullOrEmpty(await DB.IsCanPayAsync(model.BookingID.Value));

                        // Status รอโอนกรรมสิทธิ์
                        if (result.SignAgreementDate != null)
                        {
                            #region check transferno
                            var checkno = await DB.Transfers.Where(o => o.AgreementID == model.ID).Select(o => o.TransferNo).FirstOrDefaultAsync();
                            if (checkno != null)
                            {
                                result.IsViewFile = true;
                                result.IsApproveContract = true;
                                result.IsPayment = false;

                                #region "   ใบบันทึกเลื่อนโอน   "                         
                                if (productType != null && productType.Key == ProductTypeKeys.LowRise) //แสดงปุ่มนี้เฉพาะโครงการแนวราบ
                                {
                                    var postponeTransfer = await DB.PostponeTransfers
                                                   .Where(o => o.AgreementID == model.ID)
                                                   .FirstOrDefaultAsync();
                                    if (postponeTransfer != null)
                                    {
                                        result.PostponeTransferId = postponeTransfer.ID;
                                        result.IsPostponeTransferButton = true;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                var checkPAppv = await DB.Agreements.Where(o => o.ID == model.ID).FirstOrDefaultAsync();
                                result.IsViewFile = true;
                                result.IsApproveContract = true;
                                result.IsCancelContract2 = true;
                                //result.IsCancelContract = true; // add by tae
                                result.IsChangeUnit = false;
                                result.IsCancelButton = true;
                                result.IsChangePromotionButton = false;
                                result.IsChangeOwner = false;
                                //result.IsPrintApproved = false;
                                //result.IsCancelPrintApproved = false;
                                //result.IsPrintApproved = checkPAppv?.IsPrintApproved == true ? true : false;
                                //result.IsCancelPrintApproved = checkPAppv?.IsPrintApproved == true ? true : false;
                                if (checkPAppv?.IsPrintApproved == false && (checkPAppv?.RefMigrateID1 == null || checkPAppv?.RefMigrateID2 == null))
                                {
                                    result.IsPrintApproved = false;
                                    result.IsCancelPrintApproved = false;
                                } 
                                else if (checkPAppv?.IsPrintApproved == true && (checkPAppv?.RefMigrateID1 == null || checkPAppv?.RefMigrateID2 == null))
                                {
                                    result.IsPrintApproved = true;
                                    result.IsCancelPrintApproved = true;
                                }
                                else if (checkPAppv?.IsPrintApproved == false && (checkPAppv?.RefMigrateID1 != null || checkPAppv?.RefMigrateID2 != null))
                                {
                                    result.IsPrintApproved = true;
                                    result.IsCancelPrintApproved = true;
                                }
                                else if (checkPAppv?.IsPrintApproved == true && (checkPAppv?.RefMigrateID1 != null || checkPAppv?.RefMigrateID2 != null))
                                {
                                    result.IsPrintApproved = true;
                                    result.IsCancelPrintApproved = true;
                                }
                                var rejectSignContract = false;
                                var signContract = await DB.SignContractWorkflows
                                                    .Include(o => o.SignContractAction)
                                                    .Where(o => o.AgreementID == model.ID)
                                                    .OrderByDescending(o => o.ActionDate)
                                                    .FirstOrDefaultAsync();

                                if (signContract != null && signContract.SignContractAction.Key == SignContractActionKeys.RejectSignContract)
                                {
                                    rejectSignContract = true;
                                }

                                if (model.IsSignContractApproved == false && rejectSignContract)
                                {
                                    result.IsSignContractRequestPopup = true;
                                }
                                else if (model.IsSignContractApproved == false)
                                {
                                    result.IsSignContractApprovedPopup = true;
                                }

                                result.IsSignAgreementPopup = false;
                                if (model.SignContractApprovedDate != null)
                                {

                                    if (result.IsTransferPromotion ?? false)
                                    {
                                        result.IsChangePromotionButton = false;
                                        result.IsChangeUnit = false;
                                    }
                                    else
                                    {
                                        result.IsChangePromotionButton = true;
                                        result.IsChangeUnit = true;
                                    }
                                    result.IsChangeOwner = true;
                                    result.IsCancelButton = true;
                                    result.IsViewFile = true;
                                }

                                #region "   ใบบันทึกเลื่อนโอน   "                         
                                if (productType != null && productType.Key == ProductTypeKeys.LowRise) //แสดงปุ่มนี้เฉพาะโครงการแนวราบ
                                {
                                    var postponeTransfer = await DB.PostponeTransfers
                                                   .Where(o => o.AgreementID == model.ID)
                                                   .FirstOrDefaultAsync();
                                    if (postponeTransfer != null)
                                    {
                                        result.PostponeTransferId = postponeTransfer.ID;
                                        result.IsPostponeTransferButton = true;

                                        if (postponeTransfer.LCMApproveDate == null)
                                        {
                                            result.IsPostponeTransferPopup = true;

                                            //
                                            result.IsChangeUnit = false;
                                            result.IsCancelButton = false;
                                            result.IsChangePromotionButton = false;
                                            result.IsChangeOwner = false;
                                        }
                                    }
                                    else
                                    {
                                        if (model.SignAgreementDate.Value.Date >= new DateTime(2020, 9, 2))
                                        {
                                            //- แสดงหลังจากที่ LC กดลงนามสัญญา
                                            //- วันที่ LC กดลงนามสัญา - ดิวโอนในสัญญา น้อยกว่า 7 วัน หรือ พอดีกับดิวนัดโอนในสัญญา
                                            //- วันที่ LC กดลงนามสัญา - ดิวโอนในสัญญา เลยดิวโอนในสัญญาแล้ว
                                            if ((model.TransferOwnershipDate.Value.Date - model.SignAgreementDate.Value.Date).TotalDays >= 0 
                                                    && (model.TransferOwnershipDate.Value.Date - model.SignAgreementDate.Value.Date).TotalDays < 7)
                                            {
                                                result.IsPostponeTransferPopup2 = true;
                                            }

                                            if (model.TransferOwnershipDate.Value.Date < model.SignAgreementDate.Value.Date)
                                            {
                                                result.IsPostponeTransferButton = true;
                                                result.IsPostponeTransferPopup = true;

                                                //
                                                result.IsChangeUnit = false;
                                                result.IsCancelButton = true;
                                                result.IsCancelContract2 = true;
                                                result.IsChangePromotionButton = false;
                                                result.IsChangeOwner = false;
                                            }
                                        }
                                    }

                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.Transfer)
                    {
                        // Status โอนกรรมสิทธิ์แล้ว
                        result.IsViewFile = true;
                        result.IsApproveContract = true;
                        result.IsPrintApproved = true;
                        #region "   ใบบันทึกเลื่อนโอน   "                         
                        if (productType != null && productType.Key == ProductTypeKeys.LowRise) //แสดงปุ่มนี้เฉพาะโครงการแนวราบ
                        {
                            var postponeTransfer = await DB.PostponeTransfers
                                           .Where(o => o.AgreementID == model.ID)
                                           .FirstOrDefaultAsync();
                            if (postponeTransfer != null)
                            {
                                result.PostponeTransferId = postponeTransfer.ID;
                                result.IsPostponeTransferButton = true;
                            }                           
                        }
                        #endregion
                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForApproveUnit)
                    {

                        if (result.SignAgreementDate != null)
                        {
                            result.IsChangeUnit = true;
                            result.IsViewFile = true;
                            result.IsApproveContract = true;
                            result.IsPrintApproved = true;
                        }
                        else
                        {
                            result.IsPrintApproved = true;
                            result.IsChangeUnit = false;
                            result.IsViewFile = false;
                            result.IsApproveContract = false;

                        }

                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForApproveChangePromotion)
                    {
                        // Status รออนุมัติเปลียนแปลงโปร
                        result.IsChangePromotionButton = true;
                        result.IsViewFile = true;
                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForApprovePriceList)
                    {
                        result.IsPrintApproved = true;
                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForApproveMinPrice)
                    {
                        result.Isminprice = true;

                        if (result.IsChangePromotionWorkflow)
                        {
                            // Status รออนุมัติเปลียนแปลงโปร
                            result.IsChangePromotionButton = true;
                            result.IsViewFile = true;
                            result.IsPrintApproved = true;
                        }
                        else
                        {
                            if (result.IsChangeUnitWorkflow)
                            {
                                // Status รออนุมัติย้ายแปลง
                                result.IsChangeUnit = true;
                                result.IsViewFile = true;
                                result.IsApproveContract = true;
                                result.IsPrintApproved = true;
                            }
                        }
                        result.IsPrintApproved = true;
                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForApproveBudgetPromotion)
                    {
                        result.Isminprice = true;

                        if (result.IsChangePromotionWorkflow)
                        {
                            // Status รออนุมัติเปลียนแปลงโปร
                            result.IsChangePromotionButton = true;
                            result.IsViewFile = true;
                            result.IsPrintApproved = true;
                        }
                        else
                        {
                            if (result.IsChangeUnitWorkflow)
                            {
                                // Status รออนุมัติย้ายแปลง
                                result.IsChangeUnit = true;
                                result.IsViewFile = true;
                                result.IsApproveContract = true;
                                result.IsPrintApproved = true;
                            }
                        }
                        result.IsPrintApproved = true;
                    }
                    else if (result.AgreementStatus.Key == AgreementStatusKeys.WaitingForApproveMinPriceAndBudgetPromotion)
                    {
                        result.Isminprice = true;
                        if (result.IsChangePromotionWorkflow)
                        {
                            // Status รออนุมัติเปลียนแปลงโปร
                            result.IsChangePromotionButton = true;
                            result.IsViewFile = true;
                            result.IsPrintApproved = true;
                        }
                        else
                        {
                            if (result.IsChangeUnitWorkflow)
                            {
                                // Status รออนุมัติย้ายแปลง
                                result.IsChangeUnit = true;
                                result.IsViewFile = true;
                                result.IsApproveContract = true;
                                result.IsPrintApproved = true;
                            }
                        }
                        result.IsPrintApproved = true;
                    }

                    #region เช็คการตั้งเรื่องต่างๆ 
                    //if (result.IsPostponeTransferPopup)
                    //{
                    //    result.IsCancelButton = false;
                    //    result.IsSignContractApproved = false;
                    //    result.IsApproveContract = false;
                    //    result.IsChangeUnit = false;
                    //    result.IsSaveContract = false;
                    //    result.IsCancelContract = false;
                    //    result.IsPrintApproved = false;
                    //    result.IsCancelPrintApproved = false;
                    //    result.IsChangePromotionButton = false;
                    //}
                    //else if (result.IsChangeAgreementOwnerWorkflow)
                    //{
                    //    result.IsCancelButton = false;
                    //    result.IsSignContractApproved = false;
                    //    result.IsApproveContract = false;
                    //    result.IsChangeUnit = false;
                    //    result.IsSaveContract = false;
                    //    result.IsCancelContract = false;
                    //    result.IsPrintApproved = false;
                    //    result.IsCancelPrintApproved = false;
                    //    result.IsChangePromotionButton = false;
                    //}
                    //else if (result.IsChangeUnitWorkflow)
                    //{
                    //    result.IsChangeOwner = false;
                    //    result.IsSignContractApproved = false;
                    //    result.IsApproveContract = false;
                    //    result.IsCancelButton = false;
                    //    result.IsSaveContract = false;
                    //    result.IsCancelContract = false;
                    //    result.IsPrintApproved = false;
                    //    result.IsCancelPrintApproved = false;
                    //    result.IsChangePromotionButton = false;
                    //}
                    //else if (result.IsCancelMemo)
                    //{
                    //    result.IsChangeOwner = false;
                    //    result.IsSignContractApproved = false;
                    //    result.IsApproveContract = false;
                    //    result.IsChangeUnit = false;
                    //    result.IsSaveContract = false;
                    //    result.IsCancelContract = false;
                    //    result.IsPrintApproved = false;
                    //    result.IsCancelPrintApproved = false;
                    //    result.IsChangePromotionButton = false;
                    //}
                    //else if (result.IsChangePromotionWorkflow)
                    //{
                    //    result.IsChangeOwner = false;
                    //    result.IsSignContractApproved = false;
                    //    result.IsApproveContract = false;
                    //    result.IsCancelButton = false;
                    //    result.IsSaveContract = false;
                    //    result.IsCancelContract = false;
                    //    result.IsPrintApproved = false;
                    //    result.IsCancelPrintApproved = false;
                    //}
                    #endregion

                }

                #endregion

                #region มาจากการย้ายแปลง

                var fromChangeUnitData = DB.ChangeUnitWorkflows.Where(o => o.ToAgreementID == model.ID && o.IsApproved == true).Include(o => o.ToAgreement).ThenInclude(o => o.Project.ProductType).FirstOrDefault();
                if (fromChangeUnitData != null)
                {
                    if (fromChangeUnitData.ToAgreement.Project.ProductType.Key == ProductTypeKeys.HighRise)
                    {
                        result.FromChangeUnitWorkflowId = fromChangeUnitData.ID;
                        result.IsFormChangeUnitWorkflow = true;
                        result.IsCancelContract = false;
                    }
                    else
                    {
                        result.FromChangeUnitWorkflowId = fromChangeUnitData.ID;
                        result.IsFormChangeUnitWorkflow = false;
                        result.IsCancelContract = false;
                    }
                }
                else
                {
                    result.IsFormChangeUnitWorkflow = false;
                }

                #endregion
                #region Combine Unit
                var combineUnit = await DB.CombineUnits.Include(x => x.CombineStatus).Include(x=>x.Unit).Include(x=>x.UnitCombine).Where(x => x.CombineStatus.Key.Equals(MasterCombineStatusKeys.Approve) && (x.UnitID == model.UnitID || x.UnitIDCombine == model.UnitID)).FirstOrDefaultAsync();
                if(combineUnit != null)
                { 
                    var unitcombine = combineUnit.UnitID == model.UnitID ? combineUnit.UnitCombine: combineUnit.Unit;
                    result.UniCombine = UnitDropdownDTO.CreateFromModel(unitcombine);
                }
                #endregion
                return result;
            }
            else
            {
                return null;
            }
        }

        public static AgreementDTO CreateFromModelForPayment(Agreement model)
        {
            if (model != null)
            {
                AgreementDTO result = new AgreementDTO()
                {
                    Id = model.ID,
                    AgreementNo = model.AgreementNo,
                    Unit = PRJ.UnitDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    AgreementStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.AgreementStatus),
                    ContractDate = model.ContractDate,
                    TransferOwnershipDate = model.TransferOwnershipDate,
                    SignAgreementDate = model.SignAgreementDate,
                    SignContractRequestUser = USR.UserListDTO.CreateFromModel(model.SignContractRequestUser),
                    SignContractRequestDate = model.SignContractRequestDate,
                    SignContractApprovedDate = model.SignContractApprovedDate,
                    IsSignContractApproved = model.IsSignContractApproved,
                    IsPrintApproved = model.IsPrintApproved,
                    PrintApprovedDate = model.PrintApprovedDate,
                    PrintApprovedBy = USR.UserListDTO.CreateFromModel(model.PrintApprovedBy),
                    AreaPricePerUnit = model.AreaPricePerUnit,
                    OffsetArea = model.OffsetArea,
                    HighRiseConstructionStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.HighRiseConstructionStatus),
                    IsSellerPayLastDownInstallment = model.IsSellerPayLastDownInstallment,
                    Remark = model.Remark,
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static async Task<AgreementDTO> CreateFromModelByBookingAsync(Booking model, DatabaseContext DB)
        {
            if (model != null)
            {
                var agreementStatus = await DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.AgreementStatus && o.Key == AgreementStatusKeys.WaitingForContract).FirstOrDefaultAsync();

                AgreementDTO result = new AgreementDTO()
                {
                    AgreementNo = "",
                    Unit = PRJ.UnitDTO.CreateFromModel(model.Unit),
                    Project = PRJ.ProjectDTO.CreateFromModel(model.Project),
                    Booking = await BookingDTO.CreateFromModelAsync(model, DB),
                    AgreementStatus = MST.MasterCenterDropdownDTO.CreateFromModel(agreementStatus)
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
