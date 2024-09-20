using DentalApplication.AppointmentController.DTO;
using DentalApplication.User.ClientController.DTO;
using DentalDomain.Users.Clients;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        Task<Client> GetByIdAsync(Guid clinicId, Guid clientId);
        Task<PaginatedResponse<ListClient>> GetPaginatedClients(Guid clinicId, int take, int page, string? search);
        Task<bool> DeleteAsync(Client client);
        Task<ClientResponse> GetClientDetails(Guid clinicId, Guid clientId);

        Task<PaginatedResponse<ListAppointment>> GetClientAppointments(Guid clientId, Guid clinicId, int page, int take, string? search, bool history);

    }
}
