using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.Infrastructure.Migrations.ApplicationDb
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchiveTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchiveTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CUIT = table.Column<int>(type: "int", nullable: false),
                    GrossIncome = table.Column<int>(type: "int", nullable: false),
                    ActivityStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Web = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Skype = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsPerceptionAgent = table.Column<bool>(type: "bit", nullable: false),
                    Admin = table.Column<int>(type: "int", nullable: false),
                    IdClientAuto = table.Column<int>(type: "int", nullable: false),
                    IdSupplierAuto = table.Column<int>(type: "int", nullable: false),
                    Subjet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostHasIVA = table.Column<bool>(type: "bit", nullable: false),
                    Eslogan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UseAFIPTesting = table.Column<bool>(type: "bit", nullable: false),
                    IdProvinceAuto = table.Column<int>(type: "int", nullable: true),
                    DispatchUpdatedCtaCte = table.Column<bool>(type: "bit", nullable: true),
                    IVADefault = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Denomination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TimeOffset = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CutTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CutTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DollarExchangeRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    SalePrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DollarExchangeRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IncomeStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IVAConditions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IVAConditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PieceFormats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceFormats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PieceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PieceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    RawMaterialCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RawMaterialDescription = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Standar = table.Column<bool>(type: "bit", nullable: false),
                    Appreciation = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    PieceXCut = table.Column<int>(type: "int", nullable: true),
                    ColorsXPass = table.Column<int>(type: "int", nullable: true),
                    PressingCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reduction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DimensionPlates = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningMax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeightPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WidthMaxPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WidthMinPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LenghtMaxPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LenghtMinPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Density = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    Bagel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PowerHP = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    Legs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductionSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BagQuantityMax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RollQuantityMax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassageWidthMax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrintWidthMax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrintHeightMax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PressedCenter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PressedSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpeningSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LineFrequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineWeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineLenght = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineWidth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackagingDimension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkPressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InHouseManufacturing = table.Column<bool>(type: "bit", nullable: false),
                    Bought = table.Column<bool>(type: "bit", nullable: false),
                    Component = table.Column<bool>(type: "bit", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    ComponentHighPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentWidhtPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentLongPiece = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StoredStock = table.Column<bool>(type: "bit", nullable: false),
                    ManufacturingTime = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuoteRequestHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRequestHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SupplyVoltages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyVoltages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitMeasures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkOrderHeaderNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Prefix = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                    table.ForeignKey(
                        name: "FK_States_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Users = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderDisplayType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionalAreaId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_FunctionalAreas_FunctionalAreaId",
                        column: x => x.FunctionalAreaId,
                        principalTable: "FunctionalAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    NumberVersionStructure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkOrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrders_WorkOrderHeader_WorkOrderHeaderId",
                        column: x => x.WorkOrderHeaderId,
                        principalTable: "WorkOrderHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupedWorkActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkActivitiesIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupedWorkActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupedWorkActions_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Existence = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    ExistenceUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Minimum = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    MinimumUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    QuantityToOrder = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    QuantityToOrderUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProfitID = table.Column<int>(type: "int", nullable: true),
                    StorageQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    StorageUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    PhysicalSpaceWidth = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PhysicalSpaceLength = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PhysicalSpaceHeight = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    Shelves_Rack = table.Column<int>(type: "int", nullable: false),
                    Shelf = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    StockWidth = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    StockWidthUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    StockLength = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    StockLengthUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    StockHeight = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    StockHeightUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    StockQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    StockQuantityUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    StockWeight = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    StockWeightUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    StockObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    IVATypeId = table.Column<int>(type: "int", nullable: true),
                    OperationId = table.Column<int>(type: "int", nullable: true),
                    ProductFeatureId = table.Column<int>(type: "int", nullable: true),
                    LocationStationId = table.Column<int>(type: "int", nullable: true),
                    IsOnColppy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_IVAConditions_IVATypeId",
                        column: x => x.IVATypeId,
                        principalTable: "IVAConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductFeatures_ProductFeatureId",
                        column: x => x.ProductFeatureId,
                        principalTable: "ProductFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Stations_LocationStationId",
                        column: x => x.LocationStationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_ExistenceUnitMeasureId",
                        column: x => x.ExistenceUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_MinimumUnitMeasureId",
                        column: x => x.MinimumUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_QuantityToOrderUnitMeasureId",
                        column: x => x.QuantityToOrderUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_StockHeightUnitMeasureId",
                        column: x => x.StockHeightUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_StockLengthUnitMeasureId",
                        column: x => x.StockLengthUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_StockQuantityUnitMeasureId",
                        column: x => x.StockQuantityUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_StockUnitMeasureId",
                        column: x => x.StockUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_StockWeightUnitMeasureId",
                        column: x => x.StockWeightUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_StockWidthUnitMeasureId",
                        column: x => x.StockWidthUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_StorageUnitMeasureId",
                        column: x => x.StorageUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_UnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CellPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationStateId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClientDocument = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsOnColppy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BranchCompany = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SizeCompany = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProductionLevel = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IndustryServed = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_OperationStates_OperationStateId",
                        column: x => x.OperationStateId,
                        principalTable: "OperationStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IVAConditionId = table.Column<int>(type: "int", nullable: false),
                    GrossIncome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebPage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProviderType = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfitID = table.Column<int>(type: "int", nullable: true),
                    IsOnColppy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Providers_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Providers_DocumentTypes_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Providers_IVAConditions_IVAConditionId",
                        column: x => x.IVAConditionId,
                        principalTable: "IVAConditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Providers_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccessoriesProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduct = table.Column<int>(type: "int", nullable: false),
                    IdProductAccessory = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoriesProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessoriesProducts_Products_IdProduct",
                        column: x => x.IdProduct,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessoriesProducts_Products_IdProductAccessory",
                        column: x => x.IdProductAccessory,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Archives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ArchiveTypeId = table.Column<int>(type: "int", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Archives_ArchiveTypes_ArchiveTypeId",
                        column: x => x.ArchiveTypeId,
                        principalTable: "ArchiveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Archives_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductCutTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CutTypeId = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCutTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductCutTypes_CutTypes_CutTypeId",
                        column: x => x.CutTypeId,
                        principalTable: "CutTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCutTypes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPieceFormats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PieceFormatId = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPieceFormats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPieceFormats_PieceFormats_PieceFormatId",
                        column: x => x.PieceFormatId,
                        principalTable: "PieceFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPieceFormats_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPieceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PieceTypeId = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPieceTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPieceTypes_PieceTypes_PieceTypeId",
                        column: x => x.PieceTypeId,
                        principalTable: "PieceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductPieceTypes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSupplyVoltages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SupplyVoltageId = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSupplyVoltages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSupplyVoltages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSupplyVoltages_SupplyVoltages_SupplyVoltageId",
                        column: x => x.SupplyVoltageId,
                        principalTable: "SupplyVoltages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RelProductStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelProductStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelProductStations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelProductStations_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Structures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsBase = table.Column<bool>(type: "bit", nullable: false),
                    IsStandard = table.Column<bool>(type: "bit", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    LastVersionId = table.Column<int>(type: "int", nullable: false),
                    SupplyVoltageId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Structures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Structures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Structures_SupplyVoltages_SupplyVoltageId",
                        column: x => x.SupplyVoltageId,
                        principalTable: "SupplyVoltages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Structures_Versions_LastVersionId",
                        column: x => x.LastVersionId,
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ContactDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReminderCheck = table.Column<bool>(type: "bit", nullable: false),
                    ReminderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOperations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    OperationNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOperations_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Charge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobilePhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubsidiaryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryNoteHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    TransportProviderId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    wasExportedToPDF = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryNoteHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryNoteHeaders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryNoteHeaders_Providers_TransportProviderId",
                        column: x => x.TransportProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FinancialInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CBU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialInformations_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    TransportProviderId = table.Column<int>(type: "int", nullable: false),
                    DeliveryNoteNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExternalProcessStationId = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OwnTransport = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeHeaders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeHeaders_Providers_TransportProviderId",
                        column: x => x.TransportProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeHeaders_Stations_ExternalProcessStationId",
                        column: x => x.ExternalProcessStationId,
                        principalTable: "Stations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MissingProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StateMissingProductId = table.Column<int>(type: "int", nullable: true),
                    QuantityToOrder = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    QuantityToOrderUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    StorageUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasedQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissingProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissingProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MissingProducts_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MissingProducts_PurchaseStates_StateMissingProductId",
                        column: x => x.StateMissingProductId,
                        principalTable: "PurchaseStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MissingProducts_UnitMeasures_QuantityToOrderUnitMeasureId",
                        column: x => x.QuantityToOrderUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MissingProducts_UnitMeasures_StorageUnitMeasureId",
                        column: x => x.StorageUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuoteRequestResponseHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    QuoteRequestHeaderId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    IVA = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRequestResponseHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteRequestResponseHeaders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuoteRequestResponseHeaders_QuoteRequestHeaders_QuoteRequestHeaderId",
                        column: x => x.QuoteRequestHeaderId,
                        principalTable: "QuoteRequestHeaders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RelProviderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Width = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    WidthUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Lenght = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    LengthUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    WeightUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    HeightUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    PresentationQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: true),
                    PresentationUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Observations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DollarPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PesosPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelProviderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_UnitMeasures_HeightUnitMeasureId",
                        column: x => x.HeightUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_UnitMeasures_LengthUnitMeasureId",
                        column: x => x.LengthUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_UnitMeasures_PresentationUnitMeasureId",
                        column: x => x.PresentationUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_UnitMeasures_PriceUnitMeasureId",
                        column: x => x.PriceUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_UnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_UnitMeasures_WeightUnitMeasureId",
                        column: x => x.WeightUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelProviderProducts_UnitMeasures_WidthUnitMeasureId",
                        column: x => x.WidthUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelProviderStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    DollarPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PesosPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelProviderStations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelProviderStations_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelProviderStations_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelProviderStations_UnitMeasures_PriceUnitMeasureId",
                        column: x => x.PriceUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelQuoteRequestProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuoteRequestHeaderId = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelQuoteRequestProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelQuoteRequestProviders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelQuoteRequestProviders_QuoteRequestHeaders_QuoteRequestHeaderId",
                        column: x => x.QuoteRequestHeaderId,
                        principalTable: "QuoteRequestHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePurchaseOrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bonus = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    IVA = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: true),
                    PurchaseStateId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePurchaseOrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePurchaseOrderHeaders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePurchaseOrderHeaders_PurchaseStates_PurchaseStateId",
                        column: x => x.PurchaseStateId,
                        principalTable: "PurchaseStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePurchaseOrderHeaders_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subsidiaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubsidiaryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsidiaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subsidiaries_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StructureId = table.Column<int>(type: "int", nullable: true),
                    PriceAll = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceAllDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceAllObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceArg = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceArgDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceArgObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceMx = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceMxDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PriceMxObservations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Prices_Structures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StructureItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    VersionId = table.Column<int>(type: "int", nullable: false),
                    SonProductId = table.Column<int>(type: "int", nullable: true),
                    SonStructureId = table.Column<int>(type: "int", nullable: true),
                    StructureId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StructureItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StructureItems_Products_SonProductId",
                        column: x => x.SonProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructureItems_Structures_SonStructureId",
                        column: x => x.SonStructureId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructureItems_Structures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StructureItems_Versions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    StructureId = table.Column<int>(type: "int", nullable: true),
                    WorkOrderId = table.Column<int>(type: "int", nullable: false),
                    WorkOrderItemOfId = table.Column<int>(type: "int", nullable: true),
                    OrderStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_OrderStates_OrderStateId",
                        column: x => x.OrderStateId,
                        principalTable: "OrderStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_Structures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_WorkOrderItems_WorkOrderItemOfId",
                        column: x => x.WorkOrderItemOfId,
                        principalTable: "WorkOrderItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkOrderItems_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Communications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ComunicationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContactSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Incoming = table.Column<bool>(type: "bit", nullable: false),
                    Outgoing = table.Column<bool>(type: "bit", nullable: false),
                    ConsultedProduct = table.Column<bool>(type: "bit", nullable: false),
                    AnotherReason = table.Column<bool>(type: "bit", nullable: false),
                    PostSale = table.Column<bool>(type: "bit", nullable: false),
                    SaleOperationId = table.Column<int>(type: "int", nullable: true),
                    CommunicationNumber = table.Column<int>(type: "int", nullable: true),
                    CommunicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communications_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Communications_SaleOperations_SaleOperationId",
                        column: x => x.SaleOperationId,
                        principalTable: "SaleOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false, computedColumnSql: "[Id]"),
                    Taxes = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    SecureAndFreightCosts = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    TypeOfFreightAndSecure = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Packaging = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Transport = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PlaceOfDelivery = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OrderTotalPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    ValidOfferDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductionObservations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SaleObservations = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderStateId = table.Column<int>(type: "int", nullable: false),
                    Billed = table.Column<int>(type: "int", nullable: false),
                    User = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SaleOperationId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHeaders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHeaders_OrderStates_OrderStateId",
                        column: x => x.OrderStateId,
                        principalTable: "OrderStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderHeaders_SaleOperations_SaleOperationId",
                        column: x => x.SaleOperationId,
                        principalTable: "SaleOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IncomeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IncomeProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    OTNumber = table.Column<int>(type: "int", nullable: true),
                    ProductNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FatherProductId = table.Column<int>(type: "int", nullable: true),
                    FatherStructureId = table.Column<int>(type: "int", nullable: true),
                    OCNumber = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    IncomeStateId = table.Column<int>(type: "int", nullable: false),
                    IncomeHeaderId = table.Column<int>(type: "int", nullable: false),
                    IncomeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reception = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NextStation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeliveryNoteHeaderId = table.Column<int>(type: "int", nullable: true),
                    MissingProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomeDetails_DeliveryNoteHeaders_DeliveryNoteHeaderId",
                        column: x => x.DeliveryNoteHeaderId,
                        principalTable: "DeliveryNoteHeaders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeDetails_IncomeHeaders_IncomeHeaderId",
                        column: x => x.IncomeHeaderId,
                        principalTable: "IncomeHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncomeDetails_IncomeStates_IncomeStateId",
                        column: x => x.IncomeStateId,
                        principalTable: "IncomeStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeDetails_MissingProducts_MissingProductId",
                        column: x => x.MissingProductId,
                        principalTable: "MissingProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeDetails_Products_FatherProductId",
                        column: x => x.FatherProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeDetails_Products_IncomeProductId",
                        column: x => x.IncomeProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeDetails_Structures_FatherStructureId",
                        column: x => x.FatherStructureId,
                        principalTable: "Structures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IncomeDetails_UnitMeasures_UnitId",
                        column: x => x.UnitId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuoteRequestDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    QuoteRequestHeaderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    QuantityToOrder = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    QuantityToOrderUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    ProviderUnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    MissingProductId = table.Column<int>(type: "int", nullable: true),
                    PurchaseStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRequestDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteRequestDetails_MissingProducts_MissingProductId",
                        column: x => x.MissingProductId,
                        principalTable: "MissingProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_QuoteRequestDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuoteRequestDetails_PurchaseStates_PurchaseStateId",
                        column: x => x.PurchaseStateId,
                        principalTable: "PurchaseStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuoteRequestDetails_QuoteRequestHeaders_QuoteRequestHeaderId",
                        column: x => x.QuoteRequestHeaderId,
                        principalTable: "QuoteRequestHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteRequestDetails_UnitMeasures_ProviderUnitMeasureId",
                        column: x => x.ProviderUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuoteRequestDetails_UnitMeasures_QuantityToOrderUnitMeasureId",
                        column: x => x.QuantityToOrderUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    IVA = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    QuoteRequestResponseHeaderId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderHeaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeaders_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderHeaders_QuoteRequestResponseHeaders_QuoteRequestResponseHeaderId",
                        column: x => x.QuoteRequestResponseHeaderId,
                        principalTable: "QuoteRequestResponseHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuoteRequestResponseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MissingProductId = table.Column<int>(type: "int", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    AlternativeProductId = table.Column<int>(type: "int", nullable: true),
                    OriginalProviderQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    ProviderQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    ProviderUnitMeasureId = table.Column<int>(type: "int", nullable: false),
                    OriginalPriceQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceUnitMeasureId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    MoneyType = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    QuoteRequestResponseHeaderId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRequestResponseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuoteRequestResponseDetails_MissingProducts_MissingProductId",
                        column: x => x.MissingProductId,
                        principalTable: "MissingProducts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuoteRequestResponseDetails_Products_AlternativeProductId",
                        column: x => x.AlternativeProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuoteRequestResponseDetails_QuoteRequestResponseHeaders_QuoteRequestResponseHeaderId",
                        column: x => x.QuoteRequestResponseHeaderId,
                        principalTable: "QuoteRequestResponseHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteRequestResponseDetails_UnitMeasures_PriceUnitMeasureId",
                        column: x => x.PriceUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QuoteRequestResponseDetails_UnitMeasures_ProviderUnitMeasureId",
                        column: x => x.ProviderUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkActivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    WorkOrderItemId = table.Column<int>(type: "int", nullable: false),
                    ProductStationId = table.Column<int>(type: "int", nullable: false),
                    AssignedUsersIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ComebackDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextStation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateActivity = table.Column<bool>(type: "bit", nullable: false),
                    DateToProduction = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToShipments = table.Column<bool>(type: "bit", nullable: true),
                    OutsourcedProviderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkActivities_Providers_OutsourcedProviderId",
                        column: x => x.OutsourcedProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkActivities_RelProductStations_ProductStationId",
                        column: x => x.ProductStationId,
                        principalTable: "RelProductStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkActivities_WorkOrderItems_WorkOrderItemId",
                        column: x => x.WorkOrderItemId,
                        principalTable: "WorkOrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommunicationId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StructureId = table.Column<int>(type: "int", nullable: false),
                    StructureDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FunctionalityId = table.Column<int>(type: "int", nullable: false),
                    Functionality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PieceTypeId = table.Column<int>(type: "int", nullable: false),
                    PieceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultedProducts_Communications_CommunicationId",
                        column: x => x.CommunicationId,
                        principalTable: "Communications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    StructureId = table.Column<int>(type: "int", nullable: true),
                    SupplyVoltageId = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderStateId = table.Column<int>(type: "int", nullable: false),
                    PercentageOfTotalAdvance = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    DeliveredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SaleCategory = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    OrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    WorkOrderId = table.Column<int>(type: "int", nullable: true),
                    BelongToASale = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MissingProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    SaleOperationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderHeaders_OrderHeaderId",
                        column: x => x.OrderHeaderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_OrderStates_OrderStateId",
                        column: x => x.OrderStateId,
                        principalTable: "OrderStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_SaleOperations_SaleOperationId",
                        column: x => x.SaleOperationId,
                        principalTable: "SaleOperations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Structures_StructureId",
                        column: x => x.StructureId,
                        principalTable: "Structures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_SupplyVoltages_SupplyVoltageId",
                        column: x => x.SupplyVoltageId,
                        principalTable: "SupplyVoltages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderHeaderId = table.Column<int>(type: "int", nullable: false),
                    MissingProductId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    OriginalProviderQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    ProviderQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    ProviderUnitMeasureId = table.Column<int>(type: "int", nullable: false),
                    OriginalPriceQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PriceUnitMeasureId = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    MoneyType = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PurchasedQuantity = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    PurchaseStateId = table.Column<int>(type: "int", nullable: false),
                    IncomeDetailId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_IncomeDetails_IncomeDetailId",
                        column: x => x.IncomeDetailId,
                        principalTable: "IncomeDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_MissingProducts_MissingProductId",
                        column: x => x.MissingProductId,
                        principalTable: "MissingProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseOrderHeaders_PurchaseOrderHeaderId",
                        column: x => x.PurchaseOrderHeaderId,
                        principalTable: "PurchaseOrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_PurchaseStates_PurchaseStateId",
                        column: x => x.PurchaseStateId,
                        principalTable: "PurchaseStates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_UnitMeasures_PriceUnitMeasureId",
                        column: x => x.PriceUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderDetails_UnitMeasures_ProviderUnitMeasureId",
                        column: x => x.ProviderUnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DeliveryNoteDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductItemId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ConfigurationId = table.Column<int>(type: "int", nullable: true),
                    Package = table.Column<int>(type: "int", nullable: false),
                    PackageWeight = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WorkActivityId = table.Column<int>(type: "int", nullable: true),
                    DeliveryNoteHeaderId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryNoteDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryNoteDetails_DeliveryNoteHeaders_DeliveryNoteHeaderId",
                        column: x => x.DeliveryNoteHeaderId,
                        principalTable: "DeliveryNoteHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryNoteDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryNoteDetails_Products_ProductItemId",
                        column: x => x.ProductItemId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryNoteDetails_Structures_ConfigurationId",
                        column: x => x.ConfigurationId,
                        principalTable: "Structures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryNoteDetails_WorkActivities_WorkActivityId",
                        column: x => x.WorkActivityId,
                        principalTable: "WorkActivities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false),
                    WorkActivityId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkActions_WorkActivities_WorkActivityId",
                        column: x => x.WorkActivityId,
                        principalTable: "WorkActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDateHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<int>(type: "int", nullable: false),
                    OldDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewDeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDateHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryDateHistories_OrderDetails_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "OrderDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePurchaseOrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServicePOHeaderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    Bonus = table.Column<decimal>(type: "decimal(30,6)", nullable: false),
                    DeliveryNoteDetailId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConcurrencyToken = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePurchaseOrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServicePurchaseOrderDetails_DeliveryNoteDetails_DeliveryNoteDetailId",
                        column: x => x.DeliveryNoteDetailId,
                        principalTable: "DeliveryNoteDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicePurchaseOrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePurchaseOrderDetails_ServicePurchaseOrderHeaders_ServicePOHeaderId",
                        column: x => x.ServicePOHeaderId,
                        principalTable: "ServicePurchaseOrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePurchaseOrderDetails_UnitMeasures_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessoriesProducts_IdProduct",
                table: "AccessoriesProducts",
                column: "IdProduct");

            migrationBuilder.CreateIndex(
                name: "IX_AccessoriesProducts_IdProductAccessory",
                table: "AccessoriesProducts",
                column: "IdProductAccessory");

            migrationBuilder.CreateIndex(
                name: "IX_Archives_ArchiveTypeId",
                table: "Archives",
                column: "ArchiveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Archives_ProductId",
                table: "Archives",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BusinessName_DocumentNumber",
                table: "Clients",
                columns: new[] { "BusinessName", "DocumentNumber" },
                unique: true,
                filter: "[DocumentNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CityId",
                table: "Clients",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CountryId",
                table: "Clients",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_DocumentTypeId",
                table: "Clients",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_OperationStateId",
                table: "Clients",
                column: "OperationStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StateId",
                table: "Clients",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Communications_ClientId",
                table: "Communications",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Communications_SaleOperationId",
                table: "Communications",
                column: "SaleOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultedProducts_CommunicationId",
                table: "ConsultedProducts",
                column: "CommunicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_ProviderId",
                table: "Contacts",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDateHistories_OrderDetailId",
                table: "DeliveryDateHistories",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteDetails_ConfigurationId",
                table: "DeliveryNoteDetails",
                column: "ConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteDetails_DeliveryNoteHeaderId",
                table: "DeliveryNoteDetails",
                column: "DeliveryNoteHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteDetails_ProductId",
                table: "DeliveryNoteDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteDetails_ProductItemId",
                table: "DeliveryNoteDetails",
                column: "ProductItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteDetails_WorkActivityId",
                table: "DeliveryNoteDetails",
                column: "WorkActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteHeaders_ProviderId",
                table: "DeliveryNoteHeaders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryNoteHeaders_TransportProviderId",
                table: "DeliveryNoteHeaders",
                column: "TransportProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialInformations_ProviderId",
                table: "FinancialInformations",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupedWorkActions_StationId",
                table: "GroupedWorkActions",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_DeliveryNoteHeaderId",
                table: "IncomeDetails",
                column: "DeliveryNoteHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_FatherProductId",
                table: "IncomeDetails",
                column: "FatherProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_FatherStructureId",
                table: "IncomeDetails",
                column: "FatherStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_IncomeHeaderId",
                table: "IncomeDetails",
                column: "IncomeHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_IncomeProductId",
                table: "IncomeDetails",
                column: "IncomeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_IncomeStateId",
                table: "IncomeDetails",
                column: "IncomeStateId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_MissingProductId",
                table: "IncomeDetails",
                column: "MissingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeDetails_UnitId",
                table: "IncomeDetails",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeHeaders_ExternalProcessStationId",
                table: "IncomeHeaders",
                column: "ExternalProcessStationId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeHeaders_ProviderId",
                table: "IncomeHeaders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeHeaders_TransportProviderId",
                table: "IncomeHeaders",
                column: "TransportProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingProducts_ProductId",
                table: "MissingProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingProducts_ProviderId",
                table: "MissingProducts",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingProducts_QuantityToOrderUnitMeasureId",
                table: "MissingProducts",
                column: "QuantityToOrderUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingProducts_StateMissingProductId",
                table: "MissingProducts",
                column: "StateMissingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingProducts_StorageUnitMeasureId",
                table: "MissingProducts",
                column: "StorageUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderHeaderId",
                table: "OrderDetails",
                column: "OrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderStateId",
                table: "OrderDetails",
                column: "OrderStateId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SaleOperationId",
                table: "OrderDetails",
                column: "SaleOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_StructureId",
                table: "OrderDetails",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_SupplyVoltageId",
                table: "OrderDetails",
                column: "SupplyVoltageId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_WorkOrderId",
                table: "OrderDetails",
                column: "WorkOrderId",
                unique: true,
                filter: "[WorkOrderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_ClientId",
                table: "OrderHeaders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_OrderStateId",
                table: "OrderHeaders",
                column: "OrderStateId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeaders_SaleOperationId",
                table: "OrderHeaders",
                column: "SaleOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductId",
                table: "Prices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_StructureId",
                table: "Prices",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCutTypes_CutTypeId",
                table: "ProductCutTypes",
                column: "CutTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCutTypes_ProductId",
                table: "ProductCutTypes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPieceFormats_PieceFormatId",
                table: "ProductPieceFormats",
                column: "PieceFormatId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPieceFormats_ProductId",
                table: "ProductPieceFormats",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPieceTypes_PieceTypeId",
                table: "ProductPieceTypes",
                column: "PieceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPieceTypes_ProductId",
                table: "ProductPieceTypes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Code",
                table: "Products",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ExistenceUnitMeasureId",
                table: "Products",
                column: "ExistenceUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IVATypeId",
                table: "Products",
                column: "IVATypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LocationStationId",
                table: "Products",
                column: "LocationStationId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MinimumUnitMeasureId",
                table: "Products",
                column: "MinimumUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OperationId",
                table: "Products",
                column: "OperationId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductFeatureId",
                table: "Products",
                column: "ProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_QuantityToOrderUnitMeasureId",
                table: "Products",
                column: "QuantityToOrderUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockHeightUnitMeasureId",
                table: "Products",
                column: "StockHeightUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockLengthUnitMeasureId",
                table: "Products",
                column: "StockLengthUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockQuantityUnitMeasureId",
                table: "Products",
                column: "StockQuantityUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockUnitMeasureId",
                table: "Products",
                column: "StockUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockWeightUnitMeasureId",
                table: "Products",
                column: "StockWeightUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockWidthUnitMeasureId",
                table: "Products",
                column: "StockWidthUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StorageUnitMeasureId",
                table: "Products",
                column: "StorageUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitMeasureId",
                table: "Products",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplyVoltages_ProductId",
                table: "ProductSupplyVoltages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplyVoltages_SupplyVoltageId",
                table: "ProductSupplyVoltages",
                column: "SupplyVoltageId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_CityId",
                table: "Providers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_CountryId",
                table: "Providers",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_DocumentTypeId",
                table: "Providers",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_IVAConditionId",
                table: "Providers",
                column: "IVAConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_StateId",
                table: "Providers",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_IncomeDetailId",
                table: "PurchaseOrderDetails",
                column: "IncomeDetailId",
                unique: true,
                filter: "[IncomeDetailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_MissingProductId",
                table: "PurchaseOrderDetails",
                column: "MissingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PriceUnitMeasureId",
                table: "PurchaseOrderDetails",
                column: "PriceUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_ProductId",
                table: "PurchaseOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_ProviderUnitMeasureId",
                table: "PurchaseOrderDetails",
                column: "ProviderUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseOrderHeaderId",
                table: "PurchaseOrderDetails",
                column: "PurchaseOrderHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderDetails_PurchaseStateId",
                table: "PurchaseOrderDetails",
                column: "PurchaseStateId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeaders_ProviderId",
                table: "PurchaseOrderHeaders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderHeaders_QuoteRequestResponseHeaderId",
                table: "PurchaseOrderHeaders",
                column: "QuoteRequestResponseHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestDetails_MissingProductId",
                table: "QuoteRequestDetails",
                column: "MissingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestDetails_ProductId",
                table: "QuoteRequestDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestDetails_ProviderUnitMeasureId",
                table: "QuoteRequestDetails",
                column: "ProviderUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestDetails_PurchaseStateId",
                table: "QuoteRequestDetails",
                column: "PurchaseStateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestDetails_QuantityToOrderUnitMeasureId",
                table: "QuoteRequestDetails",
                column: "QuantityToOrderUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestDetails_QuoteRequestHeaderId",
                table: "QuoteRequestDetails",
                column: "QuoteRequestHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResponseDetails_AlternativeProductId",
                table: "QuoteRequestResponseDetails",
                column: "AlternativeProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResponseDetails_MissingProductId",
                table: "QuoteRequestResponseDetails",
                column: "MissingProductId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResponseDetails_PriceUnitMeasureId",
                table: "QuoteRequestResponseDetails",
                column: "PriceUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResponseDetails_ProviderUnitMeasureId",
                table: "QuoteRequestResponseDetails",
                column: "ProviderUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResponseDetails_QuoteRequestResponseHeaderId",
                table: "QuoteRequestResponseDetails",
                column: "QuoteRequestResponseHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResponseHeaders_ProviderId",
                table: "QuoteRequestResponseHeaders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteRequestResponseHeaders_QuoteRequestHeaderId",
                table: "QuoteRequestResponseHeaders",
                column: "QuoteRequestHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProductStations_ProductId",
                table: "RelProductStations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProductStations_StationId",
                table: "RelProductStations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_HeightUnitMeasureId",
                table: "RelProviderProducts",
                column: "HeightUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_LengthUnitMeasureId",
                table: "RelProviderProducts",
                column: "LengthUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_PresentationUnitMeasureId",
                table: "RelProviderProducts",
                column: "PresentationUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_PriceUnitMeasureId",
                table: "RelProviderProducts",
                column: "PriceUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_ProductId",
                table: "RelProviderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_ProviderId",
                table: "RelProviderProducts",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_UnitMeasureId",
                table: "RelProviderProducts",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_WeightUnitMeasureId",
                table: "RelProviderProducts",
                column: "WeightUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderProducts_WidthUnitMeasureId",
                table: "RelProviderProducts",
                column: "WidthUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderStations_PriceUnitMeasureId",
                table: "RelProviderStations",
                column: "PriceUnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderStations_ProviderId",
                table: "RelProviderStations",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_RelProviderStations_StationId",
                table: "RelProviderStations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_RelQuoteRequestProviders_ProviderId",
                table: "RelQuoteRequestProviders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_RelQuoteRequestProviders_QuoteRequestHeaderId",
                table: "RelQuoteRequestProviders",
                column: "QuoteRequestHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_ClientId",
                table: "Reminders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOperations_ClientId",
                table: "SaleOperations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePurchaseOrderDetails_DeliveryNoteDetailId",
                table: "ServicePurchaseOrderDetails",
                column: "DeliveryNoteDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePurchaseOrderDetails_ProductId",
                table: "ServicePurchaseOrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePurchaseOrderDetails_ServicePOHeaderId",
                table: "ServicePurchaseOrderDetails",
                column: "ServicePOHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePurchaseOrderDetails_UnitMeasureId",
                table: "ServicePurchaseOrderDetails",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePurchaseOrderHeaders_ProviderId",
                table: "ServicePurchaseOrderHeaders",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePurchaseOrderHeaders_PurchaseStateId",
                table: "ServicePurchaseOrderHeaders",
                column: "PurchaseStateId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePurchaseOrderHeaders_StationId",
                table: "ServicePurchaseOrderHeaders",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_States_CountryId",
                table: "States",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_FunctionalAreaId",
                table: "Stations",
                column: "FunctionalAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_SonProductId",
                table: "StructureItems",
                column: "SonProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_SonStructureId",
                table: "StructureItems",
                column: "SonStructureId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_StructureId",
                table: "StructureItems",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_StructureItems_VersionId",
                table: "StructureItems",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_LastVersionId",
                table: "Structures",
                column: "LastVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_ProductId",
                table: "Structures",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Structures_SupplyVoltageId",
                table: "Structures",
                column: "SupplyVoltageId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_CityId",
                table: "Subsidiaries",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_CountryId",
                table: "Subsidiaries",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_ProviderId",
                table: "Subsidiaries",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Subsidiaries_StateId",
                table: "Subsidiaries",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkActions_WorkActivityId",
                table: "WorkActions",
                column: "WorkActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkActivities_OutsourcedProviderId",
                table: "WorkActivities",
                column: "OutsourcedProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkActivities_ProductStationId",
                table: "WorkActivities",
                column: "ProductStationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkActivities_WorkOrderItemId",
                table: "WorkActivities",
                column: "WorkOrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_OrderStateId",
                table: "WorkOrderItems",
                column: "OrderStateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_ProductId",
                table: "WorkOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_StructureId",
                table: "WorkOrderItems",
                column: "StructureId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_WorkOrderId",
                table: "WorkOrderItems",
                column: "WorkOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderItems_WorkOrderItemOfId",
                table: "WorkOrderItems",
                column: "WorkOrderItemOfId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkOrderHeaderId",
                table: "WorkOrders",
                column: "WorkOrderHeaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessoriesProducts");

            migrationBuilder.DropTable(
                name: "Archives");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "ConsultedProducts");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "DeliveryDateHistories");

            migrationBuilder.DropTable(
                name: "DollarExchangeRates");

            migrationBuilder.DropTable(
                name: "FinancialInformations");

            migrationBuilder.DropTable(
                name: "GroupedWorkActions");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "ProductCutTypes");

            migrationBuilder.DropTable(
                name: "ProductPieceFormats");

            migrationBuilder.DropTable(
                name: "ProductPieceTypes");

            migrationBuilder.DropTable(
                name: "ProductSupplyVoltages");

            migrationBuilder.DropTable(
                name: "PurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "QuoteRequestDetails");

            migrationBuilder.DropTable(
                name: "QuoteRequestResponseDetails");

            migrationBuilder.DropTable(
                name: "RelProviderProducts");

            migrationBuilder.DropTable(
                name: "RelProviderStations");

            migrationBuilder.DropTable(
                name: "RelQuoteRequestProviders");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "ServicePurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "StructureItems");

            migrationBuilder.DropTable(
                name: "Subsidiaries");

            migrationBuilder.DropTable(
                name: "WorkActions");

            migrationBuilder.DropTable(
                name: "ArchiveTypes");

            migrationBuilder.DropTable(
                name: "Communications");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "CutTypes");

            migrationBuilder.DropTable(
                name: "PieceFormats");

            migrationBuilder.DropTable(
                name: "PieceTypes");

            migrationBuilder.DropTable(
                name: "IncomeDetails");

            migrationBuilder.DropTable(
                name: "PurchaseOrderHeaders");

            migrationBuilder.DropTable(
                name: "DeliveryNoteDetails");

            migrationBuilder.DropTable(
                name: "ServicePurchaseOrderHeaders");

            migrationBuilder.DropTable(
                name: "OrderHeaders");

            migrationBuilder.DropTable(
                name: "IncomeHeaders");

            migrationBuilder.DropTable(
                name: "IncomeStates");

            migrationBuilder.DropTable(
                name: "MissingProducts");

            migrationBuilder.DropTable(
                name: "QuoteRequestResponseHeaders");

            migrationBuilder.DropTable(
                name: "DeliveryNoteHeaders");

            migrationBuilder.DropTable(
                name: "WorkActivities");

            migrationBuilder.DropTable(
                name: "SaleOperations");

            migrationBuilder.DropTable(
                name: "PurchaseStates");

            migrationBuilder.DropTable(
                name: "QuoteRequestHeaders");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "RelProductStations");

            migrationBuilder.DropTable(
                name: "WorkOrderItems");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "OrderStates");

            migrationBuilder.DropTable(
                name: "Structures");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "OperationStates");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SupplyVoltages");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "WorkOrderHeader");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "IVAConditions");

            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "UnitMeasures");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "FunctionalAreas");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
