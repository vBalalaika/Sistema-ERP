using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class ServicePODetailBySPOHIdSpecification : BaseSpecification<ServicePODetail>
    {
        public ServicePODetailBySPOHIdSpecification(int spohId) : base(spod => spod.ServicePOHeaderId == spohId)
        {
            AddInclude(spod => spod.ServicePOHeader);
        }
    }
}
