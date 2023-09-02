IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    CREATE TABLE [StateMissingProducts] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_StateMissingProducts] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    ALTER TABLE [Providers] 
    ADD [ProfitID] int NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    CREATE TABLE [MissingProducts] (
        [Id] int NOT NULL IDENTITY,
        [CreatedOn] datetime2 NOT NULL,
        [ProviderId] int NULL,
        [ProductId] int NULL,
        [UnitMeasureId] int NULL,
        [StateMissingProductId] int NULL,
        [Quantity] decimal(30,6) NULL,
        [Comments] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [CreatedBy] nvarchar(max) NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedOn] datetime2 NULL,
        [ConcurrencyToken] int NOT NULL,
        CONSTRAINT [PK_MissingProducts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MissingProducts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]),
        CONSTRAINT [FK_MissingProducts_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]),
        CONSTRAINT [FK_MissingProducts_StateMissingProducts_StateMissingProductId] FOREIGN KEY ([StateMissingProductId]) REFERENCES [StateMissingProducts] ([Id]),
        CONSTRAINT [FK_MissingProducts_UnitMeasures_UnitMeasureId] FOREIGN KEY ([UnitMeasureId]) REFERENCES [UnitMeasures] ([Id])
    );
END;
GO
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    CREATE INDEX [IX_MissingProducts_ProductId] ON [MissingProducts] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    CREATE INDEX [IX_MissingProducts_ProviderId] ON [MissingProducts] ([ProviderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    CREATE INDEX [IX_MissingProducts_StateMissingProductId] ON [MissingProducts] ([StateMissingProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    CREATE INDEX [IX_MissingProducts_UnitMeasureId] ON [MissingProducts] ([UnitMeasureId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210910121022_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210910121022_Initial', N'5.0.1');
END;
GO

COMMIT;
GO