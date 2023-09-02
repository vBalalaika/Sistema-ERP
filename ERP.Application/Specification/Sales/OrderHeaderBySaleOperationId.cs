using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderHeaderBySaleOperationId : BaseSpecification<OrderHeader>
    {

        public OrderHeaderBySaleOperationId(int saleOperationId) : base(x => x.SaleOperationId == saleOperationId)
        {
            AddInclude(oh => oh.OrderDetails);
        }
    }
}
