using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StationSpecifications
{
    public class StationWithFunctionalAreaSpecification : BaseSpecification<Station>
    {
        public StationWithFunctionalAreaSpecification()
        {
            AddInclude(x => x.FunctionalArea);
        }
    }
}
