use SystemERP_DB
go
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'dbo.PurchaseOrderDetails', N'U') IS NOT NULL  
   DROP TABLE [dbo].[PurchaseOrderDetails];  
GO
CREATE TABLE [dbo].[PurchaseOrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[LastModifiedOn] [datetime2](7) NULL,
	[ConcurrencyToken] [int] NOT NULL,
	[PurchaseOrderHeaderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitMeasureId] [int] NOT NULL,
	[Quantity] [decimal](30, 6) NOT NULL,
	[WithMaterial] [bit] NOT NULL,
	[DollarPrice] [decimal](30, 6) NOT NULL,
	[PesosPrice] [decimal](30, 6) NOT NULL,
 CONSTRAINT [PK_PurchaseOrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelQuoteRequestProviders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuoteRequestHeaderId] [int] NOT NULL,
	[ProviderId] [int] NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[LastModifiedOn] [datetime2](7) NULL,
	[ConcurrencyToken] [int] NOT NULL,
 CONSTRAINT [PK_RelQuoteRequestProviders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF OBJECT_ID(N'dbo.QuoteRequestDetails', N'U') IS NOT NULL  
   DROP TABLE [dbo].[QuoteRequestDetails];  
GO
CREATE TABLE [dbo].[QuoteRequestDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[LastModifiedOn] [datetime2](7) NULL,
	[ConcurrencyToken] [int] NOT NULL,
	[QuoteRequestHeaderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitMeasureId] [int] NOT NULL,
	[Quantity] [decimal](30, 6) NOT NULL,
	[WithMaterial] [bit] NOT NULL,
	[DollarPrice] [decimal](30, 6) NOT NULL,
	[PesosPrice] [decimal](30, 6) NOT NULL,
 CONSTRAINT [PK_QuoteRequestDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PurchaseOrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrderDetails] CHECK CONSTRAINT [FK_PurchaseOrderDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[PurchaseOrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetails_PurchaseOrderHeaders_PurchaseOrderHeaderId] FOREIGN KEY([PurchaseOrderHeaderId])
REFERENCES [dbo].[PurchaseOrderHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseOrderDetails] CHECK CONSTRAINT [FK_PurchaseOrderDetails_PurchaseOrderHeaders_PurchaseOrderHeaderId]
GO
ALTER TABLE [dbo].[PurchaseOrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetails_UnitMeasures_UnitMeasureId] FOREIGN KEY([UnitMeasureId])
REFERENCES [dbo].[UnitMeasures] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseOrderDetails] CHECK CONSTRAINT [FK_PurchaseOrderDetails_UnitMeasures_UnitMeasureId]
GO
ALTER TABLE [dbo].[QuoteRequestDetails]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[QuoteRequestDetails] CHECK CONSTRAINT [FK_QuoteRequestDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[QuoteRequestDetails]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestDetails_QuoteRequestHeaders_QuoteRequestHeaderId] FOREIGN KEY([QuoteRequestHeaderId])
REFERENCES [dbo].[QuoteRequestHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteRequestDetails] CHECK CONSTRAINT [FK_QuoteRequestDetails_QuoteRequestHeaders_QuoteRequestHeaderId]
GO
ALTER TABLE [dbo].[QuoteRequestDetails]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestDetails_UnitMeasures_UnitMeasureId] FOREIGN KEY([UnitMeasureId])
REFERENCES [dbo].[UnitMeasures] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuoteRequestDetails] CHECK CONSTRAINT [FK_QuoteRequestDetails_UnitMeasures_UnitMeasureId]
GO
ALTER TABLE [dbo].[RelQuoteRequestProviders]  WITH CHECK ADD  CONSTRAINT [FK_RelQuoteRequestProviders_Providers_ProviderId] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[Providers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RelQuoteRequestProviders] CHECK CONSTRAINT [FK_RelQuoteRequestProviders_Providers_ProviderId]
GO
ALTER TABLE [dbo].[RelQuoteRequestProviders]  WITH CHECK ADD  CONSTRAINT [FK_RelQuoteRequestProviders_QuoteRequestHeaders_QuoteRequestHeaderId] FOREIGN KEY([QuoteRequestHeaderId])
REFERENCES [dbo].[QuoteRequestHeaders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RelQuoteRequestProviders] CHECK CONSTRAINT [FK_RelQuoteRequestProviders_QuoteRequestHeaders_QuoteRequestHeaderId]
GO