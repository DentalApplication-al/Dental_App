using DentalApplication.Common;
using DentalApplication.Common.Interfaces.IRepositories;
using DentalApplication.ServicesController.DTO;
using DentalApplication.User.StaffController.DTO;
using DentalDomain.Clinics;
using DentalDomain.Services;
using DentalDomain.Users.Staffs;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DentalInfrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(DentalContext context) : base(context)
        {
        }

        public async Task<bool> DeleteService(Guid clinic_id, Guid service_id)
        {
            var service = await _context.Services
                .Where(a => a.Id == service_id && a.ClinicId == clinic_id)
                .FirstOrDefaultAsync();
            if (service == null) 
                return false;

            try
            {
                _context.Remove(service);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Service> GetFullServiceById(Guid clinic_id, Guid service_id)
        {
            var service = await _context.Services
                .Where(a => a.ClinicId == clinic_id && a.Id == service_id)
                .Include(a => a.ServiceStaff)
                .FirstOrDefaultAsync();

            return service;
        }

        public async Task<PaginatedResponse<ListService>> GetPaginatedClinicServices(Guid clinic_id, int take, int page, string? search)
        {
            // Calculate the total number of elements
            var totalElements = await _context.Services.CountAsync(a => a.ClinicId == clinic_id && a.Name.Contains(search ?? ""));

            // Calculate the total number of pages
            var totalPages = (int)Math.Ceiling(totalElements / (double)take);

            // Fetch the paginated staff list
            var staff = await _context.Services
                .Where(a => a.ClinicId == clinic_id && a.Name.Contains(search ?? ""))
                .Skip((page - 1) * take)
                .Take(take)
                .Select(a => new ListService
                {
                    id = a.Id,
                    name = a.Name,
                    price = a.Price,
                    doctors = a.ServiceStaff.Select(a => $"Dr.{a.FirstName} {a.LastName}").ToList(),
                }).ToListAsync();

            // Populate the PaginatedResponse object
            var result = new PaginatedResponse<ListService>
            {
                data = staff,
                pageNumber = page,
                pageSize = take,
                totalElements = totalElements,
                totalPages = totalPages
            };

            return result;
        }

        public async Task<ServiceById> GetServiceById(Guid clinic_id, Guid service_id)
        {
            var service = await _context.Services
                .Where(a => a.Id == service_id && a.ClinicId == clinic_id)
                .Include(a => a.ServiceStaff)
                .Select(a => new ServiceById()
                {
                    id = a.Id,
                    name = a.Name,
                    price = a.Price,
                    duration = a.Duration,
                    description = a.Description,
                    doctors = a.ServiceStaff
                        .Select(a => new ListStaff
                        {
                            id = a.Id,
                            first_name = a.FirstName,
                            last_name = a.LastName,
                        }).ToList()
                }).FirstOrDefaultAsync();
            return service;
        }

        public async Task<List<Service>> GetServiceByIds(Guid clinic_id, List<Guid>? service_id)
        {
            var result = await _context.Services
                .Where(a => service_id.Contains(a.Id) && a.ClinicId == clinic_id)
                .ToListAsync();
            return result;
        }
    }
}
