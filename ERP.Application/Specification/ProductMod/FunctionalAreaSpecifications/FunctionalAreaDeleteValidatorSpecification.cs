using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.ProductMod.FunctionalAreaSpecifications
{
    public class FunctionalAreaDeleteValidatorSpecification : BaseSpecification<FunctionalArea>
    {
        public FunctionalAreaDeleteValidatorSpecification(int idFunctionalArea)
          : base(x => x.Stations.Any(sta => sta.FunctionalAreaId == idFunctionalArea))
        {
            AddInclude(x => x.Stations);
        }
    }
}
