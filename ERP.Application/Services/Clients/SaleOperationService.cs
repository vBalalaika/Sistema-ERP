using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Domain.Entities.Clients;
using System.Threading.Tasks;

namespace ERP.Application.Services.Clients
{
    public class SaleOperationService : GenericService<SaleOperation, SaleOperationDTO>, ISaleOperationService
    {
        public SaleOperationService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
        public async Task<SaleOperation> GetByIdLazyLoad(int id)
        {
            return await _unitOfWork.SaleOperationRepository.GetByIdLazyLoadAsync(id);
        }


    }
}
