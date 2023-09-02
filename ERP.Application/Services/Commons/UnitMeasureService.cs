using AutoMapper;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Commons;
using ERP.Domain.Entities.Commons;


namespace ERP.Application.Services.Commons
{
    public class UnitMeasureService : GenericService<UnitMeasure, UnitMeasureDTO>, IUnitMeasureService
    {
        public UnitMeasureService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
    }
}
