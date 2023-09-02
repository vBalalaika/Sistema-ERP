using AspNetCoreHero.Results;
using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Application.DTOs.Entities.Commons;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Application.Specification;

namespace ERP.Application.Interfaces.ViewManagers.ProductMod
{
    public interface IProductViewManager : IViewManager<Product, ProductDTO>
    {

        Task<Result<IReadOnlyList<UnitMeasureDTO>>> GetUnitsMeasure();
        Task<Result<IReadOnlyList<SubCategoryDTO>>> GetSubCategories();
        Task<Result<IReadOnlyList<CategoryDTO>>> GetCategories(int? idExcludeCategory = null);
        Task<Result<IReadOnlyList<IVAConditionDTO>>> GetIvaTypes();
        Task<Result<IReadOnlyList<PieceTypeDTO>>> GetPieceTypes();
        Task<Result<IReadOnlyList<PieceFormatDTO>>> GetPieceFormats();
        Task<Result<IReadOnlyList<CutTypeDTO>>> GetCutTypes();
        Task<Result<IReadOnlyList<OperationDTO>>> GetOperations();
        Task<Result<IReadOnlyList<SupplyVoltageDTO>>> GetSupplyVoltages();
        Task<Result<IReadOnlyList<ProductDTO>>> GetAccessoryProducts(int id);
        Task<Result<IReadOnlyList<StationDTO>>> GetStations();
        Task<Result<IReadOnlyList<ArchiveTypeDTO>>> GetArchiveTypes();

        Task<Result<SubCategoryDTO>> GetSubCategoryById(int subCategoryId);
        Task<Result<int>> UpdateImageProduct(int productId, byte[] image);
        Task<Result<IReadOnlyList<ProductDTO>>> FindBySpecification(ISpecification<Product> specification);

        Task<Result<int>> DeactivateStoredStock(int productId);

        Task<Result<bool>> UpdateExistences(List<ProductDTO> productDTOs);

    }
}
