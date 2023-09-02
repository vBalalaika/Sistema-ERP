USE [SystemERP_DB]
GO

/*
- Si se quiere borrar la información de la tabla y reiniciar la primary key ejecutar lo siguiente:

USE [SystemERP_DB]
GO
DELETE FROM Providers
GO
DBCC CHECKIDENT (Providers, RESEED,0)
GO

*/

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'López Forciniti S.A.', 5, 25, N'30579451099', NULL, N'Av. San Martín 4960', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Industrial controles S.R.L', 5, 25, N'30627739571', NULL, N'Av. Belgrano', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Shangai Lanbao Sensor Co, LTD', 5, 25, N'55000003106', NULL, N'Nº228 Kinbi', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Distribuidora Baudracco S.A.', 5, 25, N'30547596133', NULL, N'Av. Marconi 531', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'INVT International Trading  LTD', 5, 25, N'55000013106', NULL, NULL, NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Delta solutions S.A.', 5, 25, N'30711233810', NULL, N'Av. Garay 2399', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Festo S.A.', 5, 25, N'30596013399', NULL, N'Córdoba 3479', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Acerinox', 1, 25, N'30586275832', NULL, N'Av. Velez Sarsfield 1525', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Ningbo Pneubig Pneumatic CO. LTD', 5, 25, N'55000003107', NULL, N'Xikou industrial zone, Fenghua, Ningbo', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Wuxi Dekong Technology', 5, 25, N'484558232107', NULL, N'Room 516, Juanchuang building Nº888', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Abrasivos Pimaq S.R.L', 5, 25, N'30703587735', NULL, N'Vera Mujica 185', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Acplind S.R.L', 5, 25, N'30715864262', NULL, N'Lerma 63', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Alejandro Raineri', 5, 25, N'20236461522', NULL, N'calle 11 nº523', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Asia Dragon Electric Co, Ltd', 5, 25, N'55000003108', NULL, N'No 7 Road tenfei', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Autopartes Eelectricas Some S.R.L', 5, 25, N'30602647583', NULL, N'Chubut', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Ba Rodamientos', 5, 25, N'30710334257', NULL, N'Langen 3525', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Casa Astri S.A', 5, 25, N'30538247436', NULL, N'A.Cordoba 1532 Cap. Fed.', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Casa Goyo', 5, 25, N'30534466753', NULL, N'Rio Cuarto 313', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Chapas Oroño S.A', 5, 25, N'30592426044', NULL, N'Ovidio Lagos 5990', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Chavetas Galas S.R.L', 5, 25, N'33710813979', NULL, N'Bolivia 3260', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Cint-Broc', 5, 25, N'30560477836', NULL, N'Araujo 971', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Combepower S.A', 5, 25, N'30694354013', NULL, N'J.A Roca 4250 Fiorida Oeste', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'El rey del regaton', 5, 25, N'20045311458', NULL, N'Alberdi 7273', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Equal Bush S.A', 5, 25, N'30712418075', NULL, N'Suipacha 255 Piso 6', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Extrusora Argentina S.R.L', 5, 25, N'30677096876', NULL, N'Flor del Aire Nº 4928', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Famiq', 5, 25, N'30532568841', NULL, N'Av Francia 1996', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Fare Resistencias electricas S.R.L', 5, 25, N'30709463493', NULL, N'Lichtenstein 84', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Fibrocar', 5, 25, N'27178034400', NULL, N'Edison 1281', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Francisco Espocito E hijos S.R.L', 5, 25, N'30715213741', NULL, N'Peribebuy 2255', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Fundicion Damiano', 5, 25, N'27120198276', NULL, N'P. Dimmer 2155', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Galea S.R.L', 5, 25, N'30707194576', NULL, N'Limite del municipio Nº 4215', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Inbelt S.R.L', 5, 25, N'30582927231', NULL, N'Biedma 7257', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Industria Metalurgica Chirino', 5, 25, N'20288951714', NULL, N'Calle 25 y 57', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Industrial Coatings', 5, 25, N'30710280041', NULL, N'Peru 271', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Insertools', 5, 25, N'30710654332', NULL, N'Cafferata', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Intor', 5, 25, N'30710389043', NULL, N'Av 25 de Mayo 268', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Iscar tool', 5, 25, N'30707229167', NULL, N'Monteagudo 222', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'J.L Metales', 5, 25, N'33612087089', NULL, N'Av Mitre 3380', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Jacobone', 5, 25, N'20046843011', NULL, N'Paraguay 1160', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Jc Milesi', 5, 25, N'20312398878', NULL, N'Cosquin 5555', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Kussmaul', 5, 25, N'30539815322', NULL, N'Salta 3350', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Hao yu precision machinery industry co,ltd.', 5, 25, N'55000083109', NULL, N'No 290 hai wei rd lond distr taiwan', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Jiangsu gold electrical control technology co ltd', 5, 25, N'55020093110', NULL, N'C Biulding No 10 Lianhe road Hudai Industrial Zone', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Lavagcer S.R.L', 5, 25, N'30599214484', NULL, N'Richieri 1330/38', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Linqing Yuanyi Bearing co limited', 5, 25, N'55000000111', NULL, N'Pannan Village', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Lluy Alberto Ruben', 5, 25, N'20176658127', NULL, N'Isla Soledad 982', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Luczal De Angel Antonio Lucia', 5, 25, N'20121630800', NULL, N'Av Eva peron nº 2655', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Mar rodamiento', 5, 25, N'30525486318', NULL, N'Ovidio Lagos 4550', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Marotta Maderas', 5, 25, N'2046623852', NULL, N'Calle 49 e 13 y 14', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Mc Impreciones S.R.L', 5, 25, N'30714945447', NULL, N'Gdor Guillermo Udaondo 1469', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Mecan S.A', 5, 25, N'30707431411', NULL, N'Av. 12 de octubre 626/636', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Merino Herramientas', 5, 25, N'23273304339', NULL, N'Fuhr 1584', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Metalurgica Ari', 5, 25, N'20084442993', NULL, N'Colegiales 2406', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Metalurgica Jor-Cit', 5, 25, N'20149021087', NULL, N'Calle 51 e 23 y 24', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Microelectronica Componentes S.R.L', 5, 25, N'30702073657', NULL, N'Peron 1455', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Montone S.R.L', 5, 25, N'33712742009', NULL, N'Santo Domingo 1839', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Nivihe S.A', 5, 25, N'30709554049', NULL, N'Pereyra Lucena 1253', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Ogarayti de Victor M. Persico', 5, 25, N'20275362760', NULL, N'Hidalgo 1689', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Oleodinamica', 5, 25, N'30697710910', NULL, N'Av Eva Peron 1917', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES ('Olympic San Luis', 5, 25, N'306215179796', NULL, N'Carriego 940', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Onlive Belts co ltd Zhejiang', 5, 25, N'55000003211', NULL, N'CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Oxiargentina', 5, 25, N'20043064909', NULL, N'9 de julio 1058', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Padrani Aceros', 5, 25, N'20100798965', NULL, N'Calle 45 y Ruta 8', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Pentre S.A', 5, 25, N'30708037369', NULL, N'Sargento Cabral 2324', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Plastigomm', 5, 25, N'20303134809', NULL, N'Colon 769', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Plegamex S.R.L', 5, 25, N'33583193109', NULL, N'Ruta 8 km 280', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Poleas Mior S.A', 5, 25, N'30659388983', NULL, N'Ruta Ao-12 Y Z mARTINEZ', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Polimeros Indeplasts S.R.L', 5, 25, N'30710919492', NULL, N'De los poestas 2026', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Polyperfil de Hector A y G. Giesolauro', 5, 25, N'30665204509', NULL, N'Ruta 5 km 160', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Respuesto Hogar', 5, 25, N'23083659564', NULL, N'Calle 24 e 49 y 50', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Resortes lacas', 5, 25, N'20060653977', NULL, N'Av Francia 3849', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Rodinag', 5, 25, N'23085894099', NULL, N'Av Eva Peron 3095', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Rogiro Aceros S.A', 5, 25, N'30554668115', NULL, N'Limite del Municipio 4215', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Rovere Metales Puerto Norte S.R.L', 5, 25, N'30715170937', NULL, N'Monteagudo 347', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Ruedas Hofer', 5, 25, N'30707273530', NULL, N'H. Yrigoyen 860', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Salvador Palao S.A', 5, 25, N'33551703709', NULL, N'Diaz Velez 170', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Sc Cilindros', 5, 25, N'20232177110', NULL, N'Gavilan 770', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Seryven S.R.L', 5, 25, N'30690852485', NULL, N'Maipu 2212', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'Shulman Hnos S.A', 5, 25, N'30500756019', NULL, N'Hipolito  Yrigoyen', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Sichuan Migthy Machinery Co Ltd', 5, 25, N'55000003109', NULL, N'China', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Simba', 5, 25, N'30503037773', NULL, N'A del Valle 740', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Sin Par S.A', 5, 25, N'30500806695', NULL, N'Conesa 10', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Soluciones Y Embalajes S.A', 5, 25, N'30715818015', NULL, N'La Calandria 2202', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Taizhou jwd motor co, ltd', 5, 25, N'55000003105', NULL, N'No 98-3 Dongtaihe Road Zheijiang', NULL, NULL, NULL, 69, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'Tecneclima', 5, 25, N'20239529772', NULL, N'Olleros 4157', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Tempra S.A', 5, 25, N'30650189546', NULL, N'Av Prov. Unidas 3029', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Tianjin Ounaida Tarnsmissions Machinery Co Ltd', 5, 25, N'55000003103', NULL, N'No 3-10-5 Beifang Cheng', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Torneria Mecanica Epelde Hnos', 5, 25, N'30666781801', NULL, N'Calle 52 y Ruta 8', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Transcaden S.R.L', 5, 25, N'30677441123', NULL, N'Ov. Lagos 62', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Transformadores Saico', 5, 25, N'20112728245', NULL, N'L.N. Alem 2499', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'Tranmiciones Ges S.R.L', 5, 25, N'30711720436', NULL, N'Concordia 1052', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Troilo Carlos Omar', 5, 25, N'20115481712', NULL, N'Ruta 188 Km 110', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Tutti Rosca', 5, 25, N'20138062911', NULL, N'Vera Mujica 469', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Valeri Julio Antonio', 5, 25, N'20118413351', NULL, N'8 e 40 y 41', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Warbel', 5, 25, N'30599213631', NULL, N'Ruta 11 km 1006.5', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Wuxi Dekong Technology Co Ltd', 5, 25, N'55000003104', NULL, N'Room 516', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Yancheng Yuheng Electric Heating Technology Co Ltd', 5, 25, N'55000003101', NULL, N'No 58 South jiefang yancheng', NULL, NULL, NULL, 145, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Hoses Raul Alberto', 5, 35, N'333333333', NULL, N'Calle 46 y ruta Nº8', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Tecnome', 5, 25, N'33710172809', NULL, N'Gascón Nº1136', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Simplemak', 5, 25, N'30709715557', NULL, N'Calle 51, N° 1120', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Rulemanes center', 5, 25, N'30716578174', NULL, N'Calle 18, N° 670', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'BP Sociedad Anonima', 1, 25, N'30578654182', NULL, N'San Martin 1301', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ARMANDO TESSORE S.A.', 1, 25, N'30707831231', NULL, N'Bv. Alsina 74', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'E.R.H.S.A.', 1, 25, N'30506242548', NULL, N'GIRARDOT 1368', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'JCM', 1, 25, N'20102844751', NULL, N'COSQUIN 5555', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ELECTRO PERGAMINO', 1, 25, N'30707952462', NULL, N'PUEYRREDON 550', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'GARAY AGRO', 1, 25, N'30640866116', NULL, N'Calle 46 e/9 y 10', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'RODAMIENTOS COLON', 1, 25, N'20247894447', NULL, N'CALLE 47 Nº 860', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'TERMOPLASTICOS CHIVILCOY S.A.', 1, 25, N'30712039104', NULL, N'PARQUE INDUSTRIAL CHIVILCOY RUTA 5 KM 159', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'RAESA ARGENTINA', 1, 25, N'30685462121', NULL, N'RUTA 32 KM 5,8', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'PERFIL GOMA', 1, 25, N'30517031182', NULL, N'MISTER ROSS 418', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'CRIVELLO HNOS ', 1, 25, N'30707735577', NULL, N'48 Y 22', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'OXITECNICA ', 1, 25, N'23067223284', NULL, N'50 Nº1123', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'PYSEL ', 1, 25, N'20161871541', NULL, N'16 Nº 3338', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SOLDAR DISTRIBUCIONES DE GASES', 1, 25, N'30708813059', NULL, N'15 E/ 48 Y 49', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'OLYMPIC SAN LUIS ', 1, 25, N'30621517976', NULL, N'CARRIEGO 940', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'NICASTRO HNOS. Y GORDILLO', 1, 25, N'30607104596', NULL, N'MONTEVIDEO 5697', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'PRODUFLON', 1, 25, N'30640664599', NULL, N'ALEM 3247', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'VARITEL', 1, 25, N'30708222417', NULL, N'M. BALIÑA 465', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'RODASER RODAMIENTOS S.R.L.', 1, 25, N'30581183514', NULL, N'AV. RIVADAVIA 12480', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'TRANSFORMADORES LOMBARD', 1, 25, N'30644406934', NULL, N'MENDOZA 6426', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'E MOTORS S.R.L.', 1, 25, N'30709291927', NULL, N'PTE. LUIS SAENZ PEÑA 1839', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Servocem', 1, 25, N'20182527638', NULL, N'Hipolito Yrigoyen 212', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SERIKON GRAPHEIN', 1, 25, N'27028990729', NULL, N'HUMBERTO 1º 240', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'BANDAS INDUSTRIALES S.A.', 1, 25, N'30709459003', NULL, N'AV. GALICIA 1021', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ACPLIND de J. Feldman y M. Feldman SH', 1, 25, N'30505483509', NULL, N'LERMA 63', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'LINEARTEC SRL', 1, 25, N'30708337605', NULL, N'OMBU 3737', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ACESID S.A.', 1, 25, N'30630706064', NULL, N'JUAN B. JUSTO 7463', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ARGELEC S.A.', 1, 25, N'30520329273', NULL, N'EVA PERON 4242', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'NLI CHICAGO INC.', 5, 25, N'55000002126', NULL, N'1600 GOLF ROAD, SUITE 1213, ROLLING MEADOWS, ILLINOIS', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'ARGELEC CENTRO SRL', 1, 25, N'30711336911', NULL, N'AV. CORRIENTES 1178', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'JUAN CARLOS CANELO SRL', 1, 25, N'33519560009', NULL, N'AV 25 DE MAYO 571', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'BULONERA V', 1, 25, N'20051770049', NULL, N'RODOLFO LOPEZ 440', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ACEROS FB SRL', 1, 25, N'30714180475', NULL, N'MARCOS PAZ 869', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'LICHTMAIER OSVALDO M Y LITCHMAIER CINTIA SH', 1, 25, N'30702193865', NULL, N'PUEYREDON 934', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'LA INDUSTRIA ELÉCTRICA S.A', 1, 25, N'30645614735', NULL, N'MONTIEL 3060', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'VERACRUZ CLAUDIO ALBERTO', 6, 26, N'20177721876', NULL, N'DONIZETTI 348', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'TALLER BAIGORRIA S.A.', 1, 25, N'30709629901', NULL, N'SANTIAGO PARODI 3864', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SENDECO S.A.', 1, 25, N'30683684143', NULL, NULL, NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'DAMIANO EDUARDO MATIAS', 1, 25, N'20392488317', NULL, N'DIMMER 2155', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Rodrimat S.A', 1, 25, N'30611996809', NULL, N'Av. Los Quilmes 1372', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'FESTO S.A.  ROSARIO', 1, 25, N'30596013399', NULL, N'CORDOBA 3479', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SIMPLE MAK S.A.', 1, 25, N'30709715557', NULL, N'CALLE 51 ENTRE 23 Y 24', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'DELLE GRAZIE', 1, 25, N'30650542157', NULL, N'EMILIO MITRE 601', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'KADAE S.A.', 1, 25, N'30673850673', NULL, N'JUAN MANUEL DE ROSAS 176', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'AISLANTES SH SRL', 1, 25, N'30654325746', NULL, N'JUAN D. PERON 3513', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SEWING PARTS EXPRESS CORP', 5, 25, N'55000002126', NULL, N'9918 NW 41ST DORAL, FL 33178', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'FERRARI MARIO FERNANDO', 1, 25, N'20122055745', NULL, N'S. del Estero 2565-Lanus, Buenos Aires', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'NINGBO AE ELECTRIC CO. LTD', 5, 25, N'55000003106', NULL, N'XIKOU INDUSTRIAL ZONE, NINGBO, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'WENZHOU TAIMING LECTRICAL CO., LTD', 5, 25, N'55000003106', NULL, N'PAN SHI TOWN, BEIBAIXIANG TOWN, ZHEJIANG, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SHANGAI LANBAO SENSOR CO, LTD', 5, 25, N'55000003106', NULL, N'NO. 228 KINBI - SHANGAI - CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'TAIZHOU JWD MOTOR CO., LTD', 5, 25, N'55000003106', NULL, N'NO 98-3 DONGTAIHE ROAD - ZHEIJIANG', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'JIANGSU GOLD ELECTRICAL CONTROL TECHNOLOGY CO, LTD', 1, 25, N'55000003106', NULL, N'C BIULDING NO. 10, LIANHE ROAD, HUDAI INDUSTRIAL ZONE, XUVI, JIANGSU, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'INVT INTERNATIONAL TRADING LTD.', 5, 25, N'55000003106', NULL, N'SHENZHEN, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'AIRGAS USA, LLC', 5, 25, N'55000002126', NULL, N'DORAL, FL', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'YUEQING LEYU ELECTRIC AUTOMATION CO., LTD', 1, 25, N'55000003106', NULL, N'NO. 34 HUARONG ROAD, CHANXI VILLAGE, LUISHI TOWN, YUEQING CITY, ZHEJIANG PROVINCE, PR. CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'CHANGZHOU TIANHE BEARING TECHNOLOGY CO, LTD', 5, 25, N'55000003106', NULL, N'ROOM 1002, BUILDING2 YIKANG PLAZA, NO. 68, GUANHE MIDDLE ROAD, CHANGZHOU, JIANGSU, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'NEWLONG INDUSTRAL CO., LTD', 5, 25, N'55000003203', NULL, N'8-14 SHIRATORI - 4-CHORME. KATSUSHITA-KU -125-063. TOKYO. JAPON', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'PLASTICOS BUENAVENTURA', 1, 25, N'20135870030', NULL, N'AUT. ARAMBURU KM. 232,5', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'GOMA PAZ SRL', 1, 25, N'30707828079', NULL, N'AV. ARIJON 2256', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'MESTRE RUBÉN', 3, 25, N'00000000010', NULL, NULL, NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SOLDADURA', 2, 25, N'00000000020', NULL, NULL, NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'CIXI ALPHA BEARING CO LTD', 1, 25, N'55000003106', NULL, N'SHUINAN ROAD SOUTH CENTER BUILDING ROOM 121 CIXI CITY NINGBO CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'WUXI DEKONG TECHNOLOGY CO., LTD', 5, 25, N'55000003106', NULL, N'ROOM 516, JUANCHUANG BUILDING NO. 888, HONGQIAO, WUXI, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'CORTADORAS ARGENTINAS S.A.', 1, 25, N'30715056220', NULL, N'EMILIO MITRE 601', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'NINGBO PNEUBIG PNEUMATIC CO. LTD', 5, 25, N'55000003106', NULL, N'XIKOU INDUSTRIAL ZONE, FENGHUA, NINGBO, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'KUCCO STORE', 5, 25, N'55000003106', NULL, N'ALIBABA CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ASIA DRAGON ELECTRIC CO, LTD.', 5, 25, N'55000003106', NULL, N'NO.7ROAD TENFEI, LIUSHI DONGFENG IND.Z, ZHEJIANG, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'YUEQING XIANGLANG ELECTRIC CO. LTD', 5, 25, N'55000003106', NULL, N'NO. 299, LOGJING ROAD, LIUSSHI. WHENZHOU, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ONLIVE BELTS CO LTD ZHEJIANG', 5, 25, N'55000003106', NULL, N'11 CHAOYANG ROAD, HAIYOU TOWN, TAIZHOU, ZHEJIANG, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'LINQING YUANYI BEARING CO, LIMITED', 5, 25, N'55000003106', NULL, N'PANNAN VILLAGE, PANZHUANG TOWN, LINQING CITY', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'YANCHENG YUHENG ELECTRIC HEATING TECHNOLOGY CO LTD', 5, 25, N'55000003106', NULL, N'NO.58, SOUTH JIEFANG YANCHENG, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'HAO YU PRECISION MACHINERY INDUSTRY CO, LTD', 5, 25, N'55000003106', NULL, N'NO 290, HAI WEI RD, LOND DISTR, TAIWAN', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SICHUAN MIGHTY MACHINERY CO LTD', 1, 25, N'55000003106', NULL, N'ADD2, TONGFUXIANG, XIYULONG ST, SICHUAN, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'CHANGZHOU DAGUI INTERNATIONAL TRADE CO.', 5, 25, N'55000003106', NULL, N'MOUJIA VILLAGE, ZHENGLU TOWN, CHANGZHOU CITY, KIANGSU PROVICE, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn])
VALUES (N'KEESTAR INDUSTRIES CO, LTD', 5, 25, N'55000003106', NULL, N'ROOM 505, TOWER FL, SHANNXI, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SHENZHEN CAFORRE TECHNOLOGY CO LTD', 5, 25, N'55000003106', NULL, N'2/F BUILDING 8 MAWU INDUSTRIAL PARK, SHENZHEN CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'LISHUI WANGONG PRECISION MACHINERY CO LTD.', 5, 25, N'55000003106', NULL, N'892 TIANNING ROAD, TIANNING IND ZONE LISHUI CITY, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'SHENZHOU HUAMIN MACHINERY CO.', 5, 25, N'55000003106', NULL, N'CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'TORNERIA HOSES', 1, 25, N'20303126652', NULL, N'46 S/N', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'DONGGUAN BOKE PRECISE MOLDING TECHNOLOGY CO LTD', 5, 25, N'55000003106', NULL, N'FENGSHEN INDUSTRIAL ZONE, CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'DE CAI HONORISE NEW MATERIAL CO LTD', 5, 25, N'55000003106', NULL, N'NO1 OF HONGYE ROAD, SECOND INDUSTRIAL PARK, ZHEJIANG', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'JINAN HUACHEN INDUSTRIAL CO LTD', 5, 25, N'55000003106', NULL, N'XINCHENG BD 2-603 JINAN SAHNDONG', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'TAIZHOU NICE IMPORT AND EXPORT CO LTD', 5, 25, N'55000003106', NULL, N'TAIZHOU', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'JIANGYIN CHIXU INDUSTRIAL TECHNOLOGY CO LTD', 5, 25, N'55000003106', NULL, N'JIANGYIN CITY', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'HONGKONG XINGHE INTERNATIONAL TRADE CO.', 5, 25, N'55000003106', NULL, N'CHANGZHOU CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'KRAUS MADERAS', 1, 25, N'30715654853', NULL, N'ADA. CENTENARIO 831', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'HANGZHOU QIANXI INC', 5, 25, N'55000003106', NULL, N'CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'NINGBO CENTURY SHIEN INTERNATIONAL CO LTD', 5, 25, N'55000003106', NULL, N'LOUXIACHEN NINGBO CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'KATRIEL SRL', 1, 25, N'30682333347', NULL, N'CARLOS CALVO 2152', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'GUANGZHOU GOODLY INDUSTRY BELT CO LTD', 5, 25, N'55000003106', NULL, N'GUANGZHOU-CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ZHEIJIANG WUMA REDUCER CO LTD', 5, 25, N'55000003106', NULL, N'ZHEIJIANG CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'ZHEIJIANG NINOU ELECTRIC CO LTD', 5, 25, N'55000003106', NULL, N'ZHEIJIANG CHINA', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'BELLI NESTOR DANIEL', 1, 25, N'20177397203', NULL, N'AVENIDA SAN MARTIN 1026', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'REPUESTHOGAR SANITARIOS S.A.', 1, 25, N'30714162280', NULL, N'24 N° 772', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'RODRIMAT S.A.', 1, 25, N'30611996809', NULL, N'Av. Los quilmes 1372', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Sidinox S.R.L.', 1, 25, N'30677504702', NULL, N'Virasoro 1660, S2001ODJ Rosario, Santa Fe', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'S.I.M.E. ARGENTINA', 1, 25, N'30701136884', NULL, N'Av San Martín 3427 - Caseros', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'DEBERNARDI REPUESTOS', 1, 25, N'20118413262', NULL, N'17 E / 47 Y 48', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Industria Bulonera Argentina S.A.', 1, 25, N'33628782259', NULL, NULL, NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Providers] ([BusinessName], [IVAConditionId], [DocumentTypeId], [DocumentNumber], [GrossIncome], [Address], [CityId], [PostalCode], [StateId], [CountryId], [WebPage], [IsActive], [ConcurrencyToken], [CreatedBy], [CreatedOn], [LastModifiedBy], [LastModifiedOn]) 
VALUES (N'Tubos Argentinos', 1, 25, N'30622862049', NULL, N'Marcos Sastre 698 (5Ruta 197 y Panamericana)', NULL, NULL, NULL, 61, NULL, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO