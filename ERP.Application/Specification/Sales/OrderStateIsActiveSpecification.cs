using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderStateIsActiveSpecification : BaseSpecification<OrderState>
    {
        public OrderStateIsActiveSpecification() : base(sos => sos.IsActive == true)
        {

        }
    }
}
