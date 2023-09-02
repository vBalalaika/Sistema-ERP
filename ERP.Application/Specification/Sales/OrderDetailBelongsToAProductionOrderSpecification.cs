using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailBelongsToAProductionOrderSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailBelongsToAProductionOrderSpecification(bool belongsToAProductionOrder) : base(od => od.BelongsToAProductionOrder == belongsToAProductionOrder)
        {

        }
    }
}
