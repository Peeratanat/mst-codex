namespace Database.Models.DbQueries
{
    public class DBStoredNames
    {

        #region ACC
        public static string spPostGLDetailRV = "[ACC].[sp_PostGLDetail_RV]";
        public static string spPostGLDetailACRV = "[ACC].[sp_PostGLDetail_AC_RV]";
        public static string spPostGLDetailPI = "[ACC].[sp_PostGLDetail_PI]";
        public static string spPostGLDetailJV = "[ACC].[sp_PostGLDetail_JV]";
        public static string spPostGLDetailUN = "[ACC].[sp_PostGLDetail_UN]";
        public static string spPostGLDetailCA = "[ACC].[sp_PostGLDetail_CA]";
        public static string spPostGLDetailPB = "[ACC].[sp_PostGLDetail_PB]";
        public static string spPostGLDetailPBJV = "[ACC].[sp_PostGLDetail_PB_JV]";

        public static string spTransfer = "[ACC].[sp_Transfer]";
        public static string spPostGLDataList = "[ACC].[sp_PostGLDataList]";
        public static string spPostGLDetailFreedown = "[ACC].[sp_PostGLDetail_Freedown]";

        public static string fnGetActivePostingDate = "ACC.fn_GetActivePostingDate";
        #endregion

        #region FIN

        public static string spReceiptInfoSPDTO = "[FIN].[sp_ReceiptInfoSPDTO]";
        public static string spFETProjectListSPDTO = "[FIN].[sp_FETProjectListSPDTO]";
        public static string spFETUnitListSPDTO = "[FIN].[sp_FETUnitListSPDTO]";
        public static string spFETDetailList = "[FIN].[sp_FETDetailList]";
        public static string spMemoMoveMoneySPDTO = "[FIN].[sp_MemoMoveMoneySPDTO]";
        public static string dbqFeeChequeSP = "[FIN].[sp_Fee_Cheque]";
        public static string dbqCreditDebitCardSP = "[FIN].[sp_Fee_CreditDebitCard]";
        public static string sp_ReceiptHeaderSP = "[FIN].[sp_ReceiptHeaderSP]";
        public static string spDeposit = "[FIN].[sp_Deposit]";
        public static string spDepositByHID = "[FIN].[sp_DepositByHID]";
        public static string spPaymentInfoList = "[FIN].[sp_PaymentInfoList]";
        public static string spUnknownPaymentList = "[FIN].[sp_UnknownPaymentList]";
        public static string spGetExportReceiptDate = "[FIN].[sp_getExportReceiptDate]";
        public static string sp_GetRemainInstallment_ApprovedDirectDebitCredit = "[FIN].[sp_GetRemainInstallment_ApprovedDirectDebitCredit]";
        public static string spDirectCreditDebit = "[FIN].[sp_DirectCreditDebit]";
        public static string spFINUnitDocument = "[FIN].[sp_FINUnitDocument]";

        public static string spPaymentUnitPriceList = "[FIN].[sp_PaymentUnitPriceList]";
        public static string spPaymentUnitPriceV2List = "[FIN].[sp_PaymentUnitPriceV2List]"; //for PrepareTransferNew
        public static string spPaymentUnitPriceV3List = "[FIN].[sp_PaymentUnitPriceV3List]"; //for EventBooking

        public static string fn_BillPayment_Validate_ReferentKey = "[FIN].[fn_BillPayment_Validate_ReferentKey]";
        public static string fn_DirectDCDD_RemainPeriod = "[FIN].[fn_DirectDCDD_RemainPeriod]";

        public static string sp_iCRMNotiApproveDirectCredit = "[dbo].[sp_iCRM_Noti_ApproveDirectCredit]";
        public static string sp_iCRMNotiApproveDirectDebit = "[dbo].[sp_iCRM_Noti_ApproveDirectDebit]";

        public static string sp_LET_CHECK_PaidDownPayment = "[dbo].[sp_LET_CHECK_PaidDownPayment]";

        public static string sp_Search_vw_UnknownPaymentLetter = "[dbo].[sp_Search_vw_UnknownPaymentLetter_2]";

        public static string sp_BeforeTransferPaymentSeperate = "FIN.sp_BeforeTransferPaymentSeperate";

        public static string sp_Get_ProjectBGX = "[dbo].[sp_Get_ProjectBGX]";

        public static string sp_Check_AllowBookingNationality = "[dbo].[sp_Check_AllowBookingNationality]";
        public static string sp_CD_GetQuotaFQTHByProject = "[dbo].[sp_CD_GetQuotaFQTHByProject]";
        public static string sp_GetPaymentHistoryEmail = "[FIN].[SP_GetPaymentHistoryEmail]";
        #endregion

        #region SALE
        public static string spDownPaymentLetterList = "[SAL].[sp_LET_GET_DownPaymentLetter]";
        public static string spDownPaymentLetterHistory = "[SAL].[sp_LET_GET_DownPaymentLetterHistory]";
        public static string spTransferLetterList = "[SAL].[sp_LET_GET_TransferLetter]";
        public static string spTransferLetterList_REP = "DBLINK_SVR_REVREP.crmrevo.[SAL].[sp_LET_GET_TransferLetter]";
        public static string spTransferLetterHistory = "[SAL].[sp_LET_GET_TransferLetterHistory]";
        public static string sp_UNIT_SEARCH_UnitInfoList = "[SAL].[sp_UNIT_SEARCH_UnitInfoList]";
        public static string sp_UNIT_SEARCH_UnitInfoList_By_Contact = "[SAL].[sp_UNIT_SEARCH_UnitInfoList_By_Contact]";
        public static string sp_SAL_GET_TransferList = "[SAL].[sp_SAL_GET_TransferList]";
        public static string spLCTargetSel = "[SAL].[SP_LCTarget_Sel]";
        public static string sp_GetListUserKCashCard = "[dbo].[sp_GetListUserKCashCard]";
        public static string sp_ExcelDownPaymentLetterList = "[SAL].[sp_LET_GET_ExcelDownPaymentLetterList]";
        public static string sp_ExcelTransferLetterList = "[SAL].[sp_LET_GET_ExcelTransferLetterList]";
        public static string spBookingOfflineList = "[SAL].[sp_SAL_GET_BookingOfflineList]";

        public static string sp_GetYear = "[SAL].[sp_SAL_GET_Year]";
        public static string sp_GetPerformanceTarget_KPILLToGO_ByProject = "[SAL].[sp_SAL_GET_PerformanceTarget_KPILLToGO_ByProject]";
        public static string sp_GETPerformanceTarget_LC_ByProject = "[SAL].[sp_SAL_GET_PerformanceTarget_LC_ByProject]";
        public static string sp_UpdatePerformanceTarget_KPILLToGO_ByProject = "[SAL].[sp_SAL_Update_PerformanceTarget_KPILLToGO_ByProject]";
        public static string sp_UpdatePerformanceTarget_LC_ByProject = "[SAL].[sp_SAL_Update_PerformanceTarget_LC_ByProject]";
        public static string sp_SyncPerformanceTarget_LC_ByProject = "[SAL].[sp_SAL_Sync_PerformanceTarget_LC_ByProject]";

        public static string sp_Prebook_ConvertToPresale = "[SAL].[sp_Prebook_ConvertToPresale]";
        public static string sp_Prebook_ConvertPYToPresale = "[SAL].[sp_Prebook_ConvertPYToPresale]";

        public static string sp_GenTotalAmountOverDue = "[dbo].[sp_GenTotalAmountOverDue]";
        public static string sp_GenTotalPeriodOverDue = "[dbo].[sp_GenTotalPeriodOverDue]";

        public static string sp_ConvertPaymentPrebookToPayment = "[SAL].[sp_Prebook_ConvertToPayment]";

        public static string sp_GetUserAGList = "[SAL].[sp_GetUserAG]";
        public static string sp_GetAgreementFileList = "[SAL].[sp_GetAgreementFileList]";
        public static string sp_CHECK_TitledeedRequestFlow = "[SAL].[sp_CHECK_TitledeedRequestFlow]";
        public static string sp_CHECK_TitledeedRequestFlow_ChangeDue = "[SAL].[sp_CHECK_TitledeedRequestFlow_ChangeDue]";

        public static string sp_GetContactIDbyFGFCode = "[SAL].[sp_GetContactIDbyFGFCode]";

        public static string sp_UpdateFGFAlreadyInUsed = "[SAL].[sp_UpdateFGFAlreadyInUsed]";

        public static string sp_CreateNewTransferPRRequest = "[PRM].[CreateNewTransferPRRequestJob]";

        public static string sp_CreateNewSalePRRequest = "[PRM].[CreateNewSalePRRequestJob]";
        #endregion

        #region Promotion
        public static string sp_GET_PromotionRequestDelivery = "[PRM].[sp_PRM_GET_PromotionRequestDelivery]";
        public static string spGetDataItemByProject = "Stock_Promotion_REVO.[dbo].[spGetDataItemByProject]";
        public static string spCutStock = "Stock_Promotion_REVO.[dbo].[spCRMDeliveryItem]";
        public static string spCancelCutStock = "Stock_Promotion_REVO.[dbo].[spCancelDeliveryItem]";
        public static string sp_GET_SalePromotionRequestItemList = "[PRM].[sp_PRM_GET_SalePromotionRequestItemList]";
        public static string sp_GET_TransferPromotionRequestItemList = "[PRM].[sp_PRM_GET_TransferPromotionRequestItemList]";
        public static string sp_GET_PreSalePromotionRequestList = "[PRM].[sp_PRM_GET_PreSalePromotionRequestList]";
        public static string spMappingAgreementList = "[PRM].[sp_MappingAgreementList]";
        public static string sp_GET_PromotionRequestNotifictionMail = "[PRM].[sp_PRM_GET_PromotionRequestNotifictionMail]";

        public static string sp_PrintoutReceivePromotionHeader = "[dbo].[Z_SP_PRINTOUT_ReceivePromotion]";
        public static string sp_PrintoutReceivePromotionDetail = "[dbo].[Z_SP_PRINTOUT_ReceivePromotion_1]";
        #endregion

        #region IDT
        public static string spGetUserRole = "[USR].[sp_GetUserRole]";
        public static string spGetUserMenu = "[USR].[sp_GetUserMenu]";
        public static string spGetUserMenuAction = "[USR].[sp_GetUserMenuAction]";

        public static string spChangeUserRole = "[USR].[sp_ChangeUserRole]";

        public static string spGetUserDashboardMenu = "[USR].[sp_GetUserDashBoardMenu]";
        #endregion

        #region COMMISSION
        public static string sp_CommissionSettingList = "[CMS].[sp_CMS_GET_CommissionSettingList]";
        public static string sp_ChangeLCSaleList = "[CMS].[sp_CMS_GET_ChangeLCSaleList]";
        public static string sp_ChangeLCTransferList = "[CMS].[sp_CMS_GET_ChangeLCTransferList]";
        public static string sp_CommissionLowRiseList = "[CMS].[sp_CMS_GET_CommissionLowRiseList]";
        public static string sp_CommissionHighRiseSaleList = "[CMS].[sp_CMS_GET_CommissionHighRiseSaleList]";
        public static string sp_CommissionHighRiseTransferList = "[CMS].[sp_CMS_GET_CommissionHighRiseTransferList]";
        public static string sp_RateSettingSaleList = "[CMS].[sp_CMS_GET_RateSettingSaleList]";
        public static string sp_RateSettingTransferList = "[CMS].[sp_CMS_GET_RateSettingTransferList]";
        public static string sp_RateSettingAgentList = "[CMS].[sp_CMS_GET_RateSettingAgentList]"; 
        public static string sp_RateSettingFixSaleList = "[CMS].[sp_CMS_GET_RateSettingFixSaleList]";
        public static string sp_RateSettingFixTransferList = "[CMS].[sp_CMS_GET_RateSettingFixTransferList]";
        public static string sp_RateSettingFixSaleModelList = "[CMS].[sp_CMS_GET_RateSettingFixSaleModelList]";
        public static string sp_RateSettingFixTransferModelList = "[CMS].[sp_CMS_GET_RateSettingFixTransferModelList]";
        #endregion

        #region PROJECT
        public static string sp_MinPriceList = "[PRJ].[sp_PRJ_GET_MinPriceList]";
        public static string sp_BudgetPromotionList = "[PRJ].[sp_PRJ_GET_BudgetPromotionList]";
        public static string sp_MinPriceSAPList = "[PRJ].[sp_PRJ_Expt_MinPriceList]";
        public static string sp_TitleDeedStatusList = "[PRJ].[sp_PRJ_GET_TitleDeedStatusList]";
        public static string sp_GET_TitledeedDetailLandStatus = "[PRJ].[sp_GET_TitledeedDetailLandStatus]";
        #endregion

        #region Admin
        public static string sp_Admin_Search_UnitStatusList = "[dbo].[sp_Admin_Search_UnitStatusList]";
        #endregion

        #region Customer
        public static string spConsentList = "[CTM].[sp_CTM_GET_Consent]";
        public static string spCreateOppRelationWalk = "[CTM].[sp_CreateOppRelationWalk]";
        #endregion

        #region Refund
        public static string spAccountApproveGenTextFileGLToSAP = "[RFN].[sp_AccountApproveGenTextFileGLToSAP]";

        public static string spContactRefundUpdateGLTextFileName = "[RFN].[sp_ContactRefundUpdateGLTextFileName]";

        public static string spContactRefundClearData = "[RFN].[sp_ContactRefundClearData]";

        public static string spUpdateAPChangeAmountToReFund = "[RFN].[sp_UpdateAPChangeAmountToReFund]";
        #endregion

        #region Smart_finance
        //public static string sp_GetProjectDatail = "[PRJ].[sp_EXT_GET_GetProjectDatail]";
        public static string sp_GetListAllMasterProject = "[dbo].[sp_CRM2SSMFI_Get_MstProject]";
        public static string sp_GetTransferOwnerShipOfUnit = "[dbo].[sp_CRM2SSMFI_Get_UnitTransfer]";
        public static string sp_GetTitle = "[dbo].[sp_CRM2SSMFI_Get_MstTitle]";
        public static string sp_GetProvince = "[dbo].[sp_CRM2SSMFI_Get_MstProvince]";
        public static string sp_GetDistrict = "[dbo].[sp_CRM2SSMFI_Get_MstDistrict]";
        public static string sp_GetSubDistrict = "[dbo].[sp_CRM2SSMFI_Get_MstSubDistrict]";
        public static string sp_GetZipCode = "[dbo].[sp_CRM2SSMFI_Get_MstZipCode]";
        public static string sp_GetAllUnitByProject = "[dbo].[sp_CRM2SSMFI_Get_AllUnitByProject]";
        #endregion

        #region QIS
        public static string sp_qIS_GetUnitInfo = "[dbo].[sp_qIS_GetUnitInfo]";
        #endregion

        #region Digital
        public static string sp_GetMstProject = "[dbo].[sp_Get_MstProject]";
        #endregion

        #region USER
        public static string sp_GetLCUserLeadToAssign = "[USR].[sp_GetLCUserLeadToAssign]";
        public static string sp_GetLeadDatas = "[USR].[sp_GetLeadDatas]";
        public static string sp_GetContactPhones = "[USR].[sp_GetContactPhones]"; 
        public static string sp_GetAlllLCUserLead = "[USR].[sp_GetAlllLCUserLead]";
        public static string sp_UserKCashCard = "[dbo].[sp_UserKCashCard]";
        public static string sp_Sync_UserRoleProjectByEmpCode = "[dbo].[sp_Sync_UserRoleProjectByEmpCode]";
        #endregion

        #region
        public static string sp_LET_GET_PrepareDownPaymentLetter = "dbo.sp_LET_GET_PrepareDownPaymentLetter";
        #endregion

        public static string spGetUserListForChangeOpp = "[CTM].[sp_GetUserListForChangeOpp]";
        public static string spGetOppListForChangeOpp = "[CTM].[sp_GetOppListForChangeOpp]";
        #region MST
        public static string spGetUnitCanCombine = "[MST].[sp_GetUnitCanCombine]";
        public static string spGetUnitCombineList = "[MST].[sp_GetUnitCombineList]";
        public static string spGetUnitCombineHistoryList = "[MST].[sp_GetUnitCombineHistoryList]";
        public static string spGetVPUserByProject = "[MST].[sp_GetVPUserByProject]";
        public static string spGenRunningNumber = "[MST].[sp_GenRunningNumber]";
        #endregion
        #region
        public static string spPrebookConvertToContact = "[SAL].[sp_Prebook_ConvertToContact]";
        public static string spExecutiveReportTotalBooking = "[dbo].[AP2SP_ExecutiveReport_TotalBooking_V2]";
        #endregion

        #region
        public static string sp_GetMasterPlanUnitDetail = "dbo.sp_GetMasterPlanUnitDetail";
        #endregion
        #region GP Simulate
        public static string spROIGPCal1 = "[ROI].[sp_GPCal1]";
        public static string spROIGPCal2 = "[ROI].[sp_GPCal2]";
        #endregion
    }
}
