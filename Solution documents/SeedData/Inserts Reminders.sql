USE [SystemERP_DB]
GO

/*
- Si se quiere borrar la información de la tabla y reiniciar la primary key ejecutar lo siguiente:

USE [SystemERP_DB]
GO
DELETE FROM Reminders
GO
DBCC CHECKIDENT (Reminders, RESEED,0)
GO

*/

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (1, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se le vendio una cortadora de eslingas. ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (2, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'07/09 contacto telefònico. Correo de presentaciòn de nuevo vendedor. 30/11 mail con brochures.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (3, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'07/09 llamada telefònica, se envìa correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL) 
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (4, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'19/09 comunicación tel. Ya nos conoce (a Fabián) ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (5, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'07/09 llamado s/respuesta. 10/09 idem.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (6, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'11/09 llamado y correo. 13/09 llamado: Tienen buen equip. (Stallinger) por el momento no hay plan de inversión.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (8, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'21/01/19 NOS CONTACTO POR LA WEB. Le interesa cortaplano.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (9, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'21/01/19 Interesado en sacos. Mail enviado 22/01/19', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (10, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'28/01/219 envió mail por web. Interesado en BB. WS', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (16, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'09/10.Se contactó por mail web. Interesado en Impresora 4C. Se le respondio con cotizacion IBB3 y 4C.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (17, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'25/09 Charla con Marcelo Vega. Mail con info. 06/12/18 no les interesa por el momento.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (18, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'20/11/18 mail a Loreto Vargas.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (19, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'06/12/2018 Hablé con MV. Usa maq. Con métodos artesanales. Le envié info. 24/01/19 nada por el momento, pero que lo visitemos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (20, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Comercializa', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (21, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Es de Brasil', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (22, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Tiene Cortaeslinga de las primeras.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (23, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Está en Temuco, fabrican super sacos. Gerente de viaje regresa lunes 22/10', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (24, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'20/12/18 llamado telef.interesado p/2019 nos encontraremos en chile marzo19.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (26, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Tienen C2000 + CEx4s. Puesta en marcha Juan.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (27, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'25/09 contacto tel. Mail con info. 11/10 necesitan renovar maquinarias. Martes enviaran correo con inquietudes. ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (28, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'NO SE VISITO', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (29, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Prensa 4Ton.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (31, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'31/01/19 mail con info. Llamare en 15 días.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (32, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'06/12/2018 Llamado, no fabrican, importan desde india. 4 meses cada impo. Le envié mail con info. Le interesa', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (35, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'08/10 reenvìo de presupuesto.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (36, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'C2000 Y CEX4S.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (37, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Es medio socio de newbag, Luis Patricio Saldias Gonzalez. WS', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (38, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Quiere impresora para sacos 3c +apilador.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (39, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'09/10 contacto. 10/10 mail c/info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (40, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'20/12/2018 No fabrican BB sólo sacos. Le compran a EMMAIN. Están equipados con maq. Taiwan.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (41, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'06/11/18 se envio correo. No hay manera de comunicación telefónica.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (42, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Son fabricantes. Llamar desp del 24/09. 25/09 llamado. No se muestra interesado.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (43, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No fabrican por ahora. Les provee COISA Y CORESA. Cuando den el salto, nos llaman.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (44, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'08/10 VARIOS CONTACTOS. SON RECICLADORES. NO PIENSAN CAMBIAR POR AHORA.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (45, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'10/09 llamado y e-mail. Fabrican BB para almac. De materias primas de alimentos (Nestlé, etc.). 23/10/18 Va a consultar referencias con Coisa. Prox meses evaluan. 06/12/18 Hable con Regina. La industria del bb para ellos es muy marginal, no piensan en aum', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (46, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'17/09 contacto por WS s/respuesta. Analizaran en 45 días.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (47, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'06/12/18 Comercializa, le fabrica un tío en Antofagasta. Le interesa, mandé info. ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (49, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'eduardo mora', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (51, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'12/09 llamado y correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (54, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'28/12/18 mail con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (55, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Manufacturas le confecciona.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (56, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Explotación y exportación de carbón. Les interesa la prensa. Le parecio muy cara.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (57, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Titular. Le pasé info. Le interesa. Febrero 2019 analiza inversiòn.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (58, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'WS ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (59, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'24/01 mail con info. Respondió acuse.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (60, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Ya habló con FM. Viaje pendiente a la Argentina, proyecto con demoras. WS. 22/04/19 contacto telef.Están comenzando, nos avisan.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (63, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Jefe de operaciones. 6 extrusores, laminadora, 4 impresoras 5 confecc, 42 telares circ. Les interesa confeccionar BB. Se le envia mail con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (65, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'18/01/19 Conoce de máquinas, instaló Starllinger, etc.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (68, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Producen sacos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (70, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Contacto en feria de Bogotá. Solo valvulan. ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (71, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'400 bb/dia. Corte artesanal, lento. Corta eslingas china. Le interesa C2000. Le envié mail con info. 24/01/19 reenvío.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (73, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Reciclador de BB. Le pasé info. No fabrica por el momento.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (74, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'21/01/19 Interés en CF800 + alternativas. En proceso.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (75, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'16/10 WS. Fabrican pero son muy chicos. 3000 bb/mes. 14 empleados. Les intreresa prensa. Cotizamos, le parece cara. Se viene un rediseño.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (76, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'CF800, Prensa P4 Tons.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (77, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'ECUADOR', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (78, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'16/10 contacto por WS/mail. Interesado en reversedora, Idea4c y confección. Tiene deficiencias en esos trabajos. Le pasé toda la info + videos. 11/12/18 Pasa a 2019.-', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (79, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'ECUADOR', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (80, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'18/04/2019 contacto por WS, mail con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (82, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Valvuladora', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (83, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Prensa P4 Tons., C1300 y CEx4s', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (84, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'ECUADOR', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (86, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Aún sin contactar.-', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (87, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hacen todo el circuito desde grumo. 400 tn/mes. Mucho saco pequeño. Creciente mercado de bb para café. Se envió mail con info y contacto por ws. Proyectan comenzar a fabricar a partir de 2019.-', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (88, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Agave es todo yute, henequen. Misma empresa que SASICASA.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (92, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'02/11/18 envio de correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (94, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'15/01/19 mail con info.-', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (95, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'20/11/2018 mail a Alicia Fernandez. 05/01/2019 NUEVO CONTACTO, NUEVO ENVIO DE INFO. SOLO SACOS.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (96, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se contacto via Face, nos dieron el correo y se envió invitación por email, respondio y prometió ir a ver si podemos hacer algo mas adelante…Son solo distribuidores', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (97, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No fabrican. Muy reacios en dar información.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (98, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'14/11 se envió correo. Fabrican en MX Fernando es el encargado allí. 05/02/19 se retira, pasa a Rafael Rascón.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (99, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No fabrican. Muy reacios en dar información.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (100, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable por teléfono con el encargado de compras (Jesus Merín) Envie email y no contesto,28/11/18 contacto. Llamar fin enero 2019. Solo sacos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (101, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se comunicó via web. 11/2018 le envié varios correos a su web, no exponen teléfonos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (102, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se contacto via Face, amable respuesta de Gerardo Gonfer nos dieron el correo y se envió invitación por email, respondio visitarnos en feria', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (103, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se enviò correo. Se dara llamada para seguimiento.-', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (104, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'21/11/18 Adriana Dieguez encargada de compras. Son fabricantes de eslingas, correas, sogas. Le pasé cortadora de eslingas x mail.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (105, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Sandra Muñoz. 14/11/18 se envio correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (106, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Sr. Enrique. Es la persona que contacte en el DF que no pudo ir a la feria. 22/11/18 correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (107, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Mary Carmen, Dow solo comercializan, NO fabrican, igual los invite. 22/11/18 siguen comercializando.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (108, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se contacto via Face, nos atendio amablemente y pidio coti. Pase por email y prometen visitar la feria para definir y corregir el proyecto de la mesa de corte (Sr. Ing. Arturo Gomez/ Venustiano García) 20/11/18 contacto con VG, fotos WS de maq. Actuales. ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (109, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se contacto via Face, me confirmaron correo electronico, envie Email, 22/11/18 contacto. Cortan con método artesanal, impresión de sacos, necesitan dar vuelta la histora. WS.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (110, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable con Rodrigo, (es el hijo), envié email. 21/11/18 contacto con Rodrigo, le envié todo FIBCs y CO800', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (111, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame por tel y me pasaron el emai de la ing. Luz María. Le escribí un correo y estoy esperando respuesta. 22/11/18 contacto, no fabrican BB solo sacos. Le pase info por mail.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (112, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable por teléfono con Esteban, (es el hijo de Angel)22/11/18 contacto telef.c/Esteban. 23/11 mail con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (113, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Julio Cesar López. No fabrican, se quieren largar. Les interesa.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (114, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No están interesados por el momento.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (115, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable por teléfono y me pasaron el email, les escribí', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (116, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No fabrican, pero les interesa, estaban averiguando máquinas. 19/12/18 mail con info general.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (117, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'23/11/2018 ws y mail con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (118, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable por teléfono con Norma y me dijeron que le escriba al Ing. Ricardo Perez 23/11/18 Hable Con el Ing Perez.  Solo fabrican sacos, tienen todo Stallinger. Comienzos de año 19 evalúan inversión.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (120, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'26/11 Envie email con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (121, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame por tel y quiere que le cotice la mesa de corte manual. 23/11/18 cotizacion de ibb3y4', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (122, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'14/12/2018 mailc/info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (123, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No fabrican.   ', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (124, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Solo limpieza de maxisacos, no fabrican ni comercializan.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (125, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Solo costales. Le envié toda la info en máquinas de sacos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (126, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame, hable con Ana y me pason un correo (Licenciado Jose Antonio) le escribi y estoy esperando respuesta. 11/12/18 contacto con Antonio Becerril, le mandé toda la info. Le interesa.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (127, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable por tel con el Ing. Juan Pablo Martinez, me pidio que le mande email', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (128, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'12/11/18 se envió mail a encargada de compras Lic. Marina Priones.  Por el momento no hay planes de inversión. Nos esperan.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (129, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'19/12/2018 No fabrican BigBag. Solo sacos. Mail con info sacos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (130, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Me dijeron que me comunique a Puebla y no contesta nadie', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (131, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'ES NUESTRO CLIENTE MAS IMPORTANTE DE MEX. TIENE CORTA ESLINGA, 2 CORTADORAS, e IMPRESORA, SE PUEDE CONVOCAR AQUÍ PARA MOSTRAR LOS EQUIPOS. ESTA HABLADO', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (133, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'En contacto con Eric. Hacer seguimiento.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (134, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Gta:Laura Llanos/Cdora:Noemi Estrella, Hable c/Laura y le envié correo. Hablar con Cristina Espinosa.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (135, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable con Valeria, le va a avisar a Joaquin', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (136, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Nos visito en la feria, quiere una impresora chica para imprimir los sacos que trae de china', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (137, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'17/12/2018 mail. Muy reacios a dar info, ni contacto.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (138, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame, hable c/Alicia Negrete envie email:anegrete@grupo-ortiz.com, PIDIO PRECIO VARIAS VECES. 17/12/18 mail con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (139, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Contador Miguel A. Vidrio, responsable de la empresa. 14/12/2018 le envié correo con info de BB. No hace sacos pequeños.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (140, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame, no estan fabricando,17/12/18mail con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (141, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'12/11/18 contacto tel. Viajaría antes de fin de año a conocernos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (142, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Nos pusieron una OC y nunca pagaron. Pospusieron el proyecto. Estoy siempre en contacto con Juan Lopez. Iba a ir a ver nuestro equipos a FABTEX, 26/11/2018 contacto con HG. Le reenvié correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (143, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame, hable con Juan Jesus Villagomez se compro un telar, pero va a ir a vernos. 13/12/2018 hable con Juan Jesús, le intreresa, nos visitará en expo2019.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (144, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Envie email, hable c/Tina, se manejan via email, 19/12/2018 mail con info.-', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (145, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Envie email, llame y hable con Hector Manuel…son distribuidores,19/12/18 solo distribuidores. Recomiendan Tapatios.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (146, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame y hlable c/ Juan Jose, envié email: compras@langstongbag.com.mx', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (147, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame y luego envie invitación a Manuel Birchez. 22/12/18 mail con info. Dio acuse.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (148, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Fue visita en ExpoPack 2017, tenían un proyecto nuevo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (149, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'30/11/18 contacto. Recicla pero tiene proyecto de fabricar. Ws y mail.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (150, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Envie Face, no contestan, hable c/Esperanza Hablar c/Bella Velazquez evelazquez@mexinex.com, van a ir. 14/12/2018 hable con Bella. No hacen PP solo polietileno.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (151, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'12/11/18 se envió correo a compras. Hay WS.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (152, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable c/Jose Luis Anguiano, Le vendi la corta eslinga que lleve a Guadalajara, es muy piola', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (153, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'20/12/18 mail con info.-', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (154, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Rosario me visito en la feria, cotice corta eslinga y no me respondio, se puede re cotizar el modelo nuevo. Contacto por WS', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (155, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame y hable con Antonio Alfaro, nos visto en la feria Francisco Carmona', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (156, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable y me dijeron que hable c/ el titular Hector Galindez,esperar hasta el Jueves', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (157, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable c/Verónica y me pidio que mande un correo a ventas,26/11/18 enviado. 120.000 BB/mes. Corte artesanal y automatico.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (158, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Contactar a Ing. Benjamin del Arco o Juan Carlos López.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (159, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame, hable con Marianela Mondragon, no estan fabricando, envie email igual. 21/12/18 continuan siendo intermediarios.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (160, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Ya tienen C2000. Luego pidieron cotizar API.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (161, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Pidieron precio por equipos para BigBag nunca respondieron', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (162, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'26/11/2018 envío de mail a RA.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (163, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Compraron impresora 2 colores hace poco', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (164, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Negociación vía mail c/FM. Tienen impresora 2 colores.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (165, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Hable c/ Samani Suarez, envié correo a ella y a Jose Cazares, llamar a Armando Ardizu Ocampo, tiene ascendencia en la planta', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (166, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llame, hable c/Fernando Davila me pidio q mande correo a compras@rainsa.com.mx. 26/11/2018 envié correo con info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (167, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Los visite en Cordoba el año pasado. Necesitaban una cortadora de eslingas. Se puede ofertar el nuevo equipo mas economico. 14/12/18 hablé con Angel.Le envié la info por mail.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (168, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'27/05/2019 invitado a la expo y WS con toda la info.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (170, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'11/12/2018 contacto telef. Con Jesús. 12/12/2018 envío de mail con info solo supersacos.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (171, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Visite a Raul Chandomi, tienen impresoras, C2000 y CEx4s. Es una empresa muy grande. Llamar semana del 10/12/2018.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (173, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'www.angel5533.wix.com. 19/12/2018 distribuye desde Venezuela y Guatemala (Polyproductos).', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (174, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Importan desde Barcelona. Contacto con Grupo Vilá Vila (fabricantes y dueños de Bigbag Panamá) No se pudo dar con el encargado. Mucho filtro.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (175, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'10/09 llamado y correo.Pasará necesidades para cotizar. 11/09 se envió presupuesto. 13/09 envio de presupuesto completo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (176, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'30/10/18 visita a Chacabuco, interesado en cortadora + impresora idea3c.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (177, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'18/09 comunicaciòn telefónica. Están avanzando en lay out caleta norte Paraguay. Noviembre nos llaman.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (179, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Ya tiene una impresora c/apilador.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (181, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Solo sacos pequeños, pero tienen en carpeta comenzar con el BB. Mandé mail con info general.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (182, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Venden máquinas, no es el nicho.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (183, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Cotizando impresoras IBB', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (184, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Se le envió toda la linea para BB. Están por arrancar.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (185, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No fabrican.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (187, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'01/11/18 Envio de correo. Les interesa, pasa a primeros meses 2019.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (188, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Visitamos en mayo 2019. Cotizamos VAL1050', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (190, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'01/11/2018 Envio de correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (192, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'26/10/2018 contacto con Gabriel. Se envió correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (193, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Necesita C2000', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (194, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'16/10. No fabrican en Perú, per estan con proyecto de negocio en El Salvador.Se muestra interesado.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (195, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'26/10 Se envió correo con brochures.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (197, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'No fabrican, comercializan, le compran a IBEROPLAST.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (198, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Pésima atención, no me dejaron ni explicar.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (199, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llamados, mail, presupuesto IIBB6C. En curso. Termino comprando en china una rotativa.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (201, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'02/11/2018 Se envió correo. La fábrica está en Trujillo. Se tomará contacto con ellos directamente.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (202, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Solo Polietileno', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (203, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'01/11/2018 Envio de correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (204, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'12/09 mail y llamado.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (205, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'2 VALVULADORAS/1 APILADOR', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (208, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'05/11/18/ XWEB.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn])
VALUES (209, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'31/10/2018 Contacto, se envió mail. Se muestran interesados. Llamar 18/12/2018, 19/12/18 contacto con Jorvy Fernandez, Gte. Gral. Info solo BB.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (214, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'ya mantienen contacto fluído con Simplemak. No llamar.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (224, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'ya mantienen contacto fluído con Simplemak. No llamar.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (225, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llamada telefónica, se envió mail de preserntación. 06/09/18', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (226, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llamada telefónica, se envió mail de preserntación. 06/09/18', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (227, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Contacto con JT, se mandó mail. Importan desde India. 06/09/18', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (228, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llamada telefónica, se envió mail de preserntación. 06/09/18', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (229, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Llamada telefónica, se envió mail de preserntación. 07/09/18', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (230, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'22/11/18 Contacto con Rosario. Analizará para primeros meses 19. Tiene exp y sabe de máquinas. Importan. El socio es de India.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (231, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'07/12/18 Contacto con ED, gerente general. Son subsidiaria de India. Fabrican. No invertirán en el corto plazo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (233, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'12/09 llamado + correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (235, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'Opera desde USA plantas en GUA y VEN. 17 años de experiencia en rafia. Contacto por WS. Le pareció muy caro.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (242, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'11/10 Miembro EFIBCA, envío de correo.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO

INSERT [dbo].[Reminders] ([ClientId],[ContactDate],[ReminderCheck],[ReminderDate],[Comment],[ConcurrencyToken],[CreatedBy],[CreatedOn],[LastModifiedBy],[LastModifiedOn]) 
VALUES (244, CAST(N'2021-01-01T00:00:00.000' AS DateTime), 0, CAST(N'2021-01-01T00:00:00.000' AS DateTime), N'22/11/18 Acaba de comprar 2 cortad S.+impre 6c. Tiene proyecto en el extranjero. Analiza nuestra línea.', 100, NULL, CURRENT_TIMESTAMP, NULL, NULL)
GO