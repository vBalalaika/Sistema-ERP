using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Services.ProductMod
{
    public class SupplyVoltageService : GenericService<SupplyVoltage, SupplyVoltageDTO>, ISupplyVoltageService
    {
        public SupplyVoltageService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
