using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.Providers
{
    public class FinancialInformationService : GenericService<FinancialInformation, FinancialInformationDTO>, IFinancialInformationService
    {
        public FinancialInformationService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public Task<IReadOnlyList<FinancialInformation>> GetByProviderId(int id)
        {
            var financialInfo = _unitOfWork.FinancialInformationRepository.GetByProviderIdAsync(id);
            return financialInfo;
        }
    }
}
