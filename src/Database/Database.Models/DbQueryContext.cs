using System;
using Database.Models.DbQueries;
using Database.Models.DbQueries.ACC;
using Database.Models.DbQueries.ADM;
using Database.Models.DbQueries.CMS;
using Database.Models.DbQueries.CTM;
using Database.Models.DbQueries.EQN;
using Database.Models.DbQueries.EXT;
using Database.Models.DbQueries.FIN;
using Database.Models.DbQueries.Finance;
using Database.Models.DbQueries.IDT;
using Database.Models.DbQueries.MST;
using Database.Models.DbQueries.PRJ;
using Database.Models.DbQueries.PRM;
using Database.Models.DbQueries.RFN;
using Database.Models.DbQueries.SAL;
using Database.Models.DbQueries.USR;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    public class DbQueryContext : DbContext
    {
        public DbQueryContext(DbContextOptions<DbQueryContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply HasNoKey to all entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entityType.ClrType).HasNoKey();
            }

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<dbqMasterCenterResult> MasterCenterResults { get; set; }

        public DbSet<DBFunctionOutputScalar<string>> fnScalarValue_String { get; set; }
        public DbSet<DBFunctionOutputScalar<int?>> fnScalarValue_Int { get; set; }
        public DbSet<DBFunctionOutputScalar<decimal?>> fnScalarValue_Decimal { get; set; }
        public DbSet<DBFunctionOutputScalar<double?>> fnScalarValue_Double { get; set; }
        public DbSet<DBFunctionOutputScalar<DateTime?>> fnScalarValue_DateTime { get; set; }
        public DbSet<DBFunctionOutputScalar<Guid?>> fnScalarValue_Guid { get; set; }
        public DbSet<DBFunctionOutputScalar<bool?>> fnScalarValue_Boolean { get; set; }

        //public DbSet<DBFunctionOutputScalar<double?>> fnTS_GetBOConfigrate_TransferFeeRate { get; set; }
        //public DbSet<DBFunctionOutputScalar<double?>> fnTS_GetBOConfigrate_MortgageRate { get; set; }
        //public DbSet<DBFunctionOutputScalar<decimal?>> Fn_TSF_CALC_EstimatePriceBooking { get; set; }

        #region FIN
        public DbSet<dbqMemoMoveMoneySP> dbqMemoMoveMoneySPs { get; set; }
        public DbSet<dbqSPReceiptInfo> dbqSPReceiptInfos { get; set; }
        public DbSet<dbqReceiptHeaderSP> dbqReceiptHeaderSPs { get; set; }
        public DbSet<dbqFETProjectListSP> dbqFETProjectListSPs { get; set; }
        public DbSet<dbqFETUnitListSP> dbqFETUnitListSPs { get; set; }
        public DbSet<dbqFETDetailListSP> dbqFETDetailListSPs { get; set; }
        public DbSet<dbqFeeChequeSP> dbqFeeChequeSPs { get; set; }
        public DbSet<dbqDepositSP> dbqDepositSPs { get; set; }
        public DbSet<dbqCreditDebitCardSP> dbqCreditDebitCardSPs { get; set; }
        public DbSet<fn_BillPayment_Validate_ReferentKey> fn_BillPayment_Validate_ReferentKey { get; set; }
        public DbSet<dbq_GetRemainInstallment_ApprovedDirectDebitCredit> sp_GetRemainInstallment_ApprovedDirectDebitCredit { get; set; }

        public DbSet<dbqSPPaymentInfoList> dbqSPPaymentInfoLists { get; set; }
        public DbSet<dbqSPUnknownPaymentList> dbqSPUnknownPaymentLists { get; set; }
        public DbSet<dbqSPGetExportReceiptDate> dbqSPGetExportReceiptDates { get; set; }

        public DbSet<dbqDirectDCDDRemainPeriod> dbqDirectDCDDRemainPeriods { get; set; }
        public DbSet<dbpDirectCreditDebit> dbpDirectCreditDebits { get; set; }
        public DbSet<dbqFINUnitDocument> dbqFINUnitDocument { get; set; }

        public DbSet<dbqSPPaymentUnitPriceList> dbqSPPaymentUnitPriceLists { get; set; }
        public DbSet<dbqSPPaymentUnitPriceListV2> dbqSPPaymentUnitPriceListV2 { get; set; }

        public DbSet<sqlPaymentHistory.QueryResult> sqlPaymentHistoryResults { get; set; }
        public DbSet<sqlGetActiveAgreementOwner.QueryResult> sqlGetActiveAgreementOwnerResults { get; set; }
        public DbSet<sqlBookingForPayment.QueryResult> sqlBookingForPaymentResults { get; set; }
        public DbSet<sqlUnknownPaymentRemain.QueryResult> sqlUnknownPaymentRemainResults { get; set; }
        public DbSet<sqlvwAgreementByUnitLetterInfo.QueryResult> sqlvwAgreementByUnitLetterInfoResults { get; set; }
        public DbSet<sqlMstCalendarWeek.QueryResult> sqlMstCalendarWeekResults { get; set; }

        public DbSet<sqlPaymentUnitPrice.QueryResult_Document> sqlPaymentUnitPrice_DocumentResults { get; set; }
        public DbSet<sqlPaymentUnitPrice.QueryResult_UnitPrice> sqlPaymentUnitPrice_UnitPriceResults { get; set; }
        public DbSet<sqlPaymentUnitPrice.QueryResult_Installment> sqlPaymentUnitPrice_InstallmentResults { get; set; }
        public DbSet<sqlPaymentUnitPrice.QueryResult_UnitPayment> sqlPaymentUnitPrice_UnitPaymentResults { get; set; }

        public DbSet<sp_CheckPaidDownPayment> spCheckPaidDownPayment { get; set; }
        public DbSet<dbqSPSearchvwUnknownPaymentLetter> spSearchvwUnknownPaymentLetter { get; set; }

        public DbSet<sp_GetPaymentHistoryEmail> spGetPaymentHistoryEmail { get; set; }
        #endregion

        #region ACC
        public DbSet<dbqPostingGLDetail> dbqPostGLDetails { get; set; }
        public DbSet<dbqSessionKeySearch> dbqSessionKeySearchs { get; set; }
        public DbSet<dbqTransfer> dbqTransfers { get; set; }
        public DbSet<dbqPostGLHeaderID> dbqPostGLDetailFreedownGuid { get; set; }

        public DbSet<dbqPostGLData> PostGLDatas { get; set; }


        public DbSet<sqlGetContactRefund.QueryResult> sqlGetContactRefund { get; set; }

        public DbSet<sqlGetContactRefund.QueryURLResult> sqlGetContactRefundURL { get; set; }

        #endregion

        #region SALE
        public DbSet<dbqDownPaymentLetter> dbqDownPaymentLetters { get; set; }
        public DbSet<dbqDownPaymentLetterHistory> dbqDownPaymentLetterHistorys { get; set; }
        public DbSet<dbqTransferLetter> dbqTransferLetters { get; set; }
        public DbSet<dbqTransferLetterHistory> dbqTransferLetterHistorys { get; set; }

        public DbSet<dbqUnitInfoList> dbqUnitInfoLists { get; set; }
        public DbSet<dbqTransferList> dbqTransferLists { get; set; }
        public DbSet<dbqLCTargetSelsList> dbqLCTargetSelsLists { get; set; }
        public DbSet<dbqUnitAreaNationality> dbqUnitAreaNationalities { get; set; }
        public DbSet<dbqCustomerAnswers> dbqCustomerAnswers { get; set; }
        public DbSet<dbqAgreementAllOwnerName> dbqAgreementAllOwnerNames { get; set; }
        public DbSet<dbqPCardUserList> dbqPCardUserLists { get; set; }
        public DbSet<dbqWorkFlowMinPriceState> dbqWorkFlowMinPriceStates { get; set; }
        public DbSet<dbqExcelLetterList> dbqExcelLetterLists { get; set; }
        public DbSet<dbqBookingOfflineList> dbqBookingOfflineLists { get; set; }

        public DbSet<dbqGetYearsList> dbqGetYearResult { get; set; }
        public DbSet<dbqGetPerformanceTargetKpi> dbqGetPerformanceTargetKpiResult { get; set; }
        public DbSet<dbqGetPerformanceTargetLCs> dbqGetPerformanceTargetLCsResult { get; set; }

        public DbSet<dbqGetQuotation> dbqGetQuotationResult { get; set; }
        public DbSet<dbqConvertPYToPresale> dbqConvertPYToPresaleResult { get; set; }

        public DbSet<dbqUserAGList> dbqUserAGLists { get; set; }

        public DbSet<dbqAgreementFileList> dbqAgreementFileLists { get; set; }
        public DbSet<dbqCHECKTitledeedRequestFlowChangeDue> dbqCHECKTitledeedRequestFlowChangeDue { get; set; }

        public DbSet<dbqGetContactIDbyFGFCode> dbqGetContactIDbyFGFCodeResult { get; set; }

        public DbSet<dbqFGFAlreadyInUsed> dbqFGFAlreadyInUsedResult { get; set; }

        public DbSet<spGetString> spGetStringResult { get; set; }

        //public DbSet<DBFunctionOutputScalar<dbqFGFAlreadyInUsed>> dbqFGFAlreadyInUsedResult { get; set; }

        #endregion

        #region Promotion
        public DbSet<dbqPromotionRequestDeliveryList> dbqPromotionRequestDeliveryLists { get; set; }
        public DbSet<dbqStockItemList> dbqStockItemLists { get; set; }
        public DbSet<dbqSalePromotionRequestItemForDelivery> dbqSalePromotionRequestItemForDeliverys { get; set; }
        public DbSet<dbqTransferPromotionRequestItemForDelivery> dbqTransferPromotionRequestItemForDeliverys { get; set; }
        public DbSet<sqlCheckStock.QueryResult> sqlCheckStocks { get; set; }
        public DbSet<dbqPreSalePromotionRequestList> dbqPreSalePromotionRequestLists { get; set; }
        public DbSet<dbqMappingAgreement> dbqMappingAgreements { get; set; }
        public DbSet<sqlDeliveryStock.QueryResult> sqlDeliveryStocks { get; set; }
        public DbSet<sqlDupSalePromotion.QueryResult> sqlDupSalePromotion { get; set; }
        public DbSet<sqlDupPreSalePromotion.QueryResult> sqlDupPreSalePromotion { get; set; }
        public DbSet<sqlDupTransferPromotion.QueryResult> sqlDupTransferPromotion { get; set; }

        public DbSet<spPrintoutReceivePromotionHeader> spPrintoutReceivePromotionHeaderResult { get; set; }
        public DbSet<spPrintoutReceivePromotionHeaderDetail> spPrintoutReceivePromotionDetailResult { get; set; }

        #endregion

        #region IDT
        public DbSet<dbqGetUserRole> dbqGetUserRoles { get; set; }
        public DbSet<dbqGetUserDashboardMenu> dbqGetUserDashboardMenus { get; set; }
        public DbSet<dbqGetUserMenu> dbqGetUserMenus { get; set; }
        public DbSet<dbqGetUserMenuAction> dbqGetUserMenuActions { get; set; }
        public DbSet<dbqChangeUserRoleResult> dbqChangeUserRoleResults { get; set; }

        public DbSet<sqlMenuAction.QueryResult> sqlMenuActionPermissionResults { get; set; }
        public DbSet<sqlRolePermission.QueryResult> sqlRolePermissionResults { get; set; }

        #endregion

        #region COMMISSION
        public DbSet<dbqCommissionSettingList> dbqCommissionSettingLists { get; set; }
        public DbSet<dbqChangeLCSaleList> dbqChangeLCSaleLists { get; set; }
        public DbSet<dbqChangeLCTransferList> dbqChangeLCTransferLists { get; set; }
        public DbSet<dbqCommissionLowRiseList> dbqCommissionLowRiseLists { get; set; }
        public DbSet<dbqCommissionHighRiseSaleList> dbqCommissionHighRiseSaleLists { get; set; }
        public DbSet<dbqCommissionHighRiseTransferList> dbqCommissionHighRiseTransferLists { get; set; }
        public DbSet<sqlExportExcelRateSale.QueryResult> sqlExportExcelRateSales { get; set; }
        public DbSet<sqlExportExcelRateTransfer.QueryResult> sqlExportExcelRateTransfers { get; set; }
        public DbSet<sqlRateSettingSale.QueryResult> sqlRateSettingSales { get; set; }
        public DbSet<sqlRateSettingTransfer.QueryResult> sqlRateSettingTransfers { get; set; }
        public DbSet<dbqRateSettingSaleList> dbqRateSettingSaleLists { get; set; }
        public DbSet<dbqRateSettingTransferList> dbqRateSettingTransferLists { get; set; }
        public DbSet<dbqRateSettingAgentList> dbqRateSettingAgentLists { get; set; }
        public DbSet<dbqRateSettingFixSaleModel> dbqRateSettingFixSaleModels { get; set; }
        public DbSet<dbqRateSettingFixTransferModel> dbqRateSettingFixTransferModels { get; set; }
        public DbSet<dbqRateSettingFixSale> dbqRateSettingFixSales { get; set; }
        public DbSet<dbqRateSettingFixTransfer> dbqRateSettingFixTransfers { get; set; }
        public DbSet<sqlRateSettingFixSaleModelProject.QueryResult> sqlRateSettingFixSaleModelProjects { get; set; }
        public DbSet<sqlRateSettingFixTransferModelProject.QueryResult> sqlRateSettingFixTransferModelProjects { get; set; }
        public DbSet<sqlSaleUserProject.QueryResult> sqlSaleUserProjects { get; set; }
        public DbSet<sqlSaleUserProjectInZone.QueryResult> sqlSaleUserProjectInZones { get; set; }
        #endregion

        #region MST
        public DbSet<sqlCompanyAddressByBooking.QueryResult> sqlCompanyAddressByBookingResults { get; set; }
        public DbSet<sqlCompanyAddressByPreBook.QueryResult> sqlCompanyAddressByPreBookResults { get; set; }
        public DbSet<dbqUnitCanCombine> sqlGetUnitCanCombineResults { get; set; }
        public DbSet<dbqUnitCombine> sqlGetUnitCombineLitResults { get; set; }
        public DbSet<dbqVPUser> sqlGetVPUserByProject { get; set; }
        #endregion

        #region CTM
        public DbSet<sqlContactAddressByProject.QueryResult> sqlContactAddressByProjectResults { get; set; }
        public DbSet<dbqConsentListSP> dbqSPConsentLists { get; set; }
        public DbSet<dbqGetUserListForChangeOppSP> GetUserListForChangeOppList { get; set; }
        public DbSet<dbqGetOppListForChangeOppSP> GetOppListForChangeOppList { get; set; }

        #endregion

        #region PRJ
        public DbSet<dbqMinPriceList> dbqMinPriceLists { get; set; }
        public DbSet<dbqBudgetPromotionList> dbqBudgetPromotionLists { get; set; }
        public DbSet<sqlUnitDropdownSellPriceList.QueryResult> sqlUnitDropdownSellPriceLists { get; set; }
        public DbSet<dbqMinPriceSAPList> dbqMinPriceSAPLists { get; set; }
        public DbSet<dbqTitleDeedStatusList> dbqTitleDeedStatusLists { get; set; }
        public DbSet<dbqSPGETTitledeedDetailLandStatus> dbqSPGETTitledeedDetailLandStatus { get; set; }
        public DbSet<sqlUnitDropdownDefectList.QueryResult> sqlUnitDropdownDefectLists { get; set; }
        #endregion

        #region EQN
        public DbSet<sqlCustomerTransQAns.QueryResult> sqlCustomerTransQAnss { get; set; }
        #endregion

        #region Admin
        public DbSet<dbqUnitStatusList> dbqUnitStatusLists { get; set; }
        #endregion

        #region EXT

        public DbSet<sqlGetPricelist.QueryResult> sqlGetPricelistResults { get; set; }
        public DbSet<sqlPostponeDropdownList.QueryResult> sqlPostponeDropdownListResults { get; set; }
        public DbSet<sqlCancleMemoDropdownList.QueryResult> sqlCancleMemoDropdownListResults { get; set; }
        public DbSet<sqlTranferPromotionDropdownList.QueryResult> sqlTranferPromotionDropdownListResults { get; set; }
        public DbSet<sqlChangeUnitAgreementDropdownList.QueryResult> sqlChangeUnitAgreementDropdownListResults { get; set; }
        public DbSet<dbqGetTransferOwnerShipOfUnitList> dbqGetTransferOwnerShipOfUnitResults { get; set; }
        public DbSet<dbqGetProjectDetailList> dbqProjectDetailResults { get; set; }
        public DbSet<dbqGetSubDistrictList> dbqGetSubDistrictResult { get; set; }
        public DbSet<dbqGetDistrictList> dbqGetDistrictResult { get; set; }
        public DbSet<dbqGetProvinceList> dbqGetProvinceResult { get; set; }
        public DbSet<dbqGetZipCodeList> dbqGetZipCodeResult { get; set; }
        public DbSet<dbqGetTitleList> dbqGetTitleResult { get; set; }
        public DbSet<dbqGetAllUnitByProject> dbqGetAllUnitByProjectResult { get; set; }

        public DbSet<dbqGetProjectList> dbqGetProjectResults { get; set; }

        //public DbSet<dbqGetTransferOwnershipOfUnit> dbqGetTransferOwnershipOfUnitResult { get; set; }
        //public DbSet<dbqGetListAllMasterProject> dbqGetListAllMasterProjectResult { get; set; }
        public DbSet<dbqPrePareDownPaymentLetterExecData> dbqPrePareDownPaymentLetterExecResult { get; set; }

        public DbSet<dbqExecutiveReportTotalBooking> dbqExecutiveReportTotalBooking { get; set; }
        #endregion

        #region RFN
        public DbSet<sqlContactRefund> sqlContactRefundList { get; set; }
        public DbSet<sqlDeleteRefundDocref> sqlDeleteRefundDocref { get; set; }
        #endregion

        #region QIS
        public DbSet<dbqGetUnitInfo> dbqGetUnitInfoResult { get; set; }
        #endregion

        #region USER
        public DbSet<dbqLeadUserList> dbqLeadUserList { get; set; }
        public DbSet<dbqLeadDataList> dbqLeadDataList { get; set; }
        public DbSet<dbqContactPhoneList> dbqContactPhoneList { get; set; }
        public DbSet<dbqKCashCardUserList> dbqKCashCardUserList { get; set; }
        #endregion

        public DbSet<dbqGetMasterPlanUnitDetail> dbqGetMasterPlanUnitDetails { get; set; }
        public DbSet<dbqCheckAllowBookingNationality> dbqCheckAllowBookingNationalitys { get; set; }

        public DbSet<dbqCDGetQuotaFQTHByProject> dbqCDGetQuotaFQTHByProject { get; set; }

        public DbSet<dbqUnitDropdownDefect> dbqUnitDropdownDefects { get; set; }
    }
}
