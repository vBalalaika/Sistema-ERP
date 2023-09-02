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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Products
        private readonly IAccesoryProductRepository _accessoryProductRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductFeatureRepository _productFeatureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStructureVersionRepository _structureVersionRepository;
        private readonly IRelProductStationRepository _relProdutcStationRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IStructureRepository _structureRepository;
        private readonly IStructureItemRepository _structureItemRepository;
        private readonly ISubCategoryRepository _subcategoryRepository;
        private readonly IUnitMeasureRepository _unitMeasureRepository;
        private readonly IOperationRepository _operationRepository;
        private readonly IPieceTypeRepository _pieceTypeRepository;
        private readonly IPieceFormatRepository _pieceFormatRepository;
        private readonly ICutTypeRepository _cutTypeRepository;
        private readonly ISupplyVoltageRepository _supplyVoltageRepository;
        private readonly IProductSupplyVoltageRepository _productSupplyVoltageRepository;
        private readonly IProductPieceFormatRepository _productPieceFormatRepository;
        private readonly IProductPieceTypeRepository _productPieceTypeRepository;
        private readonly IProductCutTypeRepository _productCutTypeRepository;
        private readonly IArchiveRepository _archiveRepository;
        private readonly IArchiveTypeRepository _archiveTypeRepository;
        private readonly IFunctionalAreaRepository _functionalAreaRepository;
        #endregion

        #region Clients
        private readonly IClientRepository _clientRepository;
        private readonly IReminderRepository _reminderRepository;
        private readonly ICommunicationRepository _communicationRepository;
        private readonly IConsultedProductRepository _consultedProductRepository;
        private readonly ISaleOperationRepository _saleOperationRepository;
        #endregion

        #region Lists
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        #endregion

        #region Commons
        private readonly ICountryRepository _countryRepository;
        private readonly IDocumentTypeRepository _documentTypeRepository;
        private readonly IOperationStateRepository _operationStateRepository;
        private readonly IIVAConditionRepository _iVAConditionRepository;
        private readonly IDollarExchangeRateRepository _dollarExchangeRateRepository;
        #endregion

        #region Purchases

        #region Providers

        private readonly IProviderRepository _providerRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ISubsidiaryRepository _subsidiaryRepository;
        private readonly IFinancialInformationRepository _financialInformationRepository;
        private readonly IRelProviderProductRepository _relProviderProductRepository;
        private readonly IRelProviderStationRepository _relProviderStationRepository;

        #endregion

        #region MissingProducts

        private readonly IMissingProductRepository _missingProductRepository;
        private readonly IPurchaseStateRepository _purchaseStateRepository;

        #endregion

        #region PurchaseOrders

        private readonly IPurchaseOrderHeaderRepository _purchaseOrderHeaderRepository;
        private readonly IPurchaseOrderDetailRepository _purchaseOrderDetailRepository;

        private readonly IServicePOHeaderRepository _servicePOHeaderRepository;
        private readonly IServicePODetailRepository _servicePODetailRepository;

        #endregion

        #region QuoteRequests

        private readonly IQuoteRequestHeaderRepository _quoteRequestHeaderRepository;
        private readonly IQuoteRequestDetailRepository _quoteRequestDetailRepository;
        private readonly IRelQuoteRequestProviderRepository _relQuoteRequestProviderRepository;

        private readonly IQuoteRequestResponseHeaderRepository _quoteRequestResponseHeaderRepository;
        private readonly IQuoteRequestResponseDetailRepository _quoteRequestResponseDetailRepository;

        #endregion

        #endregion

        #region Sales

        private readonly IOrderHeaderRepository _OrderHeaderRepository;
        private readonly IOrderDetailRepository _OrderDetailRepository;
        private readonly IOrderStateRepository _OrderStateRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IDeliveryDateHistoryRepository _deliveryDateHistoryRepository;

        #endregion

        #region Production
        private readonly IWorkOrderHeaderRepository _WorkOrderHeaderRepository;
        private readonly IWorkOrderRepository _WorkOrderRepository;
        private readonly IWorkOrderItemRepository _WorkOrderItemRepository;
        private readonly IWorkActivityRepository _WorkActivityRepository;
        private readonly IWorkActionRepository _WorkActionRepository;
        private readonly IGroupedWorkActionRepository _GroupedWorkActionRepository;
        #endregion

        #region Logistics

        #region Incomes

        private readonly IIncomeHeaderRepository _IncomeHeaderRepository;
        private readonly IIncomeDetailRepository _IncomeDetailRepository;
        private readonly IIncomeStateRepository _IncomeStateRepository;

        #endregion

        #endregion

        #region CommercialDocuments

        #region DeliveryNote

        private readonly IDeliveryNoteHeaderRepository _deliveryNoteHeaderRepository;
        private readonly IDeliveryNoteDetailRepository _deliveryNoteDetailRepository;

        #endregion

        #endregion

        private readonly ApplicationDbContext _dbContext;
        private Dictionary<string, object> _dict = new();
        private bool disposed;

        public UnitOfWork(ApplicationDbContext dbContext, IAccesoryProductRepository accessoryProductRepository, ICategoryRepository categoryRepository, IProductFeatureRepository productFeatureRepository
            , IProductRepository productRepository, IStructureVersionRepository structureVersionRepository, IRelProductStationRepository relProductStationRepository, IStationRepository stationRepository
            , IStructureRepository structureRepository, IStructureItemRepository structureItemRepository, ISubCategoryRepository subCategoryRepository
            , IUnitMeasureRepository unitMeasureRepository, IOperationRepository operationRepository, IPieceTypeRepository pieceTypeRepository, IPieceFormatRepository pieceFormatRepository,
            ICutTypeRepository cutTypeRepository, ISupplyVoltageRepository supplyVoltageRepository, IProductSupplyVoltageRepository productSupplyVoltageRepository
            , IProductPieceFormatRepository productPieceFormatRepository, IProductPieceTypeRepository productPieceTypeRepository, IProductCutTypeRepository productCutTypeRepository,
            IArchiveRepository archiveRepository, IArchiveTypeRepository archiveTypeRepository,
            IClientRepository clientRepository, IReminderRepository reminderRepository, ICommunicationRepository communicationRepository, IConsultedProductRepository consultedProductRepository, ISaleOperationRepository saleOperationRepository, ICountryRepository countryRepository,
            IStateRepository stateRepository, ICityRepository cityRepository, IDocumentTypeRepository documentTypeRepository,
            IOperationStateRepository operationStateRepository, IIVAConditionRepository iVAConditionRepository,
            IProviderRepository providerRepository, IContactRepository contactRepository, ISubsidiaryRepository subsidiaryRepository,
            IFinancialInformationRepository financialInformationRepository, IRelProviderProductRepository relProviderProductRepository, IRelProviderStationRepository relProviderStationRepository,
            IMissingProductRepository missingProductRepository, IPurchaseStateRepository purchaseStateRepository,
            IPurchaseOrderHeaderRepository purchaseOrderHeaderRepository, IPurchaseOrderDetailRepository purchaseOrderDetailRepository,
            IServicePOHeaderRepository servicePOHeaderRepository, IServicePODetailRepository servicePODetailRepository,
            IQuoteRequestHeaderRepository quoteRequestHeaderRepository, IQuoteRequestDetailRepository quoteRequestDetailRepository,
            IRelQuoteRequestProviderRepository relQuoteRequestProviderRepository, IQuoteRequestResponseHeaderRepository quoteRequestResponseHeaderRepository, IQuoteRequestResponseDetailRepository quoteRequestResponseDetailRepository, IFunctionalAreaRepository functionalAreaRepository,
            IDollarExchangeRateRepository dollarExchangeRateRepository, IOrderHeaderRepository OrderHeaderRepository, IOrderDetailRepository OrderDetailRepository,
            IOrderStateRepository OrderStateRepository, IPriceRepository priceRepository, IWorkOrderRepository WorkOrderRepository, IWorkOrderItemRepository WorkOrderItemRepository,
            IWorkActivityRepository WorkActivityRepository, IWorkOrderHeaderRepository WorkOrderHeaderRepository, IWorkActionRepository WorkActionRepository, IGroupedWorkActionRepository GroupedWorkActionRepository, IIncomeHeaderRepository incomeHeaderRepository,
            IIncomeDetailRepository incomeDetailRepository, IIncomeStateRepository incomeStateRepository, IDeliveryNoteHeaderRepository deliveryNoteHeaderRepository, IDeliveryNoteDetailRepository deliveryNoteDetailRepository, IDeliveryDateHistoryRepository deliveryDateHistoryRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            #region Products
            _accessoryProductRepository = accessoryProductRepository;
            _categoryRepository = categoryRepository;
            _productFeatureRepository = productFeatureRepository;
            _productRepository = productRepository;

            _structureVersionRepository = structureVersionRepository;
            _relProdutcStationRepository = relProductStationRepository;
            _stationRepository = stationRepository;
            _structureRepository = structureRepository;
            _structureItemRepository = structureItemRepository;
            _subcategoryRepository = subCategoryRepository;
            _unitMeasureRepository = unitMeasureRepository;
            _operationRepository = operationRepository;
            _pieceTypeRepository = pieceTypeRepository;
            _pieceFormatRepository = pieceFormatRepository;
            _cutTypeRepository = cutTypeRepository;
            _supplyVoltageRepository = supplyVoltageRepository;
            _productSupplyVoltageRepository = productSupplyVoltageRepository;
            _productPieceFormatRepository = productPieceFormatRepository;
            _productPieceTypeRepository = productPieceTypeRepository;
            _productCutTypeRepository = productCutTypeRepository;
            _archiveRepository = archiveRepository;
            _archiveTypeRepository = archiveTypeRepository;
            _functionalAreaRepository = functionalAreaRepository;
            #endregion

            #region Clients
            _clientRepository = clientRepository;
            _reminderRepository = reminderRepository;
            _communicationRepository = communicationRepository;
            _consultedProductRepository = consultedProductRepository;
            _saleOperationRepository = saleOperationRepository;
            #endregion

            #region Lists
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            #endregion

            #region Commons
            _countryRepository = countryRepository;
            _documentTypeRepository = documentTypeRepository;
            _operationStateRepository = operationStateRepository;
            _iVAConditionRepository = iVAConditionRepository;
            _dollarExchangeRateRepository = dollarExchangeRateRepository;
            #endregion

            #region Purchases

            #region Providers

            _providerRepository = providerRepository;
            _contactRepository = contactRepository;
            _subsidiaryRepository = subsidiaryRepository;
            _financialInformationRepository = financialInformationRepository;
            _relProviderProductRepository = relProviderProductRepository;
            _relProviderStationRepository = relProviderStationRepository;

            #endregion

            #region MissingProducts

            _missingProductRepository = missingProductRepository;
            _purchaseStateRepository = purchaseStateRepository;

            #endregion

            #region PurchaseOrders

            _purchaseOrderHeaderRepository = purchaseOrderHeaderRepository;
            _purchaseOrderDetailRepository = purchaseOrderDetailRepository;
            _servicePOHeaderRepository = servicePOHeaderRepository;
            _servicePODetailRepository = servicePODetailRepository;

            #endregion

            #region QuoteRequests

            _quoteRequestHeaderRepository = quoteRequestHeaderRepository;
            _quoteRequestDetailRepository = quoteRequestDetailRepository;
            _relQuoteRequestProviderRepository = relQuoteRequestProviderRepository;

            _quoteRequestResponseHeaderRepository = quoteRequestResponseHeaderRepository;
            _quoteRequestResponseDetailRepository = quoteRequestResponseDetailRepository;

            #endregion

            #endregion

            #region Sales

            _OrderHeaderRepository = OrderHeaderRepository;
            _OrderDetailRepository = OrderDetailRepository;
            _OrderStateRepository = OrderStateRepository;
            _priceRepository = priceRepository;
            _deliveryDateHistoryRepository = deliveryDateHistoryRepository;

            #endregion

            #region Production
            _WorkOrderHeaderRepository = WorkOrderHeaderRepository;
            _WorkOrderRepository = WorkOrderRepository;
            _WorkOrderItemRepository = WorkOrderItemRepository;
            _WorkActivityRepository = WorkActivityRepository;
            _WorkActionRepository = WorkActionRepository;
            _GroupedWorkActionRepository = GroupedWorkActionRepository;
            #endregion

            #region Logistics

            #region Incomes

            _IncomeHeaderRepository = incomeHeaderRepository;
            _IncomeDetailRepository = incomeDetailRepository;
            _IncomeStateRepository = incomeStateRepository;

            #endregion

            #endregion

            #region CommercialDocuments

            #region DeliveryNote

            _deliveryNoteHeaderRepository = deliveryNoteHeaderRepository;
            _deliveryNoteDetailRepository = deliveryNoteDetailRepository;

            #endregion

            #endregion

            LoadGeneric();

        }

        #region Products
        public IAccesoryProductRepository AccesoryProductRepository => _accessoryProductRepository;
        public ICategoryRepository CategoryRepository => _categoryRepository;
        public IProductFeatureRepository ProductFeatureRepository => _productFeatureRepository;
        public IProductRepository ProductRepository => _productRepository;
        public IStructureVersionRepository StructureVersionRepository => _structureVersionRepository;
        public IRelProductStationRepository RelProductStationRepository => _relProdutcStationRepository;
        public IStationRepository StationRepository => _stationRepository;
        public IStructureRepository StructureRepository => _structureRepository;
        public IStructureItemRepository StructureItemRepository => _structureItemRepository;
        public ISubCategoryRepository SubCategoryRepository => _subcategoryRepository;
        public IUnitMeasureRepository UnitMeasureRepository => _unitMeasureRepository;
        public IOperationRepository OperationRepository => _operationRepository;
        public IPieceTypeRepository PieceTypeRepository => _pieceTypeRepository;
        public IPieceFormatRepository PieceFormatRepository => _pieceFormatRepository;
        public ICutTypeRepository CutTypeRepository => _cutTypeRepository;
        public ISupplyVoltageRepository SupplyVoltageRepository => _supplyVoltageRepository;
        public IProductCutTypeRepository ProductCutTypeRepository => _productCutTypeRepository;
        public IProductPieceFormatRepository ProductPieceFormatRepository => _productPieceFormatRepository;
        public IProductPieceTypeRepository ProductPieceTypeRepository => _productPieceTypeRepository;
        public IProductSupplyVoltageRepository ProductSupplyVoltageRepository => _productSupplyVoltageRepository;
        public IArchiveRepository ArchiveRepository => _archiveRepository;
        public IArchiveTypeRepository ArchiveTypeRepository => _archiveTypeRepository;
        public IFunctionalAreaRepository FunctionalAreaRepository => _functionalAreaRepository;
        #endregion

        #region Clients
        public IClientRepository ClientRepository => _clientRepository;
        public IReminderRepository ReminderRepository => _reminderRepository;
        public ICommunicationRepository CommunicationRepository => _communicationRepository;
        public IConsultedProductRepository ConsultedProductRepository => _consultedProductRepository;
        public ISaleOperationRepository SaleOperationRepository => _saleOperationRepository;
        #endregion

        #region Lists
        public IStateRepository StateRepository => _stateRepository;
        public ICityRepository CityRepository => _cityRepository;
        #endregion

        #region Commons
        public ICountryRepository CountryRepository => _countryRepository;
        public IDocumentTypeRepository DocumentTypeRepository => _documentTypeRepository;
        public IOperationStateRepository OperationStateRepository => _operationStateRepository;
        public IIVAConditionRepository IVAConditionRepository => _iVAConditionRepository;
        public IDollarExchangeRateRepository DollarExchangeRateRepository => _dollarExchangeRateRepository;
        #endregion

        #region Purchases

        #region Providers

        public IProviderRepository ProviderRepository => _providerRepository;
        public IContactRepository ContactRepository => _contactRepository;
        public ISubsidiaryRepository SubsidiaryRepository => _subsidiaryRepository;
        public IFinancialInformationRepository FinancialInformationRepository => _financialInformationRepository;
        public IRelProviderProductRepository RelProviderProductRepository => _relProviderProductRepository;
        public IRelProviderStationRepository RelProviderStationRepository => _relProviderStationRepository;

        #endregion

        #region MissingProducts

        public IMissingProductRepository MissingProductRepository => _missingProductRepository;
        public IPurchaseStateRepository PurchaseStateRepository => _purchaseStateRepository;

        #endregion

        #region PurchaseOrders

        public IPurchaseOrderHeaderRepository PurchaseOrderHeaderRepository => _purchaseOrderHeaderRepository;
        public IPurchaseOrderDetailRepository PurchaseOrderDetailRepository => _purchaseOrderDetailRepository;

        public IServicePOHeaderRepository ServicePOHeaderRepository => _servicePOHeaderRepository;
        public IServicePODetailRepository ServicePODetailRepository => _servicePODetailRepository;

        #endregion

        #region QuoteRequests

        public IQuoteRequestHeaderRepository QuoteRequestHeaderRepository => _quoteRequestHeaderRepository;
        public IQuoteRequestDetailRepository QuoteRequestDetailRepository => _quoteRequestDetailRepository;
        public IRelQuoteRequestProviderRepository RelQuoteRequestProviderRepository => _relQuoteRequestProviderRepository;

        public IQuoteRequestResponseHeaderRepository QuoteRequestResponseHeaderRepository => _quoteRequestResponseHeaderRepository;
        public IQuoteRequestResponseDetailRepository QuoteRequestResponseDetailRepository => _quoteRequestResponseDetailRepository;

        #endregion

        #endregion

        #region Sales

        public IOrderHeaderRepository OrderHeaderRepository => _OrderHeaderRepository;
        public IOrderDetailRepository OrderDetailRepository => _OrderDetailRepository;
        public IOrderStateRepository OrderStateRepository => _OrderStateRepository;
        public IPriceRepository PriceRepository => _priceRepository;
        public IDeliveryDateHistoryRepository DeliveryDateHistoryRepository => _deliveryDateHistoryRepository;

        #endregion

        #region Production
        public IWorkOrderHeaderRepository WorkOrderHeaderRepository => _WorkOrderHeaderRepository;
        public IWorkOrderRepository WorkOrderRepository => _WorkOrderRepository;
        public IWorkOrderItemRepository WorkOrderItemRepository => _WorkOrderItemRepository;
        public IWorkActivityRepository WorkActivityRepository => _WorkActivityRepository;
        public IWorkActionRepository WorkActionRepository => _WorkActionRepository;
        public IGroupedWorkActionRepository GroupedWorkActionRepository => _GroupedWorkActionRepository;
        #endregion

        #region Logistics

        #region Incomes

        public IIncomeHeaderRepository IncomeHeaderRepository => _IncomeHeaderRepository;
        public IIncomeDetailRepository IncomeDetailRepository => _IncomeDetailRepository;
        public IIncomeStateRepository IncomeStateRepository => _IncomeStateRepository;

        #endregion

        #endregion

        #region CommercialDocuments

        #region DeliveryNote

        public IDeliveryNoteHeaderRepository DeliveryNoteHeaderRepository => _deliveryNoteHeaderRepository;
        public IDeliveryNoteDetailRepository DeliveryNoteDetailRepository => _deliveryNoteDetailRepository;

        #endregion

        #endregion

        private void LoadGeneric()
        {
            #region Products
            _dict.Add(nameof(AccesoryProductRepository), AccesoryProductRepository);
            _dict.Add(nameof(CategoryRepository), CategoryRepository);
            _dict.Add(nameof(ProductFeatureRepository), ProductFeatureRepository);
            _dict.Add(nameof(ProductRepository), ProductRepository);
            _dict.Add(nameof(StructureVersionRepository), StructureVersionRepository);
            _dict.Add(nameof(RelProductStationRepository), RelProductStationRepository);
            _dict.Add(nameof(StationRepository), StationRepository);
            _dict.Add(nameof(StructureRepository), StructureRepository);
            _dict.Add(nameof(StructureItemRepository), StructureItemRepository);
            _dict.Add(nameof(SubCategoryRepository), SubCategoryRepository);
            _dict.Add(nameof(UnitMeasureRepository), UnitMeasureRepository);
            _dict.Add(nameof(OperationRepository), OperationRepository);
            _dict.Add(nameof(PieceTypeRepository), PieceTypeRepository);
            _dict.Add(nameof(PieceFormatRepository), PieceFormatRepository);
            _dict.Add(nameof(CutTypeRepository), CutTypeRepository);
            _dict.Add(nameof(SupplyVoltageRepository), SupplyVoltageRepository);
            _dict.Add(nameof(ProductSupplyVoltageRepository), ProductSupplyVoltageRepository);
            _dict.Add(nameof(ProductPieceTypeRepository), ProductPieceTypeRepository);
            _dict.Add(nameof(ProductPieceFormatRepository), ProductPieceFormatRepository);
            _dict.Add(nameof(ProductCutTypeRepository), ProductCutTypeRepository);
            _dict.Add(nameof(ArchiveRepository), ArchiveRepository);
            _dict.Add(nameof(ArchiveTypeRepository), ArchiveTypeRepository);
            _dict.Add(nameof(FunctionalAreaRepository), FunctionalAreaRepository);
            #endregion

            #region Clients
            _dict.Add(nameof(ClientRepository), ClientRepository);
            _dict.Add(nameof(ReminderRepository), ReminderRepository);
            _dict.Add(nameof(CommunicationRepository), CommunicationRepository);
            _dict.Add(nameof(ConsultedProductRepository), ConsultedProductRepository);
            _dict.Add(nameof(SaleOperationRepository), SaleOperationRepository);
            #endregion

            #region Lists
            _dict.Add(nameof(StateRepository), StateRepository);
            _dict.Add(nameof(CityRepository), CityRepository);
            #endregion

            #region Commons
            _dict.Add(nameof(CountryRepository), CountryRepository);
            _dict.Add(nameof(DocumentTypeRepository), DocumentTypeRepository);
            _dict.Add(nameof(OperationStateRepository), OperationStateRepository);
            _dict.Add(nameof(IVAConditionRepository), IVAConditionRepository);
            _dict.Add(nameof(DollarExchangeRateRepository), DollarExchangeRateRepository);
            #endregion

            #region Purchases

            #region Providers

            _dict.Add(nameof(ProviderRepository), ProviderRepository);
            _dict.Add(nameof(ContactRepository), ContactRepository);
            _dict.Add(nameof(SubsidiaryRepository), SubsidiaryRepository);
            _dict.Add(nameof(FinancialInformationRepository), FinancialInformationRepository);
            _dict.Add(nameof(RelProviderProductRepository), RelProviderProductRepository);
            _dict.Add(nameof(RelProviderStationRepository), RelProviderStationRepository);

            #endregion

            #region MissingProducts

            _dict.Add(nameof(MissingProductRepository), MissingProductRepository);
            _dict.Add(nameof(PurchaseStateRepository), PurchaseStateRepository);

            #endregion

            #region PurchaseOrders

            _dict.Add(nameof(PurchaseOrderHeaderRepository), PurchaseOrderHeaderRepository);
            _dict.Add(nameof(PurchaseOrderDetailRepository), PurchaseOrderDetailRepository);

            _dict.Add(nameof(ServicePOHeaderRepository), ServicePOHeaderRepository);
            _dict.Add(nameof(ServicePODetailRepository), ServicePODetailRepository);

            #endregion

            #region QuoteRequests

            _dict.Add(nameof(QuoteRequestHeaderRepository), QuoteRequestHeaderRepository);
            _dict.Add(nameof(QuoteRequestDetailRepository), QuoteRequestDetailRepository);
            _dict.Add(nameof(RelQuoteRequestProviderRepository), RelQuoteRequestProviderRepository);

            _dict.Add(nameof(QuoteRequestResponseHeaderRepository), QuoteRequestResponseHeaderRepository);
            _dict.Add(nameof(QuoteRequestResponseDetailRepository), QuoteRequestResponseDetailRepository);

            #endregion

            #endregion

            #region Sales

            _dict.Add(nameof(OrderHeaderRepository), OrderHeaderRepository);
            _dict.Add(nameof(OrderDetailRepository), OrderDetailRepository);
            _dict.Add(nameof(OrderStateRepository), OrderStateRepository);
            _dict.Add(nameof(PriceRepository), PriceRepository);
            _dict.Add(nameof(DeliveryDateHistoryRepository), DeliveryDateHistoryRepository);

            #endregion

            #region Production
            _dict.Add(nameof(WorkOrderHeaderRepository), WorkOrderHeaderRepository);
            _dict.Add(nameof(WorkOrderRepository), WorkOrderRepository);
            _dict.Add(nameof(WorkOrderItemRepository), WorkOrderItemRepository);
            _dict.Add(nameof(WorkActivityRepository), WorkActivityRepository);
            _dict.Add(nameof(WorkActionRepository), WorkActionRepository);
            _dict.Add(nameof(GroupedWorkActionRepository), GroupedWorkActionRepository);
            #endregion

            #region Logistics

            #region Incomes

            _dict.Add(nameof(IncomeHeaderRepository), IncomeHeaderRepository);
            _dict.Add(nameof(IncomeDetailRepository), IncomeDetailRepository);
            _dict.Add(nameof(IncomeStateRepository), IncomeStateRepository);

            #endregion

            #endregion

            #region CommercialDocuments

            #region DeliveryNote

            _dict.Add(nameof(DeliveryNoteHeaderRepository), DeliveryNoteHeaderRepository);
            _dict.Add(nameof(DeliveryNoteDetailRepository), DeliveryNoteDetailRepository);

            #endregion

            #endregion

        }

        public async Task<int> Commit(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                    _dbContext.Dispose();
                }
            }

            disposed = true;
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            Type _type = GetType();
            foreach (var item in _type.GetProperties())
            {
                if (item.PropertyType.GetInterfaces().Any(x => x.FullName == typeof(IGenericRepository<T>).FullName))
                {
                    return (IGenericRepository<T>)_dict[item.Name];
                }
            }
            // todo : tirar excepcion propia
            throw new NotImplementedException();
        }

        public async Task<int> Commit()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }
    }
}