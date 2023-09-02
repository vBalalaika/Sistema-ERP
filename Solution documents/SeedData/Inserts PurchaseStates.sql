USE [SystemERP_DB]
GO
INSERT INTO [dbo].[PurchaseStates] ([Name],[Description],[IsActive]) VALUES ('Pendiente', 'Aún no se ejecutó ninguna acción con ese ítem', 1)
GO
INSERT INTO [dbo].[PurchaseStates] ([Name],[Description],[IsActive]) VALUES ('Esperando cotización', 'Se envió mail para pedir cotización', 1)
GO
INSERT INTO [dbo].[PurchaseStates] ([Name],[Description],[IsActive]) VALUES ('Cotizado', 'Se le cargó una cotización', 1)
GO
INSERT INTO [dbo].[PurchaseStates] ([Name],[Description],[IsActive]) VALUES ('Comprado', 'Se envió una orden de compra', 1)
GO
INSERT INTO [dbo].[PurchaseStates] ([Name],[Description],[IsActive]) VALUES ('Finalizado', 'Se ingresó la factura', 1)
GO