using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;

namespace ERP.Application.Specification.Purchases.MissingProducts
{
    public class MissingProductWithAllSpecification : BaseSpecification<MissingProduct>
    {
        public MissingProductWithAllSpecification()
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.Provider);
            AddInclude(x => x.StateMissingProduct);
            AddInclude(x => x.QuantityToOrderUnitMeasure);
            AddInclude(x => x.StorageUnitMeasure);
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.StockUnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.LocationStation)}");
        }
    }
}
