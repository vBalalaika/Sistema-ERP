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
    public class SaleOperationForSelect2 : BaseSpecification<SaleOperation>
    {
        public SaleOperationForSelect2()
        {

        }
        public SaleOperationForSelect2(int id) : base(so => so.Id == id)
        {

        }
        public SaleOperationForSelect2(string search) : base(so => so.Operation.ToUpper().Contains(search.ToUpper()))
        {
            
        }
    }
}
