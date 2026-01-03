using Database.AuditLogs;
using Database.Models.MasterKeys;
using ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Log = Database.AuditLogs;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Database.Models.ROI;
using Database.Models.CTM;

namespace Database.Models
{
    public class DatabaseContext : DbContext
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly AuditLogContext AuditLogContext;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.HttpContextAccessor = httpContextAccessor;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IHttpContextAccessor httpContextAccessor, AuditLogContext auditLogContext) : base(options)
        {
            this.HttpContextAccessor = httpContextAccessor;
            this.AuditLogContext = auditLogContext;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options, AuditLogContext auditLogContext) : base(options)
        {
            this.AuditLogContext = auditLogContext;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        #region USR
        public DbSet<USR.AuthorizeRule> AuthorizeRules { get; set; }
        public DbSet<USR.AuthorizeRuleByRole> AuthorizeRuleByRoles { get; set; }
        public DbSet<USR.FavoriteMenu> FavoriteMenus { get; set; }
        public DbSet<USR.Role> Roles { get; set; }
        public DbSet<USR.RoleGroup> RoleGroups { get; set; }
        public DbSet<USR.MyTask> MyTasks { get; set; }
        public DbSet<USR.TaskType> TaskTypes { get; set; }
        public DbSet<USR.User> Users { get; set; }
        public DbSet<USR.UserDefaultProject> UserDefaultProjects { get; set; }
        public DbSet<USR.UserRole> UserRoles { get; set; }
        public DbSet<USR.UserBackgroundJob> UserBackgroundJobs { get; set; }
        public DbSet<USR.UserAuthorizeProject> UserAuthorizeProjects { get; set; }
        public DbSet<USR.RefreshToken> RefreshTokens { get; set; }
        public DbSet<USR.Module> Modules { get; set; }
        public DbSet<USR.Menu> Menus { get; set; }
        public DbSet<USR.MenuAction> MenuActions { get; set; }
        public DbSet<USR.UserAuthorizeProject_LCM> UserAuthorizeProject_LCMs { get; set; }
        public DbSet<USR.MenuPermission> MenuPermissions { get; set; }
        public DbSet<USR.DashboardMenu> DashboardMenus { get; set; }
        public DbSet<USR.DashboardPermission> DashboardPermissions { get; set; }

        #endregion

        #region ADM
        public DbSet<ADM.AdminLogHistory> AdminLogHistorys { get; set; }
        #endregion

        #region BI
        public DbSet<BI.Mst_LeadIndicator_Target> LeadIndicatorTargets { get; set; }
        #endregion

        #region MST
        public DbSet<MST.Bank> Banks { get; set; }
        public DbSet<MST.BankBranch> BankBranches { get; set; }
        public DbSet<MST.BankAccount> BankAccounts { get; set; }
        public DbSet<MST.Brand> Brands { get; set; }
        public DbSet<MST.Company> Companies { get; set; }
        public DbSet<MST.Country> Countries { get; set; }
        public DbSet<MST.District> Districts { get; set; }
        public DbSet<MST.LandOffice> LandOffices { get; set; }
        public DbSet<MST.LandOfficeDocCheckList> LandOfficeDocCheckLists { get; set; }
        public DbSet<MST.MasterCenter> MasterCenters { get; set; }
        public DbSet<MST.MasterCenterGroup> MasterCenterGroups { get; set; }
        public DbSet<MST.Province> Provinces { get; set; }
        public DbSet<MST.SubDistrict> SubDistricts { get; set; }
        public DbSet<MST.MasterPriceItem> MasterPriceItems { get; set; }
        public DbSet<MST.BOConfiguration> BOConfigurations { get; set; }
        public DbSet<MST.BG> BGs { get; set; }
        public DbSet<MST.SubBG> SubBGs { get; set; }
        public DbSet<MST.LegalEntity> LegalEntities { get; set; }
        public DbSet<MST.TypeOfRealEstate> TypeOfRealEstates { get; set; }
        public DbSet<MST.Agent> Agents { get; set; }
        public DbSet<MST.AgentEmployee> AgentEmployees { get; set; }
        public DbSet<MST.EDC> EDCs { get; set; }
        public DbSet<MST.EDCFee> EDCFees { get; set; }
        public DbSet<MST.ErrorMessage> ErrorMessages { get; set; }
        public DbSet<MST.RunningNumberCounter> RunningNumberCounters { get; set; }
        public DbSet<MST.CancelReason> CancelReasons { get; set; }
        public DbSet<MST.CancelReturnSetting> CancelReturnSettings { get; set; }
        public DbSet<MST.AttorneyTransfer> AttorneyTransfers { get; set; }
        public DbSet<MST.GLAccountType> GLAccountTypes { get; set; }
        public DbSet<MST.PostTracking> PostTrackings { get; set; }
        public DbSet<MST.KCashCardTransfer> KCashCardTransfer { get; set; } /* Modified by Suchat S. 2020-08-31 for support change K-Cash Card Transfer */
        public DbSet<MST.BankBranchBOT> BankBranchBOTs { get; set; }
        public DbSet<MST.Zone> Zones { get; set; }
        public DbSet<MST.AgreementProjectOwner> AgreementProjectOwners { get; set; }
        public DbSet<MST.Event> Event { get; set; }
        public DbSet<MST.ProjectInEvent> ProjectInEvents { get; set; }
        public DbSet<MST.PaymentGatewayConfig> PaymentGatewayConfigs { get; set; }
        public DbSet<MST.CombineUnit> CombineUnits { get; set; }
        public DbSet<MST.CombineUnitHist> CombineUnitHists { get; set; }
        public DbSet<MST.Servitude> Servitude { get; set; }
        public DbSet<MST.QuestionFAQ> QuestionFAQ { get; set; }
        public DbSet<MST.AnswerFAQ> AnswerFAQ { get; set; }


        #endregion

        #region PRJ
        public DbSet<PRJ.Address> Addresses { get; set; }
        public DbSet<PRJ.BudgetPromotion> BudgetPromotions { get; set; }
        public DbSet<PRJ.Floor> Floors { get; set; }
        public DbSet<PRJ.FloorPlanImage> FloorPlanImages { get; set; }
        public DbSet<PRJ.HighRiseFee> HighRiseFees { get; set; }
        public DbSet<PRJ.LowRiseBuildingPriceFee> LowRiseBuildingPriceFees { get; set; }
        public DbSet<PRJ.LowRiseFee> LowRiseFees { get; set; }
        public DbSet<PRJ.LowRiseFenceFee> LowRiseFenceFees { get; set; }
        public DbSet<PRJ.Model> Models { get; set; }
        public DbSet<PRJ.AgreementConfig> AgreementConfigs { get; set; }
        public DbSet<PRJ.PriceList> PriceLists { get; set; }
        public DbSet<PRJ.PriceListItem> PriceListItems { get; set; }
        public DbSet<PRJ.Project> Projects { get; set; }
        public DbSet<PRJ.ProjectInfoLocation> ProjectInfoLocations { get; set; }
        public DbSet<PRJ.ProjectInfoDetail> ProjectInfoDetails { get; set; }
        public DbSet<PRJ.ProjectInfo> ProjectInfos { get; set; }
        public DbSet<PRJ.RoomPlanImage> RoomPlanImages { get; set; }
        public DbSet<PRJ.RoundFee> RoundFees { get; set; }
        public DbSet<PRJ.TitledeedDetail> TitledeedDetails { get; set; }
        public DbSet<PRJ.Tower> Towers { get; set; }
        public DbSet<PRJ.Unit> Units { get; set; }
        public DbSet<PRJ.UnitControlInterest> UnitControlInterests { get; set; }
        public DbSet<PRJ.UnitControlLock> UnitControlLocks { get; set; }
        public DbSet<PRJ.WaiveQC> WaiveQCs { get; set; }
        public DbSet<PRJ.PriceListItemTemplate> PriceListItemTemplates { get; set; }
        public DbSet<PRJ.MinPrice> MinPrices { get; set; }
        public DbSet<PRJ.WaterElectricMeterPrice> WaterElectricMeterPrices { get; set; }
        public DbSet<PRJ.BudgetMinPrice> BudgetMinPrices { get; set; }
        public DbSet<PRJ.BudgetMinPriceUnit> BudgetMinPriceUnits { get; set; }
        public DbSet<PRJ.OtherUnitInfoTag> OtherUnitInfoTags { get; set; }
        public DbSet<PRJ.UnitOtherUnitInfoTag> UnitOtherUnitInfoTags { get; set; }
        public DbSet<PRJ.TitledeedDetailHistory> TitledeedDetailHistories { get; set; }
        public DbSet<PRJ.BudgetPromotionSyncJob> BudgetPromotionSyncJobs { get; set; }
        public DbSet<PRJ.BudgetPromotionSyncItem> BudgetPromotionSyncItems { get; set; }
        public DbSet<PRJ.BudgetPromotionSyncItemResult> BudgetPromotionSyncItemResults { get; set; }
        public DbSet<PRJ.SAPWBSProSyncJob> SAPWBSProSyncJobs { get; set; }
        public DbSet<PRJ.TitleDeedPublicUtility> TitleDeedPublicUtilitys { get; set; }
        public DbSet<PRJ.LCTarget> LCTargets { get; set; }
        public DbSet<PRJ.TitledeedConfig> TitledeedConfigs { get; set; }
        public DbSet<PRJ.LetterGuarantee> LetterGuarantees { get; set; }
        public DbSet<PRJ.LetterGuaranteeFile> LetterGuaranteeFiles { get; set; }


        public DbSet<PRJ.SpecMaterialCollection> SpecMaterialCollections { get; set; }
        public DbSet<PRJ.SpecMaterialItem> SpecMaterialItems { get; set; }
        public DbSet<PRJ.SpecMaterialCollectionDetail> SpecMaterialCollectionDetails { get; set; }
        public DbSet<PRJ.SpecMaterialTemplate> SpecMaterialTemplates { get; set; }



        #endregion

        #region CTM
        public DbSet<CTM.Contact> Contacts { get; set; }
        public DbSet<CTM.ContactAddress> ContactAddresses { get; set; }
        public DbSet<CTM.ContactAddressProject> ContactAddressProjects { get; set; }
        public DbSet<CTM.ContactEmail> ContactEmails { get; set; }
        public DbSet<CTM.ContactPhone> ContactPhones { get; set; }
        public DbSet<CTM.Lead> Leads { get; set; }
        public DbSet<CTM.LeadActivity> LeadActivities { get; set; }
        public DbSet<CTM.LeadActivityStatus> LeadActivityStatuses { get; set; }
        public DbSet<CTM.Opportunity> Opportunities { get; set; }
        public DbSet<CTM.OpportunityActivity> OpportunityActivities { get; set; }
        public DbSet<CTM.OpportunityActivityStatus> OpportunityActivityStatuses { get; set; }
        public DbSet<CTM.OpportunityActivityResult> OpportunityActivityResults { get; set; }
        public DbSet<CTM.Visitor> Visitors { get; set; }
        public DbSet<CTM.LeadAssign> LeadAssigns { get; set; }
        public DbSet<CTM.LeadScoring> LeadScorings { get; set; }
        public DbSet<CTM.LeadScoringType> LeadScoringTypes { get; set; }
        public DbSet<CTM.OpportunityAssign> OpportunityAssigns { get; set; }
        public DbSet<CTM.RevisitActivity> RevisitActivities { get; set; }
        public DbSet<CTM.RevisitActivityStatus> RevisitActivityStatuses { get; set; }
        public DbSet<CTM.RevisitActivityResult> RevisitActivityResults { get; set; }
        public DbSet<CTM.ProjectVisitCounterSetting> ProjectVisitCounterSettings { get; set; }
        public DbSet<CTM.LeadSyncJob> LeadSyncJobs { get; set; }
        public DbSet<CTM.ActivityTask> ActivityTasks { get; set; }
        public DbSet<CTM.ActivityTaskUpdateOverdueJob> ActivityTaskUpdateOverdueJobs { get; set; }
        public DbSet<CTM.CustomerConsentHistory> CustomerConsentHistorys { get; set; }
        public DbSet<CTM.LeadUserIgnore> LeadUsersIgnore { get; set; }
        public DbSet<CTM.MappingSubLeadType> MappingSubLeadType { get; set; }
        public DbSet<CTM.OpportunityRelatedHistory> OpportunityRelatedHistory { get; set; }
        public DbSet<CTM.ContactRegister> ContactRegisters { get; set; }
        public DbSet<CTM.Contact_Audit> Contact_Audits { get; set; }
        public DbSet<CTM.Contact_Change_Log> Contact_Change_Logs { get; set; }
        public DbSet<CTM.LeadRotateProjectConfig> LeadRotateProjectConfigs { get; set; }
        public DbSet<CTM.LeadRotateAssign> LeadRotateAssigns { get; set; }
        #endregion

        #region ACC

        public DbSet<ACC.CalendarLock> CalendarLocks { get; set; }
        public DbSet<ACC.GLDetail> GLDetails { get; set; }
        public DbSet<ACC.GLExport> GLExports { get; set; }
        public DbSet<ACC.PostGLHouseType> PostGLHouseTypes { get; set; }
        public DbSet<ACC.PostGLDetail> PostGLDetails { get; set; }
        public DbSet<ACC.PostGLFormatTextFileDetail> PostGLFormatTextFileDetails { get; set; }
        public DbSet<ACC.PostGLFormatTextFileHeader> PostGLFormatTextFileHeaders { get; set; }
        public DbSet<ACC.PostGLHeader> PostGLHeaders { get; set; }
        public DbSet<ACC.SAPUnitStatus> SAPUnitStatuss { get; set; }
        public DbSet<ACC.PostGLTemp> PostGLTemps { get; set; }
        public DbSet<ACC.PostGLHeader_RVMultiplePayment> PostGLHeader_RVMultiplePayments { get; set; }

        #endregion

        #region CMS
        public DbSet<CMS.CalculateHighRiseSale> CalculateHighRiseSales { get; set; }
        public DbSet<CMS.CalculateHighRiseTransfer> CalculateHighRiseTransfers { get; set; }
        public DbSet<CMS.CalculateLowRiseSale> CalculateLowRiseSales { get; set; }
        public DbSet<CMS.CalculateLowRiseTransfer> CalculateLowRiseTransfers { get; set; }
        public DbSet<CMS.CalculatePerMonthHighRiseSale> CalculatePerMonthHighRiseSales { get; set; }
        public DbSet<CMS.CalculatePerMonthHighRiseTransfer> CalculatePerMonthHighRiseTransfers { get; set; }
        public DbSet<CMS.CalculatePerMonthLowRise> CalculatePerMonthLowRises { get; set; }
        public DbSet<CMS.CalculateIncreaseDeductMoney> CalculateIncreaseDeductMoneys { get; set; }
        public DbSet<CMS.CommissionContract> CommissionContracts { get; set; }
        public DbSet<CMS.CommissionTransfer> CommissionTransfers { get; set; }
        public DbSet<CMS.GeneralSetting> GeneralSettings { get; set; }
        public DbSet<CMS.RateSettingAgent> RateSettingAgents { get; set; }
        public DbSet<CMS.RateSettingFixSale> RateSettingFixSales { get; set; }
        public DbSet<CMS.RateSettingFixTransfer> RateSettingFixTransfers { get; set; }
        public DbSet<CMS.RateSettingSale> RateSettingSales { get; set; }
        public DbSet<CMS.RateSettingTransfer> RateSettingTransfers { get; set; }
        public DbSet<CMS.RateSettingFixSaleModel> RateSettingFixSaleModels { get; set; }
        public DbSet<CMS.RateSettingFixTransferModel> RateSettingFixTransferModels { get; set; }
        public DbSet<CMS.IncreaseMoney> IncreaseMoneys { get; set; }
        public DbSet<CMS.DeductMoney> DeductMoneys { get; set; }
        public DbSet<CMS.ChangeLCSale> ChangeLCSales { get; set; }
        public DbSet<CMS.ChangeLCTransfer> ChangeLCTransfers { get; set; }
        public DbSet<CMS.RateSale> RateSales { get; set; }
        public DbSet<CMS.RateTransfer> RateTransfers { get; set; }
        public DbSet<CMS.CommissionIgnoreRate> CommissionIgnoreRates { get; set; }
        public DbSet<CMS.LCReconEventPeriodByProject> LCReconEventPeriodByProject { get; set; }
        public DbSet<CMS.LCReconUserByProject> LCReconUserByProject { get; set; }
        #endregion

        #region FIN
        public DbSet<FIN.BillPayment> BillPayments { get; set; }
        public DbSet<FIN.BillPaymentTemp> BillPaymentTemps { get; set; }
        public DbSet<FIN.BillPaymentDetail> BillPaymentDetails { get; set; }
        public DbSet<FIN.BillPaymentDetailTemp> BillPaymentDetailTemps { get; set; }
        public DbSet<FIN.CustomerWallet> CustomerWallets { get; set; }
        public DbSet<FIN.CustomerWalletTransaction> CustomerWalletTransactions { get; set; }
        public DbSet<FIN.DepositHeader> DepositHeaders { get; set; }
        public DbSet<FIN.DepositDetail> DepositDetails { get; set; }
        public DbSet<FIN.DirectCreditDebitApprovalForm> DirectCreditDebitApprovalForms { get; set; }
        public DbSet<FIN.DirectCreditDebitExportHeader> DirectCreditDebitExportHeaders { get; set; }
        public DbSet<FIN.DirectCreditDebitExportDetail> DirectCreditDebitExportDetails { get; set; }
        public DbSet<FIN.Payment> Payments { get; set; }
        public DbSet<FIN.PaymentItem> PaymentItems { get; set; }
        public DbSet<FIN.PaymentMethod> PaymentMethods { get; set; }
        public DbSet<FIN.PaymentMethodToItem> PaymentMethodToItems { get; set; }
        public DbSet<FIN.ReceiptHeader> ReceiptHeaders { get; set; }
        public DbSet<FIN.ReceiptDetail> ReceiptDetails { get; set; }
        public DbSet<FIN.ReceiptSendEmailHistory> ReceiptSendEmailHistories { get; set; }
        public DbSet<FIN.ReceiptSendPrintingHistory> ReceiptSendPrintingHistories { get; set; }
        public DbSet<FIN.ReceiptSendPrintingHistoryDetail> ReceiptSendPrintingHistoryDetails { get; set; }
        public DbSet<FIN.ReceiptTempHeader> ReceiptTempHeaders { get; set; }
        public DbSet<FIN.ReceiptTempDetail> ReceiptTempDetails { get; set; }
        public DbSet<FIN.UnknownPayment> UnknownPayments { get; set; }
        public DbSet<FIN.FET> FETs { get; set; }
        public DbSet<FIN.MemoMoveMoney> MemoMoveMoneys { get; set; }
        public DbSet<FIN.PaymentConvertItem> PaymentConvertItems { get; set; }

        public DbSet<FIN.OfflinePaymentHeader> OfflinePaymentHeaders { get; set; }
        public DbSet<FIN.OfflinePaymentDetail> OfflinePaymentDetails { get; set; }

        public DbSet<FIN.PaymentPrebook> PaymentPrebooks { get; set; }
        public DbSet<FIN.PaymentMethodPrebook> PaymentMethodPrebooks { get; set; }
        public DbSet<FIN.CancelDepositPrebookHistory> CancelDepositPrebookHistorys { get; set; }
        public DbSet<FIN.OnlinePaymentPrebook> OnlinePaymentPrebooks { get; set; }
        public DbSet<FIN.OnlinePaymentHistory> OnlinePaymentHistorys { get; set; }


        #endregion

        #region LET
        public DbSet<LET.DownPaymentLetter> DownPaymentLetters { get; set; }
        public DbSet<LET.TransferLetter> TransferLetters { get; set; }
        public DbSet<LET.MailConfirmCancel> MailConfirmCancels { get; set; }
        #endregion

        #region NTF
        public DbSet<NTF.EmailNotification> EmailNotifications { get; set; }
        public DbSet<NTF.MobileInstallation> MobileInstallations { get; set; }
        public DbSet<NTF.MobileNotification> MobileNotifications { get; set; }
        public DbSet<NTF.NotificationTemplate> NotificationTemplates { get; set; }
        public DbSet<NTF.WebNotification> WebNotifications { get; set; }
        public DbSet<NTF.SmsNotification> SmsNotifications { get; set; }
        public DbSet<NTF.SMS> Smses { get; set; }
        public DbSet<NTF.SMSCalendar> SMSCalendars { get; set; }
        #endregion

        #region OST
        public DbSet<OST.ContactStory> ContactStories { get; set; }
        public DbSet<OST.ContactStoryGroup> ContactStoryGroups { get; set; }
        public DbSet<OST.ContactStoryType> ContactStoryTypes { get; set; }
        public DbSet<OST.UnitStory> UnitStories { get; set; }
        public DbSet<OST.UnitStoryGroup> UnitStoryGroups { get; set; }
        public DbSet<OST.UnitStoryType> UnitStoryTypes { get; set; }
        #endregion

        #region PRM
        public DbSet<PRM.SalePromotion> SalePromotions { get; set; }
        public DbSet<PRM.SalePromotionDelivery> SalePromotionDeliveries { get; set; }
        public DbSet<PRM.SalePromotionDeliveryItem> SalePromotionDeliveryItems { get; set; }
        public DbSet<PRM.SalePromotionDeliveryStockItem> SalePromotionDeliveryStockItems { get; set; }
        public DbSet<PRM.SalePromotionExpense> SalePromotionExpenses { get; set; }
        public DbSet<PRM.SalePromotionItem> SalePromotionItems { get; set; }
        public DbSet<PRM.SalePromotionFreeItem> SalePromotionFreeItems { get; set; }
        public DbSet<PRM.SalePromotionCreditCardItem> SalePromotionCreditCardItems { get; set; }
        public DbSet<PRM.SalePromotionRequest> SalePromotionRequests { get; set; }
        public DbSet<PRM.SalePromotionRequestItem> SalePromotionRequestItems { get; set; }
        public DbSet<PRM.SalePromotionStockReceiveItem> SalePromotionStockReceiveItems { get; set; }
        public DbSet<PRM.MasterSalePromotionCreditCardItem> MasterSalePromotionCreditCardItems { get; set; }
        public DbSet<PRM.MasterSaleHouseModelFreeItem> MasterSaleHouseModelFreeItems { get; set; }
        public DbSet<PRM.MasterSaleHouseModelItem> MasterSaleHouseModelItems { get; set; }
        public DbSet<PRM.MasterSalePromotion> MasterSalePromotions { get; set; }
        public DbSet<PRM.MasterSalePromotionFreeItem> MasterSalePromotionFreeItems { get; set; }
        public DbSet<PRM.MasterSalePromotionItem> MasterSalePromotionItems { get; set; }
        public DbSet<PRM.MasterPreSaleHouseModelItem> MasterPreSaleHouseModelItems { get; set; }
        public DbSet<PRM.MasterPreSalePromotion> MasterPreSalePromotions { get; set; }
        public DbSet<PRM.MasterPreSalePromotionItem> MasterPreSalePromotionItems { get; set; }
        public DbSet<PRM.PreSalePromotionRequest> PreSalePromotionRequests { get; set; }
        public DbSet<PRM.PreSalePromotionRequestUnit> PreSalePromotionRequestUnits { get; set; }
        public DbSet<PRM.PreSalePromotionRequestItem> PreSalePromotionRequestItems { get; set; }
        public DbSet<PRM.MasterTransferHouseModelFreeItem> MasterTransferHouseModelFreeItems { get; set; }
        public DbSet<PRM.MasterTransferHouseModelItem> MasterTransferHouseModelItems { get; set; }
        public DbSet<PRM.MasterTransferPromotion> MasterTransferPromotions { get; set; }
        public DbSet<PRM.MasterTransferPromotionFreeItem> MasterTransferPromotionFreeItems { get; set; }
        public DbSet<PRM.MasterTransferPromotionItem> MasterTransferPromotionItems { get; set; }
        public DbSet<PRM.MasterTransferPromotionCreditCardItem> MasterTransferPromotionCreditCardItems { get; set; }
        public DbSet<PRM.PreSalePromotion> PreSalePromotions { get; set; }
        public DbSet<PRM.PreSalePromotionItem> PreSalePromotionItems { get; set; }
        public DbSet<PRM.PromotionMaterialItem> PromotionMaterialItems { get; set; }
        public DbSet<PRM.QuotationSalePromotion> QuotationSalePromotions { get; set; }
        public DbSet<PRM.QuotationSalePromotionItem> QuotationSalePromotionItems { get; set; }
        public DbSet<PRM.QuotationSalePromotionFreeItem> QuotationSalePromotionFreeItems { get; set; }
        public DbSet<PRM.QuotationSalePromotionCreditCardItem> QuotationSalePromotionCreditCardItems { get; set; }
        public DbSet<PRM.QuotationSalePromotionExpense> QuotationSalePromotionExpenses { get; set; }
        public DbSet<PRM.QuotationTransferPromotion> QuotationTransferPromotions { get; set; }
        public DbSet<PRM.QuotationTransferPromotionItem> QuotationTransferPromotionItems { get; set; }
        public DbSet<PRM.QuotationTransferPromotionFreeItem> QuotationTransferPromotionFreeItems { get; set; }
        public DbSet<PRM.QuotationTransferPromotionCreditCardItem> QuotationTransferPromotionCreditCardItems { get; set; }
        public DbSet<PRM.TransferPromotion> TransferPromotions { get; set; }
        public DbSet<PRM.TransferPromotionDelivery> TransferPromotionDeliveries { get; set; }
        public DbSet<PRM.TransferPromotionDeliveryItem> TransferPromotionDeliveryItems { get; set; }
        public DbSet<PRM.TransferPromotionDeliveryStockItem> TransferPromotionDeliveryStockItems { get; set; }
        public DbSet<PRM.TransferPromotionExpense> TransferPromotionExpenses { get; set; }
        public DbSet<PRM.TransferPromotionItem> TransferPromotionItems { get; set; }
        public DbSet<PRM.TransferPromotionFreeItem> TransferPromotionFreeItems { get; set; }
        public DbSet<PRM.TransferPromotionCreditCardItem> TransferPromotionCreditCardItems { get; set; }
        public DbSet<PRM.TransferPromotionRequest> TransferPromotionRequests { get; set; }
        public DbSet<PRM.TransferPromotionRequestItem> TransferPromotionRequestItems { get; set; }
        public DbSet<PRM.TransferPromotionStockReceiveItem> TransferPromotionStockReceiveItems { get; set; }
        public DbSet<PRM.MappingAgreement> MappingAgreements { get; set; }
        public DbSet<PRM.PromotionMaterialGroup> PromotionMaterialGroups { get; set; }
        public DbSet<PRM.PromotionMaterial> PromotionMaterials { get; set; }
        public DbSet<PRM.PromotionVatRate> PromotionVatRates { get; set; }
        public DbSet<PRM.SAP_ZRFCMM01> SAP_ZRFCMM01s { get; set; }
        public DbSet<PRM.SAP_ZRFCMM02> SAP_ZRFCMM02s { get; set; }
        public DbSet<PRM.SAPMaterialSyncJob> SAPMaterialSyncJobs { get; set; }
        public DbSet<PRM.PromotionMaterialAddPrice> PromotionMaterialAddPrices { get; set; }
        public DbSet<PRM.PRRequestJob> PRRequestJobs { get; set; }
        public DbSet<PRM.PRRequestJobItem> PRRequestJobItems { get; set; }
        public DbSet<PRM.PRRequestJobItemResult> PRRequestJobItemResults { get; set; }
        public DbSet<PRM.PRCancelJob> PRCancelJobs { get; set; }
        public DbSet<PRM.PRCancelJobItem> PRCancelJobItems { get; set; }
        public DbSet<PRM.PRCancelJobItemResult> PRCancelJobItemResults { get; set; }
        public DbSet<PRM.ChangePromotionWorkflow> ChangePromotionWorkflows { get; set; }
        public DbSet<PRM.ChangePromotionWorkflowFile> ChangePromotionWorkflowFiles { get; set; }

        public DbSet<PRM.QuotationSalePromotionPrebook> QuotationSalePromotionPrebooks { get; set; }
        public DbSet<PRM.QuotationTransferPromotionPrebook> QuotationTransferPromotionPrebooks { get; set; }
        public DbSet<PRM.QuotationSalePromotionExpensePrebook> QuotationSalePromotionExpensePrebooks { get; set; }
        public DbSet<PRM.QuotationPromotionTemplate> QuotationPromotionTemplates { get; set; }
        public DbSet<PRM.PromotionOperatingFee> PromotionOperatingFees { get; set; }



        #endregion

        #region WFL
        public DbSet<WFL.Workflow> Workflows { get; set; }
        public DbSet<WFL.WorkflowApprover> WorkflowApprovers { get; set; }
        public DbSet<WFL.WorkflowApproverTemplate> WorkflowApproverTemplates { get; set; }
        public DbSet<WFL.WorkflowStep> WorkflowSteps { get; set; }
        public DbSet<WFL.WorkflowStepTemplate> WorkflowStepTemplates { get; set; }
        public DbSet<WFL.WorkflowTemplate> WorkflowTemplates { get; set; }
        public DbSet<WFL.WorkflowType> WorkflowTypes { get; set; }
        #endregion

        #region SAL
        public DbSet<SAL.Agreement> Agreements { get; set; }
        public DbSet<SAL.AgreementFile> AgreementFiles { get; set; }
        public DbSet<SAL.AgreementOwner> AgreementOwners { get; set; }
        public DbSet<SAL.AgreementOwnerAddress> AgreementOwnerAddresses { get; set; }
        public DbSet<SAL.AgreementOwnerPhone> AgreementOwnerPhones { get; set; }
        public DbSet<SAL.AgreementOwnerEmail> AgreementOwnerEmails { get; set; }
        public DbSet<SAL.Booking> Bookings { get; set; }
        public DbSet<SAL.BookingControl> BookingControls { get; set; }
        public DbSet<SAL.BookingOwner> BookingOwners { get; set; }
        public DbSet<SAL.BookingOwnerAddress> BookingOwnerAddresses { get; set; }
        public DbSet<SAL.BookingOwnerPhone> BookingOwnerPhones { get; set; }
        public DbSet<SAL.BookingOwnerEmail> BookingOwnerEmails { get; set; }
        public DbSet<SAL.CreditBanking> CreditBankings { get; set; }
        public DbSet<SAL.CreditBankingPrintingHistory> CreditBankingPrintingHistories { get; set; }
        public DbSet<SAL.Quotation> Quotations { get; set; }
        public DbSet<SAL.QuotationCompare> QuotationCompares { get; set; }
        public DbSet<SAL.QuotationUnitPrice> QuotationUnitPrices { get; set; }
        public DbSet<SAL.QuotationUnitPriceItem> QuotationUnitPriceItems { get; set; }
        public DbSet<SAL.TitledeedReceive> TitledeedReceives { get; set; }
        public DbSet<SAL.TitledeedReceiveHistory> TitledeedReceiveHistories { get; set; }
        public DbSet<SAL.Transfer> Transfers { get; set; }
        public DbSet<SAL.TransferCash> TransferCashes { get; set; }
        public DbSet<SAL.TransferCheque> TransferCheques { get; set; }
        public DbSet<SAL.TransferBankTransfer> TransferBankTransfers { get; set; }
        public DbSet<SAL.TransferDocument> TransferDocuments { get; set; }
        public DbSet<SAL.TransferOwner> TransferOwners { get; set; }
        public DbSet<SAL.TransferUnit> TransferUnits { get; set; }
        public DbSet<SAL.UnitPrice> UnitPrices { get; set; }
        public DbSet<SAL.UnitPriceItem> UnitPriceItems { get; set; }
        public DbSet<SAL.UnitPriceInstallment> UnitPriceInstallments { get; set; }
        public DbSet<SAL.PriceListWorkflow> PriceListWorkflows { get; set; }
        public DbSet<SAL.MinPriceBudgetWorkflow> MinPriceBudgetWorkflows { get; set; }
        public DbSet<SAL.MinPriceBudgetApproval> MinPriceBudgetApprovals { get; set; }
        public DbSet<SAL.CancelMemo> CancelMemos { get; set; }
        public DbSet<SAL.CancelMemoFile> CancelMemoFiles { get; set; }
        public DbSet<SAL.SignContractWorkflow> SignContractWorkflows { get; set; }
        public DbSet<SAL.ChangeUnitWorkflow> ChangeUnitWorkflows { get; set; }
        public DbSet<SAL.ChangeUnitFile> ChangeUnitFiles { get; set; }
        public DbSet<SAL.ChangeAgreementOwnerWorkflow> ChangeAgreementOwnerWorkflows { get; set; }
        public DbSet<SAL.ChangeAgreementOwnerFile> ChangeAgreementOwnerFiles { get; set; }
        public DbSet<SAL.ChangeAgreementOwnerWorkflowDetail> ChangeAgreementOwnerWorkflowDetails { get; set; }
        public DbSet<SAL.AgreementPrintingHistory> AgreementPrintingHistorys { get; set; }
        public DbSet<SAL.TransferFeeResult> TransferFeeResults { get; set; }
        public DbSet<SAL.TransferPrintForm> TransferPrintForms { get; set; }
        public DbSet<SAL.TransferExpense> TransferExpenses { get; set; }
        public DbSet<SAL.RefundMemo> RefundMemos { get; set; }
        public DbSet<SAL.RefundMemoCustomer> RefundMemoCustomers { get; set; }
        public DbSet<SAL.PostponeTransfer> PostponeTransfers { get; set; }
        public DbSet<SAL.TransferPayment> TransferPayments { get; set; }
        public DbSet<SAL.BackLog> BackLogs { get; set; }
        public DbSet<SAL.BackLogHistory> BackLogHistorys { get; set; }
        public DbSet<SAL.CancelMemoPrebook> CancelMemoPrebooks { get; set; }
        public DbSet<SAL.CancelUnitIgnore> CancelUnitIgnore { get; set; }
        public DbSet<SAL.TitledeedRequestFlow> TitledeedRequestFlows { get; set; }
        public DbSet<SAL.FriendGetFriends> FriendGetFriends { get; set; }

        [DbFunction("fnTS_GetBOConfigrate_TransferFeeRate", "SAL")]
        public static decimal? fnTS_GetBOConfigrate_TransferFeeRate(Guid? agreementID, decimal? netSalePrice, decimal? loanPrice, DateTime? transferDate) => throw new NotSupportedException();
        #endregion

        #region DBO
        public DbSet<DBO.JobTransaction> JobTransactions { get; set; }
        public DbSet<DBO.MergeContactResult> MergeContactResults { get; set; }
        public DbSet<DBO.LCAPP_CallLogLead> LCAPP_CallLogLeads { get; set; }
        public DbSet<DBO.LCAPP_CallLogOpp> LCAPP_CallLogOpps { get; set; }
        #endregion

        #region EQN
        public DbSet<EQN.CustomerTransQAns> CustomerTransQAnss { get; set; }
        #endregion

        #region RFN
        //public DbQuery<RFN.crm_contact_refund> crm_contact_refund { get; set; }
        //public DbSet<RFN.crm_refund_docref> crm_refund_docref { get; set; }
        #endregion

        #region Custom Query
        public DbSet<CustomQuery.IDQueryResult> IDQueryResults { get; set; }
        #endregion

        #region LOG
        public DbSet<LOG.ImptMstProjTran> ImptMstProjTrans { get; set; }
        public DbSet<LOG.AprvPrintDocContract> AprvPrintDocContracts { get; set; }
        public DbSet<LOG.RevertAgreementNo> RevertAgreementNos { get; set; }
        public DbSet<LOG.AutoCancelHistory> AutoCancelHistory { get; set; }
        public DbSet<LOG.UndoCancelHistory> UndoCancelHistory { get; set; }
        public DbSet<LOG.RefundHistory> RefundHistory { get; set; }
        public DbSet<LOG.CombineUnitMailLog> CombineUnitMailLogs { get; set; }
        public DbSet<LOG.LineItSupportAi> LineItSupportAis { get; set; }
        public DbSet<LOG.LOGGPOriginalProject> LOGGPOriginalProjects { get; set; }
        public DbSet<LOG.LOGGPOriginalUnit> LOGGPOriginalUnits { get; set; }
        public DbSet<LOG.GPSyncOriginal> GPSyncOriginals { get; set; }
        public DbSet<LOG.ChangeSaleOfficer> ChangeSaleOfficers { get; set; }
        public DbSet<LOG.AgreementConfigEditLog> AgreementConfigEditLogs { get; set; }
        #endregion

        #region TMP
        public DbSet<TMP.Booking> TMPBookings { get; set; }
        public DbSet<TMP.BookingOwner> TMPBookingOwners { get; set; }
        public DbSet<TMP.Customer> TMPCustomers { get; set; }
        public DbSet<TMP.Pricelist> TMPPricelists { get; set; }
        #endregion

        #region CMS
        public DbSet<CMS.GeneralConfigCMS> GeneralConfigCMS { get; set; }
        #endregion

        #region ROI
        public DbSet<ROI.GPCal1> GPCal1s { get; set; }
        public DbSet<ROI.GPCal2> GPCal2s { get; set; }
        public DbSet<ROI.GPCommit> GPCommits { get; set; }
        public DbSet<ROI.GPFix> GPFixs { get; set; }
        public DbSet<ROI.GPOriginalProject> GPOriginalProjects { get; set; }
        public DbSet<ROI.GPOriginalUnit> GPOriginalUnits { get; set; }
        public DbSet<ROI.GPProject> GPProjects { get; set; }
        public DbSet<ROI.GPUnit> GPUnits { get; set; }
        public DbSet<ROI.GPVersion> GPVersions { get; set; }
        public DbSet<ROI.VWUnitStatus> VWUnitStatus { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var type in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeleteEntity).IsAssignableFrom(type.ClrType))
                    modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }

            SetDefaultFilters(modelBuilder);
            SetPrimaryKeys(modelBuilder);
            SetDefaultValues(modelBuilder);
            SetDeleteBehaviors(modelBuilder);
            SetDefaultTrigger(modelBuilder);
            modelBuilder.Entity<VWUnitStatus>().HasNoKey().ToView("vw_UnitStatus");
            base.OnModelCreating(modelBuilder);
        }

        private void SetDefaultTrigger(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureEntityTriggers(
                (typeof(Contact), "trg_contact_ainst"),
                (typeof(Contact), "trg_contact_aupd"),
                (typeof(ContactEmail), "trg_contact_email_aupd"),
                (typeof(ContactPhone), "trg_contact_phone_aupd"),
                (typeof(SAL.Agreement), "trg_IsPrintApproved_aupd"),
                (typeof(SAL.ChangeAgreementOwnerWorkflow), "trg_IsPrintApproved_ChangeAG_aupd")
            );
        }

        private void SetDefaultFilters(ModelBuilder modelBuilder)
        {
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContextAccessor?.HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }

            modelBuilder.Entity<MST.Company>().HasQueryFilter(x => x.IsUseInCRM && x.IsActive && !x.IsDeleted);

            if (userID != null)
            {
                //modelBuilder.Entity<PRJ.Project>().HasQueryFilter(o => o.UserAuthorizeProjects.Where(m => m.UserID == userID).Any());
                //modelBuilder.Entity<SAL.Quotation>().HasQueryFilter(o => o.Project.UserAuthorizeProjects.Where(m => m.UserID == userID && !m.IsDeleted).Any());
            }
        }

        private void SetPrimaryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRM.SAP_ZRFCMM01>()
               .HasKey(c => new { c.WERKS, c.MATNR });

            modelBuilder.Entity<PRM.SAP_ZRFCMM02>()
                .HasKey(c => new { c.WERKS, c.MATNR, c.EBELN, c.EBELP, c.KDATB, c.KDATE, c.DATAB, c.DATBI });

            modelBuilder.Entity<MST.RunningNumberCounter>()
                .HasKey(c => new { c.Key, c.Type });
        }

        private void SetDefaultValues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PRJ.Project>()
                .Property(b => b.IsPRAutoCost)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Project>()
                .Property(b => b.IsPRAutoExpense)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Project>()
                .Property(b => b.IsPRAutoFGF)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Project>()
                .Property(b => b.IsPRAutoStand)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Project>()
                .Property(b => b.IsActive)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Unit>()
                .Property(b => b.IsPRAutoCost)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Unit>()
                .Property(b => b.IsPRAutoExpense)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Unit>()
                .Property(b => b.IsPRAutoFGF)
                .HasDefaultValue(true);

            modelBuilder.Entity<PRJ.Unit>()
                .Property(b => b.IsPRAutoStand)
                .HasDefaultValue(true);
        }

        private void SetDeleteBehaviors(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MST.SubBG>()
                .HasOne(i => i.BG)
                .WithMany(i => i.SubBGs)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PRJ.Project>()
                .HasOne(i => i.BG)
                .WithMany(i => i.Projects)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PRJ.Project>()
                .HasOne(i => i.SubBG)
                .WithMany(i => i.Projects)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<USR.User>()
                .HasMany(c => c.UserAuthorizeProjects)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<USR.User>()
                .HasMany(c => c.UserAuthorizeProject_LCMs)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<USR.User>()
                .HasMany(c => c.UserRoles)
                .WithOne(e => e.User)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public override int SaveChanges()
        {
            UpdateLeadActivityTask();
            UpdateOpportunityActivityTask();
            UpdateRevisitActivityTask();
            AddAuditInfo();
            UpdateOpportunityInfo();
            UpdateOpportunityActivityInfo();
            UpdateOpportunityRevisitInfo();
            return base.SaveChanges();
        }
        public int SaveChangesNonUpdateUser()
        {
            return base.SaveChanges();
        }

        public int SaveChanges(Guid by)
        {
            AddAuditInfo(by);
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesNotGetUserAsync()
        {
            return await base.SaveChangesAsync();
        }

        // kim
        public async Task<int> SaveChangesAsync()
        {
            AddAuditInfo();
            UpdateLeadActivityTask();
            UpdateOpportunityActivityTask();
            UpdateRevisitActivityTask();
            UpdateOpportunityInfo();
            UpdateOpportunityActivityInfo();
            UpdateOpportunityRevisitInfo();
            AddAuditLog();
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangeAsync()
        {
            AddAuditInfo();
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangesNoUpdateUserAsync()
        {
            //AddAuditInfo();
            //PreventDeletion();
            UpdateLeadActivityTask();
            UpdateOpportunityActivityTask();
            UpdateRevisitActivityTask();
            UpdateOpportunityInfo();
            UpdateOpportunityActivityInfo();
            UpdateOpportunityRevisitInfo();
            AddAuditLog();
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync(Guid by)
        {
            AddAuditInfo(by);
            return await base.SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsyncByMigrateApp()
        {
            AddAuditInfo();
            AddAuditLog();
            return await base.SaveChangesAsync();
        }

        private void PreventDeletion()
        {
            var entries = ChangeTracker.Entries().Where(x => (x.Entity is BaseEntity) && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                if (entity.IsDeleted)
                {
                    var inEntry = entry.GetInternalEntityEntry();
                    DeleteEntity(inEntry);
                }
            }
        }

        private void DeleteEntity(InternalEntityEntry inEntry)
        {
            bool canDelete = true;
            var entity = (BaseEntity)inEntry.Entity;
            entity.IsDeleted = false;
            //Compute Restrict First
            var fks = inEntry.EntityType.GetReferencingForeignKeys().OrderBy(o => o.DeleteBehavior).ToList();
            var restrictFKs = fks.Where(o => o.DeleteBehavior == DeleteBehavior.Restrict);
            var nonRestrictFKs = fks.Where(o => o.DeleteBehavior != DeleteBehavior.Restrict);
            foreach (var fk in restrictFKs)
            {
                var tableAttr = fk.DeclaringEntityType.ClrType.GetCustomAttribute<TableAttribute>();
                var tableName = $"{tableAttr.Schema}.{tableAttr.Name}";
                var propName = ((ForeignKeyAttribute)(fk.DependentToPrincipal.PropertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute)))).Name;
                var query = $"select ID from {tableName} where {propName}='{entity.ID.ToString()}' and IsDeleted=0";
                var results = Database.SqlQueryRaw<CustomQuery.IDQueryResult>(query).ToList();
                if (results.Any())
                {
                    canDelete = false;

                    var error = new ValidateException();
                    var errorMsg = ErrorMessages.First(o => o.Key == "ERR0055");
                    error.AddError(errorMsg.Key, errorMsg.Message, (int)ErrorMessageType.PopupAlert);
                    throw error;
                }
            }

            foreach (var fk in nonRestrictFKs)
            {
                var tableAttr = fk.DeclaringEntityType.ClrType.GetCustomAttribute<TableAttribute>();
                var tableName = $"{tableAttr.Schema}.{tableAttr.Name}";
                var propName = ((ForeignKeyAttribute)(fk.DependentToPrincipal.PropertyInfo.GetCustomAttribute(typeof(ForeignKeyAttribute)))).Name;
                if (fk.DeleteBehavior == DeleteBehavior.Cascade)
                {
                    foreach (var dependent in (inEntry.StateManager.GetDependentsFromNavigation(inEntry, fk)
                        ?? inEntry.StateManager.GetDependents(inEntry, fk)).ToList())
                    {
                        DeleteEntity((InternalEntityEntry)dependent);
                    }
                }
                else if (fk.DeleteBehavior == DeleteBehavior.SetNull)
                {
                    var query = $"update {tableName} set {propName}=null where {propName}='{entity.ID}'";
                    Database.ExecuteSqlRaw(query);
                }
                else
                {
                    //canDelete = false;
                    //var error = new ValidateException();
                    //error.AddError("ERR0000", $"No DeleteBehavior of {tableName}.{propName}", (int)ErrorMessageType.PopupAlert);
                    //throw error;
                }
            }

            if (canDelete)
            {
                entity.IsDeleted = true;
            }
        }

        private void AddAuditLog()
        {
            if (AuditLogContext != null)
            {
                Guid? userID = null;
                if (Guid.TryParse(HttpContextAccessor?.HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out Guid parsedUserID))
                {
                    userID = parsedUserID;
                }
                Guid? by = userID;

                // TODO : Kim  สร้าง AuditLog
                var units = ChangeTracker.Entries().Where(x => (x.Entity is PRJ.Unit) && (x.State == EntityState.Added || x.State == EntityState.Modified));
                foreach (var entry in units)
                {
                    var model = (PRJ.Unit)entry.Entity;
                    var oldModel = Units.AsNoTracking().FirstOrDefault(o => o.ID == model.ID);
                    CreateAuditLogEntity<PRJ.Unit, Log.PRJ.UnitLog>(oldModel, model, by);
                }

                AuditLogContext.SaveChanges();
            }
        }

        private void CreateAuditLogEntity<T, U>(T oldModel, T model, Guid? by) where T : BaseEntity where U : Log.BaseEntityLog, new()
        {
            U log = new U()
            {
                KeyValue = model.ID,
                OldValues = JsonConvert.SerializeObject(oldModel, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                NewValues = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                Created = DateTime.Now,
                CreateBy = by
            };
            AuditLogContext.Add(log);
        }

        // kim
        private void AddAuditInfo(Guid? by = null)
        {
            //Get user ID
            Guid? userID = null;
            Guid parsedUserID;
            if (Guid.TryParse(HttpContextAccessor?.HttpContext?.User?.Claims.SingleOrDefault(x => x.Type == "userid")?.Value, out parsedUserID))
            {
                userID = parsedUserID;
            }
            by = userID;


            var entries = ChangeTracker.Entries().Where(x => (x.Entity is BaseEntity || x.Entity is BaseEntityWithoutKey || x.Entity is BaseEntityWithoutMigrate || x.Entity is BaseEntityWithoutUpdater) && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (((BaseEntity)entry.Entity).ID == Guid.Empty)
                        {
                            ((BaseEntity)entry.Entity).ID = Guid.NewGuid();
                        }
                        if (by != null)
                        {
                            ((BaseEntity)entry.Entity).CreatedByUserID = by;
                        }
                                ((BaseEntity)entry.Entity).Created = DateTime.Now;
                    }

                    ((BaseEntity)entry.Entity).Updated = DateTime.Now;
                    if (by != null)
                    {
                        ((BaseEntity)entry.Entity).UpdatedByUserID = by;
                    }
                }
                else if (entry.Entity is BaseEntityWithoutMigrate)
                {
                    if (entry.State == EntityState.Added)
                    {
                        if (((BaseEntityWithoutMigrate)entry.Entity).ID == Guid.Empty)
                        {
                            ((BaseEntityWithoutMigrate)entry.Entity).ID = Guid.NewGuid();
                        }
                        if (by != null)
                        {
                            ((BaseEntityWithoutMigrate)entry.Entity).CreatedByUserID = by;
                        }
                        ((BaseEntityWithoutMigrate)entry.Entity).Created = DateTime.Now;
                    }

                   ((BaseEntityWithoutMigrate)entry.Entity).Updated = DateTime.Now;
                    if (by != null)
                    {
                        ((BaseEntityWithoutMigrate)entry.Entity).UpdatedByUserID = by;
                    }
                }
                else if (entry.Entity is BaseEntityWithoutKey)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((BaseEntityWithoutKey)entry.Entity).Created = DateTime.Now;
                        if (by != null)
                        {
                            ((BaseEntityWithoutKey)entry.Entity).CreatedByUserID = by;
                        }
                    }
                    ((BaseEntityWithoutKey)entry.Entity).Updated = DateTime.Now;
                    if (by != null)
                    {
                        ((BaseEntityWithoutKey)entry.Entity).UpdatedByUserID = by;
                    }
                }
                else if (entry.Entity is BaseEntityWithoutUpdater)
                {
                    if (entry.State == EntityState.Added)
                    {
                        ((BaseEntityWithoutUpdater)entry.Entity).Created = DateTime.Now;
                        if (by != null)
                        {
                            ((BaseEntityWithoutUpdater)entry.Entity).CreatedByUserID = by;
                        }
                    }
                }
            }
        }

        private void UpdateOpportunityInfo()
        {
            var changedEntities = ChangeTracker.Entries().Where(o => (o.Entity.GetType().Name == nameof(CTM.Opportunity)) && (o.State == EntityState.Added || o.State == EntityState.Modified));

            foreach (var changedEntity in changedEntities.ToList())
            {
                if (changedEntity.Entity is BaseEntity)
                {

                    var entity = (CTM.Opportunity)changedEntity.Entity;
                    var contactModel = Contacts.Where(o => o.ID == entity.ContactID).FirstOrDefault();
                    if (contactModel != null)
                    {
                        if (changedEntity.State == EntityState.Added)
                        {
                            contactModel.OpportunityCount += 1;
                            contactModel.LastOpportunityID = ((BaseEntity)changedEntity.Entity).ID;
                            Contacts.Update(contactModel);
                        }
                        else if (changedEntity.State == EntityState.Modified && entity.IsDeleted == true)
                        {
                            var oppCount = Opportunities.Where(o => o.ContactID == contactModel.ID && o.IsDeleted != true && o.ID != entity.ID).Count();
                            var lastOppModel = Opportunities.Where(o => o.ContactID == contactModel.ID && o.IsDeleted != true && o.ID != entity.ID).OrderByDescending(o => o.Created).FirstOrDefault();

                            contactModel.OpportunityCount = oppCount;
                            contactModel.LastOpportunityID = lastOppModel?.ID;
                            Contacts.Update(contactModel);
                        }
                    }
                }
            }
        }

        private void UpdateOpportunityActivityInfo()
        {
            var changedEntities = ChangeTracker.Entries().Where(o => (o.Entity.GetType().Name == nameof(CTM.OpportunityActivity)) && (o.State == EntityState.Added || o.State == EntityState.Modified));

            foreach (var changedEntity in changedEntities.ToList())
            {
                if (changedEntity.Entity is BaseEntity)
                {

                    var entity = (CTM.OpportunityActivity)changedEntity.Entity;
                    var oppModel = Opportunities.Where(o => o.ID == entity.OpportunityID).FirstOrDefault();
                    if (oppModel != null)
                    {
                        if (changedEntity.State == EntityState.Added)
                        {
                            oppModel.LastOpportunityActivityID = ((BaseEntity)changedEntity.Entity).ID;
                            Opportunities.Update(oppModel);
                        }
                        else if (changedEntity.State == EntityState.Modified && entity.IsDeleted == true)
                        {
                            var lastOppActModel = OpportunityActivities.Where(o => o.OpportunityID == oppModel.ID && o.IsDeleted != true && o.ID != entity.ID).OrderByDescending(o => o.Created).FirstOrDefault();

                            oppModel.LastOpportunityActivityID = lastOppActModel?.ID;
                            Opportunities.Update(oppModel);
                        }
                    }
                }
            }
        }

        private void UpdateOpportunityRevisitInfo()
        {
            var changedEntities = ChangeTracker.Entries().Where(o => (o.Entity.GetType().Name == nameof(CTM.RevisitActivity)) && (o.State == EntityState.Added || o.State == EntityState.Modified));

            foreach (var changedEntity in changedEntities.ToList())
            {
                if (changedEntity.Entity is BaseEntity)
                {

                    var entity = (CTM.RevisitActivity)changedEntity.Entity;
                    var oppModel = Opportunities.Where(o => o.ID == entity.OpportunityID).FirstOrDefault();
                    if (oppModel != null)
                    {
                        if (changedEntity.State == EntityState.Added)
                        {
                            oppModel.RevisitActivityCount += 1;
                            Opportunities.Update(oppModel);
                        }
                        else if (changedEntity.State == EntityState.Modified && entity.IsDeleted == true)
                        {
                            var oppCount = RevisitActivities.Where(o => o.OpportunityID == oppModel.ID && o.IsDeleted != true && o.ID != entity.ID).Count();

                            oppModel.RevisitActivityCount = oppCount;
                            Opportunities.Update(oppModel);
                        }
                    }
                }
            }
        }

        // kim
        private void UpdateLeadActivityTask()
        {
            var changedEntities = ChangeTracker.Entries().Where(o => (o.Entity.GetType().Name == nameof(CTM.LeadActivity)) && (o.State == EntityState.Added || o.State == EntityState.Modified));

            foreach (var changedEntity in changedEntities.ToList())
            {
                if (changedEntity.Entity is BaseEntity)
                {
                    var entity = (CTM.LeadActivity)changedEntity.Entity;
                    var leadModel = Leads.Include(o => o.Contact).Where(o => o.ID == entity.LeadID).First();

                    if (changedEntity.State == EntityState.Added)
                    {
                        #region Add
                        string firstName = null;
                        string lastName = null;
                        string phone = null;
                        if (leadModel.ContactID != null)
                        {
                            firstName = leadModel.Contact.FirstNameTH;
                            lastName = leadModel.Contact.LastNameTH;
                            phone = ContactPhones.Where(o => o.ContactID == leadModel.ContactID && o.IsMain == true).Select(o => o.PhoneNumber).FirstOrDefault();
                        }
                        else
                        {
                            firstName = leadModel.FirstName;
                            lastName = leadModel.LastName;
                            phone = leadModel.PhoneNumber;
                        }

                        var activityTaskStatus = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                        Guid? activityTaskStatusId = null;

                        int overdue = 0;
                        if (!entity.IsCompleted)
                        {
                            overdue = Convert.ToInt32((entity.DueDate.Value.Date - DateTime.Today.Date).TotalDays);
                        }
                        else
                        {
                            overdue = Convert.ToInt32((entity.DueDate.Value.Date - entity.ActualDate.Value.Date).TotalDays);
                        }

                        Guid? overdueStatusId = null;
                        if (overdue == 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2").Select(o => o.ID).First();
                            activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                        }
                        else if (overdue < 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3").Select(o => o.ID).First();
                            activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                        }
                        else if (overdue > 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1").Select(o => o.ID).First();
                            activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                        }

                        var activityTypeKey = MasterCenters.Where(o => o.ID == entity.LeadActivityTypeMasterCenterID).Select(o => o.Key).First();
                        int repeatCount = 0;
                        switch (activityTypeKey)
                        {
                            case "1":
                                repeatCount = 1;
                                break;
                            case "2":
                                repeatCount = 2;
                                break;
                            case "3":
                                repeatCount = 3;
                                break;
                            case "4":
                                repeatCount = 3;
                                break;
                        }

                        var leadActivityDisqualifyTypeID = MasterCenters.Where(o => o.Key == LeadActivityFollowUpTypeKeys.Disqualify && o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityFollowUpType).Select(o => o.ID).FirstOrDefault();
                        var activityStatus = LeadActivityStatuses
                            .Include(o => o.LeadActivityFollowUpType)
                            .Where(o => o.ID == entity.StatusID).FirstOrDefault();

                        if (leadActivityDisqualifyTypeID != null && activityStatus != null)
                        {
                            if (activityStatus.LeadActivityFollowUpType.ID != leadActivityDisqualifyTypeID)
                            {
                                CTM.ActivityTask activityTask = new CTM.ActivityTask()
                                {
                                    ProjectID = leadModel.ProjectID,
                                    ContactFirstName = firstName,
                                    ContactLastName = lastName,
                                    PhoneNumber = phone,
                                    DueDate = entity.DueDate,
                                    ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                                    OverdueDays = overdue,
                                    RepeatCount = repeatCount,
                                    ActivityTaskStatusMasterCenterID = activityTaskStatusId,
                                    ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "1").Select(o => o.ID).First(),
                                    ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "2").Select(o => o.ID).First(),
                                    ActivityTypeName = MasterCenters.Where(o => o.ID == entity.LeadActivityTypeMasterCenterID).Select(o => o.Name).First(),
                                    OwnerID = leadModel.OwnerID,
                                    LeadActivityID = entity.ID
                                };
                                this.ActivityTasks.Add(activityTask);
                            }
                        }
                        else
                        {
                            CTM.ActivityTask activityTask = new CTM.ActivityTask()
                            {
                                ProjectID = leadModel.ProjectID,
                                ContactFirstName = firstName,
                                ContactLastName = lastName,
                                PhoneNumber = phone,
                                DueDate = entity.DueDate,
                                ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                                OverdueDays = overdue,
                                RepeatCount = repeatCount,
                                ActivityTaskStatusMasterCenterID = activityTaskStatusId,
                                ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "1").Select(o => o.ID).First(),
                                ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "2").Select(o => o.ID).First(),
                                ActivityTypeName = MasterCenters.Where(o => o.ID == entity.LeadActivityTypeMasterCenterID).Select(o => o.Name).First(),
                                OwnerID = leadModel.OwnerID,
                                LeadActivityID = entity.ID
                            };
                            this.ActivityTasks.Add(activityTask);
                        }

                        //if (entity.AppointmentDate != null)
                        //{
                        //    CTM.ActivityTask activityTaskAppointment = new CTM.ActivityTask()
                        //    {
                        //        ProjectID = leadModel.ProjectID,
                        //        ContactFirstName = firstName,
                        //        ContactLastName = lastName,
                        //        PhoneNumber = phone,
                        //        DueDate = entity.DueDate,
                        //        ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                        //        OverdueDays = overdue,
                        //        RepeatCount = repeatCount,
                        //        ActivityTaskStatusMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2").Select(o => o.ID).First(),
                        //        ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "1").Select(o => o.ID).First(),
                        //        ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First(),
                        //        ActivityTypeName = MasterCenters.Where(o => o.ID == entity.LeadActivityTypeMasterCenterID).Select(o => o.Name).First(),
                        //        OwnerID = leadModel.OwnerID,
                        //        LeadActivityID = entity.ID
                        //    };
                        //    this.ActivityTasks.Add(activityTaskAppointment);
                        //}
                        #endregion
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        var activityTaskTypeId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First();
                        var taskList = ActivityTasks.Where(o => o.LeadActivityID == entity.ID).ToList();
                        var isAppointment = taskList.Where(o => o.ActivityTaskTypeMasterCenterID == activityTaskTypeId).Any();
                        foreach (var item in taskList)
                        {
                            if (entity.IsDeleted)
                            {
                                item.IsDeleted = true;
                                ActivityTasks.Update(item);
                            }
                            else
                            {
                                if (item.ActivityTaskTypeMasterCenterID != activityTaskTypeId)
                                {
                                    var activityTaskStatus = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                                    Guid? activityTaskStatusId = null;

                                    int overdue = 0;
                                    if (!entity.IsCompleted)
                                    {
                                        overdue = Convert.ToInt32((entity.DueDate.Value.Date - DateTime.Today.Date).TotalDays);
                                    }
                                    else
                                    {
                                        overdue = Convert.ToInt32((entity.DueDate.Value.Date - entity.ActualDate.Value.Date).TotalDays);
                                    }
                                    Guid? overdueStatusId = null;
                                    if (overdue == 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2").Select(o => o.ID).First();
                                        activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                                    }
                                    else if (overdue < 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3").Select(o => o.ID).First();
                                        activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                                    }
                                    else if (overdue > 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1").Select(o => o.ID).First();
                                        activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                                    }

                                    item.OverdueDays = overdue;
                                    item.ActivityTaskOverdueStatusMasterCenterID = overdueStatusId;
                                    item.ActivityTaskStatusMasterCenterID = activityTaskStatusId;

                                    //var leadActivityDisqualifyTypeID = MasterCenters.Where(o => o.Key == LeadActivityFollowUpTypeKeys.Disqualify && o.MasterCenterGroupKey == MasterCenterGroupKeys.LeadActivityFollowUpType).Select(o => o.ID).FirstOrDefault();
                                    //var activityStatus = LeadActivityStatuses
                                    //    .Include(o => o.LeadActivityFollowUpType)
                                    //    .Where(o => o.ID == entity.StatusID).FirstOrDefault();

                                    //if (leadActivityDisqualifyTypeID != null && activityStatus != null)
                                    //{
                                    //    if (activityStatus.LeadActivityFollowUpType.ID == leadActivityDisqualifyTypeID)
                                    //    {
                                    //        item.IsDeleted = true;
                                    //    }
                                    //}
                                    ActivityTasks.Update(item);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateRevisitActivityTask()
        {
            var changedEntities = ChangeTracker.Entries().Where(o => (o.Entity.GetType().Name == nameof(CTM.RevisitActivity)) && (o.State == EntityState.Added || o.State == EntityState.Modified));

            foreach (var changedEntity in changedEntities.ToList())
            {
                if (changedEntity.Entity is BaseEntity)
                {

                    var entity = (CTM.RevisitActivity)changedEntity.Entity;
                    var oppModel = Opportunities.Include(o => o.Contact).Where(o => o.ID == entity.OpportunityID).First();

                    if (changedEntity.State == EntityState.Added)
                    {
                        #region Add
                        string firstName = null;
                        string lastName = null;
                        string phone = null;
                        if (oppModel.ContactID != null)
                        {
                            firstName = oppModel.Contact.FirstNameTH;
                            lastName = oppModel.Contact.LastNameTH;
                            phone = ContactPhones.Where(o => o.ContactID == oppModel.ContactID && o.IsMain == true).Select(o => o.PhoneNumber).FirstOrDefault();
                        }

                        var activityTaskStatus = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                        Guid? activityTaskStatusId = null;

                        int overdue = 0;
                        Guid? overdueStatusId = null;
                        if (overdue == 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2").Select(o => o.ID).First();
                            activityTaskStatusId = activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First();
                        }
                        else if (overdue < 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3").Select(o => o.ID).First();
                            activityTaskStatusId = activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First();
                        }
                        else if (overdue > 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1").Select(o => o.ID).First();
                            activityTaskStatusId = activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First();
                        }

                        var activityTypeKey = MasterCenters.Where(o => o.ID == entity.RevisitActivityTypeMasterCenterID).Select(o => o.Key).First();
                        int repeatCount = 0;
                        switch (activityTypeKey)
                        {
                            case "1":
                                repeatCount = 1;
                                break;
                            case "2":
                                repeatCount = 2;
                                break;
                            case "3":
                                repeatCount = 3;
                                break;
                            case "4":
                                repeatCount = 3;
                                break;
                        }

                        CTM.ActivityTask activityTask = new CTM.ActivityTask()
                        {
                            ProjectID = oppModel.ProjectID,
                            ContactFirstName = firstName,
                            ContactLastName = lastName,
                            PhoneNumber = phone,
                            DueDate = entity.ActualDate,
                            ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                            OverdueDays = overdue,
                            RepeatCount = repeatCount,
                            ActivityTaskStatusMasterCenterID = activityTaskStatusId,
                            ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "3").Select(o => o.ID).First(),
                            ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "3").Select(o => o.ID).First(),
                            ActivityTypeName = MasterCenters.Where(o => o.ID == entity.RevisitActivityTypeMasterCenterID).Select(o => o.Name).First(),
                            OwnerID = oppModel.OwnerID,
                            RevisitActivityID = entity.ID
                        };
                        this.ActivityTasks.Add(activityTask);

                        if (entity.AppointmentDate != null)
                        {
                            CTM.ActivityTask activityTaskAppointment = new CTM.ActivityTask()
                            {
                                ProjectID = oppModel.ProjectID,
                                ContactFirstName = firstName,
                                ContactLastName = lastName,
                                PhoneNumber = phone,
                                DueDate = entity.ActualDate,
                                ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                                OverdueDays = overdue,
                                RepeatCount = repeatCount,
                                ActivityTaskStatusMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2").Select(o => o.ID).First(),
                                ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "3").Select(o => o.ID).First(),
                                ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First(),
                                ActivityTypeName = MasterCenters.Where(o => o.ID == entity.RevisitActivityTypeMasterCenterID).Select(o => o.Name).First(),
                                OwnerID = oppModel.OwnerID,
                                RevisitActivityID = entity.ID
                            };
                            this.ActivityTasks.Add(activityTaskAppointment);
                        }
                        #endregion
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        var activityTaskTypeId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First();
                        var taskList = ActivityTasks.Where(o => o.RevisitActivityID == entity.ID).ToList();
                        var isAppointment = taskList.Where(o => o.ActivityTaskTypeMasterCenterID == activityTaskTypeId).Any();
                        foreach (var item in taskList)
                        {
                            if (entity.IsDeleted)
                            {
                                item.IsDeleted = true;
                                ActivityTasks.Update(item);
                            }
                            else
                            {
                                if (item.ActivityTaskTypeMasterCenterID != activityTaskTypeId)
                                {
                                    var activityTaskStatus = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                                    Guid? activityTaskStatusId = null;

                                    int overdue = 0;
                                    Guid? overdueStatusId = null;
                                    if (overdue == 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2").Select(o => o.ID).First();
                                        activityTaskStatusId = activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First();
                                    }
                                    else if (overdue < 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3").Select(o => o.ID).First();
                                        activityTaskStatusId = activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First();
                                    }
                                    else if (overdue > 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1").Select(o => o.ID).First();
                                        activityTaskStatusId = activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First();
                                    }

                                    item.OverdueDays = overdue;
                                    item.ActivityTaskOverdueStatusMasterCenterID = overdueStatusId;
                                    item.ActivityTaskStatusMasterCenterID = activityTaskStatusId;
                                    ActivityTasks.Update(item);

                                    if (!isAppointment)
                                    {
                                        if (entity.AppointmentDate != null)
                                        {
                                            CTM.ActivityTask activityTaskAppointment = new CTM.ActivityTask()
                                            {
                                                ProjectID = item.ProjectID,
                                                ContactFirstName = item.ContactFirstName,
                                                ContactLastName = item.ContactLastName,
                                                PhoneNumber = item.PhoneNumber,
                                                DueDate = item.DueDate,
                                                ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                                                OverdueDays = overdue,
                                                RepeatCount = item.RepeatCount,
                                                ActivityTaskStatusMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2").Select(o => o.ID).First(),
                                                ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "3").Select(o => o.ID).First(),
                                                ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First(),
                                                ActivityTypeName = item.ActivityTypeName,
                                                OwnerID = item.OwnerID,
                                                RevisitActivityID = item.RevisitActivityID
                                            };
                                            this.ActivityTasks.Add(activityTaskAppointment);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void UpdateOpportunityActivityTask()
        {
            var changedEntities = ChangeTracker.Entries().Where(o => (o.Entity.GetType().Name == nameof(CTM.OpportunityActivity)) && (o.State == EntityState.Added || o.State == EntityState.Modified));

            foreach (var changedEntity in changedEntities.ToList())
            {
                if (changedEntity.Entity is BaseEntity)
                {

                    var entity = (CTM.OpportunityActivity)changedEntity.Entity;
                    var oppModel = Opportunities.Include(o => o.Contact).Where(o => o.ID == entity.OpportunityID).First();

                    if (changedEntity.State == EntityState.Added)
                    {
                        #region Add
                        string firstName = null;
                        string lastName = null;
                        string phone = null;
                        if (oppModel.ContactID != null)
                        {
                            firstName = oppModel.Contact.FirstNameTH;
                            lastName = oppModel.Contact.LastNameTH;
                            phone = ContactPhones.Where(o => o.ContactID == oppModel.ContactID && o.IsMain == true).Select(o => o.PhoneNumber).FirstOrDefault();
                        }

                        var activityTaskStatus = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                        Guid? activityTaskStatusId = null;

                        int overdue = 0;
                        if (!entity.IsCompleted)
                        {
                            overdue = Convert.ToInt32((entity.DueDate.Value.Date - DateTime.Today.Date).TotalDays);
                        }
                        else
                        {
                            overdue = Convert.ToInt32((entity.DueDate.Value.Date - entity.ActualDate.Value.Date).TotalDays);
                        }
                        Guid? overdueStatusId = null;
                        if (overdue == 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2").Select(o => o.ID).First();
                            activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                        }
                        else if (overdue < 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3").Select(o => o.ID).First();
                            activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                        }
                        else if (overdue > 0)
                        {
                            overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1").Select(o => o.ID).First();
                            activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                        }

                        var ActivityTaskTypePhoneId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "2").Select(o => o.ID).First();
                        var ActivityTaskTypeHomeId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "1").Select(o => o.ID).First();
                        var ActivityTaskTypeAskId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "4").Select(o => o.ID).First();
                        var activityTypeKey = MasterCenters.Where(o => o.ID == entity.OpportunityActivityTypeMasterCenterID).Select(o => o.Key).First();
                        Guid? selectActivityTypeTask = null;
                        switch (activityTypeKey)
                        {
                            case "1":
                                selectActivityTypeTask = ActivityTaskTypeHomeId;
                                break;
                            case "2":
                                selectActivityTypeTask = ActivityTaskTypeAskId;
                                break;
                            case "3":
                                selectActivityTypeTask = ActivityTaskTypePhoneId;
                                break;
                            case "4":
                                selectActivityTypeTask = ActivityTaskTypePhoneId;
                                break;
                            case "5":
                                selectActivityTypeTask = ActivityTaskTypePhoneId;
                                break;
                            case "6":
                                selectActivityTypeTask = ActivityTaskTypePhoneId;
                                break;
                            case "7":
                                selectActivityTypeTask = ActivityTaskTypePhoneId;
                                break;
                        }

                        CTM.ActivityTask activityTask = new CTM.ActivityTask()
                        {
                            ProjectID = oppModel.ProjectID,
                            ContactFirstName = firstName,
                            ContactLastName = lastName,
                            PhoneNumber = phone,
                            DueDate = entity.DueDate,
                            ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                            OverdueDays = overdue,
                            RepeatCount = 0,
                            ActivityTaskStatusMasterCenterID = activityTaskStatusId,
                            ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "2").Select(o => o.ID).First(),
                            ActivityTaskTypeMasterCenterID = selectActivityTypeTask,
                            ActivityTypeName = MasterCenters.Where(o => o.ID == entity.OpportunityActivityTypeMasterCenterID).Select(o => o.Name).First(),
                            OwnerID = oppModel.OwnerID,
                            OpportunityActivityID = entity.ID
                        };
                        this.ActivityTasks.Add(activityTask);

                        if (entity.AppointmentDate != null)
                        {
                            CTM.ActivityTask activityTaskAppointment = new CTM.ActivityTask()
                            {
                                ProjectID = oppModel.ProjectID,
                                ContactFirstName = firstName,
                                ContactLastName = lastName,
                                PhoneNumber = phone,
                                DueDate = entity.DueDate,
                                ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                                OverdueDays = overdue,
                                RepeatCount = 0,
                                ActivityTaskStatusMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2").Select(o => o.ID).First(),
                                ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "2").Select(o => o.ID).First(),
                                ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First(),
                                ActivityTypeName = MasterCenters.Where(o => o.ID == entity.OpportunityActivityTypeMasterCenterID).Select(o => o.Name).First(),
                                OwnerID = oppModel.OwnerID,
                                OpportunityActivityID = entity.ID
                            };
                            this.ActivityTasks.Add(activityTaskAppointment);
                        }
                        #endregion
                    }
                    else if (changedEntity.State == EntityState.Modified)
                    {
                        var activityTaskTypeId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First();
                        var taskList = ActivityTasks.Where(o => o.OpportunityActivityID == entity.ID).ToList();
                        var isAppointment = taskList.Where(o => o.ActivityTaskTypeMasterCenterID == activityTaskTypeId).Any();
                        foreach (var item in taskList)
                        {
                            if (entity.IsDeleted)
                            {
                                item.IsDeleted = true;
                                ActivityTasks.Update(item);
                            }
                            else
                            {
                                if (item.ActivityTaskTypeMasterCenterID != activityTaskTypeId)
                                {
                                    var activityTaskStatus = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus).ToList();
                                    Guid? activityTaskStatusId = null;

                                    int overdue = 0;
                                    if (!entity.IsCompleted)
                                    {
                                        overdue = Convert.ToInt32((entity.DueDate.Value.Date - DateTime.Today.Date).TotalDays);
                                    }
                                    else
                                    {
                                        overdue = Convert.ToInt32((entity.DueDate.Value.Date - entity.ActualDate.Value.Date).TotalDays);
                                    }

                                    Guid? overdueStatusId = null;
                                    if (overdue == 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "2").Select(o => o.ID).First();
                                        activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                                    }
                                    else if (overdue < 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "3").Select(o => o.ID).First();
                                        activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                                    }
                                    else if (overdue > 0)
                                    {
                                        overdueStatusId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskOverdueStatus && o.Key == "1").Select(o => o.ID).First();
                                        activityTaskStatusId = (entity.IsCompleted) ? activityTaskStatus.Where(o => o.Key == "2").Select(o => o.ID).First() : activityTaskStatus.Where(o => o.Key == "3").Select(o => o.ID).First();
                                    }

                                    var ActivityTaskTypePhoneId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "2").Select(o => o.ID).First();
                                    var ActivityTaskTypeHomeId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "1").Select(o => o.ID).First();
                                    var ActivityTaskTypeAskId = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "4").Select(o => o.ID).First();
                                    var activityTypeKey = MasterCenters.Where(o => o.ID == entity.OpportunityActivityTypeMasterCenterID).Select(o => o.Key).First();
                                    Guid? selectActivityTypeTask = null;
                                    switch (activityTypeKey)
                                    {
                                        case "1":
                                            selectActivityTypeTask = ActivityTaskTypeHomeId;
                                            break;
                                        case "2":
                                            selectActivityTypeTask = ActivityTaskTypeAskId;
                                            break;
                                        case "3":
                                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                                            break;
                                        case "4":
                                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                                            break;
                                        case "5":
                                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                                            break;
                                        case "6":
                                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                                            break;
                                        case "7":
                                            selectActivityTypeTask = ActivityTaskTypePhoneId;
                                            break;
                                    }

                                    item.ActivityTaskTypeMasterCenterID = selectActivityTypeTask;
                                    item.ActivityTypeName = MasterCenters.Where(o => o.ID == entity.OpportunityActivityTypeMasterCenterID).Select(o => o.Name).First();
                                    item.OverdueDays = overdue;
                                    item.ActivityTaskOverdueStatusMasterCenterID = overdueStatusId;
                                    item.ActivityTaskStatusMasterCenterID = activityTaskStatusId;

                                    ActivityTasks.Update(item);

                                    if (!isAppointment)
                                    {
                                        if (entity.AppointmentDate != null)
                                        {
                                            CTM.ActivityTask activityTaskAppointment = new CTM.ActivityTask()
                                            {
                                                ProjectID = item.ProjectID,
                                                ContactFirstName = item.ContactFirstName,
                                                ContactLastName = item.ContactLastName,
                                                PhoneNumber = item.PhoneNumber,
                                                DueDate = item.DueDate,
                                                ActivityTaskOverdueStatusMasterCenterID = overdueStatusId,
                                                OverdueDays = overdue,
                                                RepeatCount = 0,
                                                ActivityTaskStatusMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskStatus && o.Key == "2").Select(o => o.ID).First(),
                                                ActivityTaskTopicMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskTopic && o.Key == "2").Select(o => o.ID).First(),
                                                ActivityTaskTypeMasterCenterID = MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.ActivityTaskType && o.Key == "5").Select(o => o.ID).First(),
                                                ActivityTypeName = item.ActivityTypeName,
                                                OwnerID = item.OwnerID,
                                                OpportunityActivityID = item.OpportunityActivityID
                                            };
                                            this.ActivityTasks.Add(activityTaskAppointment);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}