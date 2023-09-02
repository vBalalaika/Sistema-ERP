using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductWithAllUnitMeasuresSpecification : BaseSpecification<Product>
    {
        public ProductWithAllUnitMeasuresSpecification()
        {
            AddInclude(p => p.UnitMeasure);
            AddInclude(p => p.StockWidthUnitMeasure);
            AddInclude(p => p.StockQuantityUnitMeasure);
            AddInclude(p => p.StockLengthUnitMeasure);
            AddInclude(p => p.StockWeightUnitMeasure);
            AddInclude(p => p.StockHeightUnitMeasure);
        }
    }
}
