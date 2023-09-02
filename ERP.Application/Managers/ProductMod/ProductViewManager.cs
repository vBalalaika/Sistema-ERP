using AspNetCoreHero.Results;
using AutoMapper;

using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.Interfaces.Services.ProductMod;
using ERP.Application.Interfaces.ViewManagers.ProductMod;
using ERP.Application.Interfaces.Services.Commons;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using ERP.Application.Specification;
using ERP.Application.Specification.ProductMod.CategorySpecifications;

namespace ERP.Application.Managers.ProductMod
{
    public class ProductViewManager : ViewManager<Product, ProductDTO>, IProductViewManager
    {
        private readonly IProductService _productservice;
        private readonly IUnitMeasureService _unitemeasureservice;
        private readonly ICategoryService _categoryservice;
        private readonly ISubCategoryService _subcategoryservice;
        private readonly IIVAConditionService _ivaconditionservice;
        private readonly IPieceTypeService _piecetypeservice;
        private readonly IPieceFormatService _pieceformatservice;
        private readonly ICutTypeService _cuttypeservice;
        private readonly IOperationService _operationservice;
        private readonly ISupplyVoltageService _supplyvoltageservice;
        private readonly IStationService _stationservice;
        private readonly IArchiveTypeService _archivetypeservice;
        private readonly IMapper _mapper;

        public ProductViewManager(IProductService productservice, IMapper mapper, IUnitMeasureService unitmeasureservice, ISubCategoryService subcategory, ICategoryService categoryservice
                                    , IIVAConditionService ivatypeservice, IPieceTypeService piecetypeservice, IPieceFormatService pieceformatservice
                                    , ICutTypeService cuttypeservice, IOperationService operationservice, ISupplyVoltageService supplyvoltageservice
                                    , IStationService stationservice, IArchiveTypeService archivetypeservice) :
            base(productservice, mapper)
        {
            _productservice = productservice;
            _unitemeasureservice = unitmeasureservice;
            _categoryservice = categoryservice;
            _subcategoryservice = subcategory;
            _ivaconditionservice = ivatypeservice;
            _piecetypeservice = piecetypeservice;
            _pieceformatservice = pieceformatservice;
            _cuttypeservice = cuttypeservice;
            _operationservice = operationservice;
            _supplyvoltageservice = supplyvoltageservice;
            _stationservice = stationservice;
            _archivetypeservice = archivetypeservice;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<ProductDTO>>> FindBySpecification(ISpecification<Product> specification)
        {
            var products = await _productservice.FindWithSpecificationPattern(specification);
            IReadOnlyList<ProductDTO> prodDto = _mapper.Map<IReadOnlyList<ProductDTO>>(products);
            return Result<IReadOnlyList<ProductDTO>>.Success(prodDto);
        }
        public async Task<Result<IReadOnlyList<UnitMeasureDTO>>> GetUnitsMeasure()
        {
            var unitsmeasure = await _unitemeasureservice.GetListAsync();
            var unitsmeasureDTO = _mapper.Map<IReadOnlyList<UnitMeasureDTO>>(unitsmeasure);
            return Result<IReadOnlyList<UnitMeasureDTO>>.Success(unitsmeasureDTO);
        }
        public async Task<Result<IReadOnlyList<SubCategoryDTO>>> GetSubCategories()
        {
            var subcategories = await _subcategoryservice.GetListAsync();
            var subcategoryDTO = _mapper.Map<IReadOnlyList<SubCategoryDTO>>(subcategories);
            return Result<IReadOnlyList<SubCategoryDTO>>.Success(subcategoryDTO);
        }
        public async Task<Result<IReadOnlyList<CategoryDTO>>> GetCategories(int? idExcludeCategory = null)
        {

            IReadOnlyList<CategoryDTO> categoryDTO;
            if (idExcludeCategory == null)
            {
                var categories = await _categoryservice.GetListAsync();
                categoryDTO = _mapper.Map<IReadOnlyList<CategoryDTO>>(categories);
            }
            else
            {
                var categories = await _categoryservice.FindWithSpecificationPattern(new CategoryExcludeIdSpecification(idExcludeCategory.Value));
                categoryDTO = _mapper.Map<IReadOnlyList<CategoryDTO>>(categories);
            }

            return Result<IReadOnlyList<CategoryDTO>>.Success(categoryDTO);
        }
        public async Task<Result<IReadOnlyList<IVAConditionDTO>>> GetIvaTypes()
        {
            var ivatypes = await _ivaconditionservice.GetListAsync();
            var ivatypeDTO = _mapper.Map<IReadOnlyList<IVAConditionDTO>>(ivatypes);
            return Result<IReadOnlyList<IVAConditionDTO>>.Success(ivatypeDTO);
        }
        public async Task<Result<IReadOnlyList<PieceTypeDTO>>> GetPieceTypes()
        {
            var piecetypes = await _piecetypeservice.GetListAsync();
            var piecetypesDTO = _mapper.Map<IReadOnlyList<PieceTypeDTO>>(piecetypes);
            return Result<IReadOnlyList<PieceTypeDTO>>.Success(piecetypesDTO);
        }
        public async Task<Result<IReadOnlyList<PieceFormatDTO>>> GetPieceFormats()
        {
            var pieceformats = await _pieceformatservice.GetListAsync();
            var pieceformatsDTO = _mapper.Map<IReadOnlyList<PieceFormatDTO>>(pieceformats);
            return Result<IReadOnlyList<PieceFormatDTO>>.Success(pieceformatsDTO);
        }
        public async Task<Result<IReadOnlyList<CutTypeDTO>>> GetCutTypes()
        {
            var cuttypes = await _cuttypeservice.GetListAsync();
            var cuttypesDTO = _mapper.Map<IReadOnlyList<CutTypeDTO>>(cuttypes);
            return Result<IReadOnlyList<CutTypeDTO>>.Success(cuttypesDTO);
        }
        public async Task<Result<IReadOnlyList<OperationDTO>>> GetOperations()
        {
            var operations = await _operationservice.GetListAsync();
            var operationsDTO = _mapper.Map<IReadOnlyList<OperationDTO>>(operations);
            return Result<IReadOnlyList<OperationDTO>>.Success(operationsDTO);
        }
        public async Task<Result<IReadOnlyList<SupplyVoltageDTO>>> GetSupplyVoltages()
        {
            var supplyvoltages = await _supplyvoltageservice.GetListAsync();
            var supplyvoltageDTO = _mapper.Map<IReadOnlyList<SupplyVoltageDTO>>(supplyvoltages);
            return Result<IReadOnlyList<SupplyVoltageDTO>>.Success(supplyvoltageDTO);
        }
        public async Task<Result<IReadOnlyList<ProductDTO>>> GetAccessoryProducts(int id)
        {
            var products = await _productservice.GetAccessoryProducts(id);
            var productsDTO = _mapper.Map<IReadOnlyList<ProductDTO>>(products);
            return Result<IReadOnlyList<ProductDTO>>.Success(productsDTO);
        }
        public async Task<Result<IReadOnlyList<StationDTO>>> GetStations()
        {
            var stations = await _stationservice.GetListAsync();
            var stationsDTO = _mapper.Map<IReadOnlyList<StationDTO>>(stations);
            return Result<IReadOnlyList<StationDTO>>.Success(stationsDTO);
        }

        public async Task<Result<IReadOnlyList<ArchiveTypeDTO>>> GetArchiveTypes()
        {
            var archives = await _archivetypeservice.GetListAsync();
            var archivesDTO = _mapper.Map<IReadOnlyList<ArchiveTypeDTO>>(archives);
            return Result<IReadOnlyList<ArchiveTypeDTO>>.Success(archivesDTO);
        }

        public async Task<Result<SubCategoryDTO>> GetSubCategoryById(int subCategoryId)
        {
            var subCategory = await _subcategoryservice.GetByIdAsync(subCategoryId);
            var subCategoryDTO = _mapper.Map<SubCategoryDTO>(subCategory);
            return Result<SubCategoryDTO>.Success(subCategoryDTO);
        }


        public async Task<Result<int>> UpdateImageProduct(int productId, byte[] image)
        {
            var idUpdated = await _productservice.UpdateImageProduct(productId, image);
            return Result<int>.Success(idUpdated);
        }

        public async Task<Result<int>> DeactivateStoredStock(int productId)
        {
            var deactivateStoredStock = await _productservice.DeactivateStoredStock(productId);
            return Result<int>.Success(deactivateStoredStock);
        }

        public async Task<Result<bool>> UpdateExistences(List<ProductDTO> productDTOs)
        {
            var hasSucceeded = await _productservice.UpdateExistences(productDTOs);
            return Result<bool>.Success(hasSucceeded);
        }

    }
}
