using DentalApplication.User.ClientController.DTO;
using DentalDomain.Users.Clients;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetByIdAsync(Guid clientId, Guid clinicId);
        Task<List<ClientListResponse>> GetAllClinicCLients(Guid clinicId);
    }
}
