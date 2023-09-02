using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class PurchasesOrderDetailWithAllSpecification : BaseSpecification<PurchaseOrderDetail>
    {
        public PurchasesOrderDetailWithAllSpecification()
        {
            AddInclude(pod => pod.MissingProduct);
            AddInclude(pod => pod.Product);
            AddInclude(pod => pod.PurchaseOrderHeader);
            AddInclude(pod => pod.PriceUnitMeasure);
            AddInclude(pod => pod.ProviderUnitMeasure);
            AddInclude(pod => pod.PurchaseState);
            AddInclude($"{nameof(PurchaseOrderDetail.PurchaseOrderHeader)}.{nameof(PurchaseOrderHeader.Provider)}");
            AddInclude($"{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
        }

        public PurchasesOrderDetailWithAllSpecification(int id) : base(pod => pod.Id == id)
        {
            AddInclude(pod => pod.MissingProduct);
            AddInclude(pod => pod.Product);
            AddInclude(pod => pod.PurchaseOrderHeader);
            AddInclude(pod => pod.PriceUnitMeasure);
            AddInclude(pod => pod.ProviderUnitMeasure);
            AddInclude($"{nameof(PurchaseOrderDetail.PurchaseOrderHeader)}.{nameof(PurchaseOrderHeader.Provider)}");
            AddInclude($"{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}");
        }
    }
}
