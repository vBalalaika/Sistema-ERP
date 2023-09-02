using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class ServicePOHeaderWithAllSpecifications : BaseSpecification<ServicePOHeader>
    {
        public ServicePOHeaderWithAllSpecifications(int id) : base(spoh => spoh.Id == id)
        {
            AddInclude(spoh => spoh.Provider);
            AddInclude(spoh => spoh.Station);
            AddInclude(spoh => spoh.PurchaseState);
            AddInclude(spoh => spoh.ServicePODetails);
            AddInclude($"{nameof(ServicePOHeader.Provider)}.{nameof(Provider.IVACondition)}");
            AddInclude($"{nameof(ServicePOHeader.Provider)}.{nameof(Provider.DocumentType)}");
            AddInclude($"{nameof(ServicePOHeader.Provider)}.{nameof(Provider.Country)}");
            AddInclude($"{nameof(ServicePOHeader.Provider)}.{nameof(Provider.State)}");
            AddInclude($"{nameof(ServicePOHeader.Provider)}.{nameof(Provider.City)}");
            AddInclude($"{nameof(ServicePOHeader.ServicePODetails)}.{nameof(ServicePODetail.Product)}");
            AddInclude($"{nameof(ServicePOHeader.ServicePODetails)}.{nameof(ServicePODetail.UnitMeasure)}");
            AddInclude($"{nameof(ServicePOHeader.ServicePODetails)}.{nameof(ServicePODetail.DeliveryNoteDetail)}");
        }
    }
}
