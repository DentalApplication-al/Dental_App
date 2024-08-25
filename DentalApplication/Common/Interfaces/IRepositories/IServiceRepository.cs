using DentalApplication.ServicesController.DTO;
using DentalDomain.Services;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<PaginatedResponse<ListService>> GetPaginatedClinicServices(Guid clinic_id, int take, int page, string? search);
        Task<ServiceById> GetServiceById(Guid clinic_id, Guid service_id);
        Task<Service> GetFullServiceById(Guid clinic_id, Guid service_id);
        Task<List<Service>> GetServiceByIds(Guid clinic_id, List<Guid>? service_id);
        Task<bool> DeleteService(Guid clinic_id, Guid service_id);
    }
}
