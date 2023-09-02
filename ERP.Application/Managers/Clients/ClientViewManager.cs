using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Application.Interfaces.Services.Lists;
using ERP.Application.Interfaces.ViewManagers.Clients;
using ERP.Application.Specification;
using ERP.Domain.Entities.Clients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Clients
{
    public class ClientViewManager : ViewManager<Client, ClientDTO>, IClientViewManager
    {
        private readonly IClientService _clientservice;
        private readonly IMapper _mapper;

        public ClientViewManager(IClientService clientService, IMapper mapper, ICountryService countryService, IDocumentTypeService documentTypeService, IOperationStateService operationStateService, IStateService stateService, ICityService cityService) : base(clientService, mapper)
        {
            _clientservice = clientService;
            _mapper = mapper;
        }

        public async Task<Result<int>> DeactivateAsync(int id)
        {
            try
            {
                var idDeactivate = await _clientservice.DeactivateAsync(id);
                return Result<int>.Success(idDeactivate);
            }
            catch (ElementNotFoundException ex)
            {
                return Result<int>.Fail(ex.Message);
            }
        }

        public async Task<Result<IReadOnlyList<ClientDTO>>> FindBySpecification(ISpecification<Client> specification)
        {
            var clients = await _clientservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<ClientDTO> clientsDto = _mapper.Map<IReadOnlyList<ClientDTO>>(clients);
            return Result<IReadOnlyList<ClientDTO>>.Success(clientsDto);
        }

        public async Task<Result<ClientDTO>> GetByIdLazyLoad(int id)
        {
            Client entity = await _clientservice.GetByIdLazyLoad(id);
            ClientDTO entityDto = _mapper.Map<ClientDTO>(entity);
            return Result<ClientDTO>.Success(entityDto);
        }
    }
}
