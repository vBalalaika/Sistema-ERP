using ERP.Domain.Entities.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Specification.Sales
{
    public class ConsultedProductByCommunicationId : BaseSpecification<ConsultedProduct>
    {
        public ConsultedProductByCommunicationId(int communicationId) : base(cp => cp.CommunicationId == communicationId)
        {
            AddInclude(cp => cp.Communication);

            AddInclude($"{nameof(ConsultedProduct.Communication)}.{nameof(Communication.Client)}");
        }
    }
}
