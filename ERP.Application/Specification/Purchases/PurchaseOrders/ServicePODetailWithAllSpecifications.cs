using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class ServicePODetailWithAllSpecifications : BaseSpecification<ServicePODetail>
    {
        public ServicePODetailWithAllSpecifications()
        {
            AddInclude(spod => spod.Product);
            AddInclude(spod => spod.UnitMeasure);
            AddInclude(spod => spod.DeliveryNoteDetail);
            AddInclude(spod => spod.ServicePOHeader);
            AddInclude($"{nameof(ServicePODetail.ServicePOHeader)}.{nameof(ServicePOHeader.Provider)}");
            AddInclude($"{nameof(ServicePODetail.ServicePOHeader)}.{nameof(ServicePOHeader.Station)}");
            AddInclude($"{nameof(ServicePODetail.ServicePOHeader)}.{nameof(ServicePOHeader.PurchaseState)}");
        }
    }
}
