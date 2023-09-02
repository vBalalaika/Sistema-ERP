using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkOrderItemSpecifications
{
    public class WorkOrderItemByStructureIdAndProductIdSpecification : BaseSpecification<WorkOrderItem>
    {
        public WorkOrderItemByStructureIdAndProductIdSpecification(int structureId, int productId) : base(woi => woi.StructureId == structureId && woi.ProductId == productId)
        {
        }
    }
}