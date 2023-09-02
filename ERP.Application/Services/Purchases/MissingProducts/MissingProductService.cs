using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.MissingProducts;

namespace ERP.Application.Services.Purchases.MissingProducts
{
    public class MissingProductService : GenericService<MissingProduct, MissingProductDTO>, IMissingProductService
    {
        public MissingProductService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }
    }
}
