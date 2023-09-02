using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductByIdSpecification : BaseSpecification<Product>
    {
        public ProductByIdSpecification()
        {
            AddOrderByDescending(x => x.Id);
        }

        public ProductByIdSpecification(int id) : base(pr => pr.Id == id)
        {
            AddInclude(pr => pr.ProductFeature);
            AddInclude(pr => pr.SubCategory);
            AddInclude(pr => pr.Structures);
            AddInclude(pr => pr.ProductPieceTypes);
            AddInclude(pr => pr.RelProviderProducts);

            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.Provider)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.UnitMeasure)}");
            AddInclude($"{nameof(Product.RelProviderProducts)}.{nameof(RelProviderProduct.PriceUnitMeasure)}");

            AddInclude($"{nameof(Product.Structures)}.{nameof(Structure.LastVersion)}");
            AddInclude($"{nameof(Product.Structures)}.{nameof(Structure.StructureItems)}");
        }
    }
}
