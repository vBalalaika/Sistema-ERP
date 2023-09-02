using ERP.Domain.Entities.Production;

namespace ERP.Application.Specification.Production.WorkOrderHeaderSpecifications
{
    public class WorkOrderHeaderWithWOSpecification : BaseSpecification<WorkOrderHeader>
    {

        public WorkOrderHeaderWithWOSpecification()
        {
            AddInclude(x => x.WorkOrders);

        }
        public WorkOrderHeaderWithWOSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.WorkOrders);

        }
    }

}
