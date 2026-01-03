ALTER TABLE [CTM].[ContactRegister] DROP CONSTRAINT [FK_ContactRegister_Project_PojectID];

GO

DROP INDEX [IX_ContactRegister_PojectID] ON [CTM].[ContactRegister];

GO


DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CTM].[ContactRegister]') AND [c].[name] = N'PojectID');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [CTM].[ContactRegister] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [CTM].[ContactRegister] DROP COLUMN [PojectID];

GO


ALTER TABLE [MST].[ProjectInEvent] ADD [EventID] uniqueidentifier NULL;

GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CTM].[ContactRegister]') AND [c].[name] = N'Queue');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [CTM].[ContactRegister] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [CTM].[ContactRegister] ALTER COLUMN [Queue] int NOT NULL;

GO

CREATE INDEX [IX_ProjectInEvent_EventID] ON [MST].[ProjectInEvent] ([EventID]);

GO

ALTER TABLE [MST].[ProjectInEvent] ADD CONSTRAINT [FK_ProjectInEvent_Event_EventID] FOREIGN KEY ([EventID]) REFERENCES [MST].[Event] ([ID]) ON DELETE NO ACTION;

GO


ALTER TABLE MST.Event DROP COLUMN EventDate;

ALTER TABLE	MST.Event ADD EventDateFrom DATETIME2;
ALTER TABLE	MST.Event ADD EventDateTo DATETIME2;