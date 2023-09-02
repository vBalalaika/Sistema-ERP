using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Clients
{
    public class CommunicationViewManager : ViewManager<Communication, CommunicationDTO>, ICommunicationViewManager
    {
        private readonly ICommunicationService _communicationService;
        private readonly IMapper _mapper;

        public CommunicationViewManager(ICommunicationService communicationService, IMapper mapper) : base(communicationService, mapper)
        {
            _communicationService = communicationService;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<CommunicationDTO>>> FindBySpecification(ISpecification<Communication> specification)
        {
            var communications = await _communicationService.FindWithSpecificationPattern(specification);
            IReadOnlyList<CommunicationDTO> communicationsDto = _mapper.Map<IReadOnlyList<CommunicationDTO>>(communications);
            return Result<IReadOnlyList<CommunicationDTO>>.Success(communicationsDto);
        }
    }
}
