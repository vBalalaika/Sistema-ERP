using AspNetCoreHero.Results;
using AutoMapper;
using ERP.Application.Specification;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;

namespace ERP.Application.Managers.ProductMod
{
    public class ArchiveViewManager : ViewManager<Archive, ArchiveDTO>, IArchiveViewManager
    {
        private readonly IArchiveService archiveservice;
        private readonly IMapper _mapper;

        public ArchiveViewManager(IArchiveService archiveservice, IMapper mapper) : base(archiveservice, mapper)
        {
            this.archiveservice = archiveservice;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<ArchiveDTO>>> FindBySpecification(ISpecification<Archive> specification)
        {
            var Archives = await archiveservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<ArchiveDTO> ArchiveDTOs = _mapper.Map<IReadOnlyList<ArchiveDTO>>(Archives);
            return Result<IReadOnlyList<ArchiveDTO>>.Success(ArchiveDTOs);
        }
    }
}