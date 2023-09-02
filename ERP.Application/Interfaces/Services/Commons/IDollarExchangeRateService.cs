using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.Commons;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.Commons
{
    public interface IDollarExchangeRateService : IGenericService<DollarExchangeRate, DollarExchangeRateDTO>
    {
        Task HFGetAndSaveDollarPrice();
    }
}
