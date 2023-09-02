using AutoMapper;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Domain.Entities.Sales;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Sales
{
    public class OrderStateService : IOrderStateService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public OrderStateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderState> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<OrderState>().GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<OrderState>> GetListAsync()
        {
            return await _unitOfWork.GetRepository<OrderState>().GetListAsync();
        }
    }
}
