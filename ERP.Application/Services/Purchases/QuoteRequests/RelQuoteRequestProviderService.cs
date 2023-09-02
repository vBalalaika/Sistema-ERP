using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.QuoteRequests;

namespace ERP.Application.Services.Purchases.QuoteRequests
{
    public class RelQuoteRequestProviderService : GenericService<RelQuoteRequestProvider, RelQuoteRequestProviderDTO>, IRelQuoteRequestProviderService
    {
        public RelQuoteRequestProviderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
