using AutoMapper;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Exceptions;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Application.Specification.ProductMod;

namespace ERP.Application.Services.ProductMod
{
    public class ProductService : GenericService<Product, ProductDTO>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public async Task<IReadOnlyList<Product>> GetAccessoryProducts(int id)
        {
            return await _unitOfWork.ProductRepository.GetAccessoryProducts(id);
        }

        public async Task<int> UpdateImageProduct(int productId, byte[] image)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
            if (product is null)
            {
                throw new ApiException("EL producto no existe");
            }
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Commit();
            return productId;
        }

        public override async Task<int> CreateAsync(ProductDTO entityToCreateDTO)
        {

            await MapRelProductStation(entityToCreateDTO);
            await MapManyToManySimple(entityToCreateDTO);
            return await base.CreateAsync(entityToCreateDTO);
        }
        public override async Task<int> UpdateAsync(ProductDTO entityDtoToUpdate)
        {
            var productOld = await _unitOfWork.ProductRepository.FindAsync(new ProductWithAllSpecification(entityDtoToUpdate.Id));

            await MapRelProductStation(entityDtoToUpdate, productOld.FirstOrDefault()); //No devuelve nada porque le cambia los estados a los objetos        
            await MapManyToManySimple(entityDtoToUpdate, productOld.FirstOrDefault());
            _unitOfWork.ProductFeatureRepository.Update(entityDtoToUpdate.ProductFeature);
            return await base.UpdateAsync(entityDtoToUpdate);
        }

        private async Task MapRelProductStation(ProductDTO entityProductDTO, Product entityProductOld = null)
        {

            IList<RelProductStation> oldRelProductStation = new List<RelProductStation>();
            IList<Archive> oldArchives = new List<Archive>();
            if (entityProductOld != null)
            {
                oldRelProductStation = entityProductOld.RelProductStations.ToList();
                oldArchives = entityProductOld.Archives.ToList();
            }

            //RELPRODUCTSTATION
            //Update
            foreach (var item in entityProductDTO.RelProductStations.Where(x => x.Id != 0 && oldRelProductStation.Any(y => y.Id == x.Id)))
            {
                _unitOfWork.RelProductStationRepository.Update(item);
            }

            //Add
            foreach (var item in entityProductDTO.RelProductStations.Where(x => x.Id == 0))
            {
                item.Station = null;//Es Para que no quiera dar de alta la Station que viene cargada.
                await _unitOfWork.RelProductStationRepository.CreateAsync(item);
            }
            //Delete
            foreach (var item in oldRelProductStation.Where(x => !entityProductDTO.RelProductStations.Any(y => y.Id != 0 && y.Id == x.Id)))//ver si funciona
            {
                await _unitOfWork.RelProductStationRepository.DeleteAsync(item.Id);
            }

            //ARCHIVES
            //Update
            foreach (var item in entityProductDTO.Archives.Where(x => x.Id != 0 && oldArchives.Any(y => y.Id == x.Id)))
            {
                _unitOfWork.ArchiveRepository.Update(item);
            }

            //Add
            foreach (var item in entityProductDTO.Archives.Where(x => x.Id == 0))
            {
                //item.Station = null;//Es Para que no quiera dar de alta la Station que viene cargada.
                await _unitOfWork.ArchiveRepository.CreateAsync(item);
            }
            //Delete
            foreach (var item in oldArchives.Where(x => !entityProductDTO.Archives.Any(y => y.Id != 0 && y.Id == x.Id)))//ver si funciona
            {
                await _unitOfWork.ArchiveRepository.DeleteAsync(item.Id);
            }




        }

        private async Task MapManyToManySimple(ProductDTO entityProductDto, Product entityProductOld = null)
        {
            //Comentario general: PieceFormat,PieceType,CutType,SupplyVoltage...Estos son checkbox en el front entonces no traemos el Id de la Tabla intermedia.Comparamos por el Id FK

            //Add PieceType
            foreach (var item in entityProductDto.ProductPieceTypes.Where(x => x.Id == 0))
            {
                if (entityProductOld == null || !entityProductOld.ProductPieceTypes.Any(x => x.PieceTypeId == item.PieceTypeId))
                {
                    item.PieceType = null;//Es Para que no quiera dar de alta la Piece que viene cargada.
                    await _unitOfWork.ProductPieceTypeRepository.CreateAsync(item);
                }
            }
            if (entityProductOld != null)
            {
                //Delete PieceType               
                foreach (var item in entityProductOld.ProductPieceTypes.Where(x => !entityProductDto.ProductPieceTypes.Any(y => y.PieceTypeId == x.PieceTypeId)))
                {
                    await _unitOfWork.ProductPieceTypeRepository.DeleteAsync(item.Id);
                }

            }

            //Add PieceFormat
            foreach (var item in entityProductDto.ProductPieceFormats.Where(x => x.Id == 0))
            {
                if (entityProductOld == null || !entityProductOld.ProductPieceFormats.Any(x => x.PieceFormatId == item.PieceFormatId))
                {
                    item.PieceFormat = null;//Es Para que no quiera dar de alta la Piece que viene cargada.
                    await _unitOfWork.ProductPieceFormatRepository.CreateAsync(item);
                }
            }
            if (entityProductOld != null)
            {
                //Delete PieceFormat                
                foreach (var item in entityProductOld.ProductPieceFormats.Where(x => !entityProductDto.ProductPieceFormats.Any(y => y.PieceFormatId == x.PieceFormatId)))
                {
                    await _unitOfWork.ProductPieceFormatRepository.DeleteAsync(item.Id);
                }

            }

            //Add CutType
            foreach (var item in entityProductDto.ProductCutTypes.Where(x => x.Id == 0))
            {
                if (entityProductOld == null || !entityProductOld.ProductCutTypes.Any(x => x.CutTypeId == item.CutTypeId))
                {
                    item.CutType = null;//Es Para que no quiera dar de alta la Piece que viene cargada.
                    await _unitOfWork.ProductCutTypeRepository.CreateAsync(item);
                }
            }
            if (entityProductOld != null)
            {
                //Delete CutType                
                foreach (var item in entityProductOld.ProductCutTypes.Where(x => !entityProductDto.ProductCutTypes.Any(y => y.CutTypeId == x.CutTypeId)))
                {
                    await _unitOfWork.ProductCutTypeRepository.DeleteAsync(item.Id);
                }

            }

            //Add SupplyVoltage
            foreach (var item in entityProductDto.ProductSupplyVoltages.Where(x => x.Id == 0))
            {
                if (entityProductOld == null || !entityProductOld.ProductSupplyVoltages.Any(x => x.SupplyVoltageId == item.SupplyVoltageId))
                {
                    item.SupplyVoltage = null;//Es Para que no quiera dar de alta la Piece que viene cargada.
                    await _unitOfWork.ProductSupplyVoltageRepository.CreateAsync(item);
                }
            }
            if (entityProductOld != null)
            {
                //Delete SupplyVoltage               
                foreach (var item in entityProductOld.ProductSupplyVoltages.Where(x => !entityProductDto.ProductSupplyVoltages.Any(y => y.SupplyVoltageId == x.SupplyVoltageId)))
                {
                    await _unitOfWork.ProductSupplyVoltageRepository.DeleteAsync(item.Id);
                }

            }

            //Add AccesoryProduct
            foreach (var item in entityProductDto.AccessoryProducts.Where(x => x.Id == 0))
            {
                item.ProductAccessory = null;//Es Para que no quiera dar de alta la Piece que viene cargada.
                await _unitOfWork.AccesoryProductRepository.CreateAsync(item);
            }
            if (entityProductOld != null)
            {
                //Delete AccesoryProduct                
                foreach (var item in entityProductOld.AccessoryProducts.Where(x => !entityProductDto.AccessoryProducts.Any(y => y.Id != 0 && y.Id == x.Id)))
                {
                    await _unitOfWork.AccesoryProductRepository.DeleteAsync(item.Id);
                }

            }




        }

        public async Task<int> DeactivateStoredStock(int productId)
        {
            var productToDeactivateStoredStock = await _unitOfWork.ProductRepository.FindAsync(new ProductWithAllSpecification(productId));
            var productDTO = _mapper.Map<ProductDTO>(productToDeactivateStoredStock.FirstOrDefault());
            productDTO.ProductFeature.StoredStock = false;
            _unitOfWork.ProductFeatureRepository.Update(productDTO.ProductFeature);
            return await base.UpdateAsync(productDTO);
        }

        public async Task<bool> UpdateExistences(List<ProductDTO> productDTOs)
        {
            try
            {
                foreach (var product in productDTOs)
                {
                    _unitOfWork.ProductRepository.Update(product);
                    await _unitOfWork.Commit();
                }
                return true;
            }
            catch
            {
                return false;
            }

        }

    }

}

