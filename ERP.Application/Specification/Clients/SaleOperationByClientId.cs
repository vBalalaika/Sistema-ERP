using ERP.Domain.Entities.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Specification.Clients
{
    public class SaleOperationByClientId : BaseSpecification<SaleOperation>
    {
        public SaleOperationByClientId(int clientId) : base(so => so.ClientId == clientId)
        {

        }

        public SaleOperationByClientId(int clientId, string filter) : base(so => so.ClientId == clientId && so.State != "Concretada")
        {

        }
    }
}
