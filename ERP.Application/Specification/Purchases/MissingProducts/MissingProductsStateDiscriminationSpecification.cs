using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Purchases.MissingProducts;

namespace ERP.Application.Specification.Purchases.MissingProducts
{
    public class MissingProductsStateDiscriminationSpecification : BaseSpecification<MissingProduct>
    {
        //public MissingProductsStateDiscriminationSpecification() : base(mp => mp.StateMissingProductId != 4 && mp.StateMissingProductId != 5)
        public MissingProductsStateDiscriminationSpecification() : base(mp => mp.StateMissingProductId <= 3)
        {
            AddInclude(mp => mp.Product);
            AddInclude(mp => mp.Provider);
            AddInclude($"{nameof(MissingProduct.Product)}.{nameof(Product.RelProviderProducts)}");
        }
    }
}
