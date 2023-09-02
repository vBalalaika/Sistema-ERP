using ERP.Domain.Entities.Production;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Specification.Production.WorkActivitySpecifications
{
    public class WorkActivityWithAllByProductStationIdSpecification : BaseSpecification<WorkActivity>
    {
        public WorkActivityWithAllByProductStationIdSpecification(int workActivityId, int productStationId) : base(x => x.Id == workActivityId && x.ProductStationId == productStationId)
        {
            AddInclude($"{nameof(WorkActivity.WorkOrderItem)}.{nameof(WorkOrderItem.Product)}");
            AddInclude($"{nameof(WorkActivity.ProductStation)}.{nameof(RelProductStation.Station)}");
        }
    }
}
