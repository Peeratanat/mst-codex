using Database.Models;
using Database.Models.DbQueries.SAL;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class ProjectInfoDTO : BaseDTO
    {
        /// <summary>
        /// Master/api/MasterCenters?masterCenterGroupKey=ProjectStatus
        /// สถานะโครงการ
        /// </summary>
        public MST.MasterCenterDropdownDTO ProjectStatus { get; set; }
        /// <summary>
        /// รหัสโครงการ
        /// </summary>
        /// <example>60015</example>
        [Description("รหัสโครงการ")]
        public string ProjectNo { get; set; }
        /// <summary>
        /// รหัสโครงการ SAP
        /// </summary>
        /// <value>The sap code.</value>
        public string SapCode { get; set; }
        /// <summary>
        /// เบอร์ติดต่อ
        /// </summary>
        /// <value>The phone number.</value>
        [Description("เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// ชื่อโครงการ (TH)
        /// </summary>
        /// <value>The project name th.</value>
        [Description("ชื่อโครงการ (TH)")]
        public string ProjectNameTH { get; set; }
        /// <summary>
        /// ชื่อโครงการ (EN)
        /// </summary>
        /// <value>The project name en.</value>
        [Description("ชื่อโครงการ (EN)")]
        public string ProjectNameEN { get; set; }
        /// <summary>
        /// ชื่อย่อโครงการ
        /// </summary>
        /// <value>The name of the project short.</value>
        public string ProjectShortName { get; set; }
        /// <summary>
        /// ประเภทของโครงการ  (แนวราบ, แนวสูง)
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ProductType
        /// </summary>
        /// <value></value>
        [Description("ประเภทของโครงการ  (แนวราบ, แนวสูง)")]
        public MST.MasterCenterDropdownDTO ProductType { get; set; }
        /// <summary>
        /// ประเภทโครงการ
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ProjectType
        /// </summary>
        /// <value></value>
        [Description("ประเภทโครงการ")]
        public MST.MasterCenterDropdownDTO ProjectType { get; set; }
        /// <summary>
        /// มูลค่าโครงการ
        /// </summary>
        /// <value>10000000.00</value>
        public decimal? ProjectPrice { get; set; }
        /// <summary>
        /// BG
        /// masterdata/api/BGs/DropdownList
        /// </summary>
        [Description("BG")]
        public MST.BGDropdownDTO BG { get; set; }
        /// <summary>
        /// Sub BG
        /// masterdata/api/SubBGs/DropdownList
        /// </summary>
        [Description("SubBG")]
        public MST.SubBGDropdownDTO SubBG { get; set; }
        /// <summary>
        /// แบรนด์
        /// masterdata/api/Brands/DropdownList
        /// </summary>
        /// <value></value>
        [Description("แบรนด์")]
        public MST.BrandDropdownDTO Brand { get; set; }
        /// <summary>
        /// บริษัท
        /// masterdata/api/Companies/DropdownList
        /// </summary>
        /// <value></value>
        [Description("บริษัท")]
        public MST.CompanyDropdownDTO Company { get; set; }
        /// <summary>
        /// รหัส Cost Center
        /// </summary>
        /// <value>The cost center code.</value>
        [Description("รหัส Cost Center")]
        public string CostCenterCode { get; set; }
        /// <summary>
        /// รหัส Profit Center
        /// </summary>
        /// <value>The profit center code.</value>
        public string ProfitCenterCode { get; set; }
        /// <summary>
        /// วันที่เปิดโครงการ
        /// </summary>
        /// <value>The project start date.</value>
        public DateTime? ProjectStartDate { get; set; }
        /// <summary>
        /// วันที่ปิดโครงการ
        /// </summary>
        /// <value>The project end date.</value>
        public DateTime? ProjectEndDate { get; set; }

        /// Modified By Suchat S. 2020-10-20 for job Helpdesk : REQ2020010080
        /// <summary>
        /// วันที่โอนห้องแรก
        /// </summary>
        /// <value>The project start date.</value>
        public DateTime? FirstUnitTransferDate { get; set; }
        /// <summary>
        /// วันที่โอนห้องสุดท้าย
        /// </summary>
        /// <value>The project end date.</value>
        public DateTime? LasttUnitTransferDate { get; set; }
        /// <summary>
        /// Flag Up-Country
        /// </summary>
        /// <value>The project Flag Up-Country.</value>
        public bool? IsUpCountry { get; set; }

        /// <summary>
        /// วันที่สิ้นสุดการโอนลอย
        /// </summary>
        /// <value>The floating end date.</value>
        public DateTime? FloatingEndDate { get; set; }
        /// <summary>
        /// ธนาคารที่จดจำนอง
        /// masterdata/api/Banks/DropdownList
        /// </summary>
        /// <value>The mortgage bank.</value>
        public MST.BankDropdownDTO MortgageBank { get; set; }
        /// <summary>
        /// รวมเงินจดจำนอง
        /// </summary>
        /// <value>The mortgage amount.</value>
        public decimal? MortgageAmount { get; set; }
        /// <summary>
        /// จำนวนแปลงทั้งหมด
        /// </summary>
        /// <value>The total unit.</value>
        public double? TotalUnit { get; set; }
        /// <summary>
        /// ไร่
        /// </summary>
        /// <value>The rai.</value>
        public string Rai { get; set; }
        /// <summary>
        /// งาน
        /// </summary>
        /// <value>The ngan.</value>
        public string Ngan { get; set; }
        /// <summary>
        /// ตารางวา
        /// </summary>
        /// <value>The sqaure wa.</value>
        public string SqaureWa { get; set; }
        /// <summary>
        /// WeChat
        /// </summary>
        [Description("WeChat")]
        public string WeChat { get; set; }
        /// <summary>
        /// WhatsApp
        /// </summary>
        [Description("WhatsApp")]
        public string WhatsApp { get; set; }
        /// <summary>
        /// LineID
        /// </summary>
        [Description("LineId")]
        public string LineId { get; set; }
        /// <summary>
        /// ข้อมูลอื่นๆ
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }
        /// <summary>
        /// จำนวน Shop ทั้งหมด
        /// </summary>
        public int ShopCount { get; set; }
        /// <summary>
        /// สถานะ Active
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// % ฟรีดาวน์
        /// </summary>
        public double? PercentFreeDown { get; set; }
        /// <summary>
        /// จำนวนเงินฟรีดาวน์สูงสุด
        /// </summary>
        public decimal? MaxFreeDownAmount { get; set; }

        public bool? IsForeignProject { get; set; }
        public DateTime? RedeemLoanDate { get; set; }

        /// <summary>
        /// สถานะ IsNewProject
        /// </summary>
        public bool IsNewProject { get; set; }

        /// <summary>
        /// รูปMasterPlan
        /// </summary>
        public string URLMasterPlan { get; set; }

        public double? CurrentUseEngSaleArea { get; set; }
        public double? PercentSaleArea { get; set; }



        public double? RemainEngSaleArea { get; set; }
        public double? TotalEngArea { get; set; }


        public double? CurrentUseTHSaleArea { get; set; }
        public double? PercentSaleAreaTH { get; set; }
        public double? RemainTHSaleArea { get; set; }
        public double? TotalTHArea { get; set; }

        public DateTime? ForeignProjectStart { get; set; }
        public DateTime? ForeignProjectEnd { get; set; }

        public bool? NumberRotateDay { get; set; }

        public string ReasonCancelFQTQ { get; set; }
        public DateTime? ReasonCancelFQTQDateTime { get; set; }
        public Guid? ReasonCancelFQTQBy { get; set; }

        public async static Task<ProjectInfoDTO> CreateFromModelAsync(Project model, DatabaseContext db)
        {

            if (model != null)
            {
                var result = new ProjectInfoDTO()
                {
                    Id = model.ID,
                    ProjectNo = model.ProjectNo,
                    SapCode = model.SapCode,
                    PhoneNumber = model.PhoneNumber,
                    ProjectNameTH = model.ProjectNameTH,
                    ProjectNameEN = model.ProjectNameEN,
                    ProjectShortName = model.ProjectShortName,
                    ProductType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    ProjectType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectType),
                    ProjectPrice = model.ProjectPrice,
                    BG = MST.BGDropdownDTO.CreateFromModel(model.BG),
                    SubBG = MST.SubBGDropdownDTO.CreateFromModel(model.SubBG),
                    Company = MST.CompanyDropdownDTO.CreateFromModel(model.Company),
                    CostCenterCode = model.CostCenterCode,
                    ProfitCenterCode = model.ProfitCenterCode,
                    ProjectStartDate = model.ProjectStartDate,
                    ProjectEndDate = model.ProjectEndDate,
                    MortgageBank = MST.BankDropdownDTO.CreateFromModel(model.MortgageBank),
                    MortgageAmount = model.MortgageAmount,
                    TotalUnit = await db.Units.Where(o => o.ProjectID == model.ID).CountAsync(),
                    Rai = model.Rai,
                    Ngan = model.Ngan,
                    SqaureWa = model.SqaureWa,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineId = model.LineID,
                    Remark = model.Remark,
                    FloatingEndDate = model.FloatingEndDate,
                    Brand = MST.BrandDropdownDTO.CreateFromModel(model.Brand),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    ProjectStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectStatus),
                    IsActive = model.IsActive,
                    PercentFreeDown = model.PercentFreeDown,
                    MaxFreeDownAmount = model.MaxFreeDownAmount,
                    IsForeignProject = model.IsForeignProject,
                    RedeemLoanDate = model.RedeemLoanDate

                };
                result.ShopCount = await db.Units.Where(o => o.ProjectID == model.ID && o.AssetType.Key == AssetTypeKeys.Shop).CountAsync();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async static Task<ProjectInfoDTO> CreateInfoFromModelAsync(Project model, DatabaseContext db, dbqCDGetQuotaFQTHByProject fqPercent)
        {
            var numberOfrotateDay = await db.LeadRotateProjectConfigs.FirstOrDefaultAsync(o => o.ProjectID == model.ID);
            if (model != null)
            {
                var result = new ProjectInfoDTO()
                {
                    Id = model.ID,
                    ProjectNo = model.ProjectNo,
                    SapCode = model.SapCode,
                    PhoneNumber = model.PhoneNumber,
                    ProjectNameTH = model.ProjectNameTH,
                    ProjectNameEN = model.ProjectNameEN,
                    ProjectShortName = model.ProjectShortName,
                    ProductType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProductType),
                    ProjectType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectType),
                    ProjectPrice = model.ProjectPrice,
                    BG = MST.BGDropdownDTO.CreateFromModel(model.BG),
                    SubBG = MST.SubBGDropdownDTO.CreateFromModel(model.SubBG),
                    Company = MST.CompanyDropdownDTO.CreateFromModel(model.Company),
                    CostCenterCode = model.CostCenterCode,
                    ProfitCenterCode = model.ProfitCenterCode,
                    ProjectStartDate = model.ProjectStartDate,
                    ProjectEndDate = model.ProjectEndDate,
                    MortgageBank = MST.BankDropdownDTO.CreateFromModel(model.MortgageBank),
                    MortgageAmount = model.MortgageAmount,
                    TotalUnit = await db.Units.CountAsync(o => o.ProjectID == model.ID),
                    Rai = model.Rai,
                    Ngan = model.Ngan,
                    SqaureWa = model.SqaureWa,
                    WeChat = model.WeChatID,
                    WhatsApp = model.WhatsAppID,
                    LineId = model.LineID,
                    Remark = model.Remark,
                    FloatingEndDate = model.FloatingEndDate,
                    Brand = MST.BrandDropdownDTO.CreateFromModel(model.Brand),
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    ProjectStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ProjectStatus),
                    IsActive = model.IsActive,
                    PercentFreeDown = model.PercentFreeDown,
                    MaxFreeDownAmount = model.MaxFreeDownAmount,

                    //CurrentUseEngSaleArea = dbq != null ? dbq.CurrentUseEngSaleArea : 0,
                    //PercentSaleArea = dbq != null ? dbq.PercentSaleArea : 0,

                    //ENG
                    TotalEngArea = fqPercent != null ? fqPercent?.FQ_Aval_Area + fqPercent?.FQ_Sold_Area : 0,
                    CurrentUseEngSaleArea = fqPercent != null ? fqPercent?.FQ_Sold_Area : 0,
                    PercentSaleArea = fqPercent != null ? fqPercent?.FQ_Total_Per : 0,
                    RemainEngSaleArea = fqPercent != null ? fqPercent?.FQ_Aval_Area : 0,

                    //TH
                    TotalTHArea = fqPercent != null ? fqPercent?.TH_Aval_Area + fqPercent?.TH_Sold_Area : 0,
                    CurrentUseTHSaleArea = fqPercent != null ? fqPercent?.TH_Sold_Area : 0,
                    PercentSaleAreaTH = fqPercent != null ? fqPercent?.TH_Total_Per : 0,
                    RemainTHSaleArea = fqPercent != null ? fqPercent?.TH_Aval_Area : 0,

                    IsForeignProject = model.IsForeignProject,
                    // Modified By Suchat S. 2020-10-20 for job Helpdesk : REQ2020010080
                    FirstUnitTransferDate = model.FirstUnitTransferDate,
                    LasttUnitTransferDate = model.LasttUnitTransferDate,
                    // Modified By Suchat S. 2020-10-27 for job Helpdesk : REQ2020010233
                    IsUpCountry = model.IsUpCountry,
                    RedeemLoanDate = model.RedeemLoanDate,
                    IsNewProject = model.IsNewProject,
                    ForeignProjectEnd = model.ForeignProjectEnd,
                    ForeignProjectStart = model.ForeignProjectStart,
                    //NumberRotateDay = numberOfrotateDay.NumberRotateDay == 18 ? true : false,
                };

                if (numberOfrotateDay != null)
                {
                    if (numberOfrotateDay.NumberRotateDay == 18)
                    {
                        result.NumberRotateDay = true;
                    }
                    else if (numberOfrotateDay.NumberRotateDay == 30)
                    {
                        result.NumberRotateDay = false;
                    }
                    else
                    {
                        result.NumberRotateDay = null;
                    }
                }
                else
                {
                    result.NumberRotateDay = null;
                }

                result.ShopCount = await db.Units.CountAsync(o => o.ProjectID == model.ID && o.AssetType.Key == AssetTypeKeys.Shop);
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
            if (string.IsNullOrEmpty(this.ProjectNo))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.ProjectNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            else
            {
                if (!this.ProjectNo.CheckLang(false, true, false, true, 10, "-/"))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0036").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.ProjectNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            var checkUniqueProjectNo = this.Id != (Guid?)null
                ? await db.Projects.Where(o => o.ProjectNo == this.ProjectNo && o.ID != this.Id).CountAsync() > 0
                : await db.Projects.Where(o => o.ProjectNo == this.ProjectNo).CountAsync() > 0;
            if (checkUniqueProjectNo)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0042").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.ProjectNo)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                msg = msg.Replace("[value]", this.ProjectNo);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            // Modified by Suchat S. 2020-06-27 Cannot check phoneNumber in Master Project (isNumber) for job CRM-8117

            //if (!string.IsNullOrEmpty(PhoneNumber))
            //{
            //    if (!PhoneNumber.IsOnlyNumber())
            //    {
            //        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0025").FirstAsync();
            //        string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.PhoneNumber)).GetCustomAttribute<DescriptionAttribute>().Description;
            //        var msg = errMsg.Message.Replace("[field]", desc);
            //        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            //    }
            //}

            if (string.IsNullOrEmpty(this.ProjectNameTH))
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0006").FirstAsync();
                string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.ProjectNameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                var msg = errMsg.Message.Replace("[field]", desc);
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (!string.IsNullOrEmpty(WeChat))
            {
                if (!WeChat.CheckLang(false, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0023").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.WeChat)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(WhatsApp))
            {
                if (!WhatsApp.CheckLang(false, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0023").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.WhatsApp)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(LineId))
            {
                if (!LineId.CheckLang(false, true, true, false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0023").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ProjectInfoDTO.LineId)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        public void ToModel(ref Project model)
        {
            model.ProjectNo = this.ProjectNo;
            model.SapCode = this.SapCode;
            model.PhoneNumber = this.PhoneNumber;
            model.ProjectNameTH = this.ProjectNameTH;
            model.ProjectNameEN = this.ProjectNameEN;
            model.ProjectShortName = this.ProjectShortName;
            model.ProductTypeMasterCenterID = this.ProductType?.Id;
            model.ProjectTypeMasterCenterID = this.ProjectType?.Id;
            model.ProjectPrice = this.ProjectPrice;
            model.BGID = this.BG?.Id;
            model.SubBGID = this.SubBG?.Id;
            model.Group = this.SubBG?.SubBGNo;
            model.CompanyID = this.Company?.Id;
            model.CostCenterCode = this.CostCenterCode;
            model.ProfitCenterCode = this.ProfitCenterCode;
            model.ProjectStartDate = this.ProjectStartDate;
            model.ProjectEndDate = this.ProjectEndDate;
            model.MortgageBankID = this.MortgageBank?.Id;
            model.MortgageAmount = this.MortgageAmount;
            model.TotalUnit = this.TotalUnit;
            model.Rai = this.Rai;
            model.Ngan = this.Ngan;
            model.SqaureWa = this.SqaureWa;
            model.WeChatID = this.WeChat;
            model.WhatsAppID = this.WhatsApp;
            model.LineID = this.LineId;
            model.Remark = this.Remark;
            model.FloatingEndDate = this.FloatingEndDate;
            model.BrandID = this.Brand?.Id;
            model.ProjectStatusMasterCenterID = this.ProjectStatus?.Id;
            model.IsActive = this.IsActive;
            model.PercentFreeDown = this.PercentFreeDown;
            model.MaxFreeDownAmount = this.MaxFreeDownAmount;
            model.IsForeignProject = this.IsForeignProject;
            model.RedeemLoanDate = this.RedeemLoanDate;
            model.IsUpCountry = this.IsUpCountry;
        }
    }
}
