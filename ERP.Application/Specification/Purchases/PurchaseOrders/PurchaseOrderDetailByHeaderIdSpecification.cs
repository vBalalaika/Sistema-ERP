using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class PurchaseOrderDetailByHeaderIdSpecification : BaseSpecification<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailByHeaderIdSpecification(int headerId) : base(x => x.PurchaseOrderHeaderId == headerId)
        {
            AddInclude(x => x.MissingProduct);
            AddInclude(x => x.ProviderUnitMeasure);
        }
    }
}
