USE [SystemERP_DB]
GO
/****** Object:  Table [dbo].[OperationStates]    Script Date: 03/08/2021 17:11:09 ******/
SET IDENTITY_INSERT [dbo].[OperationStates] ON 
GO
INSERT [dbo].[OperationStates] ([Id], [Description], [IsActive]) VALUES (1, N'No iniciada', 1)
GO
INSERT [dbo].[OperationStates] ([Id], [Description], [IsActive]) VALUES (2, N'Iniciada', 1)
GO
INSERT [dbo].[OperationStates] ([Id], [Description], [IsActive]) VALUES (3, N'En curso', 1)
GO
INSERT [dbo].[OperationStates] ([Id], [Description], [IsActive]) VALUES (4, N'Concretada', 1)
GO
INSERT [dbo].[OperationStates] ([Id], [Description], [IsActive]) VALUES (5, N'De baja', 1)
GO
SET IDENTITY_INSERT [dbo].[OperationStates] OFF
GO
