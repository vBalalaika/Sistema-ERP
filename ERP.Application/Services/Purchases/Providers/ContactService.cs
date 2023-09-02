using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Services.Purchases.Providers
{
    public class ContactService : GenericService<Contact, ContactDTO>, IContactService
    {
        public ContactService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public Task<IReadOnlyList<Contact>> GetByProviderId(int id)
        {
            var contacts = _unitOfWork.ContactRepository.GetByProviderIdAsync(id);
            return contacts;
        }
    }
}
