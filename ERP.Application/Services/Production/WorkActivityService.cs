using AutoMapper;
using ERP.Application.DTOs.Entities.Production;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Production;
using ERP.Domain.Entities.Production;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Production
{
    public class WorkActivityService : GenericService<WorkActivity, WorkActivityDTO>, IWorkActivityService
    {
        public WorkActivityService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public virtual async Task<bool> UpdateListAsync(IList<WorkActivityDTO> entityListDtoToUpdate)
        {
            foreach (WorkActivityDTO waDTO in entityListDtoToUpdate)
            {
                WorkActivity entity = MapToEntity(waDTO);
                _unitOfWork.WorkActivityRepository.Update(entity);
            }
            await _unitOfWork.Commit();
            return true;
        }
    }
}
