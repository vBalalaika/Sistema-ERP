using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Services.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Clients
{
    public class SaleOperationViewManager : ViewManager<SaleOperation, SaleOperationDTO>, ISaleOperationViewManager
    {
        private readonly ISaleOperationService _saleOperationService;
        private readonly IMapper _mapper;

        public SaleOperationViewManager(ISaleOperationService saleOperationService, IMapper mapper) : base(saleOperationService, mapper)
        {
            _saleOperationService = saleOperationService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<SaleOperationDTO>>> FindBySpecification(ISpecification<SaleOperation> specification)
        {
            var saleOperations = await _saleOperationService.FindWithSpecificationPattern(specification);
            IReadOnlyList<SaleOperationDTO> saleOperationsDto = _mapper.Map<IReadOnlyList<SaleOperationDTO>>(saleOperations);
            return Result<IReadOnlyList<SaleOperationDTO>>.Success(saleOperationsDto);
        }
        public async Task<Result<SaleOperationDTO>> GetByIdLazyLoad(int id)
        {
            SaleOperation entity = await _saleOperationService.GetByIdLazyLoad(id);
            SaleOperationDTO entityDto = _mapper.Map<SaleOperationDTO>(entity);
            return Result<SaleOperationDTO>.Success(entityDto);
        }
    }
}
