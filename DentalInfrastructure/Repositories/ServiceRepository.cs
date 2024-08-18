using DentalApplication.Common.Interfaces.IRepositories;
using DentalDomain.Services;
using DentalInfrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DentalInfrastructure.Repositories
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(DentalContext context) : base(context)
        {
        }

        public async Task<List<Service>> GetClinicServices(Guid clinic_id)
        {
            return await _context.Services
                .Include(a => a.ServiceStaff)
                .Where(a => a.ClinicId == clinic_id)
                .ToListAsync();
        }

        public async Task<Service> GetServiceById(Guid clinic_id, Guid service_id)
        {
            var service = await _context.Services
                .FirstOrDefaultAsync(a => a.Id == service_id && a.ClinicId == clinic_id);
            throw new NotImplementedException();
        }

        public async Task<List<Service>> GetServiceByIds(List<Guid>? service_id)
        {
            var result = await _context.Services
                .Where(a => service_id.Contains(a.Id))
                .ToListAsync();
            return result;
        }
    }
}
