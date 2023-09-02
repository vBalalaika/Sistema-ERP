using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Managers.ProductMod
{
    class SupplyVoltageViewManager : ViewManager<SupplyVoltage, SupplyVoltageDTO>, ISupplyVoltageViewManager
    {
        private readonly ISupplyVoltageService _supplyvoltageservice;
        private readonly IMapper _mapper;

        public SupplyVoltageViewManager(ISupplyVoltageService supplyvoltageservice, IMapper mapper) :
           base(supplyvoltageservice, mapper)
        {
            _supplyvoltageservice = supplyvoltageservice;
            _mapper = mapper;
        }
    }
}
