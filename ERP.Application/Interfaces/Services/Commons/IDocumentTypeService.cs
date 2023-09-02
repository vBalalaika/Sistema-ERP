using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Commons
{
    public interface IDocumentTypeService
    {
        Task<DocumentType> GetByIdAsync(int id);
        Task<IReadOnlyList<DocumentType>> GetListAsync();
    }
}
