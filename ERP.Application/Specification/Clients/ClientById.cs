using ERP.Domain.Entities.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Application.Specification.Clients
{
    public class ClientById : BaseSpecification<Client>
    {
        public ClientById(int id) : base(c => c.Id == id)
        {

        }
    }
}
