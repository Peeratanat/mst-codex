ALTER TABLE [USR].[User] ADD [IsLCMPrimary] bit NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230314042911_addField_IsLCMPrimary_by_teerapat', N'2.2.2-servicing-10034');

GO

