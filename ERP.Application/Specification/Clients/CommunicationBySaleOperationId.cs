using ERP.Domain.Entities.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Specification.Clients
{
    public class CommunicationBySaleOperationId : BaseSpecification<Communication>
    {
        public CommunicationBySaleOperationId(int saleOperationId) : base(c => c.SaleOperationId == saleOperationId)
        {

        }
    }
}
