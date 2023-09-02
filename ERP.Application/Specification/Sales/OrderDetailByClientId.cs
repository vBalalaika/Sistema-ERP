using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailByClientId : BaseSpecification<OrderDetail>
    {
        public OrderDetailByClientId(int clientId) : base(d => d.OrderHeader.ClientId == clientId)
        {
            AddInclude(d => d.OrderHeader);
            AddInclude(d => d.Product);
            AddInclude(d => d.Structure);
            AddInclude(d => d.SupplyVoltage);

            AddInclude($"{nameof(OrderDetail.OrderHeader)}.{nameof(OrderHeader.Client)}");
            AddInclude($"{nameof(OrderDetail.Product)}.{nameof(Product.SubCategory)}");
        }
    }
}
