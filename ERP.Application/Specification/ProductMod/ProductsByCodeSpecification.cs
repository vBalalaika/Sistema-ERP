using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductsByCodeSpecification : BaseSpecification<Product>
    {

        public ProductsByCodeSpecification(string code) : base(x => x.Code.Equals(code))
        {
            AddOrderByDescending(x => x.Id);
        }

    }
}
