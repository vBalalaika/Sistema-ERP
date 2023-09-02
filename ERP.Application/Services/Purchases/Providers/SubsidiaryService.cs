using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.Providers
{
    public class SubsidiaryService : GenericService<Subsidiary, SubsidiaryDTO>, ISubsidiaryService
    {
        public SubsidiaryService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public Task<IReadOnlyList<Subsidiary>> GetByProviderId(int id)
        {
            var subsidiaries = _unitOfWork.SubsidiaryRepository.GetByProviderIdAsync(id);
            return subsidiaries;
        }
    }
}
