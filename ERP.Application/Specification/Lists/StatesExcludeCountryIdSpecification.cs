using ERP.Domain.Entities.Lists;

namespace ERP.Application.Specification.Lists
{
    public class StatesExcludeCountryIdSpecification : BaseSpecification<State>
    {
        public StatesExcludeCountryIdSpecification(int countryId) : base(st => st.CountryId != countryId)
        {
            AddInclude(st => st.Country);
        }
    }
}
