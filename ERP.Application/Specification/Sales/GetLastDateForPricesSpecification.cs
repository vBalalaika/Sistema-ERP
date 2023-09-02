using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class GetLastDateForPricesSpecification : BaseSpecification<Price>
    {
        public GetLastDateForPricesSpecification(int productId, int? structureId) : base(p => (p.ProductId == productId) && ((p.StructureId != null && structureId != null) ? p.StructureId == structureId : p.Id != null))
        {
        }
    }
}