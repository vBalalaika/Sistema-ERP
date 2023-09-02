USE [SystemERP_DB]
GO

/*

- Si se quiere borrar la información de la tabla y reiniciar la primary key ejecutar lo siguiente:

USE [SystemERP_DB]
GO
DELETE FROM Clients
GO
DBCC CHECKIDENT (Clients, RESEED,0)
GO

*/

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Agrosaco Ind. & Com.', NULL, NULL, N'C. S/NOMBRE # 399 CARR. A COTOCA KM. 4 ½ BARRIO MTE CRISTO ESTE', NULL, NULL, NULL, 63, N'Olmy Cortes', N'591 (3) 3484112', NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Embolpack', NULL, NULL, N'6to Anillo entre Av. Virgen de Luján y Av. Mutualista', NULL, NULL, NULL, 63, N'Eduardo Paz', N'00 591 33493601/3591', N'epaz@live.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Textil Y Envases Industriales Bolivianos Teisa', NULL, NULL, N'Av. 16 de Julio, Edif. San Pablo piso 9 Of. 907 ', NULL, NULL , NULL, 63, N'Ing. Paulo Parrado', N'(591) 22310247 22310277 ', NULL, NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Inbolsa Ind.boliviana De Sacos', NULL, NULL, N'Carretera a Viacha s/n (frente a SWISSPORT)', NULL, NULL, NULL, 63, N'Pedro Rivero Quesada', N'(591)22831662  2831681', N'info@inbolsa.net', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Monterossa S.r.l.', NULL, NULL, N'Parque Industrial: Vía Diagonal 4 P.I. 36-11', NULL, NULL, NULL, 63, NULL, N'(591) (33) 3468131', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Textil San Jorge Isj', NULL, NULL, N'Parque Industrial Mz. 40D. SANTA CRUZ DE LA SIERRA', NULL, NULL, NULL, 63, N'Verónica Barba(compras)', N'591 33492016/17', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Sitexpo Soc.ind.textil Potosi', NULL, NULL, N'Dirección: Carretera al Norte Km. 27 Santa Cruz de la Sierra', NULL, NULL, NULL, 63, N'Marco Antonio Cueto Garcia ', NULL, NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Bolsas Kantuta', NULL, NULL, N'Calle Magallanes 1530 Cochabamaba Bolivia', NULL, NULL, NULL, 63, N'Jaime Cáceres', N'00591 79959054', N'Jcaceres812@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Luvebol', NULL, NULL, N'Mallco Rancho', NULL, NULL, NULL, 63, N'Roger Vega', N'+591 76480801', N'luvebol@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sociedad Boliviana De Cemento', NULL, NULL, N'Avenida Arce, esq. plaza Isabel La Católica No, 2519', NULL, NULL, NULL, 63, N'Juan Carlos Auza', N'591-77799126', N'jcauza@soboce.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Maxibags', NULL, NULL, NULL, NULL, NULL, NULL, 63, N'Hugo Dalence', N'00591 71348201', N'hdalence@maxibags.com.bo', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'B&h Export & Import Ltda', NULL, NULL, NULL, NULL, NULL, NULL, 64, N'Diego Bues', NULL, N'diego@bhcomex.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agilbag Containers E Embalagens Flexiveis Ltda.', NULL, NULL, NULL, NULL, NULL, NULL, 64, N'Rodrigo Ruiz', NULL, N'rodrigo@agilbag.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bagline', NULL, NULL, NULL, NULL, NULL, NULL, 64, N'Nilton Araujo', NULL, N'nilton.bagline@terra.com.br', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Girocafe', NULL, NULL, NULL, NULL, NULL, NULL, 64, N'Jose Antonio', NULL, N'girocafe@girocafe.net', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sergio Porn', NULL, NULL, NULL, NULL, NULL, NULL, 64, NULL, NULL, N'sergiohsdembalagens@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Aloin sl', NULL, NULL, N'Loa Fresnos#600 BODEGA 4 KL.18', NULL, NULL, NULL, 69, N'Marcelo Vega', N'56 2 738 7464', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Aramis Ltda', NULL, NULL, N'Calle nueva N° 763 Hijuelas', NULL, NULL, NULL, 69, N'Loreto Vargas', N'+56 33 2273397', N'lvargas@aramisltda.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bag Chile', NULL, NULL, NULL, NULL, NULL, NULL, 69, N'Miguel Angel Vergara', N'+569 92339059', N'Miguel.vergara@bagchile.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bigbag Trading', NULL, NULL, N'San Isidro 471', NULL, NULL, NULL, 69, N'SANTIAGO', N'56 2 2222 4523/ 2635 2416', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bonsucesso Textil', NULL, NULL, N'Alonso de Cordoba 5870 of. 1423', NULL, NULL, NULL, 69, NULL, N'56 9 8972 6006', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Cargoflex E.i.r.l.', NULL, NULL, N'RIVAS 540', NULL, NULL, NULL, 69, N'Aldo Targarona/Andrés Pantoja', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Cemte S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 69, N'Carlos Quintana', N'+56 45-2923770', N'cacoquintana@gmail.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Chilesacos', NULL, NULL, N'O''Higgins 265, P. Industrial Las Arucas, Talcahuano', NULL, NULL, NULL, 69, N'Cristian Cataldo', N'(+56) 41 2929 550 ', N'contacto@chilesacos.com ', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Coisa S.a.', NULL, NULL, N'Eosales 1619', NULL, NULL, NULL, 69, N'Ivan Iñiguez', N'56 2 521 5549', N'miniguez@coisa.cl', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Comercial NeW-Bag Chile Spa', NULL, NULL, N'Jose Miguel Carrera 13805', NULL, NULL, NULL, 69, N'Jose Pascal Perez', N'56 9 5169 7186', N'jpascal@newbag.cl', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Coresa sa', NULL, NULL, N'San Nicolas 630', NULL, NULL, NULL, 69, N'José Rojas', NULL, N'gmesa@coresa.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Emmain', NULL, NULL, N'Rinconada 312', NULL, NULL, NULL, 69, N'Jose Veliz Moreno', N'56 9 96797571', N'jose.veliz@emmain.cl', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Experto Bags', NULL, NULL, N'Teniente Ponce 1622', NULL, NULL, NULL, 69, N'Erik Tobar', N'56 9 8520 9711', N'etovar@expertobags.cl', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Fabrisac', NULL, NULL, N'Puerta de Vera 1033 Santiago, Región Metropolitana', NULL, NULL, NULL, 69, N'Vicente Franco', N'0056  (2) 2683 1511/7451/6793', N'ventas@fabrisac.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Greif Chile', NULL, NULL, N'Camino de la Boza 395 Pudahuel', NULL, NULL, NULL, 69, N'Federico Toscani', N'56 2 2594 3400', N'federico.toscani@greif.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Interbins Ltda.', NULL, NULL, N'Avda. Las Torres 90, Megacentro, Rancagua', NULL, NULL, NULL, 69, N'Manuel Fuenzalida', N'+56 9 64564030', N'manuel.fuenzalida@interbins.cl ', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Labbe Comercializadora', NULL, NULL, NULL, NULL, NULL, NULL, 69, N'Josué Díaz', NULL, N'comercializadoralabbe@gmail.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Marienberg', NULL, NULL, N'Santa Marta 1600 Maipú Santiago Chile', NULL, NULL, NULL, 69, NULL, N'0056 22 947 4000', N'info@marienberg.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maxibag', NULL, NULL, N' Parcela 11 San Juan Pto. San Antonio', NULL, NULL, NULL, 69, N'Juan Ignacio Manzo', N'+56 (09) 82338278 - 035-2-370335', N'jmanzo@maxibag.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maxisacos Yireh', NULL, NULL, N'Llico 382, Santiago, San Joaquín, Región Metropolitana, Chile', NULL, NULL, NULL, 69, N'Katherine Donoso', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Maxisalcon', NULL, NULL, N'Departamental #999 B11', NULL, NULL, NULL, 69, N'Patricio Saldias', NULL, N'patricio,saldias@maxisalcon.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maxisur Ltda', NULL, NULL, N'Ruta 5 sur k652 calle servicio Pillanlelbun', NULL, NULL, NULL, 69, N'Cristian Pichara Morales', N'+569 9847 7715', N'christian.pichara@gmail.com>', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Plusbag', NULL, NULL, N'Av La Paz 724', NULL, NULL, NULL, 69, N'Roberto Galvez', N'56 72 239 7540', N'rgalvez@plusbag.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polisak', NULL, NULL, N'San Miguel, Región Metropolitana, Chile', NULL, NULL, NULL, 69, N'Julio Baeza', N'0056 2 32280054', N'jbaeza@polisak.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Probotem', NULL, NULL, N'Lote 2, Km. 26, Metrenco, Padre Las Casas, Temuco', NULL, NULL, NULL, 69, N'Gustavo Pincheira', N'56 452 920370', N'contacto@probotem.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Prodarco', NULL, NULL, N'asistente@prodarco.cl', NULL, NULL, NULL, 69, N'Gabriel Durán', N'22 823 0044/22 918 0077/56993591650/223185038', N'asistente@prodarco.cl', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Proenvases Sacos Paperos', NULL, NULL, N'Pasaje 3A Bocasur SAN PEDRO DE LA PAZ', NULL, NULL, NULL, 69, N'Eduardo Alarcon', N'56-41-2282673 (0)99993563 (0)77660553', NULL, NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Prosacos', NULL, NULL, N'Isabelina 20', NULL, NULL, NULL, 69, N'Eduardo', N'56 41 2282 673', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Reicotex Tecnologia sl', NULL, NULL, N'San Isidro 1782. Santiago - Chile', NULL, NULL, NULL, 69, N'Marco Carvacho', N'56 2 551 1780', N'marcocarvacho@reicotex.cl', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Super Sacos Compakta', NULL, NULL, N'Muñoz Gamero 1012 B, Recoleta, Santiago,', NULL, NULL, NULL, 69, N'Fernando Lagos', N'0056 951987245', N'fernandolagos@compakta.cl', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Tecnocoat', NULL, NULL, N'Dr Manuel Barros Borgoño 71 of. 1105, Providencia, Santiago', NULL, NULL, NULL, 69, N'Carlos Lopez', N'+56 2 3248 1618', N'carlos.lopez@tecnocoatchile.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Yanber', NULL, NULL, NULL, NULL, NULL, NULL, 67, NULL, NULL, NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sajiplast', NULL, NULL, NULL, NULL, NULL, NULL, 67, N'Dagoberto Serrano', N'(506) 2293-0115/ 2293-0182/ 2293-4705', N'compras@sajiplast.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Plasticos Modernos', NULL, NULL, NULL, NULL, NULL, NULL, 67, NULL, NULL, NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Fideca San Cristobal', NULL, NULL, NULL, NULL, NULL, NULL, 67, N'Juan Bautista Robles Vega', N'+506 2286-0919', N'marieth.segura@sancristobal.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Rafytica', NULL, NULL, NULL, NULL, NULL, NULL, 67, N'Guillermo Salas', N'(506) 24384305', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Costa Rica', NULL, NULL, NULL, NULL, NULL, NULL, 67, N'Gabriela Miranda', N'00 506 87127904', NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ciplas', NULL, NULL, N'Calle 11 # 60 - 03 Bogotá Colombia', NULL, NULL, NULL, 66, N'Ricardo Zubiria', N'+57 (1) 417 2222', N'ricardo.zubiria@ciplas.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Compania De Empaques', NULL, NULL, NULL, NULL, NULL, NULL, 66, NULL, N'(574) 365 8888', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Construplast', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'León Ballesteros', N'0057 314 7017272', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Drummont Ltd', NULL, NULL, N'Calle 72 No.10-07, Of.1302 Bogotá Colombia', NULL, NULL, NULL, 66, N'Marco Pérez', N'+57 300 6310372', N'mperez.cont@drummondltd.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Empaques Juan S.a.s.', NULL, NULL, N'Cl 85 48-01 Central Mayorista B-12 L-13 Itagüí Antioquia Colombia', NULL, NULL, NULL, 66, N'Juan Ignacio Toro', N'0057 4 4444998', N'gerencia@empaquesjuan.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ensacar Colombia', NULL, NULL, N'Cra. 44 No 7 - 119', NULL, NULL, NULL, 66, N'Carlos Plata', N'+57 (5) 3228000', NULL, NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ferreteria Arcangel', NULL, NULL,  N'Cra 45ª No 134d – 53 Bogotá Colombia', NULL, NULL, NULL, 66, N'Rafael Rodríguez Reyes', N'Cel: 310-8095846', N'informacion@ferreteriaarcangel.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Fibtex', NULL, NULL, N'Ac. 100 #8a-49 Torre B of 602 World Trade Center BOGOTÁ', NULL, NULL, NULL, 66, N'Sergio Campuzano', N'0057 (314)357-8746', N'gerencia@fibtexcolombia.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Hiecol', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'Alexander Perez', N'+57 313 5626853', NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Industrias Kent Y Sorrento S.a.', NULL, NULL, N'Cra 52 No. 27A-55 | Medellín', NULL, NULL, NULL, 66, N'Fernando Contreras', N'(574) 604 43 42 ', NULL, NULL, 3, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Iqa Textil', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'Iber Pardo', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Luis Hernando Molina Navas', NULL, NULL, NULL, NULL, NULL, NULL, 66, NULL, N'0057 3178867472', N'luishernandomolina@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Manufactura S.a.s', NULL, NULL, N'Calle 46 # 41- 69 Bodega A43 Complejo ind Amtex  Itagui Antioquia, ', NULL, NULL, NULL, 66, NULL, N'(574) 3777761', NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mendoza Migual Angel', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'Miguel Angel Mendoza ', N'+57 310 4805041', N'miguelangel_2027@hotmail.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Mersatex', NULL, NULL, N'CARRERA 22 32 38 AVENIDA LOS MOLINOS Dosquebradas', NULL, NULL, NULL, 66, N'Luis Fernando Pasos', N'0057 63224837', N'produccion@mersatex.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Pavco', NULL, NULL, NULL, NULL, NULL, NULL, 66, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Produbag', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'Juan Gonzalo Gimenez', NULL, N'gerencia@produbag.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Sacos Y Varios S.a.s', NULL, NULL, N'Cr 50 99 S-119 Piso 2 La Tablaza Antioquia', NULL, NULL, NULL, 66, N'Diego Quintero', N'0057 43383472/714', N'sacosvarios@yahoo.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sadecol', NULL, NULL, N'Santiago de Cali, Colombia', NULL, NULL, NULL, 66, NULL, N'+57 320 632 0213', N'sadecol@sadecol.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Super Empaques Colombia', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'Juan Carlos Lopez', N'0057 3012973752', N'juancarloslopezz@aoutlook.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Batallas Malena', NULL, NULL, NULL, NULL, NULL, NULL, 71, NULL, N'593 0984433017', N'malena_batallas@yahoo.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Caboplast Ecuador', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Esteban Villamar', N'(04)2421783', N'info@caboplast.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Corpolymsa', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Alex García', NULL, N'agarcia@corpolymsa.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Hbgh', NULL, NULL, NULL, NULL, NULL, NULL, 71, NULL, N' (593) 2 2957932', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ecosacks', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Ruben Herrera', N'(593) 0994644844', N'erherrera171@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Flexlabel', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Ronald Figueroa Leon', N'(+593) 4 - 205 - 5857', N'flexlabel@gmail.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Imfadaza S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Angel Astudillo', N'+593 0998439754', N'imdaza814@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Nutec Recycle', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'HUGO ZAVALA', NULL, NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Plasticksacks Cia Ltda.', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Héctor Parra', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Duran Reysac S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 71, NULL, NULL, N'xavierduran@reysac.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Gallardo', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Wilson Gallardo', N'Movil  593 999485221/ Fijo 593 2 2573520', N'gallardo_wilson@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Rafiatex', NULL, NULL, N'Carcelén Avenida Juan de Selis Oe1-25 y Vicente Duque', NULL, NULL, NULL, 71, N'Samir Kami', N'00593 (02) 247-0844', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ecuasacos', NULL, NULL, N'Lotización Inmaconsa entre higuerillas y quinquellas. Km 10/5 Vía a Daule', NULL, NULL, NULL, 71, N'Pamela WS', N'(593) 4 2113273 –  2113421 2113391', N'ventas@ecuasacos.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sasicasa El Salvador', NULL, NULL, NULL, NULL, NULL, NULL, 72, N'Armando Bolaños', N'(503) 2524-5500', N'exportaciones@sasicasa.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agave', NULL, NULL, NULL, NULL, NULL, NULL, 72, N'Ing. José Luis Paredes', N'00 503 25081100', N'gerencia.agave@borgonovo.com.sv', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Agroindustriales', NULL, NULL, NULL, NULL, NULL, NULL, 74, N'Luis Chang', NULL, N'ventas@sacos.com.gt', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polyproductos De Guatemala', NULL, NULL, NULL, NULL, NULL, NULL, 74, N'Michael Spencer ', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Rogelio Leon', NULL, NULL, N'Calle las Violetas, Jardines de San Lucas IV, GT', NULL, NULL, NULL, 74, NULL, N'00502 78308732 / 3329', N'rleon@multilazos.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Tecnifibras Group', NULL, NULL, N'13 calle 12-50 Lomas del Norte, Zona 17 GT', NULL, NULL, NULL, 74, N'Ricardo Santos', N'+502 2429-1700', N'rsantos@tecnifibras.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Plastinova', NULL, NULL, NULL, NULL, NULL, NULL, 77, N'Giancarlo Casco', NULL, N'giancarlo.casco@plastinova.hn', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Agroindustriales De Honduras', NULL, NULL, NULL, NULL, NULL, NULL, 77, N'Sandra Estrada', N' (504) 25519846', N'sestrada@sacoshn.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Abstecedora Nacional De Sacos', NULL, NULL, N'Nezahualcóyotl,', NULL, NULL, NULL, 79, N'Alicia Fernandez', NULL, N'abdesacos@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'All In Plastic', NULL, NULL, N'Zapopan', NULL, NULL, NULL, 79, N'Mario Navarrete Ocaña', N'0052-33 31620756', N'mnavarrete@prodigy.net.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Arpysa Del Noroeste', NULL, NULL, N'Los Mochis', NULL, NULL, NULL, 79, N'Arpysa del Noroeste', N'+52 668 818 8075', N'arpysa@msn.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bagcorp mx', NULL, NULL, N'USA', NULL, NULL, NULL, 79, N'José Sánchez - Dyrle (Gerente)', N'0052 6566371623', N'jsanchez@bagcorp.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bodega De Costales', NULL, NULL, N'Guadalupe', NULL, NULL, NULL, 79, N'Jorge Medelín', N'+52 (81) 8379-9095', N'reymex@hotmail.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Centro Costalero Universal', NULL, NULL, N'Guadalajara', NULL, NULL, NULL, 79, N'Sarai Rentería ', N'0052 3336712515/3336956683', N'ccucostales@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Clean Bag S.a.', NULL, NULL, N'Veracruz', NULL, NULL, NULL, 79, N'Francisco Zorrilla', N'0052 229 981 1613', NULL, NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Gonfer Costales Industriales', NULL, NULL, N'Santiago de Querétaro', NULL, NULL, NULL, 79, N'Gerardo Gonfer', N'0052 4271102942', N'gerardogonfer16@gmail.com', NULL, 3, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Conitex Sonoco', NULL, NULL, N'Eje Oriente Poniente Mz III Lt8 Tizayuca Hidalgo', NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Corcimex/textiles Santa Susana', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Daniel García', N'1,800,267,2463', N'daniel.garcia@corcimex.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Cordelmex', NULL, NULL, N'Adolfo Gurrión No. 135 Col. Centro DF', NULL, NULL, NULL, 79, N'Sandra Muñoz', N'0052 55 55420968', N'jarcieria_atlas@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Costal Y Mas', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Alan', N'0052-55-5600-7581', N'costalymas@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costalera La Ilusion', NULL, NULL, N'Jalisco', NULL, NULL, NULL, 79, N'Mary Carmen', N'(0052) (33) 3811 3120', N'costaleralailusion@gmail.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Bodgar', NULL, NULL, N'Av Canal de Apatlaco Mz. 21 Lt. CALLE. MANUEL GONZALEZ MANZANA 10 LOTE 7 Y 8\nCOLONIA: DARIO MARTINEZ 1RA SECCION. VALLE DE CHALCO ESTADO DE MEXICO', NULL, NULL, NULL, 79, N'Daniel García', N' (55) 1643 2057', N'costbodgar@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Costales Del Valle', NULL, NULL, N'Guadalupana', NULL, NULL, NULL, 79, N'David García - Producción', N'0052-55-65857621', N'costalesdelvalle@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales El Contador', NULL, NULL, N'AV CIRCUNVALACION 32 UNIDAD HABITACIONAL BARRIENTOS, TLALNEPANTLA DE BAZ, MEX 54140', NULL, NULL, NULL, 79, N'Rodrigo', N'0052(55)5311-2235', N'jorge.costales.ventas@live.com; jorge_alberto78@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Grumesa', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Ing. Luz María', N'0052-55-5600 3386', N'ventas@costalesgrumesa.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Industriales Pereira Sa De cv', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Esteban/Alejandro', N'0052(55)1675-3765', N'costalesindustrialespereira@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Mendieta', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Julio César López', N'0052 5548469098', NULL, NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Plasticos De Queretaro', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Fernando', N'01 44223512 CEL:4456609/4425598018/5548615035', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Policrisan', NULL, NULL, N'Puebla', NULL, NULL, NULL, 79, N'Cristian', N'0052(55) 5111-6384', N'ventas@policrisan.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Puebla', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Anastasio Ramirez', N' 0052 238 383 6795', N'ventas@costalespuebla.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales San Miguel', NULL, NULL, N'Plutarco Elias Calles, Militar Marte Delegación Iztacalco', NULL, NULL, NULL, 79, NULL, N'(55) 56746319 (55) 44370907', N'costalessanmiguel@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Tapatios', NULL, NULL, N'Guadalajara', NULL, NULL, NULL, 79, N'Abigaí Ramirez', N'0052(33) 3608 0392', N'compras@costalestapatios.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costales Y Rafias Agro Industriales De Occidente', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, N' +52 33 3671 3797', NULL, NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costalym Sa De cv', NULL, NULL, N'Cuernavaca, Morelos.', NULL, NULL, NULL, 79, N'Cdor. Arturo Casillas', N'0052(777) 100 15 05', N'costalym@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Costgar & Containers', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Rodolfo García', N'0052(55)2619-3547', N'costgar@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Coverbag', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Arturo Gallardo', N'00 52 8116940118', N'ventas@coverbag.org', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Distribuciones La Bolsa De cv', NULL, NULL, N'Sinaloa', NULL, NULL, NULL, 79, NULL, NULL, N'ricardo@labolsamx.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Dust Control', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Victoria Gonzales', N'(55)50914560', N'cotizaciones@dustcontrol.com.mx', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Edc Costales = Costales Express', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Teresa Aguallo', N'0052 133 1614 8831', N'edccostales@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Elway Distribuidora', NULL, NULL, N'San Juan del Rio, Estado de Queretaro en  Mexico DF', NULL, NULL, NULL, 79, N'Licenciado  Antonio Becerril', N'0052-4271011049/(427)1128845 Tel- 01-800-009-5929, 427-101-1049, 427 129-0484, ext 102.', N'compras@delway.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ensacar Mexico', NULL, NULL, N'San Luís Potosí', NULL, NULL, NULL, 79, N'Ing. Juan Pablo Martinez Carlos Morlet', N'(+52)4893881749', N'info@ensacarmexico.com; j.martinez@ensacarmexico.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ensacar S.a. De C.v.', NULL, NULL, N'Calle Tanuil #100 COL.Pueblo Nuevo - San Luis Potosì', NULL, NULL, NULL, 79, NULL, N'+52 (489) 388.1749', N'auxiliar.compras@ensacarmexico.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envases Agricolas', NULL, NULL, N'Santiago Tianguistenco', NULL, NULL, NULL, 79, N'Ing. Donato Quesada-Ing. Víctor Garcia.', N'0052 17131330806/89/91', N'ventas@envasesagricolas.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envases Agro Industriales/disagro', NULL, NULL, N'Puebla', NULL, NULL, NULL, 79, NULL, N'(+52)282 825 1678; +52-01-2222251407', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Fabtex', NULL, NULL, N'Xalapa', NULL, NULL, NULL, 79, N'Jesus Lopez', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Flexisac', NULL, NULL, N'Guadalajara', NULL, NULL, NULL, 79, N'Lic. Lorena Miranda', N'0052 33 3811 1402', N'gerencia@sacosyempaques.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Flores Osvaldo', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, N'losvaldo.frodriguez@gmail.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Grupo Ortiz', NULL, NULL, N'Morelia', NULL, NULL, NULL, 79, N'Cristina Espinosa', N'(+52)443-3876352', N'info.go@grupo-ortiz.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Gurpa', NULL, NULL, N'Leon', NULL, NULL, NULL, 79, N'Valeria/Joaquin', N'(+52) 477-770-8044 / 45', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ing. Jose Antonio Mackintosh', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, N'0052-33-3377-7034', NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Innovapack', NULL, NULL, N'Cuernavaca', NULL, NULL, NULL, 79, NULL, N'0052-777-3228790/6820', N'compras@innovapack.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Internacional De Sacos Y Arpillas', NULL, NULL, N'Morelia', NULL, NULL, NULL, 79, N'Alicia Negrete', N'0052-4433274753', N'alicia.negrete@grupo-ortiz.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Interplas Mexico', NULL, NULL, N'Querétaro', NULL, NULL, NULL, 79, N'Miguel Angel Vidrio', N'(+52) 4272712317', N'm.vidrio@interplasmexico.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Jarcipack', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Jarcipack', N'0052-55-54911734', N'ventas@jarcipack.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Jose Eduwides Guzman Dias', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, N'eduwguzman@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Jumbocel/jumbopack', NULL, NULL, N'Carretera a Santa Rosa Km 35,2, Apodaca, Mexico', NULL, NULL, NULL, 79, N'Juan Lopez/Hipolito Gonzales', N'(+52)8182211028 / 00528182211000 (ext:110)', N'jmorales@grupojumbopack.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Juvi Containers', NULL, NULL, N'San Luís Potosí', NULL, NULL, NULL, 79, N'Juan Jesus Villagomez', N'0052-(444)818-3357', N'juvi444@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'La Costalera', NULL, NULL, N'MONTERREY', NULL, NULL, NULL, 79, N'La costalera', N'+52(81) 8359-9595', N'contacto@lacostalera.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'La Esperanza', NULL, NULL, N'Guadalajara', NULL, NULL, NULL, 79, N'Hector Manuel', N'(33)3671-0461', N'ramirezromerohectormanuel@yahoo.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Langstonbag', NULL, NULL, N'Apodaca', NULL, NULL, NULL, 79, N'Juan Jose/Jose Carlos Madina', N'0052(01-55) 55390570 /0052-8188640000', N'armandoarcos@langstonbag.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Lecai', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Manuel Birchez', N'(+52)-55-52511872', N'vivem87@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Lecherosunidos La Gloria Sa De cv', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Héctor Soto', N'373 735 1522', N'lecherosunidosgloria@telmexmail.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Los Costales', NULL, NULL, N'Tlajamulco', NULL, NULL, NULL, 79, N'Rafael Velazco Gonzales', N'33-38040554', N'costalesderafia@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mexinex', NULL, NULL, N'Atalmpa', NULL, NULL, NULL, 79, N'Bella Velazquez', N'(55)5547-4496 / (55)5547-0202 / (55)5547-1592', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mexplast', NULL, NULL, N'Clavijero #90 Col. Tránsito  CDMX', NULL, NULL, NULL, 79, NULL, N'00 52 5515020609', N'compras@mexplast.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mfsein Sa De cv', NULL, NULL, N'Ahualulco/San Luís Potosí', NULL, NULL, NULL, 79, N'Jose Luis Anguiano Mendieta', N'(52) 444 854-81-06', N'mfsein@msi-slp.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mulox De Mexico', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Jose Muñoz Gte.General Lic. Norma Rodríguez compras', N'0052 488-882-4032/4067', N'norma.rodriguez@greif.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Murtraleon', NULL, NULL, N'Leon', NULL, NULL, NULL, 79, N'Rosario de la Mora', N'(+52)4777612054', N'rdelamora@murtraleon.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Pesa Sacos De Polipropileno Sa De cv', NULL, NULL, N'Guadalajara', NULL, NULL, NULL, 79, N'Antonio Alfaro', N'0052(33) 3656 4456 ext.136', N'pesasacos@pesasacos.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Plasticostales', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Hector Galindo', N'0052(55) 57 00 25 99', N'ventas1@plasticostales.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polinal', NULL, NULL, N'San Luís Potosí', NULL, NULL, NULL, 79, N'Verónica Ramirez Reyes', N' 0052(444) 824-6990', N'ventas@polinal.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Poly Rafia S.a.', NULL, NULL, N'Atlacomulco.', NULL, NULL, NULL, 79, N'Ing. Benjamin Del Arco, Juan Carlo Lòpez.', N'0052 17121229920', N'jclopez@polyrafia.com.mx; bdelarco@polyrafia.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polycostales Industriales Sa De cv', NULL, NULL, N'Justo Sierra', NULL, NULL, NULL, 79, N'David Servidor', N'0052 1-55-15 42 08 37//52 92 73 67', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polysnop', NULL, NULL, N'Tultitlan', NULL, NULL, NULL, 79, N'Ing. Claudio Pons', N'+52(55)5203-8787', N'claudio@rafia.com.mx', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Procoydiplastic S De Rl De cv', NULL, NULL, N'San Juan del Río, Qro. C.P. 76800', NULL, NULL, NULL, 79, N'Jesus Vega Otero', N'0052 4272727205', N'vegaotero@prodigy.net.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Propex', NULL, NULL, N'Matehuala', NULL, NULL, NULL, 79, N'Ramon Acosta', N'0052-4888821728', N'ramon.acosta@propexglobal.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Quality Sack', NULL, NULL, N'DF, Mexico', NULL, NULL, NULL, 79, N'Jesus Morales/Eber Lopez', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Quality Sack', NULL, NULL, N'Av. De Los Ángeles N° 1000 Bodega 451 Col.Garza Cartú', NULL, NULL, NULL, 79, N'Jesus Morales Méndez', N'(81) 83 31 80 85', N'finanzas@qualitysack.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Rafypak', NULL, NULL, N'Atlacomulco.', NULL, NULL, NULL, 79, N'Jose Casarez, Samani Suarez', N'(712) 122 90 00', N'armandoarvizu@berryglobal.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Rainsa Costales / Rafias Industriales', NULL, NULL, N'Guadalajara', NULL, NULL, NULL, 79, N'Jesús Magaña', N'(443) 316 2781', N'ventas@rainsa.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos De Cordoba', NULL, NULL, N'Cordoba', NULL, NULL, NULL, 79, N'Angel Hernandez', N'0052 (271) 7143232', N'info@sacosdecordoba.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Y Costales La Piedra Blanca', NULL, NULL, N'Guadalajara', NULL, NULL, NULL, 79, N'Esteban Hernandez', N' +52 345 912 0426', N'costalesexpress@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Y Empaques Internacionales Sayem', NULL, NULL, N'Calle San Mauro #106  Col. El Paisaje León, Gto.', NULL, NULL, NULL, 79, NULL, N'0052 (477) 707 0718', N'informes@sayem.mx', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Solenvasa', NULL, NULL, N'MONTERREY', NULL, NULL, NULL, 79, N'Jesùs Arrambide', N' 0052(81)-1642-8900', N'jesus.arrambide@solenvasa.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Syntex Mexico (signode)', NULL, NULL, N'Cordoba', NULL, NULL, NULL, 79, N'Raul Chandomi', N'0052(271)7170950', N'atencionaClients@syntexmexico.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'La Casa Del Empaque', NULL, NULL, N'Querétaro', NULL, NULL, NULL, 79, N'Nancy Barron', N'0057 42127790 cel 4421271044', N'nancy@lacasadelempaque.com.mx', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Megasack Panama', NULL, NULL, N'Calle 50 - Torre Global Plaza - Piso 15 Bella Vista Panamá', NULL, NULL, NULL, 81, N'Ing. Angel Muñoz', N'+507 6603 3423', N' angel@megasack.es', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Big Bags Panama', NULL, NULL, N'Calle Entrada a Cerro Azul, 24 de Diciembre (Justo pasado Fucer)', NULL, NULL, NULL, 81, NULL, N' (+507) 263-0208 Celular: (+507) 6514-9043', N'info@bigbagspanama.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Big Bag Paraguay', NULL, NULL, N'Ruta 3 Gral.Aquino km 21.5 BºSan Marcos Limpio', NULL, NULL, NULL, 82, N'Ana Gomez', N'00595 981310471', N'anagomez@bigbagparaguay.com', NULL, 3, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolpar S.a.', NULL, NULL, N'Parque industrial Avay Villeta', NULL, NULL, NULL, 82, N'Sydney Wedderhoff ', N'00595 981423588', N'iweddwehoff@bolpar.com.py', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Empresa Vial Sur S.a.', NULL, NULL, N'General Roa, casi Av. Artigas  Asunción', NULL, NULL, NULL, 82, N'Mario Sabate', N'0054 3815860952', N'mariosabate@vialsur.com.py', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envapar', NULL, NULL, N'Ruta Transchaco', NULL, NULL, NULL, 82, N'Carlos Miranda', N'595 21 974 000', N'gerencia_hilanderia@envapar.com.py', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'La Bolsa', NULL, NULL, N'Ruta Internacional Nro. 7 –  Km. 216', NULL, NULL, NULL, 82, N'Erwin', N'(0528) 222 787', N'comercial@labolsasa.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Metaflex S.a.c.i.', NULL, NULL, N'Avenida Itália y Pirapó, nº 101 Villarrica', NULL, NULL, NULL, 82, N'Rodrigo Ruiz', NULL, N'josecarlos@metaflex.net.br', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polifabril S.a.', NULL, NULL, N'1 Km 17.5, Ruta, Villa del Rosario, Paraguay', NULL, NULL, NULL, 82, N'Ubaldo Zárate', N'(595 21) 578.670', N'ubaldo@polifabril.com.py', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maquicenter', NULL, NULL, N'1 Mayo 1086 c/ Ayolas Fdo. de la Mora  Zona Sur ', NULL, NULL, NULL, 82, N'José Visconte', N'595 021512715/16', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Estelar S.a.', NULL, NULL, N'Gral. Bernardino Caballero, Mariano Roque Alonso', NULL, NULL, NULL, 82, N'José Irastorsa ', N'595 21 578 626', NULL, NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Nature Group', NULL, NULL, N'Ruta 7 , Km 13 , Minga Guazú', NULL, NULL, NULL, 82, N'Anibal Arruda', N'+595 61 583211', N'info@groupnature.net', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agribag', NULL, NULL, NULL, NULL, NULL, NULL, 82, NULL, NULL, NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Alfa Bag', NULL, NULL, NULL, NULL, NULL, NULL, 82, N'Favio Carvalhaes', NULL, NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ardelca', NULL, NULL, N'Lim Calle Los Talladores Nro 320 Urb. Ind. El Artesano', NULL, NULL, NULL, 83, N'Natalia del Carpio', N'0051-1-4363651', N'nataliadelcarpio@ardelca.com.pe', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Atlantica', NULL, NULL, N'Via de Evitamiento km.3 Chiclayo', NULL, NULL, NULL, 83, N'Saul y Néstor Estela Delgado', N'0051 949819634', N'saul@atlanticaperu.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bigbag Peru', NULL, NULL, N'Av. Los Incas 1029 2 Piso La Victoria Chiclayo', NULL, NULL, NULL, 83, N'Jony Velasquez Cruzado', N'0051-74624270', N'ventas@bigbagperu.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Corensac', NULL, NULL, N'Lima', NULL, NULL, NULL, 83, N'Miguel Garcia Calderon', N'0051-1-5444261/256', N'??corensac@corensac.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Distribuidores Generales H&d Sac', NULL, NULL, NULL, NULL, NULL, NULL, 83, N'Brahiam Pinedo López', N'0051 987294128', N'distribuidoresgenerales.hyd@gmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'El Aguila', NULL, NULL, N'Chiclayo', NULL, NULL, NULL, 83, N'Gabriel Estela Delgado', N'0051-970043700', N'gestela@elaguila.com.pe', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ensacar Peru Sac', NULL, NULL, NULL, NULL, NULL, NULL, 83, N'Fredy Alva', N'00 51955032192', N'Ensacarperu@outlook.es', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Fibrafil', NULL, NULL, NULL, NULL, NULL, NULL, 83, N'Jonny Zaidan', NULL, N'Jzaidan@grupofibrafil.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Iberoplast', NULL, NULL, N'Av. JJ Poblete s/n parcela 19 Lote 2 Lurín', NULL, NULL, NULL, 83, N'Wilinton Lozano', N'(0051 1) 430-0061 / 430-2761', N'wilinton.lozano@iberoplast.pe', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Inversiones Peru J&p Sac', NULL, NULL, N'Av. Pedro Ruiz Gallo Nº 1061 Urb. San Roque Lima', NULL, NULL, NULL, 83, N'Moises Vaisman', N'0051 994 229 395', N'moises.vaisman@inver-peru.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Latinoamericana De Envases', NULL, NULL, N'JR TRUJILLO 103 Semi Rural Pachacutec Arequipa', NULL, NULL, NULL, 83, NULL, N'054 487500 - 054 487508', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Logistica Sacos Peru S.a.c.', NULL, NULL, NULL, NULL, NULL, NULL, 83, NULL, N'01-2723130 / 01-7863977 ', N'informes@logisticasacosperu.com ', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maxibigbag = Maxisacos', NULL, NULL, N'Av. Bauzate y Meza Nº 1937 La Victoria Lima- Peru', NULL, NULL, NULL, 83, N'Bernardino Soto', N'998-104824 ', N'ventas@maxibigbag.com.', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Negociacion Andina', NULL, NULL, N'Calle Delta 230 – (Parque Internacional de la Industira y el Comercio) Callao', NULL, NULL, NULL, 83, N'Patricia Quispe/Martín Del Aguila', N' (51) 14518383 ', N'asistentegerencia@grupomekler.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Norsac', NULL, NULL, N'Lima', NULL, NULL, NULL, 83, N'Pilar Cayo', N'0051-1-4442044/4470172', N'pcayo@norsac.com.pe', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polibag Peru - Sacos Y Telas', NULL, NULL, N'Mz.E Lt.8 Sec.Victor Raul H.De La Torre San Juan de Miraflores', NULL, NULL, NULL, 83, N'Raul Bustamante', N' 499-8243', NULL, NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Procomsac', NULL, NULL, N'Carretera a Pimentel, Mz \"G\" Lte. 2, Zona Parque Industrial, Pimentel - Chiclayo', NULL, NULL, NULL, 83, N'Carlos Montenegro', N'074203805 - 201854', N'w.rafael@solesnorte.com.pe', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Recibag', NULL, NULL, N'Calle 15 Mz. 15, Lt. 2, A.H. Laura Caller Iberico Los Olivos Lima', NULL, NULL, NULL, 83, N'Ernesto Ramírez Reyes', N'994274521 - 955226837 ', N'ventas@recibag.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Pisco', NULL, NULL, N'Av. Circunvalación del Golf Los Incas # 134 - Edificio Patio Panorama', NULL, NULL, NULL, 83, N'Ing. Alcides Reyes Leiba', N'0051-1-3482256', N'areyes@sacospisco.com.pe', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'San Nicolas Sac', NULL, NULL, N'Carretera a San Jose - Z/I. Predio la Laguna S/N. - Chiclayo', NULL, NULL, NULL, 83, NULL, N' 074-630745', N'administraciongeneral@envasessannicolas.pe', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Special Peruvian Bags', NULL, NULL, N'Av. Alfredo Mendiola, 1407 San Martin de Porres Lima', NULL, NULL, NULL, 83, N'Pedro H. Barraza Guerra', N'(01) 534-7013', N'specialperuvianbags@hotmail.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Terranova Industrial', NULL, NULL, NULL, NULL, NULL, NULL, 83, NULL, NULL, N'ventas@terranovaindustrial.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Tinpac Sac', NULL, NULL, N'Car. Sub.Par.2 .las Salinas Km. 40 Fnd. Asoc.Predio Rùstico', NULL, NULL, NULL, 83, N'Jorvy Fernández', N' (01) 717-7959 / (01) 717-7960', N'admnistracion@tinpac.net', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Emusa Peru Sac', NULL, NULL, NULL, NULL, NULL, NULL, 83, N'Manuel Mondragón', N'0051 1 6176700 ', N'mmondeagon@emusa.com.pe', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bags For Africa', NULL, NULL, N'KLOOF FAMILY HEALTH 8 VILLAGE RD KLOOF DURBAN 3610', NULL, NULL, NULL, 53, N'Contact: Mr. Leon Schoeman +27 82 8000 560', N'27 031 764 2527', N'reception@bagsforafrica.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bohlabela Cooperative', NULL, NULL, N'16 JAN STREET ROCKYS DRIFT WHITE RIVER MPUMALANGA', NULL, NULL, NULL, 53, NULL, N'27  087 754 5315', N'admin@bohlabelacorp.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bonus Bag', NULL, NULL, N'33 CLOUGH STREET FACTORY 1-2 PIETERMARITZBURG 3201', NULL, NULL, NULL, 53, N'Contact: Mr. Mark Coope +27 82 337 0903', N'27 33 3422852', N'mark@bonusbags.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Dynamic Bags', NULL, NULL, N'1 ABATTOIR STREET MOORREESBURG CABO OCCIDENTAL', NULL, NULL, NULL, 53, N'CORNE KOCH', N'27  84 506 5475', N'info@dynamicbags.co.za', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ibbs Bags', NULL, NULL, N'123 14 TH AVE UNIT 12 ANDERBOLT BOKSBURG GAUTENG', NULL, NULL, NULL, 53, N'Contact: Mr. Greg Jamison +27 082 334 6650', N'27 118946424/27823346650', N'greg@ibbs.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Lcpa Packaging', NULL, NULL, N'12 SMITH ROAD CNR KIRBY ROAD BEDFORD GARDENS JOHANESBURGO', NULL, NULL, NULL, 53, N'Contact: Krishna Barua Vithal', N'27 110628300', N'www.lc-packaging.es', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Megabags Ltd', NULL, NULL, N'7 KARIBA STREET POWERVILLE VEREENIGING', NULL, NULL, NULL, 53, N'Contact: Mr. Divan Van Zyl', N'27 16 455 1498/89 ', N'sales@megabags.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mg Bag', NULL, NULL, N'26 MAIN RD FISHERS HILL PRIMROSE GERMISTON', NULL, NULL, NULL, 53, N'Contact: Mr. Leyton Burne', N'27 082 516 1520', N'mburne@global.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Olympic Bulk Bags Ltd', NULL, NULL, N'77-79 BUTLER ROAD NUFFIELD SPRINGS', NULL, NULL, NULL, 53, N'Contact: Mr. Sam Greeff', N'27 082 389 9451', N'sam@olympicbulkbags.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sabag S.a.', NULL, NULL, N'11 RABIE STREET VANDERBIJLPARK, 1900 GAUTENG', NULL, NULL, NULL, 53, N'Contact Mr. Etienne Stols', N'27 16986 0079-828502634', N'etienne@sab ag.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Southpoint Industries', NULL, NULL, N'14 NORTHGATE PLACE RED HILL DURBAN NORTH', NULL, NULL, NULL, 53, N'Contact:  Mr. R. Chetty +27 31 356 3662', N'27 031 569 3662', N'cecil@southpoint.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sunny Bag (pty) Ltd.', NULL, NULL, N'25, PINK STREET, INDUSTRIAL AREA, BOTSHABELO (FREE STATE)', NULL, NULL, NULL, 53, N'Contact: Mr. F. Tsai', N'27  51 5344188-5343784', N'mdhupli@global.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Trunel Bags', NULL, NULL, N'7 KLIPPAN STREET MONTANA PRETORIA GAUTENG 0182', NULL, NULL, NULL, 53, N'Contact: Mr. Christo Nel (owner) 27 082 4903412', N'27 082 4903412', N'info@trunelbags.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Tufbags', NULL, NULL, N'75 GOODWOOD ROAD WESTMEAD PINETOWN 3610', NULL, NULL, NULL, 53, N'GARETH JELLIMAN', N'27 086 1883-224', N'sales@tufbag.com', NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Carolina S.a.', NULL, NULL, N'Camino Carrasco 134  Paso Carrasco', NULL, NULL, NULL, 86, N'Fabián Cativeli', N'2604 5120', N'ventas@carolina.com.uy', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agromas', NULL, NULL, N'Sanlucar 1491 of.104  Montevideo', NULL, NULL, NULL, 86, N'Federico Pick (dueño)', N'2601 4411', N'agromas@agromas.com.uy', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'El Rey Del Big Bag', NULL, NULL, N'Ramón del Valle Inclán 2310 - Montevideo Uruguay', NULL, NULL, NULL, 86, N'José Tovagliare', N'2209-2920', N'tova1414@hotmail.com.uy', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Juan Cavajani S.a.', NULL, NULL, N'Rambla Dr. Baltasar Brum 3258 Montevideo Uruguay', NULL, NULL, NULL, 86, N'Ignacio Tristán', N'2200 04 66', N'jtristan@cavajani.com.uy', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Uliren S.a.', NULL, NULL, N'Av. Millan 5308', NULL, NULL, NULL, 86, N'Gustavo Fernandez', N'2355 8862', N'g.fernandez@uliren.com.uy', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Dulari Srl', NULL, NULL, NULL, NULL, NULL, NULL, 86, N'Rosario Hernández', NULL, N'info@dularisrl.com', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Oliva Garden', NULL, NULL, N'Avda. España y Ruta 101 Pando', NULL, NULL, NULL, 86, N'Eduardo Dubcovsky', N'+598 22919320 | 22915116', N'consultant@olivagarden.com.uy', NULL, 2, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polivensa', NULL, NULL, N'Av. Principal, Lote E, parcela #10. Urb. Parque Industrial La Mora II. La Victoria Edo. Aragua', NULL, NULL, NULL, 87, NULL, N'+58 244 323.11.26', N'compras@polivensa.net', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polybarq', NULL, NULL, N'Carrera 5 con Calle 31. Zona Industrial I; Barquisimeto Lara (3001)', NULL, NULL, NULL, 87, N'María Eugenia Espósito', N'58 251 713 0111', N'asistentegerenciaoperaciones@polybarq.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Anfica', NULL, NULL, NULL, NULL, NULL, NULL, 87, NULL, NULL, NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Global Packing Solutions', NULL, NULL, NULL, NULL, NULL, NULL, 87, N'Carlos Oropeza', N'001 7864031211', N'globalpacking16@gmail.com', NULL, 5, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Cesur', NULL, NULL, N'Mpumalanga', NULL, NULL, NULL, 171, N'Murat Gokce', NULL, N'murat.gokce@cesur.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Eska Bigbag', NULL, NULL, NULL, NULL, NULL, NULL, 171, NULL, NULL, N'eska@eskabigbag.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Privilege Group', NULL, NULL, N'JLT, Cluster W Liwa Heights 3803 Dubai United Arab Emirates P.O. Box 127565', NULL, NULL, NULL, 166, N'Mary Cruz', N'Office +971 4 2400330', N'mlbc@privilege.group', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Bang Polypacks', NULL, NULL, N'308, Keval Industrial Estate,1rd Floor, Senapati Bapat Marg, Lower Parel Mumbai 400013 Maharashtra India', NULL, NULL, NULL, 150, N'Vasudev Bang', N' +(91) - (22) - 40436565 ', N'vasudev@bangdataforms.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Turfbag', NULL, NULL, NULL, NULL, NULL, NULL, 156, N'Craig Matthew', NULL, N'craig.matthew@tufbag.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bohlabela Cooperative', NULL, NULL, NULL, NULL, NULL, NULL, 156, NULL, N'087 754 5315', N'admin@bohlabelacorp.co.za', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Chempack', NULL, NULL, NULL, NULL, NULL, NULL, 232, NULL, NULL, N'chempack@chempack.ru', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Climesa', NULL, NULL, NULL, NULL, NULL, NULL, 199, N'Joan Climent', NULL, N'climesa@fibcnet.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Fertiplast', NULL, NULL, N'Sevilla', NULL, NULL, NULL, 199, N'Joaquín Nuñez', N'0034 954 186 024', N'administracion@fertiplast', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Beniplast Benitex', NULL, NULL, N'Avenida Vicente Boluda Palop, 54, 46830 BENIGANIM (Valencia)', NULL, NULL, NULL, 199, NULL, N'96 292 01 46 ', N'info@benitex.com', NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Romtextil', NULL, NULL, NULL, NULL, NULL, NULL, 216, NULL, NULL, NULL, NULL, 1, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Acosta, Aldo Adrian', NULL, NULL, N'Rosario Vera Peñaloz', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agilbags Containers', NULL, NULL, NULL, NULL, NULL, NULL, 64, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agromarc Di Biasi S.r.l.', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Aldo Ortigoza Medina', NULL, NULL, N'Salvador Diaz Miron n° 23', NULL, NULL, NULL, 79, N'Aldo Ortigoza Medina', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Analogía S.a', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Andreone Esteban Raul', NULL, NULL, N'Constantini 1446', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Aniceto Moles Y Hnos S.a.c.i.', NULL, NULL, N'Dean Funes 2063', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ariel Jose Perez', NULL, NULL, N'Oro 161', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Aryes S.r.l', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Atenor Hno S.r.l.', NULL, NULL, N'Junin 486', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Atilio Ramon Brillada', NULL, NULL, N'Alberto Einstein 297', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Bag Flex S.a.', NULL, NULL, N'Mitre 1617 Piso 6 Dto. 602', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Baloni Alejandro Luis', NULL, NULL, N'Ruta Provincial Nº 2 Dpto 7', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Bernardino Dourado', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Berry Global', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bh Comex', NULL, NULL, NULL, NULL, NULL, NULL, 64, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Big Bag Amado', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Big Bags Areco S.r.l.', NULL, NULL, N'Ruta Nº 8 Km. 111,50', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolpar S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 82, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolsa Nueva S.r.l.', NULL, NULL, N'Montiel 2934', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolsagro De Andrea Delbuono', NULL, NULL, N'Alvear', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolsareco S.r.l.', NULL, NULL, N'Figuela A. Quigles 20', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolsas Bahia S.a.', NULL, NULL, N'Av. Alem 2429', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolsas Del Salado S.r.l.', NULL, NULL, N'Ruta Prov. Nº 30 Y 191', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bonacorso Cesar', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bonzi, Ricardo Pedro', NULL, NULL, N'Av. 9 De Julio 4175', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bralbol', NULL, NULL, N'Alem 309', NULL, NULL, NULL, 61, N'Alfonso Saez', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bralt S.r.l.', NULL, NULL, N'Europa 976', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Caldelbol S.a.', NULL, NULL, N'Jaime Rodes 327', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Cargoflex', NULL, NULL, NULL, NULL, NULL, NULL, 69, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Carpas Argentinas S.r.l.', NULL, NULL, N'Mendoza 8011', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Catalan, Marcelo Fabian', NULL, NULL, N'25 De Mayo Esq. Cordoba', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Cb Groups', NULL, NULL, NULL, NULL, NULL, NULL, 213, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Coisa S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 69, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Comerc. Florguez', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Osvaldo Flores', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Comercial Carlos Morales', NULL, NULL, NULL, NULL, NULL, NULL, 69, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Comintexa', NULL, NULL, N'Andonaegui 1250', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Contenedores Del Sur S.r.l.', NULL, NULL, N'Manuel Ocantos 563', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Conteplast S.r.l.', NULL, NULL, N'Santa Elena 342', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Córdoba Envases S.a.', NULL, NULL, N'Entre Rios 247', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Corporación Olympic S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Alex García', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Cosaf S.r.l.', NULL, NULL, N'Dean Funes 435', NULL, NULL, NULL, 61, N'Seibel Alberto', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Damiano, Marta Nelida Beatriz', NULL, NULL, N'14 De Julio 1253 Pb', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Dealer S.a.', NULL, NULL, N'San Martin 535', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Delplast', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Demarco Angel Alberto', NULL, NULL, N'24 E/ 51 Y 52 Nº 855', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Descansar S.a.', NULL, NULL, N'Bv. Sarmiento 1389', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Diego Moreira', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Distribuidora Danatex', NULL, NULL, NULL, NULL, NULL, NULL, 66, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Emanuel Nigro', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envagreenpack', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envapar', NULL, NULL, NULL, NULL, NULL, NULL, 82, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envaplast S.r.l.', NULL, NULL, N'Pedro De Mendoza Av. 3887 Pb', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envases Colon S.a.', NULL, NULL, N'43 Nº 1082', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envases Pueyrredon S.r.l.', NULL, NULL, N'Av. Locatell 120 Barrio San Roque', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Envases Reciclados S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 61, N'Emanuel Andrada', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Enviflex S.r.l.', NULL, NULL, N'Av. Hipolito Yrigoyen 242', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Estelar S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 82, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Excor S.r.l.', NULL, NULL, NULL, NULL, NULL, NULL, 61, N'Sebastián García', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Experto Bags', NULL, NULL, NULL, NULL, NULL, NULL, 69, N'Erik Tobar', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Fabrica Marsar', NULL, NULL, NULL, NULL, NULL, NULL, 83, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'FaB-Tex', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Francisco Lonas S.r.l.', NULL, NULL, N'Parchape 1056', NULL, NULL, NULL, 61, N'Rodriguez Ariel', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Galceran, Gabriela Carola', NULL, NULL, N'Costa Rica 7884', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Garcia Mauricio Francisco', NULL, NULL, N'Camino Vecinal', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Gonzalez, Jose Gilberto', NULL, NULL, N'Garay 3295', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Gonzalo Jose Fernando', NULL, NULL, N'Av. Venezuela 546', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Greeff''s Bulk Bags', NULL, NULL, NULL, NULL, NULL, NULL, 53, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Grupo Doble Tt S.r.l.', NULL, NULL, N'Pedro Lino Fuentes 2457', NULL, NULL, NULL, 61, N'Gama Hogar & Camping', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Grupo Marayes Srl', NULL, NULL, NULL, NULL, NULL, NULL, 61, N'Mingo Pugliese', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Guerrero Daniel', NULL, NULL, N'Intendente Camusso 145', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Gutierrez De Calvet Emilce Edith', NULL, NULL, N'Salta 2690', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Heredia, Marcelo Alejandro', NULL, NULL, N'Av. H. Yrigoyen 115', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Hernan Torres', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Hilgert S.a.', NULL, NULL, N'San Jose 1332', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Indubol Leones S.r.l.', NULL, NULL, N'Entre Ríos 535', NULL, NULL, NULL, 61, N'Ravasi, Guillermo', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Indubol S.a.', NULL, NULL, N'Int. Zanotti 976', NULL, NULL, NULL, 61, N'Ravasi, Guillermo', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Inducon Contenedores Flexibles S.r.l.', NULL, NULL, N'Av. Eva Péron 6150 Pb', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Industrias 5400 S.r.l.', NULL, NULL, N'Entre Rios 213', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Industrias Hiecol', NULL, NULL, NULL, NULL, NULL, NULL, 66, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Inpacont S.a.', NULL, NULL, N'Gavilan 2628', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Iqa Textil', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'Iber Pardo', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Jaca, Cristina Veronica', NULL, NULL, N'Espora 191', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Jaca, Jorge Raul', NULL, NULL, N'Trsitan Lobos Y Jose Hernandez', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Jb System', NULL, NULL, NULL, NULL, NULL, NULL, 213, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'José Gilverto Gonzalez', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Jose R. Centeno S.r.l.', NULL, NULL, N'Francert 2072 Talleres Oeste', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Jose Roque Monzon', NULL, NULL, N'Calle 24 E / 52 Y 53', NULL, NULL, NULL, 61, N'Monzon', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Juan De Casas S.a.', NULL, NULL, N'Sarmiento 946', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Juan Jose Carli', NULL, NULL, N'Av. San Martin 3234', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Juncal Ruiz Gutierrez', NULL, NULL, NULL, NULL, NULL, NULL, 199, N'Juncal Gutierrez', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Kindsvater Jorge Esteban', NULL, NULL, N'Avda. Libertad 1028', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'La Bolsa', NULL, NULL, NULL, NULL, NULL, NULL, 82, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'La Confeccionadora S.r.l.', NULL, NULL, N'Octavio Nanzer 326', NULL, NULL, NULL, 61, N'Boido', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Lantermo Y Filareto', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Lety Camping', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Lombardi, Adrian Ricardo', NULL, NULL, N'Florencio Varela 645', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Lopez Alejandro Omar', NULL, NULL, N'Galvez 1651', NULL, NULL, NULL, 61, N'Pasacalles Rosario', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Lopez, Rodolfo Idelfonso', NULL, NULL, N'Bouchard 5841', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mac Pallet S.a.', NULL, NULL, N'Fournier 2343', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Manufactura S.a.s.', NULL, NULL, NULL, NULL, NULL, NULL, 66, N'Juana', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maquibol Furlitenko Y Cia. S.r.l.', NULL, NULL, N'Lavalle 1605', NULL, NULL, NULL, 61,  N'Furlitenko Gregorio', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Martin Pullaro', NULL, NULL, N'Hughes', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Martinotti, Horacio', NULL, NULL, N'Motturas 172', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Masseto G Y Saita l.', NULL, NULL, N'Estrada 2645', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maxi Big Bag', NULL, NULL, NULL, NULL, NULL, NULL, 83, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Maxisacos Yireh', NULL, NULL, NULL, NULL, NULL, NULL, 69, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Metaflex', NULL, NULL, NULL, NULL, NULL, NULL, 82, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Mf Sein', NULL, NULL, NULL, NULL,NULL, NULL, 82, N'José Medina', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Michelotto Gustavo Javier', NULL, NULL, N'Colon 1636 E / Machado Y Del Valle', NULL, NULL, NULL, 61, N'Gustavo Michelotto', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Miguelez, Simon', NULL, NULL, N'Antartida Argentina 150', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Miguenz, Julio Cesar', NULL, NULL, N'Doctor Bozzetti 321', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Minichello Mauricio', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'New Bag Chile', NULL, NULL, NULL, NULL, NULL, NULL, 69, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Organización Pitarch S.r.l.', NULL, NULL, N'Peribebuy 85', Null, NULL, Null, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Pacheco, Diego Adolfo', NULL, NULL, N'9 De Julio 685', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Pacheco, Hector Raul', NULL, NULL, N'Buenos Aires 818', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Paola Siruk', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Papelera Norte S.r.l.', NULL, NULL, N'Fotheringham 42', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Pavco De Occidente', NULL, NULL, NULL, NULL, NULL, Null, 66, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'PetroniO- DussI- Wiegrebe S.r.l.', NULL, NULL, N'Caferata 1209', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Plasticksacks', NULL, NULL, NULL, NULL, NULL, NULL, 71, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polinal', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Politejidos S.r.l.', NULL, NULL, N'Dr. Sande 456', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polyproductos De Guatemala', NULL, NULL, NULL, NULL, NULL, NULL, 74, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polysnop', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Procoydiplastic Sadecv', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Proyectos S.r.l.', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Quality Sacks', NULL, NULL, NULL, NULL, NULL, NULL, 79, N'Jesus Morales', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Quintana, Silvia Ines', NULL, NULL, N'Calle 71 Nº 532', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Recibag R Y R', NULL, NULL, NULL, NULL, NULL, NULL, 83, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Replen S.r.l.', NULL, NULL, N'Ministro Combet 5181', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Ricardo Giovanetti E Ignacio Giovanetti S.h.', NULL, NULL, N'E. Bordabehere 1657', NULL, NULL, NULL, 61, N'Ignacio Giovanetti- Giovanetti Bolsas', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Rodolfo Jose Valguarnera S.a.', NULL, NULL, N'Sarmiento 730', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Romero Hnos. S.h.', NULL, NULL, N'Los Fresnos y Junin De Los Andes', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Rosario Bolsas S.r.l.', NULL, NULL, N'Cdte. Andresito 859', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacomaar S.r.l.', NULL, NULL, N'Simon Bolivar 1292', NULL, NULL, NULL, 61, N'Antonio Pellegrina', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos Duran Reysac', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Xavier Duran', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sampedro, Antonio Pedro', NULL, NULL, N'Arredondo 3650', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Sapisco Contenedores', NULL, NULL, NULL, NULL, NULL, NULL, 83, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sebastian Pedro Palacios', NULL, NULL, N'Pichetto 547', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sebastian Tornello', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sedas Del Caribe', NULL, NULL, NULL, NULL, NULL, NULL, 66, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Serafin Sanchez Y Mirta S. Micheo S.h.', NULL, NULL, N'Av. Falucho 1346', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sersa S.r.l.', NULL, NULL, N'Av. España Norte 245', NULL, NULL, NULL, 61, N'Serrate Eduardo', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sesana', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sharman Bag Argentina S.r.l.', NULL, NULL, N'Ratto 641', NULL, NULL, NULL, 61, N'Luciano Bernardini', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'SignodE-Syntex', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Skretting Ecuador', NULL, NULL, N'Km 6.5 & 4.5 vía Durán', NULL, NULL, NULL, 71, N'Julio Jara', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Soliz De Cortes', NULL, NULL, NULL, NULL, NULL, NULL, 63, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Speed Bag Srl', NULL, NULL, N'San Jose 112', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Split Servicios S.a.', NULL, NULL, N'Av. Corrientes Nº 330 Piso 5 Dto. 509', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Stirnemann, Cesar Carlos', NULL, NULL, N'25 De Mayo 1909', Null, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Tecno Flec S.r.l. (bolsa Nueva + Seibel)', NULL, NULL, N'Schmidt Ulrico Av. 6974', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Texcon S.r.l.', NULL, NULL, N'Montiel 2416', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Textil Kopruch S.a.', NULL, NULL, N'Ciudadela 3444', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Trame S.r.l.', NULL, NULL, NULL, NULL, NULL, NULL, 61, N'Ernesto Cristian Rivarola', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Tufbag Ltd.', NULL, NULL, NULL, NULL, NULL, NULL, 53, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Unibag S.a.', NULL, NULL, N'Emilio Barletti 1260', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Urabayen Ramiro', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Vaccarini Mariano Y Medina Abel S.h.', NULL, NULL, N'Bv. Rivadavia S/N', NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Alexis Antonelli', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Sacos De Costa Rica', NULL, NULL, NULL, NULL, NULL, NULL, 67, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Bolsas Polimar', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Intersac Spa', NULL, NULL, NULL, NULL, NULL, NULL, 69, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Jumbocel', NULL, NULL, NULL, NULL, NULL, NULL, 79, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Marcelo Soria', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agroenvases', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Gerardo Sarries', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Valpall Flex', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Emmain', NULL, NULL, NULL, NULL, NULL, NULL, 69, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (N'Bael Espíndola', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Polo Andrés Luis', NULL, NULL, NULL, NULL, NULL, NULL, 61, NULL, NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Agroquimica Alberdi S.r.l.', NULL, NULL, N'Lidoro Quinteros 222', NULL, NULL, NULL, 61, N'Vargas Martin, Moya Jose María', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Imfadaza S.a.', NULL, NULL, NULL, NULL, NULL, NULL, 71, N'Astudillo Angel, David Zambrano', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Clients] ([BusinessName],[DocumentTypeId],[DocumentNumber],[Address],[CityId],[PostalCode],[StateId],[CountryId],[ContactName],[CellPhoneNumber],[Email],[DateOfBirth],[OperationStateId],[IsActive],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (N'Climesa S.a.', NULL, NULL, N'Av. Onze de Setembre, 23', NULL, NULL, NULL, 199, N'Jordi Climent', NULL, NULL, NULL, 4, 1, 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO