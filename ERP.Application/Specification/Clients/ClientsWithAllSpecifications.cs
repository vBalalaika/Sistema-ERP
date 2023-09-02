using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class ClientsWithAllSpecifications : BaseSpecification<Client>
    {
        public ClientsWithAllSpecifications()
        {
            AddInclude(c => c.Country);
            AddInclude(c => c.State);
            AddInclude(c => c.City);
            AddInclude(c => c.OperationState);
            AddInclude(c => c.DocumentType);
        }

        public ClientsWithAllSpecifications(int id) : base(c => c.Id == id)
        {
            AddInclude(c => c.Country);
            AddInclude(c => c.State);
            AddInclude(c => c.City);
            AddInclude(c => c.OperationState);
            AddInclude(c => c.DocumentType);
        }
    }
}
