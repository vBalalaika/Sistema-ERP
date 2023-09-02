using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderHeaderWithAllDetailsSpecification : BaseSpecification<OrderHeader>
    {
        public OrderHeaderWithAllDetailsSpecification()
        {
            AddInclude(oh => oh.OrderDetails);
        }

        public OrderHeaderWithAllDetailsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(oh => oh.OrderDetails);
        }
    }
}
