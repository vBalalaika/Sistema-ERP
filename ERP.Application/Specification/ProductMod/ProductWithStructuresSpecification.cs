using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithStructuresSpecification : BaseSpecification<Product>
    {
        public ProductWithStructuresSpecification(int id)
            : base(x => x.Id == id)
        {

            AddInclude(x => x.Structures);
            AddInclude(x => x.SubCategory);

            AddInclude($"{nameof(Product.Structures)}.{nameof(Structure.SupplyVoltage)}");

        }
    }
}
