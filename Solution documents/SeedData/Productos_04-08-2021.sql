USE [SystemERP_DB]
GO
SET IDENTITY_INSERT [dbo].[ArchiveTypes] ON 
GO
INSERT [dbo].[ArchiveTypes] ([Id], [Description], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [IconClass]) VALUES (1, N'Planos', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-14T16:09:02.1577315' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-14T16:09:02.1577315' AS DateTime2), N'fas fa-pencil-ruler')
GO
INSERT [dbo].[ArchiveTypes] ([Id], [Description], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [IconClass]) VALUES (3, N'3D', 2, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-14T16:31:33.9233398' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-14T16:31:33.9233398' AS DateTime2), N'fab fa-unity')
GO
INSERT [dbo].[ArchiveTypes] ([Id], [Description], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [IconClass]) VALUES (4, N'PDF', 3, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-14T16:31:33.9233398' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-14T16:31:33.9233398' AS DateTime2), N'far fa-file-pdf')
GO
SET IDENTITY_INSERT [dbo].[ArchiveTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (1, N'Maquinas y Accesorios', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-03T17:48:14.6696260' AS DateTime2), 991)
GO
INSERT [dbo].[Categories] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (2, N'Conjuntos', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-11T14:46:05.4600420' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-11T14:46:05.4600422' AS DateTime2), 182)
GO
INSERT [dbo].[Categories] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (3, N'Piezas Individuales', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-11T14:46:16.7261246' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-11T14:46:16.7261248' AS DateTime2), 678)
GO
INSERT [dbo].[Categories] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (4, N'Componentes Comprados', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-11T16:17:15.7147250' AS DateTime2), 747)
GO
INSERT [dbo].[Categories] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (5, N'Herramientas e Insumos', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-15T12:39:51.3741878' AS DateTime2), 891)
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[CutTypes] ON 
GO
INSERT [dbo].[CutTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (1, N'Caliente', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[CutTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (3, N'Frio', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 3)
GO
SET IDENTITY_INSERT [dbo].[CutTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Operations] ON 
GO
INSERT [dbo].[Operations] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (1, N'Automatica', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Operations] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (2, N'Semi-Automatica', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2)
GO
INSERT [dbo].[Operations] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (3, N'Manual', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 3)
GO
SET IDENTITY_INSERT [dbo].[Operations] OFF
GO
SET IDENTITY_INSERT [dbo].[PieceFormats] ON 
GO
INSERT [dbo].[PieceFormats] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (1, N'Tela Plana', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[PieceFormats] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (2, N'Tela Tubo', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2)
GO
INSERT [dbo].[PieceFormats] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (3, N'Cinta', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 3)
GO
SET IDENTITY_INSERT [dbo].[PieceFormats] OFF
GO
SET IDENTITY_INSERT [dbo].[PieceTypes] ON
GO
INSERT [dbo].[PieceTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (1, N'Saco Chico', 1, NULL, CAST(N'2021-06-15T20:57:02.7684983' AS DateTime2), NULL, NULL, 100)
GO
INSERT [dbo].[PieceTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (2, N'Big Bag', 1, NULL, CAST(N'2021-06-15T20:57:02.7684983' AS DateTime2), NULL, NULL, 101)
GO
INSERT [dbo].[PieceTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (3, N'Cinta Reposera', 1, NULL, CAST(N'2021-06-15T20:57:02.7684983' AS DateTime2), NULL, NULL, 102)
GO
INSERT [dbo].[PieceTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (4, N'Eslinga', 1, NULL, CAST(N'2021-06-15T20:57:02.7684983' AS DateTime2), NULL, NULL, 103)
GO
INSERT [dbo].[PieceTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (5, N'Pasacalle', 1, NULL, CAST(N'2021-06-15T20:57:02.7684983' AS DateTime2), NULL, NULL, 104)
GO
INSERT [dbo].[PieceTypes] ([Id], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (6, N'Tejido Coversol', 1, NULL, CAST(N'2021-06-15T20:57:02.7684983' AS DateTime2), NULL, NULL, 105)
GO
SET IDENTITY_INSERT [dbo].[PieceTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[SubCategories] ON 
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (2, N'Cortadoras', 1, N'MC', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:18:01.6202468' AS DateTime2), 287)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (3, N'Piezas corte láser', 1, N'PC', 3, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:18:50.2732613' AS DateTime2), 470)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (4, N'Conjuntos soldados', 1, N'CS', 2, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:25:03.2308929' AS DateTime2), 718)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (5, N'Buloneria', 1, N'CT', 4, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-06-14T19:54:27.4262637' AS DateTime2), 671)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (7, N'Impresoras', 1, N'MI', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:19:25.8409077' AS DateTime2), 584)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (12, N'Prensas', 1, N'MP', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:19:31.6720418' AS DateTime2), 428)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (13, N'De Conversión', 1, N'MV', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:19:57.2397937' AS DateTime2), 745)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (17, N'Apiladoras', 1, N'MA', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:20:17.0646595' AS DateTime2), 346)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (19, N'Accesorios', 1, N'AC', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:20:48.6966092' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:20:48.6966093' AS DateTime2), 316)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (20, N'Piezas de revolución', 1, N'PR', 3, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:21:28.6555752' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:21:28.6555756' AS DateTime2), 502)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (21, N'Piezas estructurales', 1, N'PE', 3, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:21:44.4238697' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:21:44.4238699' AS DateTime2), 427)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (22, N'Piezas plegadas', 1, N'PD', 3, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:22:03.4150048' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:22:03.4150050' AS DateTime2), 598)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (23, N'Piezas varias-Piezas de fundición', 1, N'PV', 3, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:22:30.0713429' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:22:30.0713430' AS DateTime2), 989)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (24, N'Piezas fabricadas por terceros', 1, N'PX', 3, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:22:45.9912362' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:22:45.9912363' AS DateTime2), 87)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (25, N'Conjuntos armados', 1, N'CA', 2, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:23:14.7996935' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:23:14.7996938' AS DateTime2), 555)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (26, N'Conjuntos electricidad ', 1, N'CE', 2, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:23:28.1283784' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:23:28.1283785' AS DateTime2), 150)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (27, N'Conjuntos neumática', 1, N'CN', 2, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:24:42.4469222' AS DateTime2), 880)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (28, N'Conjuntos motores', 1, N'CM', 2, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:23:57.0873275' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:23:57.0873276' AS DateTime2), 37)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (29, N'Electricidad', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:25:42.5347084' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:25:42.5347086' AS DateTime2), 149)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (30, N'Hidráulica', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:25:55.7986454' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:25:55.7986455' AS DateTime2), 779)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (31, N'Materia prima', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:10.2871796' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:10.2871797' AS DateTime2), 406)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (32, N'Motores', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:19.3981064' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:19.3981065' AS DateTime2), 828)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (33, N'Neumática', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:32.2953358' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:32.2953359' AS DateTime2), 558)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (34, N'Rodamientos', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:47.5903977' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:26:47.5903978' AS DateTime2), 98)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (35, N'Transmisión', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:27:06.2806370' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:27:06.2806373' AS DateTime2), 958)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (36, N'Resortes', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:27:15.7268549' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:27:15.7268550' AS DateTime2), 181)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (37, N'Varios', 1, N'CT', 4, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:27:25.2789141' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:27:25.2789142' AS DateTime2), 232)
GO
INSERT [dbo].[SubCategories] ([Id], [Description], [IsActive], [Prefix], [CategoryId], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (38, N'Herramientas e Insumos', 1, N'HI', 5, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:28:02.4478227' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'2021-08-04T12:28:02.4478228' AS DateTime2), 429)
GO
SET IDENTITY_INSERT [dbo].[SubCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[SupplyVoltages] ON
GO
INSERT [dbo].[SupplyVoltages] ([Id], [Description], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (1, N'1x220', 1, 12, NULL, CAST(N'2021-06-23T19:21:43.3856255' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[SupplyVoltages] ([Id], [Description], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (3, N'3x220', 1, 16, NULL, CAST(N'2021-06-23T19:21:43.3856255' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[SupplyVoltages] ([Id], [Description], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (4, N'3x380', 1, 15, NULL, CAST(N'2021-06-23T19:21:43.3856255' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[SupplyVoltages] ([Id], [Description], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (5, N'Neutro', 1, 13, NULL, CAST(N'2021-06-23T19:21:43.3856255' AS DateTime2), NULL, NULL)
GO
INSERT [dbo].[SupplyVoltages] ([Id], [Description], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) VALUES (6, N'Tierra', 1, 14, NULL, CAST(N'2021-06-23T19:21:43.3856255' AS DateTime2), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[SupplyVoltages] OFF
GO
SET IDENTITY_INSERT [dbo].[UnitMeasures] ON 
GO
INSERT [dbo].[UnitMeasures] ([Id], [Code], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (1, 1, N'KM', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[UnitMeasures] ([Id], [Code], [Description], [IsActive], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn], [ConcurrencyToken]) VALUES (2, 2, N'Mts', 1, N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), N'7403c28b-2fa2-42b5-92b5-2b7e4eb02a2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 2)
GO
SET IDENTITY_INSERT [dbo].[UnitMeasures] OFF
GO
ALTER TABLE [dbo].[ProductFeatures] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Standar]
GO
ALTER TABLE [dbo].[ProductFeatures] ADD  DEFAULT (CONVERT([bit],(0))) FOR [InHouseManufacturing]
GO
ALTER TABLE [dbo].[ProductFeatures] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Bought]
GO
ALTER TABLE [dbo].[ProductFeatures] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Component]
GO
ALTER TABLE [dbo].[ProductFeatures] ADD  DEFAULT (CONVERT([bit],(0))) FOR [Finished]
GO
ALTER TABLE [dbo].[ProductFeatures] ADD  DEFAULT (CONVERT([bit],(0))) FOR [StoredStock]
GO