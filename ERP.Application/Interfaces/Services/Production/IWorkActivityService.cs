using ERP.Application.DTOs.Entities.Production;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Production
{
    public interface IWorkActivityService : IGenericService<WorkActivity, WorkActivityDTO>
    {
        Task<bool> UpdateListAsync(IList<WorkActivityDTO> entityListDtoToUpdate);
    }
}
