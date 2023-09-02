using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Specification;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.ViewManagers.Purchases.QuoteRequests
{
    public interface IQuoteRequestResponseDetailViewManager : IViewManager<QuoteRequestResponseDetail, QuoteRequestResponseDetailDTO>
    {
        Task<Result<IReadOnlyList<QuoteRequestResponseDetailDTO>>> FindBySpecification(ISpecification<QuoteRequestResponseDetail> specification);
    }
}
