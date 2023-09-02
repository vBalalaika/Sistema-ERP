USE [SystemERP_DB]
GO

ALTER TABLE [dbo].[Products] ADD [Version] [nvarchar](max) NULL
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FunctionalAreas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[LastModifiedBy] [nvarchar](max) NULL,
	[LastModifiedOn] [datetime2](7) NULL,
	[ConcurrencyToken] [int] NOT NULL,
 CONSTRAINT [PK_FunctionalAreas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Stations] ADD [FunctionalAreaId] [int] NOT NULL
GO

ALTER TABLE [dbo].[Stations]  WITH CHECK ADD  CONSTRAINT [FK_Stations_FunctionalAreas_FunctionalAreaId] FOREIGN KEY([FunctionalAreaId])
REFERENCES [dbo].[FunctionalAreas] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Stations] CHECK CONSTRAINT [FK_Stations_FunctionalAreas_FunctionalAreaId]
GO


ALTER TABLE [dbo].[Products] ADD [LocationStationId] [int] NULL
GO

ALTER TABLE [dbo].[Products] WITH CHECK ADD CONSTRAINT [FK_Products_Stations_LocationStationId] FOREIGN KEY ([LocationStationId]) REFERENCES [Stations] ([Id]) ON DELETE NO ACTION
GO

        