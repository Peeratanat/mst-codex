DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[USR].[User]') AND [c].[name] = N'IsLCMPrimary');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [USR].[User] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [USR].[User] DROP COLUMN [IsLCMPrimary];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230316021416_DropField_IsLCMPrimary_by_teerapat', N'2.2.2-servicing-10034');

GO

