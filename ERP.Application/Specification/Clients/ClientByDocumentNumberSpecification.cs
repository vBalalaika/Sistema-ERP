using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class ClientByDocumentNumberSpecification : BaseSpecification<Client>
    {
        public ClientByDocumentNumberSpecification(string documentNumber) : base(x => x.DocumentNumber != null && x.DocumentNumber.Equals(documentNumber))
        {
            AddOrderByDescending(x => x.Id);
        }
    }
}