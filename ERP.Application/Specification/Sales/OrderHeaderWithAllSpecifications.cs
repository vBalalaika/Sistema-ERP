using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;
using System.Linq;

namespace ERP.Application.Specification.Sales
{
    public class OrderHeaderWithAllSpecifications : BaseSpecification<OrderHeader>
    {
        public OrderHeaderWithAllSpecifications()
        {
            AddInclude(oh => oh.Client);
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.OrderDetails);
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.Product)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.SupplyVoltage)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.OrderState)}");
        }

        public OrderHeaderWithAllSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(oh => oh.Client);
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.OrderDetails);
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.SupplyVoltage)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.OrderState)}");
        }
        public OrderHeaderWithAllSpecifications(string productNumber) : base(x => x.OrderDetails.Any(od => od.ProductNumber.Trim().Equals(productNumber)))
        {
            AddInclude(oh => oh.Client);
            AddInclude(oh => oh.OrderState);
            AddInclude(oh => oh.OrderDetails);
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.Product)}.{nameof(Product.UnitMeasure)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.Structure)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.SupplyVoltage)}");
            AddInclude($"{nameof(OrderHeader.OrderDetails)}.{nameof(OrderDetail.OrderState)}");
        }
    }
}
