using ERP.Domain.Entities.Lists;

namespace ERP.Application.Specification.Lists
{
    public class CitySpecification : BaseSpecification<City>
    {
        public CitySpecification()
        {
            AddInclude(c => c.State);
            AddOrderByDescending(c => c.Name);
        }

        public CitySpecification(int id) : base(ct => ct.Id == id)
        {

        }
    }
}
