using ERP.Domain.Entities.Sales;
using System;

namespace ERP.Application.Specification.Sales
{
    public class OrderDetailGetMaxRowOrderSpecification : BaseSpecification<OrderDetail>
    {
        public OrderDetailGetMaxRowOrderSpecification(DateTime deliveryDate) : base(od => od.DeliveryDate.Value == deliveryDate)
        {

        }
    }
}
