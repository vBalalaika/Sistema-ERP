using ERP.Application.Interfaces.Repositories.Production;
using ERP.Domain.Entities.Production;
using ERP.Infrastructure.DbContexts;
using System.Linq;

namespace ERP.Infrastructure.Repositories.Production
{
    public class WorkOrderItemRepositoryAsync : GenericRepository<WorkOrderItem>, IWorkOrderItemRepository
    {
        public WorkOrderItemRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public int DeleteByWorkOrderId(int workOrderId)
        {
            var entitiesToDelete = _dbContext.Set<WorkOrderItem>().Where(woi => woi.WorkOrderId == workOrderId).ToList();
            foreach (var woi in entitiesToDelete)
            {
                _dbContext.WorkOrderItems.Remove(woi);
            }
            return workOrderId;
        }
    }
}