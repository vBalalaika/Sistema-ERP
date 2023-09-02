using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class ClientsIsActiveSpecification : BaseSpecification<Client>
    {
        public ClientsIsActiveSpecification() : base(c => c.IsActive == true)
        {
            AddInclude(c => c.Country);
            AddInclude(c => c.State);
            AddInclude(c => c.City);
            AddInclude(c => c.OperationState);
            AddInclude(c => c.DocumentType);
        }
        public ClientsIsActiveSpecification(int id) : base(c => c.IsActive == true && c.Id == id)
        {
            AddInclude(c => c.Country);
            AddInclude(c => c.State);
            AddInclude(c => c.City);
            AddInclude(c => c.OperationState);
            AddInclude(c => c.DocumentType);
        }
    }
}
