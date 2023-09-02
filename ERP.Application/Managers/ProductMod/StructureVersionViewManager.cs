using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;


namespace ERP.Application.Managers.ProductMod
{
    public class StructureVersionViewManager : ViewManager<StructureVersion, StructureVersionDTO>, IStructureVersionViewManager
    {
        private readonly IStructureVersionService _structureversionservice;
        private readonly IMapper _mapper;
        public StructureVersionViewManager(IStructureVersionService structureversionservice, IMapper mapper) : base(structureversionservice, mapper)
        {
            _structureversionservice = structureversionservice;
            _mapper = mapper;
        }
    }
}
