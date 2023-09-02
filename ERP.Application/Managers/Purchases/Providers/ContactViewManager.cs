using AutoMapper;
using ERP.Application.DTOs.Entities.Purchases.Providers;
using ERP.Application.Interfaces.Services.Purchases.Providers;
using ERP.Application.Interfaces.ViewManagers.Purchases.Providers;
using ERP.Domain.Entities.Purchases.Providers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Application.Managers.Purchases.Providers
{
    public class ContactViewManager : ViewManager<Contact, ContactDTO>, IContactViewManager
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactViewManager(IContactService contactService, IMapper mapper) : base(contactService, mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<Contact>> GetContactsByProviderId(int id)
        {
            var contacts = await _contactService.GetByProviderId(id);
            var contactsDTO = _mapper.Map<IReadOnlyList<ContactDTO>>(contacts);
            return contactsDTO;
        }
    }
}
