using ERP.Domain.Entities.Lists;

namespace ERP.Application.Specification.Lists
{
    public class StatesByCountryIdSpecification : BaseSpecification<State>
    {
        public StatesByCountryIdSpecification(int countryId) : base(s => s.CountryId == countryId)
        {
            AddInclude(st => st.Country);
        }
    }
}
