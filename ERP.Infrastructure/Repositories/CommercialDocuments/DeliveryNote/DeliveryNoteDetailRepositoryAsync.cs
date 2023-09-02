using ERP.Application.Interfaces.Repositories.CommercialDocuments.DeliveryNote;
using ERP.Domain.Entities.CommercialDocuments.DeliveryNote;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.CommercialDocuments.DeliveryNote
{
    public class DeliveryNoteDetailRepositoryAsync : GenericRepository<DeliveryNoteDetail>, IDeliveryNoteDetailRepository
    {
        public DeliveryNoteDetailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IReadOnlyList<DeliveryNoteDetail>> GetAll()
        {
            return await _dbContext
              .Set<DeliveryNoteDetail>()
              .AsNoTracking()
              .ToListAsync();
        }
    }
}
