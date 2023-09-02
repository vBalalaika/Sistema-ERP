using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailByStructureIdSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailByStructureIdSpecification(int structureId) : base(x => x.StructureId == structureId)
        {

        }
    }
}
