using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;
using System.Linq;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivityWithProductDataSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivityWithProductDataSpecification(int activityId) : base(x => x.StateActivity == true && x.Id == activityId)
        {
            AddInclude(x => x.WorkActions.OrderBy(wa => wa.StartDate));
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}.{nameof(Product.ProductFeature)}");
        }
    }
}
