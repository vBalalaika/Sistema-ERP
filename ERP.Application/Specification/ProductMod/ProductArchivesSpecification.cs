using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductArchivesSpecification : BaseSpecification<Product>
    {
        public ProductArchivesSpecification(int productId) : base(pr => pr.Id == productId)
        {
            AddInclude(x => x.Archives);
            AddInclude($"{nameof(Product.Archives)}.{nameof(Archive.ArchiveType)}");
        }
    }
}
