using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.Providers
{
    public class FinancialInformationViewManager : ViewManager<FinancialInformation, FinancialInformationDTO>, IFinancialInformationViewManager
    {
        private readonly IFinancialInformationService _financialInformationService;
        private readonly IMapper _mapper;

        public FinancialInformationViewManager(IFinancialInformationService financialInformationService, IMapper mapper) : base(financialInformationService, mapper)
        {
            _financialInformationService = financialInformationService;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<FinancialInformation>> GetFinancialInfosByProviderId(int id)
        {
            var financialInfo = await _financialInformationService.GetByProviderId(id);
            var financialInfoDTO = _mapper.Map<IReadOnlyList<FinancialInformationDTO>>(financialInfo);
            return financialInfoDTO;
        }
    }
}
