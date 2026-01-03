
DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PRJ].[LetterGuarantee]') AND [c].[name] = N'ExpiredDate');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [PRJ].[LetterGuarantee] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [PRJ].[LetterGuarantee] ALTER COLUMN [ExpiredDate] datetime2 NULL;

GO

CREATE TABLE [PRJ].[LetterGuaranteeFile] (
    [ID] uniqueidentifier NOT NULL,
    [Created] datetime2 NULL,
    [Updated] datetime2 NULL,
    [CreatedByUserID] uniqueidentifier NULL,
    [UpdatedByUserID] uniqueidentifier NULL,
    [IsDeleted] bit NOT NULL,
    [LetterGuaranteeID] uniqueidentifier NOT NULL,
    [FileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [Remark] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    CONSTRAINT [PK_LetterGuaranteeFile] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_LetterGuaranteeFile_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LetterGuaranteeFile_LetterGuarantee_LetterGuaranteeID] FOREIGN KEY ([LetterGuaranteeID]) REFERENCES [PRJ].[LetterGuarantee] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_LetterGuaranteeFile_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_LetterGuaranteeFile_CreatedByUserID] ON [PRJ].[LetterGuaranteeFile] ([CreatedByUserID]);

GO

CREATE INDEX [IX_LetterGuaranteeFile_LetterGuaranteeID] ON [PRJ].[LetterGuaranteeFile] ([LetterGuaranteeID]);

GO

CREATE INDEX [IX_LetterGuaranteeFile_UpdatedByUserID] ON [PRJ].[LetterGuaranteeFile] ([UpdatedByUserID]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230313030411_LetterGuaranteeHistory_by_teerapat', N'2.2.2-servicing-10034');

GO

