using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductByIdWithUnitMeasureSpecification : BaseSpecification<Product>
    {
        public ProductByIdWithUnitMeasureSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductFeature);
            AddInclude(x => x.UnitMeasure);
            AddInclude(x => x.StockUnitMeasure);
            AddInclude(x => x.RelProviderProducts);
            AddInclude(x => x.LocationStation);
            AddInclude(x => x.StorageUnitMeasure);
            AddInclude(x => x.StockWidthUnitMeasure);
            AddInclude(x => x.StockLengthUnitMeasure);
            AddInclude(x => x.StockHeightUnitMeasure);
            AddInclude(x => x.StockQuantityUnitMeasure);
            AddInclude(x => x.StockWeightUnitMeasure);
            AddInclude(x => x.ExistenceUnitMeasure);
            AddInclude(x => x.MinimumUnitMeasure);
            AddInclude(x => x.QuantityToOrderUnitMeasure);
            AddInclude(x => x.SubCategory);

            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WidthUnitMeasure)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.HeightUnitMeasure)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.LengthUnitMeasure)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PriceUnitMeasure)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PresentationUnitMeasure)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.WeightUnitMeasure)}");
        }
    }
}
