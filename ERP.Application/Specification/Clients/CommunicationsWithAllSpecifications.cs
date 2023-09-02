using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class CommunicationsWithAllSpecifications : BaseSpecification<Communication>
    {
        public CommunicationsWithAllSpecifications()
        {
            AddInclude(c => c.Client);
            AddInclude(c => c.ConsultedProducts);
            AddInclude(c => c.SaleOperation);
        }

        //public CommunicationsWithAllSpecifications(int clientId) : base(x => x.ClientId == clientId && x.PostSale == false)
        public CommunicationsWithAllSpecifications(int clientId) : base(x => x.ClientId == clientId)
        {
            AddInclude(c => c.Client);
            AddInclude(c => c.ConsultedProducts);
        }

        // For menu list of communications with postSale = false and list of communications post sale with postSale == true
        public CommunicationsWithAllSpecifications(bool? postSale) : base(x => x.PostSale == postSale)
        {
            AddInclude(c => c.Client);
            AddInclude(c => c.ConsultedProducts);
        }
    }
}
