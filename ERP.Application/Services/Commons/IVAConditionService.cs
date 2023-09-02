using AutoMapper;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Commons
{
    public class IVAConditionService : IIVAConditionService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public IVAConditionService(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            _unitOfWork = unitOfWOrk;
            _mapper = mapper;
        }

        public async Task<IVACondition> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<IVACondition>().GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<IVACondition>> GetListAsync()
        {
            return await _unitOfWork.GetRepository<IVACondition>().GetListAsync();
        }
    }
}
