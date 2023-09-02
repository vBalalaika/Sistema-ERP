using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.MissingProducts
{
    public class MissingProductByIdSpecification : BaseSpecification<MissingProduct>
    {
        public MissingProductByIdSpecification(int id) : base(mp => mp.Id == id)
        {
            AddInclude(x => x.Product);
            AddInclude(x => x.Provider);
            AddInclude(x => x.QuantityToOrderUnitMeasure);
            AddInclude(x => x.StorageUnitMeasure);
            AddInclude(x => x.StateMissingProduct);
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.StockUnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.LocationStation)}");

            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.Provider)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PriceUnitMeasure)}");
        }
    }
}
