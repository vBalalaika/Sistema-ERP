1- Modificar los ConnectionStrings en el appsettings.json
2- Abrir la consola de package manager
	En la consola seleccionar el project de infrastructure
	Luego en la consola ejecutar los siguiente comandos:
		update-database -context IdentityContext
		update-database -context ApplicationDbContext


		Para regenerar la base de datos: 
		- borrar la carpeta Migrations/ApplicationDB
		- luego desde ERP:Insfrastructure ejecutar:
		Add-Migration Initial -Context ApplicationDbContext
		y luego update-database -context ApplicationDbContext

		Para generar script de la DB:
		Script-Migration -From older_migration_name -To newer_migration_name -Context ApplicationDbContext
		El archivo se guarda en:
		C:\Users\Natalia\Source\repos\ERP SimpleMAK\ERP.Infrastructure\obj\Debug\net5.0