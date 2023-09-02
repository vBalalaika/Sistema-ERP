using ERP.Domain.Entities.Lists;

namespace ERP.Application.Specification.Lists
{
    public class GetCityByNameSpecification : BaseSpecification<City>
    {
        public GetCityByNameSpecification(string Name) : base(c => c.Name == Name)
        {

        }
    }
}
