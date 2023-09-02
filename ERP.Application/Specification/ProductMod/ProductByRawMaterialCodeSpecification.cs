using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductByRawMaterialCodeSpecification : BaseSpecification<Product>
    {
        public ProductByRawMaterialCodeSpecification(string rawMaterialCode) : base(pr => pr.Code.Equals(rawMaterialCode))
        {
            AddInclude(pr => pr.ProductFeature);

            AddInclude(pr => pr.SubCategory);
            AddInclude(pr => pr.LocationStation);
            AddInclude(pr => pr.StockUnitMeasure);
            AddInclude(pr => pr.StorageUnitMeasure);
            AddInclude(pr => pr.StockWidthUnitMeasure);
            AddInclude(pr => pr.StockLengthUnitMeasure);
            AddInclude(pr => pr.StockHeightUnitMeasure);
            AddInclude(pr => pr.StockQuantityUnitMeasure);
            AddInclude(pr => pr.StockWeightUnitMeasure);
            AddInclude(pr => pr.ExistenceUnitMeasure);
            AddInclude(pr => pr.MinimumUnitMeasure);
            AddInclude(pr => pr.QuantityToOrderUnitMeasure);

            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");

        }
    }
}
