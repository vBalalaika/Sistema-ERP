using ERP.Domain.Entities.Sales;

namespace ERP.Application.Specification.Sales
{
    public class DeliveryDateHistoryByODIdSpecifications : BaseSpecification<DeliveryDateHistory>
    {
        public DeliveryDateHistoryByODIdSpecifications(int orderDetailId) : base(ddh => ddh.OrderDetailId == orderDetailId)
        {
            AddInclude(ddh => ddh.OrderDetail);
        }
    }
}
