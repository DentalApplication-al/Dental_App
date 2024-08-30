using DentalApplication.User.ClientController.DTO;
using DentalDomain.Users.Clients;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetByIdAsync(Guid clinicId, Guid clientId);
        Task<PaginatedResponse<ListClient>> GetPaginatedClients(Guid clinicId, int take, int page, string? search);
        Task<bool> DeleteAsync(Client client);
    }
}
