using DentalApplication.AppointmentController.DTO;
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
            var totalElements = await _context.Clients.CountAsync(a => a.ClinicId == clinicId && (a.FirstName.Contains(search ?? "") || a.LastName.Contains(search ?? "")));

            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            var clients = await _context.Clients
                .Where(a => a.ClinicId == clinicId && (a.FirstName.Contains(search ?? "") || a.LastName.Contains(search ?? "")))
                .Include(a => a.Appointments)
                .Select(a => new ListClient
                {
                    email = a.Email,
                    first_name = a.FirstName,
                    last_name = a.LastName,
                    id = a.Id,
                    phone = a.Phone,
                    registered_date = a.CreatedOn.ToString("dd:MM:yyyy"),
                    last_appointment = a.Appointments.Count > 0 ? a.Appointments.Max(a => a.StartDate).ToString("dd:MM:yyyy:HH:mm") : null,
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

        public async Task<ClientResponse> GetClientDetails(Guid clinicId, Guid clientId)
        {
            var client = await _context.Clients
                .Where(a => a.ClinicId == clinicId && a.Id == clientId)
                .Select(a => new ClientResponse 
                {
                     first_name = a.FirstName,
                     last_name = a.LastName,
                     birthday = a.Birthday.Value.ToString("dd/MM/yyyy"),
                     email = a.Email,
                     phone = a.Phone,
                     registered_date = a.CreatedOn.ToString("dd/MM/yyyy"),
                     upcoming = a.Appointments.Count(b => b.StartDate > DateTime.UtcNow),
                     past = a.Appointments.Count(b => b.StartDate < DateTime.UtcNow)
                }).FirstOrDefaultAsync();


            return client;
        }

        public async Task<PaginatedResponse<ListAppointment>> GetClientAppointments(Guid clientId, Guid clinicId, int page, int take, string? search, bool history)
        {
            if (history)
            {
                return await GetClientPastAppointments(clientId, clinicId, page, take, search);
            }
            else
            {
                return await GetClientUpcomingAppointments(clientId, clinicId, page, take, search);
            }
        }

        private async Task<PaginatedResponse<ListAppointment>> GetClientPastAppointments(Guid clientId, Guid clinicId, int page, int take, string? search)
        {
            var date = DateTime.Now;
            
            var totalElements = await _context.Clients
                .Where(a => a.ClinicId == clinicId && a.Id == clientId)
                .Include(b => b.Appointments.Where(a => a.StartDate < date))
                .SelectMany(c => c.Appointments.Where (a => a.StartDate < date))
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            var appointments = await _context.Clients
                .Where(a => a.Id == clientId && a.ClinicId == clinicId)
                .Include(a => a.Appointments.Where(a => a.StartDate < date)).ThenInclude(a => a.Client)
                .SelectMany(a => a.Appointments.Where(a => a.StartDate < date))
                .Skip((page - 1) * take)
                .Take(take)
                .Select(b => new ListAppointment
                {
                    client = $"{b.Client.FirstName} {b.Client.LastName}",
                    id = b.Id,
                    doctor = $"{b.Doctor.FirstName} {b.Doctor.LastName}",
                    treatment = b.Service.Name,
                    date = $"{b.StartDate.Day}-{b.StartDate.Month}-{b.StartDate.Year}",
                    time = $"{b.StartDate:HH:mm} - {b.EndDate:HH:mm}",
                }).ToListAsync();

            var result = new PaginatedResponse<ListAppointment>
            {
                data = appointments,
                totalPages = totalPages,
                pageNumber = page,
                totalElements = totalElements,
                pageSize = take,
            };
            return result;
        }

        private async Task<PaginatedResponse<ListAppointment>> GetClientUpcomingAppointments(Guid clientId, Guid clinicId, int page, int take, string? search)
        {
            var date = DateTime.Now;

            var totalElements = await _context.Clients
                .Where(a => a.ClinicId == clinicId && a.Id == clientId)
                .Include(b => b.Appointments.Where(a => a.StartDate > date))
                .SelectMany(c => c.Appointments.Where(a => a.StartDate > date))
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            var appointments = await _context.Clients
                .Where(a => a.Id == clientId && a.ClinicId == clinicId)
                .Include(a => a.Appointments.Where(a => a.StartDate > date)).ThenInclude(a => a.Client)
                .SelectMany(a => a.Appointments.Where(a => a.StartDate > date))
                .Skip((page - 1) * take)
                .Take(take)
                .Select(b => new ListAppointment
                {
                    client = $"{b.Client.FirstName} {b.Client.LastName}",
                    id = b.Id,
                    doctor = $"{b.Doctor.FirstName} {b.Doctor.LastName}",
                    treatment = b.Service.Name,
                    date = $"{b.StartDate.Day}-{b.StartDate.Month}-{b.StartDate.Year}",
                    time = $"{b.StartDate:HH:mm} - {b.EndDate:HH:mm}",
                }).ToListAsync();

            var result = new PaginatedResponse<ListAppointment>
            {
                data = appointments,
                totalPages = totalPages,
                pageNumber = page,
                totalElements = totalElements,
                pageSize = take,
            };
            return result;
        }
    }
}
