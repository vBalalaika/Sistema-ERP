using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Managers.ProductMod
{
    public class CutTypeViewManager : ViewManager<CutType, CutTypeDTO>, ICutTypeViewManager
    {
        private readonly ICutTypeService _cuttypeservice;
        private readonly IMapper _mapper;

        public CutTypeViewManager(ICutTypeService cuttypeservice, IMapper mapper) :
           base(cuttypeservice, mapper)
        {
            _cuttypeservice = cuttypeservice;
            _mapper = mapper;
        }
    }
}