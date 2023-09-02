using ERP.Domain.Entities.Purchases.QuoteRequests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Repositories.Purchases.QuoteRequests
{
    public interface IQuoteRequestResponseDetailRepository : IGenericRepository<QuoteRequestResponseDetail>
    {
        Task<IReadOnlyList<QuoteRequestResponseDetail>> GetAll();
    }
}
