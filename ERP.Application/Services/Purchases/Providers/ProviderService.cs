using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.Providers
{
    public class ProviderService : GenericService<Provider, ProviderDTO>, IProviderService
    {
        public ProviderService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public override async Task<int> CreateAsync(ProviderDTO entityToCreateDTO)
        {
            await MapContacts(entityToCreateDTO);
            await MapSubsidiaries(entityToCreateDTO);
            await MapFinancialInfo(entityToCreateDTO);
            return await base.CreateAsync(entityToCreateDTO);
        }

        public override async Task<int> UpdateAsync(ProviderDTO entityDtoToUpdate)
        {
            await MapContacts(entityDtoToUpdate);
            await MapSubsidiaries(entityDtoToUpdate);
            await MapFinancialInfo(entityDtoToUpdate);
            return await base.UpdateAsync(entityDtoToUpdate);
        }

        public async Task MapContacts(ProviderDTO providerDTO)
        {
            IReadOnlyList<Contact> oldContacts = _unitOfWork.ContactRepository.GetAll().Result.Where(c => c.ProviderId == providerDTO.Id).ToList();

            // Create
            foreach (var contact in providerDTO.Contacts.Where(c => c.Id == 0))
            {
                await _unitOfWork.ContactRepository.CreateAsync(contact);
            }

            // Update
            foreach (var contact in providerDTO.Contacts.Where(c => c.Id != 0 && oldContacts.Any(oc => oc.Id == c.Id)))
            {
                _unitOfWork.ContactRepository.Update(contact);
            }

            // Delete
            foreach (var contact in oldContacts.Where(oc => !providerDTO.Contacts.Any(c => c.Id != 0 && c.Id == oc.Id)))
            {
                await _unitOfWork.ContactRepository.DeleteAsync(contact.Id);
            }
        }

        public async Task MapSubsidiaries(ProviderDTO providerDTO)
        {
            IReadOnlyList<Subsidiary> oldSubsidiaries = _unitOfWork.SubsidiaryRepository.GetAll().Result.Where(s => s.ProviderId == providerDTO.Id).ToList();

            // Create
            foreach (var subsidiary in providerDTO.Subsidiaries.Where(s => s.Id == 0))
            {
                await _unitOfWork.SubsidiaryRepository.CreateAsync(subsidiary);
            }

            // Update
            foreach (var subsidiary in providerDTO.Subsidiaries.Where(s => s.Id != 0 && oldSubsidiaries.Any(os => os.Id == s.Id)))
            {
                _unitOfWork.SubsidiaryRepository.Update(subsidiary);
            }

            // Delete
            foreach (var subsidiary in oldSubsidiaries.Where(os => !providerDTO.Subsidiaries.Any(s => s.Id != 0 && s.Id == os.Id)))
            {
                await _unitOfWork.SubsidiaryRepository.DeleteAsync(subsidiary.Id);
            }
        }

        public async Task MapFinancialInfo(ProviderDTO providerDTO)
        {
            IReadOnlyList<FinancialInformation> oldFinancialInfo = _unitOfWork.FinancialInformationRepository.GetAll().Result.Where(f => f.ProviderId == providerDTO.Id).ToList();

            // Create
            foreach (var financialInfo in providerDTO.FinancialInformations.Where(f => f.Id == 0))
            {
                await _unitOfWork.FinancialInformationRepository.CreateAsync(financialInfo);
            }

            // Update
            foreach (var financialInfo in providerDTO.FinancialInformations.Where(f => f.Id != 0 && oldFinancialInfo.Any(of => of.Id == f.Id)))
            {
                _unitOfWork.FinancialInformationRepository.Update(financialInfo);
            }

            // Delete
            foreach (var financialInfo in oldFinancialInfo.Where(of => !providerDTO.FinancialInformations.Any(f => f.Id != 0 && f.Id == of.Id)))
            {
                await _unitOfWork.FinancialInformationRepository.DeleteAsync(financialInfo.Id);
            }
        }
    }
}