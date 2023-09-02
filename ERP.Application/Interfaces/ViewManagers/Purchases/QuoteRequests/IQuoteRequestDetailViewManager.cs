using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.QuoteRequests
{
    public interface IQuoteRequestDetailViewManager : IViewManager<QuoteRequestDetail, QuoteRequestDetailDTO>
    {
        Task<Result<IReadOnlyList<QuoteRequestDetailDTO>>> FindBySpecification(ISpecification<QuoteRequestDetail> specification);
    }
}
