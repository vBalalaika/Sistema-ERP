using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class RemindersWithAllSpecifications : BaseSpecification<Reminder>
    {
        public RemindersWithAllSpecifications()
        {
            AddInclude(r => r.Client);
        }
    }
}
