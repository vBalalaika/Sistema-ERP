using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailEmptySpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailEmptySpecification()
        {


        }

        public OrderDetailEmptySpecification(int id) : base(x => x.Id == id)
        {


        }
    }
}
