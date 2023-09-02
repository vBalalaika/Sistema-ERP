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
    public class QuoteRequestResponseDetailService : GenericService<QuoteRequestResponseDetail, QuoteRequestResponseDetailDTO>, IQuoteRequestResponseDetailService
    {
        public QuoteRequestResponseDetailService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> DeleteAsync(int id)
        {
            // Antes de borrar la respuesta de cotizacion, debo volver los estados de los faltantes al estado anterior => "A cotizar". 
            UpdateMissingProductStatesOnDelete(id);

            return await base.DeleteAsync(id);
        }

        public void UpdateMissingProductStatesOnDelete(int qrrdId)
        {
            try
            {
                var getQRRDById = _unitOfWork.QuoteRequestResponseDetailRepository.FindAsync(new QuoteRequestResponseDetailWithAllSpecifications(qrrdId));
                if (getQRRDById.Result.Count() > 0)
                {
                    var qRRDEntity = getQRRDById.Result.First();
                    if (qRRDEntity.MissingProductId.HasValue)
                    {
                        var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(qRRDEntity.MissingProductId.Value));
                        if (getMPById.Result.Count() > 0)
                        {
                            var mpEntity = getMPById.Result.First();
                            if (mpEntity.StateMissingProductId <= 3)
                            {
                                var mpDTO = _mapper.Map<MissingProductDTO>(mpEntity);
                                mpDTO.ProviderId = null;
                                mpDTO.StateMissingProductId = 2;
                                var mp = _mapper.Map<MissingProduct>(mpDTO);
                                _unitOfWork.MissingProductRepository.Update(mp);
                            }
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
