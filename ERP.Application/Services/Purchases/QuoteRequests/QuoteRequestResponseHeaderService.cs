using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.DTOs.Entities.Purchases.QuoteRequests;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.QuoteRequests;
using ERP.Application.Specification.Purchases.MissingProducts;
using ERP.Application.Specification.Purchases.ProviderProduct;
using ERP.Application.Specification.Purchases.QuoteRequests;
using ERP.Domain.Entities.Purchases.Providers;
using ERP.Domain.Entities.Purchases.QuoteRequests;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.QuoteRequests
{
    public class QuoteRequestResponseHeaderService : GenericService<QuoteRequestResponseHeader, QuoteRequestResponseHeaderDTO>, IQuoteRequestResponseHeaderService
    {
        public QuoteRequestResponseHeaderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(QuoteRequestResponseHeaderDTO entityToCreateDTO)
        {
            await MapDetails(entityToCreateDTO);

            // Luego de que se crea la respuesta, tengo que setearle al QuoteRequestHeader su ResponseDate y estado "Cotizado" al detalle si esta disponible.
            UpdateResponseDateAndPurchaseState(entityToCreateDTO);

            // Luego de que se crea la respuesta, debo cambiar el estado de los productos en faltantes => "Cotizado".
            UpdateMissingProductStates(entityToCreateDTO);

            // Luego de que se crea la respuesta, tengo que actualizar el DollarPrice o PesosPrce con el valor del UnitPrice del detalle por cada producto.
            UpdatePrices(entityToCreateDTO);

            // Luego de que se crea la respuesta, tengo que ver si hay algun detalle con codigo de reemplazo, en ese caso genero una respuesta de cotizacion nueva con esos detalles y el mismo encabezado.
            await createNewQRRByAlternativeProduct(entityToCreateDTO);

            return await base.CreateAsync(entityToCreateDTO);
        }

        public override async Task<int> UpdateAsync(QuoteRequestResponseHeaderDTO entityDtoToUpdate)
        {
            await MapDetails(entityDtoToUpdate);

            return await base.UpdateAsync(entityDtoToUpdate);
        }

        public override async Task<int> DeleteAsync(int id)
        {
            // Antes de borrar la respuesta de cotizacion, tengo que setear null su ResponseDate y estado "A cotizar" para la solicitud original.
            UpdateResponseDateAndPurchaseStateOnDelete(id);

            return await base.DeleteAsync(id);
        }

        public async Task MapDetails(QuoteRequestResponseHeaderDTO headerDTO)
        {
            IReadOnlyList<QuoteRequestResponseDetail> oldDetails = _unitOfWork.QuoteRequestResponseDetailRepository.GetAll().Result.Where(x => x.QuoteRequestResponseHeaderId == headerDTO.Id).ToList();

            // Create
            foreach (var detail in headerDTO.QuoteRequestResponseDetails.Where(x => x.Id == 0))
            {
                await _unitOfWork.QuoteRequestResponseDetailRepository.CreateAsync(detail);
            }

            // Update
            foreach (var detail in headerDTO.QuoteRequestResponseDetails.Where(x => x.Id != 0 && oldDetails.Any(od => od.Id == x.Id)))
            {
                _unitOfWork.QuoteRequestResponseDetailRepository.Update(detail);
            }

            // Delete
            foreach (var detail in oldDetails.Where(od => !headerDTO.QuoteRequestResponseDetails.Any(x => x.Id != 0 && x.Id == od.Id)))
            {
                await _unitOfWork.QuoteRequestResponseDetailRepository.DeleteAsync(detail.Id);
            }
        }

        public void UpdateResponseDateAndPurchaseState(QuoteRequestResponseHeaderDTO qrrHeaderDTO)
        {
            try
            {
                var getQRHById = _unitOfWork.QuoteRequestHeaderRepository.FindAsync(new QuoteRequestHeaderSpecification(qrrHeaderDTO.QuoteRequestHeaderId));
                if (getQRHById.Result.Count() > 0)
                {
                    var qrhDTO = _mapper.Map<QuoteRequestHeaderDTO>(getQRHById.Result.First());
                    qrhDTO.ResponseDate = qrrHeaderDTO.Date;
                    foreach (var qrd in qrhDTO.QuoteRequestDetails)
                    {
                        foreach (var qrrd in qrrHeaderDTO.QuoteRequestResponseDetails)
                        {
                            if (qrrd.Available && qrd.MissingProductId == qrrd.MissingProductId)
                            {
                                var qrdDTO = _mapper.Map<QuoteRequestDetailDTO>(qrd);
                                qrdDTO.PurchaseStateId = 3;
                                var qrdEntity = _mapper.Map<QuoteRequestDetail>(qrdDTO);
                                _unitOfWork.QuoteRequestDetailRepository.Update(qrdEntity);
                            }
                        }
                    }

                    var qrhEntity = _mapper.Map<QuoteRequestHeader>(qrhDTO);
                    _unitOfWork.QuoteRequestHeaderRepository.Update(qrhEntity);
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateMissingProductStates(QuoteRequestResponseHeaderDTO qrrHeaderDTO)
        {
            try
            {
                foreach (var qrrd in qrrHeaderDTO.QuoteRequestResponseDetails)
                {
                    if (qrrd.Available)
                    {
                        if (qrrd.MissingProductId.HasValue)
                        {
                            //var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(qrrd.MissingProductId.Value));
                            //if (getMPById.Result.Count() > 0)
                            //{
                            //    var mpDTO = _mapper.Map<MissingProductDTO>(getMPById.Result.First());
                            //    mpDTO.StateMissingProductId = 3;
                            //    var mp = _mapper.Map<MissingProduct>(mpDTO);
                            //    _unitOfWork.MissingProductRepository.Update(mp);
                            //}
                            var getMPById = _unitOfWork.MissingProductRepository.GetByIdAsync(qrrd.MissingProductId.Value);
                            getMPById.Result.StateMissingProductId = 3;
                            _unitOfWork.MissingProductRepository.Update(getMPById.Result);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePrices(QuoteRequestResponseHeaderDTO qrrHeaderDTO)
        {
            try
            {
                foreach (var qrrd in qrrHeaderDTO.QuoteRequestResponseDetails)
                {
                    if (qrrd.Available)
                    {
                        if (qrrd.MissingProductId.HasValue)
                        {
                            var getMPById = _unitOfWork.MissingProductRepository.FindAsync(new MissingProductByIdSpecification(qrrd.MissingProductId.Value));
                            if (getMPById.Result.Count() > 0)
                            {
                                var mpEntity = getMPById.Result.First();
                                var getRPPByProductAndProviderId = _unitOfWork.RelProviderProductRepository.FindAsync(new RelProviderProductByProductAndProviderIdSpecification(mpEntity.ProductId, qrrHeaderDTO.ProviderId));
                                if (getRPPByProductAndProviderId.Result.Count() > 0)
                                {
                                    var rppEntity = getRPPByProductAndProviderId.Result.First();
                                    var rppDTO = _mapper.Map<RelProviderProductDTO>(rppEntity);

                                    if (qrrd.MoneyType == 1)
                                    {
                                        rppDTO.PesosPrice = qrrd.UnitPrice;
                                    }
                                    else if (qrrd.MoneyType == 2)
                                    {
                                        rppDTO.DollarPrice = qrrd.UnitPrice;
                                    }

                                    rppEntity = _mapper.Map<RelProviderProduct>(rppDTO);
                                    _unitOfWork.RelProviderProductRepository.Update(rppEntity);
                                }
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

        public async Task createNewQRRByAlternativeProduct(QuoteRequestResponseHeaderDTO qrrHeaderDTO)
        {
            try
            {
                var newQRRHEntity = new QuoteRequestResponseHeader();
                if (qrrHeaderDTO.QuoteRequestResponseDetails.Any(x => x.AlternativeProductId != null))
                {
                    var qrrHeader = MapToEntity(qrrHeaderDTO);
                    newQRRHEntity.Date = qrrHeader.Date;
                    newQRRHEntity.Number = qrrHeader.Number;
                    newQRRHEntity.QuoteRequestHeaderId = qrrHeader.QuoteRequestHeaderId;
                    newQRRHEntity.ProviderId = qrrHeader.ProviderId;
                    //newQRRHEntity.Bonus = qrrHeader.Bonus;
                    newQRRHEntity.IVA = 1;
                    //newQRRHEntity.Total = qrrHeader.Total;
                    newQRRHEntity.Comments = qrrHeader.Comments;
                }

                foreach (var qrrd in qrrHeaderDTO.QuoteRequestResponseDetails)
                {
                    if (qrrd.AlternativeProductId != null && qrrd.AlternativeProductId.HasValue)
                    {
                        var newQRRDEntity = new QuoteRequestResponseDetail();
                        if (qrrd.Available)
                        {
                            // Hay disponibilidad parcial, se agrega otro producto para completar la cantidad faltante del producto original.
                            if (qrrd.OriginalProviderQuantity - qrrd.ProviderQuantity > 0)
                            {
                                newQRRDEntity.ProviderQuantity = qrrd.OriginalProviderQuantity - qrrd.ProviderQuantity;
                            }
                            else
                            {
                                newQRRDEntity.ProviderQuantity = 0;
                            }
                            if (qrrd.OriginalPriceQuantity - qrrd.PriceQuantity > 0)
                            {
                                newQRRDEntity.PriceQuantity = qrrd.OriginalPriceQuantity - qrrd.PriceQuantity;
                            }
                            else
                            {
                                newQRRDEntity.PriceQuantity = 0;
                            }
                            newQRRDEntity.MissingProductId = null;
                            newQRRDEntity.Available = true;
                            newQRRDEntity.AlternativeProductId = qrrd.AlternativeProductId.Value;
                            newQRRDEntity.ProviderUnitMeasureId = qrrd.ProviderUnitMeasureId;
                            newQRRDEntity.PriceUnitMeasureId = qrrd.PriceUnitMeasureId;
                            newQRRHEntity.QuoteRequestResponseDetails.Add(newQRRDEntity);
                        }
                        else
                        {
                            // No hay nada de disponibilidad, se reemplaza completamente por otro producto.
                            newQRRDEntity.MissingProductId = null;
                            newQRRDEntity.Available = true;
                            newQRRDEntity.AlternativeProductId = qrrd.AlternativeProductId.Value;
                            newQRRDEntity.ProviderQuantity = qrrd.ProviderQuantity;
                            newQRRDEntity.ProviderUnitMeasureId = qrrd.ProviderUnitMeasureId;
                            newQRRDEntity.PriceQuantity = qrrd.PriceQuantity;
                            newQRRDEntity.PriceUnitMeasureId = qrrd.PriceUnitMeasureId;
                            newQRRDEntity.MoneyType = qrrd.MoneyType;
                            newQRRDEntity.UnitPrice = qrrd.UnitPrice;
                            newQRRDEntity.Bonus = qrrd.Bonus;
                            newQRRDEntity.Total = qrrd.Total;
                            newQRRHEntity.QuoteRequestResponseDetails.Add(newQRRDEntity);
                        }
                    }
                }
                if (newQRRHEntity.QuoteRequestResponseDetails.Count > 0)
                {
                    var newQRRHDto = MapToDto(newQRRHEntity);

                    await MapDetails(newQRRHDto);

                    newQRRHEntity = MapToEntity(newQRRHDto);

                    await _unitOfWork.QuoteRequestResponseHeaderRepository.CreateAsync(newQRRHEntity);
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateResponseDateAndPurchaseStateOnDelete(int qrrdId)
        {
            try
            {
                var getQRRDById = _unitOfWork.QuoteRequestResponseDetailRepository.FindAsync(new QuoteRequestResponseDetailWithAllSpecifications(qrrdId));
                if (getQRRDById.Result.Count() > 0)
                {
                    var qRRDEntity = getQRRDById.Result.First();
                    var getQRHById = _unitOfWork.QuoteRequestHeaderRepository.FindAsync(new QuoteRequestHeaderSpecification(qRRDEntity.QuoteRequestResponseHeader.QuoteRequestHeaderId));
                    if (getQRHById.Result.Count() > 0)
                    {
                        var qrhDTO = _mapper.Map<QuoteRequestHeaderDTO>(getQRHById.Result.First());
                        qrhDTO.ResponseDate = null;

                        foreach (var qrd in qrhDTO.QuoteRequestDetails)
                        {
                            var qrdDTO = _mapper.Map<QuoteRequestDetailDTO>(qrd);
                            qrdDTO.PurchaseStateId = 2;
                            var qrdEntity = _mapper.Map<QuoteRequestDetail>(qrdDTO);
                            _unitOfWork.QuoteRequestDetailRepository.Update(qrdEntity);
                        }

                        var qrhEntity = _mapper.Map<QuoteRequestHeader>(qrhDTO);
                        _unitOfWork.QuoteRequestHeaderRepository.Update(qrhEntity);
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
