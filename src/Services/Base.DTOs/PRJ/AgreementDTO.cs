using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;
using ErrorHandling;
using Database.Models;
using Database.Models.MST;

namespace Base.DTOs.PRJ
{
    public class AgreementDTO : BaseDTO
    {
        /// <summary>
        /// ผู้รับมอบอำนาจ 1 (TH)
        /// </summary>
        [Description("ผู้รับมอบอำนาจ 1 (TH)")]
        public string AttorneyNameTH1 { get; set; }
        /// <summary>
        /// ผู้รับมอบอำนาจ 2 (TH)
        /// </summary>
        [Description("ผู้รับมอบอำนาจ 2 (TH)")]
        public string AttorneyNameTH2 { get; set; }
        /// <summary>
        /// ผู้รับมอบอำนาจ 1 (EN)
        /// </summary>
        [Description("ผู้รับมอบอำนาจ 1 (EN)")]
        public string AttorneyNameEN1 { get; set; }
        /// <summary>
        /// ผู้รับมอบอำนาจ 2 (EN)
        /// </summary>
        [Description("ผู้รับมอบอำนาจ 2 (EN)")]
        public string AttorneyNameEN2 { get; set; }
        /// <summary>
        /// พยาน 1 (TH)
        /// </summary>
        [Description("พยาน 1 (TH)")]
        public string WitnessTH1 { get; set; }
        /// <summary>
        /// พยาน 2 (TH)
        /// </summary>
        [Description("พยาน 2 (TH)")]
        public string WitnessTH2 { get; set; }
        /// <summary>
        /// พยาน 1 (EN)
        /// </summary>
        [Description("พยาน 1 (EN)")]
        public string WitnessEN1 { get; set; }
        /// <summary>
        /// พยาน 2 (EN)
        /// </summary>
        [Description("พยาน 2 (EN)")]
        public string WitnessEN2 { get; set; }
        /// <summary>
        /// ผู้รับมอบอำนาจขอปลด
        /// </summary>
        [Description("ผู้รับมอบอำนาจขอปลด")]
        public string PreferApproveName { get; set; }
        /// <summary>
        /// ตำแหน่งผู้รับอำนาจขอปลด
        /// </summary>
        [Description("ตำแหน่งผู้รับอำนาจขอปลด")]
        public string PreferApprovePosition { get; set; }
        /// <summary>
        /// ผู้รับมอบอำนาจโอนกรรมสิทธิ์
        /// </summary>
        [Description("ผู้รับมอบอำนาจโอนกรรมสิทธิ์")]
        public MST.AttorneyTransferDTO AttorneyNameTransfer { get; set; }
        /// <summary>
        /// นิติบุคคล
        /// Master/api/LegalEntities/DropdownList
        /// </summary>
        [Description("นิติบุคคล")]
        public MST.LegalEntityDropdownDTO LegalEntity { get; set; }
        /// <summary>
        /// อัตรากองทุนคอนโด (บาท)
        /// </summary>
        public decimal? CondoFundRate { get; set; }
        /// <summary>
        /// ค่าเบี้ยประกันอาคาร (บาท)
        /// </summary>
        public decimal? BuildingInsurance { get; set; }
        /// <summary>
        /// อัตราค่าส่วนกลาง (บาท)
        /// </summary>
        public decimal? PublicFundRate { get; set; }
        /// <summary>
        /// อัตราค่าส่วนกลาง AP ช่วยจ่าย (บาท)
        /// </summary>
        public decimal? PublicFundRateAP { get; set; }
        /// <summary>
        /// จำนวนเดือนที่เก็บค่าส่วนกลาง 
        /// </summary>
        public int? PublicFundMonths { get; set; }
        /// <summary>
        /// จำนวนเดือนที่เก็บค่าส่วนกลาง AP ช่วยจ่าย
        /// </summary>
        public int? PublicFundMonthsAP { get; set; }
        /// <summary>
        ///  ค่าธรรมเนียทการย้ายห้อง (บาท)
        /// </summary>
        public decimal? RoomTransferFee { get; set; }
        /// <summary>
        ///  ค่าธรรมเนียทการเปลี่ยนชื่อ (บาท)
        /// </summary>
        public decimal? ChangeNameFee { get; set; }
        /// <summary>
        ///  ค่าปรับอาศัยอยู่ร่วมกัน (บาท)
        /// </summary>
        public decimal? VisitFine { get; set; }
        /// <summary>
        ///  ค่าปรับอาศัยอยู่ร่วมกัน (วัน)
        /// </summary>
        public int? VisitFineDay { get; set; }
        /// <summary>
        ///  อัตราค่าปรับโอนกรรมสิทธิ์ล่าช้า (ร้อยละ)
        /// </summary>
        public decimal? DelayTransfer { get; set; }
        /// <summary>
        ///  จำนวนที่จอดรถ (คัน)
        /// </summary>
        public int? ParkingUnits { get; set; }
        /// <summary>
        ///  รวมจอดรถซ้อนคัน
        /// </summary>
        public bool IsIncludeDoubleParking { get; set; }
        /// <summary>
        ///  วันที่หนังสือมอบอำนาจ
        /// </summary>
        public DateTime? AttorneyIssueDate { get; set; }
        /// <summary>
        ///  วันที่สิ้นสุดสาธารณะ
        /// </summary>
        public DateTime? EndPublicDate { get; set; }
        /// <summary>
        ///  วันที่หนังสือกรรมสิทธิ์ห้องชุด
        /// </summary>
        public DateTime? OwnerShipDate { get; set; }
        /// <summary>
        ///  ผ่าน EIA 
        /// </summary>
        public bool? EIAApproved { get; set; }
        /// <summary>
        ///  วันที่ผ่าน EIA
        /// </summary>
        public DateTime? EIAApprovedDate { get; set; }
        /// <summary>
        /// เลขที่ใบรับคำขออนุญาตจัดสรรที่ดิน
        /// </summary>
        public string PreLicenseLandNo { get; set; }
        /// <summary>
        /// วันที่ออกใบรับคำขออนุญาตจัดสรรที่ดิน
        /// </summary>
        public DateTime? PreLicenseLandIssueDate { get; set; }
        /// <summary>
        /// วันที่หมดอายุใบรับคำขออนุญาตจัดสรรที่ดิน
        /// </summary>
        public DateTime? PreLicenseLandExpireDate { get; set; }
        /// <summary>
        /// เลขที่ใบอนุญาตจัดสรรที่ดิน
        /// </summary>
        public string LicenseLandNo { get; set; }
        /// <summary>
        /// วันที่ออกใบอนุญาตจัดสรรที่ดิน
        /// </summary>
        public DateTime? LicenseLandIssueDate { get; set; }
        /// <summary>
        /// วันที่หมดอายุใบอนุญาตจัดสรรที่ดิน
        /// </summary>
        public DateTime? LicenseLandExpireDate { get; set; }
        /// <summary>
        /// ไม่จัดสรรที่ดิน
        /// </summary>
        public bool IsNotLicenseLand { get; set; }
        /// <summary>
        /// เลขที่ใบอนุญาตก่อสร้างโครงการ
        /// </summary>
        public string LicenseProductNo { get; set; }
        /// <summary>
        /// วันที่ออกใบอนุญาตก่อสร้างโครงการ
        /// </summary>
        public DateTime? LicenseProductIssueDate { get; set; }
        /// <summary>
        /// วันที่หมดอายุใบอนุญาตก่อสร้างโครงการ
        /// </summary>
        public DateTime? LicenseProductExpireDate { get; set; }
        /// <summary>
        /// หมายเหตุใบอนุญาตก่อสร้างโครงการ
        /// </summary>
        public string LicenseProductRemark { get; set; }
        /// <summary>
        ///การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด สำหรับผู้ซื้อ
        /// </summary>
        public bool IsPrintAgreementForBuyer { get; set; }
        /// <summary>
        ///การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด สำหรับผู้ขาย
        /// </summary>
        public bool IsPrintAgreementForSeller { get; set; }
        /// <summary>
        ///การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด สำหรับสรรพกร
        /// </summary>
        public bool IsPrintAgreementForRevenue { get; set; }
        /// <summary>
        ///การพิมพ์เอกสาร หนังสือสัญญาขายที่ดิน/ห้องชุด ใบเปล่า
        /// </summary>
        public bool IsPrintAgreementEmpty { get; set; }

        public USR.UserListDTO AGOwner { get; set; }
        public List<KCashCardTransferDTO> KCC { get; set; }
        public bool? IsLockPublicFundRate { get; set; }
        public DateTime? LockPublicFundRateDate { get; set; }
        public string ProductTypeKey { get; set; }
        public DateTime? ExpectedEnvironmentalApprovalDate { get; set; }
        public DateTime? CondoConstructionPermitSubmitDate { get; set; }
        public DateTime? ExpectedPermitReceiveDate { get; set; }
        public string? CondoConstructionPermitNo { get; set; }
        public DateTime? CondoConstructionPermitDate { get; set; }
        public DateTime? CondoConstructionPermitExpireDate { get; set; }
        public string? CondoConstructionPermitRemark { get; set; }

        public static AgreementDTO CreateFromModel(AgreementConfig model)
        {
            if (model != null)
            {
                var result = new AgreementDTO()
                {
                    Id = model.ID,
                    AttorneyNameTH1 = model.AttorneyNameTH1,
                    AttorneyNameEN1 = model.AttorneyNameEN1,
                    AttorneyNameTH2 = model.AttorneyNameTH2,
                    AttorneyNameEN2 = model.AttorneyNameEN2,
                    WitnessTH1 = model.WitnessTH1,
                    WitnessEN1 = model.WitnessEN1,
                    WitnessTH2 = model.WitnessTH2,
                    WitnessEN2 = model.WitnessEN2,
                    PreferApproveName = model.PreferApproveName,
                    PreferApprovePosition = model.PreferApprovePosition,
                    AttorneyNameTransfer = MST.AttorneyTransferDTO.CreateFromModel(model.AttorneyNameTransfer),
                    LegalEntity = MST.LegalEntityDropdownDTO.CreateFromModel(model.LegalEntity),
                    CondoFundRate = model.CondoFundRate,
                    BuildingInsurance = model.BuildingInsurance,
                    PublicFundRate = model.PublicFundRate,
                    PublicFundRateAP = model.PublicFundRateAP,
                    PublicFundMonths = model.PublicFundMonths,
                    PublicFundMonthsAP = model.PublicFundMonthsAP,
                    RoomTransferFee = model.RoomTransferFee,
                    ChangeNameFee = model.ChangeNameFee,
                    VisitFine = model.VisitFine,
                    VisitFineDay = model.VisitFineDay,
                    DelayTransfer = model.DelayTransfer,
                    ParkingUnits = model.ParkingUnits,
                    IsIncludeDoubleParking = model.IsIncludeDoubleParking,
                    AttorneyIssueDate = model.AttorneyIssueDate,
                    EndPublicDate = model.EndPublicDate,
                    OwnerShipDate = model.OwnerShipDate,
                    EIAApprovedDate = model.EIAApprovedDate,
                    PreLicenseLandNo = model.PreLicenseLandNo,
                    PreLicenseLandIssueDate = model.PreLicenseLandIssueDate,
                    PreLicenseLandExpireDate = model.PreLicenseLandExpireDate,
                    LicenseLandNo = model.LicenseLandNo,
                    LicenseLandIssueDate = model.LicenseLandIssueDate,
                    LicenseLandExpireDate = model.LicenseLandExpireDate,
                    IsNotLicenseLand = model.IsNotLicenseLand,
                    LicenseProductNo = model.LicenseProductNo,
                    LicenseProductIssueDate = model.LicenseProductIssueDate,
                    LicenseProductExpireDate = model.LicenseProductExpireDate,
                    LicenseProductRemark = model.LicenseProductRemark,
                    IsPrintAgreementEmpty = model.IsPrintAgreementEmpty,
                    IsPrintAgreementForBuyer = model.IsPrintAgreementForBuyer,
                    IsPrintAgreementForRevenue = model.IsPrintAgreementForRevenue,
                    IsPrintAgreementForSeller = model.IsPrintAgreementForSeller,
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
        public static AgreementDTO CreateFromModels(AgreementConfig model, AgreementProjectOwner AGOwner, List<KCashCardTransfer> KCC, Project pdType)
        {
            if (model != null)
            {
                var result = new AgreementDTO()
                {
                    Id = model.ID,
                    AttorneyNameTH1 = model.AttorneyNameTH1,
                    AttorneyNameEN1 = model.AttorneyNameEN1,
                    AttorneyNameTH2 = model.AttorneyNameTH2,
                    AttorneyNameEN2 = model.AttorneyNameEN2,
                    WitnessTH1 = model.WitnessTH1,
                    WitnessEN1 = model.WitnessEN1,
                    WitnessTH2 = model.WitnessTH2,
                    WitnessEN2 = model.WitnessEN2,
                    PreferApproveName = model.PreferApproveName,
                    PreferApprovePosition = model.PreferApprovePosition,
                    AttorneyNameTransfer = MST.AttorneyTransferDTO.CreateFromModel(model.AttorneyNameTransfer),
                    LegalEntity = MST.LegalEntityDropdownDTO.CreateFromModel(model.LegalEntity),
                    CondoFundRate = model.CondoFundRate,
                    BuildingInsurance = model.BuildingInsurance,
                    PublicFundRate = model.PublicFundRate,
                    PublicFundRateAP = model.PublicFundRateAP,
                    PublicFundMonths = model.PublicFundMonths,
                    PublicFundMonthsAP = model.PublicFundMonthsAP,
                    RoomTransferFee = model.RoomTransferFee,
                    ChangeNameFee = model.ChangeNameFee,
                    VisitFine = model.VisitFine,
                    VisitFineDay = model.VisitFineDay,
                    DelayTransfer = model.DelayTransfer,
                    ParkingUnits = model.ParkingUnits,
                    IsIncludeDoubleParking = model.IsIncludeDoubleParking,
                    AttorneyIssueDate = model.AttorneyIssueDate,
                    EndPublicDate = model.EndPublicDate,
                    OwnerShipDate = model.OwnerShipDate,
                    EIAApprovedDate = model.EIAApprovedDate,
                    PreLicenseLandNo = model.PreLicenseLandNo,
                    PreLicenseLandIssueDate = model.PreLicenseLandIssueDate,
                    PreLicenseLandExpireDate = model.PreLicenseLandExpireDate,
                    LicenseLandNo = model.LicenseLandNo,
                    LicenseLandIssueDate = model.LicenseLandIssueDate,
                    LicenseLandExpireDate = model.LicenseLandExpireDate,
                    IsNotLicenseLand = model.IsNotLicenseLand,
                    LicenseProductNo = model.LicenseProductNo,
                    LicenseProductIssueDate = model.LicenseProductIssueDate,
                    LicenseProductExpireDate = model.LicenseProductExpireDate,
                    LicenseProductRemark = model.LicenseProductRemark,
                    IsPrintAgreementEmpty = model.IsPrintAgreementEmpty,
                    IsPrintAgreementForBuyer = model.IsPrintAgreementForBuyer,
                    IsPrintAgreementForRevenue = model.IsPrintAgreementForRevenue,
                    IsPrintAgreementForSeller = model.IsPrintAgreementForSeller,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    KCC = KCashCardTransferDTO.CreateFromModel(KCC),
                    IsLockPublicFundRate = model.IsLockPublicFundRate,
                    LockPublicFundRateDate = model.LockPublicFundRateDate,
                    ProductTypeKey = pdType.ProductType?.Key,
                    ExpectedEnvironmentalApprovalDate = model.ExpectedEnvironmentalApprovalDate,
                    CondoConstructionPermitSubmitDate = model.CondoConstructionPermitSubmitDate,
                    ExpectedPermitReceiveDate = model.ExpectedPermitReceiveDate,
                    CondoConstructionPermitNo = model.CondoConstructionPermitNo,
                    CondoConstructionPermitDate = model.CondoConstructionPermitDate,
                    CondoConstructionPermitExpireDate = model.CondoConstructionPermitExpireDate,
                    CondoConstructionPermitRemark = model.CondoConstructionPermitRemark,

                };
                if (AGOwner != null)
                {
                    result.AGOwner = USR.UserListDTO.CreateFromModel(AGOwner.User);
                }
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!string.IsNullOrEmpty(this.AttorneyNameTH1))
            {
                if (!this.AttorneyNameTH1.CheckLang(true, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.AttorneyNameTH1)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.AttorneyNameTH2))
            {
                if (!this.AttorneyNameTH2.CheckLang(true, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.AttorneyNameTH2)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.AttorneyNameEN1))
            {
                if (!this.AttorneyNameEN1.CheckLang(false, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.AttorneyNameEN1)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }


            if (!string.IsNullOrEmpty(this.AttorneyNameEN2))
            {
                if (!this.AttorneyNameEN2.CheckLang(false, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.AttorneyNameEN2)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.WitnessTH1))
            {
                if (!this.WitnessTH1.CheckLang(true, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.WitnessTH1)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.WitnessTH2))
            {
                if (!this.WitnessTH2.CheckLang(true, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.WitnessTH2)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.WitnessEN1))
            {
                if (!this.WitnessEN1.CheckLang(false, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.WitnessEN1)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.WitnessEN2))
            {
                if (!this.WitnessEN2.CheckLang(false, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0008").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.WitnessEN2)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.PreferApproveName))
            {
                if (!this.PreferApproveName.CheckLang(true, false, false, false, null, " "))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.PreferApproveName)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (!string.IsNullOrEmpty(this.PreferApprovePosition))
            {
                if (!this.PreferApprovePosition.CheckLang(true, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0017").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(AgreementDTO.PreferApprovePosition)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (this.AttorneyNameTransfer != null)
            {
                if (!string.IsNullOrEmpty(this.AttorneyNameTransfer.Atty_FullName))
                {
                    if (!this.AttorneyNameTransfer.Atty_FullName.CheckLang(true, false, false, false, null, " "))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                        string desc = this.GetType().GetProperty(nameof(AgreementDTO.AttorneyNameTransfer)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (LegalEntity != null)
            {
                if (!string.IsNullOrEmpty(this.LegalEntity.NameTH))
                {
                    if (!this.LegalEntity.NameTH.CheckLang(true, false, false, false, null, " "))
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                        string desc = typeof(MST.LegalEntityDropdownDTO).GetProperty(nameof(MST.LegalEntityDropdownDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                        var msg = errMsg.Message.Replace("[field]", desc);
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }


            if (ex.HasError)
            {
                throw ex;
            }
        }

        public async Task ValidateUpdateAsync(DatabaseContext db, Guid projectID)
        {
            ValidateException ex = new ValidateException();

            if (LegalEntity != null)
            {
                var OldLegalEntity = await db.AgreementConfigs.Where(x => x.ProjectID != projectID
                && x.LegalEntityID == this.LegalEntity.Id
                ).FirstOrDefaultAsync();
                if (OldLegalEntity != null)
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0020").FirstAsync();
                    var msg = "ไม่สามารถใช้นิติบุคคลได้ เนื่องจากนิติบุคคลนี้ถูกใช้แล้ว";
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref AgreementConfig model)
        {
            model.AttorneyNameTH1 = this.AttorneyNameTH1;
            model.AttorneyNameEN1 = this.AttorneyNameEN1;
            model.AttorneyNameTH2 = this.AttorneyNameTH2;
            model.AttorneyNameEN2 = this.AttorneyNameEN2;
            model.WitnessTH1 = this.WitnessTH1;
            model.WitnessEN1 = this.WitnessEN1;
            model.WitnessTH2 = this.WitnessTH2;
            model.WitnessEN2 = this.WitnessEN2;
            model.PreferApproveName = this.PreferApproveName;
            model.PreferApprovePosition = this.PreferApprovePosition;
            model.AttorneyNameTransferID = this.AttorneyNameTransfer?.Id;
            model.LegalEntityID = this.LegalEntity?.Id;
            model.CondoFundRate = this.CondoFundRate;
            model.BuildingInsurance = this.BuildingInsurance;
            model.PublicFundRate = this.PublicFundRate;
            model.PublicFundRateAP = this.PublicFundRateAP;
            model.PublicFundMonths = this.PublicFundMonths;
            model.PublicFundMonthsAP = this.PublicFundMonthsAP;
            model.RoomTransferFee = this.RoomTransferFee;
            model.ChangeNameFee = this.ChangeNameFee;
            model.VisitFine = this.VisitFine;
            model.VisitFineDay = this.VisitFineDay;
            model.DelayTransfer = this.DelayTransfer;
            model.ParkingUnits = this.ParkingUnits;
            model.IsIncludeDoubleParking = this.IsIncludeDoubleParking;
            model.AttorneyIssueDate = this.AttorneyIssueDate;
            model.EndPublicDate = this.EndPublicDate;
            model.OwnerShipDate = this.OwnerShipDate;
            model.EIAApprovedDate = this.EIAApprovedDate;
            model.PreLicenseLandNo = this.PreLicenseLandNo;
            model.PreLicenseLandIssueDate = this.PreLicenseLandIssueDate;
            model.PreLicenseLandExpireDate = this.PreLicenseLandExpireDate;
            model.LicenseLandNo = this.LicenseLandNo;
            model.LicenseLandIssueDate = this.LicenseLandIssueDate;
            model.LicenseLandExpireDate = this.LicenseLandExpireDate;
            model.IsNotLicenseLand = this.IsNotLicenseLand;
            model.LicenseProductNo = this.LicenseProductNo;
            model.LicenseProductIssueDate = this.LicenseProductIssueDate;
            model.LicenseProductExpireDate = this.LicenseProductExpireDate;
            model.LicenseProductRemark = this.LicenseProductRemark;
            model.IsPrintAgreementEmpty = this.IsPrintAgreementEmpty;
            model.IsPrintAgreementForBuyer = this.IsPrintAgreementForBuyer;
            model.IsPrintAgreementForRevenue = this.IsPrintAgreementForRevenue;
            model.IsPrintAgreementForSeller = this.IsPrintAgreementForSeller;

            if (ProductTypeKey == ProductTypeKeys.HighRise)
            {
                model.ExpectedEnvironmentalApprovalDate = this.ExpectedEnvironmentalApprovalDate;
                model.CondoConstructionPermitSubmitDate = this.CondoConstructionPermitSubmitDate;
                model.ExpectedPermitReceiveDate = this.ExpectedPermitReceiveDate;
                model.CondoConstructionPermitNo = this.CondoConstructionPermitNo;
                model.CondoConstructionPermitDate = this.CondoConstructionPermitDate;
                model.CondoConstructionPermitExpireDate = this.CondoConstructionPermitExpireDate;
                model.CondoConstructionPermitRemark = this.CondoConstructionPermitRemark;
            }
        }
    }
}
