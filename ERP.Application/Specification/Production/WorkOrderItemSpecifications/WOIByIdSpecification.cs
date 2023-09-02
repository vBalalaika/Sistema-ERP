using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.Production.WorkOrderItemSpecifications
{
    public class WOIByIdSpecification : BaseSpecification<WorkOrderItem>
    {
        public WOIByIdSpecification(int woiId) : base(woi => woi.Id == woiId)
        {
            AddInclude($"{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
        }
    }
}
