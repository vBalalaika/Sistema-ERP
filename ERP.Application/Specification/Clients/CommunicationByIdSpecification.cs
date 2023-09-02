using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class CommunicationByIdSpecification : BaseSpecification<Communication>
    {
        public CommunicationByIdSpecification(int id) : base(c => c.Id == id)
        {
            AddInclude(c => c.Client);
            AddInclude(c => c.ConsultedProducts);
        }
    }
}
