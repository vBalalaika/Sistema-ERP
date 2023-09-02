using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderHeaderByIdSpecification : BaseSpecification<OrderHeader>
    {
        public OrderHeaderByIdSpecification(int? id) : base(x => x.Id == id)
        {
        }
    }
}