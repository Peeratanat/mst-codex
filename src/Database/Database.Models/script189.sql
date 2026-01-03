ALTER TABLE [CTM].[ContactRegister] ADD [Prefix] nvarchar(max) NULL;

GO

--CREATE TABLE [PRJ].[UnitControlInterest] (
--    [ID] uniqueidentifier NOT NULL,
--    [Created] datetime2 NULL,
--    [Updated] datetime2 NULL,
--    [CreatedByUserID] uniqueidentifier NULL,
--    [UpdatedByUserID] uniqueidentifier NULL,
--    [IsDeleted] bit NOT NULL,
--    [RefMigrateID1] nvarchar(100) NULL,
--    [RefMigrateID2] nvarchar(100) NULL,
--    [RefMigrateID3] nvarchar(100) NULL,
--    [LastMigrateDate] datetime2 NULL,
--    [ProjectID] uniqueidentifier NULL,
--    [UnitID] uniqueidentifier NULL,
--    [InterestCounter] int NULL,
--    [EffectiveDate] datetime2 NULL,
--    [ExpiredDate] datetime2 NULL,
--    [Remark] nvarchar(max) NULL,
--    CONSTRAINT [PK_UnitControlInterest] PRIMARY KEY ([ID]),
--    CONSTRAINT [FK_UnitControlInterest_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_UnitControlInterest_Project_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [PRJ].[Project] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_UnitControlInterest_Unit_UnitID] FOREIGN KEY ([UnitID]) REFERENCES [PRJ].[Unit] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_UnitControlInterest_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
--);

--GO

--CREATE TABLE [PRJ].[UnitControlLock] (
--    [ID] uniqueidentifier NOT NULL,
--    [Created] datetime2 NULL,
--    [Updated] datetime2 NULL,
--    [CreatedByUserID] uniqueidentifier NULL,
--    [UpdatedByUserID] uniqueidentifier NULL,
--    [IsDeleted] bit NOT NULL,
--    [RefMigrateID1] nvarchar(100) NULL,
--    [RefMigrateID2] nvarchar(100) NULL,
--    [RefMigrateID3] nvarchar(100) NULL,
--    [LastMigrateDate] datetime2 NULL,
--    [ProjectID] uniqueidentifier NULL,
--    [UnitID] uniqueidentifier NULL,
--    [FloorID] uniqueidentifier NULL,
--    [EffectiveDate] datetime2 NULL,
--    [ExpiredDate] datetime2 NULL,
--    [Remark] nvarchar(max) NULL,
--    CONSTRAINT [PK_UnitControlLock] PRIMARY KEY ([ID]),
--    CONSTRAINT [FK_UnitControlLock_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_UnitControlLock_Floor_FloorID] FOREIGN KEY ([FloorID]) REFERENCES [PRJ].[Floor] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_UnitControlLock_Project_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [PRJ].[Project] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_UnitControlLock_Unit_UnitID] FOREIGN KEY ([UnitID]) REFERENCES [PRJ].[Unit] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_UnitControlLock_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
--);

--GO

--CREATE TABLE [SAL].[BookingControl] (
--    [ID] uniqueidentifier NOT NULL,
--    [Created] datetime2 NULL,
--    [Updated] datetime2 NULL,
--    [CreatedByUserID] uniqueidentifier NULL,
--    [UpdatedByUserID] uniqueidentifier NULL,
--    [IsDeleted] bit NOT NULL,
--    [RefMigrateID1] nvarchar(100) NULL,
--    [RefMigrateID2] nvarchar(100) NULL,
--    [RefMigrateID3] nvarchar(100) NULL,
--    [LastMigrateDate] datetime2 NULL,
--    [BookingID] uniqueidentifier NULL,
--    [ProjectID] uniqueidentifier NULL,
--    [UnitID] uniqueidentifier NULL,
--    [EffectiveDate] datetime2 NULL,
--    [ExpiredDate] datetime2 NULL,
--    [Remark] nvarchar(max) NULL,
--    [BookingLockMasterCenterID] uniqueidentifier NULL,
--    CONSTRAINT [PK_BookingControl] PRIMARY KEY ([ID]),
--    CONSTRAINT [FK_BookingControl_Booking_BookingID] FOREIGN KEY ([BookingID]) REFERENCES [SAL].[Booking] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_BookingControl_User_CreatedByUserID] FOREIGN KEY ([CreatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_BookingControl_Project_ProjectID] FOREIGN KEY ([ProjectID]) REFERENCES [PRJ].[Project] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_BookingControl_Unit_UnitID] FOREIGN KEY ([UnitID]) REFERENCES [PRJ].[Unit] ([ID]) ON DELETE NO ACTION,
--    CONSTRAINT [FK_BookingControl_User_UpdatedByUserID] FOREIGN KEY ([UpdatedByUserID]) REFERENCES [USR].[User] ([ID]) ON DELETE NO ACTION
--);

--GO

--CREATE INDEX [IX_UnitControlInterest_CreatedByUserID] ON [PRJ].[UnitControlInterest] ([CreatedByUserID]);

--GO

--CREATE INDEX [IX_UnitControlInterest_ProjectID] ON [PRJ].[UnitControlInterest] ([ProjectID]);

--GO

--CREATE INDEX [IX_UnitControlInterest_UnitID] ON [PRJ].[UnitControlInterest] ([UnitID]);

--GO

--CREATE INDEX [IX_UnitControlInterest_UpdatedByUserID] ON [PRJ].[UnitControlInterest] ([UpdatedByUserID]);

--GO

--CREATE INDEX [IX_UnitControlLock_CreatedByUserID] ON [PRJ].[UnitControlLock] ([CreatedByUserID]);

--GO

--CREATE INDEX [IX_UnitControlLock_FloorID] ON [PRJ].[UnitControlLock] ([FloorID]);

--GO

--CREATE INDEX [IX_UnitControlLock_ProjectID] ON [PRJ].[UnitControlLock] ([ProjectID]);

--GO

--CREATE INDEX [IX_UnitControlLock_UnitID] ON [PRJ].[UnitControlLock] ([UnitID]);

--GO

--CREATE INDEX [IX_UnitControlLock_UpdatedByUserID] ON [PRJ].[UnitControlLock] ([UpdatedByUserID]);

--GO

--CREATE INDEX [IX_BookingControl_BookingID] ON [SAL].[BookingControl] ([BookingID]);

--GO

--CREATE INDEX [IX_BookingControl_CreatedByUserID] ON [SAL].[BookingControl] ([CreatedByUserID]);

--GO

--CREATE INDEX [IX_BookingControl_ProjectID] ON [SAL].[BookingControl] ([ProjectID]);

--GO

--CREATE INDEX [IX_BookingControl_UnitID] ON [SAL].[BookingControl] ([UnitID]);

--GO

--CREATE INDEX [IX_BookingControl_UpdatedByUserID] ON [SAL].[BookingControl] ([UpdatedByUserID]);

--GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230515033157_addField_in_Event_by_teerapat', N'2.2.2-servicing-10034');

GO

