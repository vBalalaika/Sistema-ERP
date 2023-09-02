using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailByRowOrderSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailByRowOrderSpecification(int rowOrder)
        {

        }

        //public OrderDetailByRowOrderSpecification(int rowOrder) : base(od => od.rowOrder == rowOrder)
        //{

        //}
    }
}
