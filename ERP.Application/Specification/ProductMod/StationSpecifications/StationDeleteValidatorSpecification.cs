using System.Linq;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.ProductMod.StationSpecifications
{
    public class StationDeleteValidatorSpecification : BaseSpecification<Station>
    {
        public StationDeleteValidatorSpecification(int idStation)
            : base(x => x.RelProductStations.Any(rel => rel.StationId == idStation))
        {
            AddInclude(x => x.RelProductStations);
        }
    }
}
