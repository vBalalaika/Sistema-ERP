using AutoMapper;
using ERP.Application.DTOs.Entities.Logistics.Incomes;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Logistics.Incomes;
using ERP.Domain.Entities.Logistics.Incomes;

namespace ERP.Application.Services.Logistics.Incomes
{
    public class IncomeStateService : GenericService<IncomeState, IncomeStateDTO>, IIncomeStateService
    {
        public IncomeStateService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
