using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.Providers
{
    public class SubsidiaryViewManager : ViewManager<Subsidiary, SubsidiaryDTO>, ISubsidiaryViewManager
    {
        private readonly ISubsidiaryService _subsidiaryService;
        private readonly IMapper _mapper;

        public SubsidiaryViewManager(ISubsidiaryService subsidiaryService, IMapper mapper) : base(subsidiaryService, mapper)
        {
            _subsidiaryService = subsidiaryService;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Subsidiary>> GetSubsidiariesByProviderId(int id)
        {
            var subsidiaries = await _subsidiaryService.GetByProviderId(id);
            var subsidiariesDTO = _mapper.Map<IReadOnlyList<SubsidiaryDTO>>(subsidiaries);
            return subsidiariesDTO;
        }
    }
}
