using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class ClientByBusinessNameSpecification : BaseSpecification<Client>
    {
        public ClientByBusinessNameSpecification(string businessName) : base(x => x.BusinessName.Equals(businessName))
        {
            AddOrderByDescending(x => x.Id);
        }
    }
}
