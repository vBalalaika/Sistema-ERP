using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.Purchases.MissingProducts
{
    public class MissingProductByProductIdSpecification : BaseSpecification<MissingProduct>
    {
        public MissingProductByProductIdSpecification(int productId) : base(mp => mp.ProductId == productId)
        {
            AddInclude(mp => mp.Product);
            AddInclude(mp => mp.QuantityToOrderUnitMeasure);
            AddInclude(mp => mp.StorageUnitMeasure);
            AddInclude(mp => mp.Provider);

            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.QuantityToOrderUnitMeasure)}");

            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WidthUnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.HeightUnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.LengthUnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PriceUnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PresentationUnitMeasure)}");
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WeightUnitMeasure)}");
        }
    }
}
