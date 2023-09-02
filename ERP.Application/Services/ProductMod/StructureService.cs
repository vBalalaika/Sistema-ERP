using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Specification.ProductMod.StructureItemSpecifications;
using ERP.Application.Specification.ProductMod.StructureSpecifications;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.ProductMod
{
    public class StructureService : GenericService<Structure, StructureDTO>, IStructureService
    {
        public StructureService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        public override async Task<int> UpdateAsync(StructureDTO entityDtoToUpdate)
        {
            var structureOld = await _unitOfWork.StructureRepository.FindAsync(new StructureWithItemsSpecification(entityDtoToUpdate.Id));
            // Versionado
            foreach (var item in entityDtoToUpdate.StructureItems) // Asignamos la version recien creada a los items
            {
                item.VersionId = entityDtoToUpdate.LastVersionId;
            }

            await MapStructureItems(entityDtoToUpdate, structureOld.FirstOrDefault()); //No devuelve nada porque le cambia los estados a los objetos        
            return await base.UpdateAsync(entityDtoToUpdate);
        }
        public override async Task<int> CreateAsync(StructureDTO entityDtoToCreate)
        {
            // Versionado
            foreach (var item in entityDtoToCreate.StructureItems) // Asignamos la version recien creada a los items
            {
                item.VersionId = entityDtoToCreate.LastVersionId;
            }
            return await base.CreateAsync(entityDtoToCreate);
        }
        private async Task MapStructureItems(StructureDTO entityStructureDTO, Structure entityStructureOld = null)
        {
            //Add
            foreach (var item in entityStructureDTO.StructureItems)
            {
                item.SonProduct = null; //Es Para que no quiera dar de alta el producto y la estructura que vienen cargados.
                item.SonStructure = null;
                item.Id = 0;
                await _unitOfWork.StructureItemRepository.CreateAsync(item);
            }

        }
        public override async Task<int> DeleteAsync(int id)
        {
            var response = await base.FindWithSpecificationPattern(new StructureDeleteValidatorSpecification(id));
            IReadOnlyList<StructureDTO> structureDto = _mapper.Map<IReadOnlyList<StructureDTO>>(response);
            if (structureDto.Count == 0)
            {
                var responseItems = _unitOfWork.StructureItemRepository.FindAsync(new StructureItemsByStructureIdSpecification(id));
                IReadOnlyList<StructureItemDTO> structureItemsDto = _mapper.Map<IReadOnlyList<StructureItemDTO>>(responseItems.Result);
                // Eliminamos todos los items de la estructura primero 
                foreach (var item in structureItemsDto)
                {
                    await _unitOfWork.StructureItemRepository.DeleteAsync(item.Id);
                }
                return await base.DeleteAsync(id);
            }



            throw new ElementNotFoundException("No se puede borrar la estructura, está en uso");
        }
    }
}
