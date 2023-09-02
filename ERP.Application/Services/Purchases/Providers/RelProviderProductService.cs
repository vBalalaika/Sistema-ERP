using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;

namespace ERP.Application.Services.Purchases.Providers
{
    public class RelProviderProductService : GenericService<RelProviderProduct, RelProviderProductDTO>, IRelProviderProductService
    {
        public RelProviderProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

    }
}
