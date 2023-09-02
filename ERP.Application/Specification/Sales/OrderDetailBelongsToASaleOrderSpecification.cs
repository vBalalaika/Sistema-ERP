using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailBelongsToASaleOrderSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailBelongsToASaleOrderSpecification(bool belongsToASaleOrder) : base(od => od.BelongsToASaleOrder == belongsToASaleOrder)
        {

        }
    }
}
