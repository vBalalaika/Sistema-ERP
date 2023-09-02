using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductsStoredStockSpecification : BaseSpecification<Product>
    {
        public ProductsStoredStockSpecification(bool storedStock) : base(prod => prod.ProductFeature.StoredStock == storedStock)
        {
            AddInclude(x => x.UnitMeasure);

            if (storedStock)
            {
                AddInclude(x => x.ProductFeature);
                AddInclude(x => x.SubCategory);
                AddInclude(x => x.LocationStation);
                AddInclude(x => x.StockUnitMeasure);
                AddInclude(x => x.StorageUnitMeasure);
                AddInclude(x => x.StockWidthUnitMeasure);
                AddInclude(x => x.StockLengthUnitMeasure);
                AddInclude(x => x.StockHeightUnitMeasure);
                AddInclude(x => x.StockQuantityUnitMeasure);
                AddInclude(x => x.StockWeightUnitMeasure);
                AddInclude(x => x.ExistenceUnitMeasure);
                AddInclude(x => x.MinimumUnitMeasure);
                AddInclude(x => x.QuantityToOrderUnitMeasure);
                AddInclude(x => x.Structures);
                AddInclude(x => x.Archives);

                AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
                AddInclude($"{nameof(Product.Archives)}.{nameof(Archive.ArchiveType)}");
                AddInclude($"{nameof(Product.Structures)}.{nameof(Structure.StructureItems)}.{nameof(StructureItem.SonProduct)}");
            }

        }

        // For efficient search on select2
        public ProductsStoredStockSpecification(bool storedStock, string searchParameterByCodeOrDescription) : base(prod => prod.ProductFeature.StoredStock == storedStock && (prod.Code.Contains(searchParameterByCodeOrDescription) || prod.Description.Contains(searchParameterByCodeOrDescription)))
        {
            AddInclude(x => x.UnitMeasure);

            if (storedStock)
            {
                AddInclude(x => x.ProductFeature);
                AddInclude(x => x.SubCategory);
                AddInclude(x => x.LocationStation);
                AddInclude(x => x.StockUnitMeasure);
                AddInclude(x => x.StorageUnitMeasure);
                AddInclude(x => x.StockWidthUnitMeasure);
                AddInclude(x => x.StockLengthUnitMeasure);
                AddInclude(x => x.StockHeightUnitMeasure);
                AddInclude(x => x.StockQuantityUnitMeasure);
                AddInclude(x => x.StockWeightUnitMeasure);
                AddInclude(x => x.ExistenceUnitMeasure);
                AddInclude(x => x.MinimumUnitMeasure);
                AddInclude(x => x.QuantityToOrderUnitMeasure);
                AddInclude(x => x.Structures);
                AddInclude(x => x.Archives);

                AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
                AddInclude($"{nameof(Product.Archives)}.{nameof(Archive.ArchiveType)}");
                AddInclude($"{nameof(Product.Structures)}.{nameof(Structure.StructureItems)}.{nameof(StructureItem.SonProduct)}");
            }

        }

        public ProductsStoredStockSpecification(int productId) : base(prod => prod.ProductFeature.StoredStock == true && prod.Id == productId)
        {
            AddInclude(x => x.ProductFeature);
            AddInclude(x => x.UnitMeasure);
            AddInclude(x => x.Structures);
            AddInclude(x => x.SubCategory);
            AddInclude(x => x.LocationStation);
            AddInclude(x => x.StockUnitMeasure);

            AddInclude(x => x.StorageUnitMeasure);
            AddInclude(x => x.StockWidthUnitMeasure);
            AddInclude(x => x.StockLengthUnitMeasure);
            AddInclude(x => x.StockHeightUnitMeasure);
            AddInclude(x => x.StockQuantityUnitMeasure);
            AddInclude(x => x.StockWeightUnitMeasure);
            AddInclude(x => x.ExistenceUnitMeasure);
            AddInclude(x => x.MinimumUnitMeasure);
            AddInclude(x => x.QuantityToOrderUnitMeasure);

            AddInclude(x => x.Archives);

            AddInclude($"{nameof(Product.SubCategory)}.{nameof(SubCategory.Category)}");
            AddInclude($"{nameof(Product.Archives)}.{nameof(Archive.ArchiveType)}");
        }
    }
}
