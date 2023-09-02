using AutoMapper;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Commons
{
    public class OperationStateService : IOperationStateService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public OperationStateService(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            _unitOfWork = unitOfWOrk;
            _mapper = mapper;
        }

        public async Task<OperationState> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<OperationState>().GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<OperationState>> GetListAsync()
        {
            return await _unitOfWork.GetRepository<OperationState>().GetListAsync();
        }
    }
}
