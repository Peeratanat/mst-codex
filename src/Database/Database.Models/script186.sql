CREATE TABLE [MST].[Event] (
    [ID] uniqueidentifier NOT NULL,
    [Created] datetime2 NULL,
    [Updated] datetime2 NULL,
    [CreatedByUserID] uniqueidentifier NULL,
    [UpdatedByUserID] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL,
    [NameTH] NVARCHAR(100) NULL,
    [NameEN] nvarchar(100) NULL,
    [EventDate] datetime2 NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Event_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Event_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [MST].[ProjectInEvent] (
    [ID] uniqueidentifier NOT NULL,
    [Created] datetime2 NULL,
    [Updated] datetime2 NULL,
    [CreatedByUserID] uniqueidentifier NULL,
    [UpdatedByUserID] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL,
    [PojectID] uniqueidentifier NULL,
    CONSTRAINT [PK_ProjectInEvent] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_ProjectInEvent_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectInEvent_Project_PojectID] FOREIGN KEY ([PojectID]) REFERENCES [PRJ].[Project] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProjectInEvent_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
);

GO

CREATE TABLE [CTM].[ContactRegister] (
    [ID] uniqueidentifier NOT NULL,
    [Created] datetime2 NULL,
    [Updated] datetime2 NULL,
    [CreatedByUserID] uniqueidentifier NULL,
    [UpdatedByUserID] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL,
    [ContactID] uniqueidentifier NULL,
    [RegisterDate] datetime2 NULL,
    [EventID] uniqueidentifier NULL,
    [PojectID] uniqueidentifier NULL,
    [Queue] nvarchar(max) NULL,
    CONSTRAINT [PK_ContactRegister] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_ContactRegister_Contact_ContactID] FOREIGN KEY ([ContactID]) REFERENCES [CTM].[Contact] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContactRegister_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContactRegister_Event_EventID] FOREIGN KEY ([EventID]) REFERENCES [MST].[Event] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContactRegister_Project_PojectID] FOREIGN KEY ([PojectID]) REFERENCES [PRJ].[Project] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ContactRegister_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_ContactRegister_ContactID] ON [CTM].[ContactRegister] ([ContactID]);

GO

CREATE INDEX [IX_ContactRegister_CreatedByUserID] ON [CTM].[ContactRegister] ([CreatedByUserID]);

GO

CREATE INDEX [IX_ContactRegister_EventID] ON [CTM].[ContactRegister] ([EventID]);

GO

CREATE INDEX [IX_ContactRegister_PojectID] ON [CTM].[ContactRegister] ([PojectID]);

GO

CREATE INDEX [IX_ContactRegister_UpdatedByUserID] ON [CTM].[ContactRegister] ([UpdatedByUserID]);

GO

CREATE INDEX [IX_Event_CreatedByUserID] ON [MST].[Event] ([CreatedByUserID]);

GO

CREATE INDEX [IX_Event_UpdatedByUserID] ON [MST].[Event] ([UpdatedByUserID]);

GO

CREATE INDEX [IX_ProjectInEvent_CreatedByUserID] ON [MST].[ProjectInEvent] ([CreatedByUserID]);

GO

CREATE INDEX [IX_ProjectInEvent_PojectID] ON [MST].[ProjectInEvent] ([PojectID]);

GO

CREATE INDEX [IX_ProjectInEvent_UpdatedByUserID] ON [MST].[ProjectInEvent] ([UpdatedByUserID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230420043448_altertable_Event_by_teerapat', N'2.2.2-servicing-10034');

GO

