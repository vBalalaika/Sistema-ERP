using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Enums;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail.Models;
using EntityFramework.Exceptions.SqlServer;
using ERP.Application.Interfaces.Contexts;
using ERP.Application.Interfaces.Shared;
using ERP.Domain.Common;
using ERP.Domain.Entities.Clients;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.Commons;
using ERP.Domain.Entities.Lists;
using ERP.Domain.Entities.Logistics.Incomes;
using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.PurchaseOrders;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using ERP.Domain.Entities.Sales;
using ERP.Infrastructure.EntitiesProfile.Clients;
using ERP.Infrastructure.EntitiesProfile.CommercialDocuments.DeliveryNote;
using ERP.Infrastructure.EntitiesProfile.Commons;
using ERP.Infrastructure.EntitiesProfile.Lists;
using ERP.Infrastructure.EntitiesProfile.Logistics.Incomes;
using ERP.Infrastructure.EntitiesProfile.Production;
using ERP.Infrastructure.EntitiesProfile.ProductMod;
using ERP.Infrastructure.EntitiesProfile.Purchases.MissingProducts;
using ERP.Infrastructure.EntitiesProfile.Purchases.Providers;
using ERP.Infrastructure.EntitiesProfile.Purchases.PurchaseOrders;
using ERP.Infrastructure.EntitiesProfile.Purchases.QuoteRequests;
using ERP.Infrastructure.EntitiesProfile.Sales;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ERP.Infrastructure.DbContexts
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        #region Products

        public DbSet<Audit> AuditLogs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<AccessoryProduct> AccessoriesProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<RelProductStation> RelProductStations { get; set; }
        public DbSet<Structure> Structures { get; set; }
        public DbSet<StructureItem> StructureItems { get; set; }
        public DbSet<StructureVersion> Versions { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<UnitMeasure> UnitMeasures { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<PieceType> PieceTypes { get; set; }
        public DbSet<PieceFormat> PieceFormats { get; set; }
        public DbSet<CutType> CutTypes { get; set; }
        public DbSet<SupplyVoltage> SupplyVoltages { get; set; }
        public DbSet<ProductCutType> ProductCutTypes { get; set; }
        public DbSet<ProductPieceFormat> ProductPieceFormats { get; set; }
        public DbSet<ProductPieceType> ProductPieceTypes { get; set; }
        public DbSet<ProductSupplyVoltage> ProductSupplyVoltages { get; set; }
        public DbSet<Archive> Archives { get; set; }
        public DbSet<ArchiveType> ArchiveTypes { get; set; }
        public DbSet<FunctionalArea> FunctionalAreas { get; set; }

        #endregion Products

        #region Clients

        public DbSet<Client> Clients { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Communication> Communications { get; set; }
        public DbSet<ConsultedProduct> ConsultedProducts { get; set; }
        public DbSet<SaleOperation> SaleOperations { get; set; }

        #endregion Clients

        #region Lists

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        #endregion Lists

        #region Commons

        public DbSet<Country> Countries { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<OperationState> OperationStates { get; set; }
        public DbSet<IVACondition> IVAConditions { get; set; }
        public DbSet<DollarExchangeRate> DollarExchangeRates { get; set; }

        #endregion Commons

        #region Purchases

        #region Providers

        public DbSet<Provider> Providers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Subsidiary> Subsidiaries { get; set; }
        public DbSet<FinancialInformation> FinancialInformations { get; set; }
        public DbSet<RelProviderProduct> RelProviderProducts { get; set; }
        public DbSet<RelProviderStation> RelProviderStations { get; set; }

        #endregion Providers

        #region MissingProducts

        public DbSet<MissingProduct> MissingProducts { get; set; }
        public DbSet<PurchaseState> PurchaseStates { get; set; }

        #endregion MissingProducts

        #region PurchaseOrders

        public DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public DbSet<ServicePOHeader> ServicePurchaseOrderHeaders { get; set; }
        public DbSet<ServicePODetail> ServicePurchaseOrderDetails { get; set; }

        #endregion PurchaseOrders

        #region QuoteRequests

        public DbSet<QuoteRequestHeader> QuoteRequestHeaders { get; set; }
        public DbSet<QuoteRequestDetail> QuoteRequestDetails { get; set; }
        public DbSet<RelQuoteRequestProvider> RelQuoteRequestProviders { get; set; }

        public DbSet<QuoteRequestResponseHeader> QuoteRequestResponseHeaders { get; set; }
        public DbSet<QuoteRequestResponseDetail> QuoteRequestResponseDetails { get; set; }

        #endregion QuoteRequests

        #endregion Purchases

        #region Sales

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<DeliveryDateHistory> DeliveryDateHistories { get; set; }

        #endregion Sales

        #region Production

        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderItem> WorkOrderItems { get; set; }
        public DbSet<WorkActivity> WorkActivities { get; set; }
        public DbSet<WorkAction> WorkActions { get; set; }
        public DbSet<GroupedWorkAction> GroupedWorkActions { get; set; }

        #endregion Production

        #region Logistics

        #region Incomes

        public DbSet<IncomeHeader> IncomeHeaders { get; set; }
        public DbSet<IncomeDetail> IncomeDetails { get; set; }
        public DbSet<IncomeState> IncomeStates { get; set; }

        #endregion

        #endregion Sales

        #region CommercialDocuments

        #region DeliveryNote

        public DbSet<DeliveryNoteHeader> DeliveryNoteHeaders { get; set; }
        public DbSet<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }

        #endregion

        #endregion

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        private void SetConcurrencyToken()
        {
            int token = new Random().Next(1000);
            foreach (var entry in ChangeTracker.Entries<IConcurrencyToken>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.ConcurrencyToken = token;
                        break;
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AuditEntities();
            SetConcurrencyToken();

            if (_authenticatedUser.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                List<AuditEntry> auditEntries = OnBeforeSaveChanges(_authenticatedUser.UserId);
                var result = await base.SaveChangesAsync();
                await OnAfterSaveChanges(auditEntries);
                return result;
            }
        }

        private void AuditEntities()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.Now;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.Now;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
        }

        private List<AuditEntry> OnBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = userId;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            if (entry.Entity is IAuditableBaseEntity)
                            {
                                ((IAuditableBaseEntity)entry.Entity).CreatedBy = userId;
                                ((IAuditableBaseEntity)entry.Entity).CreatedOn = _dateTime.Now;
                                ((IAuditableBaseEntity)entry.Entity).LastModifiedBy = userId;
                                ((IAuditableBaseEntity)entry.Entity).LastModifiedOn = _dateTime.Now;
                            }
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }
                            if (entry.Entity is IAuditableBaseEntity)
                            {
                                ((IAuditableBaseEntity)entry.Entity).LastModifiedBy = userId;
                                ((IAuditableBaseEntity)entry.Entity).LastModifiedOn = _dateTime.Now;
                            }
                            break;
                    }
                }
            }
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            foreach (var auditEntry in auditEntries)
            {
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
                AuditLogs.Add(auditEntry.ToAudit());
            }
            return SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SetDecimalPrecision(builder);
            // ConfigureEntities debe ir despues de setDecimalPrecision para sobre escribir los valores
            ConfigureEntities(builder);

            #region Lists

            builder.Entity<City>()
                .HasOne<State>(c => c.State)
                .WithMany(s => s.Cities)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion Lists

            #region Clients

            //builder.Entity<Client>()
            //    .HasIndex(c => c.BusinessName)
            //    .IsUnique();
            //builder.Entity<Client>()
            //    .HasIndex(c => c.DocumentNumber)
            //    .IsUnique();

            builder.Entity<Client>()
                .HasIndex(c => new { c.BusinessName, c.DocumentNumber }).IsUnique();

            builder.Entity<Client>()
                .HasOne(pt => pt.State)
                .WithMany()
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Client>()
                .HasOne(pt => pt.City)
                .WithMany()
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Client>()
                .HasOne(pt => pt.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Communication>()
                   .HasOne(com => com.Client)
                   .WithMany(com => com.Communications)
                   .HasForeignKey(com => com.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);


            #endregion Clients

            #region Purchases

            #region Providers

            builder.Entity<Provider>()
                .HasOne(pt => pt.State)
                .WithMany()
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Provider>()
                .HasOne(pt => pt.City)
                .WithMany()
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Provider>()
                .HasOne(pt => pt.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subsidiary>()
                .HasOne(pt => pt.State)
                .WithMany()
                .HasForeignKey(p => p.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subsidiary>()
                .HasOne(pt => pt.City)
                .WithMany()
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Subsidiary>()
                .HasOne(pt => pt.Country)
                .WithMany()
                .HasForeignKey(p => p.CountryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RelProviderProduct>()
                .HasOne(p => p.Product)
                .WithMany(rpp => rpp.RelProviderProducts)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<RelProviderProduct>()
                .HasOne(p => p.Provider)
                .WithMany(rpp => rpp.RelProviderProducts)
                .HasForeignKey(p => p.ProviderId);

            builder.Entity<RelProviderStation>()
                .HasOne(rps => rps.Station)
                .WithMany(st => st.RelProviderStations)
                .HasForeignKey(rps => rps.StationId);

            builder.Entity<RelProviderStation>()
                .HasOne(rps => rps.Provider)
                .WithMany(pr => pr.RelProviderStations)
                .HasForeignKey(rps => rps.ProviderId);


            #endregion Providers

            #region MissingProducts

            builder.Entity<MissingProduct>()
                .HasOne(mp => mp.Product)
                .WithMany()
                .HasForeignKey(mp => mp.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<MissingProduct>()
                .HasOne(mp => mp.Provider)
                .WithMany()
                .HasForeignKey(mp => mp.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<MissingProduct>()
                .HasOne(mp => mp.StateMissingProduct)
                .WithMany()
                .HasForeignKey(mp => mp.StateMissingProductId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion MissingProducts

            #region PurchaseOrders

            builder.Entity<PurchaseOrderHeader>()
                .HasOne(mp => mp.Provider)
                .WithMany()
                .HasForeignKey(mp => mp.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PurchaseOrderDetail>()
                .HasOne(mp => mp.PurchaseState)
                .WithMany()
                .HasForeignKey(mp => mp.PurchaseStateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PurchaseOrderDetail>()
                .HasOne(mp => mp.MissingProduct)
                .WithMany()
                .HasForeignKey(mp => mp.MissingProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PurchaseOrderDetail>()
                .HasOne(mp => mp.ProviderUnitMeasure)
                .WithMany()
                .HasForeignKey(mp => mp.ProviderUnitMeasureId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PurchaseOrderDetail>()
                .HasOne(mp => mp.PriceUnitMeasure)
                .WithMany()
                .HasForeignKey(mp => mp.PriceUnitMeasureId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<PurchaseOrderDetail>()
                .HasOne(mp => mp.MissingProduct)
                .WithMany()
                .HasForeignKey(mp => mp.MissingProductId)
                .OnDelete(DeleteBehavior.SetNull);

            #endregion PurchaseOrders

            #region QuoteRequests

            builder.Entity<QuoteRequestDetail>()
              .HasOne(mp => mp.PurchaseState)
              .WithMany()
              .HasForeignKey(mp => mp.PurchaseStateId)
              .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<QuoteRequestDetail>()
                .HasOne(mp => mp.Product)
                .WithMany()
                .HasForeignKey(mp => mp.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<QuoteRequestDetail>()
                .HasOne(mp => mp.ProviderUnitMeasure)
                .WithMany()
                .HasForeignKey(mp => mp.ProviderUnitMeasureId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<QuoteRequestDetail>()
                .HasOne(mp => mp.MissingProduct)
                .WithMany()
                .HasForeignKey(mp => mp.MissingProductId)
                .OnDelete(DeleteBehavior.SetNull);

            // M2M QuoteRequest with Provider
            builder.Entity<RelQuoteRequestProvider>()
                .HasOne(qrp => qrp.QuoteRequestHeader)
                .WithMany(qr => qr.RelQuoteRequestProviders)
                .HasForeignKey(qrp => qrp.QuoteRequestHeaderId);
            builder.Entity<RelQuoteRequestProvider>()
                .HasOne(qrp => qrp.Provider)
                .WithMany(p => p.RelQuoteRequestProviders)
                .HasForeignKey(qrp => qrp.ProviderId);

            //builder.Entity<QuoteRequestResponseHeader>()
            //    .HasOne(qrrh => qrrh.QuoteRequestHeader)
            //    .WithMany()
            //    .HasForeignKey(qrrh => qrrh.QuoteRequestHeaderId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<QuoteRequestResponseHeader>()
                .HasOne(qrrh => qrrh.QuoteRequestHeader)
                .WithMany(qrh => qrh.QuoteRequestResponseHeaders)
                .HasForeignKey(qrrh => qrrh.QuoteRequestHeaderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<QuoteRequestResponseHeader>()
                .HasOne(qrrh => qrrh.Provider)
                .WithMany()
                .HasForeignKey(qrrh => qrrh.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<QuoteRequestResponseDetail>()
               .HasOne(qrrd => qrrd.PriceUnitMeasure)
               .WithMany()
               .HasForeignKey(qrrd => qrrd.PriceUnitMeasureId)
               .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<QuoteRequestResponseDetail>()
               .HasOne(qrrd => qrrd.ProviderUnitMeasure)
               .WithMany()
               .HasForeignKey(qrrd => qrrd.ProviderUnitMeasureId)
               .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<QuoteRequestResponseDetail>()
                .HasOne(mp => mp.MissingProduct)
                .WithMany()
                .HasForeignKey(mp => mp.MissingProductId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion QuoteRequests

            #endregion Purchases

            #region Products

            builder.Entity<AccessoryProduct>()
                .HasOne(pt => pt.ProductAccessory)
                .WithMany(p => p.AccessoryProductsOf)
                .HasForeignKey(pt => pt.IdProductAccessory)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccessoryProduct>()
                .HasOne(pt => pt.Product)
                .WithMany(t => t.AccessoryProducts)
                .HasForeignKey(pt => pt.IdProduct);

            builder.Entity<RelProductStation>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.RelProductStations)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<RelProductStation>()
                .HasOne(pt => pt.Station)
                .WithMany(t => t.RelProductStations)
                .HasForeignKey(pt => pt.StationId);

            builder.Entity<ProductCutType>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductCutTypes)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProductCutType>()
                .HasOne(pt => pt.CutType)
                .WithMany(t => t.ProductsCutType)
                .HasForeignKey(pt => pt.CutTypeId);

            builder.Entity<ProductPieceFormat>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductPieceFormats)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProductPieceFormat>()
                .HasOne(pt => pt.PieceFormat)
                .WithMany(t => t.ProductsPieceFormat)
                .HasForeignKey(pt => pt.PieceFormatId);

            builder.Entity<ProductPieceType>()
               .HasOne(pt => pt.Product)
               .WithMany(p => p.ProductPieceTypes)
               .HasForeignKey(pt => pt.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProductPieceType>()
                .HasOne(pt => pt.PieceType)
                .WithMany(t => t.ProductsPieceType)
                .HasForeignKey(pt => pt.PieceTypeId);

            builder.Entity<ProductSupplyVoltage>()
               .HasOne(pt => pt.Product)
               .WithMany(p => p.ProductSupplyVoltages)
               .HasForeignKey(pt => pt.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ProductSupplyVoltage>()
                .HasOne(pt => pt.SupplyVoltage)
                .WithMany(t => t.ProductSupplyVoltage)
                .HasForeignKey(pt => pt.SupplyVoltageId);

            builder.Entity<StructureItem>()
                .HasOne(p => p.Structure)
                .WithMany(b => b.StructureItems)
                .HasForeignKey(p => p.StructureId);

            builder.Entity<Product>()
                .HasIndex(u => u.Code)
                .IsUnique();


            #endregion Products

            #region Sales

            //OrderHeader - Client
            builder.Entity<OrderHeader>()
                .HasOne(soh => soh.Client)
                .WithMany(soh => soh.OrderHeaders)
                .HasForeignKey(soh => soh.ClientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<OrderHeader>()
                .HasOne(soh => soh.OrderState)
                .WithMany()
                .HasForeignKey(soh => soh.OrderStateId)
                .OnDelete(DeleteBehavior.NoAction);
            //OrderHeader - OrderDetails
            builder.Entity<OrderHeader>()
                .HasMany(soh => soh.OrderDetails)
                .WithOne(soh => soh.OrderHeader)
                .HasForeignKey(soh => soh.OrderHeaderId)
                .OnDelete(DeleteBehavior.Cascade);
            //OrderHeader - Product
            builder.Entity<OrderDetail>()
                .HasOne(sod => sod.Product)
                .WithMany()
                .HasForeignKey(sod => sod.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            //OrderDetail - Structure
            builder.Entity<OrderDetail>()
                .HasOne(sod => sod.Structure)
                .WithMany()
                .HasForeignKey(p => p.StructureId)
                .OnDelete(DeleteBehavior.NoAction);

            // Price -> Product
            builder.Entity<Price>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.NoAction);
            // Price -> Structure
            builder.Entity<Price>()
                .HasOne(p => p.Structure)
                .WithMany()
                .HasForeignKey(p => p.StructureId)
                .OnDelete(DeleteBehavior.NoAction);

            // OrderDetail - WorkOrder
            builder.Entity<OrderDetail>()
                .HasOne(od => od.WorkOrder)
                .WithOne(wo => wo.OrderDetail)
                .HasForeignKey<OrderDetail>(od => od.WorkOrderId)
                .OnDelete(DeleteBehavior.SetNull);

            //builder.Entity<OrderDetail>()
            //    .Property(od => od.BelongToASale)
            //    .HasDefaultValue(false);

            builder.Entity<OrderDetail>()
                .Property(od => od.BelongsToASaleOrder)
                .HasDefaultValue(false);

            builder.Entity<OrderDetail>()
                .Property(od => od.BelongsToAProductionOrder)
                .HasDefaultValue(false);

            #endregion Sales

            #region Production

            //WorkOrderHeader - WorkOrder
            builder.Entity<WorkOrderHeader>()
               .HasMany(woh => woh.WorkOrders)
               .WithOne(wo => wo.WorkOrderHeader)
               .HasForeignKey(wo => wo.WorkOrderHeaderId)
               .OnDelete(DeleteBehavior.Cascade);

            //WorkOrder - OrderDetail
            //builder.Entity<WorkOrder>()
            //    .HasOne(wo => wo.OrderDetail)
            //    .WithOne(od => od.WorkOrder)
            //    .HasForeignKey<WorkOrder>(wo => wo.OrderDetailId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //WorkOrder - WorkOrderItem
            builder.Entity<WorkOrder>()
               .HasMany(wo => wo.WorkOrderItems)
               .WithOne(od => od.WorkOrder)
               .HasForeignKey(wo => wo.WorkOrderId)
               .OnDelete(DeleteBehavior.Cascade);

            //WorkOrderItem - WorkActivity
            builder.Entity<WorkOrderItem>()
               .HasMany(wo => wo.WorkActivities)
               .WithOne(od => od.WorkOrderItem)
               .HasForeignKey(wo => wo.WorkOrderItemId)
               .OnDelete(DeleteBehavior.Cascade);

            //WorkActivity - WorkActions
            builder.Entity<WorkActivity>()
              .HasMany(wa => wa.WorkActions)
              .WithOne(wa => wa.WorkActivity)
              .HasForeignKey(wo => wo.WorkActivityId)
              .OnDelete(DeleteBehavior.Cascade);

            //WorkOrderItem - WorkOrderItemOf
            builder.Entity<WorkOrderItem>()
                .HasOne(woi => woi.WorkOrderItemOf)
                .WithMany(woi => woi.ChildsWorkOrderItems)
                .HasForeignKey(woi => woi.WorkOrderItemOfId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion Production

            #region Logistics

            #region Incomes

            builder.Entity<IncomeHeader>()
                .HasOne(ih => ih.Provider)
                .WithMany()
                .HasForeignKey(ih => ih.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeHeader>()
                .HasOne(ih => ih.TransportProvider)
                .WithMany()
                .HasForeignKey(ih => ih.TransportProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeHeader>()
                .HasOne(ih => ih.ExternalProcessStation)
                .WithMany()
                .HasForeignKey(ih => ih.ExternalProcessStationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeDetail>()
                .HasOne(id => id.IncomeProduct)
                .WithMany()
                .HasForeignKey(id => id.IncomeProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeDetail>()
                .HasOne(id => id.FatherProduct)
                .WithMany()
                .HasForeignKey(id => id.FatherProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeDetail>()
                .HasOne(id => id.FatherStructure)
                .WithMany()
                .HasForeignKey(id => id.FatherStructureId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeDetail>()
                .HasOne(id => id.IncomeState)
                .WithMany()
                .HasForeignKey(id => id.IncomeStateId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeDetail>()
                .HasOne(id => id.PurchaseOrderDetail)
                .WithOne(pod => pod.IncomeDetail)
                .HasForeignKey<PurchaseOrderDetail>(pod => pod.IncomeDetailId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<IncomeDetail>()
                .HasOne(id => id.DeliveryNoteHeader)
                .WithMany()
                .HasForeignKey(id => id.DeliveryNoteHeaderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<IncomeDetail>()
                .HasOne(id => id.MissingProduct)
                .WithMany()
                .HasForeignKey(id => id.MissingProductId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #endregion

            #region CommercialDocuments

            #region DeliveryNote

            builder.Entity<DeliveryNoteHeader>()
                .HasMany(dnh => dnh.DeliveryNoteDetails)
                .WithOne(dnh => dnh.DeliveryNoteHeader)
                .HasForeignKey(dnh => dnh.DeliveryNoteHeaderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DeliveryNoteHeader>()
                .HasOne(dnh => dnh.Provider)
                .WithMany()
                .HasForeignKey(dnh => dnh.ProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DeliveryNoteHeader>()
                .HasOne(dnh => dnh.TransportProvider)
                .WithMany()
                .HasForeignKey(dnh => dnh.TransportProviderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DeliveryNoteDetail>()
                .HasOne(dnd => dnd.ProductItem)
                .WithMany()
                .HasForeignKey(dnd => dnd.ProductItemId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DeliveryNoteDetail>()
                .HasOne(dnd => dnd.Product)
                .WithMany()
                .HasForeignKey(dnd => dnd.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DeliveryNoteDetail>()
                .HasOne(dnd => dnd.Configuration)
                .WithMany()
                .HasForeignKey(dnd => dnd.ConfigurationId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<DeliveryNoteDetail>()
                .HasOne(dnd => dnd.WorkActivity)
                .WithMany()
                .HasForeignKey(dnd => dnd.WorkActivityId)
                .OnDelete(DeleteBehavior.NoAction);

            #endregion

            #endregion

            base.OnModelCreating(builder);
        }

        // Fran 2/12/2022: to handle exceptions with EntityFramework.Exceptions library (https://github.com/Giorgi/EntityFramework.Exceptions) 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseExceptionProcessor();
        }

        private static void ConfigureEntities(ModelBuilder builder)
        {
            #region Products
            builder.ApplyConfiguration<Product>(new ProductEntityProfile());
            #endregion

            #region Commons
            builder.ApplyConfiguration<UnitMeasure>(new UnitMeasureEntityProfile());
            #endregion

            #region Clients

            builder.ApplyConfiguration<Client>(new ClientEntityProfile());
            builder.ApplyConfiguration<Reminder>(new ReminderEntityProfile());
            builder.ApplyConfiguration<Communication>(new CommunicationEntityProfile());
            builder.ApplyConfiguration<ConsultedProduct>(new ConsultedProductEntityProfile());
            builder.ApplyConfiguration<SaleOperation>(new SaleOperationEntityProfile());

            #endregion Clients

            #region Lists

            builder.ApplyConfiguration<State>(new StateEntityProfile());
            builder.ApplyConfiguration<City>(new CityEntityProfile());

            #endregion Lists

            #region Purchases

            #region Providers

            builder.ApplyConfiguration<Provider>(new ProviderEntityProfile());
            builder.ApplyConfiguration<Contact>(new ContactEntityProfile());
            builder.ApplyConfiguration<Subsidiary>(new SubsidiaryEntityProfile());
            builder.ApplyConfiguration<FinancialInformation>(new FinancialInformationEntityProfile());

            #endregion Providers

            #region MissingProducts

            builder.ApplyConfiguration<MissingProduct>(new MissingProductEntityProfile());

            #endregion MissingProducts

            #region PurchaseOrders

            builder.ApplyConfiguration<PurchaseOrderHeader>(new PurchaseOrderHeaderEntityProfile());
            builder.ApplyConfiguration<PurchaseOrderDetail>(new PurchaseOrderDetailEntityProfile());

            builder.ApplyConfiguration<ServicePOHeader>(new ServicePOHeaderEntityProfile());
            builder.ApplyConfiguration<ServicePODetail>(new ServicePODetailEntityProfile());

            #endregion PurchaseOrders

            #region QuoteRequests

            builder.ApplyConfiguration<QuoteRequestHeader>(new QuoteRequestHeaderEntityProfile());
            builder.ApplyConfiguration<QuoteRequestDetail>(new QuoteRequestDetailEntityProfile());
            builder.ApplyConfiguration<RelQuoteRequestProvider>(new RelQuoteRequestProviderEntityProfile());

            builder.ApplyConfiguration<QuoteRequestResponseHeader>(new QuoteRequestResponseHeaderEntityProfile());
            builder.ApplyConfiguration<QuoteRequestResponseDetail>(new QuoteRequestResponseDetailEntityProfile());

            #endregion QuoteRequests

            #endregion Purchases

            #region Products

            builder.ApplyConfiguration<AccessoryProduct>(new AccessoryProductEntityProfile());
            builder.ApplyConfiguration<Category>(new CategoryEntityProfile());
            builder.ApplyConfiguration<Company>(new CompanyEntityProfile());
            builder.ApplyConfiguration<ProductFeature>(new ProductFeatureEntityProfile());
            builder.ApplyConfiguration<RelProductStation>(new RelProductStationEntityProfile());
            builder.ApplyConfiguration<Station>(new StationEntityProfile());
            builder.ApplyConfiguration<Structure>(new StructureEntityProfile());
            builder.ApplyConfiguration<StructureItem>(new StructureItemEntityProfile());
            builder.ApplyConfiguration<SubCategory>(new SubCategoryEntityProfile());
            builder.ApplyConfiguration<StructureVersion>(new StructureVersionEntityProfile());
            builder.ApplyConfiguration<ProductPieceType>(new ProductPieceTypeEntityProfile());
            builder.ApplyConfiguration<ProductPieceFormat>(new ProductPieceFormatEntityProfile());
            builder.ApplyConfiguration<ProductSupplyVoltage>(new ProductSupplyVoltageEntityProfile());
            builder.ApplyConfiguration<ProductCutType>(new ProductCutTypeEntityProfile());
            builder.ApplyConfiguration<Archive>(new ArchiveEntityProfile());
            builder.ApplyConfiguration<ArchiveType>(new ArchiveTypeEntityProfile());

            #endregion Products

            #region Sales

            builder.ApplyConfiguration<OrderHeader>(new OrderHeaderEntityProfile());
            builder.ApplyConfiguration<OrderDetail>(new OrderDetailEntityProfile());
            builder.ApplyConfiguration<Price>(new PriceEntityProfile());
            builder.ApplyConfiguration<DeliveryDateHistory>(new DeliveryDateHistoryEntityProfile());

            #endregion Sales

            #region Production

            builder.ApplyConfiguration<WorkOrder>(new WorkOrderEntityProfile());
            builder.ApplyConfiguration<WorkOrderItem>(new WorkOrderItemEntityProfile());
            builder.ApplyConfiguration<WorkActivity>(new WorkActivityEntityProfile());
            builder.ApplyConfiguration<WorkAction>(new WorkActionEntityProfile());
            builder.ApplyConfiguration<GroupedWorkAction>(new GroupedWorkActionEntityProfile());

            #endregion Production

            #region Logistics

            #region Incomes

            builder.ApplyConfiguration<IncomeHeader>(new IncomeHeaderEntityProfile());
            builder.ApplyConfiguration<IncomeDetail>(new IncomeDetailEntityProfile());
            builder.ApplyConfiguration<IncomeState>(new IncomeStateEntityProfile());

            #endregion

            #endregion

            #region CommercialDocuments

            #region DeliveryNote

            builder.ApplyConfiguration<DeliveryNoteHeader>(new DeliveryNoteHeaderEntityProfile());
            builder.ApplyConfiguration<DeliveryNoteDetail>(new DeliveryNoteDetailEntityProfile());

            #endregion

            #endregion
        }

        private static void SetDecimalPrecision(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(30,6)");
            }
        }
    }
}