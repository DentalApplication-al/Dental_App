using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using DentalDomain.Users.Clients;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DentalInfrastructure.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DentalContext context) : base(context)
        {
        }

        public async Task<List<ClientListResponse>> GetAllClinicCLients(Guid clinicId)
        {
            try
            {
                var clientss = await _context.Clients
                .Where(a => a.ClinicId == clinicId)
                .Select(a => new ClientListResponse
                {
                    email = a.Email,
                    first_name = a.FirstName,
                    last_name = a.LastName,
                    id = a.Id,
                    phone = a.Phone,
                    registered_date = a.CreatedOn,
                    //last_appointment = a.Appointments.MaxBy(a => a.EndDate).StartDate,
                })
                .ToListAsync();

            }
            catch (Exception)
            {

                throw;
            }
            var clients = await _context.Clients
                .Where(a => a.ClinicId == clinicId)
                .Select(a => new ClientListResponse
                {
                    email = a.Email,
                    first_name = a.FirstName,
                    last_name = a.LastName,
                    id = a.Id,
                    phone = a.Phone,
                    registered_date = a.CreatedOn,
                    //last_appointment = a.Appointments.MaxBy(a => a.EndDate).StartDate,
                })
                .ToListAsync();
            return clients;
        }

        public async Task<Client> GetByIdAsync(Guid clientId, Guid clinicId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(a => a.Id == clientId && a.ClinicId == clinicId);
            return client;
        }
    }
}
