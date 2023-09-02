using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;

namespace ERP.Application.Specification.ProductMod
{
    public class ProductsBySubCategoryIdSpecification : BaseSpecification<Product>
    {
        public ProductsBySubCategoryIdSpecification(int subCategoryId) :
            base(x => x.SubCategoryId == subCategoryId)
        {

        }
        public ProductsBySubCategoryIdSpecification(List<int> subCategoryIds) :
            base(x => subCategoryIds.Contains(x.SubCategoryId))
        {

        }

    }
}
