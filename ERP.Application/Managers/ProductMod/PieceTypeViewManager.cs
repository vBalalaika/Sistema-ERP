using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;

namespace ERP.Application.Managers.ProductMod
{
    public class PieceTypeViewManager : ViewManager<PieceType, PieceTypeDTO>, IPieceTypeViewManager
    {
        private readonly IPieceTypeService _piecetypeservice;
        private readonly IMapper _mapper;

        public PieceTypeViewManager(IPieceTypeService piecetypeservice, IMapper mapper) :
           base(piecetypeservice, mapper)
        {
            _piecetypeservice = piecetypeservice;
            _mapper = mapper;
        }
    }
}