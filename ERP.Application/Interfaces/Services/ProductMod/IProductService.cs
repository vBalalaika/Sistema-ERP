using ERP.Application.DTOs.Entities.ProductMod;
using ERP.Domain.Entities.ProductMod;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Interfaces.Services.ProductMod
{
    public interface IProductService : IGenericService<Product, ProductDTO>
    {
        Task<int> UpdateImageProduct(int productId, byte[] image);
        Task<IReadOnlyList<Product>> GetAccessoryProducts(int id);
        Task<int> DeactivateStoredStock(int productId);
        Task<bool> UpdateExistences(List<ProductDTO> productDTOs);
    }
}
