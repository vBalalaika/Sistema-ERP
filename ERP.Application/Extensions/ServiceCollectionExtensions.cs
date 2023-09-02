using AutoMapper;
using ERP.Application.Interfaces.Services;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.Services.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.Services.Lists;
using ERP.Application.Interfaces.Services.Logistics.Incomes;
using ERP.Application.Interfaces.Services.Production;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.Services.Purchases.MissingProducts;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Application.Interfaces.Services.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.Services.Purchases.QuoteRequests;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Interfaces.ViewManagers.CommercialDocuments.DeliveryNote;
using ERP.Application.Interfaces.ViewManagers.Commons;
using ERP.Application.Interfaces.ViewManagers.Lists;
using ERP.Application.Interfaces.ViewManagers.Logistics.Incomes;
using ERP.Application.Interfaces.ViewManagers.Production;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.ViewManagers.Purchases.MissingProducts;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.PurchaseOrders;
using ERP.Application.Interfaces.ViewManagers.Purchases.QuoteRequests;
using ERP.Application.Interfaces.ViewManagers.Sales;
using ERP.Application.Managers.Clients;
using ERP.Application.Managers.CommercialDocuments.DeliveryNote;
using ERP.Application.Managers.Commons;
using ERP.Application.Managers.Lists;
using ERP.Application.Managers.Logistics.Incomes;
using ERP.Application.Managers.Production;
using ERP.Application.Managers.ProductMod;
using ERP.Application.Managers.Purchases.MissingProducts;
using ERP.Application.Managers.Purchases.Providers;
using ERP.Application.Managers.Purchases.PurchaseOrders;
using ERP.Application.Managers.Purchases.QuoteRequests;
using ERP.Application.Managers.Sales;
using ERP.Application.Services;
using ERP.Application.Services.Clients;
using ERP.Application.Services.CommercialDocuments.DeliveryNote;
using ERP.Application.Services.Commons;
using ERP.Application.Services.Lists;
using ERP.Application.Services.Logistics.Incomes;
using ERP.Application.Services.Production;
using ERP.Application.Services.ProductMod;
using ERP.Application.Services.Purchases.MissingProducts;
using ERP.Application.Services.Purchases.Providers;
using ERP.Application.Services.Purchases.PurchaseOrders;
using ERP.Application.Services.Purchases.QuoteRequests;
using ERP.Application.Services.Sales;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ERP.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            AddMediator(services);
            AddViewManagers(services);
            AddServices(services);
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericService<,>), typeof(GenericService<,>));

            services.AddScoped<IProductService, ProductService>();

            #region Clients
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IReminderService, ReminderService>();
            services.AddScoped<ICommunicationService, CommunicationService>();
            services.AddScoped<IConsultedProductService, ConsultedProductService>();
            services.AddScoped<ISaleOperationService, SaleOperationService>();
            #endregion

            #region Lists
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityService, CityService>();
            #endregion

            #region Commons
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IDocumentTypeService, DocumentTypeService>();
            services.AddScoped<IOperationStateService, OperationStateService>();
            services.AddScoped<IIVAConditionService, IVAConditionService>();
            services.AddScoped<IDollarExchangeRateService, DollarExchangeRateService>();
            #endregion

            #region Purchases

            #region Providers

            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ISubsidiaryService, SubsidiaryService>();
            services.AddScoped<IFinancialInformationService, FinancialInformationService>();
            services.AddScoped<IRelProviderProductService, RelProviderProductService>();
            services.AddScoped<IRelProviderStationService, RelProviderStationService>();

            #endregion

            #region MissingProducts

            services.AddScoped<IMissingProductService, MissingProductService>();
            services.AddScoped<IPurchaseStateService, PurchaseStateService>();

            #endregion

            #region PurchaseOrders

            services.AddScoped<IPurchaseOrderHeaderService, PurchaseOrderHeaderService>();
            services.AddScoped<IPurchaseOrderDetailService, PurchaseOrderDetailService>();

            services.AddScoped<IServicePOHeaderService, ServicePOHeaderService>();
            services.AddScoped<IServicePODetailService, ServicePODetailService>();

            #endregion

            #region QuoteRequests

            services.AddScoped<IQuoteRequestHeaderService, QuoteRequestHeaderService>();
            services.AddScoped<IQuoteRequestDetailService, QuoteRequestDetailService>();
            services.AddScoped<IRelQuoteRequestProviderService, RelQuoteRequestProviderService>();

            services.AddScoped<IQuoteRequestResponseHeaderService, QuoteRequestResponseHeaderService>();
            services.AddScoped<IQuoteRequestResponseDetailService, QuoteRequestResponseDetailService>();

            #endregion

            #endregion

            #region Products
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISubCategoryService, SubCategoryService>();
            services.AddScoped<IStationService, StationService>();
            //services.AddScoped<IIVATypeService, IVATypeService>();
            services.AddScoped<IUnitMeasureService, UnitMeasureService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IPieceFormatService, PieceFormatService>();
            services.AddScoped<IPieceTypeService, PieceTypeService>();
            services.AddScoped<ICutTypeService, CutTypeService>();
            services.AddScoped<ISupplyVoltageService, SupplyVoltageService>();
            services.AddScoped<IAccessoryProductService, AccessoryProductService>();
            services.AddScoped<IProductFeatureService, ProductFeatureService>();
            services.AddScoped<IStructureVersionService, StructureVersionService>();
            services.AddScoped<IRelProductStationService, RelProductStationService>();
            services.AddScoped<IStructureItemService, StructureItemService>();
            services.AddScoped<IStructureService, StructureService>();
            services.AddScoped<IProductCutTypeService, ProductCutTypeService>();
            services.AddScoped<IProductPieceFormatService, ProductPieceFormatService>();
            services.AddScoped<IProductPieceTypeService, ProductPieceTypeService>();
            services.AddScoped<IProductSupplyVoltageService, ProductSupplyVoltageService>();
            services.AddScoped<IArchiveService, ArchiveService>();
            services.AddScoped<IArchiveTypeService, ArchiveTypeService>();
            services.AddScoped<IFunctionalAreaService, FunctionalAreaService>();
            #endregion

            #region Sales

            services.AddScoped<IOrderHeaderService, OrderHeaderService>();
            services.AddScoped<IOrderDetailService, OrderDetailService>();
            services.AddScoped<IOrderStateService, OrderStateService>();
            services.AddScoped<IPriceService, PriceService>();
            services.AddScoped<IDeliveryDateHistoryService, DeliveryDateHistoryService>();

            #endregion

            #region Production
            services.AddScoped<IWorkOrderHeaderService, WorkOrderHeaderService>();
            services.AddScoped<IWorkOrderService, WorkOrderService>();
            services.AddScoped<IWorkOrderItemService, WorkOrderItemService>();
            services.AddScoped<IWorkActivityService, WorkActivityService>();
            services.AddScoped<IWorkActionService, WorkActionService>();
            services.AddScoped<IGroupedWorkActionService, GroupedWorkActionService>();
            #endregion

            #region Logistics

            #region Incomes
            services.AddScoped<IIncomeHeaderService, IncomeHeaderService>();
            services.AddScoped<IIncomeDetailService, IncomeDetailService>();
            services.AddScoped<IIncomeStateService, IncomeStateService>();
            #endregion

            #endregion

            #region CommercialDocuments

            #region DeliveryNote

            services.AddScoped<IDeliveryNoteHeaderService, DeliveryNoteHeaderService>();
            services.AddScoped<IDeliveryNoteDetailService, DeliveryNoteDetailService>();

            #endregion

            #endregion

        }

        private static void AddMediator(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }

        private static void AddViewManagers(IServiceCollection services)
        {


            #region Clients
            services.AddScoped<IClientViewManager, ClientViewManager>();
            services.AddScoped<IReminderViewManager, ReminderViewManager>();
            services.AddScoped<ICommunicationViewManager, CommunicationViewManager>();
            services.AddScoped<IConsultedProductViewManager, ConsultedProductViewManager>();
            services.AddScoped<ISaleOperationViewManager, SaleOperationViewManager>();
            #endregion

            #region Lists
            services.AddScoped<IStateViewManager, StateViewManager>();
            services.AddScoped<ICityViewManager, CityViewManager>();
            #endregion

            #region Commons
            services.AddScoped<ICountryViewManager, CountryViewManager>();
            services.AddScoped<IDocumentTypeViewManager, DocumentTypeViewManager>();
            services.AddScoped<IOperationStateViewManager, OperationStateViewManager>();
            services.AddScoped<IIVAConditionViewManager, IVAConditionViewManager>();
            services.AddScoped<IDollarExchangeRateViewManager, DollarExchangeRateViewManager>();
            #endregion

            #region Products

            services.AddScoped<IProductViewManager, ProductViewManager>();
            services.AddScoped<IStructureViewManager, StructureViewManager>();
            services.AddScoped<IStructureItemViewManager, StructureItemViewManager>();
            services.AddScoped<IStructureVersionViewManager, StructureVersionViewManager>();
            services.AddScoped<ICategoryViewManager, CategoryViewManager>();
            services.AddScoped<ISubCategoryViewManager, SubCategoryViewManager>();
            services.AddScoped<IStationViewManager, StationViewManager>();
            //services.AddScoped<IIVATypeViewManager, IVATypeViewManager>();
            services.AddScoped<IUnitMeasureViewManager, UnitMeasureViewManager>();
            services.AddScoped<IFunctionalAreaViewManager, FunctionalAreaViewManager>();
            services.AddScoped<IProductSupplyVoltageViewManager, ProductSupplyVoltageViewManager>();
            services.AddScoped<IArchiveViewManager, ArchiveViewManager>();
            #endregion

            #region Purchases

            #region Providers

            services.AddScoped<IProviderViewManager, ProviderViewManager>();
            services.AddScoped<IContactViewManager, ContactViewManager>();
            services.AddScoped<ISubsidiaryViewManager, SubsidiaryViewManager>();
            services.AddScoped<IFinancialInformationViewManager, FinancialInformationViewManager>();
            services.AddScoped<IRelProviderProductViewManager, RelProviderProductViewManager>();
            services.AddScoped<IRelProviderStationViewManager, RelProviderStationViewManager>();

            #endregion

            #region MissingProducts

            services.AddScoped<IMissingProductViewManager, MissingProductViewManager>();
            services.AddScoped<IPurchaseStateViewManager, PurchaseStateViewManager>();

            #endregion

            #region PurchaseOrders

            services.AddScoped<IPurchaseOrderHeaderViewManager, PurchaseOrderHeaderViewManager>();
            services.AddScoped<IPurchaseOrderDetailViewManager, PurchaseOrderDetailViewManager>();

            services.AddScoped<IServicePOHeaderViewManager, ServicePOHeaderViewManager>();
            services.AddScoped<IServicePODetailViewManager, ServicePODetailViewManager>();

            #endregion

            #region QuoteRequests

            services.AddScoped<IQuoteRequestHeaderViewManager, QuoteRequestHeaderViewManager>();
            services.AddScoped<IQuoteRequestDetailViewManager, QuoteRequestDetailViewManager>();
            services.AddScoped<IRelQuoteRequestProviderViewManager, RelQuoteRequestProviderViewManager>();

            services.AddScoped<IQuoteRequestResponseHeaderViewManager, QuoteRequestResponseHeaderViewManager>();
            services.AddScoped<IQuoteRequestResponseDetailViewManager, QuoteRequestResponseDetailViewManager>();

            #endregion

            #endregion

            #region Sales

            services.AddScoped<IOrderHeaderViewManager, OrderHeaderViewManager>();
            services.AddScoped<IOrderDetailViewManager, OrderDetailViewManager>();
            services.AddScoped<IOrderStateViewManager, OrderStateViewManager>();
            services.AddScoped<IPriceViewManager, PriceViewManager>();
            services.AddScoped<IDeliveryDateHistoryViewManager, DeliveryDateHistoryViewManager>();

            #endregion

            #region Production
            services.AddScoped<IWorkOrderHeaderViewManager, WorkOrderHeaderViewManager>();
            services.AddScoped<IWorkOrderViewManager, WorkOrderViewManager>();
            services.AddScoped<IWorkOrderItemViewManager, WorkOrderItemViewManager>();
            services.AddScoped<IWorkActivityViewManager, WorkActivityViewManager>();
            services.AddScoped<IWorkActionViewManager, WorkActionViewManager>();
            services.AddScoped<IGroupedWorkActionViewManager, GroupedWorkActionViewManager>();
            #endregion

            #region Logistics

            #region Incomes
            services.AddScoped<IIncomeHeaderViewManager, IncomeHeaderViewManager>();
            services.AddScoped<IIncomeDetailViewManager, IncomeDetailViewManager>();
            services.AddScoped<IIncomeStateViewManager, IncomeStateViewManager>();
            #endregion

            #endregion

            #region CommercialDocuments

            #region DeliveryNote

            services.AddScoped<IDeliveryNoteHeaderViewManager, DeliveryNoteHeaderViewManager>();
            services.AddScoped<IDeliveryNoteDetailViewManager, DeliveryNoteDetailViewManager>();

            #endregion

            #endregion
        }
    }
}