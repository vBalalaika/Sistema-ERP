using AutoMapper;
using ERP.Application.DTOs.Entities.Clients;
using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Services.Clients;
using ERP.Domain.Entities.Clients;
using System.Threading.Tasks;

namespace ERP.Application.Services.Clients
{
    public class ClientService : GenericService<Client, ClientDTO>, IClientService
    {
        public ClientService(IUnitOfWork unitOfWOrk, IMapper mapper) : base(unitOfWOrk, mapper)
        {

        }

        public async Task<Client> GetByIdLazyLoad(int id)
        {
            return await _unitOfWork.ClientRepository.GetByIdLazyLoadAsync(id);
        }

        public virtual async Task<int> DeactivateAsync(int id)
        {
            var entityToDeactivate = await _unitOfWork.ClientRepository.GetByIdAsync(id);

            entityToDeactivate.IsActive = false;

            _unitOfWork.ClientRepository.Update(entityToDeactivate);
            await _unitOfWork.Commit();
            return id;
        }
    }
}
