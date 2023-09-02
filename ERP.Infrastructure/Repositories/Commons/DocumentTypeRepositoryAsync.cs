using ERP.Application.Interfaces.Repositories.Commons;
using ERP.Domain.Entities.Commons;
using ERP.Infrastructure.DbContexts;

namespace ERP.Infrastructure.Repositories.Commons
{
    public class DocumentTypeRepositoryAsync : GenericRepository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
