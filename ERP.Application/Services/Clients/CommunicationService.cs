using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Domain.Entities.Clients;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Clients
{
    public class CommunicationService : GenericService<Communication, CommunicationDTO>, ICommunicationService
    {
        public CommunicationService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(CommunicationDTO entityToCreateDTO)
        {
            await MapConsultedProducts(entityToCreateDTO);
            return await base.CreateAsync(entityToCreateDTO);
        }

        public override async Task<int> UpdateAsync(CommunicationDTO entityDtoToUpdate)
        {
            await MapConsultedProducts(entityDtoToUpdate);
            return await base.UpdateAsync(entityDtoToUpdate);
        }

        public async Task DeleteAllConsultedProducts(CommunicationDTO entityToDeleteCP)
        {
            IReadOnlyList<ConsultedProduct> consultedProducts = _unitOfWork.ConsultedProductRepository.GetAll().Result.Where(x => x.CommunicationId == entityToDeleteCP.Id).ToList();

            foreach (var consultedProduct in entityToDeleteCP.ConsultedProducts)
            {
                await _unitOfWork.ConsultedProductRepository.DeleteAsync(consultedProduct.Id);
            }
        }

        public async Task DeleteConsultedProducts(CommunicationDTO entityToUpdate)
        {
            IReadOnlyList<ConsultedProduct> consultedProducts = _unitOfWork.ConsultedProductRepository.GetAll().Result.Where(x => x.CommunicationId == entityToUpdate.Id).ToList();

            if(!entityToUpdate.ConsultedProducts.Equals(consultedProducts))
            {
                foreach (var consultedProduct in consultedProducts)
                {
                    if(!(entityToUpdate.ConsultedProducts.Contains(consultedProduct)))
                    {
                        await _unitOfWork.ConsultedProductRepository.DeleteAsync(consultedProduct.Id);
                    }
                }
            }
        }

        public async Task MapConsultedProducts(CommunicationDTO entityToCreateDTO)
        {
            IReadOnlyList<ConsultedProduct> oldConsultedProducts = _unitOfWork.ConsultedProductRepository.GetAll().Result.Where(x => x.CommunicationId == entityToCreateDTO.Id).ToList();

            // Create
            foreach (var consultedProduct in entityToCreateDTO.ConsultedProducts.Where(x => x.Id == 0))
            {
                await _unitOfWork.ConsultedProductRepository.CreateAsync(consultedProduct);
            }

            // Update
            if(!entityToCreateDTO.AnotherReason)
            {
                foreach (var consultedProduct in entityToCreateDTO.ConsultedProducts.Where(c => c.Id != 0 && oldConsultedProducts.Any(oc => oc.Id == c.Id)))
                {
                    _unitOfWork.ConsultedProductRepository.Update(consultedProduct);
                }
            }
            else
            {
                foreach (var consultedProduct in entityToCreateDTO.ConsultedProducts)
                {
                    await _unitOfWork.ConsultedProductRepository.DeleteAsync(consultedProduct.Id); //Si està seleccionado AnotherReason, borra todos los productos
                }
            }

            //Update
            //foreach (var oldConsultedProduct in oldConsultedProducts)
            //{
            //    bool Ok = false;

            //    foreach (var consultedProduct in entityToCreateDTO.ConsultedProducts)
            //    {
            //        if(consultedProduct.Id == oldConsultedProduct.Id)
            //        {
            //            Ok = true;
            //        }
            //    }
            //    if(Ok)
            //    {
            //        _unitOfWork.ConsultedProductRepository.Update(oldConsultedProduct);
            //    }
            //    else
            //    {
            //        await _unitOfWork.ConsultedProductRepository.DeleteAsync(oldConsultedProduct.Id);
            //    }
            //}

            // Delete
            foreach (var consultedProduct in oldConsultedProducts.Where(ocp => !entityToCreateDTO.ConsultedProducts.Any(x => x.Id != 0 && x.Id == ocp.Id)))
            {
                await _unitOfWork.ConsultedProductRepository.DeleteAsync(consultedProduct.Id);
            }


        }
    }
}
