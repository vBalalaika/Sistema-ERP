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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Contexts
{
    public interface IApplicationDbContext
    {
        IDbConnection Connection { get; }
        bool HasChanges { get; }

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        #region Products
        DbSet<Product> Products { get; set; }
        #endregion

        #region Clients
        DbSet<Client> Clients { get; set; }
        DbSet<Reminder> Reminders { get; set; }
        DbSet<Communication> Communications { get; set; }
        DbSet<ConsultedProduct> ConsultedProducts { get; set; }
        DbSet<SaleOperation> SaleOperations { get; set; }
        #endregion

        #region Lists
        DbSet<State> States { get; set; }
        DbSet<City> Cities { get; set; }
        #endregion

        #region Commons
        DbSet<Country> Countries { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<OperationState> OperationStates { get; set; }
        DbSet<IVACondition> IVAConditions { get; set; }
        DbSet<DollarExchangeRate> DollarExchangeRates { get; set; }
        #endregion

        #region Purchases

        #region Providers

        DbSet<Provider> Providers { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<Subsidiary> Subsidiaries { get; set; }
        DbSet<FinancialInformation> FinancialInformations { get; set; }
        DbSet<RelProviderProduct> RelProviderProducts { get; set; }
        DbSet<RelProviderStation> RelProviderStations { get; set; }

        #endregion

        #region MissingProducts

        DbSet<MissingProduct> MissingProducts { get; set; }
        DbSet<PurchaseState> PurchaseStates { get; set; }

        #endregion

        #region PurchaseOrders

        DbSet<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
        DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        DbSet<ServicePOHeader> ServicePurchaseOrderHeaders { get; set; }
        DbSet<ServicePODetail> ServicePurchaseOrderDetails { get; set; }

        #endregion

        #region QuoteRequests

        DbSet<QuoteRequestHeader> QuoteRequestHeaders { get; set; }
        DbSet<QuoteRequestDetail> QuoteRequestDetails { get; set; }
        DbSet<RelQuoteRequestProvider> RelQuoteRequestProviders { get; set; }

        DbSet<QuoteRequestResponseHeader> QuoteRequestResponseHeaders { get; set; }
        DbSet<QuoteRequestResponseDetail> QuoteRequestResponseDetails { get; set; }

        #endregion

        #endregion

        #region Sales

        DbSet<OrderHeader> OrderHeaders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<OrderState> OrderStates { get; set; }
        DbSet<Price> Prices { get; set; }
        DbSet<DeliveryDateHistory> DeliveryDateHistories { get; set; }

        #endregion

        #region Production
        DbSet<WorkOrder> WorkOrders { get; set; }
        DbSet<WorkOrderItem> WorkOrderItems { get; set; }

        DbSet<GroupedWorkAction> GroupedWorkActions { get; set; }
        #endregion

        #region Logistics

        #region Incomes
        DbSet<IncomeHeader> IncomeHeaders { get; set; }
        DbSet<IncomeDetail> IncomeDetails { get; set; }
        DbSet<IncomeState> IncomeStates { get; set; }
        #endregion

        #endregion

        #region CommercialDocuments

        #region DeliveryNote

        DbSet<DeliveryNoteHeader> DeliveryNoteHeaders { get; set; }
        DbSet<DeliveryNoteDetail> DeliveryNoteDetails { get; set; }

        #endregion

        #endregion

    }
}