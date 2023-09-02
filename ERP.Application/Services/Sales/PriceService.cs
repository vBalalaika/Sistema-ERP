using AutoMapper;
using ERP.Application.DTOs.Entities.Sales;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Sales;
using ERP.Domain.Entities.Sales;

namespace ERP.Application.Services.Sales
{
    public class PriceService : GenericService<Price, PriceDTO>, IPriceService
    {
        public PriceService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
