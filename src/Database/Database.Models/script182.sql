CREATE TABLE [PRJ].[LetterGuarantee] (
    [ID] uniqueidentifier NOT NULL,
    [IssueDate] datetime2 NULL,
    [ExpiredDate] nvarchar(max) NULL,
    [MeterNumber] nvarchar(max) NULL,
    [IsJuristicSetup] bit NULL,
    [JuristicSetupDate] datetime2 NULL,
    [JuristicSetupBy] nvarchar(max) NULL,
    [JuristicSetupRemarks] nvarchar(max) NULL,
    [BankID] uniqueidentifier NULL,
    [Bank] nvarchar(max) NULL,
    [CompanyID] uniqueidentifier NULL,
    [CompanyCode] nvarchar(max) NULL,
    [CostCenter] nvarchar(max) NULL,
    [ProjectID] uniqueidentifier NULL,
    [ProjectArea] float NULL,
    [LetterOfGuaranteeNo] nvarchar(max) NULL,
    [LGGuarantorMasterCenterID] uniqueidentifier NULL,
    [LGTypeMasterCenterID] uniqueidentifier NULL,
    [IssueAmount] decimal(18,2) NULL,
    [RefundAmount] decimal(18,2) NULL,
    [RemainAmount] decimal(18,2) NULL,
    [LGGuaranteeConditionsMasterCenterID] uniqueidentifier NULL,
    [Remark] nvarchar(max) NULL,
    [IsCanceled] bit NULL,
    [CancelDate] datetime2 NULL,
    [CancelByUserID] uniqueidentifier NULL,
    [CancelRemark] nvarchar(max) NULL,
    [EffectiveDate] datetime2 NULL,
	[ExpiredPeriodDate] datetime2 NULL,
    [ConditionCalFee] int NULL,
    [FeeRate] float NULL,
    [FeeRateAmountByPeriod] decimal(18,2) NULL,
	[IsDeleted] bit NOT NULL,
	[Created] datetime2 NULL,
    [Updated] datetime2 NULL,
    [CreatedByUserID] uniqueidentifier NULL,
    [UpdatedByUserID] uniqueidentifier NULL,

    CONSTRAINT [PK_LetterGuarantee] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_LetterGuarantee_Bank_BankID] FOREIGN KEY ([BankID]) REFERENCES [MST].[Bank] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_User_CancelByUserID] FOREIGN KEY ([CancelByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_Company_CompanyID] FOREIGN KEY ([CompanyID]) REFERENCES [MST].[Company] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_MasterCenter_LGGuaranteeConditionsMasterCenterID] FOREIGN KEY ([LGGuaranteeConditionsMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_MasterCenter_LGGuarantorMasterCenterID] FOREIGN KEY ([LGGuarantorMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_MasterCenter_LGTypeMasterCenterID] FOREIGN KEY ([LGTypeMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_Project_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [PRJ].[Project] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuarantee_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_LetterGuarantee_BankID] ON [PRJ].[LetterGuarantee] ([BankID]);

GO

CREATE INDEX [IX_LetterGuarantee_CancelByUserID] ON [PRJ].[LetterGuarantee] ([CancelByUserID]);

GO

CREATE INDEX [IX_LetterGuarantee_CompanyID] ON [PRJ].[LetterGuarantee] ([CompanyID]);

GO

CREATE INDEX [IX_LetterGuarantee_CreatedByUserID] ON [PRJ].[LetterGuarantee] ([CreatedByUserID]);

GO

CREATE INDEX [IX_LetterGuarantee_LGGuaranteeConditionsMasterCenterID] ON [PRJ].[LetterGuarantee] ([LGGuaranteeConditionsMasterCenterID]);

GO

CREATE INDEX [IX_LetterGuarantee_LGGuarantorMasterCenterID] ON [PRJ].[LetterGuarantee] ([LGGuarantorMasterCenterID]);

GO

CREATE INDEX [IX_LetterGuarantee_LGTypeMasterCenterID] ON [PRJ].[LetterGuarantee] ([LGTypeMasterCenterID]);

GO

CREATE INDEX [IX_LetterGuarantee_ProjectID] ON [PRJ].[LetterGuarantee] ([ProjectID]);

GO

CREATE INDEX [IX_LetterGuarantee_UpdatedByUserID] ON [PRJ].[LetterGuarantee] ([UpdatedByUserID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'25660222100407_Table+LetterGuarantee_model_by_teerapat', N'2.2.2-servicing-10034');

GO

