using System;
using System.Collections.Generic;
using System.Linq;
using static Database.Models.DbQueries.DBQueryParam;

namespace Database.Models.DbQueries.SAL
{
    public static class sqlGetTransferLetterList
    {
        public static string QueryString = @"
        DECLARE @ProjectID UNIQUEIDENTIFIER = NULL
          , @ProjectIDs NVARCHAR(MAX) = NULL
          , @TransferLetterStutusID INT = 0
          , @ChangeAgreementOwnerTypeID UNIQUEIDENTIFIER = NULL
          , @UnitNo NVARCHAR(100) = N''
          , @DisplayName NVARCHAR(1000) = N''
          , @TransferOwnershipDateFrom DATETIME = '19000101'
          , @TransferOwnershipDateTo DATETIME = '70000101'
          , @TransferLetterType UNIQUEIDENTIFIER = NULL
          , @ResponseDateFrom DATETIME = '19000101'
          , @ResponseDateTo DATETIME = '70000101'
          , @LetterStatus UNIQUEIDENTIFIER = NULL
          , @LastTransferLetterTypeID UNIQUEIDENTIFIER = NULL
          , @LastDownPaymentLetterTypeID UNIQUEIDENTIFIER = NULL
          , @AppointmentTransferDateFrom DATETIME = '19000101'
          , @AppointmentTransferDateTo DATETIME = '70000101'
          , @NewTransferOwnershipDateFrom DATETIME = '19000101'
          , @NewTransferOwnershipDateTo DATETIME = '70000101'
          , @PostponeTransferDateFrom DATETIME = '19000101'
          , @PostponeTransferDateTo DATETIME = '70000101'
          , @WaitForCancelStatusID INT = 0
          , @LastTransferLetterDateFrom DATETIME = '19000101'
          , @LastTransferLetterDateTo DATETIME = '70000101'
          , @MailConfirmCancelSendDateFrom DATETIME = '19000101'
          , @MailConfirmCancelSendDateTo DATETIME = '70000101'
          , @MailConfirmCancelResponseDateFrom DATETIME = '19000101'
          , @MailConfirmCancelResponseDateTo DATETIME = '70000101'
          , @MailConfirmCancelResponseTypeID UNIQUEIDENTIFIER = NULL
          , @Sys_SortBy NVARCHAR(50) = N''
          , @Sys_SortType NVARCHAR(50) = N'asc'
          , @Page INT = 1
          , @PageSize INT = 50;

        BEGIN

            SELECT [Param]

            IF (@TransferOwnershipDateFrom IS NULL)
                SET @TransferOwnershipDateFrom = '19000101';
            IF (@TransferOwnershipDateTo IS NULL)
                SET @TransferOwnershipDateTo = '70000101';
            IF (@ResponseDateFrom IS NULL)
                SET @ResponseDateFrom = '19000101';
            IF (@ResponseDateTo IS NULL)
                SET @ResponseDateTo = '70000101';
            IF (@AppointmentTransferDateFrom IS NULL)
                SET @AppointmentTransferDateFrom = '19000101';
            IF (@AppointmentTransferDateTo IS NULL)
                SET @AppointmentTransferDateTo = '70000101';
            IF (@NewTransferOwnershipDateFrom IS NULL)
                SET @NewTransferOwnershipDateFrom = '19000101';
            IF (@NewTransferOwnershipDateTo IS NULL)
                SET @NewTransferOwnershipDateTo = '70000101';
            IF (@PostponeTransferDateFrom IS NULL)
                SET @PostponeTransferDateFrom = '19000101';
            IF (@PostponeTransferDateTo IS NULL)
                SET @PostponeTransferDateTo = '70000101';
            IF (@LastTransferLetterDateFrom IS NULL)
                SET @PostponeTransferDateFrom = '19000101';
            IF (@LastTransferLetterDateTo IS NULL)
                SET @PostponeTransferDateTo = '70000101';
            IF (@MailConfirmCancelSendDateFrom IS NULL)
                SET @MailConfirmCancelSendDateFrom = '19000101';
            IF (@MailConfirmCancelSendDateTo IS NULL)
                SET @MailConfirmCancelSendDateTo = '70000101';
            IF (@MailConfirmCancelResponseDateFrom IS NULL)
                SET @MailConfirmCancelResponseDateFrom = '19000101';
            IF (@MailConfirmCancelResponseDateTo IS NULL)
                SET @MailConfirmCancelResponseDateTo = '70000101';
            DECLARE @TransferLetterID1 UNIQUEIDENTIFIER
              , @TransferLetterID2 UNIQUEIDENTIFIER
              , @TransferLetterID3 UNIQUEIDENTIFIER;
            DECLARE @TransferLetterName1 NVARCHAR(500)
              , @TransferLetterName2 NVARCHAR(500)
              , @TransferLetterName3 NVARCHAR(500);
            DECLARE @DateNow DATETIME = CAST(GETDATE() AS DATE);
            SELECT @TransferLetterID1 = ID
              , @TransferLetterName1 = [Name]
            FROM MST.MasterCenter
            WHERE MasterCenterGroupKey = 'TransferLetterType'
                AND IsDeleted = 0
                AND [Key] = '1';
            SELECT @TransferLetterID2 = ID
              , @TransferLetterName2 = [Name]
            FROM MST.MasterCenter
            WHERE MasterCenterGroupKey = 'TransferLetterType'
                AND IsDeleted = 0
                AND [Key] = '2';
            SELECT @TransferLetterID3 = ID
              , @TransferLetterName3 = [Name]
            FROM MST.MasterCenter
            WHERE MasterCenterGroupKey = 'TransferLetterType'
                AND IsDeleted = 0
                AND [Key] = '3';
            IF (OBJECT_ID('tempdb..#tempPostPoneTransfer') IS NOT NULL)
                DROP TABLE #tempPostPoneTransfer;
            SELECT A.ID AgreementID
              , A.TransferOwnershipDate
              , DATEDIFF(DAY, A.SignAgreementDate, A.TransferOwnershipDate) TransferOwnershipDateDateDiff
            INTO #tempPostPoneTransfer
            FROM SAL.Agreement A WITH (NOLOCK)
            LEFT OUTER JOIN PRJ.Project P WITH (NOLOCK) ON P.ID = A.ProjectID
            LEFT OUTER JOIN MST.MasterCenter PT WITH (NOLOCK) ON PT.ID = P.ProductTypeMasterCenterID
            LEFT OUTER JOIN SAL.[Transfer] T WITH (NOLOCK) ON T.AgreementID = A.ID
            WHERE PT.[Key] = '1'
                AND T.ID IS NULL
                AND A.IsDeleted = 0
                AND A.IsCancel = 0
                AND A.SignAgreementDate >= '20200902'
                AND (A.SignAgreementDate > A.TransferOwnershipDate)
                AND (@ProjectID IS NULL OR A.ProjectID = @ProjectID)
                AND (@ProjectIDs IS NULL OR (A.ProjectID IN (SELECT Val FROM dbo.fn_SplitString(@ProjectIDs, ',') )))
            UNION
            SELECT A.ID AgreementID
              , A.TransferOwnershipDate
              , DATEDIFF(DAY, A.SignAgreementDate, A.TransferOwnershipDate) TransferOwnershipDateDateDiff
            FROM SAL.Agreement A WITH (NOLOCK)
            LEFT OUTER JOIN PRJ.Project P WITH (NOLOCK) ON P.ID = A.ProjectID
            LEFT OUTER JOIN MST.MasterCenter PT WITH (NOLOCK) ON PT.ID = P.ProductTypeMasterCenterID
            LEFT OUTER JOIN SAL.[Transfer] T WITH (NOLOCK) ON T.AgreementID = A.ID
            WHERE PT.[Key] = '1'
                AND T.ID IS NULL
                AND A.IsDeleted = 0
                AND A.IsCancel = 0
                AND A.SignAgreementDate >= '20200902'
                AND (DATEDIFF(DAY, A.SignAgreementDate, A.TransferOwnershipDate) >= 0 AND DATEDIFF(DAY, A.SignAgreementDate, A.TransferOwnershipDate) < 7)
                AND (@ProjectID IS NULL OR A.ProjectID = @ProjectID)
                AND (@ProjectIDs IS NULL OR (A.ProjectID IN (SELECT Val FROM dbo.fn_SplitString(@ProjectIDs, ',') )));
            IF (OBJECT_ID('tempdb..#tempLetter') IS NOT NULL)
                DROP TABLE #tempLetter;
            SELECT A.ID AS AgreementID
              , A.AgreementNo
              , A.ContractDate
              , U.UnitNo
              , A.UnitID
              , P.ID AS ProjectID
              , P.ProjectNo
              , P.ProjectNameTH
              , PT.[Key] AS ProjectType
              , PT.[Name] AS ProjectTypeName
              , CASE WHEN TN.[Key] = '-1' THEN ISNULL(AO.TitleExtTH, '')ELSE ISNULL(TN.[Name], '')END + ISNULL(AO.FirstNameTH, '') + N' ' + ISNULL(AO.LastNameTH, '') AS DisplayName
              , [SAL].[fn_GetAgreementVIPAll](A.ID) AS IsVIP
              , AO.FromContactID AS ContactID
              , [SAL].[fn_GetContractNoAgreementAll](A.ID) AS AllContactID
              , W.NewTransferOwnershipDate
              , A.TransferOwnershipDate
              , PP.PostponeTransferDate
              , CAST(NULL AS UNIQUEIDENTIFIER) AS ChangeAgreementOwnerTypeID
              , NULL AS ChangeAgreementOwnerType
              , TL.TransferLetterID AS LastTransferLetterID
              , TL.TransferLetterDate AS LastTransferLetterDate
              , TL.TransferLetterTypeMasterCenterID AS LastTransferLetterTypeID
              , TL.TransferLetterType AS LastTransferLetterType
              , TL.TransferLetterTypeName AS LastTransferLetterTypeName
              , TL.ResponseDate AS LastResponseDate
              , TL.LetterStatusMasterCenterID AS LastLetterStatusID
              , TL.LetterStatusName AS LastLetterStatusName
              , TL.AppointmentTransferDate
              , TL.AppointmentTransferTime
              , TL.PostTrackingNo
              , TL.ContactAddressTypeMasterCenterID
              , DATEDIFF(DAY, TL.TransferLetterDate, @DateNow) AS TransferLetterDateAmount
              , DP.DownPaymentLetterTypeMasterCenterID AS LastDownPaymentLetterTypeID
              , DP.DownPaymentLetterType AS LastDownPaymentLetterType
              , DP.DownPaymentLetterTypeName AS LastDownPaymentLetterTypeName
              , DP.DownPaymentLetterDate AS LastDownPaymentLetterDate
              , (   SELECT COUNT(*)
                    FROM LET.TransferLetter L WITH (NOLOCK)
                    LEFT OUTER JOIN MST.MasterCenter T WITH (NOLOCK) ON T.ID = L.TransferLetterTypeMasterCenterID
                    WHERE L.AgreementID = A.ID
                        AND ISNULL(L.IsDeleted, 0) = 0
                        AND T.[Key] = '1') AS CTM1Amount
              , (   SELECT COUNT(*)
                    FROM LET.TransferLetter L WITH (NOLOCK)
                    LEFT OUTER JOIN MST.MasterCenter T WITH (NOLOCK) ON T.ID = L.TransferLetterTypeMasterCenterID
                    WHERE L.AgreementID = A.ID
                        AND ISNULL(L.IsDeleted, 0) = 0
                        AND T.[Key] = '2') AS CTM2Amount
              , (   SELECT COUNT(*)
                    FROM LET.TransferLetter L WITH (NOLOCK)
                    LEFT OUTER JOIN MST.MasterCenter T WITH (NOLOCK) ON T.ID = L.TransferLetterTypeMasterCenterID
                    WHERE L.AgreementID = A.ID
                        AND ISNULL(L.IsDeleted, 0) = 0
                        AND T.[Key] = '3') AS CTM3Amount
              , R.LastReceiveDate
              , L1.CTM1AppointmentTransferDate
              , L2.CTM2AppointmentTransferDate
              , ISNULL(L1.CTM1DateDiff, 0) AS CTM1DateDiff
              , ISNULL(L2.CTM2DateDiff, 0) AS CTM2DateDiff
              , CASE WHEN CN.ID IS NOT NULL THEN 'ย้ายแปลง' WHEN CC.ID IS NOT NULL THEN 'ยกเลิกสัญญา' ELSE '' END AS AgreementStatusName
              , M.SendDate AS MailConfirmCancelSendDate
              , M.ResponseDate AS MailConfirmCancelResponseDate
              , M.MailConfirmCancelResponseTypeName
            INTO #tempLetter
            FROM SAL.Agreement AS A WITH (NOLOCK)
            LEFT OUTER JOIN MST.MasterCenter AS AST WITH (NOLOCK) ON AST.ID = A.AgreementStatusMasterCenterID
            INNER JOIN SAL.AgreementOwner AS AO WITH (NOLOCK) ON AO.AgreementID = A.ID AND ISNULL(AO.IsMainOwner, 0) = 1 AND ISNULL(AO.IsDeleted, 0) = 0
            LEFT OUTER JOIN MST.MasterCenter TN WITH (NOLOCK) ON TN.ID = AO.ContactTitleTHMasterCenterID
            LEFT OUTER JOIN PRJ.Project AS P WITH (NOLOCK) ON A.ProjectID = P.ID
            LEFT OUTER JOIN MST.MasterCenter PT WITH (NOLOCK) ON PT.ID = P.ProductTypeMasterCenterID
            LEFT OUTER JOIN PRJ.Unit AS U WITH (NOLOCK) ON A.UnitID = U.ID
            LEFT OUTER JOIN (   SELECT D1.AgreementID
                                  , D1.TransferLetterTypeMasterCenterID
                                  , D1.TransferLetterDate
                                  , D1.ResponseDate
                                  , D1.LetterStatusMasterCenterID
                                  , D1.AppointmentTransferDate
                                  , D1.AppointmentTransferTime
                                  , M.[Key] AS TransferLetterType
                                  , M.Name AS TransferLetterTypeName
                                  , M2.Name AS LetterStatusName
                                  , P.PostTrackingNo
                                  , D1.ContactAddressTypeMasterCenterID
                                  , D1.ID AS TransferLetterID
                                FROM LET.TransferLetter AS D1 WITH (NOLOCK)
                                INNER JOIN (   SELECT AgreementID
                                                 , MAX(ISNULL(Created, TransferLetterDate)) AS Created
                                               FROM LET.TransferLetter WITH (NOLOCK)
                                               WHERE ISNULL(IsDeleted, 0) = 0
                                               GROUP BY AgreementID) D2 ON D2.AgreementID = D1.AgreementID
                                                                            AND D2.Created = ISNULL(D1.Created, D1.TransferLetterDate)
                                LEFT OUTER JOIN MST.MasterCenter M WITH (NOLOCK) ON M.ID = D1.TransferLetterTypeMasterCenterID
                                LEFT OUTER JOIN MST.MasterCenter M2 WITH (NOLOCK) ON M2.ID = D1.LetterStatusMasterCenterID
                                LEFT OUTER JOIN MST.PostTracking P WITH (NOLOCK) ON P.ID = D1.PostTrackingID
                                WHERE ISNULL(D1.IsDeleted, 0) = 0) TL ON A.ID = TL.AgreementID
            OUTER APPLY (   SELECT DATEDIFF(DAY, L1.TransferLetterDate, L1.AppointmentTransferDate) + 1 AS CTM1DateDiff
                              , L1.AppointmentTransferDate AS CTM1AppointmentTransferDate
                            FROM LET.TransferLetter L1 WITH (NOLOCK)
                            INNER JOIN (   SELECT AgreementID
                                             , MAX(ISNULL(L1.Created, L1.TransferLetterDate)) AS Created
                                           FROM LET.TransferLetter L1 WITH (NOLOCK)
                                           LEFT OUTER JOIN MST.MasterCenter T WITH (NOLOCK) ON T.ID = L1.TransferLetterTypeMasterCenterID
                                           WHERE ISNULL(L1.IsDeleted, 0) = 0
                                               AND T.[Key] = '1'
                                           GROUP BY AgreementID) L2 ON L2.AgreementID = L1.AgreementID AND L2.Created = ISNULL(L1.Created, L1.TransferLetterDate)
                            LEFT OUTER JOIN MST.MasterCenter T WITH (NOLOCK) ON T.ID = L1.TransferLetterTypeMasterCenterID
                            WHERE L1.AgreementID = A.ID
                                AND ISNULL(L1.IsDeleted, 0) = 0
                                AND T.[Key] = '1') L1
            OUTER APPLY (   SELECT SUM(DATEDIFF(DAY, L1.TransferLetterDate, L1.AppointmentTransferDate) + 1) AS CTM2DateDiff
                              , MAX(L1.AppointmentTransferDate) AS CTM2AppointmentTransferDate
                            FROM LET.TransferLetter L1 WITH (NOLOCK)
                            LEFT OUTER JOIN MST.MasterCenter T WITH (NOLOCK) ON T.ID = L1.TransferLetterTypeMasterCenterID
                            WHERE L1.AgreementID = A.ID
                                AND ISNULL(L1.IsDeleted, 0) = 0
                                AND T.[Key] = '2') L2
            LEFT OUTER JOIN (   SELECT W1.AgreementID
                                  , W1.ChangeAgreementOwnerTypeMasterCenterID
                                  , M.[Name] AS ChangeAgreementOwnerType
                                  , W1.NewTransferOwnershipDate
                                FROM SAL.ChangeAgreementOwnerWorkflow W1 WITH (NOLOCK)
                                INNER JOIN (   SELECT AgreementID
                                                 , MAX(W1.ApprovedDate) AS ApprovedDate
                                               FROM SAL.ChangeAgreementOwnerWorkflow W1 WITH (NOLOCK)
                                               LEFT OUTER JOIN MST.MasterCenter M WITH (NOLOCK) ON M.ID = W1.ChangeAgreementOwnerTypeMasterCenterID
                                               WHERE ISNULL(W1.IsApproved, 0) = 1
                                                   AND ISNULL(W1.IsDeleted, 0) = 0
                                                   AND M.[Key] IN ('3', '2')
                                               GROUP BY AgreementID) W2 ON W2.AgreementID = W1.AgreementID AND W2.ApprovedDate = W1.ApprovedDate
                                LEFT OUTER JOIN MST.MasterCenter M WITH (NOLOCK) ON M.ID = W1.ChangeAgreementOwnerTypeMasterCenterID
                                WHERE ISNULL(W1.IsApproved, 0) = 1
                                    AND ISNULL(W1.IsDeleted, 0) = 0
                                    AND M.[Key] IN ('3', '2')) AS W ON W.AgreementID = A.ID
            LEFT OUTER JOIN SAL.[Transfer] AS T WITH (NOLOCK) ON A.ID = T.AgreementID AND T.IsDeleted = 0
            LEFT OUTER JOIN (   SELECT D1.AgreementID
                                  , D1.DownPaymentLetterTypeMasterCenterID
                                  , M.[Key] AS DownPaymentLetterType
                                  , M.[Name] AS DownPaymentLetterTypeName
                                  , D1.DownPaymentLetterDate
                                FROM LET.DownPaymentLetter AS D1 WITH (NOLOCK)
                                INNER JOIN (   SELECT D.AgreementID
                                                 , MAX(D.Created) AS Created
                                               FROM LET.DownPaymentLetter D WITH (NOLOCK)
                                               LEFT OUTER JOIN MST.MasterCenter m WITH (NOLOCK) ON m.ID = D.DownPaymentLetterTypeMasterCenterID
                                               WHERE ISNULL(D.IsDeleted, 0) = 0
                                                   AND m.[Key] <> '0'
                                               GROUP BY D.AgreementID) D2 ON D2.AgreementID = D1.AgreementID AND D2.Created = D1.Created
                                LEFT OUTER JOIN MST.MasterCenter M WITH (NOLOCK) ON M.ID = D1.DownPaymentLetterTypeMasterCenterID
                                LEFT OUTER JOIN MST.MasterCenter M2 WITH (NOLOCK) ON M2.ID = D1.LetterStatusMasterCenterID
                                LEFT OUTER JOIN MST.PostTracking P WITH (NOLOCK) ON P.ID = D1.PostTrackingID
                                WHERE ISNULL(D1.IsDeleted, 0) = 0
                                    AND M.[Key] <> '0') DP ON A.ID = DP.AgreementID
            LEFT OUTER JOIN SAL.PostponeTransfer AS PP WITH (NOLOCK) ON A.ID = PP.AgreementID AND PP.IsDeleted = 0 AND PP.LCMApproveDate IS NOT NULL
            LEFT OUTER JOIN (   SELECT a.BookingID
                                  , MAX(a.ReceiveDate) LastReceiveDate
                                FROM FIN.Payment a WITH (NOLOCK)
                                LEFT OUTER JOIN FIN.PaymentItem b WITH (NOLOCK) ON b.PaymentID = a.ID
                                LEFT OUTER JOIN MST.MasterPriceItem m WITH (NOLOCK) ON m.ID = b.MasterPriceItemID
                                LEFT OUTER JOIN FIN.PaymentMethod p WITH (NOLOCK) ON p.PaymentID = a.ID
                                LEFT OUTER JOIN MST.MasterCenter mt WITH (NOLOCK) ON mt.ID = p.PaymentMethodTypeMasterCenterID
                                WHERE ISNULL(a.IsDeleted, 0) = 0
                                    AND ISNULL(a.IsCancel, 0) = 0
                                    AND m.[Key] = 'InstallmentAmount'
                                    AND (   (   mt.[Key] IN ('Cash', 'CashierCheque', 'CreditCard', 'PersonalCheque', 'PostDateCheque', 'BankTransfer', 'DebitCard', 'FBankTransfer', 'QR')
                                                AND ISNULL(p.DepositNo, '') <> '')
                                            OR (mt.[Key] IN ('BillPayment', 'DirectDebit', 'DirectCredit', 'OldContract', 'Unknown')))
                                GROUP BY a.BookingID) AS R ON R.BookingID = A.BookingID
            LEFT OUTER JOIN SAL.ChangeUnitWorkflow CN WITH (NOLOCK) ON CN.FromAgreementID = A.ID AND CN.ApprovedDate IS NULL AND CN.IsDeleted = 0
            LEFT OUTER JOIN SAL.CancelMemo CC WITH (NOLOCK) ON CC.AgreementID = A.ID AND CC.ApproveDate IS NULL AND CC.IsDeleted = 0
            LEFT OUTER JOIN CTM.Contact CT WITH (NOLOCK) ON CT.ID = AO.FromContactID
            LEFT OUTER JOIN MST.MasterCenter CST WITH (NOLOCK) ON CST.ID = CT.CustomerTypeMasterCenterID
            LEFT OUTER JOIN (   SELECT M1.TransferLetterID
                                  , M1.MailConfirmCancelResponseTypeID
                                  , M.[Name] AS MailConfirmCancelResponseTypeName
                                  , M1.SendDate
                                  , M1.ResponseDate
                                FROM LET.MailConfirmCancel M1 WITH (NOLOCK)
                                INNER JOIN (   SELECT TransferLetterID
                                                 , MAX(Created) AS Created
                                               FROM LET.MailConfirmCancel WITH (NOLOCK)
                                               WHERE ISNULL(IsDeleted, 0) = 0
                                                   AND TransferLetterID IS NOT NULL
                                               GROUP BY TransferLetterID) M2 ON M1.TransferLetterID = M2.TransferLetterID AND M1.Created = M2.Created
                                LEFT OUTER JOIN MST.MasterCenter M WITH (NOLOCK) ON M.ID = M1.MailConfirmCancelResponseTypeID
                                WHERE ISNULL(M1.IsDeleted, 0) = 0
                                    AND M1.TransferLetterID IS NOT NULL) M ON M.TransferLetterID = TL.TransferLetterID
            WHERE 1 = 1
                AND T.ID IS NULL
                AND ISNULL(A.IsCancel, 0) = 0
                AND ISNULL(A.IsDeleted, 0) = 0
                AND A.SignAgreementDate IS NOT NULL
                AND (@ProjectID IS NULL OR A.ProjectID = @ProjectID)
                AND (@ProjectIDs IS NULL OR (A.ProjectID IN (SELECT Val FROM dbo.fn_SplitString(@ProjectIDs, ',') )))
                AND (@UnitNo = '' OR @UnitNo IS NULL OR U.UnitNo LIKE '%' + @UnitNo + '%')
                AND ((YEAR(@MailConfirmCancelSendDateFrom) = 1900 AND YEAR(@MailConfirmCancelSendDateTo) = 7000)
                        OR (M.SendDate BETWEEN ISNULL(@MailConfirmCancelSendDateFrom, '19000101') AND ISNULL(@MailConfirmCancelSendDateTo, '70000101')))
                AND ((YEAR(@MailConfirmCancelResponseDateFrom) = 1900 AND YEAR(@MailConfirmCancelResponseDateTo) = 7000)
                        OR (M.ResponseDate BETWEEN ISNULL(@MailConfirmCancelResponseDateFrom, '19000101') AND ISNULL(@MailConfirmCancelResponseDateTo, '70000101')))
                AND (@MailConfirmCancelResponseTypeID IS NULL OR M.MailConfirmCancelResponseTypeID = @MailConfirmCancelResponseTypeID);
            IF (OBJECT_ID('tempdb..#temp') IS NOT NULL)
                DROP TABLE #temp;
            SELECT DISTINCT CAST(0 AS BIT) AS IsSelected
              , A.AgreementID
              , A.ProjectID
              , A.ChangeAgreementOwnerType
              , A.ProjectType
              , A.ProjectNo
              , A.ProjectNameTH
              , A.UnitNo
              , A.UnitID
              , A.DisplayName
              , A.TransferOwnershipDate
              , CASE
                    WHEN A.ProjectType = '1' THEN CASE
                                                      WHEN A.CTM3Amount > 0 THEN ''
                                                      WHEN A.CTM1Amount = 0 THEN 'CTM1'
                                                      WHEN @DateNow > A.CTM1AppointmentTransferDate
                                                          AND @DateNow > ISNULL(A.NewTransferOwnershipDate, ISNULL(A.AppointmentTransferDate, A.TransferOwnershipDate)) THEN 'CTM3'
                                                  ELSE ''
                                                  END
                ELSE CASE
                         WHEN A.CTM3Amount > 0 THEN ''
                         WHEN A.CTM1Amount = 0 THEN 'CTM1'
                         WHEN ISNULL(A.CTM1DateDiff, 0) >= 30
                             AND @DateNow > A.CTM1AppointmentTransferDate
                             AND @DateNow > ISNULL(A.NewTransferOwnershipDate, A.TransferOwnershipDate)
                             AND @DateNow > A.TransferOwnershipDate THEN 'CTM3'
                         WHEN ISNULL(A.CTM1DateDiff, 0) < 30
                             AND ISNULL(A.CTM2Amount, 0) = 0
                             AND @DateNow > A.CTM1AppointmentTransferDate THEN 'CTM2'
                         WHEN ISNULL(A.CTM2Amount, 0) > 0
                             AND ISNULL(A.CTM1DateDiff, 0) + ISNULL(A.CTM2DateDiff, 0) < 30
                             AND @DateNow > A.CTM2AppointmentTransferDate THEN 'CTM2D'
                         WHEN ISNULL(A.CTM2Amount, 0) > 0
                             AND ISNULL(A.CTM1DateDiff, 0) + ISNULL(A.CTM2DateDiff, 0) >= 30 THEN CASE
                                                                                                      WHEN @DateNow > ISNULL(A.CTM2AppointmentTransferDate, A.CTM1AppointmentTransferDate)
                                                                                                          AND @DateNow > ISNULL(A.NewTransferOwnershipDate, A.TransferOwnershipDate)
                                                                                                          AND @DateNow > A.TransferOwnershipDate THEN 'CTM3'
                                                                                                  ELSE ''
                                                                                                  END
                     ELSE ''
                     END
                END AS NowTransferLetterType
              , CAST(NULL AS UNIQUEIDENTIFIER) AS NowTransferLetterID
              , CAST('' AS NVARCHAR(500)) AS NowTransferLetterName
              , A.LastTransferLetterID
              , A.LastTransferLetterType
              , A.LastTransferLetterTypeID
              , A.LastTransferLetterTypeName
              , A.LastResponseDate
              , A.LastLetterStatusName
              , A.LastTransferLetterDate
              , A.AppointmentTransferDate
              , A.AppointmentTransferTime
              , A.NewTransferOwnershipDate
              , A.PostponeTransferDate
              , A.LastDownPaymentLetterTypeID
              , A.LastDownPaymentLetterType
              , A.LastDownPaymentLetterTypeName
              , A.LastDownPaymentLetterDate
              , A.PostTrackingNo
              , CA.PostalCode
              , C.NameTH AS CountryNameTH
              , A.ContactID
              , A.AllContactID
              , 'Page' = @Page
              , 'PageSize' = @PageSize
              , 'PageCount' = CONVERT(INT, 0)
              , 'RecordCount' = CONVERT(INT, 0)
              , CAST(0 AS BIT) AS CanCreateLetter
              , CAST(0 AS BIT) AS CanCreateMailConfirmCancel
              , CAST(CASE
                         WHEN A.ProjectType = '1' THEN CASE WHEN A.CTM1Amount > 0 THEN 1 ELSE 0 END
                     ELSE CASE WHEN ISNULL(A.CTM1DateDiff, 0) + ISNULL(A.CTM2DateDiff, 0) >= 30 THEN 1 ELSE 0 END
                     END AS BIT) AS WaitForCancel
              , A.LastReceiveDate
              , A.CTM1AppointmentTransferDate
              , A.CTM2AppointmentTransferDate
              , A.CTM1DateDiff
              , A.CTM2DateDiff
              , A.CTM1Amount
              , A.CTM2Amount
              , A.CTM3Amount
              , A.AgreementStatusName
              , A.MailConfirmCancelSendDate
              , A.MailConfirmCancelResponseDate
              , A.MailConfirmCancelResponseTypeName
			  , ISNULL(P.CountDueAmount,0) AS CountDueAmount
            INTO #temp
            FROM #tempLetter A WITH (NOLOCK)
            LEFT OUTER JOIN CTM.ContactAddress CA WITH (NOLOCK) ON A.ContactID = CA.ContactID
                                                                    AND A.ContactAddressTypeMasterCenterID = CA.ContactAddressTypeMasterCenterID
                                                                    AND CA.IsDeleted = 0
            LEFT OUTER JOIN MST.MasterCenter ADT WITH (NOLOCK) ON ADT.ID = CA.ContactAddressTypeMasterCenterID
            LEFT OUTER JOIN MST.Country C WITH (NOLOCK) ON C.ID = CA.CountryID
			LEFT OUTER JOIN LET.PrepareDownPaymentLetter P WITH (NOLOCK) ON P.AgreementID = A.AgreementID
            WHERE 1 = 1
                AND (   (   ADT.[Key] = '0'
                            AND CA.ID = (   SELECT CAP.ContactAddressID
                                            FROM CTM.ContactAddressProject CAP WITH (NOLOCK)
                                            WHERE CAP.ContactAddressID = CA.ID
                                                AND CAP.IsDeleted = 0
                                                AND CAP.ProjectID = A.ProjectID))
                        OR ADT.[Key] = '1'
                        OR ADT.[Key] IS NULL)
                AND (@ProjectID IS NULL OR A.ProjectID = @ProjectID)
                AND (@ProjectIDs IS NULL OR (A.ProjectID IN (SELECT Val FROM dbo.fn_SplitString(@ProjectIDs, ',') )))
                AND ((ISNULL(@TransferLetterStutusID, 0) = 0 AND ISNULL(A.IsVIP, 0) = 0)
                        OR (ISNULL(@TransferLetterStutusID, 0) = 1 AND A.AgreementID IS NOT NULL AND ISNULL(A.IsVIP, 0) = 0)
                        OR (ISNULL(@TransferLetterStutusID, 0) = 2 AND ISNULL(A.IsVIP, 0) = 1 /*vip*/))
                AND (@ChangeAgreementOwnerTypeID IS NULL OR A.ChangeAgreementOwnerTypeID = @ChangeAgreementOwnerTypeID)
                AND (@UnitNo = '' OR @UnitNo IS NULL OR A.UnitNo LIKE '%' + @UnitNo + '%')
                AND (@DisplayName = '' OR @DisplayName IS NULL OR A.DisplayName LIKE '%' + @DisplayName + '%')
                AND ((YEAR(@TransferOwnershipDateFrom) = 1900 AND YEAR(@TransferOwnershipDateTo) = 7000)
                        OR (A.TransferOwnershipDate BETWEEN ISNULL(@TransferOwnershipDateFrom, '19000101') AND ISNULL(@TransferOwnershipDateTo, '70000101')))
                AND ((YEAR(@ResponseDateFrom) = 1900 AND YEAR(@ResponseDateTo) = 7000)
                        OR (A.LastResponseDate BETWEEN ISNULL(@ResponseDateFrom, '19000101') AND ISNULL(@ResponseDateTo, '70000101')))
                AND (@LetterStatus IS NULL OR A.LastLetterStatusID = @LetterStatus)
                AND (@LastTransferLetterTypeID IS NULL OR A.LastTransferLetterTypeID = @LastTransferLetterTypeID)
                AND (@LastDownPaymentLetterTypeID IS NULL OR A.LastDownPaymentLetterTypeID = @LastDownPaymentLetterTypeID)
                AND ((YEAR(@AppointmentTransferDateFrom) = 1900 AND YEAR(@AppointmentTransferDateTo) = 7000)
                        OR (A.AppointmentTransferDate BETWEEN ISNULL(@AppointmentTransferDateFrom, '19000101') AND ISNULL(@AppointmentTransferDateTo, '70000101')))
                AND ((YEAR(@NewTransferOwnershipDateFrom) = 1900 AND YEAR(@NewTransferOwnershipDateTo) = 7000)
                        OR (A.NewTransferOwnershipDate BETWEEN ISNULL(@NewTransferOwnershipDateFrom, '19000101') AND ISNULL(@NewTransferOwnershipDateTo, '70000101')))
                AND ((YEAR(@PostponeTransferDateFrom) = 1900 AND YEAR(@PostponeTransferDateTo) = 7000)
                        OR (A.PostponeTransferDate BETWEEN ISNULL(@PostponeTransferDateFrom, '19000101') AND ISNULL(@PostponeTransferDateTo, '70000101')))
                AND ((YEAR(@LastTransferLetterDateFrom) = 1900 AND YEAR(@LastTransferLetterDateTo) = 7000)
                        OR (A.LastTransferLetterDate BETWEEN ISNULL(@LastTransferLetterDateFrom, '19000101') AND ISNULL(@LastTransferLetterDateTo, '70000101')));
            UPDATE #temp
            SET NowTransferLetterID = CASE
                                          WHEN EXISTS (SELECT * FROM #tempPostPoneTransfer WHERE AgreementID = #temp.AgreementID)
                                              OR PostponeTransferDate IS NOT NULL THEN NULL
                                          WHEN NowTransferLetterType = 'CTM1' THEN @TransferLetterID1
                                          WHEN NowTransferLetterType = 'CTM2'
                                              OR NowTransferLetterType = 'CTM2D' THEN @TransferLetterID2
                                          WHEN NowTransferLetterType = 'CTM3' THEN @TransferLetterID3
                                      ELSE NULL
                                      END
              , NowTransferLetterName = CASE
                                            WHEN EXISTS (SELECT * FROM #tempPostPoneTransfer WHERE AgreementID = #temp.AgreementID)
                                                OR PostponeTransferDate IS NOT NULL THEN ''
                                            WHEN NowTransferLetterType = 'CTM1' THEN @TransferLetterName1
                                            WHEN NowTransferLetterType = 'CTM2'
                                                OR NowTransferLetterType = 'CTM2D' THEN @TransferLetterName2
                                            WHEN NowTransferLetterType = 'CTM3' THEN @TransferLetterName3
                                        ELSE ''
                                        END
              , CanCreateLetter = CAST(CASE
                                           WHEN AgreementStatusName <> '' THEN 0
                                           WHEN NowTransferLetterType = '' THEN 0
                                           WHEN LastDownPaymentLetterType = '3' THEN CASE
                                                                                         WHEN (ProjectNo = '60010' AND UnitNo = 'B35B209')
                                                                                             OR (ProjectNo = '60010' AND UnitNo = 'A39C111')
                                                                                             OR (ProjectNo = '60010' AND UnitNo = 'B42E224')
                                                                                             OR (ProjectNo = '60010' AND UnitNo = 'B44B103') THEN 1
                                                                                         WHEN LastReceiveDate >= LastDownPaymentLetterDate THEN 1
                                                                                     ELSE 0
                                                                                     END
                                       ELSE CASE
                                                WHEN ProjectType = '1' THEN CASE WHEN PostponeTransferDate IS NOT NULL THEN 0 WHEN EXISTS (SELECT * FROM #tempPostPoneTransfer WHERE AgreementID = #temp.AgreementID) THEN 0 ELSE 1 END
                                            ELSE 1
                                            END
                                       END AS BIT)
              , WaitForCancel = CAST(CASE WHEN NowTransferLetterType = 'CTM3' OR LastTransferLetterType = '3' THEN 0 ELSE WaitForCancel END AS BIT)
              , CanCreateMailConfirmCancel = CAST(CASE WHEN ProjectType = '2' THEN 0 WHEN NowTransferLetterType = 'CTM3' THEN 1 ELSE 0 END AS BIT);
            UPDATE #temp SET [PageCount] = ((SELECT COUNT(*)FROM #temp) / @PageSize) + 1;
            UPDATE #temp SET [RecordCount] = (SELECT COUNT(*)FROM #temp);
            IF (OBJECT_ID('tempdb..#display') IS NOT NULL)
                DROP TABLE #display;
            SELECT *
            INTO #display
            FROM #temp WITH (NOLOCK)
            WHERE 1 = 1
                AND (@TransferLetterType IS NULL OR NowTransferLetterID = @TransferLetterType)
                AND (ISNULL(@WaitForCancelStatusID, 0) = 0 OR WaitForCancel = @WaitForCancelStatusID);
            UPDATE #display SET [PageCount] = ((SELECT COUNT(*)FROM #display) / @PageSize) + 1;
            UPDATE #display SET [RecordCount] = (SELECT COUNT(*)FROM #display);
            SELECT *
            FROM #display WITH (NOLOCK)
            WHERE 1 = 1
            ORDER BY ProjectNo
              , CASE WHEN ISNULL(@Sys_SortBy, '') IN ('', 'UnitNo') AND ISNULL(@Sys_SortType, '') IN ('', 'desc') THEN UnitNo ELSE '' END DESC
              , CASE WHEN @Sys_SortBy = 'UnitNo' AND @Sys_SortType <> 'desc' THEN UnitNo ELSE '' END ASC
              , CASE WHEN @Sys_SortBy = 'FullName' AND @Sys_SortType <> 'desc' THEN DisplayName ELSE '' END ASC
              , CASE WHEN @Sys_SortBy = 'FullName' AND @Sys_SortType = 'desc' THEN DisplayName ELSE '' END DESC
              , CASE WHEN @Sys_SortBy = 'TransferOwnershipDate' AND @Sys_SortType <> 'desc' THEN TransferOwnershipDate ELSE '' END ASC
              , CASE WHEN @Sys_SortBy = 'TransferOwnershipDate' AND @Sys_SortType = 'desc' THEN TransferOwnershipDate ELSE '' END DESC
              , CASE WHEN @Sys_SortBy = 'TransferLetterType' AND @Sys_SortType <> 'desc' THEN NowTransferLetterType ELSE '' END ASC
              , CASE WHEN @Sys_SortBy = 'TransferLetterType' AND @Sys_SortType = 'desc' THEN NowTransferLetterType ELSE '' END DESC
              , CASE WHEN @Sys_SortBy = 'AppointmentTransferDate' AND @Sys_SortType <> 'desc' THEN AppointmentTransferDate ELSE '' END ASC
              , CASE WHEN @Sys_SortBy = 'AppointmentTransferDate' AND @Sys_SortType = 'desc' THEN AppointmentTransferDate ELSE '' END DESC
              , CASE WHEN @Sys_SortBy = 'NewTransferOwnershipDate' AND @Sys_SortType <> 'desc' THEN NewTransferOwnershipDate ELSE '' END ASC
              , CASE WHEN @Sys_SortBy = 'NewTransferOwnershipDate' AND @Sys_SortType = 'desc' THEN NewTransferOwnershipDate ELSE '' END DESC
              , CASE WHEN @Sys_SortBy = 'PostponeTransferDate' AND @Sys_SortType <> 'desc' THEN PostponeTransferDate ELSE '' END ASC
              , CASE WHEN @Sys_SortBy = 'PostponeTransferDate' AND @Sys_SortType = 'desc' THEN PostponeTransferDate ELSE '' END DESC OFFSET (@Page - 1) * @PageSize ROWS FETCH FIRST @PageSize ROWS ONLY;
        END;";

        public static void QueryFilter(ref string QueryString, List<dbqParam> ParamList)
        {
            if (ParamList != null)
            {
                QueryString = QueryString.Replace("[Param]", string.Join(", ", ParamList.Select(o => string.Format("{0} = {1}", o.Key, o.sqlparam.ParameterName))));
            }
        }

        public class QueryResult
        {
            public bool? IsSelected { get; set; }
            public Guid? AgreementID { get; set; }
            public Guid? ProjectID { get; set; }
            public Guid? UnitID { get; set; }
            public string ChangeAgreementOwnerType { get; set; }
            public string ProjectType { get; set; }
            public string ProjectNo { get; set; }
            public string ProjectNameTH { get; set; }
            public string UnitNo { get; set; }
            public string DisplayName { get; set; }
            public string NowTransferLetterType { get; set; }
            public Guid? NowTransferLetterID { get; set; }
            public string NowTransferLetterName { get; set; }
            public string LastTransferLetterType { get; set; }
            public Guid? LastTransferLetterTypeID { get; set; }
            public string LastTransferLetterTypeName { get; set; }
            public DateTime? LastResponseDate { get; set; }
            public string LastLetterStatusName { get; set; }
            public DateTime? TransferOwnershipDate { get; set; }
            public DateTime? AppointmentTransferDate { get; set; }
            public DateTime? AppointmentTransferTime { get; set; }
            public string LastDownPaymentLetterType { get; set; }
            public Guid? LastDownPaymentLetterTypeID { get; set; }
            public string LastDownPaymentLetterTypeName { get; set; }
            public string PostalCode { get; set; }
            public string CountryNameTH { get; set; }
            public string PostTrackingNo { get; set; }
            public DateTime? NewTransferOwnershipDate { get; set; }
            public DateTime? PostponeTransferDate { get; set; }
            public Guid ContactID { get; set; }
            public Guid? LastTransferLetterID { get; set; }
            public bool? CanCreateLetter { get; set; }
            public DateTime? LastTransferLetterDate { get; set; }
            public bool? WaitForCancel { get; set; }
            public string AllContactID { get; set; }
            public string AgreementStatusName { get; set; }
            public DateTime? MailConfirmCancelSendDate { get; set; }
            public DateTime? MailConfirmCancelResponseDate { get; set; }
            public string MailConfirmCancelResponseTypeName { get; set; }
            public bool? CanCreateMailConfirmCancel { get; set; }
            public int? CountDueAmount { get; set; }
        }
    }
}


