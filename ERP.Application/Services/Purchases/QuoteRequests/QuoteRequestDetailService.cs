using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.QuoteRequests;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.QuoteRequests
{
    public class QuoteRequestDetailService : GenericService<QuoteRequestDetail, QuoteRequestDetailDTO>, IQuoteRequestDetailService
    {
        public QuoteRequestDetailService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> DeleteAsync(int id)
        {
            // Antes de borrar la solicitud de cotizacion, debo volver los estados de los faltantes al estado anterior => "Pendiente". 
            UpdateMissingProductStatesOnDelete(id);

            return await base.DeleteAsync(id);
        }

        public void UpdateMissingProductStatesOnDelete(int qrdId)
        {
            try
            {
                var getQRDById = _unitOfWork.QuoteRequestDetailRepository.FindAsync(new QuoteRequestDetailWithAllSpecifications(qrdId));
                if (getQRDById.Result.Count() > 0)
                {
                    var qrdEntity = getQRDById.Result.First();
                    if (qrdEntity.MissingProductId.HasValue)
                    {
                        var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(qrdEntity.MissingProductId.Value));
                        if (getMPById.Result.Count() > 0)
                        {
                            var mpEntity = getMPById.Result.First();
                            var mpDto = _mapper.Map<MissingProductDTO>(mpEntity);
                            mpDto.ProviderId = null;
                            mpDto.StateMissingProductId = 1;
                            mpEntity = _mapper.Map<MissingProduct>(mpDto);
                            _unitOfWork.MissingProductRepository.Update(mpEntity);
                        }
                    }
                    else
                    {
                        var getMPByProductId = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByProductIdSpecification(qrdEntity.ProductId));
                        if (getMPByProductId.Result.Count() > 0)
                        {
                            var mpEntity = getMPByProductId.Result.First();
                            var mpDto = _mapper.Map<MissingProductDTO>(mpEntity);
                            mpDto.ProviderId = null;
                            mpDto.StateMissingProductId = 1;
                            mpEntity = _mapper.Map<MissingProduct>(mpDto);
                            _unitOfWork.MissingProductRepository.Update(mpEntity);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
