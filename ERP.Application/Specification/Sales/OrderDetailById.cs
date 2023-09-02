using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailById : BaseSpecification<OrderDetail>
    {
        public OrderDetailById(int orderHeaderId) : base(od => od.OrderHeaderId == orderHeaderId)
        {
            AddInclude(od => od.Product);
            AddInclude(od => od.Structure);
            AddInclude(od => od.SupplyVoltage);
            
        }
    }
}
