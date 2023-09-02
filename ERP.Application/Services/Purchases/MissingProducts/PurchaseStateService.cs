using AutoMapper;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.MissingProducts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.MissingProducts
{
    public class PurchaseStateService : IPurchaseStateService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public PurchaseStateService(IUnitOfWork unitOfWOrk, IMapper mapper)
        {
            _unitOfWork = unitOfWOrk;
            _mapper = mapper;
        }

        public async Task<PurchaseState> GetByIdAsync(int id)
        {
            return await _unitOfWork.GetRepository<PurchaseState>().GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<PurchaseState>> GetListAsync()
        {
            return await _unitOfWork.GetRepository<PurchaseState>().GetListAsync();
        }
    }
}
