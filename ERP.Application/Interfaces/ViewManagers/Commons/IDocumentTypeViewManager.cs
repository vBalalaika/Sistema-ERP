using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Commons
{
    public interface IDocumentTypeViewManager
    {
        Task<Result<DocumentTypeDTO>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<DocumentTypeDTO>>> LoadAll();
    }
}
