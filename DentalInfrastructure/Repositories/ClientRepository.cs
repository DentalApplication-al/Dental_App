using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.User.ClientController.DTO;
using DentalDomain.Users.Clients;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;

namespace DentalInfrastructure.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(DentalContext context) : base(context)
        {
        }

        public async Task<PaginatedResponse<ListClient>> GetPaginatedClients(Guid clinicId, int take, int page, string? search)
        {
            var totalElements = await _context.Clients.CountAsync(a => a.ClinicId == clinicId && a.FirstName.Contains(search?? ""));

            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            var clients = await _context.Clients
                .Where(a => a.ClinicId == clinicId)
                .Include(a => a.Appointments)
                .Select(a => new ListClient
                {
                    email = a.Email,
                    first_name = a.FirstName,
                    last_name = a.LastName,
                    id = a.Id,
                    phone = a.Phone,
                    registered_date = DateOnly.FromDateTime(a.CreatedOn),
                    last_appointment = a.Appointments.Count > 0 ? a.Appointments.Max(a => a.StartDate) : null,
                }).ToListAsync();

            var response = new PaginatedResponse<ListClient>()
            {
                data = clients,
                pageNumber = page,
                pageSize = take,
                totalElements = totalElements,
                totalPages = totalPages,
            };
            return response;
        }

        public async Task<Client> GetByIdAsync(Guid clinicId, Guid clientId)
        {
            var client = await _context.Clients
                .FirstOrDefaultAsync(a => a.Id == clientId && a.ClinicId == clinicId);
            return client;
        }

        public async Task<bool> DeleteAsync(Client client)
        {
            try
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
