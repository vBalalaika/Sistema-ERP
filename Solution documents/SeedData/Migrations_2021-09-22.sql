BEGIN TRANSACTION;
GO

BEGIN
    CREATE TABLE [PurchaseStates] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Description] nvarchar(max) NULL,
        [IsActive] bit NOT NULL,
        CONSTRAINT [PK_PurchaseStates] PRIMARY KEY ([Id])
    );
END;
GO
BEGIN
    CREATE TABLE [QuoteRequestHeaders] (
        [Id] int NOT NULL IDENTITY,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedOn] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedOn] datetime2 NULL,
        [ConcurrencyToken] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Comments] nvarchar(max) NULL,
        [PurchaseStateId] int NOT NULL,
        [ResponseDate] datetime2 NULL,
        CONSTRAINT [PK_QuoteRequestHeaders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_QuoteRequestHeaders_PurchaseStates_PurchaseStateId] FOREIGN KEY ([PurchaseStateId]) REFERENCES [PurchaseStates] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE TABLE [Structures] (
        [Id] int NOT NULL IDENTITY,
        [Description] nvarchar(50) NULL,
        [IsBase] bit NOT NULL,
        [ProductId] int NOT NULL,
        [LastVersionId] int NOT NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedOn] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedOn] datetime2 NULL,
        [ConcurrencyToken] int NOT NULL,
        CONSTRAINT [PK_Structures] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Structures_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Structures_Versions_LastVersionId] FOREIGN KEY ([LastVersionId]) REFERENCES [Versions] ([Id]) ON DELETE CASCADE
    );
END;
GO
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE TABLE [StructureItems] (
        [Id] int NOT NULL IDENTITY,
        [Quantity] int NOT NULL,
        [Order] int NOT NULL,
        [VersionId] int NOT NULL,
        [SonProductId] int NULL,
        [SonStructureId] int NULL,
        [StructureId] int NULL,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedOn] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedOn] datetime2 NULL,
        [ConcurrencyToken] int NOT NULL,
        CONSTRAINT [PK_StructureItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_StructureItems_Products_SonProductId] FOREIGN KEY ([SonProductId]) REFERENCES [Products] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_StructureItems_Structures_SonStructureId] FOREIGN KEY ([SonStructureId]) REFERENCES [Structures] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_StructureItems_Structures_StructureId] FOREIGN KEY ([StructureId]) REFERENCES [Structures] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_StructureItems_Versions_VersionId] FOREIGN KEY ([VersionId]) REFERENCES [Versions] ([Id]) ON DELETE CASCADE
    );
END;
GO
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE TABLE [MissingProducts] (
        [Id] int NOT NULL IDENTITY,
        [CreatedOn] datetime2 NOT NULL,
        [ProviderId] int NULL,
        [ProductId] int NULL,
        [UnitMeasureId] int NULL,
        [StateMissingProductId] int NULL,
        [Quantity] decimal(30,6) NOT NULL,
        [Comments] nvarchar(max) NULL,
        [UserName] nvarchar(max) NULL,
        [CreatedBy] nvarchar(max) NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedOn] datetime2 NULL,
        [ConcurrencyToken] int NOT NULL,
        CONSTRAINT [PK_MissingProducts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_MissingProducts_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]),
        CONSTRAINT [FK_MissingProducts_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]),
        CONSTRAINT [FK_MissingProducts_PurchaseStates_StateMissingProductId] FOREIGN KEY ([StateMissingProductId]) REFERENCES [PurchaseStates] ([Id]),
        CONSTRAINT [FK_MissingProducts_UnitMeasures_UnitMeasureId] FOREIGN KEY ([UnitMeasureId]) REFERENCES [UnitMeasures] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE TABLE [PurchaseOrderHeaders] (
        [Id] int NOT NULL IDENTITY,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedOn] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedOn] datetime2 NULL,
        [ConcurrencyToken] int NOT NULL,
        [Date] datetime2 NOT NULL,
        [Comments] nvarchar(max) NULL,
        [PurchaseStateId] int NOT NULL,
        [ProviderId] int NOT NULL,
        [ResponseDate] datetime2 NULL,
        CONSTRAINT [PK_PurchaseOrderHeaders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_PurchaseOrderHeaders_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]),
        CONSTRAINT [FK_PurchaseOrderHeaders_PurchaseStates_PurchaseStateId] FOREIGN KEY ([PurchaseStateId]) REFERENCES [PurchaseStates] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE TABLE [QuoteRequestDetails] (
        [Id] int NOT NULL IDENTITY,
        [CreatedBy] nvarchar(max) NULL,
        [CreatedOn] datetime2 NOT NULL,
        [LastModifiedBy] nvarchar(max) NULL,
        [LastModifiedOn] datetime2 NULL,
        [ConcurrencyToken] int NOT NULL,
        [QuoteRequestHeaderId] int NOT NULL,
        [ProviderId] int NOT NULL,
        [ProductId] int NOT NULL,
        [UnitMeasureId] int NOT NULL,
        [Quantity] decimal(30,6) NOT NULL,
        [WithMaterial] bit NOT NULL,
        [DollarPrice] decimal(30,6) NOT NULL,
        [PesosPrice] decimal(30,6) NOT NULL,
        CONSTRAINT [PK_QuoteRequestDetails] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_QuoteRequestDetails_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]),
        CONSTRAINT [FK_QuoteRequestDetails_Providers_ProviderId] FOREIGN KEY ([ProviderId]) REFERENCES [Providers] ([Id]),
        CONSTRAINT [FK_QuoteRequestDetails_QuoteRequestHeaders_QuoteRequestHeaderId] FOREIGN KEY ([QuoteRequestHeaderId]) REFERENCES [QuoteRequestHeaders] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_QuoteRequestDetails_UnitMeasures_UnitMeasureId] FOREIGN KEY ([UnitMeasureId]) REFERENCES [UnitMeasures] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_PurchaseOrderDetails_ProductId] ON [PurchaseOrderDetails] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_PurchaseOrderDetails_PurchaseOrderHeaderId] ON [PurchaseOrderDetails] ([PurchaseOrderHeaderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_PurchaseOrderDetails_UnitMeasureId] ON [PurchaseOrderDetails] ([UnitMeasureId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_PurchaseOrderHeaders_ProviderId] ON [PurchaseOrderHeaders] ([ProviderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_PurchaseOrderHeaders_PurchaseStateId] ON [PurchaseOrderHeaders] ([PurchaseStateId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_QuoteRequestDetails_ProductId] ON [QuoteRequestDetails] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_QuoteRequestDetails_ProviderId] ON [QuoteRequestDetails] ([ProviderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_QuoteRequestDetails_QuoteRequestHeaderId] ON [QuoteRequestDetails] ([QuoteRequestHeaderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_QuoteRequestDetails_UnitMeasureId] ON [QuoteRequestDetails] ([UnitMeasureId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_QuoteRequestHeaders_PurchaseStateId] ON [QuoteRequestHeaders] ([PurchaseStateId]);
END;
GO
IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_StructureItems_StructureId] ON [StructureItems] ([StructureId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_StructureItems_VersionId] ON [StructureItems] ([VersionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_Structures_LastVersionId] ON [Structures] ([LastVersionId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    CREATE INDEX [IX_Structures_ProductId] ON [Structures] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210923014235_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210923014235_Initial', N'5.0.1');
END;
GO

COMMIT;
GO