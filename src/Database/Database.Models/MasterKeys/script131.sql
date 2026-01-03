--ALTER TABLE [EQN].[CustomerTransQAns] ADD [OpportunityID] uniqueidentifier NULL;

--GO

--ALTER TABLE [EQN].[CustomerTransQAns] ADD [revisit_flag] nvarchar(max) NULL;

--GO

--ALTER TABLE [EQN].[CustomerTransQAns] ADD [total_required_answer] int NULL;

--GO

--ALTER TABLE [EQN].[CustomerTransQAns] ADD [tran_type] nvarchar(max) NULL;

--GO

ALTER TABLE [CTM].[Opportunity] ADD [ConsentTypeMasterCenterID] uniqueidentifier NULL;

GO

ALTER TABLE [CTM].[Opportunity] ADD [IsSendToBC] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [CTM].[Lead] ADD [ConsentTypeMasterCenterID] uniqueidentifier NULL;

GO

ALTER TABLE [CTM].[Lead] ADD [IsSendToBC] bit NOT NULL DEFAULT 0;

GO

ALTER TABLE [CTM].[Contact] ADD [IsSendToBC] bit NOT NULL DEFAULT 0;

GO

CREATE TABLE [CTM].[CustomerConsentHistory] (
    [ID] uniqueidentifier NOT NULL,
    [Created] datetime2 NULL,
    [ReferentTypeID] uniqueidentifier NULL,
    [ConsentReferentTypeMasterCenterID] uniqueidentifier NULL,
    [ReferentID] uniqueidentifier NULL,
    [OleConsentTypeMasterCenterID] uniqueidentifier NULL,
    [OldConsentTypeMasterCenterID] uniqueidentifier NULL,
    [NewConsentTypeMasterCenterID] uniqueidentifier NULL,
    [EmpoyeeNo] nvarchar(100) NULL,
    [EmpoyeeName] nvarchar(100) NULL,
    [CreateBySystem] nvarchar(100) NULL,
    CONSTRAINT [PK_CustomerConsentHistory] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_CustomerConsentHistory_MasterCenter_ConsentReferentTypeMasterCenterID] FOREIGN KEY ([ConsentReferentTypeMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerConsentHistory_MasterCenter_NewConsentTypeMasterCenterID] FOREIGN KEY ([NewConsentTypeMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_CustomerConsentHistory_MasterCenter_OldConsentTypeMasterCenterID] FOREIGN KEY ([OldConsentTypeMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Opportunity_ConsentTypeMasterCenterID] ON [CTM].[Opportunity] ([ConsentTypeMasterCenterID]);

GO

CREATE INDEX [IX_Lead_ConsentTypeMasterCenterID] ON [CTM].[Lead] ([ConsentTypeMasterCenterID]);

GO

CREATE INDEX [IX_CustomerConsentHistory_ConsentReferentTypeMasterCenterID] ON [CTM].[CustomerConsentHistory] ([ConsentReferentTypeMasterCenterID]);

GO

CREATE INDEX [IX_CustomerConsentHistory_NewConsentTypeMasterCenterID] ON [CTM].[CustomerConsentHistory] ([NewConsentTypeMasterCenterID]);

GO

CREATE INDEX [IX_CustomerConsentHistory_OldConsentTypeMasterCenterID] ON [CTM].[CustomerConsentHistory] ([OldConsentTypeMasterCenterID]);

GO

ALTER TABLE [CTM].[Lead] ADD CONSTRAINT [FK_Lead_MasterCenter_ConsentTypeMasterCenterID] FOREIGN KEY ([ConsentTypeMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION;

GO

ALTER TABLE [CTM].[Opportunity] ADD CONSTRAINT [FK_Opportunity_MasterCenter_ConsentTypeMasterCenterID] FOREIGN KEY ([ConsentTypeMasterCenterID]) REFERENCES [MST].[MasterCenter] ([ID]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201130095730_add_tb_CustomerConsentHistory_Kim_Kim', N'2.2.2-servicing-10034');

GO

