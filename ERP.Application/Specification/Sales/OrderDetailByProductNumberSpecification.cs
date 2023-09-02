using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailByProductNumberSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailByProductNumberSpecification(string ProductNumber) : base(od => od.ProductNumber.Equals(ProductNumber))
        {
            AddInclude(od => od.Product);
            AddInclude(od => od.Structure);
        }
    }
}
