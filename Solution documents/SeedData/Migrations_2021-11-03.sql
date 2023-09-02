BEGIN TRANSACTION;
GO

BEGIN
    ALTER TABLE [MissingProducts] ADD [StockUnitMeasureId] int NULL;
END;
GO

BEGIN
    ALTER TABLE [RelProviderProducts] ADD [WidthUnitMeasureId] int NULL;
    ALTER TABLE [RelProviderProducts] ADD [LengthUnitMeasureId] int NULL;
    ALTER TABLE [RelProviderProducts] ADD [WeightUnitMeasureId] int NULL;
END;
GO

BEGIN
    CREATE INDEX [IX_MissingProducts_StockUnitMeasureId] ON [MissingProducts] ([StockUnitMeasureId]);
END;
GO

BEGIN
    CREATE INDEX [IX_RelProviderProducts_LengthUnitMeasureId] ON [RelProviderProducts] ([LengthUnitMeasureId]);
END;
GO

BEGIN
    CREATE INDEX [IX_RelProviderProducts_WeightUnitMeasureId] ON [RelProviderProducts] ([WeightUnitMeasureId]);
END;
GO

BEGIN
    CREATE INDEX [IX_RelProviderProducts_WidthUnitMeasureId] ON [RelProviderProducts] ([WidthUnitMeasureId]);
END;
GO

BEGIN
    ALTER TABLE [MissingProducts] ADD CONSTRAINT [FK_MissingProducts_UnitMeasures_StockUnitMeasureId] FOREIGN KEY ([StockUnitMeasureId]) REFERENCES [UnitMeasures] ([Id]) ON DELETE NO ACTION;
END;
GO

ALTER TABLE [dbo].[RelProviderProducts]  WITH CHECK ADD  CONSTRAINT [FK_RelProviderProducts_UnitMeasures_LengthUnitMeasureId] FOREIGN KEY([LengthUnitMeasureId])
REFERENCES [dbo].[UnitMeasures] ([Id]) ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[RelProviderProducts] CHECK CONSTRAINT [FK_RelProviderProducts_UnitMeasures_LengthUnitMeasureId]
GO
ALTER TABLE [dbo].[RelProviderProducts]  WITH CHECK ADD  CONSTRAINT [FK_RelProviderProducts_UnitMeasures_WeightUnitMeasureId] FOREIGN KEY([WeightUnitMeasureId])
REFERENCES [dbo].[UnitMeasures] ([Id]) ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[RelProviderProducts] CHECK CONSTRAINT [FK_RelProviderProducts_UnitMeasures_WeightUnitMeasureId]
GO
ALTER TABLE [dbo].[RelProviderProducts]  WITH CHECK ADD  CONSTRAINT [FK_RelProviderProducts_UnitMeasures_WidthUnitMeasureId] FOREIGN KEY([WidthUnitMeasureId])
REFERENCES [dbo].[UnitMeasures] ([Id]) ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[RelProviderProducts] CHECK CONSTRAINT [FK_RelProviderProducts_UnitMeasures_WidthUnitMeasureId]
GO

COMMIT;
GO