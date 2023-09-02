using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class ClientsIsActiveExceptOperationStateDeBajaSpecification : BaseSpecification<Client>
    {
        public ClientsIsActiveExceptOperationStateDeBajaSpecification() : base(c => c.IsActive == true && c.OperationStateId != 5)
        {
            AddInclude(c => c.Country);
            AddInclude(c => c.State);
            AddInclude(c => c.City);
            AddInclude(c => c.OperationState);
            AddInclude(c => c.DocumentType);
        }

        public ClientsIsActiveExceptOperationStateDeBajaSpecification(int id) : base(c => c.IsActive == true && c.Id == id && c.OperationStateId != 5)
        {
            AddInclude(c => c.Country);
            AddInclude(c => c.State);
            AddInclude(c => c.City);
            AddInclude(c => c.OperationState);
            AddInclude(c => c.DocumentType);
        }
    }
}
