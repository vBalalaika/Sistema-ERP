using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class PriceSpecification : BaseSpecification<Price>
    {
        public PriceSpecification()
        {
            AddOrderByDescending(p => p.Id);
            AddInclude(p => p.Product);
            AddInclude(p => p.Structure);
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.ProductFeature)}");

            AddInclude($"{nameof(Price.Product)}.{nameof(Product.UnitMeasure)}");
        }

        public PriceSpecification(int id) : base(p => p.Id == id)
        {
            AddOrderByDescending(p => p.Id);
            AddInclude(p => p.Product);
            AddInclude(p => p.Structure);
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.RelProviderProducts)}");
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.ProductFeature)}");

            AddInclude($"{nameof(Price.Product)}.{nameof(Product.UnitMeasure)}");
        }

        public PriceSpecification(int productId, int? structureId) : base(p => (p.StructureId != null && structureId != null) ? p.StructureId == structureId && p.ProductId == productId : p.ProductId == productId)
        {
            AddOrderByDescending(p => p.Id);
            AddInclude(p => p.Product);
            AddInclude(p => p.Structure);
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.SubCategory)}");
            AddInclude($"{nameof(Price.Product)}.{nameof(Product.ProductFeature)}");

            AddInclude($"{nameof(Price.Product)}.{nameof(Product.UnitMeasure)}");
        }
    }
}