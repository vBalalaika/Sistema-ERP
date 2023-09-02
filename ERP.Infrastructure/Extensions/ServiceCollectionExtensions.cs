using AutoMapper;
using ERP.Application.Interfaces.Contexts;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Repositories.Clients;
using ERP.Application.Interfaces.Repositories.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.Repositories.Commons;
using ERP.Application.Interfaces.Repositories.Lists;
using ERP.Application.Interfaces.Repositories.Logistics.Incomes;
using ERP.Application.Interfaces.Repositories.Production;
using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Application.Interfaces.Repositories.Purchases.MissingProducts;
using ERP.Application.Interfaces.Repositories.Purchases.Providers;
using ERP.Application.Interfaces.Repositories.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.Repositories.Purchases.QuoteRequests;
using ERP.Application.Interfaces.Repositories.Sales;
using ERP.Infrastructure.DbContexts;
using ERP.Infrastructure.Repositories;
using ERP.Infrastructure.Repositories.Clients;
using ERP.Infrastructure.Repositories.CommercialDocuments.DeliveryNote;
using ERP.Infrastructure.Repositories.Commons;
using ERP.Infrastructure.Repositories.Lists;
using ERP.Infrastructure.Repositories.Logistics.Incomes;
using ERP.Infrastructure.Repositories.Production;
using ERP.Infrastructure.Repositories.ProductMod;
using ERP.Infrastructure.Repositories.Purchases.MissingProducts;
using ERP.Infrastructure.Repositories.Purchases.Providers;
using ERP.Infrastructure.Repositories.Purchases.PurchaseOrders;
using ERP.Infrastructure.Repositories.Purchases.QuoteRequests;
using ERP.Infrastructure.Repositories.Sales;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ERP.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            #region Repositories

            services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            #region Products
            services.AddTransient<IProductRepository, ProductRepositoryAsync>();
            services.AddTransient<IAccesoryProductRepository, AccesoryProductRepositoryAsync>();
            services.AddTransient<ICategoryRepository, CategoryRepositoryAsync>();
            services.AddTransient<IIVAConditionRepository, IVAConditionRepositoryAsync>();
            services.AddTransient<IProductFeatureRepository, ProductFeatureRepositoryAsync>();
            services.AddTransient<IStructureVersionRepository, StructureVersionRepositoryAsync>();
            services.AddTransient<IRelProductStationRepository, RelProductStationRepositoryAsync>();
            services.AddTransient<IStationRepository, StationRepositoryAsync>();
            services.AddTransient<IStructureItemRepository, StructureItemRepositoryAsync>();
            services.AddTransient<IStructureRepository, StructureRepositoryAsync>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepositoryAsync>();
            services.AddTransient<IUnitMeasureRepository, UnitMeasureRepositoryAsync>();
            services.AddTransient<IOperationRepository, OperationRepositoryAsync>();
            services.AddTransient<IPieceTypeRepository, PieceTypeRepositoryAsync>();
            services.AddTransient<IPieceFormatRepository, PieceFormatRepositoryAsync>();
            services.AddTransient<ICutTypeRepository, CutTypeRepositoryAsync>();
            services.AddTransient<ISupplyVoltageRepository, SupplyVoltageRepositoryAsync>();
            services.AddTransient<IProductCutTypeRepository, ProductCutTypeRepositoryAsync>();
            services.AddTransient<IProductPieceFormatRepository, ProductPieceFormatRepositoryAsync>();
            services.AddTransient<IProductPieceTypeRepository, ProductPieceTypeRepositoryAsync>();
            services.AddTransient<IProductSupplyVoltageRepository, ProductSupplyVoltageRepositoryAsync>();
            services.AddTransient<IArchiveRepository, ArchiveRepositoryAsync>();
            services.AddTransient<IArchiveTypeRepository, ArchiveTypeRepositoryAsync>();
            services.AddTransient<IFunctionalAreaRepository, FunctionalAreaRepositoryAsync>();
            #endregion

            #region Clients

            services.AddTransient<IClientRepository, ClientRepositoryAsync>();
            services.AddTransient<IReminderRepository, ReminderRepositoryAsync>();
            services.AddTransient<ICommunicationRepository, CommunicationRepositoryAsync>();
            services.AddTransient<IConsultedProductRepository, ConsultedProductRepositoryAsync>();
            services.AddTransient<ISaleOperationRepository, SaleOperationRepositoryAsync>();

            #endregion

            #region Lists
            services.AddTransient<IStateRepository, StateRepositoryAsync>();
            services.AddTransient<ICityRepository, CityRepositoryAsync>();
            #endregion

            #region Commons
            services.AddTransient<ICountryRepository, CountryRepositoryAsync>();
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepositoryAsync>();
            services.AddTransient<IOperationStateRepository, OperationStateRepositoryAsync>();
            services.AddTransient<IIVAConditionRepository, IVAConditionRepositoryAsync>();
            services.AddTransient<IDollarExchangeRateRepository, DollarExchangeRateRepositoryAsync>();
            #endregion

            #region Purchases

            #region Providers

            services.AddTransient<IProviderRepository, ProviderRepositoryAsync>();
            services.AddTransient<IContactRepository, ContactRepositoryAsync>();
            services.AddTransient<ISubsidiaryRepository, SubsidiaryRepositoryAsync>();
            services.AddTransient<IFinancialInformationRepository, FinancialInformationRepositoryAsync>();
            services.AddTransient<IRelProviderProductRepository, RelProviderProductRepositoryAsync>();
            services.AddTransient<IRelProviderStationRepository, RelProviderStationRepositoryAsync>();

            #endregion

            #region MissingProducts

            services.AddTransient<IMissingProductRepository, MissingProductRepositoryAsync>();
            services.AddTransient<IPurchaseStateRepository, StateMissingProductRepositoryAsync>();

            #endregion

            #region PurchaseOrders

            services.AddTransient<IPurchaseOrderHeaderRepository, PurchaseOrderHeaderRepositoryAsync>();
            services.AddTransient<IPurchaseOrderDetailRepository, PurchaseOrderDetailRepositoryAsync>();

            services.AddTransient<IServicePOHeaderRepository, ServicePOHeaderRepositoryAsync>();
            services.AddTransient<IServicePODetailRepository, ServicePODetailRepositoryAsync>();

            #endregion

            #region QuoteRequests

            services.AddTransient<IQuoteRequestHeaderRepository, QuoteRequestHeaderRepositoryAsync>();
            services.AddTransient<IQuoteRequestDetailRepository, QuoteRequestDetailRepositoryAsync>();
            services.AddTransient<IRelQuoteRequestProviderRepository, RelQuoteRequestProviderRepositoryAsync>();

            services.AddTransient<IQuoteRequestResponseHeaderRepository, QuoteRequestResponseHeaderRepositoryAsync>();
            services.AddTransient<IQuoteRequestResponseDetailRepository, QuoteRequestResponseDetailRepositoryAsync>();

            #endregion

            #endregion

            #region Sales

            services.AddTransient<IOrderHeaderRepository, OrderHeaderRepositoryAsync>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepositoryAsync>();
            services.AddTransient<IOrderStateRepository, OrderStateRepositoryAsync>();
            services.AddTransient<IPriceRepository, PriceRepositoryAsync>();
            services.AddTransient<IDeliveryDateHistoryRepository, DeliveryDateHistoryRepositoryAsync>();

            #endregion

            #region Production
            services.AddTransient<IWorkOrderHeaderRepository, WorkOrderHeaderRepositoryAsync>();
            services.AddTransient<IWorkOrderRepository, WorkOrderRepositoryAsync>();
            services.AddTransient<IWorkOrderItemRepository, WorkOrderItemRepositoryAsync>();
            services.AddTransient<IWorkActivityRepository, WorkActivityRepositoryAsync>();
            services.AddTransient<IWorkActionRepository, WorkActionRepositoryAsync>();
            services.AddTransient<IGroupedWorkActionRepository, GroupedWorkActionRepositoryAsync>();
            #endregion

            #region Logistics

            #region Incomes

            services.AddTransient<IIncomeHeaderRepository, IncomeHeaderRepositoryAsync>();
            services.AddTransient<IIncomeDetailRepository, IncomeDetailRepositoryAsync>();
            services.AddTransient<IIncomeStateRepository, IncomeStateRepositoryAsync>();

            #endregion

            #endregion

            #region CommercialDocuments

            #region DeliveryNote

            services.AddTransient<IDeliveryNoteHeaderRepository, DeliveryNoteHeaderRepositoryAsync>();
            services.AddTransient<IDeliveryNoteDetailRepository, DeliveryNoteDetailRepositoryAsync>();

            #endregion

            #endregion

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ILogRepository, LogRepository>();

            #endregion Repositories
        }
    }
}