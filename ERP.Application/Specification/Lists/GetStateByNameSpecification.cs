using ERP.Domain.Entities.Lists;

namespace ERP.Application.Specification.Lists
{
    public class GetStateByNameSpecification : BaseSpecification<State>
    {
        public GetStateByNameSpecification(string Name) : base(s => s.Name == Name)
        {

        }
    }
}
