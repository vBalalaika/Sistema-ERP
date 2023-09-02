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
using System;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        #region Products
        public IProductRepository ProductRepository { get; }
        public IAccesoryProductRepository AccesoryProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductFeatureRepository ProductFeatureRepository { get; }
        public IStructureVersionRepository StructureVersionRepository { get; }
        public IRelProductStationRepository RelProductStationRepository { get; }
        public IStationRepository StationRepository { get; }
        public IStructureRepository StructureRepository { get; }
        public IStructureItemRepository StructureItemRepository { get; }
        public ISubCategoryRepository SubCategoryRepository { get; }
        public IUnitMeasureRepository UnitMeasureRepository { get; }
        public IOperationRepository OperationRepository { get; }
        public IPieceTypeRepository PieceTypeRepository { get; }
        public IPieceFormatRepository PieceFormatRepository { get; }
        public ICutTypeRepository CutTypeRepository { get; }
        public ISupplyVoltageRepository SupplyVoltageRepository { get; }
        public IProductCutTypeRepository ProductCutTypeRepository { get; }
        public IProductPieceFormatRepository ProductPieceFormatRepository { get; }
        public IProductPieceTypeRepository ProductPieceTypeRepository { get; }
        public IProductSupplyVoltageRepository ProductSupplyVoltageRepository { get; }
        public IArchiveRepository ArchiveRepository { get; }
        public IArchiveTypeRepository ArchiveTypeRepository { get; }
        #endregion

        #region Clients
        public IClientRepository ClientRepository { get; }
        public IReminderRepository ReminderRepository { get; }
        public ICommunicationRepository CommunicationRepository { get; }
        public IConsultedProductRepository ConsultedProductRepository { get; }
        public ISaleOperationRepository SaleOperationRepository { get; }
        #endregion

        #region Lists
        public IStateRepository StateRepository { get; }
        public ICityRepository CityRepository { get; }
        #endregion

        #region Commons
        public ICountryRepository CountryRepository { get; }
        public IDocumentTypeRepository DocumentTypeRepository { get; }
        public IOperationStateRepository OperationStateRepository { get; }
        public IIVAConditionRepository IVAConditionRepository { get; }
        public IDollarExchangeRateRepository DollarExchangeRateRepository { get; }
        #endregion

        #region Purchases

        #region Providers

        public IProviderRepository ProviderRepository { get; }
        public IContactRepository ContactRepository { get; }
        public ISubsidiaryRepository SubsidiaryRepository { get; }
        public IFinancialInformationRepository FinancialInformationRepository { get; }
        public IRelProviderProductRepository RelProviderProductRepository { get; }
        public IRelProviderStationRepository RelProviderStationRepository { get; }

        #endregion

        #region MissingProducts

        public IMissingProductRepository MissingProductRepository { get; }
        public IPurchaseStateRepository PurchaseStateRepository { get; }

        #endregion

        #region PurchaseOrders

        public IPurchaseOrderHeaderRepository PurchaseOrderHeaderRepository { get; }
        public IPurchaseOrderDetailRepository PurchaseOrderDetailRepository { get; }

        public IServicePOHeaderRepository ServicePOHeaderRepository { get; }
        public IServicePODetailRepository ServicePODetailRepository { get; }

        #endregion

        #region QuoteRequests

        public IQuoteRequestHeaderRepository QuoteRequestHeaderRepository { get; }
        public IQuoteRequestDetailRepository QuoteRequestDetailRepository { get; }
        public IRelQuoteRequestProviderRepository RelQuoteRequestProviderRepository { get; }

        public IQuoteRequestResponseHeaderRepository QuoteRequestResponseHeaderRepository { get; }
        public IQuoteRequestResponseDetailRepository QuoteRequestResponseDetailRepository { get; }

        #endregion

        #endregion

        #region Sales

        public IOrderHeaderRepository OrderHeaderRepository { get; }
        public IOrderDetailRepository OrderDetailRepository { get; }
        public IOrderStateRepository OrderStateRepository { get; }
        public IPriceRepository PriceRepository { get; }
        public IDeliveryDateHistoryRepository DeliveryDateHistoryRepository { get; }

        #endregion

        #region Production
        public IWorkOrderRepository WorkOrderRepository { get; }
        public IWorkOrderHeaderRepository WorkOrderHeaderRepository { get; }
        public IWorkOrderItemRepository WorkOrderItemRepository { get; }
        public IWorkActivityRepository WorkActivityRepository { get; }
        public IGroupedWorkActionRepository GroupedWorkActionRepository { get; }
        #endregion

        #region Logistics

        #region Incomes
        public IIncomeHeaderRepository IncomeHeaderRepository { get; }
        public IIncomeDetailRepository IncomeDetailRepository { get; }
        public IIncomeStateRepository IncomeStateRepository { get; }
        #endregion

        #endregion

        #region CommercialDocuments

        #region DeliveryNote

        public IDeliveryNoteHeaderRepository DeliveryNoteHeaderRepository { get; }
        public IDeliveryNoteDetailRepository DeliveryNoteDetailRepository { get; }

        #endregion

        #endregion

        IGenericRepository<T> GetRepository<T>() where T : class;
        Task<int> Commit();


    }
}