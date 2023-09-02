using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.MissingProducts;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.QuoteRequests;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.MissingProducts;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.QuoteRequests
{
    public class QuoteRequestHeaderService : GenericService<QuoteRequestHeader, QuoteRequestHeaderDTO>, IQuoteRequestHeaderService
    {
        public QuoteRequestHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(QuoteRequestHeaderDTO entityToCreateDTO)
        {
            await MapDetails(entityToCreateDTO);
            await MapRelQuoteRequestProviders(entityToCreateDTO);

            // Debo cambiar el estado de los productos en faltantes => "A cotizar"
            UpdateMissingProductStates(entityToCreateDTO);

            return await base.CreateAsync(entityToCreateDTO);
        }

        public override async Task<int> UpdateAsync(QuoteRequestHeaderDTO entityDtoToUpdate)
        {
            var quoteRequestHeaderOld = await _unitOfWork.QuoteRequestHeaderRepository.FindAsync(new QuoteRequestHeaderWithAllSpecifications(entityDtoToUpdate.Id));

            await MapDetails(entityDtoToUpdate);
            await MapRelQuoteRequestProviders(entityDtoToUpdate, quoteRequestHeaderOld.First());
            return await base.UpdateAsync(entityDtoToUpdate);
        }

        public async Task MapDetails(QuoteRequestHeaderDTO headerDTO)
        {
            IReadOnlyList<QuoteRequestDetail> oldDetails = _unitOfWork.QuoteRequestDetailRepository.GetAll().Result.Where(x => x.QuoteRequestHeaderId == headerDTO.Id).ToList();

            // Create
            foreach (var detail in headerDTO.QuoteRequestDetails.Where(x => x.Id == 0))
            {
                await _unitOfWork.QuoteRequestDetailRepository.CreateAsync(detail);
            }

            // Update
            foreach (var detail in headerDTO.QuoteRequestDetails.Where(x => x.Id != 0 && oldDetails.Any(od => od.Id == x.Id)))
            {
                _unitOfWork.QuoteRequestDetailRepository.Update(detail);
            }

            // Delete
            foreach (var detail in oldDetails.Where(od => !headerDTO.QuoteRequestDetails.Any(x => x.Id != 0 && x.Id == od.Id)))
            {
                await _unitOfWork.QuoteRequestDetailRepository.DeleteAsync(detail.Id);
            }
        }

        public async Task MapRelQuoteRequestProviders(QuoteRequestHeaderDTO entityDtoToUpdate, QuoteRequestHeader quoteRequestHeaderOld = null)
        {
            IList<RelQuoteRequestProvider> relQuoteRequestProvidersOld = quoteRequestHeaderOld == null ? new List<RelQuoteRequestProvider>() : quoteRequestHeaderOld.RelQuoteRequestProviders.ToList();

            // Create
            foreach (var item in entityDtoToUpdate.RelQuoteRequestProviders.Where(x => x.Id == 0))
            {
                await _unitOfWork.RelQuoteRequestProviderRepository.CreateAsync(item);
            }

            // Update
            foreach (var item in entityDtoToUpdate.RelQuoteRequestProviders.Where(x => x.Id != 0 && relQuoteRequestProvidersOld.Any(y => y.Id == x.Id)))
            {
                _unitOfWork.RelQuoteRequestProviderRepository.Update(item);
            }

            // Delete
            foreach (var item in relQuoteRequestProvidersOld.Where(x => !entityDtoToUpdate.RelQuoteRequestProviders.Any(y => y.Id != 0 && y.Id == x.Id)))
            {
                await _unitOfWork.RelQuoteRequestProviderRepository.DeleteAsync(item.Id);
            }
        }

        public void UpdateMissingProductStates(QuoteRequestHeaderDTO qrHeaderDTO)
        {
            try
            {
                foreach (var qrd in qrHeaderDTO.QuoteRequestDetails)
                {
                    if (qrd.MissingProductId.HasValue)
                    {
                        var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(qrd.MissingProductId.Value));
                        if (getMPById.Result.Count() > 0)
                        {
                            var mpDTO = _mapper.Map<MissingProductDTO>(getMPById.Result.First());
                            mpDTO.StateMissingProductId = 2;
                            var mpEntity = _mapper.Map<MissingProduct>(mpDTO);
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
