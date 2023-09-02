using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.PurchaseOrders;

namespace ERP.Application.Specification.Purchases.PurchaseOrders
{
    public class PurchaseOrderDetailSpecification : BaseSpecification<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailSpecification()
        {
            AddInclude(pod => pod.PurchaseOrderHeader);
            AddInclude(pod => pod.MissingProduct);
            AddInclude(pod => pod.ProviderUnitMeasure);
            AddInclude(pod => pod.PriceUnitMeasure);

            AddInclude($"{nameof(PurchaseOrderDetail.PurchaseOrderHeader)}.{nameof(PurchaseOrderHeader.Provider)}");
            AddInclude($"{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.UnitMeasure)}");
        }

        public PurchaseOrderDetailSpecification(int id) : base(pod => pod.Id == id)
        {
            AddInclude(pod => pod.PurchaseOrderHeader);
            AddInclude(pod => pod.MissingProduct);
            AddInclude(pod => pod.ProviderUnitMeasure);
            AddInclude(pod => pod.PriceUnitMeasure);

            AddInclude($"{nameof(PurchaseOrderDetail.PurchaseOrderHeader)}.{nameof(PurchaseOrderHeader.Provider)}");
            AddInclude($"{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(PurchaseOrderDetail.MissingProduct)}.{nameof(MissingProduct.Product)}.{nameof(Product.UnitMeasure)}");
        }
    }
}
