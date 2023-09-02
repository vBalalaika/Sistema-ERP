using ERP.Application.Interfaces.Repositories.ProductMod;
using ERP.Domain.Entities.ProductMod;
using ERP.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Infrastructure.Repositories.ProductMod
{
    public class ProductRepositoryAsync : GenericRepository<Product>, IProductRepository
    {
        public ProductRepositoryAsync(ApplicationDbContext context) : base(context)
        {

        }

        //TODO Definir si muestra todos los productos o solo los filtrados por alguna categoría!
        public async Task<IReadOnlyList<Product>> GetAccessoryProducts(int id)
        {
            return await _dbContext.Set<Product>().Where(x => x.SubCategory.CategoryId == 1 && x.Id != id).ToListAsync();
        }



    }
}
