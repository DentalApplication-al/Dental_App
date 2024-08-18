using DentalDomain.Services;

namespace DentalApplication.Common.Interfaces.IRepositories
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<List<Service>> GetClinicServices(Guid clinic_id);
        Task<Service> GetServiceById(Guid clinic_id, Guid service_id);
        Task<List<Service>> GetServiceByIds(List<Guid>? service_id);
    }
}
