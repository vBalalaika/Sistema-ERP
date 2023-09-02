using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class PurchaseOrderDetailByPOIdSpecification : BaseSpecification<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailByPOIdSpecification(int pohId) : base(pod => pod.PurchaseOrderHeaderId == pohId)
        {
            AddInclude(pod => pod.PurchaseOrderHeader);

        }
    }
}
