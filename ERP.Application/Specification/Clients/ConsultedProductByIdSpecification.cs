using ERP.Domain.Entities.Clients;

namespace ERP.Application.Specification.Clients
{
    public class ConsultedProductByIdSpecification : BaseSpecification<ConsultedProduct>
    {
        public ConsultedProductByIdSpecification(int id) : base(cp => cp.Id == id)
        {
            AddInclude(cp => cp.Communication);
            AddInclude($"{nameof(ConsultedProduct.Communication)}.{nameof(Communication.Client)}");
        }
    }
}
