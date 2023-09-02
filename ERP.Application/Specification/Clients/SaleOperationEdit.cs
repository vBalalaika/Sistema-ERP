using ERP.Domain.Entities.Clients;
using ERP.Domain.Entities.ProductMod;
using ERP.Domain.Entities.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Specification.Clients
{
    public class SaleOperationEdit : BaseSpecification<SaleOperation>
    {
        public SaleOperationEdit(int id) : base(so => so.Id == id)
        {
            AddInclude(so => so.OrderDetails);
            AddInclude(so => so.Communications);

            //AddInclude($"{nameof(SaleOperation.Communications)}.{nameof(Communication.ConsultedProduct)}");
            AddInclude($"{nameof(SaleOperation.OrderDetails)}.{nameof(OrderDetail.Product)}");

        }
    }
}
