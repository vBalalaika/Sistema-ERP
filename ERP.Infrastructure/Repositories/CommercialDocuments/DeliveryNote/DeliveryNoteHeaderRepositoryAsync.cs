using ERP.Application.Interfaces.Repositories.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteHeaderRepositoryAsync : GenericRepository<DeliveryNoteHeader>, IDeliveryNoteHeaderRepository
    {
        public DeliveryNoteHeaderRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
