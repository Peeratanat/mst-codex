ALTER TABLE [MST].[Event] ADD [Isactive] bit NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230420075204_Edit_tableEvent_Event_by_teerapat', N'2.2.2-servicing-10034');

GO

