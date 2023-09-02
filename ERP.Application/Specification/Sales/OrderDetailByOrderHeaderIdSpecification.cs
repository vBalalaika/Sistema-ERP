using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailByOrderHeaderIdSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailByOrderHeaderIdSpecification(int orderHeaderId) : base(od => od.OrderHeaderId == orderHeaderId)
        {

        }
    }
}
